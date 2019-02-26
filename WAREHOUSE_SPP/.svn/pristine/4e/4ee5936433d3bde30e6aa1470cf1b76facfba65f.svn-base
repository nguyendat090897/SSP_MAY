using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_QUOCGIAProvider
    {
        bool InsertorUpdateDM_QUOCGIA(DataTable ds);
        bool DeleteDM_QUOCGIA(string pMA_DVIQLY, long pQUOCGIA_ID, string pModifiedBy);
        DataTable GetListDM_QUOCGIA(string pMA_DVIQLY);
        DataTable GetListDM_QUOCGIA_KEY(string pMA_DVIQLY, string pMA_QUOCGIA);

    }
    public class DM_QUOCGIAProvider : BaseSqlExecute, IDM_QUOCGIAProvider
    {
        public DM_QUOCGIAProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public bool InsertorUpdateDM_QUOCGIA(DataTable dst)
        {
            if (NumberHelper.ConvertStringToLong(dst.Rows[0]["QUOCGIA_ID"].ToString()) > 0)
            {

                var paramObj = new object[]{ConstCommon.DonViQuanLy,
                            dst.Rows[0]["QUOCGIA_ID"],
                            dst.Rows[0]["MA_QUOCGIA"],
                            dst.Rows[0]["TEN_QUOCGIA"],
                            dst.Rows[0]["TEN_VIETTAT"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                              CommonDataHelper.UserName
                             };
                var result = base.ExecScalar("DM_QUOCGIA_UPDATE", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                var paramObj = new object[]{ ConstCommon.DonViQuanLy,
                            dst.Rows[0]["MA_QUOCGIA"],
                            dst.Rows[0]["TEN_QUOCGIA"],
                            dst.Rows[0]["TEN_VIETTAT"],
                            dst.Rows[0]["TINH_TRANG"],
                            dst.Rows[0]["RowVersion"],
                            CommonDataHelper.UserName
                             };
                var result = base.ExecScalar("DM_QUOCGIA_INSERT", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool DeleteDM_QUOCGIA(string pMA_DVIQLY, long pQUOCGIA_ID, string pModifiedBy)
        {
            var paramObj = new object[] { pMA_DVIQLY, pQUOCGIA_ID, pModifiedBy };
            var result = base.ExecScalar("DM_QUOCGIA_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        
        public DataTable GetListDM_QUOCGIA(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_QUOCGIA_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
    

    public DataTable GetListDM_QUOCGIA_KEY(string pMA_DVIQLY, string pMA_QUOCGIA)
        {
            var paramObj = new object[] { pMA_DVIQLY, pMA_QUOCGIA };
            var result = base.ExecuteDataSet("DM_QUOCGIA_GET_KEY", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;


        }
    }
}