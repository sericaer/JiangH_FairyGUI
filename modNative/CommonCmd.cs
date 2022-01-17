using JiangH.API;
using System;

namespace modNative
{
    public class CommonCmd : ICommand
    {
        private Action act;

        public CommonCmd(Action act)
        {
            this.act = act;
        }

        public void Exec()
        {
            act.Invoke();
        }
    }
}
