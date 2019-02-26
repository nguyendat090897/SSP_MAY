﻿using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_SANPHAM_SLUONG_MINMAX_KHUVUCProvider
    {
        bool InsertOrUpdateDM_SANPHAM_SLUONG_MINMAX_KHUVUC(DataTable dst);
        DataTable DeleteDM_SANPHAM_SLUONG_MINMAX_KHUVUC(string pMA_DVIQLY, long @pSANPHAM_SLUONG_MINMAX_KHUVUC_ID, string pModifiedBy);
        DataTable GetListDM_SANPHAM_SLUONG_MINMAX_KHUVUC(string pMA_DVIQLY, long pKHO_KHUVUC_ID);
        
    }
    public class DM_SANPHAM_SLUONG_MINMAX_KHUVUCProvider : BaseSqlExecute, IDM_SANPHAM_SLUONG_MINMAX_KHUVUCProvider
    {
        public DM_SANPHAM_SLUONG_MINMAX_KHUVUCProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }

        public bool InsertOrUpdateDM_SANPHAM_SLUONG_MINMAX_KHUVUC(DataTable dst)
        {
            try
            {
                    var paramObj = new object[]{
                            ConstCommon.DonViQuanLy,
                            dst.Rows[0]["SANPHAM_SLUONG_MINMAX_KHUVUC_ID"],
                            dst.Rows[0]["KHO_KHUVUC_ID"],
                            dst.Rows[0]["SANPHAM_ID"],
                            dst.Rows[0]["SL_MIN"],
                            dst.Rows[0]["SL_MAX"],
                            dst.Rows[0]["VITRI"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("DM_SANPHAM_SLUONG_MINMAX_KHUVUC_INSERT", paramObj);
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

        public DataTable DeleteDM_SANPHAM_SLUONG_MINMAX_KHUVUC(string pMA_DVIQLY, long @pSANPHAM_SLUONG_MINMAX_KHUVUC_ID, string pModifiedBy)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, @pSANPHAM_SLUONG_MINMAX_KHUVUC_ID, pModifiedBy };
                var result = base.ExecuteDataSet("DM_SANPHAM_SLUONG_MINMAX_KHUVUC_DELETE", paramObj);
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
        public DataTable GetListDM_SANPHAM_SLUONG_MINMAX_KHUVUC(string pMA_DVIQLY, long pKHO_KHUVUC_ID)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, pKHO_KHUVUC_ID };
                var result = base.ExecuteDataSet("DM_SANPHAM_SLUONG_MINMAX_KHUVUC_GET_LIST_ALL", paramObj);
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
    }
}
