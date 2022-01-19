using JiangH.API;

namespace JiangH.Kernels.Entities
{
    public abstract class Entity : Point
    {
        protected GameSession session;

        public Entity(GameSession session)
        {
            this.session = session;
        }
    }
}