
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_KHO_KHUVUC_NCCBussiness
    {
        bool InsertOrUpdateDM_KHO_KHUVUC_NCC(DataTable dst);
        bool DeleteDM_KHO_KHUVUC_NCC(string pMA_DVIQLY, long pKHO_KHUVUC_HANG_ID, string pModifiedBy);
        DataTable GetListDM_KHO_KHUVUC_NCC(string pMA_DVIQLY, long pKHO_ID, long pKHO_KHUVUC_ID);
        DataTable GetListDM_KHO_KHUVUC_NCC_TEN(string pMA_DVIQLY);
        bool DM_KHO_KHUVUC_NCC_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_ID, long pKHO_ID, long pKHO_KHUVUC_ID, string pKHACHHANG_NCC_ID);

    }
    public class DM_KHO_KHUVUC_NCCBussiness : IDM_KHO_KHUVUC_NCCBussiness
    {
        IDM_KHO_KHUVUC_NCCProvider _DM_KHO_KHUVUC_NCCProvider;
        public DM_KHO_KHUVUC_NCCBussiness(string appId, string userId = "0")
        {
            _DM_KHO_KHUVUC_NCCProvider = new DM_KHO_KHUVUC_NCCProvider(appId, userId);
        }
        public bool InsertOrUpdateDM_KHO_KHUVUC_NCC(DataTable dst)
        {
            return _DM_KHO_KHUVUC_NCCProvider.InsertOrUpdateDM_KHO_KHUVUC_NCC(dst);
        }
        public bool DeleteDM_KHO_KHUVUC_NCC(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_ID, string pModifiedBy)
        {
            return _DM_KHO_KHUVUC_NCCProvider.DeleteDM_KHO_KHUVUC_NCC(pMA_DVIQLY, pKHO_KHUVUC_NCC_ID, pModifiedBy);
        }
        public DataTable GetListDM_KHO_KHUVUC_NCC(string pMA_DVIQLY, long pKHO_ID, long pKHO_KHUVUC_ID)
        {
            return _DM_KHO_KHUVUC_NCCProvider.GetListDM_KHO_KHUVUC_NCC(pMA_DVIQLY, pKHO_ID, pKHO_KHUVUC_ID);
        }
        public DataTable GetListDM_KHO_KHUVUC_NCC_TEN(string pMA_DVIQLY)
        {
            return _DM_KHO_KHUVUC_NCCProvider.GetListDM_KHO_KHUVUC_NCC_TEN(pMA_DVIQLY);
        }
        public bool DM_KHO_KHUVUC_NCC_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_ID, long pKHO_ID, long pKHO_KHUVUC_ID, string pKHACHHANG_NCC_ID)
        {
            return _DM_KHO_KHUVUC_NCCProvider.DM_KHO_KHUVUC_NCC_CHECKEXISTS(pMA_DVIQLY, pKHO_KHUVUC_NCC_ID, pKHO_ID, pKHO_KHUVUC_ID, pKHACHHANG_NCC_ID);
        }
    }
}
