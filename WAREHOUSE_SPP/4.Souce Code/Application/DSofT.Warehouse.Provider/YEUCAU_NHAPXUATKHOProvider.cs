﻿using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DSofT.Warehouse.Provider
{
    public interface IYEUCAU_NHAPXUATKHOProvider
    {
        DataTable GetData_For_Cbo_KhachHang_Ncc(string pMA_DVIQLY);
        DataTable getDIADIEM_XUATHANG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable getSO_HOPDONG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable getDM_HINHTHU_NHAPXUAT(string pMA_DVIQLY);
        DataTable getDONVIVANCHUYEN_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE(string pMA_DVIQLY, string pMA_ITEM_TYPE);
        DataTable GetListDM_KHO(string pMA_DVIQLY);
        DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID);
        long InsertKO_PHIEUYEUCAU_NHAPXUATKHO(DataTable dst, string pUser,string pX_OR_N);
        long InsertKO_PHIEUYEUCAU_NHAPXUATKHO_CTIET(DataRow dst, string pUser);
        DataSet getKO_PHIEUYEUCAU_NHAPXUATKHO(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID);
        DataTable KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, string pUser);
        DataSet getKO_PHIEUYEUCAU_NHAPXUATKHO_ALL(string pMA_DVIQLY,string pX_OR_N);
        bool KO_PHIEUYEUCAU_NHAPXUATKHO_DELETE_BYID(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pUser);
        bool KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pSOPHIEU);
        DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE_KHO(long pKHO_ID, string pMA_DVIQLY, string pMA_ITEM_TYPE);
    }
    public class YEUCAU_NHAPXUATKHOProvider : BaseSqlExecute, IYEUCAU_NHAPXUATKHOProvider
    {
        public YEUCAU_NHAPXUATKHOProvider(string appId, string userId) :
         base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }


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


        public DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE_KHO(long pKHO_ID,string pMA_DVIQLY, string pMA_ITEM_TYPE)
        {
            var sqlParams = new object[] { pKHO_ID,pMA_DVIQLY, pMA_ITEM_TYPE };
            var result = base.ExecuteDataSet("DM_SANPHAM_GET_LIST_ALL_BY_TYPE_KHO", sqlParams);
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



        public long InsertKO_PHIEUYEUCAU_NHAPXUATKHO(DataTable dst, string pUser,string pX_OR_N)
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
                            dst.Rows[0]["GHI_CHU"],pUser
                             };

            var result = base.ExecScalar("KO_PHIEUYEUCAU_NHAPXUATKHO_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;

        }



        public long InsertKO_PHIEUYEUCAU_NHAPXUATKHO_CTIET(DataRow dst, string pUser)
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
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }


        public DataSet getKO_PHIEUYEUCAU_NHAPXUATKHO(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEUYEUCAU_NHAPXUATKHO_ID };
            var result = base.ExecuteDataSet("getKO_PHIEUYEUCAU_NHAPXUATKHO", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;

        }


        public DataTable KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID,string pUser)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, pUser };
            var result = base.ExecuteDataSet("PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE", sqlParams);
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
            if ((result!= null)&&( ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }

        public DataSet getKO_PHIEUYEUCAU_NHAPXUATKHO_ALL(string pMA_DVIQLY,string pX_OR_N)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pX_OR_N };
            var result = base.ExecuteDataSet("getKO_PHIEUYEUCAU_NHAPXUATKHO_ALL", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;

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

    }
}