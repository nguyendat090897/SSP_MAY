using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_KHO_KHUVUCProvider
    {
        bool InsertOrUpdateDM_KHO_KHUVUC(DataTable dst);
        bool DeleteDM_KHO_KHUVUC(string pMA_DVIQLY, long pKHO_KHUVUC_ID, string pModifiedBy);
        DataTable GetListDM_KHO_KHUVUC(string pMA_DVIQLY, long pKHO_ID);
        bool DM_KHO_KHUVUC_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_ID, long pKHO_ID, string pMAKHO);

    }
    public class DM_KHO_KHUVUCProvider : BaseSqlExecute, IDM_KHO_KHUVUCProvider
    {
        public DM_KHO_KHUVUCProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }

        public bool InsertOrUpdateDM_KHO_KHUVUC(DataTable dst)
        {
            try
            {
                if (NumberHelper.ConvertStringToLong(dst.Rows[0]["KHO_KHUVUC_ID"].ToString()) > 0)
                {
                    var paramObj = new object[]{
                            ConstCommon.DonViQuanLy,
                            dst.Rows[0]["KHO_KHUVUC_ID"],
                            dst.Rows[0]["KHO_ID"],
                            dst.Rows[0]["MA_KHO_KHUVUC"],
                            dst.Rows[0]["TEN_KHO_KHUVUC"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("DM_KHO_KHUVUC_UPDATE", paramObj);
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
                            dst.Rows[0]["MA_KHO_KHUVUC"],
                            dst.Rows[0]["TEN_KHO_KHUVUC"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("DM_KHO_KHUVUC_INSERT", paramObj);
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

        public bool DeleteDM_KHO_KHUVUC(string pMA_DVIQLY, long pKHO_KHUVUC_ID, string pModifiedBy)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, pKHO_KHUVUC_ID, pModifiedBy };
                var result = base.ExecScalar("DM_KHO_KHUVUC_DELETE", paramObj);
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
        public DataTable GetListDM_KHO_KHUVUC(string pMA_DVIQLY, long pKHO_ID)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, pKHO_ID };
                var result = base.ExecuteDataSet("DM_KHO_KHUVUC_GET_LIST_ALL", paramObj);
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

        public bool DM_KHO_KHUVUC_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_ID, long pKHO_ID, string pMAKHO)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pKHO_KHUVUC_ID, pKHO_ID, pMAKHO };
            var result = base.ExecuteDataSet("DM_KHO_KHUVUC_CHECKEXISTS", sqlParams);
            if ((result != null) && (ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }
    }
}
