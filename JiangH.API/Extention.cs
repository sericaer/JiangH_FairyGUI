using ReactiveMarbles.PropertyChanged;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;


namespace JiangH.API
{
    public static class BindExtensions
    {
        public static IDisposable BindOneWay<TFrom, TPropertyType, TTarget>(
        this TFrom fromObject,
        TTarget targetObject,
        Expression<Func<TFrom, TPropertyType>> fromProperty,
        Expression<Func<TTarget, TPropertyType>> toProperty,
        IScheduler scheduler = null)
        where TFrom : class, INotifyPropertyChanged
        {
            if (fromObject is null)
            {
                throw new ArgumentNullException(nameof(fromObject));
            }

            var setter = toProperty.GetSetter();
            var func = fromProperty.Compile();
            setter.Invoke(targetObject, func.Invoke(fromObject));

            return OneWayBindImplementation(targetObject, fromObject.WhenChanged(fromProperty), toProperty, scheduler);
        }

        public static IDisposable BindTwoWay<TFrom, TProperty, TTarget>(
        this TFrom fromObject,
        Expression<Func<TFrom, TProperty>> fromProperty,
        TTarget targetObject,
        Expression<Func<TTarget, TProperty>> toProperty,
        IScheduler scheduler = null)
        where TFrom : class, INotifyPropertyChanged
        where TTarget : class, INotifyPropertyChanged
        {
            var hostObs = fromObject.WhenChanged(fromProperty);
            var targetObs = targetObject.WhenChanged(toProperty)
                .Skip(1); // We have the host to win first off.

            return BindTwoWayImplementation(fromObject, targetObject, hostObs, targetObs, fromProperty, toProperty, scheduler);
        }

        private static IDisposable BindTwoWayImplementation<TFrom, TFromProperty, TTarget, TTargetProperty>(
            TFrom fromObject,
            TTarget targetObject,
            IObservable<TTargetProperty> hostObs,
            IObservable<TFromProperty> targetObs,
            Expression<Func<TFrom, TFromProperty>> fromProperty,
            Expression<Func<TTarget, TTargetProperty>> toProperty,
            IScheduler scheduler)
        {
            if (hostObs is null)
            {
                throw new ArgumentNullException(nameof(hostObs));
            }

            if (toProperty is null)
            {
                throw new ArgumentNullException(nameof(toProperty));
            }

            if (fromProperty is null)
            {
                throw new ArgumentNullException(nameof(fromProperty));
            }


            if ((scheduler ?? ImmediateScheduler.Instance) != ImmediateScheduler.Instance)
            {
                hostObs = hostObs.ObserveOn(scheduler);
                targetObs = targetObs.ObserveOn(scheduler);
            }

            var setterTo = toProperty.GetSetter();
            var setterFrom = fromProperty.GetSetter();

            return new CompositeDisposable(
                hostObs.Subscribe(x => setterTo(targetObject, x)),
                targetObs.Subscribe(x => setterFrom(fromObject, x)));
        }

        private static IDisposable OneWayBindImplementation<TTarget, TPropertyType>(
            TTarget targetObject,
            IObservable<TPropertyType> hostObs,
            Expression<Func<TTarget, TPropertyType>> property,
            IScheduler scheduler)
        {
            if (hostObs is null)
            {
                throw new ArgumentNullException(nameof(hostObs));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }


            if ((scheduler ?? ImmediateScheduler.Instance) != ImmediateScheduler.Instance)
            {
                hostObs = hostObs.ObserveOn(scheduler);
            }

            var setter = property.GetSetter();

            return hostObs.Subscribe(x => setter(targetObject, x));
        }
    }

    internal static class ExpressionExtensions
    {
        private static readonly ConcurrentDictionary<string, object> _actionCache =
            new ConcurrentDictionary<string, object>();

        internal static List<MemberExpression> GetExpressionChain(this Expression expression)
        {
            var expressions = new List<MemberExpression>(16);

            var node = expression;

            while (node.NodeType != ExpressionType.Parameter)
            {
                switch (node.NodeType)
                {
                    case ExpressionType.MemberAccess:
                        var memberExpression = (MemberExpression)node;
                        expressions.Add(memberExpression);
                        node = memberExpression.Expression;
                        break;
                    default:
                        throw new NotSupportedException($"Unsupported expression type: '{node.NodeType.ToString()}'");
                }
            }

            expressions.Reverse();

            return expressions;
        }

        internal static Action<T, TProperty> GetSetter<T, TProperty>(this Expression<Func<T, TProperty>> expression)
            => (Action<T, TProperty>)_actionCache.GetOrAdd(
                $"{typeof(T).FullName}|{typeof(TProperty).FullName}|{expression}",
                _ =>
                {
                    var instanceParameter = expression.Parameters.Single();
                    var valueParameter = Expression.Parameter(typeof(TProperty), "value");

                    return Expression.Lambda<Action<T, TProperty>>(
                            Expression.Assign(expression.Body, valueParameter),
                            instanceParameter,
                            valueParameter)
                        .Compile();
                });
    }
}
