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

        public void OnDaysInc()
        {
            foreach(var moneyContainer in moneyContainers)
            {
                moneyContainer.count += moneyContainer.producters.Sum(x => x.total);
            }
        }

        public void OnRelationAdd(IRelation relation)
        {
            var mRelation = GetMoneyProductRelation(relation);
            if (mRelation.container != null && mRelation.producter != null)
            {
                mRelation.container.producters.Add(mRelation.producter);
                moneyContainers.Add(mRelation.container);
            }
        }

        public void OnRelationRemove(IRelation relation)
        {
            var mRelation = GetMoneyProductRelation(relation);
            if (mRelation.container != null && mRelation.producter != null)
            {
                mRelation.container.producters.Remove(mRelation.producter);
                moneyContainers.Add(mRelation.container);
            }
        }

        public (IMoneyContainer container, IMoneyProducter producter) GetMoneyProductRelation(IRelation relation)
        {
            var container = relation.p1.GetComponent<IMoneyContainer>();
            if (container != null)
            {
                return (container, relation.p2.GetComponent<IMoneyProducter>());
            }

            container = relation.p2.GetComponent<IMoneyContainer>();
            if (container != null)
            {
                return (container, relation.p1.GetComponent<IMoneyProducter>());
            }

            return (null, null);
        }
    }
}
