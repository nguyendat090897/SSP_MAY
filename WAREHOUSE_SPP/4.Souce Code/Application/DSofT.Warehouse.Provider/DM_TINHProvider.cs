using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_TINHProvider
    {
        bool InsertorUpdateDM_TINH(DataTable ds);
        bool DeleteDM_TINH(long pTINH_ID, string pModifiedBy);
        DataTable GetListDM_TINH();
        DataTable GetListDM_TINH_GET_KEY( string pMA_TINH);

    }
    public class DM_TINHProvider : BaseSqlExecute, IDM_TINHProvider
    {
        public DM_TINHProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public bool InsertorUpdateDM_TINH(DataTable dst)
        {
            if (NumberHelper.ConvertStringToLong(dst.Rows[0]["TINH_ID"].ToString()) > 0)
            {
                var paramObj = new object[]{
                            dst.Rows[0]["TINH_ID"],
                            dst.Rows[0]["MA_TINH"],
                            dst.Rows[0]["TEN_TINH"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                             };
                var result = base.ExecScalar("DM_TINH_UPDATE", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                var paramObj = new object[]{
                            dst.Rows[0]["MA_TINH"],
                            dst.Rows[0]["TEN_TINH"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                         CommonDataHelper.UserName
                             };
                var result = base.ExecScalar("DM_TINH_INSERT", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool DeleteDM_TINH(long pTINH_ID, string pModifiedBy)
        {
            var paramObj = new object[] { pTINH_ID, pModifiedBy };
            var result = base.ExecScalar("DM_TINH_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable GetListDM_TINH()
        {
            var paramObj = new object[] { };
            var result = base.ExecuteDataSet("DM_TINH_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
        public DataTable GetListDM_TINH_GET_KEY(string pMA_TINH)
        {
            var paramObj = new object[] { pMA_TINH };
            var result = base.ExecuteDataSet("DM_TINH_GET_KEY", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

    }
}