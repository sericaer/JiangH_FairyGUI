using System;
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

        public ISystemManager systemManager { get; set; }

        public void OnDaysInc()
        {
            date.OnDaysInc();
            player.OnDaysInc();
            systemManager.OnDaysInc();
        }
    }

    public interface IDate : INotifyPropertyChanged
    {
        int year { get; }
        int month { get;}
        int day { get;}

        void OnDaysInc();
    }

    public interface ISystemManager
    {
        void OnDaysInc();
        void OnRelationAdd(IRelation relation);
        void OnRelationRemove(IRelation relation);
    }


    public interface ISystem
    {
        void OnDaysInc();
        void OnRelationAdd(IRelation relation);
        void OnRelationRemove(IRelation relation);
    }

    public interface IMoneyProducter : JiangH.API.IComponent
    {
        int total { get; }
    }

    public interface IMoneyContainer : JiangH.API.IComponent
    {
        int count { get; set; }

        ObservableCollection<IMoneyProducter> producters { get; }

        string detailIncome { get; set; }
    }

    public interface IPerson : IPoint, INotifyPropertyChanged
    {
        string name { get; set; }

        int age { get; set; }

        IMoneyContainer money { get; }

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

    public interface IComponent : INotifyPropertyChanged
    {

    }

    public interface IPoint
    {
        ObservableCollection<IRelation> relations { get; }
        ObservableCollection<IComponent> components { get; }

        T GetComponent<T>() where T : IComponent;
    }

    public interface IRelation
    {
        IPoint p1 { get; }

        IPoint p2 { get; }

        Dictionary<string, object> attrib { get; }

        IPoint GetPeer(IPoint point);
    }

    public interface IRelationManager
    {
        Action<IRelation> onRelationAdd { get; set; }
        Action<IRelation> onRelationRemove { get; set; }
        void Add(IPoint p1, IPoint p2, Dictionary<string, object> attrib);
        void Remove(IPoint p1, IPoint p2);
    }
}
