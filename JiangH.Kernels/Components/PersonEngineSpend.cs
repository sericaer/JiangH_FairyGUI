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

        //public int realValue
        //{
        //    get
        //    {
        //        return _realValue;
        //    }
        //    set
        //    {
        //        if (isValidValue == null || isValidValue(value))
        //        {
        //            _realValue = value;
        //        }
        //    }
        //}

        //public int expectValue { get; private set; }

        //public int percent => realValue * 100 / expectValue;

        public int realValue { get; private set; }
        public IPoint owner { get; private set; }

        public string name => owner.name;

        //public Func<int, bool> isValidValue { get; set; }

        private int _realValue;

        public PersonEngineSpend(IPoint owner)
        {
            this.owner = owner;
            this.realValue = 20;
            //this.expectValue = 20;
        }
    }
}
