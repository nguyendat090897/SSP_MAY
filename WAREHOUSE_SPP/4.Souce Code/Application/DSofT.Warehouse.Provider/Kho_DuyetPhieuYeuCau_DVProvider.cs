﻿using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;
using System;

namespace DSofT.Warehouse.Provider
{
    public interface IKho_DuyetPhieuYeuCau_DVProvider
    {
        int InsertorUpdateKho_Lam_Phieu_DV(string MA_DVIQLY, DataTable dtInput,string User);
        int Update_Kho_Lam_Phieu_DV(string MA_DVIQLY, DataTable dtInput, string User);
        DataTable GetListKho_Lam_Phieu_DV(string MA_DVIQLY);
        DataSet GetListKho_Lam_Phieu_DV_By_ID(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID);
        DataTable GetListDM_KHO(string pMA_DVIQLY);
        DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID);
        DataTable GetData_Loai_DV(string MA_DVIQLY);
        DataTable GetList_LamPhieuDV_CTiet_NVL(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID);
    }
    public class Kho_DuyetPhieuYeuCau_DVProvider : BaseSqlExecute, IKho_DuyetPhieuYeuCau_DVProvider
    {
        public Kho_DuyetPhieuYeuCau_DVProvider(string appId, string userId) : base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public int Update_Kho_Lam_Phieu_DV(string MA_DVIQLY, DataTable dtInput, string User)
        {
            var sqlParams = new object[] {
                                                MA_DVIQLY
                                                ,dtInput.Rows[0]["PHIEUYEUCAU_DV_ID"]
                                                ,dtInput.Rows[0]["TRANGTHAI_DUYET"]
                                                ,dtInput.Rows[0]["NGAY_DUYET"]
                                                ,dtInput.Rows[0]["NGUOI_DUYET"]
                                                ,dtInput.Rows[0]["LYDO"]
                                                ,User

                                             };
            var result = base.ExecScalar("KO_PHIEUYEUCAU_DV_UPDATE", sqlParams);
            if (ConstCommon.NVL_NUM_NT_NEW(result.ToString()) > 0)
            {
                return ConstCommon.NVL_NUM_NT_NEW(result.ToString());
            }
            return 0;
        }
        public DataTable GetList_LamPhieuDV_CTiet_NVL(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID)
        {
            var paramObj = new object[] { MA_DVIQLY, PHIEUYEUCAU_DV_ID };
            var result = base.ExecuteDataSet("KO_PHIEUYEUCAU_DV_CTIET_NVL_GET_DATA", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetData_Loai_DV(string MA_DVIQLY)
        {
            var paramObj = new object[] { MA_DVIQLY };
            var result = base.ExecuteDataSet("DM_LOAI_DV_GETDATA", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY, pKHO_ID };
                var result = base.ExecuteDataSet("DM_KHO_KHUVUC_GET_LIST_BY_KHO", paramObj);
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
        public DataTable GetListDM_KHO(string pMA_DVIQLY)
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
        public int InsertorUpdateKho_Lam_Phieu_DV(string MA_DVIQLY, DataTable dtInput, string User)
        {
            var sqlParams = new object[] {
                                                MA_DVIQLY
                                                ,dtInput.Rows[0]["PHIEUYEUCAU_DV_ID"]
                                                ,dtInput.Rows[0]["SOPHIEU"]
                                                ,dtInput.Rows[0]["THOIGIAN_HOANTAT"]
                                                ,dtInput.Rows[0]["MA_LOAI_DICHVU"]
                                                ,dtInput.Rows[0]["GHI_CHU"]
                                                ,dtInput.Rows[0]["TRANGTHAI_DUYET"]
                                                ,dtInput.Rows[0]["NGAY_DUYET"]
                                                ,dtInput.Rows[0]["NGUOI_DUYET"]
                                                ,dtInput.Rows[0]["LYDO"]
                                                ,dtInput.Rows[0]["DIEUKIEN_THUCHIEN"]
                                                ,User

                                             };
            var result = base.ExecScalar("KO_PHIEUYEUCAU_DV_INSERT", sqlParams);
            if (ConstCommon.NVL_NUM_NT_NEW(result.ToString()) > 0)
            {
                return ConstCommon.NVL_NUM_NT_NEW(result.ToString());
            }
            return 0;
        }
        public int DeleteKho_Lam_Phieu_DV_CT(string MA_DVIQLY, int PHIEUYEUCAU_DV_CTIET_ID, string ModifiledBy)
        {
            var paramObj = new object[] { MA_DVIQLY, PHIEUYEUCAU_DV_CTIET_ID, ModifiledBy };
            var result = Convert.ToInt32(base.ExecScalar("KO_PHIEUYEUCAU_DV_CTIET_DELETE", paramObj));
            return result;
        }
        public DataTable GetListKho_Lam_Phieu_DV(string MA_DVIQLY)
        {
            var paramObj = new object[] { MA_DVIQLY};
            var result = base.ExecuteDataSet("KO_PHIEUYEUCAU_DV_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
        public DataSet GetListKho_Lam_Phieu_DV_By_ID(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID)
        {
            var paramObj = new object[] { MA_DVIQLY, PHIEUYEUCAU_DV_ID };
            var result = base.ExecuteDataSet("KO_PHIEUYEUCAU_DV_CTIET_GET_LIST_BY_IDPHIEU_YC", paramObj);
            if (result != null)
            {
                return result;
            }
            return null;

        }
    }
}
