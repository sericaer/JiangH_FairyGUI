using JiangH.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH.Kernels.Systems
{
    class SystemManager : ISystemManager
    {
        private List<ISystem> all = new List<ISystem>()
        {
            new MoneyProductSystem()
        };

        public void OnDaysInc()
        {
            foreach(var system in all)
            {
                system.OnDaysInc();
            }
        }

        public void OnRelationAdd(IRelation relation)
        {
            foreach (var system in all)
            {
                system.OnRelationAdd(relation);
            }
        }

        public void OnRelationRemove(IRelation relation)
        {
            foreach (var system in all)
            {
                system.OnRelationRemove(relation);
            }
        }

        public void OnComponentAdd(IComponent component)
        {
            foreach (var system in all)
            {
                system.OnComponentAdd(component);
            }
        }

        public void OnComponentRemove(IComponent component)
        {
            foreach (var system in all)
            {
                system.OnComponentRemove(component);
            }
        }
    }
}
