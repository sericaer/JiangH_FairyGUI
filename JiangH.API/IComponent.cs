using System.ComponentModel;

namespace JiangH.API
{
    public interface IComponent : INotifyPropertyChanged
    {
        IPoint owner { get; }
    }
}
