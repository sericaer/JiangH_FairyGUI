using JiangH.API;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JiangH.Kernels.Relations
{
    public class RelationManager : IRelationManager
    {
        private List<IRelation> all;

        public Action<IRelation> onRelationAdd { get; set; }
        public Action<IRelation> onRelationRemove { get; set; }

        public RelationManager()
        {
            all = new List<IRelation>();
        }

        public void Add(IPoint p1, IPoint p2, Dictionary<string, object> attrib)
        {
            var relation = new Relation(p1, p2, attrib);
            all.Add(relation);

            p1.relations.Add(relation);
            p2.relations.Add(relation);

            onRelationAdd?.Invoke(relation);
        }

        public void Remove(IPoint p1, IPoint p2)
        {
            var relation = all.SingleOrDefault(x => (x.p1 == p1 && x.p2 == p2) || (x.p2 == p1 && x.p1 == p2));
            all.Remove(relation);

            p1.relations.Remove(relation);
            p2.relations.Remove(relation);

            onRelationRemove?.Invoke(relation);
        }
    }

}
