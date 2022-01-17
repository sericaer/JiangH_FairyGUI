using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            date.WhenChanged(x => x.year).Subscribe(_ => player.age++);
        }
    }

    public interface IDate : INotifyPropertyChanged
    {
        int year { get; set; }
        int month { get; set; }
        int day { get; set; }
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
            set
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
            set
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
            set
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
    }

    public interface IPerson : INotifyPropertyChanged
    {
        string name { get; set; }

        int age { get; set; }
    }

    public class Person : IPerson
    {
#pragma warning disable 0067 // No "Never used" warning
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public string name { get ; set; }
        public int age { get; set; }

        public Person()
        {
            name = "Test1";
            age = 20;
        }

    }
}
