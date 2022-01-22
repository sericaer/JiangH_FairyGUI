namespace JiangH.API
{
    public interface ISystem
    {
        void OnDaysInc();
        void OnRelationAdd(IRelation relation);
        void OnRelationRemove(IRelation relation);
    }
}
