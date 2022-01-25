using JiangH.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace modNative
{
    [UISceneBind("PersonEngineSpendSlider")]
    public class PersonEngineSpendSliderView : UIView
    {
        public string title { get; set; }

        public IPersonEngineSpend engineSpend { get; set; }

        public PersonEngineSpendSliderView(object param)
        {
            engineSpend = param as IPersonEngineSpend;

            BindOneWay(engineSpend, x => x.owner.name, this, t => t.title);
        }
    }
}
