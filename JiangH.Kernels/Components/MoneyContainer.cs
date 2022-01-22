using JiangH.API;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using ReactiveMarbles.PropertyChanged;

namespace JiangH.Kernels.Entities
{
    public class MoneyContainer : IMoneyContainer
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int count { get; set; }

        public string detailIncome { get; set; }
        public string detailOutput { get; set; }

        public ObservableCollection<IMoneyProducter> producters { get; private set; }

        public IPoint owner { get; private set; }

        private Dictionary<IMoneyProducter, IDisposable> disposeDict;

        public MoneyContainer(IPoint owner)
        {
            this.owner = owner;

            producters = new ObservableCollection<IMoneyProducter>();
            disposeDict = new Dictionary<IMoneyProducter, IDisposable>();

            producters.CollectionChanged += (sender, e) =>
            {
                switch(e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach(IMoneyProducter producter in e.NewItems)
                        {
                            var dispose = producter.WhenChanged(x => x.total).Subscribe(_ =>
                            {
                                detailIncome = string.Join("\n", producters.Select(x =>$"{x.owner.name} : {x.total}"));
                            });

                            disposeDict.Add(producter, dispose);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (IMoneyProducter producter in e.OldItems)
                        {
                            disposeDict[producter].Dispose();
                            disposeDict.Remove(producter);
                        }
                        break;
                }
            };
        }
    }
}
