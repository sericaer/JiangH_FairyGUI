using System;
using System.Collections.Generic;

namespace JiangH.API
{
    public interface IRelationManager
    {
        Action<IRelation> onRelationAdd { get; set; }
        Action<IRelation> onRelationRemove { get; set; }
        void Add(IPoint p1, IPoint p2, Dictionary<string, object> attrib);
        void Remove(IPoint p1, IPoint p2);
    }
}
