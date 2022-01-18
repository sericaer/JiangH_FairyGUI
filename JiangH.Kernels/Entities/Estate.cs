using JiangH.API;
using System.ComponentModel;

namespace JiangH.Kernels.Entities
{
    public class Estate : IEstate
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string name { get; set; }
    }

}
