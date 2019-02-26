using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;
using System;

namespace DSofT.Warehouse.Provider
{
    public interface IKho_Lam_Phieu_DVProvider
    {
        int InsertorUpdateKho_Lam_Phieu_DV(string MA_DVIQLY, DataTable dtInput,string User);
        int DeleteKho_Lam_Phieu_DV(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID, string ModifiledBy);
        DataTable GetListKho_Lam_Phieu_DV(string MA_DVIQLY);
        DataSet GetListKho_Lam_Phieu_DV_By_ID(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID);

        DataTable GetData_Loai_DV(string MA_DVIQLY);
        DataTable GetListDM_KHO(string pMA_DVIQLY);
        DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID);

        int InsertorUpdateKho_Lam_Phieu_DV_CT(string MA_DVIQLY, DataRow dtInput, string User);
        int DeleteKho_Lam_Phieu_DV_CT(string MA_DVIQLY, int PHIEUYEUCAU_DV_CTIET_ID, string ModifiledBy);
        int CheckExist(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID, string SoPhieu);
        DataTable GetList_LamPhieuDV_CTiet_NVL(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID);
        DataTable GetList_LamPhieuDV_CTiet_NVL_NEW(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID);
        #region hoapd header
        DataSet getDINHMUC_SANPHAM_ALL(string pMA_DVIQLY);
        int KO_PHIEUYEUCAU_DV_CTIET_NVL_DELETE_NEW(string pMA_DVIQLY, long ppPHIEUYEUCAU_DV_ID, string ModifiledBy);
        int KO_PHIEUYEUCAU_DV_CTIET_NVL_INSERT_NEW(string pMA_DVIQLY, long pPHIEUYEUCAU_DV_ID, string pMA_ITEM_TYPE, long pSANPHAM_ID, int pSO_LUONG, long pDONGIA, long pTHANHTIEN, int pIS_SPKM, string pUser);
        #endregion

    }
    public class Kho_Lam_Phieu_DVProvider : BaseSqlExecute, IKho_Lam_Phieu_DVProvider
    {
        public Kho_Lam_Phieu_DVProvider(string appId, string userId) : base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public int CheckExist(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID, string SoPhieu)
        {
            var sqlParams = new object[] { MA_DVIQLY, PHIEUYEUCAU_DV_ID, SoPhieu };
            var result = base.ExecScalar("KO_PHIEUYEUCAU_DV_CHECK_EXIST", sqlParams);
            return int.Parse(result.ToString());
        }
        public DataTable GetList_LamPhieuDV_CTiet_NVL_NEW(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID)
        {
            var paramObj = new object[] { MA_DVIQLY, PHIEUYEUCAU_DV_ID };
            var result = base.ExecuteDataSet("KO_PHIEUYEUCAU_DV_CTIET_NVL_GETDATA", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetList_LamPhieuDV_CTiet_NVL(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID)
        {
            var paramObj = new object[] { MA_DVIQLY, PHIEUYEUCAU_DV_ID };
            var result = base.ExecuteDataSet("KO_PHIEUYEUCAU_DV_CTIET_NVL_GET_DATA", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID)
        {
            var paramObj = new object[] { pMA_DVIQLY, pKHO_ID };
            var result = base.ExecuteDataSet("DM_KHO_KHUVUC_GET_LIST_BY_KHO", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_KHO(string pMA_DVIQLY)
        {
            try
            {

                var paramObj = new object[] { pMA_DVIQLY };
                var result = base.ExecuteDataSet("DM_KHO_GET_LIST_ALL", paramObj);
                if (result != null)
                {
                    return result.Tables[0];
                }
                return null;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public int InsertorUpdateKho_Lam_Phieu_DV(string MA_DVIQLY, DataTable dtInput, string User)
        {
            var sqlParams = new object[] {
                                                MA_DVIQLY
                                                ,dtInput.Rows[0]["PHIEUYEUCAU_DV_ID"]
                                                ,dtInput.Rows[0]["SOPHIEU"]
                                                ,dtInput.Rows[0]["THOIGIAN_HOANTAT"]
                                                ,dtInput.Rows[0]["MA_LOAI_DICHVU"]
                                                ,dtInput.Rows[0]["GHI_CHU"]
                                                ,dtInput.Rows[0]["TRANGTHAI_DUYET"]
                                                ,dtInput.Rows[0]["NGAY_DUYET"]
                                                ,dtInput.Rows[0]["NGUOI_DUYET"]
                                                ,dtInput.Rows[0]["LYDO"]
                                                ,dtInput.Rows[0]["DIEUKIEN_THUCHIEN"]
                                                ,User

                                             };
            var result = base.ExecScalar("KO_PHIEUYEUCAU_DV_INSERT", sqlParams);
            if (ConstCommon.NVL_NUM_NT_NEW(result.ToString()) > 0)
            {
                return ConstCommon.NVL_NUM_NT_NEW(result.ToString());
            }
            return 0;
        }
        public int InsertorUpdateKho_Lam_Phieu_DV_CT(string MA_DVIQLY, DataRow dtInput, string User)
        {
            var sqlParams = new object[] {
                                                MA_DVIQLY
                                                ,dtInput["PHIEUYEUCAU_DV_CTIET_ID"]
                                                ,dtInput["PHIEUYEUCAU_DV_ID"]
                                                ,dtInput["KHO_ID"]
                                                ,dtInput["KHO_KHUVUC_ID"]
                                                ,dtInput["MA_ITEM_TYPE"]
                                                ,dtInput["SANPHAM_ID"]
                                                ,dtInput["SOLO"]
                                                ,dtInput["HANDUNG"]
                                                ,dtInput["SO_LUONG_TONG"]
                                                ,dtInput["SO_LUONG_THUNG"]
                                                ,dtInput["SO_LUONG_LE"]
                                                ,dtInput["SODK"]
                                                ,dtInput["GIAYPHEP_NK"]
                                                ,User

                                             };
            var result = base.ExecScalar("KO_PHIEUYEUCAU_DV_CTIET_INSERT", sqlParams);
            if (ConstCommon.NVL_NUM_NT_NEW(result.ToString()) > 0)
            {
                return ConstCommon.NVL_NUM_NT_NEW(result.ToString());
            }
            return 0;
        }
        public int DeleteKho_Lam_Phieu_DV(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID, string ModifiledBy)
        {
            var paramObj = new object[] { MA_DVIQLY, PHIEUYEUCAU_DV_ID, ModifiledBy };
            var result = Convert.ToInt32(base.ExecScalar("KO_PHIEUYEUCAU_DV_DELETE", paramObj));
            return result;
        }
        public int DeleteKho_Lam_Phieu_DV_CT(string MA_DVIQLY, int PHIEUYEUCAU_DV_CTIET_ID, string ModifiledBy)
        {
            var paramObj = new object[] { MA_DVIQLY, PHIEUYEUCAU_DV_CTIET_ID, ModifiledBy };
            var result = Convert.ToInt32(base.ExecScalar("KO_PHIEUYEUCAU_DV_CTIET_DELETE", paramObj));
            return result;
        }
        public DataTable GetListKho_Lam_Phieu_DV(string MA_DVIQLY)
        {
            var paramObj = new object[] { MA_DVIQLY};
            var result = base.ExecuteDataSet("KO_PHIEUYEUCAU_DV_GET_LIST_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;

        }
        public DataSet GetListKho_Lam_Phieu_DV_By_ID(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID)
        {
            var paramObj = new object[] { MA_DVIQLY, PHIEUYEUCAU_DV_ID };
            var result = base.ExecuteDataSet("KO_PHIEUYEUCAU_DV_CTIET_GET_LIST_BY_IDPHIEU_YC", paramObj);
            if (result != null)
            {
                return result;
            }
            return null;

        }
        public DataTable GetData_Loai_DV(string MA_DVIQLY)
        {
            var paramObj = new object[] { MA_DVIQLY };
            var result = base.ExecuteDataSet("DM_LOAI_DV_GETDATA", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        #region hoapd
        public DataSet getDINHMUC_SANPHAM_ALL(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("getDINHMUC_SANPHAM_ALL", paramObj);
            if (result != null)
            {
                return result;
            }
            return null;
        }


        public int KO_PHIEUYEUCAU_DV_CTIET_NVL_DELETE_NEW(string pMA_DVIQLY, long ppPHIEUYEUCAU_DV_ID, string ModifiledBy)
        {
            var paramObj = new object[] { pMA_DVIQLY, ppPHIEUYEUCAU_DV_ID, ModifiledBy };
            var result = Convert.ToInt32(base.ExecScalar("KO_PHIEUYEUCAU_DV_CTIET_NVL_DELETE_NEW", paramObj));
            return result;
        }




        public int KO_PHIEUYEUCAU_DV_CTIET_NVL_INSERT_NEW(string pMA_DVIQLY,long pPHIEUYEUCAU_DV_ID,string pMA_ITEM_TYPE,long pSANPHAM_ID,int pSO_LUONG, long pDONGIA ,long pTHANHTIEN,int pIS_SPKM, string pUser)
        {
            var sqlParams = new object[] {pMA_DVIQLY,pPHIEUYEUCAU_DV_ID,pMA_ITEM_TYPE,pSANPHAM_ID,pSO_LUONG,pDONGIA ,pTHANHTIEN,pIS_SPKM,pUser

                                             };
            var result = base.ExecScalar("KO_PHIEUYEUCAU_DV_CTIET_NVL_INSERT_NEW", sqlParams);
            if (ConstCommon.NVL_NUM_NT_NEW(result.ToString()) > 0)
            {
                return ConstCommon.NVL_NUM_NT_NEW(result.ToString());
            }
            return 0;
        }

        #endregion
    }
}
