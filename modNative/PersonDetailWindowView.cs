using JiangH.API;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace modNative
{
    [UISceneBind("PersonDetailWindow")]
    public class PersonDetailWindowView : UIView
    {
        public string name => person.name;

        public ReadOnlyObservableCollection<IEstate> estates => person.estates;

        public ReadOnlyObservableCollection<IPersonEngineSpend> engineSpends => person.engineSpends;

        private IPerson person { get; set; }

        public PersonDetailWindowView(object param)
        {
            person = param as IPerson;

            //engineSpends = new ObservableCollection<PersonEngineSpendSliderView>();

            //((INotifyCollectionChanged)person.engineSpends).CollectionChanged += OnCollectionChanged;
        }

        //private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    switch(e.Action)
        //    {
        //        case NotifyCollectionChangedAction.Add:
        //            foreach(IPersonEngineSpend elem in e.NewItems)
        //            {
        //                engineSpends.Add(new PersonEngineSpendSliderView(elem));
        //            }
        //            break;
        //        case NotifyCollectionChangedAction.Remove:
        //            foreach (IPersonEngineSpend elem in e.OldItems)
        //            {
        //                var needRemove = engineSpends.SingleOrDefault(x => x.engineSpend == elem);
        //                if(needRemove != null)
        //                {
        //                    engineSpends.Remove(needRemove);
        //                }
        //            }
        //            break;
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}

        //public override void Dispose()
        //{
        //    ((INotifyCollectionChanged)person.engineSpends).CollectionChanged -= OnCollectionChanged;
        //}

    }
}
