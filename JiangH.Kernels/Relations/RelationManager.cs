using JiangH.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace JiangH.Kernels.Relations
{
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
