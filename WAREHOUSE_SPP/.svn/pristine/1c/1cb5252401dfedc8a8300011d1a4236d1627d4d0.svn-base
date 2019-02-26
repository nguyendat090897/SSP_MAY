using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_KHO_KHUVUC_NCC_VITRIProvider
    {
        bool InsertOrUpdateDM_KHO_KHUVUC_NCC_VITRI(DataTable dst);
        bool DeleteDM_KHO_KHUVUC_NCC_VITRI(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_VITRI_ID, string pModifiedBy);
        DataTable GetListDM_KHO_KHUVUC_NCC_VITRI(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_ID);
        bool DM_KHO_KHUVUC_NCC_VITRI_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_VITRI_ID, long pKHO_KHUVUC_NCC_ID, string pVITRI);

    }
    public class DM_KHO_KHUVUC_NCC_VITRIProvider : BaseSqlExecute, IDM_KHO_KHUVUC_NCC_VITRIProvider
    {
        public DM_KHO_KHUVUC_NCC_VITRIProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }

        public bool InsertOrUpdateDM_KHO_KHUVUC_NCC_VITRI(DataTable dst)
        {
            try
            {
                if (NumberHelper.ConvertStringToLong(dst.Rows[0]["KHO_KHUVUC_NCC_VITRI_ID"].ToString()) > 0)
                {
                    var paramObj = new object[]{
                            ConstCommon.DonViQuanLy,
                            dst.Rows[0]["KHO_KHUVUC_NCC_VITRI_ID"],
                            dst.Rows[0]["KHO_KHUVUC_NCC_ID"],
                            dst.Rows[0]["VITRI"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("DM_KHO_KHUVUC_NCC_VITRI_UPDATE", paramObj);
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
                            dst.Rows[0]["KHO_KHUVUC_NCC_ID"],
                            dst.Rows[0]["VITRI"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("DM_KHO_KHUVUC_NCC_VITRI_INSERT", paramObj);
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

        public bool DeleteDM_KHO_KHUVUC_NCC_VITRI(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_VITRI_ID, string pModifiedBy)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, pKHO_KHUVUC_NCC_VITRI_ID, pModifiedBy };
                var result = base.ExecScalar("DM_KHO_KHUVUC_NCC_VITRI_DELETE", paramObj);
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
        public DataTable GetListDM_KHO_KHUVUC_NCC_VITRI(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_ID)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, pKHO_KHUVUC_NCC_ID};
                var result = base.ExecuteDataSet("DM_KHO_KHUVUC_NCC_VITRI_GET_LIST_ALL", paramObj);
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
        public bool DM_KHO_KHUVUC_NCC_VITRI_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_NCC_VITRI_ID, long pKHO_KHUVUC_NCC_ID, string pVITRI)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pKHO_KHUVUC_NCC_VITRI_ID, pKHO_KHUVUC_NCC_ID, pVITRI };
            var result = base.ExecuteDataSet("DM_KHO_KHUVUC_NCC_VITRI_CHECKEXISTS", sqlParams);
            if ((result != null) && (ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }
    }
}
