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
    public interface I_QuyTacBanHangCham_Provider
    {
        DataTable GetAll(string MaDVQLY);
        DataTable GetKey(string MaDVQLY, int SP_ID);
        bool Delete(string MaDVQLY, int QuitacHSD_ID);
        bool Insert_Udate(DataRow dtr);
    }

    public class QuyTacBanHangCham_Provider : BaseSqlExecute, I_QuyTacBanHangCham_Provider
    {
        public QuyTacBanHangCham_Provider(string appId, string userId) : base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public DataTable GetAll(string MaDVQLY)
        {
            var paramObj = new object[] { MaDVQLY };
            var result = base.ExecuteDataSet("HT_QUITAC_BANCHAM_GetAll", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetKey(string MaDVQLY, int SP_ID)
        {
            var paramObj = new object[] { MaDVQLY, SP_ID };
            var result = base.ExecuteDataSet("[HT_QUITAC_BANCHAM_GetKey]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool Delete(string MaDVQLY, int QuitacHSD_ID)
        {
            var paramObj = new object[] { MaDVQLY, QuitacHSD_ID };
            var result = base.ExecuteDataSet("HT_QUITAC_BANCHAM_DELETE", paramObj);
            if (result != null)
            {
                return true;
            }
            return false;
        }
        public bool Insert_Udate(DataRow dtr)
        {
            try
            {
                if (NumberHelper.ConvertStringToLong(dtr["QUITAC_BANCHAM_ID"].ToString()) > 0)
                {
                    var paramObj = new object[]{
                             ConstCommon.DonViQuanLy,
                             dtr["QUITAC_BANCHAM_ID"],
                             dtr["THOIGIAN_LUUKHO"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("HT_QUITAC_BANCHAM_UPDATE", paramObj);
                    return true;
                }
                else
                {
                    var paramObj = new object[]{
                            ConstCommon.DonViQuanLy,
                             dtr["MA_ITEM_TYPE"],
                             dtr["SANPHAM_ID"],
                             dtr["THOIGIAN_LUUKHO"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("HT_QUITAC_BANCHAM_INSERT", paramObj);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
