﻿using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_LOAIKHOProvider
    {
        bool InsertOrUpdateDM_LOAIKHO(DataTable dst);
        bool DeleteDM_LOAIKHO(string pMA_DVIQLY, long pLOAI_KTHUOC_ID, string pModifiedBy);
        DataTable GetListDM_LOAIKHO(string pMA_DVIQLY);
        DataTable DM_KHO_GET_LIST_ALL(string pMA_DVIQLY);
        DataTable GetListDM_LOAIKHO_TEN(string pMA_DVIQLY, string pTEN_LOAIKHO);

    }
    public class DM_LOAIKHOProvider : BaseSqlExecute, IDM_LOAIKHOProvider
    {
        public DM_LOAIKHOProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }

        public bool InsertOrUpdateDM_LOAIKHO(DataTable dst)
        {
            try
            {
                if (NumberHelper.ConvertStringToLong(dst.Rows[0]["LOAIKHO_ID"].ToString()) > 0)
                {
                    var paramObj = new object[]{
                            ConstCommon.DonViQuanLy,
                            dst.Rows[0]["LOAIKHO_ID"],
                            dst.Rows[0]["TEN_LOAIKHO"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                            };
                    var result = base.ExecScalar("DM_LOAIKHO_UPDATE", paramObj);
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
                            dst.Rows[0]["TEN_LOAIKHO"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                            };
                    var result = base.ExecScalar("DM_LOAIKHO_INSERT", paramObj);
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

        public bool DeleteDM_LOAIKHO(string pMA_DVIQLY, long pLOAI_KTHUOC_ID, string pModifiedBy)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, pLOAI_KTHUOC_ID, pModifiedBy };
                var result = base.ExecScalar("DM_LOAIKHO_DELETE", paramObj);
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
        public DataTable GetListDM_LOAIKHO(string pMA_DVIQLY)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY };
                var result = base.ExecuteDataSet("DM_LOAIKHO_GET_LIST_ALL", paramObj);
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


        public DataTable DM_KHO_GET_LIST_ALL(string pMA_DVIQLY)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY };
                var result = base.ExecuteDataSet("DM_KHO_GET_LIST_ALL", paramObj);
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

        public DataTable GetListDM_LOAIKHO_TEN(string pMA_DVIQLY, string pTEN_LOAIKHO)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, pTEN_LOAIKHO };
                var result = base.ExecuteDataSet("DM_LOAIKHO_GET_TEN", paramObj);
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