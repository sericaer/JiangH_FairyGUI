using JiangH.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH.Kernels.Systems
{
    class PersonEngineSystem : ISystem
    {
        private HashSet<IPersonEngineSpend> personEngines = new HashSet<IPersonEngineSpend>();

        public void OnComponentAdd(IComponent component)
        {

        }

        public void OnComponentRemove(IComponent component)
        {

        }

        public void OnDaysInc()
        {
            
        }

        public void OnRelationAdd(IRelation relation)
        {
            var mRelation = relation.GetMoneyProductRelation();
        }

        public void OnRelationRemove(IRelation relation)
        {
            var mRelation = relation.GetMoneyProductRelation();
        }
    }
}
