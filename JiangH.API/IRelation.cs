using System.Collections.Generic;

namespace JiangH.API
{
    public interface IRelation
    {
        IPoint p1 { get; }

        IPoint p2 { get; }

        Dictionary<string, object> attrib { get; }

        IPoint GetPeer(IPoint point);
    }
}
