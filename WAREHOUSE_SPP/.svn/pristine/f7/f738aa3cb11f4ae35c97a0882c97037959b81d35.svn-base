
using DSofT.Warehouse.Provider;
using System;
using System.Data;
using System.Transactions;

namespace DSofT.Warehouse.Business
{
    public interface IKO_PHIEU_XACNHAN_HUHONGBussiness
    {
        DataSet KO_PHIEU_XACNHAN_HUHONG_GET_ALL(string pMA_DVIQLY);
     //   DataSet KO_PHIEU_XACNHAN_HUHONG_GET_BY_ID(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_ID);


        DataTable GetData_For_gird_VITRI_HANG(string pMA_DVIQLY, long pKHO_ID);
        DataTable GetData_For_gird_TINHTRANG_HANG(string pMA_DVIQLY);
        DataTable GetData_For_gird_TENKHO_KHUVUC(string pMA_DVIQLY);
        DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE(string pMA_DVIQLY, string pMA_ITEM_TYPE);
        DataTable GetListDM_KHO(string pMA_DVIQLY);
        DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID);


        bool KO_PHIEU_XACNHAN_HUHONG_DELETE(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_ID, string pUser);
        bool KO_PHIEU_XACNHAN_HUHONG_CTIET_DELETE(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_CTIET_ID, string pUser);
        DataSet InsertorUpdateKO_PHIEU_XACNHAN_HUHONG(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser);
        bool KO_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_ID, string pSOPHIEU);

    }
    public class KO_PHIEU_XACNHAN_HUHONGBussiness : IKO_PHIEU_XACNHAN_HUHONGBussiness
    {
        IKO_PHIEU_XACNHAN_HUHONGProvider _PHIEU_XACNHAN_HUHONGProvider;
        public KO_PHIEU_XACNHAN_HUHONGBussiness(string appId, string userId = "0")
        {
            _PHIEU_XACNHAN_HUHONGProvider = new KO_PHIEU_XACNHAN_HUHONGProvider(appId, userId);
        }
        #region Lập phiếu xác nhận hàng hư hỏng
       public DataSet KO_PHIEU_XACNHAN_HUHONG_GET_ALL(string pMA_DVIQLY)
       {
            return _PHIEU_XACNHAN_HUHONGProvider.KO_PHIEU_XACNHAN_HUHONG_GET_ALL(pMA_DVIQLY);
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
        public DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE(string pMA_DVIQLY, string pMA_ITEM_TYPE)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.DM_SANPHAM_GET_LIST_ALL_BY_TYPE(pMA_DVIQLY, pMA_ITEM_TYPE);
        }
        public DataTable GetListDM_KHO(string pMA_DVIQLY)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.GetListDM_KHO(pMA_DVIQLY);
        }
        public DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID)
        {
          return  _PHIEU_XACNHAN_HUHONGProvider.DM_KHO_KHUVUC_GET_LIST_BY_KHO(pMA_DVIQLY, pKHO_ID);
        }
        public bool KO_PHIEU_XACNHAN_HUHONG_DELETE(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_ID, string pUser)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.KO_PHIEU_XACNHAN_HUHONG_DELETE(pMA_DVIQLY, pPHIEU_XACNHAN_HUHONG_ID, pUser);
        }
        public bool KO_PHIEU_XACNHAN_HUHONG_CTIET_DELETE(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_CTIET_ID, string pUser)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.KO_PHIEU_XACNHAN_HUHONG_CTIET_DELETE(pMA_DVIQLY, pPHIEU_XACNHAN_HUHONG_CTIET_ID, pUser);
        }
        public DataSet InsertorUpdateKO_PHIEU_XACNHAN_HUHONG(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser)
        {

            DataSet ds_return = null;
            long pPHIEU_XACNHAN_HUHONG_ID = 0;
            long pPHIEU_XACNHAN_HUHONG_CTIET_ID = 0;
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {


                pPHIEU_XACNHAN_HUHONG_ID = _PHIEU_XACNHAN_HUHONGProvider.InsertorUpdateKO_PHIEU_XACNHAN_HUHONG(dtDauphieu, pUser);
                if (((dtPhieu_Ctiet != null) && (dtPhieu_Ctiet.Rows.Count > 0)) && (pPHIEU_XACNHAN_HUHONG_ID > 0))
                {
                    for (int i = 0; i < dtPhieu_Ctiet.Rows.Count; i++)
                    {
                        dtPhieu_Ctiet.Rows[i]["PHIEU_XACNHAN_HUHONG_ID"] = pPHIEU_XACNHAN_HUHONG_ID;
                        pPHIEU_XACNHAN_HUHONG_CTIET_ID = _PHIEU_XACNHAN_HUHONGProvider.InsertorUpdateKO_PHIEU_XACNHAN_HUHONG_CTIET(dtPhieu_Ctiet.Rows[i], pUser);
                        if (pPHIEU_XACNHAN_HUHONG_CTIET_ID <= 0)
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
            ds_return = _PHIEU_XACNHAN_HUHONGProvider.KO_PHIEU_XACNHAN_HUHONG_GET_BY_ID(dtDauphieu.Rows[0]["MA_DVIQLY"].ToString(), pPHIEU_XACNHAN_HUHONG_ID);
            return ds_return;
        }
        public bool KO_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_XACNHAN_HUHONG_ID, string pSOPHIEU)
        {
            return _PHIEU_XACNHAN_HUHONGProvider.KO_NHAPXUATKHO_CHECKEXISTS(pMA_DVIQLY, pPHIEU_XACNHAN_HUHONG_ID, pSOPHIEU);
        }
        #endregion
    }
}
