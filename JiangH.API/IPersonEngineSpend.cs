using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH.API
{
    public interface IPersonEngineSpend : IComponent
    {
        string name { get; }
        int realValue { get; set; }
        int expectValue { get; }

        int percent { get; }

    }
}
