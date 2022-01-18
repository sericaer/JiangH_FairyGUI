using JiangH.API;

namespace modNative
{
    [UISceneBind("PersonDetailWindow")]
    public class PersonDetailWindowView : UIView
    {
        public string name => person.name;

        public IPerson person { get; set; }

        public override void Init(object param)
        {
            person = param as IPerson;
        }
    }
}
