﻿using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;
using System;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_SanPham_ThungVLProvider
    {
        int InsertorUpdateDM_SanPham_ThungVL(DataTable dtInput);
        int DeleteDM_SanPham_ThungVL(string MA_DVIQLY, int SANPHAM_THUNG_VATLY_ID, string ModifiledBy);
        DataTable GetListDM_SanPham_ThungVL(string MA_DVIQLY, int SANPHAM_ID);

    }
    public class DM_SanPham_ThungVLProvider : BaseSqlExecute, IDM_SanPham_ThungVLProvider
    {
        public DM_SanPham_ThungVLProvider(string appId, string userId) : base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }

        public int InsertorUpdateDM_SanPham_ThungVL(DataTable dtInput)
        {
            //----------------update-------------
            if (NumberHelper.ConvertStringToLong(dtInput.Rows[0]["SANPHAM_THUNG_VATLY_ID"].ToString()) > 0)
            {
                var sqlParams = new object[] {
                                                CommonDataHelper.DonViQuanLy
                                                ,dtInput.Rows[0]["SANPHAM_THUNG_VATLY_ID"]
                                                ,dtInput.Rows[0]["SANPHAM_ID"]
                                                ,dtInput.Rows[0]["KHOI_LUONG"]
                                                ,dtInput.Rows[0]["LOAI_THUNG"]
                                                ,dtInput.Rows[0]["CAO_MM"]
                                                ,dtInput.Rows[0]["RONG_MM"]
                                                ,dtInput.Rows[0]["DAI_MM"]
                                                ,dtInput.Rows[0]["BANKINH_MM"]
                                                ,dtInput.Rows[0]["THE_TICH"]
                                                ,dtInput.Rows[0]["GHI_CHU"]
                                                ,dtInput.Rows[0]["TINH_TRANG"]
                                                ,CommonDataHelper.UserName

                                             };
                var result = base.ExecScalar("DM_SANPHAM_THUNG_VATLY_UPDATE", sqlParams);
                return int.Parse(result.ToString());
            }//-------------------------insert------------------
            else
            {
                var sqlParams = new object[] {
                                                CommonDataHelper.DonViQuanLy
                                                ,dtInput.Rows[0]["SANPHAM_ID"]
                                                ,dtInput.Rows[0]["KHOI_LUONG"]
                                                ,dtInput.Rows[0]["LOAI_THUNG"]
                                                ,dtInput.Rows[0]["CAO_MM"]
                                                ,dtInput.Rows[0]["RONG_MM"]
                                                ,dtInput.Rows[0]["DAI_MM"]
                                                ,dtInput.Rows[0]["BANKINH_MM"]
                                                ,dtInput.Rows[0]["THE_TICH"]
                                                ,dtInput.Rows[0]["GHI_CHU"]
                                                ,dtInput.Rows[0]["TINH_TRANG"]
                                                ,CommonDataHelper.UserName
                                             };
                var result = base.ExecScalar("DM_SANPHAM_THUNG_VATLY_INSERT", sqlParams);
                return int.Parse(result.ToString());
            }
        }

        public int DeleteDM_SanPham_ThungVL(string MA_DVIQLY, int SANPHAM_THUNG_VATLY_ID, string ModifiledBy)
        {
            try
            {
                var paramObj = new object[] { MA_DVIQLY, SANPHAM_THUNG_VATLY_ID, ModifiledBy };
                var result = Convert.ToInt32(base.ExecScalar("DM_SANPHAM_THUNG_VATLY_DELETE", paramObj));
                return result;
            }
            catch (Exception ex) { throw ex; }
        }
        public DataTable GetListDM_SanPham_ThungVL(string MA_DVIQLY, int SANPHAM_ID)
        {
            try
            {
                var paramObj = new object[] { MA_DVIQLY, SANPHAM_ID };
                var result = base.ExecuteDataSet("DM_SANPHAM_THUNG_VATLY_GET_DATA", paramObj);
                if (result != null)
                {
                    return result.Tables[0];
                }
                return null;
            }
            catch (Exception ex) { throw ex; }

        }
    }
}
