
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_PALLETBussiness
    {
        bool InsertorUpdateDM_PALLET(DataTable dst);
        bool DeleteDM_PALLET(string pMA_DVIQLY, long pPALLET_ID, string pModifiedBy);
        DataTable GetListDM_PALLET(string pMA_DVIQLY);
        DataTable GetListDM_PALLET_BY_ID(string pMA_DVIQLY, long pNHOM_PALLET_ID);
        DataTable GetListDM_PALLET_NHOM_PALLET(string pMA_DVIQLY);
        DataTable GetListDM_PALLET_KHU_QUAN_LY(string pMA_DVIQLY);
        DataTable GetListDM_PALLET_KEY(string pMA_DVIQLY, string pMA_PALLET);
    }
    public class DM_PALLETBussiness : IDM_PALLETBussiness
    {
        IDM_PALLETProvider _DM_PALLETProvider;
        public DM_PALLETBussiness(string appId, string userId = "0")
        {
            _DM_PALLETProvider = new DM_PALLETProvider(appId, userId);
        }
       public bool InsertorUpdateDM_PALLET(DataTable dst)
        {
            return _DM_PALLETProvider.InsertorUpdateDM_PALLET(dst);
        }
        public bool DeleteDM_PALLET(string pMA_DVIQLY, long pNHOM_PALLET_ID, string pModifiedBy)
        {
            return _DM_PALLETProvider.DeleteDM_PALLET(pMA_DVIQLY, pNHOM_PALLET_ID, pModifiedBy);
        }
        public DataTable GetListDM_PALLET(string pMA_DVIQLY)
        {
            return _DM_PALLETProvider.GetListDM_PALLET(pMA_DVIQLY);
        }
        public DataTable GetListDM_PALLET_BY_ID(string pMA_DVIQLY, long pNHOM_PALLET_ID)
        {
            return _DM_PALLETProvider.GetListDM_PALLET_BY_ID(pMA_DVIQLY, pNHOM_PALLET_ID);
        }
        public DataTable GetListDM_PALLET_NHOM_PALLET(string pMA_DVIQLY)
        {
            return _DM_PALLETProvider.GetListDM_PALLET_NHOM_PALLET(pMA_DVIQLY);
        }
        public DataTable GetListDM_PALLET_KHU_QUAN_LY(string pMA_DVIQLY)
        {
            return _DM_PALLETProvider.GetListDM_PALLET_KHU_QUAN_LY(pMA_DVIQLY);
        }
        public DataTable GetListDM_PALLET_KEY(string pMA_DVIQLY, string pMA_PALLET)
        {
            return _DM_PALLETProvider.GetListDM_PALLET_KEY(pMA_DVIQLY, pMA_PALLET);
        }
    }
}
