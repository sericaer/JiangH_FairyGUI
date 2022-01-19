using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public class GameSession : INotifyPropertyChanged
    {
#pragma warning disable 0067 // No "Never used" warning
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public IPerson  player { get; set; }

        public IDate date { get; set; }

        public IRelationManager relationManager { get; set; }

        public void OnDaysInc()
        {
            date.OnDaysInc();
            player.OnDaysInc();
        }
    }

    public interface IDate : INotifyPropertyChanged
    {
        int year { get; }
        int month { get;}
        int day { get;}

        void OnDaysInc();
    }

    public interface IPerson : IPoint, INotifyPropertyChanged
    {
        string name { get; set; }

        int age { get; set; }

        int money { get; set; }

        ReadOnlyObservableCollection<IEstate> estates { get; set; }

        void OnDaysInc();

        void AddEstate(IEstate estate);
        void RemoveEstate(IEstate estate);
    }

    public interface IEstate : IPoint, INotifyPropertyChanged
    {
        string name { get; set; }
    }


    public interface IGameSessionBuilder
    {
        GameSession build();
    }

    public interface IPoint
    {
        ObservableCollection<IRelation> relations { get; }
    }

    public interface IRelation
    {
        IPoint p1 { get; }

        IPoint p2 { get; }

        Dictionary<string, object> attrib { get; }
    }

    public interface IRelationManager
    {
        void Add(IPoint p1, IPoint p2, Dictionary<string, object> attrib);
        void Remove(IPoint p1, IPoint p2);
    }
}
