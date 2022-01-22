using JiangH.API;
using JiangH.Kernels.Components;
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
}
