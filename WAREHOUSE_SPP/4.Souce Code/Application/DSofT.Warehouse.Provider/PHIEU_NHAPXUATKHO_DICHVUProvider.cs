﻿using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DSofT.Warehouse.Provider
{
    public interface IPHIEU_NHAPXUATKHO_DICHVUProvider
    {
        
        DataSet getKO_PHIEU_NHAPXUATKHO_ALL_PHIEU_YEU_CAU(string pMA_DVIQLY,string pX_OR_N);
        DataSet getKO_PHIEU_NHAPXUATKHO_ALL(string pMA_DVIQLY, string pX_OR_N);
        DataSet getKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID);
        DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_LIST_ALL(string pMA_DVIQLY, long pID_DV);
        DataSet KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_LIST_ALL(string pMA_DVIQLY, long pID_DV);

        DataTable GetData_For_gird_VITRI_HANG(string pMA_DVIQLY,long pKHO_ID);
        DataTable GetData_For_gird_TINHTRANG_HANG(string pMA_DVIQLY);
        DataTable GetData_For_gird_TINHTRANG_CV(string pMA_DVIQLY);
        DataTable GetData_For_gird_TENKHO_KHUVUC(string pMA_DVIQLY);
        DataTable GetListDM_PALLET(string pMA_DVIQLY);
        DataTable GetListSO_PHIEU_KIEM_KE(string pMA_DVIQLY);
        DataTable GetListDM_DONVI_TINH_COMBOBOX();
        DataTable GetListSO_LUONG_QUYDOI(string pMA_DVIQLY,string pMA_DON_VI_TINH, long pSANPHAM_ID);

        DataTable GetData_For_Cbo_KhachHang_Ncc(string pMA_DVIQLY);
        DataTable getDIADIEM_XUATHANG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable getSO_HOPDONG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable getDM_HINHTHU_NHAPXUAT(string pMA_DVIQLY);
        DataTable getDONVIVANCHUYEN_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE(string pMA_DVIQLY, string pMA_ITEM_TYPE);
        DataTable GetListDM_KHO(string pMA_DVIQLY);
        DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID);
        bool KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DV_ID, string pSOPHIEU);
        DataTable DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, string pUser);
        bool KO_PHIEUYEUCAU_NHAPXUATKHO_DELETE_BYID(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pUser);
        DataTable KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, string pUser);
        DataTable KO_PHIEU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, string pUser);
        bool InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO(DataTable dst, string pUser, string pX_OR_N);
        bool InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO_CTIET(DataRow dst, string pUser);
        long InsertorUpdateKO_PHIEU_NHAPXUATKHO(DataTable dst, string pUser, string pX_OR_N);
        long InsertorUpdateKO_PHIEU_NHAPXUATKHO_CTIET(DataRow dst, string pUser);
        DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_BY_ID(string pMA_DVIQLY, long pID_XUAT);
        DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG(string pMA_DVIQLY, long pPHIEUYEUCAU_DV_ID, long pSANPHAM_ID);
        DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_ID(string pMA_DVIQLY,long pPHIEU_NHAPXUATKHO_ID, long pPHIEUYEUCAU_DV_ID, long pSANPHAM_ID);
        bool KO_PHIEU_NHAPXUATKHO_CHECKEXISTS_DICHVU_NHAPVTTL(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DV_ID, long pSANPHAM_ID);
        DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_VTTL(string pMA_DVIQLY, long pPHIEUYEUCAU_DV_ID, long pSANPHAM_ID);
    }
    public class PHIEU_NHAPXUATKHO_DICHVUProvider : BaseSqlExecute, IPHIEU_NHAPXUATKHO_DICHVUProvider
    {
        public PHIEU_NHAPXUATKHO_DICHVUProvider(string appId, string userId) :
         base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }

        public DataTable GetListDM_DONVI_TINH_COMBOBOX()
        {
            var paramObj = new object[] { };
            var result = base.ExecuteDataSet("DM_DONVI_TINH_GET_COMBOBOX", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataSet getKO_PHIEU_NHAPXUATKHO_ALL_PHIEU_YEU_CAU(string pMA_DVIQLY,string pX_OR_N)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pX_OR_N };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_GET_PHIEUYEUCAU", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;

        }
        public DataSet getKO_PHIEU_NHAPXUATKHO_ALL(string pMA_DVIQLY, string pX_OR_N)
        {
            var paramObj = new object[] { pMA_DVIQLY, pX_OR_N };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_GET_ALL", paramObj);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public DataSet getKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_GET_BY_ID", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;

        }

        public DataTable GetData_For_gird_VITRI_HANG(string pMA_DVIQLY, long pKHO_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pKHO_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_get_VITRI_HANG", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListSO_LUONG_QUYDOI(string pMA_DVIQLY, string pMA_DON_VI_TINH, long pSANPHAM_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pMA_DON_VI_TINH, pSANPHAM_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_GET_SOLUONG_QUYDOI", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetData_For_gird_TINHTRANG_HANG(string pMA_DVIQLY)
        {
            var sqlParams = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_get_TINHTRANG_HANG", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetData_For_gird_TINHTRANG_CV(string pMA_DVIQLY)
        {
            var sqlParams = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_get_TINHTRANG_CV", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetData_For_gird_TENKHO_KHUVUC(string pMA_DVIQLY)
        {
            var sqlParams = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_get_TENKHO_KHUVUC", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_PALLET(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_PALLET_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListSO_PHIEU_KIEM_KE(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_GET_PHIEU_KIEM_KE", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }


        public DataTable GetData_For_Cbo_KhachHang_Ncc(string pMA_DVIQLY)
        {
            var sqlParams = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_SPHAM_GET_KHACHHANG_NCC", sqlParams);

            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable getDIADIEM_XUATHANG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pKHACHHANG_NCC_ID };
            var result = base.ExecuteDataSet("getDIADIEM_XUATHANG_BY_NCC", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
        public DataTable getSO_HOPDONG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pKHACHHANG_NCC_ID };
            var result = base.ExecuteDataSet("getSO_HOPDONG_BY_NCC", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable getDM_HINHTHU_NHAPXUAT(string pN_OR_X)
        {
            var sqlParams = new object[] { pN_OR_X };
            var result = base.ExecuteDataSet("getDM_HINHTHU_NHAPXUAT_NVL", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable getDONVIVANCHUYEN_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {

            var sqlParams = new object[] { pMA_DVIQLY, pKHACHHANG_NCC_ID };
            var result = base.ExecuteDataSet("getDONVIVANCHUYEN_BY_NCC", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE(string pMA_DVIQLY, string pMA_ITEM_TYPE)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pMA_ITEM_TYPE };
            var result = base.ExecuteDataSet("DM_SANPHAM_GET_LIST_ALL_BY_TYPE", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
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
        public bool KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DV_ID, string pSOPHIEU)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_DV_ID, pSOPHIEU };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_CHECKEXISTS", sqlParams);
            if ((result != null) && (ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }
        public DataTable DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, string pUser)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID, pUser };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_DELETE_BY_ID", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool KO_PHIEUYEUCAU_NHAPXUATKHO_DELETE_BYID(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pUser)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEUYEUCAU_NHAPXUATKHO_ID, pUser };
            var result = base.ExecuteDataSet("KO_PHIEUYEUCAU_NHAPXUATKHO_DELETE_BYID", sqlParams);
            if ((result != null) && (ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }
        public DataTable KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, string pUser)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, pUser };
            var result = base.ExecuteDataSet("PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable KO_PHIEU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, string pUser)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_CTIET_ID, pUser };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_CTIET_DELETE", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO(DataTable dst, string pUser, string pX_OR_N)
        {
            var paramObj = new object[]{
                            dst.Rows[0]["MA_DVIQLY"],pX_OR_N,
                            ConstCommon.NVL_NUM_LONG_NEW (dst.Rows[0]["PHIEUYEUCAU_NHAPXUATKHO_ID"]),
                            dst.Rows[0]["KHACHHANG_NCC_ID"],
                            dst.Rows[0]["HOPDONG_ID"],
                            dst.Rows[0]["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"],
                            dst.Rows[0]["KHACHHANG_NCC_NOI_XUATHANG_ID"],
                            dst.Rows[0]["NGUOILIENHE_A"],
                            dst.Rows[0]["NGUOILIENHE_B"],
                            dst.Rows[0]["SO_CHUNGTU"],
                            dst.Rows[0]["NGAY_CHUNGTU"],
                            dst.Rows[0]["MA_HINHTHU_NHAPXUAT"],
                            dst.Rows[0]["SOPHIEU"],
                            dst.Rows[0]["NGAYLAP"],
                            dst.Rows[0]["TEN_TAIXE"],
                            dst.Rows[0]["SO_DIENTHOAI"],
                            dst.Rows[0]["SO_XE"],
                            dst.Rows[0]["SO_CONT"],
                            dst.Rows[0]["GHI_CHU"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEUYEUCAU_NHAPXUATKHO_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO_CTIET(DataRow dst, string pUser)
        {
            var paramObj = new object[]{
                            dst["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst["PHIEUYEUCAU_NHAPXUATKHO_CTIET_ID"]),
                            dst["PHIEUYEUCAU_NHAPXUATKHO_ID"],
                            dst["KHO_ID"],
                            dst["KHO_KHUVUC_ID"],
                            dst["MA_ITEM_TYPE"],
                            dst["SANPHAM_ID"],
                            dst["SOLO"],
                            dst["HANDUNG"],
                            dst["SO_LUONG"],
                            dst["DONGIA"],
                            dst["THANHTIEN"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public long InsertorUpdateKO_PHIEU_NHAPXUATKHO(DataTable dst, string pUser, string pX_OR_N)
        {
            var paramObj = new object[]{
                            dst.Rows[0]["MA_DVIQLY"],pX_OR_N,
                            ConstCommon.NVL_NUM_LONG_NEW (dst.Rows[0]["PHIEU_NHAPXUATKHO_ID"]),
                            dst.Rows[0]["PHIEUYEUCAU_DV_ID"],
                            dst.Rows[0]["MA_HINHTHU_NHAPXUAT"],
                            dst.Rows[0]["SOPHIEU"],
                            dst.Rows[0]["NGAYLAP"],
                            dst.Rows[0]["GHI_CHU"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_DICHVU_NVL_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }
        public long InsertorUpdateKO_PHIEU_NHAPXUATKHO_CTIET(DataRow dst, string pUser)
        {
            var paramObj = new object[]{
                            dst["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst["PHIEU_NHAPXUATKHO_CTIET_ID"]),
                            dst["PHIEU_NHAPXUATKHO_ID"],
                            dst["KHO_ID"],
                            dst["KHO_KHUVUC_ID"],
                            dst["MA_ITEM_TYPE"],
                            dst["SANPHAM_ID"],
                            dst["SO_LUONG_TONG"],
                            dst["MA_TINHTRANG_HANG"],
                            dst["VITRI"],
                            dst["MA_VACH"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_DICHVU_NVL_CTIET_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }

        public DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_LIST_ALL(string pMA_DVIQLY, long pID_DV)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pID_DV };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_DICHVU_GET_LIST_ALL", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }

        public DataSet KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_LIST_ALL(string pMA_DVIQLY, long pID_DV)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pID_DV };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_LIST_ALL", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;

        }

        public DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_BY_ID(string pMA_DVIQLY, long pID_XUAT)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pID_XUAT };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_BY_ID", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }

        public DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG(string pMA_DVIQLY, long pPHIEUYEUCAU_DV_ID, long pSANPHAM_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEUYEUCAU_DV_ID, pSANPHAM_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, long pPHIEUYEUCAU_DV_ID, long pSANPHAM_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID, pPHIEUYEUCAU_DV_ID, pSANPHAM_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_ID", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public bool KO_PHIEU_NHAPXUATKHO_CHECKEXISTS_DICHVU_NHAPVTTL(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DV_ID, long pSANPHAM_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_DV_ID, pSANPHAM_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_CHECKEXISTS_DICHVU_NHAPVTTL", sqlParams);
            if ((result != null) && (ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }

        public DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_VTTL(string pMA_DVIQLY, long pPHIEUYEUCAU_DV_ID, long pSANPHAM_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEUYEUCAU_DV_ID, pSANPHAM_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_VTTL", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
    }
}
