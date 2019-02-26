﻿using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using DSofT.Warehouse.Domain.Constant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DSofT.Warehouse.Provider
{
    public interface IPHIEU_NHAPXUATKHOProvider
    {
        
        DataSet getKO_PHIEU_NHAPXUATKHO_ALL_PHIEU_YEU_CAU(string pMA_DVIQLY,string pX_OR_N);
        DataSet getKO_PHIEU_NHAPXUATKHO_ALL(string pMA_DVIQLY, string pX_OR_N);
        DataSet getKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID);
        DataSet KO_PHIEU_NHAPXUATKHO_GET_NHAP_NHIEU_LAN(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID);


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
        bool KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pSOPHIEU);
        bool DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, string pUser);
        bool KO_PHIEU_NHAPXUATKHO_NHAP_NHIEU_LAN_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, string pUser);
        bool KO_PHIEUYEUCAU_NHAPXUATKHO_DELETE_BYID(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pUser);
        DataTable KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, string pUser);
        DataTable KO_PHIEU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, string pUser);
        bool InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO(DataTable dst, string pUser, string pX_OR_N);
        bool InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO_CTIET(DataRow dst, string pUser);
        long InsertorUpdateKO_PHIEU_NHAPXUATKHO(DataTable dst, string pUser, string pX_OR_N);
        long InsertorUpdateKO_PHIEU_NHAPXUATKHO_CTIET(DataRow dst, string pUser);
        bool InsertKO_PHIEU_NHAPXUATKHO_CTIET(DataRow dst, string pUser);
        bool KO_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, string pSOPHIEU);


        long InsertorUpdateKO_PHIEU_NHAPXUATKHO_DC(DataTable dst, string pUser, string pLOAI_DC);
        long InsertorUpdateKO_PHIEU_NHAPXUATKHO_DC_CTIET(DataRow dst, string pUser);
        DataSet getKO_PHIEU_NHAPXUATKHO_DC_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DC_ID);
        DataSet KO_PHIEU_NHAPXUATKHO_DC_GET_ALL(string pMA_DVIQLY);
        bool KO_PHIEU_NHAPXUATKHO_DC_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DC_ID, string pUser);
        DataSet KO_PHIEU_NHAPXUATKHO_GET_BY_DC(string pMA_DVIQLY, string pX_OR_N);
        bool KO_PHIEU_NHAPXUATKHO_DC_CT_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DC_CTIET_ID, string pUser);
        bool KO_PHIEU_NHAPXUATKHO_DC_UPDATE(DataTable dst, string pUser);


        DataSet KO_PHIEU_NHAPXUATKHO_DCNB_GETALL(string pMA_DVIQLY);
        long InsertorUpdateKO_PHIEU_NHAPXUATKHO_DCNB(DataTable dst, string pUser, string pLOAI_DCHUYEN);
        DataTable KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYXUAT(string pMA_DVIQLY);
        DataTable KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYNHAN(string pMA_DVIQLY);
        long InsertorUpdateKO_PHIEU_NHAPXUATKHO_DCNB_PHIEUNXK(DataTable dst, string pUser, string pX_OR_N);
        bool KO_PHIEU_NHAPXUATKHO_DCNB_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DCNB_ID, string pUser);

        DataSet KO_PHIEU_KIEMKE_GET_ALL(string pMA_DVIQLY);
        long InsertorUpdateKO_PHIEU_KIEMKE(DataTable dst, string pUser);
        long InsertorUpdateKO_PHIEU_KIEMKE_CTIET(DataRow dst, string pUser);
        bool KO_PHIEU_KIEMKE_DELETE(string pMA_DVIQLY, long pPHIEU_KIEMKE_ID, string pUser);
        DataSet KO_PHIEU_KIEMKE_GET_BY_ID(string pMA_DVIQLY, long pPHIEU_KIEMKE_ID);
        bool UpdateKO_PHIEU_NHAPXUATKHO_CTIET(DataRow dst, long pPHIEU_KIEMKE_ID, int pSO_LUONG_TONG, int pSO_LUONG_THUNG, int pSO_LUONG_LE,
            string pX_OR_N, string pUser);


        DataSet KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_BY_PHIEU(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, long pPHIEU_NHAPXUATKHO_CTIET_ID, long pSANPHAM_ID);
        DataSet KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_ALL(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID);
        long InsertorUpdateKO_PHIEU_NHAPXUATKHO_CTIET_VTRI(DataRow dst, string pUser);
       
        int GET_SO_LUONG_SAN_PHAM_NHAP(string pMA_DVIQLY, long pKHO_ID, long pSANPHAM_ID, string pso_lo, string phan_dung, string pVITRI);
        int GET_SO_LUONG_SAN_PHAM_XUAT(string pMA_DVIQLY, long pKHO_ID, long pSANPHAM_ID, string pso_lo, string phan_dung, string pVITRI);
        DataTable GET_ALL_SANPHAM_TONKHO_ONLINE_THEO_VITRI(string pMA_DVIQLY, long pSANPHAM_ID, long pKHO_ID, string pVITRI);
        bool HT_TONKHO_ONLINE_UPDATE(DataTable dst, string pUser);
        bool  HT_TONKHO_ONLINE_INSERT(string pMA_DVIQLY, long pKHO_ID, string pVITRI, long SANPHAM_ID, int so_luong_ton, string pUser);
        bool HT_TONKHO_ONLINE_DELETE(string pMA_DVIQLY, long pTONKHO_ONLINE_ID, string pUser);
        bool KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, bool pIS_GHISO);
        bool KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO_ALL(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, bool pIS_GHISO);
    }
    public class PHIEU_NHAPXUATKHOProvider : BaseSqlExecute, IPHIEU_NHAPXUATKHOProvider
    {
        public PHIEU_NHAPXUATKHOProvider(string appId, string userId) :
         base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        #region Nhập kho nhập nhiều lần poup
        public DataSet KO_PHIEU_NHAPXUATKHO_GET_NHAP_NHIEU_LAN(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_CT_GET_SOLUONG_TON", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public bool KO_PHIEU_NHAPXUATKHO_NHAP_NHIEU_LAN_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, string pUser)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_CTIET_ID, pUser };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_CTIET_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Nhập kho ,nhập kho poup, phiếu yêu cầu poup
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
            var result = base.ExecuteDataSet("getDM_HINHTHU_NHAPXUAT", sqlParams);
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
        public bool KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pSOPHIEU)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEUYEUCAU_NHAPXUATKHO_ID, pSOPHIEU };
            var result = base.ExecuteDataSet("KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS", sqlParams);
            if ((result != null) && (ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }
        public bool DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, string pUser)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID, pUser };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_DELETE_BY_ID", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
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
            try
            {
                var paramObj = new object[]{
                            dst.Rows[0]["MA_DVIQLY"],pX_OR_N,
                            ConstCommon.NVL_NUM_LONG_NEW (dst.Rows[0]["PHIEU_NHAPXUATKHO_ID"]),
                            //dst.Rows[0]["PHIEUYEUCAU_NHAPXUATKHO_ID"],
                            dst.Rows[0]["KHACHHANG_NCC_ID"],
                            //dst.Rows[0]["HOPDONG_ID"],
                            //dst.Rows[0]["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"],
                            //dst.Rows[0]["KHACHHANG_NCC_NOI_XUATHANG_ID"],
                            //dst.Rows[0]["NGUOILIENHE_A"],
                            //dst.Rows[0]["NGUOILIENHE_B"],
                            //dst.Rows[0]["SO_CHUNGTU"],
                            //DateTimeHelper.ConvertDateTimeWithFormat(  dst.Rows[0]["NGAY_CHUNGTU"].ToString(),SystemConstant.DateFormat),
                            dst.Rows[0]["MA_HINHTHU_NHAPXUAT"],
                            dst.Rows[0]["SOPHIEU"],
                             DateTimeHelper.ConvertDateTimeWithFormat(  dst.Rows[0]["NGAYLAP"].ToString(),SystemConstant.DateFormat),
                            //dst.Rows[0]["TEN_TAIXE"],
                            //dst.Rows[0]["SO_DIENTHOAI"],
                            //dst.Rows[0]["SO_XE"],
                            //dst.Rows[0]["SO_CONT"],
                            //dst.Rows[0]["IS_NHAPNHIEULAN"],
                            //dst.Rows[0]["GHI_CHU"],
                            //dst.Rows[0]["PHIEU_KIEMKE_ID"],
                            pUser
                             };
                var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_INSERT", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return NumberHelper.ConvertStringToLong(result.ToString());
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }
        public long InsertorUpdateKO_PHIEU_NHAPXUATKHO_CTIET(DataRow dst, string pUser)
        {
            var paramObj = new object[]{
                            dst["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst["PHIEU_NHAPXUATKHO_CTIET_ID"]),
                            dst["PHIEU_NHAPXUATKHO_ID"],
                            dst["KHO_ID"],
                            //dst["KHO_KHUVUC_ID"],
                            dst["MA_ITEM_TYPE"],
                            dst["SANPHAM_ID"],
                            dst["SOLO"],
                            //dst["HANDUNG"],
                            //dst["QUYCACHDONGGOI"],
                            dst["SO_LUONG_TONG"],
                            //dst["SO_LUONG_THUNG"],
                            //dst["SO_LUONG_LE"],
                            //dst["MA_TINHTRANG_HANG"],
                            //dst["SODK"],
                            //dst["DONGIA"],
                            //dst["THANHTIEN"],
                            //dst["PALLET_ID"],
                            //dst["VITRI"],
                            //dst["MA_VACH"],
                            //dst["NGAY_SANXUAT"],
                            //dst["MA_TINHTRANG_CV"],
                            //dst["IS_NHAPNHIEULAN"],
                            //dst["SO_LUONG_TONG_THUCNHAP"],
                            //dst["SO_LUONG_THUNG_THUCNHAP"],
                            //dst["SO_LUONG_LE_THUCNHAP"],
                            //pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_CTIET_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }
        public bool InsertKO_PHIEU_NHAPXUATKHO_CTIET(DataRow dst, string pUser)
        {
            var paramObj = new object[]{
                            dst["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst["PHIEU_NHAPXUATKHO_CTIET_ID"]),
                            dst["PHIEU_NHAPXUATKHO_ID"],
                            dst["KHO_ID"],
                            dst["KHO_KHUVUC_ID"],
                            dst["MA_ITEM_TYPE"],
                            dst["SANPHAM_ID"],
                            dst["SOLO"],
                            dst["HANDUNG"],
                            dst["QUYCACHDONGGOI"],
                            dst["SO_LUONG_TONG"],
                            dst["SO_LUONG_THUNG"],
                            dst["SO_LUONG_LE"],
                            dst["MA_TINHTRANG_HANG"],
                            dst["SODK"],
                            dst["DONGIA"],
                            dst["THANHTIEN"],
                            dst["PALLET_ID"],
                            //dst["VITRI"],
                            dst["MA_VACH"],
                            dst["NGAY_SANXUAT"],
                            dst["MA_TINHTRANG_CV"],
                            dst["IS_NHAPNHIEULAN"],
                            dst["SO_LUONG_TONG_THUCNHAP"],
                            dst["SO_LUONG_THUNG_THUCNHAP"],
                            dst["SO_LUONG_LE_THUCNHAP"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_CTIET_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        public bool KO_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, string pSOPHIEU)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID, pSOPHIEU };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_CHECKEXISTS", sqlParams);
            if ((result != null) && (ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Điều chỉnh xuất kho
        public long InsertorUpdateKO_PHIEU_NHAPXUATKHO_DC(DataTable dst, string pUser,string pLOAI_DC)
        {
            var paramObj = new object[]{
                            dst.Rows[0]["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst.Rows[0]["PHIEU_NHAPXUATKHO_DC_ID"]),
                            dst.Rows[0]["PHIEU_NHAPXUATKHO_ID"],
                            pLOAI_DC,
                            dst.Rows[0]["GHI_CHU"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_DC_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }
        public long InsertorUpdateKO_PHIEU_NHAPXUATKHO_DC_CTIET(DataRow dst, string pUser)
        {
            var paramObj = new object[]{
                            dst["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst["PHIEU_NHAPXUATKHO_DC_CTIET_ID"]),
                            dst["PHIEU_NHAPXUATKHO_DC_ID"],
                            dst["KHO_ID"],
                            dst["KHO_KHUVUC_ID"],
                            dst["MA_ITEM_TYPE"],
                            dst["SANPHAM_ID"],
                            dst["SOLO"],
                            dst["HANDUNG"],
                            dst["QUYCACHDONGGOI"],
                            dst["SO_LUONG_TONG"],
                            dst["SO_LUONG_THUNG"],
                            dst["SO_LUONG_LE"],
                            dst["MA_TINHTRANG_HANG"],
                            dst["SODK"],
                            dst["DONGIA"],
                            dst["THANHTIEN"],
                            dst["PALLET_ID"],
                            dst["VITRI"],
                            dst["MA_VACH"],
                            dst["NGAY_SANXUAT"],
                            dst["MA_TINHTRANG_CV"],
                            dst["IS_NHAPNHIEULAN"],
                            dst["SO_LUONG_TONG_THUCNHAP"],
                            dst["SO_LUONG_THUNG_THUCNHAP"],
                            dst["SO_LUONG_LE_THUCNHAP"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_DC_CTIET_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }
        public DataSet getKO_PHIEU_NHAPXUATKHO_DC_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DC_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_DC_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_DC_GET_BY_ID", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public DataSet KO_PHIEU_NHAPXUATKHO_DC_GET_ALL(string pMA_DVIQLY)
        {
            var sqlParams = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_DC_GET_ALL", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public bool KO_PHIEU_NHAPXUATKHO_DC_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DC_ID, string pUser)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_DC_ID, pUser };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_DC_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public DataSet KO_PHIEU_NHAPXUATKHO_GET_BY_DC(string pMA_DVIQLY, string pX_OR_N)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pX_OR_N };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_GET_BY_DC", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public bool KO_PHIEU_NHAPXUATKHO_DC_CT_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DC_CTIET_ID, string pUser)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_DC_CTIET_ID, pUser };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_DC_CT_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool KO_PHIEU_NHAPXUATKHO_DC_UPDATE(DataTable dst, string pUser)
        {
            var paramObj = new object[]{
                            dst.Rows[0]["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst.Rows[0]["PHIEU_NHAPXUATKHO_DC_ID"]),
                            dst.Rows[0]["GHI_CHU"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_DC_UPDATE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Điều chuyển phiếu xuất nhập kho
        public DataSet KO_PHIEU_NHAPXUATKHO_DCNB_GETALL(string pMA_DVIQLY)
        {
            var sqlParams = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_DCNB_GETALL", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public long InsertorUpdateKO_PHIEU_NHAPXUATKHO_DCNB(DataTable dst, string pUser, string pLOAI_DCHUYEN)
        {
            var paramObj = new object[]{
                            dst.Rows[0]["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst.Rows[0]["PHIEU_NHAPXUATKHO_DCNB_ID"]),
                            pLOAI_DCHUYEN,
                            dst.Rows[0]["SOPHIEU_DC"],
                            dst.Rows[0]["NGAY_DC"],
                            dst.Rows[0]["NGUOI_DC"],
                            dst.Rows[0]["SOPHIEU_NHAN"],
                            dst.Rows[0]["NGUOI_NHAN"],
                            dst.Rows[0]["NGAY_NHAN"],
                            dst.Rows[0]["MA_DVIQLY_CHUYEN"],
                            dst.Rows[0]["MA_DVIQLY_NHAN"],
                            dst.Rows[0]["GHI_CHU"],   
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_DCNB_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }
        public DataTable KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYXUAT(string pMA_DVIQLY)
        {
            var sqlParams = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYXUAT", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYNHAN(string pMA_DVIQLY)
        {
            var sqlParams = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYNHAN", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public long InsertorUpdateKO_PHIEU_NHAPXUATKHO_DCNB_PHIEUNXK(DataTable dst, string pUser, string pX_OR_N)
        {
            var paramObj = new object[]{
                            dst.Rows[0]["MA_DVIQLY"],pX_OR_N,
                            ConstCommon.NVL_NUM_LONG_NEW (dst.Rows[0]["PHIEU_NHAPXUATKHO_ID"]),
                            dst.Rows[0]["PHIEUYEUCAU_NHAPXUATKHO_ID"],
                            dst.Rows[0]["KHACHHANG_NCC_ID"],
                            dst.Rows[0]["HOPDONG_ID"],
                            dst.Rows[0]["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"],
                            dst.Rows[0]["KHACHHANG_NCC_NOI_XUATHANG_ID"],
                            dst.Rows[0]["NGUOILIENHE_A"],
                            dst.Rows[0]["NGUOILIENHE_B"],
                            dst.Rows[0]["SO_CHUNGTU"],
                            DateTimeHelper.ConvertDateTimeWithFormat(  dst.Rows[0]["NGAY_CHUNGTU"].ToString(),SystemConstant.DateFormat),
                            dst.Rows[0]["MA_HINHTHU_NHAPXUAT"],
                            dst.Rows[0]["SOPHIEU"],
                             DateTimeHelper.ConvertDateTimeWithFormat(  dst.Rows[0]["NGAYLAP"].ToString(),SystemConstant.DateFormat),
                            dst.Rows[0]["TEN_TAIXE"],
                            
                            dst.Rows[0]["SO_DIENTHOAI"],
                            dst.Rows[0]["SO_XE"],
                            dst.Rows[0]["SO_CONT"],
                            dst.Rows[0]["IS_NHAPNHIEULAN"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["PHIEU_KIEMKE_ID"],
                            dst.Rows[0]["PHIEU_NHAPXUATKHO_DCNB_ID"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_DCNB_INSERT_PHIEUNXK", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }
        public bool KO_PHIEU_NHAPXUATKHO_DCNB_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DCNB_ID, string pUser)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_DCNB_ID, pUser };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_DCNB_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Kiểm kê hàng hóa
        public DataSet KO_PHIEU_KIEMKE_GET_ALL(string pMA_DVIQLY)
        {
            var sqlParams = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_KIEMKE_GET_ALL", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public bool KO_PHIEU_KIEMKE_DELETE(string pMA_DVIQLY, long pPHIEU_KIEMKE_ID, string pUser)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPHIEU_KIEMKE_ID, pUser };
            var result = base.ExecScalar("KO_PHIEU_KIEMKE_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public long InsertorUpdateKO_PHIEU_KIEMKE(DataTable dst, string pUser)
        {
            var paramObj = new object[]{
                            dst.Rows[0]["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst.Rows[0]["PHIEU_KIEMKE_ID"]),
                            dst.Rows[0]["KHACHHANG_NCC_ID"],
                            dst.Rows[0]["SOPHIEU"],
                            dst.Rows[0]["NGAY_KIEMKE"],
                            dst.Rows[0]["GHI_CHU"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_KIEMKE_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }
        public long InsertorUpdateKO_PHIEU_KIEMKE_CTIET(DataRow dst, string pUser)
        {
            var paramObj = new object[]{
                            dst["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst["PHIEU_KIEMKE_CTIET_ID"]),
                            dst["PHIEU_KIEMKE_ID"],
                            dst["KHO_ID"],
                            dst["KHO_KHUVUC_ID"],
                            dst["MA_ITEM_TYPE"],
                            dst["SANPHAM_ID"],
                            dst["SOLO"],
                            dst["HANDUNG"],
                            dst["QUYCACHDONGGOI"],
                            dst["SO_LUONG_TONG_TON"],
                            dst["SO_LUONG_THUNG_TON"],
                            dst["SO_LUONG_LE_TON"],
                            dst["SO_LUONG_TONG_THUCTE"],
                            dst["SO_LUONG_THUNG_THUCTE"],
                            dst["SO_LUONG_LE_THUCTE"],
                            dst["SO_LUONG_TONG_LECH"],
                            dst["SO_LUONG_THUNG_LECH"],
                            dst["SO_LUONG_LE_LECH"],
                            dst["MA_TINHTRANG_HANG"],
                            dst["CHATLUONG_HANGHOA"],
                            dst["CHATLUONG_BAOBI"],
                            dst["PALLET_ID"],
                            dst["VITRI"],
                            dst["MA_VACH"],
                            dst["MA_TINHTRANG_CV"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_KIEMKE_CTIET_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }
        public DataSet KO_PHIEU_KIEMKE_GET_BY_ID(string pMA_DVIQLY, long pPHIEU_KIEMKE_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_KIEMKE_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_KIEMKE_GET_BY_ID", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;

        }
        public bool UpdateKO_PHIEU_NHAPXUATKHO_CTIET(DataRow dst, long pPHIEU_KIEMKE_ID ,int pSO_LUONG_TONG,int pSO_LUONG_THUNG,int pSO_LUONG_LE ,
            string pX_OR_N, string pUser)
        {
            var paramObj = new object[]{
                            dst["MA_DVIQLY"],pX_OR_N,
                            ConstCommon.NVL_NUM_LONG_NEW (dst["KHO_KHUVUC_ID"]),
                            dst["MA_ITEM_TYPE"],
                            dst["SANPHAM_ID"],
                            dst["KHO_ID"],
                            pPHIEU_KIEMKE_ID,
                            dst["SOLO"],
                            dst["HANDUNG"],
                            dst["QUYCACHDONGGOI"],
                            pSO_LUONG_TONG,
                            pSO_LUONG_THUNG,
                            pSO_LUONG_LE,
                            dst["MA_TINHTRANG_HANG"],
                            dst["PALLET_ID"],
                            dst["VITRI"],
                            dst["MA_VACH"],
                            dst["MA_TINHTRANG_CV"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Chuyển hàng vào kho,ghi sổ, bỏ ghi sổ
        public DataSet KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_BY_PHIEU(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID,long pPHIEU_NHAPXUATKHO_CTIET_ID,long pSANPHAM_ID)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID, pPHIEU_NHAPXUATKHO_CTIET_ID, pSANPHAM_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_BY_PHIEU", paramObj);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public DataSet KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_ALL(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_ALL", paramObj);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public long InsertorUpdateKO_PHIEU_NHAPXUATKHO_CTIET_VTRI(DataRow dst, string pUser)
        {
            var paramObj = new object[]{
                            dst["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst["PHIEU_NHAPXUATKHO_CTIET_VTRI_ID"]),
                            dst["PHIEU_NHAPXUATKHO_CTIET_ID"],
                            dst["KHO_ID"],
                            dst["VITRI"],
                            dst["SANPHAM_ID"],
                            dst["SOLO"],
                            dst["HANDUNG"],
                            dst["SO_LUONG_TONG"],
                            dst["MA_VACH"],
                            dst["IS_XUAT"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }

        public int GET_SO_LUONG_SAN_PHAM_NHAP(string pMA_DVIQLY, long pKHO_ID, long pSANPHAM_ID, string pso_lo, string phan_dung,string pVITRI)
        {
            var paramObj = new object[] { pMA_DVIQLY, pKHO_ID, pSANPHAM_ID, pso_lo, phan_dung, pVITRI };
            var result = base.ExecScalar("HT_TONKHO_ONLINE_SUM_PHIEU_NHAP", paramObj);
            if (NumberHelper.ConvertStringToInt(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToInt(result.ToString());
            }
            return 0;
        }
        public int GET_SO_LUONG_SAN_PHAM_XUAT(string pMA_DVIQLY, long pKHO_ID, long pSANPHAM_ID, string pso_lo, string phan_dung, string pVITRI)
        {
            var paramObj = new object[] { pMA_DVIQLY,pKHO_ID, pSANPHAM_ID, pso_lo, phan_dung, pVITRI };
            var result = base.ExecScalar("HT_TONKHO_ONLINE_SUM_PHIEU_XUAT", paramObj);
            if (NumberHelper.ConvertStringToInt(result.ToString()) > 0)
            {
                NumberHelper.ConvertStringToInt(result.ToString());
            }
            return 0;
        }
        public DataTable GET_ALL_SANPHAM_TONKHO_ONLINE_THEO_VITRI(string pMA_DVIQLY, long pSANPHAM_ID,long pKHO_ID, string pVITRI)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pSANPHAM_ID, pKHO_ID, pVITRI };
            var result = base.ExecuteDataSet("HT_TONKHO_ONLINE_GET_DANHSACH_SANPHAM", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool HT_TONKHO_ONLINE_UPDATE(DataTable dst,string pUser)
        {
            var paramObj = new object[]{
                            dst.Rows[0]["MA_DVIQLY"],   
                            ConstCommon.NVL_NUM_LONG_NEW (dst.Rows[0]["TONKHO_ONLINE_ID"]),
                            dst.Rows[0]["KHO_ID"],
                            dst.Rows[0]["VITRI"],
                            dst.Rows[0]["SANPHAM_ID"],
                            dst.Rows[0]["SO_LUONG_TON"],pUser
                             };
            var result = base.ExecScalar("HT_TONKHO_ONLINE_UPDATE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool HT_TONKHO_ONLINE_INSERT(string pMA_DVIQLY, long pKHO_ID,string pVITRI,long SANPHAM_ID,int so_luong_ton, string pUser)
        {
            var paramObj = new object[]{
                            pMA_DVIQLY,
                            pKHO_ID,
                            pVITRI,
                            SANPHAM_ID,
                            so_luong_ton,pUser
                             };
            var result = base.ExecScalar("HT_TONKHO_ONLINE_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool HT_TONKHO_ONLINE_DELETE(string pMA_DVIQLY, long pTONKHO_ONLINE_ID, string pUser)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pTONKHO_ONLINE_ID, pUser };
            var result = base.ExecuteDataSet("HT_TONKHO_ONLINE_DELETE", sqlParams);
            if ((result != null) && (ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }
        public bool KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, bool pIS_GHISO)
        {
            var paramObj = new object[]{
                           pMA_DVIQLY,
                            pPHIEU_NHAPXUATKHO_CTIET_ID,
                            pIS_GHISO
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO_ALL(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, bool pIS_GHISO)
        {
            var paramObj = new object[]{
                           pMA_DVIQLY,
                            pPHIEU_NHAPXUATKHO_ID,
                            pIS_GHISO
                             };
            var result = base.ExecScalar("KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO_ALL", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}