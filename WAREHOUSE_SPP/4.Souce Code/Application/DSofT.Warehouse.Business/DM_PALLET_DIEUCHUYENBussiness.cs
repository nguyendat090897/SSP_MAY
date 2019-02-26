
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_PALLET_DIEUCHUYENBussiness
    {
        bool DeleteDM_PALLET_DIEUCHUYEN(string pMA_DVIQLY, long PALLET_DIEUCHUYEN_ID, string pModifiedBy);
        DataTable GetListDM_PALLET_DIEUCHUYEN(string pMA_DVIQLY);
        DataTable GetListDM_PALLET_DIEUCHUYEN_TEN_PALLET(string pMA_DVIQLY, long pPALLET_DIEUCHUYEN_CTIET_ID);

    }
    public class DM_PALLET_DIEUCHUYENBussiness : IDM_PALLET_DIEUCHUYENBussiness
    {
        IDM_PALLET_DIEUCHUYENProvider _DM_PALLET_DIEUCHUYENProvider;
        public DM_PALLET_DIEUCHUYENBussiness(string appId, string userId = "0")
        {
            _DM_PALLET_DIEUCHUYENProvider = new DM_PALLET_DIEUCHUYENProvider(appId, userId);
        }
        public bool DeleteDM_PALLET_DIEUCHUYEN(string pMA_DVIQLY, long PALLET_DIEUCHUYEN_ID, string pModifiedBy)
        {
            return _DM_PALLET_DIEUCHUYENProvider.DeleteDM_PALLET_DIEUCHUYEN(pMA_DVIQLY, PALLET_DIEUCHUYEN_ID, pModifiedBy);
        }
        public DataTable GetListDM_PALLET_DIEUCHUYEN(string pMA_DVIQLY)
        {
            return _DM_PALLET_DIEUCHUYENProvider.GetListDM_PALLET_DIEUCHUYEN(pMA_DVIQLY);
        }
        public DataTable GetListDM_PALLET_DIEUCHUYEN_TEN_PALLET(string pMA_DVIQLY, long pPALLET_DIEUCHUYEN_CTIET_ID)
        {
            return _DM_PALLET_DIEUCHUYENProvider.GetListDM_PALLET_DIEUCHUYEN_TEN_PALLET(pMA_DVIQLY, pPALLET_DIEUCHUYEN_CTIET_ID);
        }
    }
}
