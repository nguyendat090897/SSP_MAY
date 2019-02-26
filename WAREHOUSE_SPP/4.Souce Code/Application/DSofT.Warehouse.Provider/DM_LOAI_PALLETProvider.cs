﻿using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_LOAI_PALLETProvider
    {
        bool InsertorUpdateDM_LOAI_PALLET(DataTable dst);
        bool DeleteDM_LOAI_PALLET(string pMA_DVIQLY, long pDM_LOAI_PALLETID, string pModifiedBy);
        //bool DeleteDM_LOAI_PALLET(DataTable dtInput);
        DataTable GetListDM_LOAI_PALLET(string pMA_DVIQLY);
        // DataTable GetListDM_LOAI_PALLET(DataTable dtInput);
        DataTable GetListDM_LOAI_KICKTHUOC_COMBOBOX(string pMA_DVIQLY);
        DataTable GetListDM_LOAI_PALLET_GET_ALL_SO_LUONG(string pMA_DVIQLY);
        DataTable GetListDM_LOAI_PALLET_GET_ALL_SO_LUONG_BY_LOAI_PALLET_ID(string pMA_DVIQLY, string pLOAI_PALLET_ID);
        DataTable GetListDM_LOAI_PALLET_KEY(string pMA_DVIQLY, string pTEN_LOAI_PALLET, long pLOAI_KTHUOC_ID);
    }
    public class DM_LOAI_PALLETProvider : BaseSqlExecute, IDM_LOAI_PALLETProvider
    {
        public DM_LOAI_PALLETProvider(string appId, string userId) :
            base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public bool InsertorUpdateDM_LOAI_PALLET(DataTable dst)
        {
            if (NumberHelper.ConvertStringToLong(dst.Rows[0]["LOAI_PALLET_ID"].ToString()) > 0)
            {
                var paramObj = new object[]{ConstCommon.DonViQuanLy,
                            dst.Rows[0]["LOAI_PALLET_ID"],
                            dst.Rows[0]["TEN_LOAI_PALLET"],
                            dst.Rows[0]["LOAI_KTHUOC_ID"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                            //dst.Rows[0]["ModifiedBy"],
                             };
                var result = base.ExecScalar("DM_LOAI_PALLET_UPDATE", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                var paramObj = new object[]{ConstCommon.DonViQuanLy,
                            dst.Rows[0]["TEN_LOAI_PALLET"],
                            dst.Rows[0]["LOAI_KTHUOC_ID"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                            //dst.Rows[0]["CreateBy"]
                             };
                var result = base.ExecScalar("DM_LOAI_PALLET_INSERT", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool DeleteDM_LOAI_PALLET(string pMA_DVIQLY, long pDM_LOAI_PALLETID, string pModifiedBy)
        {
            var paramObj = new object[] { pMA_DVIQLY, pDM_LOAI_PALLETID, pModifiedBy };
           // var paramObj = new object[] { ConstCommon.DonViQuanLy,dtInput.Rows[0]["LOAI_PALLET_ID"]
             //                                , CommonDataHelper.UserName };
            var result = base.ExecScalar("DM_LOAI_PALLET_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable GetListDM_LOAI_PALLET(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
           // var paramObj = new object[] { dtInput.Rows[0]["MA_DVIQLY"]};
            var result = base.ExecuteDataSet("DM_LOAI_PALLET_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_LOAI_PALLET_GET_ALL_SO_LUONG(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            // var paramObj = new object[] { dtInput.Rows[0]["MA_DVIQLY"]};
            var result = base.ExecuteDataSet("DM_LOAI_PALLET_GET_ALL_SO_LUONG", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_LOAI_PALLET_GET_ALL_SO_LUONG_BY_LOAI_PALLET_ID(string pMA_DVIQLY, string pLOAI_PALLET_ID)
        {
            var paramObj = new object[] { pMA_DVIQLY, pLOAI_PALLET_ID };
            var result = base.ExecuteDataSet("DM_LOAI_PALLET_GET_ALL_SO_LUONG_BY_LOAI_PALLET_ID", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_LOAI_KICKTHUOC_COMBOBOX(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_LOAI_KTHUOC_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_LOAI_PALLET_KEY(string pMA_DVIQLY, string pTEN_LOAI_PALLET, long pLOAI_KTHUOC_ID)
        {
            var paramObj = new object[] { pMA_DVIQLY, pTEN_LOAI_PALLET, pLOAI_KTHUOC_ID };
            var result = base.ExecuteDataSet("DM_LOAI_PALLET_GET_KEY", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
    }
}
