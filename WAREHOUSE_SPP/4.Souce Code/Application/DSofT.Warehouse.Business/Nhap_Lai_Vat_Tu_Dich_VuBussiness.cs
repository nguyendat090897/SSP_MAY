
using DSofT.Warehouse.Provider;
using System;
using System.Data;
using System.Transactions;

namespace DSofT.Warehouse.Business
{
    public interface INhap_Lai_Vat_Tu_Dich_VuBussiness
    {
        DataSet getKO_PHIEU_NHAPXUATKHO_ALL_PHIEU_YEU_CAU(string pMA_DVIQLY,string pX_OR_N);
        DataSet getKO_PHIEU_NHAPXUATKHO_ALL(string pMA_DVIQLY, string pX_OR_N);
        DataSet KO_PHIEU_NHAPXUATKHO_GET_NHAP_NHIEU_LAN(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID);
        DataSet getKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID);
        bool InsertKO_PHIEU_NHAPXUATKHO_NHAP_NHIEU_LAN(DataTable dtSO_LUONG_TON, string pUser, long pPHIEU_NHAPXUATKHO_ID);
        bool KO_PHIEU_NHAPXUATKHO_NHAP_NHIEU_LAN_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, string pUser);
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
        bool KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pSOPHIEU);
        bool DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, string pUser);
        bool DeleteKO_PHIEUYEUCAU_NHAPXUATKHO_BYID(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pUser);
        DataTable KO_PHIEUYEUCAU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_CTIET_ID, string pUser);
        DataTable KO_PHIEU_NHAPXUATKHO_CTIET_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, string pUser);
        bool InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO(DataTable dst, string pUser, string pX_OR_N);
        bool InsertorUpdateKO_PHIEUYEUCAU_NHAPXUATKHO_CTIET(DataRow dst, string pUser);
        DataSet InsertKO_PHIEU_NHAPXUATKHO(string pMA_DVIQLY, DataTable dtNVL_Ton, DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser, string pX_OR_N);
        DataSet getDataPhieuYeuCau_TraLaiVatTu(string pMA_DVIQLY);
        DataTable getData_CTIET_NVL(string pMA_DVIQLY, int pPHIEUYEUCAU_DV_ID);
        DataSet getKO_PHIEUYEUCAU_NHAPXUATKHO_TRALAI_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID);
        int Delete_TraLai_NVL_Ton(string pMA_DVIQLY, long PHIEU_NHAPXUATKHO_ID, string User);
    }
    public class Nhap_Lai_Vat_Tu_Dich_VuBussiness : INhap_Lai_Vat_Tu_Dich_VuBussiness
    {
        INhap_Lai_Vat_Tu_DichVuProvider _DM_KHOProvider;
        public Nhap_Lai_Vat_Tu_Dich_VuBussiness(string appId, string userId = "0")
        {
            _DM_KHOProvider = new Nhap_Lai_Vat_Tu_DichVuProvider(appId, userId);
        }
        public int Delete_TraLai_NVL_Ton(string pMA_DVIQLY, long PHIEU_NHAPXUATKHO_ID, string User)
        {
            return _DM_KHOProvider.Delete_TraLai_NVL_Ton(pMA_DVIQLY, PHIEU_NHAPXUATKHO_ID, User);
        }
        public DataSet getKO_PHIEUYEUCAU_NHAPXUATKHO_TRALAI_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID)
        {
            return _DM_KHOProvider.getKO_PHIEUYEUCAU_NHAPXUATKHO_TRALAI_BY_ID(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID);
        }
        public DataTable getData_CTIET_NVL(string pMA_DVIQLY, int pPHIEUYEUCAU_DV_ID)
        {
            return _DM_KHOProvider.getData_CTIET_NVL(pMA_DVIQLY, pPHIEUYEUCAU_DV_ID);
        }
        public DataSet getDataPhieuYeuCau_TraLaiVatTu(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.getDataPhieuYeuCau_TraLaiVatTu(pMA_DVIQLY);
        }
        #region Nhập kho nhập nhiều lần poup
        //public DataTable KO_PHIEU_NHAPXUATKHO_GET_SO_LUONG_TON_TREN_PHIEU(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID)
        //{
        //    return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_GET_SO_LUONG_TON_TREN_PHIEU(pMA_DVIQLY,pPHIEU_NHAPXUATKHO_ID);
        //}
        //public DataTable KO_PHIEU_NHAPXUATKHO_GET_LICH_SU_NHAP_PHIEU(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID)
        //{
        //    return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_GET_LICH_SU_NHAP_PHIEU(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID);
        //}
        public DataSet KO_PHIEU_NHAPXUATKHO_GET_NHAP_NHIEU_LAN(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_GET_NHAP_NHIEU_LAN(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID);
        }
        public bool InsertKO_PHIEU_NHAPXUATKHO_NHAP_NHIEU_LAN(DataTable dtSO_LUONG_TON, string pUser,long pPHIEU_NHAPXUATKHO_ID)
        {
            bool result = false;
            if (((dtSO_LUONG_TON != null) && (dtSO_LUONG_TON.Rows.Count > 0)) && (pPHIEU_NHAPXUATKHO_ID > 0))
            {
                for (int i = 0; i < dtSO_LUONG_TON.Rows.Count; i++)
                {
                    if (ConstCommon.NVL_NUM_NT_NEW(dtSO_LUONG_TON.Rows[i]["SO_LUONG_TONG"]) == 0
                        && ConstCommon.NVL_NUM_NT_NEW(dtSO_LUONG_TON.Rows[i]["SO_LUONG_THUNG"]) == 0
                        && ConstCommon.NVL_NUM_NT_NEW(dtSO_LUONG_TON.Rows[i]["SO_LUONG_LE"]) == 0)
                        continue;
                    else
                    {
                        dtSO_LUONG_TON.Rows[i]["PHIEU_NHAPXUATKHO_ID"] = pPHIEU_NHAPXUATKHO_ID;
                        dtSO_LUONG_TON.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"] = 0;
                        dtSO_LUONG_TON.Rows[i]["IS_NHAPNHIEULAN"] = "1";
                        result = _DM_KHOProvider.InsertKO_PHIEU_NHAPXUATKHO_CTIET(dtSO_LUONG_TON.Rows[i], pUser);
                    }
                }
            }
            return result;
        }
        public bool KO_PHIEU_NHAPXUATKHO_NHAP_NHIEU_LAN_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, string pUser)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_NHAP_NHIEU_LAN_DELETE(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_CTIET_ID, pUser);
        }

#endregion
#region Nhập kho , nhập kho poup, phiếu yêu cầu poup
        public DataSet getKO_PHIEU_NHAPXUATKHO_ALL_PHIEU_YEU_CAU(string pMA_DVIQLY,string pX_OR_N)
        {
            return _DM_KHOProvider.getKO_PHIEU_NHAPXUATKHO_ALL_PHIEU_YEU_CAU(pMA_DVIQLY, pX_OR_N);
        }

        public DataSet getKO_PHIEU_NHAPXUATKHO_ALL(string pMA_DVIQLY, string pX_OR_N)
        {
            return _DM_KHOProvider.getKO_PHIEU_NHAPXUATKHO_ALL(pMA_DVIQLY, pX_OR_N);
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
        public bool KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEUYEUCAU_NHAPXUATKHO_ID, string pSOPHIEU)
        {
            return _DM_KHOProvider.KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(pMA_DVIQLY, pPHIEUYEUCAU_NHAPXUATKHO_ID, pSOPHIEU);
        }


        public bool DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, string pUser)
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
        public DataSet InsertKO_PHIEU_NHAPXUATKHO(string pMA_DVIQLY, DataTable dtNVL_Ton, DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser, string pX_OR_N)
        {

            DataSet ds_return = null;
            long pPHIEU_NHAPXUATKHO_ID = 0;
            long pPHIEU_NHAPXUATKHO_CTIET_ID = 0;
            long pPHIEUYEUCAU_DV_TRANVL_TON_ID = 0;
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {

                
                
                pPHIEU_NHAPXUATKHO_ID = _DM_KHOProvider.InsertorUpdateKO_PHIEU_NHAPXUATKHO(dtDauphieu, pUser, pX_OR_N);
                for (int j = 0; j < dtNVL_Ton.Rows.Count; j++)
                {
                    dtNVL_Ton.Rows[j]["PHIEU_NHAPXUATKHO_ID"] = pPHIEU_NHAPXUATKHO_ID;
                    pPHIEUYEUCAU_DV_TRANVL_TON_ID = _DM_KHOProvider.TraLai_NVL_Ton_InsertAndUpdate(pMA_DVIQLY, dtNVL_Ton.Rows[j], pUser);
                    if (pPHIEUYEUCAU_DV_TRANVL_TON_ID <= 0)
                    {
                        return null;
                    }
                }
                if (((dtPhieu_Ctiet != null) && (dtPhieu_Ctiet.Rows.Count > 0)) && (pPHIEU_NHAPXUATKHO_ID > 0) && (pPHIEUYEUCAU_DV_TRANVL_TON_ID) >0)
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
            ds_return = _DM_KHOProvider.getKO_PHIEUYEUCAU_NHAPXUATKHO_TRALAI_BY_ID(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID);
            return ds_return;
        }
        public DataSet getKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID)
        {
            return _DM_KHOProvider.getKO_PHIEU_NHAPXUATKHO_BY_ID(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID);
        }
#endregion
    }
}
