using JiangH.API;

namespace modNative
{
    [UISceneBind("PersonDetailWindow")]
    public class PersonDetailWindowView : UIView
    {
        [UIButtonBinding("BtnClose", UIButtonBinding.Attrib.TriggerCmd)]
        public ICommand closeButtonCmd => _closeButtonCmd;

        [UITextBinding("name", UITextBinding.Attrib.text)]
        public string name => person.name;

        public IPerson person { get; set; }

        private CommonCmd _closeButtonCmd;

        public PersonDetailWindowView()
        {
            _closeButtonCmd = new CommonCmd(() =>
            {
                isRemoved = true;
            });
        }

        public override void Init(object param)
        {
            person = param as IPerson;
        }
    }
}
