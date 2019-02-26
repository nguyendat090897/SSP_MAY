using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using DSofT.Warehouse.Domain.Constant;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DSofT.Warehouse.Provider
{
    public interface IKO_PHIEU_BT_CHOPHEPXUATProvider
    {

        DataSet KO_PHIEU_BT_CHOPHEPXUAT_GET_ALL(string pMA_DVIQLY);
        DataSet KO_PHIEU_BT_CHOPHEPXUAT_GET_BY_ID(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_ID);

        DataTable KO_PHIEU_BT_CHOPHEPXUAT_GET_KHO_BIET_TRU(string pMA_DVIQLY);
        DataTable GetData_For_gird_VITRI_HANG(string pMA_DVIQLY, long pKHO_ID);
        DataTable GetData_For_gird_TINHTRANG_HANG(string pMA_DVIQLY);
        DataTable GetData_For_gird_TENKHO_KHUVUC(string pMA_DVIQLY);
        DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID);
        DataTable KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHO(long pKHO_ID, string pMA_DVIQLY);
        DataTable KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHUVUC(long pKHO_ID, long pKHO_KHUVUC_ID, string pMA_DVIQLY);

        bool KO_PHIEU_BT_CHOPHEPXUAT_DELETE(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_ID, string pUser);
        long  InsertorUpdateKO_PHIEU_BT_CHOPHEPXUAT(DataTable dst, string pUser);
        long InsertorUpdateKO_PHIEU_BT_CHOPHEPXUAT_CTIET(DataRow dst, string pUser);
        bool KO_PHIEU_BT_CHOPHEPXUAT_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_ID, string pSOPHIEU);
        bool KO_PHIEU_BT_CHOPHEPXUAT_CTIET_CHECK_NGAY(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_CTIET_ID, long pKHO_ID,
          long pKHO_KHUVUC_ID, string pMA_ITEM_TYPE, long pSANPHAM_ID, string pVITRI, string pTU_NGAY, string pDEN_NGAY);
    }
    public class KO_PHIEU_BT_CHOPHEPXUATProvider : BaseSqlExecute, IKO_PHIEU_BT_CHOPHEPXUATProvider
    {
        public KO_PHIEU_BT_CHOPHEPXUATProvider(string appId, string userId) :
         base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        #region Lập phiếu cho phép xuất biệt trữ
        public DataSet KO_PHIEU_BT_CHOPHEPXUAT_GET_ALL(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_BT_CHOPHEPXUAT_GET_ALL", paramObj);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public DataSet KO_PHIEU_BT_CHOPHEPXUAT_GET_BY_ID(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_BT_CHOPHEPXUAT_ID };
            var result = base.ExecuteDataSet("KO_PHIEU_BT_CHOPHEPXUAT_GET_BY_ID", sqlParams);
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
            var result = base.ExecuteDataSet("KO_PHIEU_BT_CHOPHEPXUAT_GET_KHUVUC_BIET_TRU", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable KO_PHIEU_BT_CHOPHEPXUAT_GET_KHO_BIET_TRU(string pMA_DVIQLY)
        {
            try
            {
                var paramObj = new object[] { pMA_DVIQLY };
                var result = base.ExecuteDataSet("KO_PHIEU_BT_CHOPHEPXUAT_GET_KHO_BIET_TRU", paramObj);
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
        public DataTable KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHO(long pKHO_ID, string pMA_DVIQLY)
        {
            var sqlParams = new object[] { pKHO_ID, pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHO", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHUVUC(long pKHO_ID, long pKHO_KHUVUC_ID,string pMA_DVIQLY)
        {
            var sqlParams = new object[] { pKHO_ID, pKHO_KHUVUC_ID,pMA_DVIQLY };
            var result = base.ExecuteDataSet("KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHUVUC", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool KO_PHIEU_BT_CHOPHEPXUAT_DELETE(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_ID, string pUser)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPHIEU_BT_CHOPHEPXUAT_ID, pUser };
            var result = base.ExecScalar("KO_PHIEU_BT_CHOPHEPXUAT_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }       
        public long InsertorUpdateKO_PHIEU_BT_CHOPHEPXUAT(DataTable dst, string pUser)
        {
            var paramObj = new object[]{
                            dst.Rows[0]["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst.Rows[0]["PHIEU_BT_CHOPHEPXUAT_ID"]),
                            dst.Rows[0]["SOPHIEU"],
                            dst.Rows[0]["NGAY_CHOPHEP"],
                            dst.Rows[0]["GHI_CHU"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_BT_CHOPHEPXUAT_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }
        public long InsertorUpdateKO_PHIEU_BT_CHOPHEPXUAT_CTIET(DataRow dst, string pUser)
        {
            var paramObj = new object[]{
                            dst["MA_DVIQLY"],
                            ConstCommon.NVL_NUM_LONG_NEW (dst["PHIEU_BT_CHOPHEPXUAT_CTIET_ID"]),
                            dst["PHIEU_BT_CHOPHEPXUAT_ID"],
                            dst["KHO_ID"],
                            dst["KHO_KHUVUC_ID"],
                            dst["MA_ITEM_TYPE"],
                            dst["SANPHAM_ID"],
                            dst["SOLO"],
                            dst["HANDUNG"],
                            dst["VITRI"],
                            dst["IS_CHOPHEP"],
                             DateTimeHelper.ConvertDateTimeWithFormat(  dst["TU_NGAY"].ToString(),SystemConstant.DateFormat),
                             DateTimeHelper.ConvertDateTimeWithFormat(  dst["DEN_NGAY"].ToString(),SystemConstant.DateFormat),
                            //dst["DEN_NGAY"],
                            dst["MA_TINHTRANG_HANG"],
                            pUser
                             };
            var result = base.ExecScalar("KO_PHIEU_BT_CHOPHEPXUAT_CTIET_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }       
        public bool KO_PHIEU_BT_CHOPHEPXUAT_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_ID, string pSOPHIEU)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_BT_CHOPHEPXUAT_ID, pSOPHIEU };
            var result = base.ExecuteDataSet("KO_PHIEU_BT_CHOPHEPXUAT_CHECKEXISTS", sqlParams);
            if ((result != null) && (ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }

        public bool KO_PHIEU_BT_CHOPHEPXUAT_CTIET_CHECK_NGAY(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_CTIET_ID,long pKHO_ID,
          long pKHO_KHUVUC_ID,string pMA_ITEM_TYPE, long pSANPHAM_ID,string pVITRI,string pTU_NGAY, string pDEN_NGAY)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPHIEU_BT_CHOPHEPXUAT_CTIET_ID, pKHO_ID,
                pKHO_KHUVUC_ID, pMA_ITEM_TYPE , pSANPHAM_ID , pVITRI , DateTimeHelper.ConvertDateTimeWithFormat(pTU_NGAY, SystemConstant.DateFormat),
                DateTimeHelper.ConvertDateTimeWithFormat(pDEN_NGAY, SystemConstant.DateFormat) };
            var result = base.ExecuteDataSet("KO_PHIEU_BT_CHOPHEPXUAT_CTIET_CHECK_NGAY", sqlParams);
            if ((result != null) && (ConstCommon.NVL_NUM_NT_NEW(result.Tables[0].Rows[0][0]) > 0))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
