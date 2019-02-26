
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_KHO_KHUVUCBussiness
    {
        bool InsertOrUpdateDM_KHO_KHUVUC(DataTable dst);
        bool DeleteDM_KHO_KHUVUC(string pMA_DVIQLY, long pKHO_KHUVUC_ID, string pModifiedBy);
        DataTable GetListDM_KHO_KHUVUC(string pMA_DVIQLY, long pKHO_ID);
        bool DM_KHO_KHUVUC_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_ID, long pKHO_ID, string pMAKHO);
    }
    public class DM_KHO_KHUVUCBussiness : IDM_KHO_KHUVUCBussiness
    {
        IDM_KHO_KHUVUCProvider _DM_KHO_KHUVUCProvider;
        public DM_KHO_KHUVUCBussiness(string appId, string userId = "0")
        {
            _DM_KHO_KHUVUCProvider = new DM_KHO_KHUVUCProvider(appId, userId);
        }
        public bool InsertOrUpdateDM_KHO_KHUVUC(DataTable dst)
        {
            return _DM_KHO_KHUVUCProvider.InsertOrUpdateDM_KHO_KHUVUC(dst);
        }
        public bool DeleteDM_KHO_KHUVUC(string pMA_DVIQLY, long pKHO_KHUVUC_ID, string pModifiedBy)
        {
            return _DM_KHO_KHUVUCProvider.DeleteDM_KHO_KHUVUC(pMA_DVIQLY, pKHO_KHUVUC_ID, pModifiedBy);
        }
        public DataTable GetListDM_KHO_KHUVUC(string pMA_DVIQLY, long pKHO_ID)
        {
            return _DM_KHO_KHUVUCProvider.GetListDM_KHO_KHUVUC(pMA_DVIQLY, pKHO_ID);
        }
        public bool DM_KHO_KHUVUC_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_ID, long pKHO_ID, string pMAKHO)
        {
            return _DM_KHO_KHUVUCProvider.DM_KHO_KHUVUC_CHECKEXISTS(pMA_DVIQLY, pKHO_KHUVUC_ID, pKHO_ID, pMAKHO);
        }
    }
}
