using JiangH.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JiangH.Kernels.Components
{
    class EffectElement : IEffectElement
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public object key { get; private set; }

        public int effectPercent { get ; set; }

        public string desc => $"{key} : {effectPercent}";

        public EffectElement(PersonEngineSpend personEngineSpend, int percent)
        {
            this.key = personEngineSpend;
            this.effectPercent = percent;
        }
    }
}
