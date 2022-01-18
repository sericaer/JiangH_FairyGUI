using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ReactiveMarbles.PropertyChanged;

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

        public GameSession()
        {
            player = new Person();
            date = new Date();

            for(int i=0; i< 3; i++)
            {
                player.AddEstate(new Estate() { name = $"{i}_Estate" });
            }
        }

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

    public class Date : IDate
    {
#pragma warning disable 0067 // No "Never used" warning
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public int year
        {
            get
            {
                return _year;
            }
            private set
            {
                _year = value;
            }
        }

        public int month
        {
            get
            {
                return _month;
            }
            private set
            {
                _month = value;
                if (_month > 12)
                {
                    year += 1;
                    _month = 1;
                }
            }
        }

        public int day
        {
            get
            {
                return _day;
            }
            private set
            {
                _day = value;
                if (_day > 30)
                {
                    month += 1;
                    _day = 1;
                }
            }
        }

        private int _year;
        private int _month;
        private int _day;

        public Date()
        {
            year = 1;
            month = 1;
            day = 1;
        }

        public void OnDaysInc()
        {
            day++;
        }
    }

    public interface IPerson : INotifyPropertyChanged
    {
        string name { get; set; }

        int age { get; set; }

        int money { get; set; }

        ReadOnlyObservableCollection<IEstate> estates { get; set; }

        void OnDaysInc();

        void AddEstate(IEstate estate);
        void RemoveEstate();
    }

    public class Person : IPerson
    {
#pragma warning disable 0067 // No "Never used" warning
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public string name { get ; set; }
        public int age { get; set; }
        public int money { get; set; }

        public ReadOnlyObservableCollection<IEstate> estates { get; set; }

        private ObservableCollection<IEstate> _estates;

        public Person()
        {
            name = "Test1";
            age = 20;

            _estates = new ObservableCollection<IEstate>();

            estates = new ReadOnlyObservableCollection<IEstate>(_estates);
        }

        public void AddEstate(IEstate estate)
        {
            _estates.Add(estate);
        }

        public void OnDaysInc()
        {
            
        }

        public void RemoveEstate()
        {
            _estates.Remove(_estates.First());
        }
    }

    public interface IEstate : INotifyPropertyChanged
    {
        string name { get; set; }
    }

    public class Estate : IEstate
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string name { get; set; }
    }

}
