using JiangH.API;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using ReactiveMarbles.PropertyChanged;

namespace JiangH.Kernels.Entities
{
    public class Person : Entity, IPerson
    {
        public int age { get; set; }

        public IMoneyContainer money { get; private set; }

        public ReadOnlyObservableCollection<IEstate> estates { get; set; }
        private ObservableCollection<IEstate> _estates;

        public IPersonEngine engine { get; private set; }


        public Person(GameSession session) : base(session)
        {
            name = "Test1";
            age = 20;

            _estates = new ObservableCollection<IEstate>();
            estates = new ReadOnlyObservableCollection<IEstate>(_estates);

            money = new MoneyContainer(this);
            components.Add(money);

            engine = new PersonEngine();
        }

        public void AddEstate(IEstate estate)
        {
            session.relationManager.Add(this, estate, null);
        }

        public void OnDaysInc()
        {
            engine.spendItems[0].realValue++;
        }

        public void RemoveEstate(IEstate estate)
        {
            session.relationManager.Remove(this, estate);
        }

        public override void OnRelationAdd(IRelation relation)
        {
            var peer = relation.GetPeer(this);
            if (peer is IEstate estate)
            {
                _estates.Add(estate);
            }

            var engineSpend = peer.GetComponent<IPersonEngineSpend>();
            if (engineSpend != null)
            {
                engine.spendItems.Add(engineSpend);
            }
        }

        public override void OnRelationRemove(IRelation relation)
        {
            var peer = relation.GetPeer(this);
            if (peer is IEstate estate)
            {
                _estates.Remove(estate);
            }

            var engineSpend = peer.GetComponent<IPersonEngineSpend>();
            if (engineSpend != null)
            {
                engine.spendItems.Remove(engineSpend);
            }
        }
    }
}
