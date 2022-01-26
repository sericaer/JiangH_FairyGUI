using JiangH.API;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using ReactiveMarbles.PropertyChanged;
using System.Collections.Generic;

namespace JiangH.Kernels.Entities
{
    internal class PersonEngine : IPersonEngine
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int total { get; private set; }

        public int spend { get; private set; }

        public int reserve => total - spend;

        public int maxSpend { get; private set; }

        public ObservableCollection<IPersonEngineSpend> spendItems { get; private set; }

        private Dictionary<IPersonEngineSpend, IDisposable> disposeDict;
        
        public PersonEngine()
        {
            total = 100;
            maxSpend = 200;

            spendItems = new ObservableCollection<IPersonEngineSpend>();
            disposeDict = new Dictionary<IPersonEngineSpend, IDisposable>();

            spendItems.CollectionChanged += OnSpendItemChanged;
        }

        private void OnSpendItemChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        foreach (IPersonEngineSpend elem in e.NewItems)
                        {
                            var disp = elem.WhenChanged(x => x.realValue).Subscribe(_ => 
                            {
                                var sum = spendItems.Sum(x => x.realValue);
                                if (sum >= maxSpend)
                                {
                                    elem.realValue -= sum - maxSpend;
                                }
                                else
                                {
                                    UpdateSpendValue();
                                }
                            });

                            disposeDict.Add(elem, disp);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        foreach (IPersonEngineSpend elem in e.OldItems)
                        {
                            elem.isValidValue = null;

                            disposeDict[elem].Dispose();
                            disposeDict.Remove(elem);
                        }
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void UpdateSpendValue()
        {
            spend = spendItems.Sum(x => x.realValue);
        }
    }
}