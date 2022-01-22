﻿using JiangH.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace JiangH.Kernels.Entities
{
    class Relation : IRelation
    {
        public IPoint p1 { get; set; }

        public IPoint p2 { get; set; }

        public Dictionary<string, object> attrib { get; set; }


        public Relation(IPoint p1, IPoint p2, Dictionary<string, object> attrib)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.attrib = attrib;
        }

        public IPoint GetPeer(IPoint point)
        {
            if (point == p1)
            {
                return p2;
            }
            else if (point == p2)
            {
                return p1;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }

    public abstract class Point : IPoint
    {
        public string name { get; set; }

        public ObservableCollection<IRelation> relations { get; private set; }
        public ObservableCollection<IComponent> components { get; private set; }

        public abstract void OnRelationAdd(IRelation relation);
        public abstract void OnRelationRemove(IRelation relation);

        public T GetComponent<T>() where T : IComponent
        {
            return (T)(components.First(x => x is T));
        }

        public Point()
        {
            relations = new ObservableCollection<IRelation>();
            components = new ObservableCollection<IComponent>();

            relations.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach(IRelation relation in e.NewItems)
                        {
                            OnRelationAdd(relation);
                        }
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        foreach (IRelation relation in e.OldItems)
                        {
                            OnRelationRemove(relation);
                        }
                        break;
                }
            };
        }
    }

    public class RelationManager : IRelationManager
    {
        private ObservableCollection<IRelation> all;

        public Action<IRelation> onRelationAdd { get; set; }
        public Action<IRelation> onRelationRemove { get; set; }

        public RelationManager()
        {
            all = new ObservableCollection<IRelation>();

            all.CollectionChanged += (sender, e) =>
            {
                switch(e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (IRelation relation in e.NewItems)
                        {
                            relation.p1.relations.Add(relation);
                            relation.p2.relations.Add(relation);

                            onRelationAdd?.Invoke(relation);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (IRelation relation in e.OldItems)
                        {
                            relation.p1.relations.Remove(relation);
                            relation.p2.relations.Remove(relation);

                            onRelationRemove?.Invoke(relation);
                        }
                        break;
                }
            };
        }

        public void Add(IPoint p1, IPoint p2, Dictionary<string, object> attrib)
        {
            var relation = new Relation(p1, p2, attrib);
            all.Add(relation);
        }

        public void Remove(IPoint p1, IPoint p2)
        {
            var relation = p1.relations.SingleOrDefault(x => x.p1 == p2 || x.p2 == p2);
            all.Remove(relation);
        }
    }

}
