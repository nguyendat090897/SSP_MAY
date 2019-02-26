using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;
using System;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_NHOM_SAN_PHAM_Provider
    {
        int InsertorUpdateDM_NHOM_SAN_PHAM(DataTable dt);
        int DeleteDM_NHOM_SAN_PHAM(string MA_DVIQLY, int NHOM_SPHAM_ID, string ModifiledBy);
        DataTable GetListDM_NHOM_SAN_PHAM(string MA_DVIQLY);

        int Check_Exist_MA_NHOM_SAN_PHAM(string MA_DVIQLY, string MA_NHOM_SPHAM);

        int Check_Exist_MA_NHOM_SAN_PHAM_For_Update(string MA_NHOM_SPHAM,int NHOM_SPHAM_ID);

        DataTable GetList_ITEM_TYPE();

    }
    public class DM_NHOM_SAN_PHAM_Provider : BaseSqlExecute, IDM_NHOM_SAN_PHAM_Provider
    {
        public DM_NHOM_SAN_PHAM_Provider(string appId, string userId) : base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }

        public DataTable GetList_ITEM_TYPE()
        {
            var sqlParams = new object[] { };
            var result = base.ExecuteDataSet("DM_NHOM_SPHAM_GET_ITEM_TYPE", sqlParams);
            if (result != null)
                return result.Tables[0];
            return null;

        }
        public int Check_Exist_MA_NHOM_SAN_PHAM(string MA_DVIQLY, string MA_NHOM_SAN_PHAM)
        {
            var sqlParams = new object[] { MA_DVIQLY, MA_NHOM_SAN_PHAM };
            var result = base.ExecScalar("DM_NHOM_SPHAM_CHECK_EXIST_MA", sqlParams);
            return int.Parse(result.ToString());
        }
        public int Check_Exist_MA_NHOM_SAN_PHAM_For_Update(string MA_NHOM_SPHAM, int NHOM_SPHAM_ID)
        {
            var sqlParams = new object[] { CommonDataHelper.DonViQuanLy, MA_NHOM_SPHAM, NHOM_SPHAM_ID };
            var result = base.ExecScalar("DM_NHOM_SP_CHECK_EXIST_MA_FOR_UPDATE", sqlParams);
            return int.Parse(result.ToString());
        }
        public int InsertorUpdateDM_NHOM_SAN_PHAM(DataTable dtInput)
        {
            //----------------update-------------
            if (NumberHelper.ConvertStringToLong(dtInput.Rows[0]["NHOM_SPHAM_ID"].ToString()) > 0)
            {
                var sqlParams = new object[] {
                                             CommonDataHelper.DonViQuanLy
                                             ,dtInput.Rows[0]["NHOM_SPHAM_ID"]
                                             ,dtInput.Rows[0]["MA_NHOM_SPHAM"]
                                             , dtInput.Rows[0]["TEN_NHOM_SPHAM"]
                                             , dtInput.Rows[0]["MA_ITEM_TYPE"]
                                             , dtInput.Rows[0]["GHI_CHU"]
                                             ,int.Parse(dtInput.Rows[0]["TINH_TRANG"].ToString())
                                             , CommonDataHelper.UserName
                                             
                                             };
                var result = base.ExecScalar("DM_NHOM_SPHAM_UPDATE", sqlParams);
                return int.Parse(result.ToString());
            }//-------------------------insert------------------
            else
            {
                var sqlParams = new object[] {
                                              CommonDataHelper.DonViQuanLy
                                             ,dtInput.Rows[0]["MA_NHOM_SPHAM"]
                                             , dtInput.Rows[0]["TEN_NHOM_SPHAM"]
                                             , dtInput.Rows[0]["MA_ITEM_TYPE"]
                                             , dtInput.Rows[0]["GHI_CHU"]
                                             ,int.Parse(dtInput.Rows[0]["TINH_TRANG"].ToString())
                                             , CommonDataHelper.UserName
                                             };
                var result = base.ExecScalar("DM_NHOM_SPHAM_INSERT", sqlParams);
                return int.Parse(result.ToString());
            }
        }
    
        public int DeleteDM_NHOM_SAN_PHAM(string MA_DVIQLY, int NHOM_SPHAM_ID, string ModifiledBy)
        {
            var paramObj = new object[] { MA_DVIQLY, NHOM_SPHAM_ID, ModifiledBy };
            var result = Convert.ToInt32(base.ExecScalar("DM_NHOM_SPHAM_DELETE", paramObj));
            return result;
        }
        public DataTable GetListDM_NHOM_SAN_PHAM(string MA_DVIQLY)
        {
            var paramObj = new object[] { MA_DVIQLY };
            var result = base.ExecuteDataSet("DM_NHOM_SPHAM_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
    }
}
