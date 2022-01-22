using JiangH.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH.Kernels.Relations
{
    class Relation : IRelation
    {
        public IPoint p1 { get; set; }

        public IPoint p2 { get; set; }

        public Dictionary<string, object> attrib { get; set; }


        public Relation(IPoint p1, IPoint p2, Dictionary<string, object> attrib)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.attrib = attrib;
        }

        public IPoint GetPeer(IPoint point)
        {
            if (point == p1)
            {
                return p2;
            }
            else if (point == p2)
            {
                return p1;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }

}
