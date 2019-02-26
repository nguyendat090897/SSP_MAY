using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSofT.Warehouse.Provider
{
    public interface I_ThamSo_Provider
    {
        DataTable GetAll(string MaDVQLY);
        DataTable GetGT(DataRow dr);
        bool Update(DataRow dr);
    }
    public class ThamSo_Provider: BaseSqlExecute,I_ThamSo_Provider
    {
        public ThamSo_Provider(string appId, string userId) : base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public DataTable GetAll(string MaDVQLY)
        {
            var paramObj = new object[] { MaDVQLY };
            var result = base.ExecuteDataSet("HT_THAMSO_GetAll", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool Update(DataRow dr)
        {
            try
            {
                var paramObj = new object[]{
                             ConstCommon.DonViQuanLy,
                             dr["MA_THAMSO"],
                             dr["GIATRI"],
                             dr["GHI_CHU"],
                             dr["TINH_TRANG"],
                            CommonDataHelper.UserName
                             };
                var result = base.ExecScalar("HT_THAMSO_UPDATE", paramObj);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public DataTable GetGT(DataRow dr)
        {
            var paramObj = new object[] {
                             ConstCommon.DonViQuanLy,
                             dr["MA_THAMSO"],
                             dr["GIATRI"]
            };
            var result = base.ExecuteDataSet("HT_THAMSO_GETgt", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
    }
}
