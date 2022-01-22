using System.ComponentModel;

namespace JiangH.API
{
    public class GameEnv : INotifyPropertyChanged
    {
#pragma warning disable 0067 // No "Never used" warning
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public int DayIncSpeed { get; set; }

        public GameEnv()
        {
            DayIncSpeed = 1;
        }
    }
}
