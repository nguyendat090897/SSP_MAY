using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface I_ThietLapTonKhoTT_Provider
    {
        DataTable GetList(string MaDVQLY);
        DataTable GetKho(string MaDVQLY);
        DataTable GetKey(string MaDVQLY, int KHO_ID, int SP_ID);
        bool HT_THIETLAPTONKHO_TT_Del(int ma_QUITAC_BANCHAM_ID);
        bool HT_THIETLAPTONKHO_TT_Insert(DataRow dst);

    }

    public class ThietLapTonKhoTT_Provider : BaseSqlExecute, I_ThietLapTonKhoTT_Provider
    {
        public ThietLapTonKhoTT_Provider(string appId, string userId) : base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public DataTable GetList(string MaDVQLY)
        {
            var paramObj = new object[] { MaDVQLY };
            var result = base.ExecuteDataSet("[HT_QUITAC_TONMIN_GETALL]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool HT_THIETLAPTONKHO_TT_Del(int ma_QUITAC_BANCHAM_ID)
        {
            var paramObj = new object[] { ma_QUITAC_BANCHAM_ID };
            var result = base.ExecuteDataSet("[HT_QUITAC_TONMIN_DELETE]", paramObj);
            if (result != null)
            {
                return true;
            }
            return false;
        }
        public DataTable GetKho(string MaDVQLY)
        {
            var paramObj = new object[] { MaDVQLY };
            var result = base.ExecuteDataSet("[HT_QUITAC_TONMIN_GET_KHO]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool HT_THIETLAPTONKHO_TT_Insert(DataRow dst)
        {
            try
            {
                if(NumberHelper.ConvertStringToLong(dst["QUITAC_BANCHAM_ID"].ToString())>0)
                {
                    var paramObj = new object[]{
                             dst["QUITAC_BANCHAM_ID"],
                            ConstCommon.DonViQuanLy,
                             dst["TON_MIN"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("HT_QUITAC_TONMIN_UPDATE", paramObj);
                    return true;
                }
                else
                {
                    var paramObj = new object[]{
                            ConstCommon.DonViQuanLy,
                             dst["KHO_ID"],
                             dst["MA_ITEM_TYPE"],
                             dst["SANPHAM_ID"],
                             dst["TON_MIN"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("HT_QUITAC_TONMIN_INSERT", paramObj);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable GetKey(string MaDVQLY, int KHO_ID, int SP_ID)
        {
            var paramObj = new object[] { MaDVQLY, KHO_ID, SP_ID };
            var result = base.ExecuteDataSet("[HT_QUITAC_TONMIN_GETKEY]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
    }
}
