using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH.API
{
    public interface IPersonEngineSpend : IComponent
    {
        string name { get; }

        int realValue { get; }
        //int expectValue { get; }

        //int percent { get; }

        //Func<int, bool> isValidValue { get; set; }

    }
}
