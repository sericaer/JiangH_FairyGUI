using System;
using JiangH.API;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using JiangH.Kernels.Entities;

namespace JiangH.Kernels.Systems
{
    class MoneyProductSystem : ISystem
    {
        private HashSet<IMoneyContainer> moneyContainers = new HashSet<IMoneyContainer>();

        public MoneyProductSystem()
        {

        }

        public void OnComponentAdd(IComponent component)
        {

        }

        public void OnComponentRemove(IComponent component)
        {
            
        }

        public void OnDaysInc()
        {
            foreach(var moneyContainer in moneyContainers)
            {
                moneyContainer.count += moneyContainer.producters.Sum(x => x.total);
            }
        }

        public void OnRelationAdd(IRelation relation)
        {
            var mRelation = relation.GetMoneyProductRelation();
            if (mRelation.container != null && mRelation.producter != null)
            {
                mRelation.container.producters.Add(mRelation.producter);
                moneyContainers.Add(mRelation.container);
            }
        }

        public void OnRelationRemove(IRelation relation)
        {
            var mRelation = relation.GetMoneyProductRelation();
            if (mRelation.container != null && mRelation.producter != null)
            {
                mRelation.container.producters.Remove(mRelation.producter);
                moneyContainers.Add(mRelation.container);
            }
        }
    }
}
