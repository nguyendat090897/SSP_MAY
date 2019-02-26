﻿using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_LOAI_KTHUOCProvider
    {
        bool InsertOrUpdateDM_LOAI_KTHUOC(DataTable dst);
        bool DeleteDM_LOAI_KTHUOC(string pMA_DVIQLY, long pLOAI_KTHUOC_ID, string pModifiedBy);
        DataTable GetListDM_LOAI_KTHUOC(string pMA_DVIQLY);
        DataTable GetListDM_LOAI_KTHUOC_MA(string pMA_DVIQLY, string pMA_SIZE);
        DataTable GetListDM_LOAI_KTHUOC_DRC(string pMA_DVIQLY, decimal pDAI, decimal pRONG, decimal pCAO);


    }
    public class DM_LOAI_KTHUOCProvider : BaseSqlExecute, IDM_LOAI_KTHUOCProvider
    {
        public DM_LOAI_KTHUOCProvider(string appId, string userId) :
    base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public bool InsertOrUpdateDM_LOAI_KTHUOC(DataTable dst)
        {
            try
            {
                if (NumberHelper.ConvertStringToLong(dst.Rows[0]["LOAI_KTHUOC_ID"].ToString()) > 0)
                {
                    var paramObj = new object[]{
                            ConstCommon.DonViQuanLy,
                            dst.Rows[0]["LOAI_KTHUOC_ID"],
                            dst.Rows[0]["MA_SIZE"],
                            dst.Rows[0]["DAI"],
                            dst.Rows[0]["RONG"],
                            dst.Rows[0]["CAO"],
                            dst.Rows[0]["THETICH"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                            };
                    var result = base.ExecScalar("DM_LOAI_KTHUOC_UPDATE", paramObj);
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
                            dst.Rows[0]["MA_SIZE"],
                            dst.Rows[0]["DAI"],
                            dst.Rows[0]["RONG"],
                            dst.Rows[0]["CAO"],
                            dst.Rows[0]["THETICH"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                            }; 
                     var result = base.ExecScalar("DM_LOAI_KTHUOC_INSERT", paramObj);
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

       
        public bool DeleteDM_LOAI_KTHUOC(string pMA_DVIQLY, long pLOAI_KTHUOC_ID, string pModifiedBy)
        {
            try
            { 
            var paramObj = new object[] { pMA_DVIQLY, pLOAI_KTHUOC_ID, pModifiedBy };
            var result = base.ExecScalar("DM_LOAI_KTHUOC_DELETE", paramObj);
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

        public DataTable GetListDM_LOAI_KTHUOC(string pMA_DVIQLY)
        {
            try
            { 
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_LOAI_KTHUOC_GET_LIST_ALL", paramObj);
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


        public DataTable GetListDM_LOAI_KTHUOC_MA(string pMA_DVIQLY, string pMA_SIZE)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, pMA_SIZE };
                var result = base.ExecuteDataSet("DM_LOAI_KTHUOC_KIEMTRA_MA", paramObj);
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

        public DataTable GetListDM_LOAI_KTHUOC_DRC(string pMA_DVIQLY, decimal pDAI, decimal pRONG, decimal pCAO)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, pDAI, pRONG, pCAO };
                var result = base.ExecuteDataSet("DM_LOAI_KTHUOC_GET_DRC", paramObj);
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
