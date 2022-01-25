using JiangH.API;
using System.Collections.ObjectModel;

namespace modNative
{
    [UISceneBind("PersonDetailWindow")]
    public class PersonDetailWindowView : UIView
    {
        public string name => person.name;

        public ReadOnlyObservableCollection<IEstate> estates => person.estates;

        public ReadOnlyObservableCollection<IPersonEngineSpend> engineSpends => person.engineSpends;

        private IPerson person { get; set; }

        public override void Init(object param)
        {
            person = param as IPerson;
        }
    }
}
