using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_PALLET_TRANGTHAIProvider
    {
        bool InsertorUpdateDM_PALLET_TRANGTHAI(DataTable dst);
        bool DeleteDM_PALLET_TRANGTHAI(string pMA_DVIQLY, long PALLET_TRANGTHAI_ID, string pModifiedBy);
        DataTable GetListDM_PALLET_TRANGTHAI(string pMA_DVIQLY);
        DataTable GetListDM_PALLET_BY_ID(string pMA_DVIQLY, long pNHOM_PALLET_ID);
        DataTable GetListDM_PALLET_NHOM_PALLET(string pMA_DVIQLY);
        DataTable GetListDM_PALLET_TRANGTHAI_KEY(string pMA_DVIQLY, long pPALLET_ID, string pLOAI_TRANGTHAI, string pNGAY_TRANGTHAI);
    }
    public class DM_PALLET_TRANGTHAIProvider : BaseSqlExecute, IDM_PALLET_TRANGTHAIProvider
    {
        public DM_PALLET_TRANGTHAIProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public bool InsertorUpdateDM_PALLET_TRANGTHAI(DataTable dst)
        {
            if (NumberHelper.ConvertStringToLong(dst.Rows[0]["PALLET_TRANGTHAI_ID"].ToString()) > 0)
            {
                var paramObj = new object[]{ dst.Rows[0]["PALLET_TRANGTHAI_ID"],
                            ConstCommon.DonViQuanLy,
                            dst.Rows[0]["PALLET_ID"],
                            dst.Rows[0]["LOAI_TRANGTHAI"],
                            dst.Rows[0]["NGAY_TRANGTHAI"],
                            dst.Rows[0]["GHI_CHU_TRANG_THAI"],
                            dst.Rows[0]["RowVersion"],
                             CommonDataHelper.UserName
                             };

                var result = base.ExecScalar("DM_PALLET_TRANGTHAI_UPDATE", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                var paramObj = new object[]{ConstCommon.DonViQuanLy,
                            dst.Rows[0]["PALLET_ID"],
                            dst.Rows[0]["LOAI_TRANGTHAI"],
                            dst.Rows[0]["NGAY_TRANGTHAI"],
                            dst.Rows[0]["GHI_CHU_TRANG_THAI"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                             };
                var result = base.ExecScalar("DM_PALLET_TRANGTHAI_INSERT", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool DeleteDM_PALLET_TRANGTHAI(string pMA_DVIQLY, long PALLET_TRANGTHAI_ID, string pModifiedBy)
        {
            var paramObj = new object[] { PALLET_TRANGTHAI_ID,pMA_DVIQLY, pModifiedBy };
            var result = base.ExecScalar("DM_PALLET_TRANGTHAI_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable GetListDM_PALLET_TRANGTHAI(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_PALLET_TRANGTHAI_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
        public DataTable GetListDM_PALLET_BY_ID(string pMA_DVIQLY, long pNHOM_PALLET_ID)
        {
            var paramObj = new object[] { pMA_DVIQLY, pNHOM_PALLET_ID};
            var result = base.ExecuteDataSet("DM_PALLET_GET_LIST_ALL_BY_ID", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
        public DataTable GetListDM_PALLET_NHOM_PALLET(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_NHOM_PALLET_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
        public DataTable GetListDM_PALLET_TRANGTHAI_KEY(string pMA_DVIQLY,long pPALLET_ID, string pLOAI_TRANGTHAI, string pNGAY_TRANGTHAI)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPALLET_ID , pLOAI_TRANGTHAI, pNGAY_TRANGTHAI };
            var result = base.ExecuteDataSet("DM_PALLET_TRANGTHAI_GET_KEY", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
    }
}
