using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface I_DM_ThietBi_Provider
    {
        bool InsertorUpdateDM_THIETBI(DataTable ds);
        bool DeleteDM_THIETBI(string MaTB, string pModifiedBy);
        DataTable GetListDM_THIETBI();
        DataTable GetListDM_THIETBI_GET_KEY(string pMA_THIETBI);
        DataTable GetListDM_THIETBI_QG(string MaDVQL);
        DataTable GetListDM_THIETBI_NSX(string MaDVQL);
    }

    public class DM_ThietBi_Provider : BaseSqlExecute, I_DM_ThietBi_Provider
    {
        public DM_ThietBi_Provider(string appId, string userId) : base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public DataTable GetListDM_THIETBI()
        {
            var paramObj = new object[] { };
            var result = base.ExecuteDataSet("DM_THIETBI_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }

        public DataTable GetListDM_THIETBI_GET_KEY(string pMA_THIETBI)
        {
            var paramObj = new object[] { pMA_THIETBI };
            var result = base.ExecuteDataSet("DM_THIETBI_GET_KEY", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public bool DeleteDM_THIETBI(string MaTB, string pModifiedBy)
        {
            var paramObj = new object[] { MaTB, pModifiedBy };
            var result = base.ExecScalar("DM_THIETBI_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable GetListDM_THIETBI_QG(string MaDVQL)
        {
            var paramObj = new object[] { MaDVQL };
            var result = base.ExecuteDataSet("DM_THIETBI_GETQG", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }

        public DataTable GetListDM_THIETBI_NSX(string MaDVQL)
        {
            var paramObj = new object[] { MaDVQL };
            var result = base.ExecuteDataSet("DM_THIETBI_GETNSX", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        // THEM SUA
        public bool InsertorUpdateDM_THIETBI(DataTable dst)
        {
            if (NumberHelper.ConvertStringToLong(dst.Rows[0]["THIETBI_ID"].ToString()) >0)
            {
                var paramObj = new object[]{
                           dst.Rows[0]["THIETBI_ID"],
                           dst.Rows[0]["MA_TBI"],
                           dst.Rows[0]["TEN_TBI"],
                           dst.Rows[0]["LOAI_TBI"],
                           dst.Rows[0]["THONG_SO"],
                           dst.Rows[0]["QUOCGIA_ID"],
                           dst.Rows[0]["NHA_SXUAT_ID"],
                           dst.Rows[0]["TINH_TRANG"],
                           dst.Rows[0]["GHI_CHU"],
                            CommonDataHelper.UserName
                             };
                var result = base.ExecScalar("DM_THIETBI_UPDATE", paramObj);
                    if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                    {
                        return true;
                    }
                return false;
            }
            else
            {
                var paramObj = new object[]{
                           ConstCommon.DonViQuanLy,
                           dst.Rows[0]["MA_TBI"],
                           dst.Rows[0]["TEN_TBI"],
                           dst.Rows[0]["LOAI_TBI"],
                           dst.Rows[0]["THONG_SO"],
                           dst.Rows[0]["QUOCGIA_ID"],
                           dst.Rows[0]["NHA_SXUAT_ID"],
                           dst.Rows[0]["GHI_CHU"],
                           dst.Rows[0]["TINH_TRANG"],
                         CommonDataHelper.UserName
                             };
                var result = base.ExecScalar("DM_THIETBI_INSERT", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
