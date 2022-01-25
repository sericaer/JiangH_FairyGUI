using JiangH.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JiangH.Kernels.Components
{
    class PersonEngineSpend : IPersonEngineSpend
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int realValue { get; set; }

        public int expectValue { get; private set; }

        public int percent => realValue * 100 / expectValue;

        public IPoint owner { get; private set; }

        public string name => owner.name;

        private IPoint _owner;

        public PersonEngineSpend(IPoint owner)
        {
            this.owner = owner;
            this.expectValue = 20;
        }
    }
}
