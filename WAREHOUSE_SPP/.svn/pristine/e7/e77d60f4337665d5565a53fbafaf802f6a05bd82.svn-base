using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_PALLET_DIEUCHUYEN_CTIETProvider
    {
        bool UpdateDM_PALLET_DIEUCHUYEN_SO_LUONG(long pPALLET_DIEUCHUYEN_ID, string pMA_DVQLY, int pSO_LUONG_CHUYEN);
        bool DeleteDM_PALLET_DIEUCHUYEN_CTIET(string pMA_DVIQLY, long PALLET_DIEUCHUYEN_CTIET_ID, string pModifiedBy);
        DataTable GetListDM_PALLET_DIEUCHUYEN_CTIET(string pMA_DVIQLY, long pPALLET_DIEUCHUYEN_ID);
        DataTable GetListDM_PALLET_DIEUCHUYEN_DONVIQUANLY();
        DataTable GetDataBySO_PHIEU(string pMA_DVIQLY, string pSO_PHIEU);
        long InsertDM_PALLET_DIEUCHUYEN(DataTable dst, string pUser, string pDONVIQUANLY);
        long InsertDM_PALLET_DIEUCHUYEN_CTIET(DataRow dst, string pUser, string pDONVIQUANLY);
        DataSet getListDM_PALLET_DIEUCHUYEN(string pMA_DVIQLY, long pPALLET_DIEUCHUYEN_ID);
        bool UpdateDM_PHIEU_PALLET_DIEU_CHUYEN(DataTable dst, string pUser, string pDONVIQUANLY);

    }
    public class DM_PALLET_DIEUCHUYEN_CTIETProvider : BaseSqlExecute, IDM_PALLET_DIEUCHUYEN_CTIETProvider
    {
        public DM_PALLET_DIEUCHUYEN_CTIETProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public bool UpdateDM_PALLET_DIEUCHUYEN_SO_LUONG(long pPALLET_DIEUCHUYEN_ID, string pMA_DVQLY,int pSO_LUONG_CHUYEN)
        {
                var paramObj = new object[]{ pPALLET_DIEUCHUYEN_ID,
                            pMA_DVQLY,
                            pSO_LUONG_CHUYEN,
                             };
                var result = base.ExecScalar("DM_PALLET_DIEUCHUYEN_UPDATE_SO_LUONG", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
        }
        public bool UpdateDM_PHIEU_PALLET_DIEU_CHUYEN(DataTable dst, string pUser, string pDONVIQUANLY)
        {
            var paramObj = new object[]{dst.Rows[0]["PALLET_DIEUCHUYEN_ID"],
                            pDONVIQUANLY,
                            dst.Rows[0]["NGAY_NHAN"],
                            pUser,
                             dst.Rows[0]["TINH_TRANG"],
                             };
            var result = base.ExecScalar("DM_PALLET_DIEUCHUYEN_UPDATE_PHIEU", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool DeleteDM_PALLET_DIEUCHUYEN_CTIET(string pMA_DVIQLY, long PALLET_DIEUCHUYEN_CTIET_ID, string pModifiedBy)
        {
            var paramObj = new object[] { pMA_DVIQLY,PALLET_DIEUCHUYEN_CTIET_ID,pModifiedBy };
            var result = base.ExecScalar("DM_PALLET_DIEUCHUYEN_CTIET_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable GetListDM_PALLET_DIEUCHUYEN_CTIET(string pMA_DVIQLY,long pPALLET_DIEUCHUYEN_ID)
        {
            var paramObj = new object[] { pMA_DVIQLY, pPALLET_DIEUCHUYEN_ID };
            var result = base.ExecuteDataSet("DM_PALLET_DIEUCHUYEN_CTIET_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetDataBySO_PHIEU(string pMA_DVIQLY, string pSO_PHIEU)
        {
            var paramObj = new object[] { pMA_DVIQLY, pSO_PHIEU };
            var result = base.ExecuteDataSet("DM_PALLET_DIEUCHUYEN_GET_BY_SOPHIEU", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_PALLET_DIEUCHUYEN_DONVIQUANLY()
        {
            var paramObj = new object[] { };
            var result = base.ExecuteDataSet("DM_PALLET_DIEUCHUYEN_GET_LIST_DONVI_QUANLY", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
        public long InsertDM_PALLET_DIEUCHUYEN(DataTable dst, string pUser,string pDONVIQUANLY)
        {

            var paramObj = new object[]{
                            pDONVIQUANLY,
                            ConstCommon.NVL_NUM_LONG_NEW (dst.Rows[0]["PALLET_DIEUCHUYEN_ID"]),
                            dst.Rows[0]["MA_DVIQLY_NHAN"],
                            dst.Rows[0]["SO_PHIEU"],
                            dst.Rows[0]["SOLUONG_CHOPHEP"],
                            dst.Rows[0]["SOLUONG_CHUYEN"],
                            dst.Rows[0]["NGAY_CHUYEN"],
                            dst.Rows[0]["NGAY_NHAN"],
                            dst.Rows[0]["NGUOI_NHAN"],
                            dst.Rows[0]["LYDO"],pUser
                             };

            var result = base.ExecScalar("DM_PALLET_DIEUCHUYEN_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;

        }
        public long InsertDM_PALLET_DIEUCHUYEN_CTIET(DataRow dst, string pUser, string pDONVIQUANLY)
        {
            var paramObj = new object[]{
                            pDONVIQUANLY,
                            ConstCommon.NVL_NUM_LONG_NEW (dst["PALLET_DIEUCHUYEN_CTIET_ID"]),
                            dst["PALLET_DIEUCHUYEN_ID"],
                            dst["PALLET_ID"],
                            pUser
                             };
            var result = base.ExecScalar("DM_PALLET_DIEUCHUYEN_CTIET_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return NumberHelper.ConvertStringToLong(result.ToString());
            }
            return 0;
        }
        public DataSet getListDM_PALLET_DIEUCHUYEN(string pMA_DVIQLY, long pPALLET_DIEUCHUYEN_ID)
        {
            var sqlParams = new object[] { pMA_DVIQLY, pPALLET_DIEUCHUYEN_ID };
            var result = base.ExecuteDataSet("getDM_PALLET_DIEUCHUYEN", sqlParams);
            if (result != null)
            {
                return result;
            }
            return null;

        }
    }
}
