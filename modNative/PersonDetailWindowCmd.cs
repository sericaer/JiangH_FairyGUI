using JiangH.API;
using System;

namespace modNative
{
    public class PersonDetailWindowCmd : INewWindowCmd
    {
        public string newWindowName { get; private set; }
        public object param { get; private set; }

        public Func<object> paramGetter { get; private set; }

        public PersonDetailWindowCmd(Func<IPerson> personGetter)
        {
            newWindowName = "PersonDetailWindow";
            paramGetter = personGetter;
        }

        public void Exec()
        {
            param = paramGetter();
        }
    }
}
