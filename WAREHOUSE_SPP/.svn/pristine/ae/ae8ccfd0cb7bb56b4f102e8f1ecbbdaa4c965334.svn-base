using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;
using System;

namespace DSofT.Warehouse.Provider
{
    public interface IGPNKProvider
    {
        int InsertorUpdateGPNK(DataTable dtInput);
        int DeleteGPNK(string MA_DVIQLY, int SANPHAM_GPNK_ID, string ModifiledBy);
        DataTable GetList_GPNK(string MA_DVIQLY, int SANPHAM_ID);
        

    }
    public class GPNKProvider : BaseSqlExecute, IGPNKProvider
    {
        public GPNKProvider(string appId, string userId) : base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public int InsertorUpdateGPNK(DataTable dtInput)
        {
            //----------------update-------------
            if (NumberHelper.ConvertStringToLong(dtInput.Rows[0]["SANPHAM_GPNK_ID"].ToString()) > 0)
            {
                var sqlParams = new object[] {
                                             CommonDataHelper.DonViQuanLy
                                             ,int.Parse(dtInput.Rows[0]["SANPHAM_GPNK_ID"].ToString())
                                             ,int.Parse(dtInput.Rows[0]["SANPHAM_ID"].ToString())
                                             , int.Parse(dtInput.Rows[0]["STT"].ToString())
                                             , dtInput.Rows[0]["SOLO"]
                                             ,dtInput.Rows[0]["GPNK"]
                                             ,dtInput.Rows[0]["NGAY_KY"]
                                             ,dtInput.Rows[0]["NGAY_HH"]
                                             ,dtInput.Rows[0]["GHI_CHU"]
                                             ,dtInput.Rows[0]["TINH_TRANG"]
                                             , CommonDataHelper.UserName
                                             
                                             };
                var result = base.ExecScalar("DM_SANPHAM_GPNK_UPDATE", sqlParams);
                return int.Parse(result.ToString());
            }//-------------------------insert------------------
            else
            {
                var sqlParams = new object[] {
                                              CommonDataHelper.DonViQuanLy
                                             ,int.Parse(dtInput.Rows[0]["SANPHAM_ID"].ToString())
                                             , int.Parse(dtInput.Rows[0]["STT"].ToString())
                                             , dtInput.Rows[0]["SOLO"]
                                             ,dtInput.Rows[0]["GPNK"]
                                             ,dtInput.Rows[0]["NGAY_KY"]
                                             ,dtInput.Rows[0]["NGAY_HH"]
                                             ,dtInput.Rows[0]["GHI_CHU"]
                                             ,dtInput.Rows[0]["TINH_TRANG"]
                                             , CommonDataHelper.UserName
                                             };
                var result = base.ExecScalar("DM_SANPHAM_GPNK_INSERT", sqlParams);
                return int.Parse(result.ToString());
            }
        }
    
        public int DeleteGPNK(string MA_DVIQLY, int SANPHAM_GPNK_ID, string ModifiledBy)
        {
            var paramObj = new object[] { MA_DVIQLY, SANPHAM_GPNK_ID, ModifiledBy };
            var result = Convert.ToInt32(base.ExecScalar("DM_SANPHAM_GPNK_DELETE", paramObj));
            return result;
        }
        public DataTable GetList_GPNK(string MA_DVIQLY, int SANPHAM_ID)
        {
            var paramObj = new object[] { MA_DVIQLY, SANPHAM_ID };
            var result = base.ExecuteDataSet("DM_SANPHAM_GPNK_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
    }
}
