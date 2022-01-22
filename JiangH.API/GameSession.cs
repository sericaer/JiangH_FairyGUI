using System.ComponentModel;

namespace JiangH.API
{
    public class GameSession : INotifyPropertyChanged
    {
#pragma warning disable 0067 // No "Never used" warning
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public IPerson  player { get; set; }

        public IDate date { get; set; }

        public IRelationManager relationManager { get; set; }

        public ISystemManager systemManager { get; set; }

        public void OnDaysInc()
        {
            date.OnDaysInc();
            player.OnDaysInc();
            systemManager.OnDaysInc();
        }
    }
}
