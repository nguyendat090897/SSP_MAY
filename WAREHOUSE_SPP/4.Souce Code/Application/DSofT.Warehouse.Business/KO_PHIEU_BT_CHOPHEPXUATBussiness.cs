
using DSofT.Warehouse.Provider;
using System;
using System.Data;
using System.Transactions;

namespace DSofT.Warehouse.Business
{
    public interface IKO_PHIEU_BT_CHOPHEPXUATBussiness
    {
        DataSet KO_PHIEU_BT_CHOPHEPXUAT_GET_ALL(string pMA_DVIQLY);

        DataTable GetData_For_gird_VITRI_HANG(string pMA_DVIQLY, long pKHO_ID);
        DataTable GetData_For_gird_TINHTRANG_HANG(string pMA_DVIQLY);
        DataTable GetData_For_gird_TENKHO_KHUVUC(string pMA_DVIQLY);
        DataTable GetListDM_KHO(string pMA_DVIQLY);
        DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID);
        DataTable KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHO(long pKHO_ID, string pMA_DVIQLY);
        DataTable KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHUVUC(long pKHO_ID, long pKHO_KHUVUC_ID, string pMA_DVIQLY);

        bool KO_PHIEU_BT_CHOPHEPXUAT_DELETE(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_ID, string pUser);
        bool KO_PHIEU_BT_CHOPHEPXUAT_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_ID, string pSOPHIEU);
        DataSet InsertorUpdateKO_PHIEU_BT_CHOPHEPXUAT(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser);
        bool KO_PHIEU_BT_CHOPHEPXUAT_CTIET_CHECK_NGAY(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_CTIET_ID, long pKHO_ID,
          long pKHO_KHUVUC_ID, string pMA_ITEM_TYPE, long pSANPHAM_ID, string pVITRI, string pTU_NGAY, string pDEN_NGAY);
    }
    public class KO_PHIEU_BT_CHOPHEPXUATBussiness : IKO_PHIEU_BT_CHOPHEPXUATBussiness
    {
        IKO_PHIEU_BT_CHOPHEPXUATProvider _PHIEU_XACNHAN_HUHONGProvider;
        public KO_PHIEU_BT_CHOPHEPXUATBussiness(string appId, string userId = "0")
        {
            _PHIEU_XACNHAN_HUHONGProvider = new KO_PHIEU_BT_CHOPHEPXUATProvider(appId, userId);
        }
        #region Lập phiếu cho phép xuất biệt trữ
       public DataSet KO_PHIEU_BT_CHOPHEPXUAT_GET_ALL(string pMA_DVIQLY)
       {
            return _PHIEU_XACNHAN_HUHONGProvider.KO_PHIEU_BT_CHOPHEPXUAT_GET_ALL(pMA_DVIQLY);
       }
        public DataTable GetData_For_gird_VITRI_HANG(string pMA_DVIQLY, long pKHO_ID)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.GetData_For_gird_VITRI_HANG(pMA_DVIQLY, pKHO_ID);
        }
        public DataTable GetData_For_gird_TINHTRANG_HANG(string pMA_DVIQLY)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.GetData_For_gird_TINHTRANG_HANG(pMA_DVIQLY);
        }
        public DataTable GetData_For_gird_TENKHO_KHUVUC(string pMA_DVIQLY)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.GetData_For_gird_TENKHO_KHUVUC(pMA_DVIQLY);
        }
        public DataTable GetListDM_KHO(string pMA_DVIQLY)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.KO_PHIEU_BT_CHOPHEPXUAT_GET_KHO_BIET_TRU(pMA_DVIQLY);
        }
        public DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID)
        {
          return  _PHIEU_XACNHAN_HUHONGProvider.DM_KHO_KHUVUC_GET_LIST_BY_KHO(pMA_DVIQLY, pKHO_ID);
        }
        public bool KO_PHIEU_BT_CHOPHEPXUAT_DELETE(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_ID, string pUser)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.KO_PHIEU_BT_CHOPHEPXUAT_DELETE(pMA_DVIQLY, pPHIEU_BT_CHOPHEPXUAT_ID, pUser);
        }
        public DataSet InsertorUpdateKO_PHIEU_BT_CHOPHEPXUAT(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser)
        {

            DataSet ds_return = null;
            long pPHIEU_BT_CHOPHEPXUAT_ID = 0;
            long pPHIEU_BT_CHOPHEPXUAT_CTIET_ID = 0;
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {


                pPHIEU_BT_CHOPHEPXUAT_ID = _PHIEU_XACNHAN_HUHONGProvider.InsertorUpdateKO_PHIEU_BT_CHOPHEPXUAT(dtDauphieu, pUser);
                if (((dtPhieu_Ctiet != null) && (dtPhieu_Ctiet.Rows.Count > 0)) && (pPHIEU_BT_CHOPHEPXUAT_ID > 0))
                {
                    for (int i = 0; i < dtPhieu_Ctiet.Rows.Count; i++)
                    {
                        dtPhieu_Ctiet.Rows[i]["PHIEU_BT_CHOPHEPXUAT_ID"] = pPHIEU_BT_CHOPHEPXUAT_ID;
                        pPHIEU_BT_CHOPHEPXUAT_CTIET_ID = _PHIEU_XACNHAN_HUHONGProvider.InsertorUpdateKO_PHIEU_BT_CHOPHEPXUAT_CTIET(dtPhieu_Ctiet.Rows[i], pUser);
                        if (pPHIEU_BT_CHOPHEPXUAT_CTIET_ID <= 0)
                        {
                            return null;
                        }

                    }
                }
                else
                {
                    return null;
                }
                trans.Complete();

            }
            ds_return = _PHIEU_XACNHAN_HUHONGProvider.KO_PHIEU_BT_CHOPHEPXUAT_GET_BY_ID(dtDauphieu.Rows[0]["MA_DVIQLY"].ToString(), pPHIEU_BT_CHOPHEPXUAT_ID);
            return ds_return;
        }
        public bool KO_PHIEU_BT_CHOPHEPXUAT_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_ID, string pSOPHIEU)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.KO_PHIEU_BT_CHOPHEPXUAT_CHECKEXISTS(pMA_DVIQLY, pPHIEU_BT_CHOPHEPXUAT_ID, pSOPHIEU);
        }
        public DataTable KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHO(long pKHO_ID, string pMA_DVIQLY)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHO(pKHO_ID, pMA_DVIQLY);
        }
        public DataTable KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHUVUC(long pKHO_ID, long pKHO_KHUVUC_ID, string pMA_DVIQLY)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHUVUC(pKHO_ID, pKHO_KHUVUC_ID, pMA_DVIQLY);
        }
        public bool KO_PHIEU_BT_CHOPHEPXUAT_CTIET_CHECK_NGAY(string pMA_DVIQLY, long pPHIEU_BT_CHOPHEPXUAT_CTIET_ID, long pKHO_ID,
          long pKHO_KHUVUC_ID, string pMA_ITEM_TYPE, long pSANPHAM_ID, string pVITRI, string pTU_NGAY, string pDEN_NGAY)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.KO_PHIEU_BT_CHOPHEPXUAT_CTIET_CHECK_NGAY(pMA_DVIQLY, pPHIEU_BT_CHOPHEPXUAT_CTIET_ID, pKHO_ID, pKHO_KHUVUC_ID, pMA_ITEM_TYPE, pSANPHAM_ID, pVITRI, pTU_NGAY, pDEN_NGAY);
        }
        #endregion
    }
}
