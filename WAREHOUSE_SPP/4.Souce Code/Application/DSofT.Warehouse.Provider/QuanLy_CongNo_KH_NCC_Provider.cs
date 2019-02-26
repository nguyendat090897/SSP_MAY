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
    public interface I_QuanLy_CongNo_KH_NCC_Provider
    {
        DataTable GetAll(string MaDVQLY,string loai);
        DataTable GetHD(string MaDVQLY, int KH_NCC_ID);
        DataTable ChekHD(string MaDVQLY, string So_HD);
        bool Insert(DataTable dt);
        bool Update(DataTable dt);
        bool Delete(string MaDVQLY, int HOPDONG_ID);
    }

    public class QuanLy_CongNo_KH_NCC_Provider: BaseSqlExecute, I_QuanLy_CongNo_KH_NCC_Provider
    {
        public QuanLy_CongNo_KH_NCC_Provider(string appId, string userId) : base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public DataTable GetAll(string MaDVQLY, string loai)
        {
            var paramObj = new object[] { MaDVQLY, loai };
            var result = base.ExecuteDataSet("[CONGNO_HD_GetKH]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetHD(string MaDVQLY, int KH_NCC_ID)
        {
            var paramObj = new object[] { MaDVQLY, KH_NCC_ID };
            var result = base.ExecuteDataSet("[CONGNO_HD_GetHD]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable ChekHD(string MaDVQLY, string So_HD)
        {
            var paramObj = new object[] { MaDVQLY, So_HD };
            var result = base.ExecuteDataSet("[CONGNO_HD_CHECK_HD]", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool Insert(DataTable dt)
        {
            try
            {
                var paramObj = new object[]{
                           ConstCommon.DonViQuanLy,
                           dt.Rows[0]["MA_KH"],
                           dt.Rows[0]["SO_HOPDONG"],
                           dt.Rows[0]["NGAYKY"],
                           dt.Rows[0]["NGAY_BDAU"],
                           dt.Rows[0]["NGAY_KTHUC"],
                           dt.Rows[0]["GIATRI"],
                           dt.Rows[0]["HAN_TTOAN"],
                           dt.Rows[0]["GHI_CHU"],
                           CommonDataHelper.UserName
                             };
                var result = base.ExecScalar("CONGNO_HD_Insert", paramObj);
                return true;
            }
            catch(Exception ex)
            { return false; }
        }
        public bool Update(DataTable dt)
        {
            try
            {
                var paramObj = new object[]{
                           ConstCommon.DonViQuanLy,
                           dt.Rows[0]["HOPDONG_ID"],
                           dt.Rows[0]["MA_KH"],
                           dt.Rows[0]["SO_HOPDONG"],
                           dt.Rows[0]["NGAYKY"],
                           dt.Rows[0]["NGAY_BDAU"],
                           dt.Rows[0]["NGAY_KTHUC"],
                           dt.Rows[0]["GIATRI"],
                           dt.Rows[0]["HAN_TTOAN"],
                           dt.Rows[0]["GHI_CHU"],
                           CommonDataHelper.UserName
                             };
                var result = base.ExecScalar("CONGNO_HD_UPDATE", paramObj);
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool Delete(string MaDVQLY, int HOPDONG_ID)
        {
            try
            {
                var paramObj = new object[]{ MaDVQLY, HOPDONG_ID };
                var result = base.ExecScalar("CONGNO_HD_DELETE", paramObj);
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
    }
}
