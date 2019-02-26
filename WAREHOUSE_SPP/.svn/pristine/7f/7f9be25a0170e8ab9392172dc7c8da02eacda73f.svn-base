using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_PALLET_DIEUCHUYENProvider
    {
        bool DeleteDM_PALLET_DIEUCHUYEN(string pMA_DVIQLY, long PALLET_DIEUCHUYEN_ID, string pModifiedBy);
        DataTable GetListDM_PALLET_DIEUCHUYEN(string pMA_DVIQLY);
        DataTable GetListDM_PALLET_DIEUCHUYEN_TEN_PALLET(string pMA_DVIQLY,long pPALLET_DIEUCHUYEN_CTIET_ID);
        //DataTable GetListDM_PALLET_BY_ID(string pMA_DVIQLY, long pNHOM_PALLET_ID);
        //DataTable GetListDM_PALLET_NHOM_PALLET(string pMA_DVIQLY);
        //DataTable GetListDM_PALLET_DIEUCHUYEN_KEY(string pMA_DVIQLY, long pPALLET_ID, string pLOAI_DIEUCHUYEN, string pNGAY_DIEUCHUYEN);
    }
    public class DM_PALLET_DIEUCHUYENProvider : BaseSqlExecute, IDM_PALLET_DIEUCHUYENProvider
    {
        public DM_PALLET_DIEUCHUYENProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public bool DeleteDM_PALLET_DIEUCHUYEN(string pMA_DVIQLY, long PALLET_DIEUCHUYEN_ID, string pModifiedBy)
        {
            var paramObj = new object[] { pMA_DVIQLY, PALLET_DIEUCHUYEN_ID, pModifiedBy };
            var result = base.ExecScalar("DM_PALLET_DIEUCHUYEN_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable GetListDM_PALLET_DIEUCHUYEN(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_PALLET_DIEUCHUYEN_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
        public DataTable GetListDM_PALLET_DIEUCHUYEN_TEN_PALLET(string pMA_DVIQLY, long pPALLET_DIEUCHUYEN_CTIET_ID)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPALLET_DIEUCHUYEN_CTIET_ID };
            var result = base.ExecuteDataSet("DM_PALLET_DIEUCHUYEN_GET_LIST_TEN_PALLET", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
       
    }
}
