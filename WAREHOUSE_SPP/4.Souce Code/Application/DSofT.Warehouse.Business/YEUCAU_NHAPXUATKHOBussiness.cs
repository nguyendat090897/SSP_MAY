
using DSofT.Warehouse.Provider;
using System;
using System.Data;
using System.Transactions;

namespace DSofT.Warehouse.Business
{
    public interface IYEUCAU_NHAPXUATKHOBussiness
    {

        DataTable GetData_For_Cbo_KhachHang_Ncc(string pMA_DVIQLY);
        DataTable getDIADIEM_XUATHANG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable getSO_HOPDONG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable getDM_HINHTHU_NHAPXUAT(string pN_OR_X);
        DataTable getDONVIVANCHUYEN_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE(string pMA_DVIQLY, string pMA_ITEM_TYPE);
        DataTable GetListDM_KHO(string pMA_DVIQLY);
        DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID);
        DataSet InsertKO_PHIEUYEUCAU_NHAPXUATKHO(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser,string pX_OR_N);
        DataTable KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, string pUser);
        DataSet getKO_PHIEUYEUCAU_NHAPXUATKHO_ALL(string pMA_DVIQLY,string pX_OR_N);
        bool KO_PHIEUYEUCAU_NHAPXUATKHO_DELETE_BYID(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pUser);
        bool KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pSOPHIEU);
        DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE_KHO(long pKHO_ID, string pMA_DVIQLY, string pMA_ITEM_TYPE);
    }
    public class YEUCAU_NHAPXUATKHOBussiness : IYEUCAU_NHAPXUATKHOBussiness
    {
        IYEUCAU_NHAPXUATKHOProvider _DM_KHOProvider;
        public YEUCAU_NHAPXUATKHOBussiness(string appId, string userId = "0")
        {
            _DM_KHOProvider = new YEUCAU_NHAPXUATKHOProvider(appId, userId);
        }

        public DataTable GetData_For_Cbo_KhachHang_Ncc(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.GetData_For_Cbo_KhachHang_Ncc(pMA_DVIQLY);
        }

        public DataTable getDIADIEM_XUATHANG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {
            return _DM_KHOProvider.getDIADIEM_XUATHANG_BY_NCC(pMA_DVIQLY, pKHACHHANG_NCC_ID);
        }

        public DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE(string pMA_DVIQLY, string pMA_ITEM_TYPE)
        {
            return _DM_KHOProvider.DM_SANPHAM_GET_LIST_ALL_BY_TYPE(pMA_DVIQLY, pMA_ITEM_TYPE);
        }

        public DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE_KHO(long pKHO_ID, string pMA_DVIQLY, string pMA_ITEM_TYPE)
        {
            return _DM_KHOProvider.DM_SANPHAM_GET_LIST_ALL_BY_TYPE_KHO(pKHO_ID,pMA_DVIQLY, pMA_ITEM_TYPE);
        }


        public DataSet InsertKO_PHIEUYEUCAU_NHAPXUATKHO(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser,string pX_OR_N)
        {

            DataSet ds_return =null;
            long pPHIEUYEUCAU_NHAPXUATKHO_ID = 0;
            long pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID = 0;
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {


                pPHIEUYEUCAU_NHAPXUATKHO_ID = _DM_KHOProvider.InsertKO_PHIEUYEUCAU_NHAPXUATKHO(dtDauphieu, pUser, pX_OR_N);
                if (((dtPhieu_Ctiet != null) && (dtPhieu_Ctiet.Rows.Count > 0)) && (pPHIEUYEUCAU_NHAPXUATKHO_ID > 0))
                {
                    for (int i = 0; i < dtPhieu_Ctiet.Rows.Count; i++)
                    {
                        dtPhieu_Ctiet.Rows[i]["PHIEUYEUCAU_NHAPXUATKHO_ID"] = pPHIEUYEUCAU_NHAPXUATKHO_ID;
                        pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID = _DM_KHOProvider.InsertKO_PHIEUYEUCAU_NHAPXUATKHO_CTIET(dtPhieu_Ctiet.Rows[i], pUser);
                        if (pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID <= 0)
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
            ds_return = _DM_KHOProvider.getKO_PHIEUYEUCAU_NHAPXUATKHO(dtDauphieu.Rows[0]["MA_DVIQLY"].ToString(), pPHIEUYEUCAU_NHAPXUATKHO_ID);
            return ds_return;
        }

        public DataTable getSO_HOPDONG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {

            return _DM_KHOProvider.getSO_HOPDONG_BY_NCC(pMA_DVIQLY, pKHACHHANG_NCC_ID);
        }


        public DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID)
        {

            return _DM_KHOProvider.DM_KHO_KHUVUC_GET_LIST_BY_KHO(pMA_DVIQLY, pKHO_ID);
        }



        public DataTable GetListDM_KHO(string pMA_DVIQLY)
        {

            return _DM_KHOProvider.GetListDM_KHO(pMA_DVIQLY);
        }


        public DataTable getDM_HINHTHU_NHAPXUAT(string pN_OR_X)
        {
            return _DM_KHOProvider.getDM_HINHTHU_NHAPXUAT(pN_OR_X);

        }

        public DataTable getDONVIVANCHUYEN_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {


            return _DM_KHOProvider.getDONVIVANCHUYEN_BY_NCC(pMA_DVIQLY, pKHACHHANG_NCC_ID);


        }

        public DataTable KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, string pUser)
        {


            return _DM_KHOProvider.KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE(pMA_DVIQLY, pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, pUser);


        }


        public DataSet getKO_PHIEUYEUCAU_NHAPXUATKHO_ALL(string pMA_DVIQLY,string pX_OR_N)
        {


            return _DM_KHOProvider.getKO_PHIEUYEUCAU_NHAPXUATKHO_ALL(pMA_DVIQLY, pX_OR_N);


        }


        public bool KO_PHIEUYEUCAU_NHAPXUATKHO_DELETE_BYID(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pUser)
        {


            return _DM_KHOProvider.KO_PHIEUYEUCAU_NHAPXUATKHO_DELETE_BYID(pMA_DVIQLY, pPHIEUYEUCAU_NHAPXUATKHO_ID, pUser);


        }


        public bool KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pSOPHIEU)
        {


            return _DM_KHOProvider.KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(pMA_DVIQLY, pPHIEUYEUCAU_NHAPXUATKHO_ID, pSOPHIEU);


        }



    }
}
