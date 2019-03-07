
using DSofT.Warehouse.Provider;
using System;
using System.Data;
using System.Transactions;

namespace DSofT.Warehouse.Business
{
    public interface IPHIEU_NHAPXUATKHOBussiness
    {
        DataSet getKO_PHIEU_NHAPXUATKHO_ALL_PHIEU_YEU_CAU(string pMA_DVIQLY,string pX_OR_N);
        DataSet getKO_PHIEU_NHAPXUATKHO_ALL(string pMA_DVIQLY, string pX_OR_N);
        DataSet KO_PHIEU_NHAPXUATKHO_GET_NHAP_NHIEU_LAN(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID);
        DataSet getKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID);
        bool InsertKO_PHIEU_NHAPXUATKHO_NHAP_NHIEU_LAN(DataTable dtSO_LUONG_TON, string pUser, long pPHIEU_NHAPXUATKHO_ID);
        bool KO_PHIEU_NHAPXUATKHO_NHAP_NHIEU_LAN_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, string pUser);
        DataTable GetData_For_gird_VITRI_HANG(string pMA_DVIQLY, long pKHO_ID);
        DataTable GetData_For_gird_TINHTRANG_HANG(string pMA_DVIQLY, string pMA_HINHTHU_NHAPXUAT);
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
        DataSet InsertKO_PHIEU_NHAPXUATKHO(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser, string pX_OR_N);
        bool KO_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, string pSOPHIEU);


        DataSet InsertKO_PHIEU_NHAPXUATKHO_DC(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser, string pLOAI_DC);
        long InsertorUpdateKO_PHIEU_NHAPXUATKHO_DC_CTIET(DataRow dst, string pUser);
        DataSet KO_PHIEU_NHAPXUATKHO_DC_GET_ALL(string pMA_DVIQLY);
        bool KO_PHIEU_NHAPXUATKHO_DC_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DC_ID, string pUser);
        bool KO_PHIEU_NHAPXUATKHO_DC_CT_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DC_CTIET_ID, string pUser);
        DataSet KO_PHIEU_NHAPXUATKHO_GET_BY_DC(string pMA_DVIQLY, string pX_OR_N);
        bool KO_PHIEU_NHAPXUATKHO_DC_UPDATE(DataTable dst, string pUser);


        DataSet KO_PHIEU_NHAPXUATKHO_DCNB_GETALL(string pMA_DVIQLY);
        long InsertorUpdateKO_PHIEU_NHAPXUATKHO_DCNB(DataTable dtDauphieu, string pUser, string pLOAI_DCHUYEN);
        DataTable KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYXUAT(string pMA_DVIQLY);
        DataTable KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYNHAN(string pMA_DVIQLY);
        DataSet InsertKO_PHIEU_NHAPXUATKHO_DCNB_PHIEUNXK(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser, string pX_OR_N);
        bool KO_PHIEU_NHAPXUATKHO_DCNB_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DCNB_ID, string pUser);


        DataSet KO_PHIEU_KIEMKE_GET_ALL(string pMA_DVIQLY);
        DataSet InsertorUpdateKO_PHIEU_KIEMKE_AND_CTIET(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser);
        bool KO_PHIEU_KIEMKE_DELETE(string pMA_DVIQLY, long pPHIEU_KIEMKE_ID, string pUser);
        DataSet KO_PHIEU_KIEMKE_GET_ID(string pMA_DVIQLY, long pPHIEU_KIEMKE_ID);
        bool UpdateKO_PHIEU_NHAPXUATKHO_CTIET(DataRow dst, long pPHIEU_KIEMKE_ID, int pSO_LUONG_TONG, int pSO_LUONG_THUNG, int pSO_LUONG_LE,
            string pX_OR_N, string pUser);

        DataSet KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_BY_PHIEU(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, long pPHIEU_NHAPXUATKHO_CTIET_ID, long pSANPHAM_ID);
        DataSet KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_ALL(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID);
        long InsertorUpdateKO_PHIEU_NHAPXUATKHO_CTIET_VTRI(DataTable dtPhieu_Ctiet, string pUser);
        bool Ghiso(DataRow drPHIEU_NHAPXUATKHO_CTIET_ID, DateTime pCreatedDate, string pUser, string pmadvi_qly, string pN_or_X, bool pghi_so);
        bool KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, bool pIS_GHISO);
        bool KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO_ALL(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, bool pIS_GHISO);
        int GET_SO_LUONG_SAN_PHAM_NHAP(string pmadvi_qly, long psanpham_id, long pkho_id, string so_lo, string han_dung, string pvi_tri);
        int GET_SO_LUONG_SAN_PHAM_XUAT(string pmadvi_qly, long psanpham_id, long pkho_id, string so_lo, string han_dung, string pvi_tri);
    }
    public class PHIEU_NHAPXUATKHOBussiness : IPHIEU_NHAPXUATKHOBussiness
    {
        IPHIEU_NHAPXUATKHOProvider _DM_KHOProvider;
        public PHIEU_NHAPXUATKHOBussiness(string appId, string userId = "0")
        {
            _DM_KHOProvider = new PHIEU_NHAPXUATKHOProvider(appId, userId);
        }
        #region Nhập kho nhập nhiều lần poup
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
        public DataTable GetData_For_gird_TINHTRANG_HANG(string pMA_DVIQLY, string pMA_HINHTHU_NHAPXUAT)
        {
            return _DM_KHOProvider.GetData_For_gird_TINHTRANG_HANG(pMA_DVIQLY,pMA_HINHTHU_NHAPXUAT);
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
        public DataSet getKO_PHIEU_NHAPXUATKHO_BY_ID(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID)
        {
            return _DM_KHOProvider.getKO_PHIEU_NHAPXUATKHO_BY_ID(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID);
        }
        public bool KO_NHAPXUATKHO_CHECKEXISTS(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, string pSOPHIEU)
        {
            return _DM_KHOProvider.KO_NHAPXUATKHO_CHECKEXISTS(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID, pSOPHIEU);
        }
        #endregion
        #region Điều chỉnh phiếu xuất
        public DataSet InsertKO_PHIEU_NHAPXUATKHO_DC(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser, string pLOAI_DC)
        {
            DataSet ds_return = null;
            long pPHIEU_NHAPXUATKHO_DC_ID = 0;
            long pPHIEU_NHAPXUATKHO_DC_CTIET_ID = 0;
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                pPHIEU_NHAPXUATKHO_DC_ID = _DM_KHOProvider.InsertorUpdateKO_PHIEU_NHAPXUATKHO_DC(dtDauphieu, pUser, pLOAI_DC);
                if (((dtPhieu_Ctiet != null) && (dtPhieu_Ctiet.Rows.Count > 0)) && (pPHIEU_NHAPXUATKHO_DC_ID > 0))
                {
                    for (int i = 0; i < dtPhieu_Ctiet.Rows.Count; i++)
                    {
                        dtPhieu_Ctiet.Rows[i]["PHIEU_NHAPXUATKHO_DC_ID"] = pPHIEU_NHAPXUATKHO_DC_ID;
                        pPHIEU_NHAPXUATKHO_DC_CTIET_ID = _DM_KHOProvider.InsertorUpdateKO_PHIEU_NHAPXUATKHO_DC_CTIET(dtPhieu_Ctiet.Rows[i], pUser);
                        if (pPHIEU_NHAPXUATKHO_DC_CTIET_ID <= 0)
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
            ds_return = _DM_KHOProvider.getKO_PHIEU_NHAPXUATKHO_DC_BY_ID(dtDauphieu.Rows[0]["MA_DVIQLY"].ToString(), pPHIEU_NHAPXUATKHO_DC_ID);
            return ds_return;
        }
        public long InsertorUpdateKO_PHIEU_NHAPXUATKHO_DC_CTIET(DataRow dst, string pUser)
        {
            return _DM_KHOProvider.InsertorUpdateKO_PHIEU_NHAPXUATKHO_DC_CTIET(dst, pUser);
        }
        public DataSet KO_PHIEU_NHAPXUATKHO_DC_GET_ALL(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DC_GET_ALL(pMA_DVIQLY);
        }
        public bool KO_PHIEU_NHAPXUATKHO_DC_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DC_ID, string pUser)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DC_DELETE(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_DC_ID, pUser);
        }
        public DataSet KO_PHIEU_NHAPXUATKHO_GET_BY_DC(string pMA_DVIQLY, string pX_OR_N)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_GET_BY_DC(pMA_DVIQLY, pX_OR_N);
        }
        public bool KO_PHIEU_NHAPXUATKHO_DC_CT_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DC_CTIET_ID, string pUser)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DC_CT_DELETE(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_DC_CTIET_ID, pUser);
        }
        public bool KO_PHIEU_NHAPXUATKHO_DC_UPDATE(DataTable dst, string pUser)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DC_UPDATE(dst, pUser);
        }
        #endregion
        #region Điểu chuyển nhập xuất kho
        public DataSet KO_PHIEU_NHAPXUATKHO_DCNB_GETALL(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DCNB_GETALL(pMA_DVIQLY);
        }
        public long InsertorUpdateKO_PHIEU_NHAPXUATKHO_DCNB(DataTable dtDCNB, string pUser, string pLOAI_DCHUYEN)
        {
           return _DM_KHOProvider.InsertorUpdateKO_PHIEU_NHAPXUATKHO_DCNB(dtDCNB, pUser, pLOAI_DCHUYEN);
        }
        public DataTable KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYXUAT(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYXUAT(pMA_DVIQLY);
        }
        public DataTable KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYNHAN(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYNHAN(pMA_DVIQLY);
        }

        public DataSet InsertKO_PHIEU_NHAPXUATKHO_DCNB_PHIEUNXK(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser, string pX_OR_N)
        {

            DataSet ds_return = null;
            long pPHIEU_NHAPXUATKHO_ID = 0;
            long pPHIEU_NHAPXUATKHO_CTIET_ID = 0;
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {


                pPHIEU_NHAPXUATKHO_ID = _DM_KHOProvider.InsertorUpdateKO_PHIEU_NHAPXUATKHO_DCNB_PHIEUNXK(dtDauphieu, pUser, pX_OR_N);
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
        public bool KO_PHIEU_NHAPXUATKHO_DCNB_DELETE(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_DCNB_ID, string pUser)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_DCNB_DELETE(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_DCNB_ID, pUser);
        }
        #endregion

        #region Kiểm kê hàng hóa
        public DataSet KO_PHIEU_KIEMKE_GET_ALL(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.KO_PHIEU_KIEMKE_GET_ALL(pMA_DVIQLY);
        }
        public DataSet InsertorUpdateKO_PHIEU_KIEMKE_AND_CTIET(DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string pUser)
        {
            DataSet ds_return = null;
            long pPHIEU_KIEMKE_ID = 0;
            long pPHIEU_KIEMKE_CTIET_ID = 0;
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {


                pPHIEU_KIEMKE_ID = _DM_KHOProvider.InsertorUpdateKO_PHIEU_KIEMKE(dtDauphieu, pUser);
                if (((dtPhieu_Ctiet != null) && (dtPhieu_Ctiet.Rows.Count > 0)) && (pPHIEU_KIEMKE_ID > 0))
                {
                    for (int i = 0; i < dtPhieu_Ctiet.Rows.Count; i++)
                    {
                        dtPhieu_Ctiet.Rows[i]["PHIEU_KIEMKE_ID"] = pPHIEU_KIEMKE_ID;
                        pPHIEU_KIEMKE_CTIET_ID = _DM_KHOProvider.InsertorUpdateKO_PHIEU_KIEMKE_CTIET(dtPhieu_Ctiet.Rows[i], pUser);
                        if (pPHIEU_KIEMKE_CTIET_ID <= 0)
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
            ds_return = _DM_KHOProvider.KO_PHIEU_KIEMKE_GET_BY_ID(dtDauphieu.Rows[0]["MA_DVIQLY"].ToString(), pPHIEU_KIEMKE_ID);
            return ds_return;
        }
        public bool KO_PHIEU_KIEMKE_DELETE(string pMA_DVIQLY, long pPHIEU_KIEMKE_ID, string pUser)
        {
            return _DM_KHOProvider.KO_PHIEU_KIEMKE_DELETE(pMA_DVIQLY, pPHIEU_KIEMKE_ID, pUser);
        }
        public DataSet KO_PHIEU_KIEMKE_GET_ID(string pMA_DVIQLY, long pPHIEU_KIEMKE_ID)
        {
            return _DM_KHOProvider.KO_PHIEU_KIEMKE_GET_BY_ID(pMA_DVIQLY, pPHIEU_KIEMKE_ID);
        }
        public bool UpdateKO_PHIEU_NHAPXUATKHO_CTIET(DataRow dst, long pPHIEU_KIEMKE_ID, int pSO_LUONG_TONG, int pSO_LUONG_THUNG, int pSO_LUONG_LE,
            string pX_OR_N, string pUser)
        {
            return _DM_KHOProvider.UpdateKO_PHIEU_NHAPXUATKHO_CTIET(dst, pPHIEU_KIEMKE_ID, pSO_LUONG_TONG, pSO_LUONG_THUNG, pSO_LUONG_LE, pX_OR_N, pUser);
        }
        #endregion

        #region Chuyển hàng vào kho, ghi sổ, bỏ ghi sổ
        public DataSet KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_BY_PHIEU(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, long pPHIEU_NHAPXUATKHO_CTIET_ID, long pSANPHAM_ID)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_BY_PHIEU(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID, pPHIEU_NHAPXUATKHO_CTIET_ID, pSANPHAM_ID);
        }
        public DataSet KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_ALL(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_ALL(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID);
        }
        public long InsertorUpdateKO_PHIEU_NHAPXUATKHO_CTIET_VTRI(DataTable dtPhieu_Ctiet, string pUser)
        {
            long pPHIEU_NHAPXUATKHO_CTIET_VITRI_ID = 0;
            if (((dtPhieu_Ctiet != null) && (dtPhieu_Ctiet.Rows.Count > 0)))
            {
                for (int i = 0; i < dtPhieu_Ctiet.Rows.Count; i++)
                {
                    if (ConstCommon.NVL_NUM_LONG_NEW(dtPhieu_Ctiet.Rows[i]["LA_PARENT"]) == 0)
                    {
                        pPHIEU_NHAPXUATKHO_CTIET_VITRI_ID = _DM_KHOProvider.InsertorUpdateKO_PHIEU_NHAPXUATKHO_CTIET_VTRI(dtPhieu_Ctiet.Rows[i], pUser);
                        if (pPHIEU_NHAPXUATKHO_CTIET_VITRI_ID <= 0)
                        {
                            return 0;
                        }
                    }
                }
            }
            else
            {
                return 0;
            }
            return pPHIEU_NHAPXUATKHO_CTIET_VITRI_ID;
        }

        public int GET_SO_LUONG_SAN_PHAM_NHAP(string pmadvi_qly, long psanpham_id, long pkho_id,string so_lo,string han_dung, string pvi_tri)
        {
            return _DM_KHOProvider.GET_SO_LUONG_SAN_PHAM_NHAP(pmadvi_qly, pkho_id, psanpham_id,  so_lo,  han_dung, pvi_tri);
        }

        public int GET_SO_LUONG_SAN_PHAM_XUAT(string pmadvi_qly, long psanpham_id, long pkho_id, string so_lo, string han_dung, string pvi_tri)
        {
            return _DM_KHOProvider.GET_SO_LUONG_SAN_PHAM_XUAT(pmadvi_qly, pkho_id, psanpham_id,  so_lo,  han_dung, pvi_tri);
        }

        //----------------hàm dùng chung
        public string Update(string pmadvi_qly, long psanpham_id, long pkho_id, string pvi_tri, int soluong_nhap_vao, int so_luongxuat_ra, string pUser, bool pghi_so)
        {
            string error = "";
            //đoạn này viết store lấy tất cả các phieu nhap tuong ung voi sản phẩm nhập  whre thêm vị trí
            //int tong_soluong_nhap = _DM_KHOProvider.GET_SO_LUONG_SAN_PHAM_NHAP(pmadvi_qly, pkho_id, psanpham_id, pvi_tri);

            //đoạn này viết store lấy tất cả các phieu xuat tuong ung voi sản phẩm nhập  whre thêm vị trí
            //int tong_soluong_xuat = _DM_KHOProvider.GET_SO_LUONG_SAN_PHAM_XUAT(pmadvi_qly,pkho_id, psanpham_id, pvi_tri);
            int so_luong_ton = 0;
            //tinh ton kho
            if (pghi_so == true)
            {
                //so_luong_ton = (tong_soluong_nhap) - (tong_soluong_xuat) + soluong_nhap_vao - so_luongxuat_ra;
            }
            //neu la bo ghi so
            else
            {
               // so_luong_ton = (tong_soluong_nhap) - (tong_soluong_xuat) - soluong_nhap_vao - so_luongxuat_ra;
            }
            //đoạn này viết store  lay cac dong trong trong bang tonkho online where them vi tri
            DataTable dt = _DM_KHOProvider.GET_ALL_SANPHAM_TONKHO_ONLINE_THEO_VITRI(pmadvi_qly, psanpham_id, pkho_id, pvi_tri);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    long id = ConstCommon.NVL_NUM_LONG_NEW(dt.Rows[i]["TONKHO_ONLINE_ID"]);
                    //xoa chi du 1 dong dau
                    bool result = _DM_KHOProvider.HT_TONKHO_ONLINE_DELETE(pmadvi_qly, id, pUser);
                }
            }
            //Sau khi thay đổi dữ liệu phải đảm bảo tồn kho >= 0
            if (true)//inventory >= 0)
            {
                if (pghi_so == true || pghi_so == false) //neu la ghi so hoac bo ghi so
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (ConstCommon.NVL_NUM_NT_NEW(dt.Rows[0]["SO_LUONG_TON"]) != so_luong_ton)
                        {
                            dt.Rows[0]["SO_LUONG_TON"] = so_luong_ton ;
                            //update vao bang tonkho online
                            _DM_KHOProvider.HT_TONKHO_ONLINE_UPDATE(dt, pUser);//sai ngay thang nay
                        }
                    }
                    else
                    {
                        //neu chua co trong tonkho online thi insert vao
                        _DM_KHOProvider.HT_TONKHO_ONLINE_INSERT(pmadvi_qly, pkho_id, pvi_tri,psanpham_id,soluong_nhap_vao, pUser);
                    }
                }
            }
            else
            {
                error += string.Format("<br/>Dữ liệu tồn kho của sản phẩm vừa thêm là {1}: không hợp lệ!", so_luong_ton);
            }
            return error;
        }
        public string Check(string pmadvi_qly, long psanpham_id, long pkho_id, string pvi_tri, int soluong_nhap_vao, int so_luongxuat_ra,string pUser, bool pghi_so)
        {
            return Update(pmadvi_qly, psanpham_id, pkho_id, pvi_tri, soluong_nhap_vao, so_luongxuat_ra, pUser, pghi_so);
        }
        public void GhisoSP(DataRow row,string pUser,string pmadvi_qly, string pN_or_X,bool pghi_so)
        {

            //Kiểm tra dữ liệu sau khi bỏ ghi sổ có hợp lệ không
            string check = "";
            long psanpham_id = ConstCommon.NVL_NUM_LONG_NEW(row["SANPHAM_ID"]);
            long pkho_id = ConstCommon.NVL_NUM_LONG_NEW(row["KHO_ID"]);
            string pvi_tri = row["VITRI"].ToString();
            int pso_luong_tong = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]);
            //neu so luong lon hon khong moi di kiem tra
            if (pN_or_X == "N")//Neu la nhap kho
            {
                if (pso_luong_tong > 0)
                {
                    check = Check(pmadvi_qly, psanpham_id, pkho_id, pvi_tri, pso_luong_tong, 0, pUser, pghi_so);
                }

                if (string.IsNullOrEmpty(check))
                {
                    //Khi đã hợp lệ thì mới update
                    Update(pmadvi_qly, ConstCommon.NVL_NUM_LONG_NEW(row["SANPHAM_ID"]), ConstCommon.NVL_NUM_LONG_NEW(row["KHO_ID"]), row["VITRI"].ToString(), ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]), 0, pUser, pghi_so);
                }
            }
            else //Neu la xuat kho
            {
                if (ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]) > 0)
                {
                    check = Check(pmadvi_qly, ConstCommon.NVL_NUM_LONG_NEW(row["SANPHAM_ID"]), ConstCommon.NVL_NUM_LONG_NEW(row["KHO_ID"]), row["VITRI"].ToString(),0,ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]), pUser, pghi_so);
                }

                if (string.IsNullOrEmpty(check))
                {
                    //Khi đã hợp lệ thì mới update
                    Update(pmadvi_qly, ConstCommon.NVL_NUM_LONG_NEW(row["SANPHAM_ID"]), ConstCommon.NVL_NUM_LONG_NEW(row["KHO_ID"]), row["VITRI"].ToString(),0, ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]), pUser, pghi_so);
                }
            }

        }
        public  bool KiemTraNgaySuaChungTu(DateTime CreatedDate)
        {
            int limit_daterange_for_update_data = ConstCommon.pSO_NGAY_DUOC_PHEP_SUA;
            if (string.IsNullOrEmpty(limit_daterange_for_update_data.ToString()))
            {
                limit_daterange_for_update_data = 0;
            }

            if (CreatedDate.AddDays(Convert.ToInt32(limit_daterange_for_update_data)) > DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public  bool Ghiso(DataRow drPHIEU_NHAPXUATKHO_CTIET_ID, DateTime pCreatedDate,string pUser, string pmadvi_qly, string pN_or_X, bool pghi_so)
        {
            //Kiểm tra cho phép sửa chứng từ này hay không
            if (KiemTraNgaySuaChungTu(pCreatedDate) == false)
            {
                return false;
            }
            GhisoSP(drPHIEU_NHAPXUATKHO_CTIET_ID, pUser, pmadvi_qly, pN_or_X, pghi_so);
            return true;
        }
        public bool KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_CTIET_ID, bool pIS_GHISO)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_CTIET_ID, pIS_GHISO);
        }
        public bool KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO_ALL(string pMA_DVIQLY, long pPHIEU_NHAPXUATKHO_ID, bool pIS_GHISO)
        {
            return _DM_KHOProvider.KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO_ALL(pMA_DVIQLY, pPHIEU_NHAPXUATKHO_ID, pIS_GHISO);
        }
        //----------------end
        #endregion
    }
}
