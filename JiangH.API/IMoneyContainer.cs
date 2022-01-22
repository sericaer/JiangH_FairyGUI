using System.Collections.ObjectModel;

namespace JiangH.API
{
    public interface IMoneyContainer : JiangH.API.IComponent
    {
        int count { get; set; }

        ObservableCollection<IMoneyProducter> producters { get; }

        string detailIncome { get; set; }
    }
}
