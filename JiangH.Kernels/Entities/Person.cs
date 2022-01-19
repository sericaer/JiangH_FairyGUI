using JiangH.API;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;

namespace JiangH.Kernels.Entities
{
    public class Person : Entity, IPerson
    {
#pragma warning disable 0067 // No "Never used" warning
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public string name { get ; set; }
        public int age { get; set; }
        public int money { get; set; }

        public ReadOnlyObservableCollection<IEstate> estates { get; set; }

        private ObservableCollection<IEstate> _estates;

        public Person(GameSession session) : base(session)
        {
            name = "Test1";
            age = 20;

            _estates = new ObservableCollection<IEstate>();

            estates = new ReadOnlyObservableCollection<IEstate>(_estates);
        }

        public void AddEstate(IEstate estate)
        {
            session.relationManager.Add(this, estate, null);
        }

        public void OnDaysInc()
        {
            
        }

        public void RemoveEstate(IEstate estate)
        {
            session.relationManager.Remove(this, estate);
        }

        public override void OnRelationAdd(IRelation relation)
        {
            if (relation.p1 is IEstate estate1)
            {
                _estates.Add(estate1);
            }
            if (relation.p2 is IEstate estate2)
            {
                _estates.Add(estate2);
            }
        }

        public override void OnRelationRemove(IRelation relation)
        {
            if (relation.p1 is IEstate estate1)
            {
                _estates.Remove(estate1);
            }
            if (relation.p2 is IEstate estate2)
            {
                _estates.Remove(estate2);
            }
        }
    }

}
