
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_KHO_VITRIBussiness
    {
        bool InsertOrUpdateDM_KHO_VITRI(DataTable dst);
        bool DeleteDM_KHO_VITRI(string pMA_DVIQLY, long pKHO_VITRI_ID, string pModifiedBy);
        DataTable GetListDM_KHO_VITRI(string pMA_DVIQLY, long pKHO_ID);
        DataTable GetListDM_KHO_VITRI_VITRI(string pMA_DVIQLY, long pKHO_ID, string @pVITRI);
        bool DM_KHO_VITRI_CHECKEXISTS(string pMA_DVIQLY, long pKHO_VITRI_ID, long pKHO_ID, string pVITRI);
    }
    public class DM_KHO_VITRIBussiness : IDM_KHO_VITRIBussiness
    {
        IDM_KHO_VITRIProvider _DM_KHO_VITRIProvider;
        public DM_KHO_VITRIBussiness(string appId, string userId = "0")
        {
            _DM_KHO_VITRIProvider = new DM_KHO_VITRIProvider(appId, userId);
        }
        public bool InsertOrUpdateDM_KHO_VITRI(DataTable dst)
        {
            return _DM_KHO_VITRIProvider.InsertOrUpdateDM_KHO_VITRI(dst);
        }
        public bool DeleteDM_KHO_VITRI(string pMA_DVIQLY, long pKHO_VITRI_ID, string pModifiedBy)
        {
            return _DM_KHO_VITRIProvider.DeleteDM_KHO_VITRI(pMA_DVIQLY, pKHO_VITRI_ID, pModifiedBy);
        }
        public DataTable GetListDM_KHO_VITRI(string pMA_DVIQLY,long pKHO_ID)
        {
            return _DM_KHO_VITRIProvider.GetListDM_KHO_VITRI(pMA_DVIQLY, pKHO_ID);
        }

        public DataTable GetListDM_KHO_VITRI_VITRI(string pMA_DVIQLY, long pKHO_ID, string @pVITRI)
        {
            return _DM_KHO_VITRIProvider.GetListDM_KHO_VITRI_VITRI(pMA_DVIQLY, pKHO_ID, @pVITRI);
        }
        public bool DM_KHO_VITRI_CHECKEXISTS(string pMA_DVIQLY, long pKHO_VITRI_ID, long pKHO_ID, string pVITRI)
        {
            return _DM_KHO_VITRIProvider.DM_KHO_VITRI_CHECKEXISTS(pMA_DVIQLY, pKHO_VITRI_ID, pKHO_ID, pVITRI);
        }
    }
}
