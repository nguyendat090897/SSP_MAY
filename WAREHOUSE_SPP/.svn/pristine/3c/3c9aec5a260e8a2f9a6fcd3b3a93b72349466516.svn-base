
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_KHO_KHUVUC_NCC_VITRIBussiness
    {
        bool InsertOrUpdateDM_KHO_KHUVUC_NCC_VITRI(DataTable dst);
        bool DeleteDM_KHO_KHUVUC_NCC_VITRI(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_VITRI_ID, string pModifiedBy);
        DataTable GetListDM_KHO_KHUVUC_NCC_VITRI(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_ID);
        bool DM_KHO_KHUVUC_NCC_VITRI_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_VITRI_ID, long pKHO_KHUVUC_NCC_ID, string pVITRI);

    }
    public class DM_KHO_KHUVUC_NCC_VITRIBussiness : IDM_KHO_KHUVUC_NCC_VITRIBussiness
    {
        IDM_KHO_KHUVUC_NCC_VITRIProvider _DM_KHO_KHUVUC_NCC_VITRIProvider;
        public DM_KHO_KHUVUC_NCC_VITRIBussiness(string appId, string userId = "0")
        {
            _DM_KHO_KHUVUC_NCC_VITRIProvider = new DM_KHO_KHUVUC_NCC_VITRIProvider(appId, userId);
        }
        public bool InsertOrUpdateDM_KHO_KHUVUC_NCC_VITRI(DataTable dst)
        {
            return _DM_KHO_KHUVUC_NCC_VITRIProvider.InsertOrUpdateDM_KHO_KHUVUC_NCC_VITRI(dst);
        }
        public bool DeleteDM_KHO_KHUVUC_NCC_VITRI(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_VITRI_ID, string pModifiedBy)
        {
            return _DM_KHO_KHUVUC_NCC_VITRIProvider.DeleteDM_KHO_KHUVUC_NCC_VITRI(pMA_DVIQLY, pKHO_KHUVUC_NCC_VITRI_ID, pModifiedBy);
        }
        public DataTable GetListDM_KHO_KHUVUC_NCC_VITRI(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_VITRI_ID)
        {
            return _DM_KHO_KHUVUC_NCC_VITRIProvider.GetListDM_KHO_KHUVUC_NCC_VITRI(pMA_DVIQLY, pKHO_KHUVUC_NCC_VITRI_ID);
        }
        public bool DM_KHO_KHUVUC_NCC_VITRI_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_VITRI_ID, long pKHO_KHUVUC_NCC_ID, string pVITRI)
        {
            return _DM_KHO_KHUVUC_NCC_VITRIProvider.DM_KHO_KHUVUC_NCC_VITRI_CHECKEXISTS(pMA_DVIQLY, pKHO_KHUVUC_NCC_VITRI_ID, pKHO_KHUVUC_NCC_ID, pVITRI);
        }
    }
}
