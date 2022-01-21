using JiangH.API;
using System.ComponentModel;

namespace JiangH.Kernels.Entities
{
    public class Estate : Point, IEstate
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string name { get; set; }
        
        public Estate()
        {
            components.Add(new MoneyProducter());
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

        public MoneyProducter()
        {
            total = 10;
        }
    }
}
