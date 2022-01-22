using JiangH.API;
using System.ComponentModel;

namespace JiangH.Kernels.Entities
{
    public class Estate : Point, IEstate
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public Estate()
        {
            components.Add(new MoneyProducter(this));
        }

        public override void OnRelationAdd(IRelation relation)
        {

        }

        public override void OnRelationRemove(IRelation relation)
        {

        }
    }

    public class MoneyProducter : IMoneyProducter
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int total { get; set; }

        public IPoint owner { get; private set; }

        public MoneyProducter(IPoint owner)
        {
            total = 10;

            this.owner = owner;
        }
    }
}
