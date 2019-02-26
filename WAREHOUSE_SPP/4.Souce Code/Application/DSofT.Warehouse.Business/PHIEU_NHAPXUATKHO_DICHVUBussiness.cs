
using DSofT.Warehouse.Provider;
using System;
using System.Data;
using System.Transactions;

namespace DSofT.Warehouse.Business
{
    public interface IPHIEU_NHAPXUATKHO_DICHVUBussiness
    {
        DataSet getKO_PHIEU_NHAPXUATKHO_ALL_PHIEU_YEU_CAU(string pMA_DVIQLY,string pX_OR_N);
        DataSet getKO_PHIEU_NHAPXUATKHO_ALL(string pMA_DVIQLY, string pX_OR_N);
        DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_LIST_ALL(string pMA_DVIQLY, long pID_DV);
        DataSet KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_LIST_ALL(string pMA_DVIQLY, long pID_DV);
        DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_BY_ID(string pMA_DVIQLY, long pID_XUAT);

        DataTable GetData_For_gird_VITRI_HANG(string pMA_DVIQLY, long pKHO_ID);
        DataTable GetData_For_gird_TINHTRANG_HANG(string pMA_DVIQLY);
        DataTable GetData_For_gird_TINHTRANG_CV(string pMA_DVIQLY);
        DataTable GetData_For_gird_TENKHO_KHUVUC(string pMA_DVIQLY);
        DataTable GetListDM_PALLET(string pMA_DVIQLY);
        DataTable GetListSO_PHIEU_KIEM_KE(string pMA_DVIQLY);
        DataTable GetListDM_DONVI_TINH_COMBOBOX();
        DataTable GetListSO_LUONG_QUYDOI(string pMA_DVIQLY, string pMA_DON_VI_TINH, long pSANPHAM_ID);


        DataTable GetData_For_Cbo_KhachHang_Ncc(string pMA_DVIQLY);
        DataTable getDIADIEM_XUATHANG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable getSO_HOPDONG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable getDM_HINHTHU_NHAPXUAT(string pMA_DVIQLY);
        DataTable getDONVIVANCHUYEN_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE(string pMA_DVIQLY, string pMA_ITEM_TYPE);
        DataTable GetListDM_KHO(string pMA_DVIQLY);
        DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID);
        bool KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DV_ID, string pSOPHIEU);
        DataTable DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, string pUser);
        bool DeleteKO_PHIEUYEUCAU_NHAPXUATKHO_BYID(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pUser);
        DataTable KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, string pUser);
        DataTable KO_PHIEU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, string pUser);
        bool InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO(DataTable dst, string pUser, string pX_OR_N);
        bool InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO_CTIET(DataRow dst, string pUser);
        DataSet InsertKO_PHIEU_NHAPXUATKHO(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser, string pX_OR_N);
        DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG(string pMA_DVIQLY, long pPHIEUYEUCAU_DV_ID, long pSANPHAM_ID);
        DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, long pPHIEUYEUCAU_DV_ID, long pSANPHAM_ID);
        bool KO_PHIEU_NHAPXUATKHO_CHECKEXISTS_DICHVU_NHAPVTTL(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DV_ID, long pSANPHAM_ID);
        DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_VTTL(string pMA_DVIQLY, long pPHIEUYEUCAU_DV_ID, long pSANPHAM_ID);
    }
    public class PHIEU_NHAPXUATKHO_DICHVUBussiness : IPHIEU_NHAPXUATKHO_DICHVUBussiness
    {
        IPHIEU_NHAPXUATKHO_DICHVUProvider _DM_KHOProvider;
        public PHIEU_NHAPXUATKHO_DICHVUBussiness(string appId, string userId = "0")
        {
            _DM_KHOProvider = new PHIEU_NHAPXUATKHO_DICHVUProvider(appId, userId);
        }

        public DataSet getKO_PHIEU_NHAPXUATKHO_ALL_PHIEU_YEU_CAU(string pMA_DVIQLY,string pX_OR_N)
        {
            return _DM_KHOProvider.getKO_PHIEU_NHAPXUATKHO_ALL_PHIEU_YEU_CAU(pMA_DVIQLY, pX_OR_N);
        }

        public DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_LIST_ALL(string pMA_DVIQLY, long pID_DV)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_LIST_ALL(pMA_DVIQLY, pID_DV);
        }
        public DataSet KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_LIST_ALL(string pMA_DVIQLY, long pID_DV)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_LIST_ALL(pMA_DVIQLY, pID_DV);
        }

        public DataSet getKO_PHIEU_NHAPXUATKHO_ALL(string pMA_DVIQLY, string pX_OR_N)
        {
            return _DM_KHOProvider.getKO_PHIEU_NHAPXUATKHO_ALL(pMA_DVIQLY, pX_OR_N);
        }

        public DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_BY_ID(string pMA_DVIQLY, long pID_XUAT)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_BY_ID(pMA_DVIQLY, pID_XUAT);
        }

        public DataTable GetListDM_DONVI_TINH_COMBOBOX()
        {
            return _DM_KHOProvider.GetListDM_DONVI_TINH_COMBOBOX();
        }

        public DataTable GetData_For_gird_VITRI_HANG(string pMA_DVIQLY, long pKHO_ID)
        {
            return _DM_KHOProvider.GetData_For_gird_VITRI_HANG(pMA_DVIQLY, pKHO_ID);
        }
        public DataTable GetData_For_gird_TINHTRANG_HANG(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.GetData_For_gird_TINHTRANG_HANG(pMA_DVIQLY);
        }
        public DataTable GetData_For_gird_TINHTRANG_CV(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.GetData_For_gird_TINHTRANG_CV(pMA_DVIQLY);
        }
        public DataTable GetData_For_gird_TENKHO_KHUVUC(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.GetData_For_gird_TENKHO_KHUVUC(pMA_DVIQLY);
        }
        public DataTable GetListSO_LUONG_QUYDOI(string pMA_DVIQLY, string pMA_DON_VI_TINH, long pSANPHAM_ID)
        {
            return _DM_KHOProvider.GetListSO_LUONG_QUYDOI(pMA_DVIQLY, pMA_DON_VI_TINH, pSANPHAM_ID);
        }
        public DataTable GetListDM_PALLET(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.GetListDM_PALLET(pMA_DVIQLY);
        }
        public DataTable GetListSO_PHIEU_KIEM_KE(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.GetListSO_PHIEU_KIEM_KE(pMA_DVIQLY);
        }


        public DataTable GetData_For_Cbo_KhachHang_Ncc(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.GetData_For_Cbo_KhachHang_Ncc(pMA_DVIQLY);
        }
        public DataTable getDIADIEM_XUATHANG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {
            return _DM_KHOProvider.getDIADIEM_XUATHANG_BY_NCC(pMA_DVIQLY, pKHACHHANG_NCC_ID);
        }
        public DataTable getSO_HOPDONG_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {
            return _DM_KHOProvider.getSO_HOPDONG_BY_NCC(pMA_DVIQLY, pKHACHHANG_NCC_ID);
        }
        public DataTable getDM_HINHTHU_NHAPXUAT(string pN_OR_X)
        {
            return _DM_KHOProvider.getDM_HINHTHU_NHAPXUAT(pN_OR_X);
        }
        public DataTable getDONVIVANCHUYEN_BY_NCC(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {
            return _DM_KHOProvider.getDONVIVANCHUYEN_BY_NCC(pMA_DVIQLY, pKHACHHANG_NCC_ID);
        }
        public DataTable DM_SANPHAM_GET_LIST_ALL_BY_TYPE(string pMA_DVIQLY, string pMA_ITEM_TYPE)
        {
            return _DM_KHOProvider.DM_SANPHAM_GET_LIST_ALL_BY_TYPE(pMA_DVIQLY, pMA_ITEM_TYPE);
        }
        public DataTable GetListDM_KHO(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.GetListDM_KHO(pMA_DVIQLY);
        }
        public DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID)
        {
            return _DM_KHOProvider.DM_KHO_KHUVUC_GET_LIST_BY_KHO(pMA_DVIQLY, pKHO_ID);
        }
        public bool KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DV_ID, string pSOPHIEU)
        {
            return _DM_KHOProvider.KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_DV_ID, pSOPHIEU);
        }


        public DataTable DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, string pUser)
        {
            return _DM_KHOProvider.DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID, pUser);
        }
        public bool DeleteKO_PHIEUYEUCAU_NHAPXUATKHO_BYID(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pUser)
        {
            return _DM_KHOProvider.KO_PHIEUYEUCAU_NHAPXUATKHO_DELETE_BYID(pMA_DVIQLY, pPHIEUYEUCAU_NHAPXUATKHO_ID, pUser);
        }
        public DataTable KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, string pUser)
        {
            return _DM_KHOProvider.KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE(pMA_DVIQLY, pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, pUser);
        }
        public DataTable KO_PHIEU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, string pUser)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_CTIET_DELETE(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_CTIET_ID, pUser);
        }

        public bool InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO(DataTable dst, string pUser, string pX_OR_N)
        {
            return _DM_KHOProvider.InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO(dst, pUser, pX_OR_N);
        }
        public bool InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO_CTIET(DataRow dst, string pUser)
        {
            return _DM_KHOProvider.InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO_CTIET(dst, pUser);
        }
        public DataSet InsertKO_PHIEU_NHAPXUATKHO(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser, string pX_OR_N)
        {

            DataSet ds_return = null;
            long pPHIEU_NHAPXUATKHO_ID = 0;
            long pPHIEU_NHAPXUATKHO_CTIET_ID = 0;
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {


                pPHIEU_NHAPXUATKHO_ID = _DM_KHOProvider.InsertorUpdateKO_PHIEU_NHAPXUATKHO(dtDauphieu, pUser, pX_OR_N);
                if (((dtPhieu_Ctiet != null) && (dtPhieu_Ctiet.Rows.Count > 0)) && (pPHIEU_NHAPXUATKHO_ID > 0))
                {
                    for (int i = 0; i < dtPhieu_Ctiet.Rows.Count; i++)
                    {
                        dtPhieu_Ctiet.Rows[i]["PHIEU_NHAPXUATKHO_ID"] = pPHIEU_NHAPXUATKHO_ID;
                        pPHIEU_NHAPXUATKHO_CTIET_ID = _DM_KHOProvider.InsertorUpdateKO_PHIEU_NHAPXUATKHO_CTIET(dtPhieu_Ctiet.Rows[i], pUser);
                        if (pPHIEU_NHAPXUATKHO_CTIET_ID <= 0)
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
            ds_return = _DM_KHOProvider.getKO_PHIEU_NHAPXUATKHO_BY_ID(dtDauphieu.Rows[0]["MA_DVIQLY"].ToString(), pPHIEU_NHAPXUATKHO_ID);
            return ds_return;
        }

        public DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG(string pMA_DVIQLY, long pPHIEUYEUCAU_DV_ID, long pSANPHAM_ID)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG(pMA_DVIQLY, pPHIEUYEUCAU_DV_ID, pSANPHAM_ID);
        }

        public DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, long pPHIEUYEUCAU_DV_ID, long pSANPHAM_ID)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_ID(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID, pPHIEUYEUCAU_DV_ID, pSANPHAM_ID);
        }
        public bool KO_PHIEU_NHAPXUATKHO_CHECKEXISTS_DICHVU_NHAPVTTL(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DV_ID, long pSANPHAM_ID)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_CHECKEXISTS_DICHVU_NHAPVTTL(pMA_DVIQLY,pPHIEU_NHAPXUATKHO_DV_ID,pSANPHAM_ID);
        }
        public DataTable KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_VTTL(string pMA_DVIQLY, long pPHIEUYEUCAU_DV_ID, long pSANPHAM_ID)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_VTTL(pMA_DVIQLY, pPHIEUYEUCAU_DV_ID, pSANPHAM_ID);
        }
    }
}
