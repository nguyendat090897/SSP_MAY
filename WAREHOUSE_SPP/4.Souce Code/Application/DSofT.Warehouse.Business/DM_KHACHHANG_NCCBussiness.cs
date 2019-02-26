using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{//
    public interface IDM_KHACHHANG_NCCBussiness
    {
        bool InsertorUpdateDM_KHACHHANG_NCC(DataTable dst);
        bool DeleteDM_KHACHHANG_NCC(long pKHACHHANG_NCC_ID, string pMA_DVIQLY, string pUser);
        bool DeleteDM_KHACHHANG_NCC_DIA_DIEM_XUAT_HANG(string pMA_DVIQLY, long pKHACHHANG_NCC_NOI_XUATHANG_ID, string pUser);
        bool DeleteDM_KHACHHANG_NCC_DON_VI_VAN_CHUYEN(string pMA_DVIQLY, long pKHACHHANG_NCC_DONVI_VANCHUYEN_ID, string pUser);
        DataTable GetListDM_KHACHHANG_NCC(string pMA_DVIQLY);
        DataTable GetListDM_KHACHHANG_NCC_TINH_THANHPHO();
        DataTable GetListDM_KHACHHANG_NCC_KHACHHANG(string pMA_DVIQLY);
        DataTable GetListDM_KHACHHANG_NCC_BY_MAKH(string pMA_DVIQLY, string pMA_KH);
        DataTable GetKHACHHANG_NCC_ID(string pMA_DVIQLY, string pMA_KH);
        DataTable GetListDM_KHACHHANG_NCC_BY_LOAI_KH_NCC(string pMA_DVIQLY, string pLOAI_KH_NCC);
        DataTable GetListDM_KHACHHANG_NCC_DIA_DIEM_XUAT_HANG(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable GetListDM_KHACHHANG_NCC_DON_VI_VAN_CHUYEN(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        bool InsertorUpdateDIA_DIEM_XUAT_HANG(DataTable dst, string pUser, long pKHACHHANG_NCC_ID);
        bool InsertorUpdateDON_VI_VAN_CHUYEN(DataTable dst, string pUser, long pKHACHHANG_NCC_ID);
    }
    public class DM_KHACHHANG_NCCBussiness : IDM_KHACHHANG_NCCBussiness
    {
        IDM_KHACHHANG_NCCProvider _DM_KHACHHANG_NCCProvider;
        public DM_KHACHHANG_NCCBussiness(string appId, string userId = "0")
        {
            _DM_KHACHHANG_NCCProvider = new DM_KHACHHANG_NCCProvider(appId, userId);
        }
        //insert or update
        public bool InsertorUpdateDM_KHACHHANG_NCC(DataTable dst)
        {
            return _DM_KHACHHANG_NCCProvider.InsertorUpdateDM_KHACHHANG_NCC(dst);
        }
        public bool InsertorUpdateDIA_DIEM_XUAT_HANG(DataTable dst, string pUser, long pKHACHHANG_NCC_ID)
        {
            return _DM_KHACHHANG_NCCProvider.InsertorUpdateDIA_DIEM_XUAT_HANG(dst, pUser, pKHACHHANG_NCC_ID);
        }
        public bool InsertorUpdateDON_VI_VAN_CHUYEN(DataTable dst, string pUser, long pKHACHHANG_NCC_ID)
        {
            return _DM_KHACHHANG_NCCProvider.InsertorUpdateDON_VI_VAN_CHUYEN(dst, pUser, pKHACHHANG_NCC_ID);
        }
        //delete
        public bool DeleteDM_KHACHHANG_NCC(long pKHACHHANG_NCC_ID, string pMA_DVIQLY,string pUser)
        {
            return _DM_KHACHHANG_NCCProvider.DeleteDM_KHACHHANG_NCC(pKHACHHANG_NCC_ID, pMA_DVIQLY,pUser);
        }
        public bool DeleteDM_KHACHHANG_NCC_DIA_DIEM_XUAT_HANG(string pMA_DVIQLY, long pKHACHHANG_NCC_NOI_XUATHANG_ID, string pUser)
        {
            return _DM_KHACHHANG_NCCProvider.DeleteDM_KHACHHANG_NCC_DIA_DIEM_XUAT_HANG( pMA_DVIQLY, pKHACHHANG_NCC_NOI_XUATHANG_ID, pUser);
        }
        public bool DeleteDM_KHACHHANG_NCC_DON_VI_VAN_CHUYEN(string pMA_DVIQLY, long pKHACHHANG_NCC_DONVI_VANCHUYEN_ID, string pUser)
        {
            return _DM_KHACHHANG_NCCProvider.DeleteDM_KHACHHANG_NCC_DON_VI_VAN_CHUYEN( pMA_DVIQLY, pKHACHHANG_NCC_DONVI_VANCHUYEN_ID,pUser);
        }
        //get list all
        public DataTable GetListDM_KHACHHANG_NCC(string pMA_DVIQLY)
        {
            return _DM_KHACHHANG_NCCProvider.GetListDM_KHACHHANG_NCC(pMA_DVIQLY);
        }
        public DataTable GetListDM_KHACHHANG_NCC_TINH_THANHPHO()
        {
            return _DM_KHACHHANG_NCCProvider.GetListDM_KHACHHANG_NCC_TINH_THANHPHO();
        }
        public DataTable GetListDM_KHACHHANG_NCC_KHACHHANG(string pMA_DVIQLY)
        {
            return _DM_KHACHHANG_NCCProvider.GetListDM_KHACHHANG_NCC_KHACHHANG(pMA_DVIQLY);
        }
        public DataTable GetListDM_KHACHHANG_NCC_BY_MAKH(string pMA_DVIQLY, string pMA_KH)
        {
            return _DM_KHACHHANG_NCCProvider.GetListDM_KHACHHANG_NCC_BY_MAKH(pMA_DVIQLY, pMA_KH);
        }
        public DataTable GetKHACHHANG_NCC_ID(string pMA_DVIQLY, string pMA_KH)
        {
            return _DM_KHACHHANG_NCCProvider.GetKHACHHANG_NCC_ID(pMA_DVIQLY, pMA_KH);
        }
        public DataTable GetListDM_KHACHHANG_NCC_BY_LOAI_KH_NCC(string pMA_DVIQLY, string pLOAI_KH_NCC)
        {
            return _DM_KHACHHANG_NCCProvider.GetListDM_KHACHHANG_NCC_BY_LOAI_KH_NCC(pMA_DVIQLY, pLOAI_KH_NCC);
        }
        public DataTable GetListDM_KHACHHANG_NCC_DIA_DIEM_XUAT_HANG(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {
            return _DM_KHACHHANG_NCCProvider.GetListDM_KHACHHANG_NCC_DIA_DIEM_XUAT_HANG(pMA_DVIQLY, pKHACHHANG_NCC_ID);
        }
        public DataTable GetListDM_KHACHHANG_NCC_DON_VI_VAN_CHUYEN(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {
            return _DM_KHACHHANG_NCCProvider.GetListDM_KHACHHANG_NCC_DON_VI_VAN_CHUYEN(pMA_DVIQLY, pKHACHHANG_NCC_ID);
        }

    }
}