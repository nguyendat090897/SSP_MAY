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
    public interface I_PhanCongThietBi
    {
        DataTable GetListPhanCongThietBi(string MaDVQL);
        bool DeleteHT_PHANCONGTHIETBI(int MaPCTB_ID, string pModifiedBy);
        DataTable GetListPhanCongThietBi_CheckDate(string ThietBi_ID,string NgayBD,string NgayKT);
        DataTable GetListLoaiTB(int PCONG_TBI_ID, string MaDVQL, string LoaiTbi);
        DataTable GetList_TBI(string MaDVQL, string Loai_TBI);
        DataTable GetList_USER();
        //bool InsertorUpdatePC_THIETBI(DataTable ds);
        long Insert_Update_PhangCongThietBi(DataRow dst);
    }
    public class PHANCONGTHIETBIProvider : BaseSqlExecute, I_PhanCongThietBi
    {
        public PHANCONGTHIETBIProvider(string appId, string userId): base(DbCommon.WarehouseDbConnstr, appId, userId) { }

        public DataTable GetListPhanCongThietBi(string MaDVQL)
        {
            try
            {
                var sqlParams = new object[] { MaDVQL };
                var result = base.ExecuteDataSet("HT_PHANCONGTHIETBI_GETALL", sqlParams);
                if (result != null)
                    return result.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteHT_PHANCONGTHIETBI(int MaPCTB_ID, string pModifiedBy)
        {
            try
            {
                var sqlParams = new object[] { MaPCTB_ID, pModifiedBy };
                var result = base.ExecuteDataSet("HT_PHANCONGTHIETBI_DELETE", sqlParams);
                if (result != null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetListPhanCongThietBi_CheckDate(string ThietBi_ID, string NgayBD, string NgayKT)
        {
            try
            {
                var sqlParams = new object[] {   NgayBD, NgayKT, ThietBi_ID };
                var result = base.ExecuteDataSet("HT_PHANCONGTHIETBI_CheckDate", sqlParams);
                if (result != null)
                    return result.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetList_USER()
        {
            try
            {
                var sqlParams = new object[] { };
                var result = base.ExecuteDataSet("HT_PHANCONGTHIETBI_GETUSER", sqlParams);
                if (result != null)
                    return result.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetListLoaiTB(int PCONG_TBI_ID, string MaDVQL ,string LoaiTbi)
        {
            try
            {
                var sqlParams = new object[] { PCONG_TBI_ID, MaDVQL , LoaiTbi };
                var result = base.ExecuteDataSet("HT_PHANCONGTHIETBI_LOAITB", sqlParams);
                if (result != null)
                    return result.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetList_TBI(string MaDVQL, string Loai_TBI)
        {
            try
            {
                var sqlParams = new object[] { MaDVQL, Loai_TBI };
                var result = base.ExecuteDataSet("HT_PHANCONGTHIETBI_GET_TBI", sqlParams);
                if (result != null)
                    return result.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* public bool InsertorUpdatePC_THIETBI(DataTable dst)
         {
             if (NumberHelper.ConvertStringToLong(dst.Rows[0]["PCONG_TBI_ID"].ToString()) > 0)
             {
                 var paramObj = new object[]{
                            dst.Rows[0]["PCONG_TBI_ID"],
                            dst.Rows[0]["THIETBI_ID"],
                            dst.Rows[0]["UserName"],
                            dst.Rows[0]["TU_NGAY"],
                            dst.Rows[0]["DEN_NGAY"],
                             CommonDataHelper.UserName
                              };
                 var result = base.ExecScalar("HT_PHANCONGTHIETBI_UPDATE", paramObj);
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
                            dst.Rows[0]["THIETBI_ID"],
                            dst.Rows[0]["UserName"],
                            dst.Rows[0]["TU_NGAY"],
                            dst.Rows[0]["DEN_NGAY"],
                          CommonDataHelper.UserName
                              };
                 var result = base.ExecScalar("DM_THIETBI_INSERT", paramObj);
                 if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                 {
                     return true;
                 }
                 return false;
             }
         }*/
        public long Insert_Update_PhangCongThietBi(DataRow dst)
        {
            var paramObj = new object[]{
                            dst["PCONG_TBI_ID"],
                            ConstCommon.DonViQuanLy,
                             dst["THIETBI_ID"],
                             dst["UserName"],
                             dst["TU_NGAY"],
                             dst["DEN_NGAY"],
                            CommonDataHelper.UserName
                             };
            var result = base.ExecScalar("HT_PHANCONGTHIETBI_INSERT_UPDATE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }
    }
}
