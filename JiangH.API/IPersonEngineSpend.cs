using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH.API
{
    public interface IPersonEngineSpend : IComponent
    {
        int realValue { get; set; }
        int expectValue { get; }
    }
}
