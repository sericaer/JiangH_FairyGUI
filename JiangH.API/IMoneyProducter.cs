using System.Collections.ObjectModel;
using System.ComponentModel;

namespace JiangH.API
{
    public interface IMoneyProducter : JiangH.API.IComponent
    {
        int total { get; }

        int baseValue { get; }
        ObservableCollection<IEffectElement> effects { get; }
    }

    public interface IEffectElement : INotifyPropertyChanged
    {
        object key { get; }

        int effectPercent { get; set; }

        string desc { get; }
    }
}
