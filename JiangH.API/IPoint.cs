using System.Collections.ObjectModel;

namespace JiangH.API
{
    public interface IPoint
    {
        string name { get; set; }

        ObservableCollection<IRelation> relations { get; }
        ObservableCollection<IComponent> components { get; }

        T GetComponent<T>() where T : IComponent;
    }
}
