using System.Collections.ObjectModel;
using System.ComponentModel;

namespace JiangH.API
{
    public interface IPersonEngine : INotifyPropertyChanged
    {
        int total { get; }
        int spend { get; }
        int reserve { get; }

        ObservableCollection<IPersonEngineSpend> spendItems { get; }
    }
}