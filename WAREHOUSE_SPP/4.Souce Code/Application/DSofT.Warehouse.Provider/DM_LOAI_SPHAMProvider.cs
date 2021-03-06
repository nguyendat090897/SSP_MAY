﻿using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{//
    public interface IDM_LOAI_SPHAMProvider
    {
        bool InsertorUpdateDM_LOAI_SPHAM(DataTable ds);
        bool DeleteDM_LOAI_SPHAM(string pMA_DVIQLY, long pLOAI_SPHAM_ID, string pModifiedBy);
        DataTable GetListDM_LOAI_SPHAM(string pMA_DVIQLY);
        DataTable GetListDM_NHOM_SPHAM_COMBOBOX(string pMA_DVIQLY);
        DataTable GetListDM_LOAI_SPHAM_KEY(string pMA_DVIQLY, string pMA_LOAI_SPHAM);
        DataTable GetListDM_SPHAM_BY_ID(string pMA_DVIQLY, long pNHOM_SPHAM_ID);
    }
    public class DM_LOAI_SPHAMProvider : BaseSqlExecute, IDM_LOAI_SPHAMProvider
    {
        public DM_LOAI_SPHAMProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public bool InsertorUpdateDM_LOAI_SPHAM(DataTable dst)
        {
            if (NumberHelper.ConvertStringToLong(dst.Rows[0]["LOAI_SPHAM_ID"].ToString()) > 0)
            {
                var paramObj = new object[]{ ConstCommon.DonViQuanLy,
                            dst.Rows[0]["LOAI_SPHAM_ID"],
                            dst.Rows[0]["MA_LOAI_SPHAM"],
                            dst.Rows[0]["TEN_LOAI_SPHAM"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["NHOM_SPHAM_ID"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                             };
            var result = base.ExecScalar("DM_LOAI_SPHAM_UPDATE", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                var paramObj = new object[]{ConstCommon.DonViQuanLy,
                            dst.Rows[0]["MA_LOAI_SPHAM"],
                            dst.Rows[0]["TEN_LOAI_SPHAM"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["NHOM_SPHAM_ID"],
                            dst.Rows[0]["RowVersion"],
                         CommonDataHelper.UserName
                             };
            var result =base.ExecScalar("DM_LOAI_SPHAM_INSERT", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool DeleteDM_LOAI_SPHAM(string pMA_DVIQLY, long pLOAI_SPHAM_ID, string pModifiedBy)
        {
            var paramObj = new object[] { pMA_DVIQLY, pLOAI_SPHAM_ID, pModifiedBy };
            var result = base.ExecScalar("DM_LOAI_SPHAM_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable GetListDM_LOAI_SPHAM(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_LOAI_SPHAM_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }

        public DataTable GetListDM_NHOM_SPHAM_COMBOBOX(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_NHOM_SPHAM_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_LOAI_SPHAM_KEY(string pMA_DVIQLY, string pMA_LOAI_SPHAM)
        {
            var paramObj = new object[] { pMA_DVIQLY, pMA_LOAI_SPHAM };
            var result = base.ExecuteDataSet("DM_LOAI_SPHAM_GET_KEY", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetListDM_SPHAM_BY_ID(string pMA_DVIQLY, long pNHOM_SPHAM_ID)
        {
            var paramObj = new object[] { pMA_DVIQLY, pNHOM_SPHAM_ID };
            var result = base.ExecuteDataSet("DM_LOAI_SPHAM_GET_LIST_ALL_BY_ID", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
    }
}