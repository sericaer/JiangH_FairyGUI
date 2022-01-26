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

        public int engineTotal { get; private set; }
        public int engineSpend { get; private set; }

        public ReadOnlyObservableCollection<IEstate> estates => person.estates;

        public ObservableCollection<IPersonEngineSpend> engineSpendItems => person.engine.spendItems;

        private IPerson person { get; set; }

        public PersonDetailWindowView(object param)
        {
            person = param as IPerson;

            BindOneWay(person, x => x.engine.total, this, y => y.engineTotal);
            BindOneWay(person, x => x.engine.spend, this, y => y.engineSpend);
        }

    }
}
