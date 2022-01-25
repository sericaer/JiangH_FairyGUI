using System.Collections.ObjectModel;
using System.ComponentModel;

namespace JiangH.API
{
    public interface IPoint : INotifyPropertyChanged
    {
        string name { get; set; }

        ObservableCollection<IRelation> relations { get; }
        ObservableCollection<IComponent> components { get; }

        T GetComponent<T>() where T : IComponent;
    }
}
