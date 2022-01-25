namespace JiangH.API
{
    public interface ISystem
    {
        void OnDaysInc();
        void OnRelationAdd(IRelation relation);
        void OnRelationRemove(IRelation relation);
        void OnComponentAdd(IComponent component);
        void OnComponentRemove(IComponent component);
    }
}
