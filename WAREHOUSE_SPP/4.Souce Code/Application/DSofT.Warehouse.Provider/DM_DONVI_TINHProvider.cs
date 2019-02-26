using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_DONVI_TINHProvider
    {
        bool InsertorUpdateDM_DONVI_TINH(int result1,DataTable ds);
        bool DeleteDM_DONVI_TINH(string pMA_DONVI_TINH, string pModifiedBy);
        DataTable GetListDM_DONVI_TINH();
        DataTable GetListDM_DONVI_TINH_GET_KEY(string pMA_DONVI_TINH);
        DataTable GetListDM_DONVI_TINH_COMBOBOX();

        DataTable GetListDM_SANPHAM_QUYDOI(string pMA_DVIQLY);

        bool InsertDM_SANPHAM_QUYDOIDVT(DataRow dst, DataTable dt, string pUser, string pDONVIQUANLY);
    }
    public class DM_DONVI_TINHProvider : BaseSqlExecute, IDM_DONVI_TINHProvider
    {
        public DM_DONVI_TINHProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }

        public bool InsertorUpdateDM_DONVI_TINH(int result1, DataTable ds)
        {
            var paramObj = new object[]{
                             ds.Rows[0]["MA_DONVI_TINH"],
                            ds.Rows[0]["TEN_DONVI_TINH"],
                            ds.Rows[0]["GHI_CHU"],
                            ds.Rows[0]["TINH_TRANG"],
                            CommonDataHelper.UserName
                             };
            
            if (result1==1)
            {
                var result = base.ExecScalar("DM_DONVI_TINH_UPDATE", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) >= 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                var result = base.ExecScalar("DM_DONVI_TINH_INSERT", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) >= 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool DeleteDM_DONVI_TINH(string pMA_DONVI_TINH, string pModifiedBy)
        {
            var paramObj = new object[] { pMA_DONVI_TINH, pModifiedBy };
            var result = base.ExecScalar("DM_DONVI_TINH_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable GetListDM_DONVI_TINH()
        {
            var paramObj = new object[] { };
            var result = base.ExecuteDataSet("DM_DONVI_TINH_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_DONVI_TINH_GET_KEY(string pMA_DONVI_TINH)
        {
            var paramObj = new object[] { pMA_DONVI_TINH };
            var result = base.ExecuteDataSet("DM_DONVI_TINH_GET_KEY", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetListDM_DONVI_TINH_COMBOBOX()
        {
            var paramObj = new object[] {};
            var result = base.ExecuteDataSet("DM_DONVI_TINH_GET_COMBOBOX", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public bool InsertDM_SANPHAM_QUYDOIDVT(DataRow dst, DataTable dt, string pUser, string pDONVIQUANLY)
        {

            var paramObj = new object[]{
                           ConstCommon.DonViQuanLy,
                            // ConstCommon.NVL_NUM_LONG_NEW (dst["SANPHAM_QUYDOI_ID"]),
                            dst["MA_ITEM_TYPE"],
                            dst["SANPHAM_ID"],
                            dt.Rows[0]["MA_DONVI_TINH_TU"],
                            dt.Rows[0]["MA_DONVI_TINH_DEN"],
                            dt.Rows[0]["SOLUONG_QUYDOI"],pUser
                             };

            var result = base.ExecScalar("DM_SANPHAM_QUYDOI_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable GetListDM_SANPHAM_QUYDOI(string pMA_DVIQLY)
        {

            var paramObj = new object[] { pMA_DVIQLY};
            var result = base.ExecuteDataSet("DM_SANPHAM_GET_LIST_ALL_SANPHAM_QUYDOI", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
    }
}
