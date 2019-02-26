
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_PALLET_TRANGTHAIBussiness
    {
        bool InsertorUpdateDM_PALLET_TRANGTHAI(DataTable dst);
        bool DeleteDM_PALLET_TRANGTHAI(string pMA_DVIQLY, long PALLET_TRANGTHAI_ID, string pModifiedBy);
        DataTable GetListDM_PALLET_TRANGTHAI(string pMA_DVIQLY);
        DataTable GetListDM_PALLET_NHOM_PALLET(string pMA_DVIQLY);
        DataTable GetListDM_PALLET_BY_ID(string pMA_DVIQLY, long pNHOM_PALLET_ID);
        DataTable GetListDM_PALLET_TRANGTHAI_KEY(string pMA_DVIQLY, long pPALLET_ID, string pLOAI_TRANGTHAI, string pNGAY_TRANGTHAI);
    }
    public class DM_PALLET_TRANGTHAIBussiness : IDM_PALLET_TRANGTHAIBussiness
    {
        IDM_PALLET_TRANGTHAIProvider _DM_PALLET_TRANGTHAIProvider;
        public DM_PALLET_TRANGTHAIBussiness(string appId, string userId = "0")
        {
            _DM_PALLET_TRANGTHAIProvider = new DM_PALLET_TRANGTHAIProvider(appId, userId);
        }
       public bool InsertorUpdateDM_PALLET_TRANGTHAI(DataTable dst)
        {
            return _DM_PALLET_TRANGTHAIProvider.InsertorUpdateDM_PALLET_TRANGTHAI(dst);
        }
        public bool DeleteDM_PALLET_TRANGTHAI(string pMA_DVIQLY, long PALLET_TRANGTHAI_ID, string pModifiedBy)
        {
            return _DM_PALLET_TRANGTHAIProvider.DeleteDM_PALLET_TRANGTHAI(pMA_DVIQLY, PALLET_TRANGTHAI_ID, pModifiedBy);
        }
        public DataTable GetListDM_PALLET_TRANGTHAI(string pMA_DVIQLY)
        {
            return _DM_PALLET_TRANGTHAIProvider.GetListDM_PALLET_TRANGTHAI(pMA_DVIQLY);
        }
        public DataTable GetListDM_PALLET_BY_ID(string pMA_DVIQLY, long pNHOM_PALLET_ID)
        {
            return _DM_PALLET_TRANGTHAIProvider.GetListDM_PALLET_BY_ID(pMA_DVIQLY, pNHOM_PALLET_ID);
        }
        public DataTable GetListDM_PALLET_NHOM_PALLET(string pMA_DVIQLY)
        {
            return _DM_PALLET_TRANGTHAIProvider.GetListDM_PALLET_NHOM_PALLET(pMA_DVIQLY);
        }
        public DataTable GetListDM_PALLET_TRANGTHAI_KEY(string pMA_DVIQLY, long pPALLET_ID, string pLOAI_TRANGTHAI, string pNGAY_TRANGTHAI)
        {
            return _DM_PALLET_TRANGTHAIProvider.GetListDM_PALLET_TRANGTHAI_KEY(pMA_DVIQLY, pPALLET_ID, pLOAI_TRANGTHAI, pNGAY_TRANGTHAI);
        }
    }
}
