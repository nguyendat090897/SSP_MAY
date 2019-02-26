using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_PALLETProvider
    {
        bool InsertorUpdateDM_PALLET(DataTable dst);
        bool DeleteDM_PALLET(string pMA_DVIQLY, long pPALLET_ID, string pModifiedBy);
        DataTable GetListDM_PALLET(string pMA_DVIQLY);
        DataTable GetListDM_PALLET_BY_ID(string pMA_DVIQLY, long pNHOM_PALLET_ID);
        DataTable GetListDM_PALLET_NHOM_PALLET(string pMA_DVIQLY);
        DataTable GetListDM_PALLET_KHU_QUAN_LY(string pMA_DVIQLY);
        DataTable GetListDM_PALLET_KEY(string pMA_DVIQLY, string pMA_PALLET);
    }
    public class DM_PALLETProvider : BaseSqlExecute, IDM_PALLETProvider
    {
        public DM_PALLETProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public bool InsertorUpdateDM_PALLET(DataTable dst)
        {
            if (NumberHelper.ConvertStringToLong(dst.Rows[0]["PALLET_ID"].ToString()) > 0)
            {
                var paramObj = new object[]{dst.Rows[0]["PALLET_ID"],
                            ConstCommon.DonViQuanLy,
                            dst.Rows[0]["MA_PALLET"],
                            dst.Rows[0]["MA_VACH"],
                            dst.Rows[0]["TEN_PALLET"],
                            dst.Rows[0]["NHOM_PALLET_ID"],
                            dst.Rows[0]["LOAI_PALLET_ID"],
                            dst.Rows[0]["LOAI_KTHUOC_ID"],
                            dst.Rows[0]["NHA_SXUAT_ID"],
                            dst.Rows[0]["MA_PL_THEO_NSX"],
                            dst.Rows[0]["TEN_PL_THEO_NSX"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                             CommonDataHelper.UserName
                             };

                var result = base.ExecScalar("DM_PALLET_UPDATE", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                var paramObj = new object[]{ ConstCommon.DonViQuanLy,
                            dst.Rows[0]["MA_PALLET"],
                            dst.Rows[0]["MA_VACH"],
                            dst.Rows[0]["TEN_PALLET"],
                            dst.Rows[0]["NHOM_PALLET_ID"],
                            dst.Rows[0]["LOAI_PALLET_ID"],
                            dst.Rows[0]["LOAI_KTHUOC_ID"],
                            dst.Rows[0]["NHA_SXUAT_ID"],
                            dst.Rows[0]["MA_PL_THEO_NSX"],
                            dst.Rows[0]["TEN_PL_THEO_NSX"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                             };

                var result = base.ExecScalar("DM_PALLET_INSERT", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool DeleteDM_PALLET(string pMA_DVIQLY, long pPALLET_ID, string pModifiedBy)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPALLET_ID, pModifiedBy };
            var result = base.ExecScalar("DM_PALLET_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable GetListDM_PALLET(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_PALLET_GET_LIST_ALL", paramObj);
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
        public DataTable GetListDM_PALLET_KHU_QUAN_LY(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_LOAI_PALLET_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
        public DataTable GetListDM_PALLET_KEY(string pMA_DVIQLY,string pMA_PALLET)
        {
            var paramObj = new object[] { pMA_DVIQLY, pMA_PALLET};
            var result = base.ExecuteDataSet("DM_PALLET_GET_KEY", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
    }
}
