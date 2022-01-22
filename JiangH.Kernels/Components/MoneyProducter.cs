using JiangH.API;
using System.ComponentModel;

namespace JiangH.Kernels.Components
{
    public class MoneyProducter : IMoneyProducter
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int total { get; set; }

        public IPoint owner { get; private set; }

        public MoneyProducter(IPoint owner)
        {
            total = 10;

            this.owner = owner;
        }
    }
}
