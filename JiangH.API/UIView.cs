using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reflection;

namespace JiangH.API
{

    public class NewWindowCmd
    {
        public string name { get; set; }
        public object param { get; set; }
    }

    public abstract class UIView : INotifyPropertyChanged, IDisposable
    {
#pragma warning disable 0067 
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067
        public bool isRemoved { get; set; }

        private CompositeDisposable comDisposable;

        public UIView()
        {
            isRemoved = false;
            comDisposable = new CompositeDisposable();
        }

        public void OnClickClose()
        {
            isRemoved = true;
        }

        public void BindOneWay<TFrom, TPropertyType, TTarget>(TFrom fromObject, Expression<Func<TFrom, TPropertyType>> fromProperty,
            TTarget targetObject, Expression<Func<TTarget, TPropertyType>> toProperty)
            where TFrom : class, INotifyPropertyChanged
        {
            var dis = fromObject.BindOneWay(targetObject, fromProperty, toProperty);
            comDisposable.Add(dis);
        }

        public void BindTwoWay<TFrom, TProperty, TTarget>(TFrom fromObject, Expression<Func<TFrom, TProperty>> fromProperty,
            TTarget targetObject, Expression<Func<TTarget, TProperty>> toProperty)
            where TFrom : class, INotifyPropertyChanged
            where TTarget : class, INotifyPropertyChanged
        {
            var dis = fromObject.BindTwoWay(fromProperty, targetObject, toProperty);
            comDisposable.Add(dis);
        }

        public virtual void Dispose()
        {
            comDisposable.Dispose();
        }
    }

    public class UIBindingAttribute : Attribute
    {
        public readonly string label;
        public UIBindingAttribute(string label)
        {
            this.label = label;
        }
    }

    public class UISceneBind : UIBindingAttribute
    {
        public UISceneBind(string label) : base(label)
        {
        }
    }

    public class UITextBinding : UIBindingAttribute
    {
        public enum Attrib
        {
            text
        }

        public readonly Attrib attrib;

        public UITextBinding(string label, Attrib attrib) : base(label)
        {
            this.attrib = attrib;
        }
    }

    public class UIButtonBinding : UIBindingAttribute
    {
        public enum Attrib
        {
            isSelected,
            TriggerCmd
        }

        public readonly Attrib attrib;

        public UIButtonBinding(string label, Attrib attrib) : base(label)
        {
            this.attrib = attrib;
        }
    }

    public class UIToggleGroupBinding : UIBindingAttribute
    {
        public enum Attrib
        {
            Single,
            Multi
        }

        public readonly Attrib attrib;

        public UIToggleGroupBinding(string label, Attrib attrib) : base(label)
        {
            this.attrib = attrib;
        }
    }

    public interface ICommand
    {
        void Exec();
    }

    public interface INewWindowCmd : ICommand
    {
        string newWindowName { get; }
        object param { get;  }
    }
}
