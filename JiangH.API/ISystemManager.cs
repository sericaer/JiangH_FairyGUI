namespace JiangH.API
{
    public interface ISystemManager
    {
        void OnDaysInc();
        void OnRelationAdd(IRelation relation);
        void OnRelationRemove(IRelation relation);
    }
}
