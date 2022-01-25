using JiangH.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH.Kernels.Systems
{
    public static class Extentions
    {
        public static (IMoneyContainer container, IMoneyProducter producter) GetMoneyProductRelation(this IRelation relation)
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
