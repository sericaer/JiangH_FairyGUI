using JiangH.API;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace JiangH.Kernels.Entities
{
    public class Person : Entity, IPerson
    {
#pragma warning disable 0067 // No "Never used" warning
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public int age { get; set; }

        public IMoneyContainer money { get; private set; }

        public ReadOnlyObservableCollection<IEstate> estates { get; set; }
        
        private ObservableCollection<IEstate> _estates;

        public Person(GameSession session) : base(session)
        {
            name = "Test1";
            age = 20;

            _estates = new ObservableCollection<IEstate>();

            estates = new ReadOnlyObservableCollection<IEstate>(_estates);

            money = new MoneyContainer(this);
            components.Add(money);
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
            var peer = relation.GetPeer(this);
            if (peer is IEstate estate)
            {
                _estates.Add(estate);
            }
        }

        public override void OnRelationRemove(IRelation relation)
        {
            if (relation.GetPeer(this) is IEstate estate)
            {
                _estates.Remove(estate);
            }
        }
    }
}
