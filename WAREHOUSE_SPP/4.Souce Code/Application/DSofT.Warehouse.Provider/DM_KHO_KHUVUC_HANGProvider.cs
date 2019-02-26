using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_KHO_KHUVUC_HANGProvider
    {
        bool InsertOrUpdateDM_KHO_KHUVUC_HANG(DataTable dst);
        bool DeleteDM_KHO_KHUVUC_HANG(string pMA_DVIQLY, long pKHO_KHUVUC_HANG_ID, string pModifiedBy);
        DataTable GetListDM_KHO_KHUVUC_HANG(string pMA_DVIQLY, long pKHO_ID, long pKHO_KHUVUC_ID);
        DataTable GetListDM_KHO_KHUVUC_HANG_TENTT(string pMA_DVIQLY);
        bool DM_KHO_KHUVUC_HANG_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_HANG_ID, long pKHO_ID, long pKHO_KHUVUC_ID, string pMA_TINHTRANG_HANG);

    }
    public class DM_KHO_KHUVUC_HANGProvider : BaseSqlExecute, IDM_KHO_KHUVUC_HANGProvider
    {
        public DM_KHO_KHUVUC_HANGProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }

        public bool InsertOrUpdateDM_KHO_KHUVUC_HANG(DataTable dst)
        {
            try
            {
                if (NumberHelper.ConvertStringToLong(dst.Rows[0]["KHO_KHUVUC_HANG_ID"].ToString()) > 0)
                {
                    var paramObj = new object[]{
                            ConstCommon.DonViQuanLy,
                            dst.Rows[0]["KHO_KHUVUC_HANG_ID"],
                            dst.Rows[0]["KHO_ID"],
                            dst.Rows[0]["KHO_KHUVUC_ID"],
                            dst.Rows[0]["MA_TINHTRANG_HANG"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("DM_KHO_KHUVUC_HANG_UPDATE", paramObj);
                    if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    var paramObj = new object[]{
                            ConstCommon.DonViQuanLy,
                            dst.Rows[0]["KHO_ID"],
                            dst.Rows[0]["KHO_KHUVUC_ID"],
                            dst.Rows[0]["MA_TINHTRANG_HANG"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("DM_KHO_KHUVUC_HANG_INSERT", paramObj);
                    if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDM_KHO_KHUVUC_HANG(string pMA_DVIQLY, long pKHO_KHUVUC_HANG_ID, string pModifiedBy)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, pKHO_KHUVUC_HANG_ID, pModifiedBy };
                var result = base.ExecScalar("DM_KHO_KHUVUC_HANG_DELETE", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetListDM_KHO_KHUVUC_HANG(string pMA_DVIQLY, long pKHO_ID, long pKHO_KHUVUC_ID)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, pKHO_ID, pKHO_KHUVUC_ID };
                var result = base.ExecuteDataSet("DM_KHO_KHUVUC_HANG_GET_LIST_ALL", paramObj);
                if (result != null)
                {
                    return result.Tables[0];
                }
                return null;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetListDM_KHO_KHUVUC_HANG_TENTT(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY};
            var result = base.ExecuteDataSet("DM_TINHTRANG_HANG_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool DM_KHO_KHUVUC_HANG_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_HANG_ID, long pKHO_ID, long pKHO_KHUVUC_ID, string pMA_TINHTRANG_HANG)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pKHO_KHUVUC_HANG_ID, pKHO_ID, pKHO_KHUVUC_ID, pMA_TINHTRANG_HANG };
            var result = base.ExecuteDataSet("DM_KHO_KHUVUC_HANG_CHECKEXISTS", sqlParams);
            if ((result != null) && (ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }
    }
}
