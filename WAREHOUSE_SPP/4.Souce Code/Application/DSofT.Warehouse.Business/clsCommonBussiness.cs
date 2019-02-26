
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IclsCommonBussiness
    {
         DataTable GetList_ITEM_TYPE();
    }
    public  class clsCommonBussiness : IclsCommonBussiness
    {
        IclsCommonProvider _DM_KHOProvider;

        public clsCommonBussiness(string appId, string userId = "0")
        {
            _DM_KHOProvider = new clsCommonProvider(appId, userId);
        }

        public DataTable GetList_ITEM_TYPE()
        {
            return _DM_KHOProvider.GetList_ITEM_TYPE();
        }
    }
}
