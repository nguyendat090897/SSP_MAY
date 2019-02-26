using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using DSofT.Warehouse.Domain.Constant;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DSofT.Warehouse.Provider
{
    public interface IKO_PHIEU_XACNHAN_HUHONGProvider
    {

        DataSet KO_PHIEU_XACNHAN_HUHONG_GET_ALL(string pMA_DVIQLY);
        DataSet KO_PHIEU_XACNHAN_HUHONG_GET_BY_ID(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_ID);


        DataTable GetData_For_gird_VITRI_HANG(string pMA_DVIQLY, long pKHO_ID);
        DataTable GetData_For_gird_TINHTRANG_HANG(string pMA_DVIQLY);
        DataTable GetData_For_gird_TENKHO_KHUVUC(string pMA_DVIQLY);
        DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE(string pMA_DVIQLY, string pMA_ITEM_TYPE);
        DataTable GetListDM_KHO(string pMA_DVIQLY);
        DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID);


        bool KO_PHIEU_XACNHAN_HUHONG_DELETE(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_ID, string pUser);
        bool KO_PHIEU_XACNHAN_HUHONG_CTIET_DELETE(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_CTIET_ID, string pUser);
        long  InsertorUpdateKO_PHIEU_XACNHAN_HUHONG(DataTable dst, string pUser);
        long InsertorUpdateKO_PHIEU_XACNHAN_HUHONG_CTIET(DataRow dst, string pUser);
        bool KO_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_ID, string pSOPHIEU);
    }
    public class KO_PHIEU_XACNHAN_HUHONGProvider : BaseSqlExecute, IKO_PHIEU_XACNHAN_HUHONGProvider
    {
        public KO_PHIEU_XACNHAN_HUHONGProvider(string appId, string userId) :
         base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        #region Lập phiếu xác nhận hàng hư hỏng
        public DataSet KO_PHIEU_XACNHAN_HUHONG_GET_ALL(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_XACNHAN_HUHONG_GET_ALL", paramObj);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public DataSet KO_PHIEU_XACNHAN_HUHONG_GET_BY_ID(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_XACNHAN_HUHONG_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_XACNHAN_HUHONG_GET_BY_ID", sqlParams);
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
        public bool KO_PHIEU_XACNHAN_HUHONG_DELETE(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_ID, string pUser)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPHIEU_XACNHAN_HUHONG_ID, pUser };
            var result = base.ExecScalar("KO_PHIEU_XACNHAN_HUHONG_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }       
        public bool KO_PHIEU_XACNHAN_HUHONG_CTIET_DELETE(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_CTIET_ID, string pUser)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_XACNHAN_HUHONG_CTIET_ID, pUser };
            var result = base.ExecuteDataSet("KO_PHIEU_XACNHAN_HUHONG_CTIET_DELETE", sqlParams);
            if (result != null)
            {
                return true;
            }
            return false;
        }     
        public long InsertorUpdateKO_PHIEU_XACNHAN_HUHONG(DataTable dst, string pUser)
        {
            var paramObj = new object[]{
                            dst.Rows[0]["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst.Rows[0]["PHIEU_XACNHAN_HUHONG_ID"]),
                            dst.Rows[0]["SOPHIEU"],
                            dst.Rows[0]["NGAY_XACNHAN"],
                            dst.Rows[0]["SOPHIEU_CTU"],
                            dst.Rows[0]["NGAY_CTU"],
                            dst.Rows[0]["GHI_CHU"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_XACNHAN_HUHONG_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }
        public long InsertorUpdateKO_PHIEU_XACNHAN_HUHONG_CTIET(DataRow dst, string pUser)
        {
            var paramObj = new object[]{
                            dst["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst["PHIEU_XACNHAN_HUHONG_CTIET_ID"]),
                            dst["PHIEU_XACNHAN_HUHONG_ID"],
                            dst["KHO_ID"],
                            dst["KHO_KHUVUC_ID"],
                            dst["MA_ITEM_TYPE"],
                            dst["SANPHAM_ID"],
                            dst["SOLO"],
                            dst["HANDUNG"],
                            dst["VITRI"],
                            dst["LYDO"],
                            dst["SO_LUONG_TONG"],
                            dst["SO_LUONG_THUNG"],
                            dst["SO_LUONG_LE"],
                            dst["SO_DKY"],
                            dst["MA_TINHTRANG_HANG"],
                            dst["GHI_CHU"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_XACNHAN_HUHONG_CTIET_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }       
        public bool KO_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_ID, string pSOPHIEU)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_XACNHAN_HUHONG_ID, pSOPHIEU };
            var result = base.ExecuteDataSet("KO_PHIEU_XACNHAN_HUHONG_CHECKEXISTS", sqlParams);
            if ((result != null) && (ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
