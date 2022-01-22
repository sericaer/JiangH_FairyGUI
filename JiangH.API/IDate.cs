using System.ComponentModel;

namespace JiangH.API
{
    public interface IDate : INotifyPropertyChanged
    {
        int year { get; }
        int month { get;}
        int day { get;}

        void OnDaysInc();
    }
}
