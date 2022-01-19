using JiangH.API;
using System.ComponentModel;

namespace JiangH.Kernels.Entities
{
    public class Estate : Point, IEstate
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string name { get; set; }

        public override void OnRelationAdd(IRelation relation)
        {

        }

        public override void OnRelationRemove(IRelation relation)
        {

        }
    }

}
