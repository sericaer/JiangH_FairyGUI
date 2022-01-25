using JiangH.API;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ReactiveMarbles.PropertyChanged;

namespace JiangH.Kernels.Components
{
    public class MoneyProducter : IMoneyProducter
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public int total { get; private set; }

        public IPoint owner { get; private set; }

        public int baseValue { get; private set; }

        public ObservableCollection<IEffectElement> effects { get; private set; }

        public MoneyProducter(IPoint owner, int baseValue)
        {
            this.owner = owner;
            this.baseValue = baseValue;
            this.effects = new ObservableCollection<IEffectElement>();

            this.WhenChanged(x => x.baseValue).Subscribe(_ =>
            {
                  UpdateTotalValue();
            });

            effects.CollectionChanged += (sender, e) =>
            {
                UpdateTotalValue();
            };
        }

        private void UpdateTotalValue()
        {
            total = baseValue * (100 + effects.Sum(x => x.effectPercent)) / 100;
        }
    }
}
