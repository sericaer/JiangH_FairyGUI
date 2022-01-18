using JiangH.API;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace JiangH.Kernels.Entities
{
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

}
