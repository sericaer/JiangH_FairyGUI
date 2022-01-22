using System.Collections.ObjectModel;
using System.ComponentModel;

namespace JiangH.API
{
    public interface IPerson : IPoint, INotifyPropertyChanged
    {
        int age { get; set; }

        IMoneyContainer money { get; }

        ReadOnlyObservableCollection<IEstate> estates { get; set; }

        void OnDaysInc();

        void AddEstate(IEstate estate);
        void RemoveEstate(IEstate estate);
    }
}
