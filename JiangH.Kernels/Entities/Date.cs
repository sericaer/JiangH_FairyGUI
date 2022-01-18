using JiangH.API;
using System.ComponentModel;

namespace JiangH.Kernels.Entities
{
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

}
