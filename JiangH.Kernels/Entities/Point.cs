using JiangH.API;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace JiangH.Kernels.Entities
{
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

}
