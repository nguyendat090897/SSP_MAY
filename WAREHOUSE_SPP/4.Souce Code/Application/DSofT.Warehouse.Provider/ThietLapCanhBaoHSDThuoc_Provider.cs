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
    public interface I_ThietLapCanhBaoHSDThuoc_Provider
    {
        DataTable GetAll(string MaDVQLY);
        DataTable GetKey(string MaDVQLY, int SP_ID);
        bool Delete(string MaDVQLY, int QuitacHSD_ID);
        bool Insert_Udate(DataRow dtr);
    }

    public class ThietLapCanhBaoHSDThuoc_Provider : BaseSqlExecute, I_ThietLapCanhBaoHSDThuoc_Provider
    {
        public ThietLapCanhBaoHSDThuoc_Provider(string appId, string userId) : base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public DataTable GetAll(string MaDVQLY)
        {
            var paramObj = new object[] { MaDVQLY };
            var result = base.ExecuteDataSet("[HT_QUITAC_HSD_GetAll]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetKey(string MaDVQLY, int SP_ID)
        {
            var paramObj = new object[] { MaDVQLY ,SP_ID};
            var result = base.ExecuteDataSet("[HT_QUITAC_HSD_GetKey]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool Delete(string MaDVQLY, int QuitacHSD_ID)
        {
            var paramObj = new object[] { MaDVQLY, QuitacHSD_ID };
            var result = base.ExecuteDataSet("[HT_QUITAC_HSD_DELETE]", paramObj);
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
                if (NumberHelper.ConvertStringToLong(dtr["QUITAC_HSD_ID"].ToString()) > 0)
                {
                    var paramObj = new object[]{
                             ConstCommon.DonViQuanLy,
                             dtr["QUITAC_HSD_ID"],
                             dtr["CANHBAO_TRUOC"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("HT_QUITAC_HSD_UPDATE", paramObj);
                    return true;
                }
                else
                {
                    var paramObj = new object[]{
                            ConstCommon.DonViQuanLy,
                             dtr["MA_ITEM_TYPE"],
                             dtr["SANPHAM_ID"],
                             dtr["CANHBAO_TRUOC"],
                            CommonDataHelper.UserName
                             };
                    var result = base.ExecScalar("HT_QUITAC_HSD_INSERT", paramObj);
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
