using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_TINHBussiness
    {
    
        bool InsertorUpdateDM_TINH(DataTable ds);
        bool DeleteDM_TINH(long pTINH_ID, string pModifiedBy);
        DataTable GetListDM_TINH();
        DataTable GetListDM_TINH_GET_KEY(string pMA_TINH);

    }
    public class DM_TINHBussiness : IDM_TINHBussiness
    {
        IDM_TINHProvider _DM_TINHProvider;
        public DM_TINHBussiness(string appId, string userId = "0")
        {
            _DM_TINHProvider = new DM_TINHProvider(appId, userId);
        }
        public bool InsertorUpdateDM_TINH(DataTable ds)
        {
            return _DM_TINHProvider.InsertorUpdateDM_TINH(ds);
        }
        public bool DeleteDM_TINH(long pTINH_ID, string pModifiedBy)
        {
            return _DM_TINHProvider.DeleteDM_TINH( pTINH_ID, pModifiedBy);
        }
        public DataTable GetListDM_TINH()
        {
            return _DM_TINHProvider.GetListDM_TINH();
        }
        public DataTable GetListDM_TINH_GET_KEY(string pMA_TINH)
        {
            return _DM_TINHProvider.GetListDM_TINH_GET_KEY(pMA_TINH);
        }
    }
}