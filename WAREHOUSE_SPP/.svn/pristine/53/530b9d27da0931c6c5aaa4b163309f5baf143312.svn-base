
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_LOAIKHOBussiness
    {
        bool InsertOrUpdateDM_LOAIKHO(DataTable dst);
        bool DeleteDM_LOAIKHO(string pMA_DVIQLY, long pLOAI_KTHUOC_ID, string pModifiedBy);
        DataTable GetListDM_LOAIKHO(string pMA_DVIQLY);
        DataTable DM_KHO_GET_LIST_ALL(string pMA_DVIQLY);
        DataTable GetListDM_LOAIKHO_TEN(string pMA_DVIQLY, string pTEN_LOAIKHO);
    }
    public class DM_LOAIKHOBussiness : IDM_LOAIKHOBussiness
    {
        IDM_LOAIKHOProvider _DM_LOAIKHOProvider;
        public DM_LOAIKHOBussiness(string appId, string userId = "0")
        {
            _DM_LOAIKHOProvider = new DM_LOAIKHOProvider(appId, userId);
        }
        public bool InsertOrUpdateDM_LOAIKHO(DataTable dst)
        {
            return _DM_LOAIKHOProvider.InsertOrUpdateDM_LOAIKHO(dst);
        }
        public bool DeleteDM_LOAIKHO(string pMA_DVIQLY, long pLOAI_KTHUOC_ID, string pModifiedBy)
        {
            return _DM_LOAIKHOProvider.DeleteDM_LOAIKHO(pMA_DVIQLY, pLOAI_KTHUOC_ID, pModifiedBy);
        }
        public DataTable GetListDM_LOAIKHO(string pMA_DVIQLY)
        {
            return _DM_LOAIKHOProvider.GetListDM_LOAIKHO(pMA_DVIQLY);
        }

        public DataTable DM_KHO_GET_LIST_ALL(string pMA_DVIQLY)
        {
            return _DM_LOAIKHOProvider.DM_KHO_GET_LIST_ALL(pMA_DVIQLY);
        }

        public DataTable GetListDM_LOAIKHO_TEN(string pMA_DVIQLY, string pTEN_LOAIKHO)
        {
            return _DM_LOAIKHOProvider.GetListDM_LOAIKHO_TEN(pMA_DVIQLY, pTEN_LOAIKHO);
        }
    }
}
