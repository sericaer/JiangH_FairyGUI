using JiangH.API;
using JiangH.Kernels.Components;
using System;
using System.ComponentModel;
using System.Linq;
using ReactiveMarbles.PropertyChanged;

namespace JiangH.Kernels.Entities
{
    public class Estate : Point, IEstate
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public Estate()
        {
            var moneyProduct = new MoneyProducter(this, 10);
            components.Add(moneyProduct);

            var personEngineSpend = new PersonEngineSpend(this);
            components.Add(personEngineSpend);

            personEngineSpend.WhenChanged(x => x.percent).Subscribe(percent => {
                var effectElem = new EffectElement(personEngineSpend, percent-100);
                for(int i=0; i< moneyProduct.effects.Count; i++)
                {
                    if(moneyProduct.effects[i].key == effectElem.key)
                    {
                        moneyProduct.effects[i] = effectElem;
                        return;
                    }
                }

                moneyProduct.effects.Add(effectElem);
            });

        }

        public override void OnRelationAdd(IRelation relation)
        {

        }

        public override void OnRelationRemove(IRelation relation)
        {

        }
    }
}
