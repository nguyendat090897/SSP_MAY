using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{//
    public interface IDM_KHACHHANG_NCCProvider
    {
        bool InsertorUpdateDM_KHACHHANG_NCC(DataTable dst);
        bool DeleteDM_KHACHHANG_NCC( long pKHACHHANG_NCC_ID, string pMA_DVIQLY,string pUser);
        bool DeleteDM_KHACHHANG_NCC_DIA_DIEM_XUAT_HANG( string pMA_DVIQLY,long pKHACHHANG_NCC_NOI_XUATHANG_ID,string pUser);
        bool DeleteDM_KHACHHANG_NCC_DON_VI_VAN_CHUYEN( string pMA_DVIQLY,long pKHACHHANG_NCC_DONVI_VANCHUYEN_ID, string pUser);
        DataTable GetListDM_KHACHHANG_NCC(string pMA_DVIQLY);
        DataTable GetListDM_KHACHHANG_NCC_DIA_DIEM_XUAT_HANG(string pMA_DVIQLY,long pKHACHHANG_NCC_ID);
        DataTable GetListDM_KHACHHANG_NCC_DON_VI_VAN_CHUYEN(string pMA_DVIQLY, long pKHACHHANG_NCC_ID);
        DataTable GetListDM_KHACHHANG_NCC_TINH_THANHPHO();
        DataTable GetListDM_KHACHHANG_NCC_KHACHHANG(string pMA_DVIQLY);
        DataTable GetListDM_KHACHHANG_NCC_BY_MAKH(string pMA_DVIQLY,string pMA_KH);
        DataTable GetKHACHHANG_NCC_ID(string pMA_DVIQLY, string pMA_KH);
        DataTable GetListDM_KHACHHANG_NCC_BY_LOAI_KH_NCC(string pMA_DVIQLY, string pLOAI_KH_NCC);
        bool InsertorUpdateDIA_DIEM_XUAT_HANG(DataTable dst, string pUser,long pKHACHHANG_NCC_ID);
        bool InsertorUpdateDON_VI_VAN_CHUYEN(DataTable dst, string pUser, long pKHACHHANG_NCC_ID);
    }
    public class DM_KHACHHANG_NCCProvider : BaseSqlExecute, IDM_KHACHHANG_NCCProvider
    {
        public DM_KHACHHANG_NCCProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }
        public bool InsertorUpdateDM_KHACHHANG_NCC(DataTable dst)
        {
            if (NumberHelper.ConvertStringToLong(dst.Rows[0]["KHACHHANG_NCC_ID"].ToString()) > 0)
            {
                var paramObj = new object[]{ dst.Rows[0]["KHACHHANG_NCC_ID"],
                            ConstCommon.DonViQuanLy,
                            dst.Rows[0]["LOAI_KHNCC"],
                            dst.Rows[0]["MA_KH"],
                            dst.Rows[0]["TEN_KH"],
                            dst.Rows[0]["DIA_CHI"],
                            dst.Rows[0]["TINH_ID"],
                            dst.Rows[0]["MA_SOTHUE"],
                            dst.Rows[0]["TK_NGANHANG"],
                            dst.Rows[0]["TEN_NGANHANG"],
                            dst.Rows[0]["DIENTHOAI"],
                            dst.Rows[0]["FAX"],
                            dst.Rows[0]["LOAI_KHACHHANG_ID"],
                            dst.Rows[0]["NGUOI_LIENHE"],
                            dst.Rows[0]["GIOI_TINH"],
                            dst.Rows[0]["CHUC_DANH"],
                            dst.Rows[0]["DIENTHOAI_CQ"],
                            dst.Rows[0]["DIENTHOAI_NR"],
                            dst.Rows[0]["DIENTHOAI_DD"],
                            dst.Rows[0]["CMND"],
                            dst.Rows[0]["NGAY_CAP"],
                            dst.Rows[0]["NOI_CAP"],
                            dst.Rows[0]["EMAIL"],
                            dst.Rows[0]["DIA_CHI_LHE"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            CommonDataHelper.UserName
                             };
                var result = base.ExecScalar("DM_KHACHHANG_NCC_UPDATE", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                var paramObj = new object[]{
                             ConstCommon.DonViQuanLy,
                            dst.Rows[0]["LOAI_KHNCC"],
                            dst.Rows[0]["MA_KH"],
                            dst.Rows[0]["TEN_KH"],
                            dst.Rows[0]["DIA_CHI"],
                            dst.Rows[0]["TINH_ID"],
                            dst.Rows[0]["MA_SOTHUE"],
                            dst.Rows[0]["TK_NGANHANG"],
                            dst.Rows[0]["TEN_NGANHANG"],
                            dst.Rows[0]["DIENTHOAI"],
                            dst.Rows[0]["FAX"],
                            dst.Rows[0]["LOAI_KHACHHANG_ID"],
                            dst.Rows[0]["NGUOI_LIENHE"],
                            dst.Rows[0]["GIOI_TINH"],
                            dst.Rows[0]["CHUC_DANH"],
                            dst.Rows[0]["DIENTHOAI_CQ"],
                            dst.Rows[0]["DIENTHOAI_NR"],
                            dst.Rows[0]["DIENTHOAI_DD"],
                            dst.Rows[0]["CMND"],
                            dst.Rows[0]["NGAY_CAP"],
                            dst.Rows[0]["NOI_CAP"],
                            dst.Rows[0]["EMAIL"],
                            dst.Rows[0]["DIA_CHI_LHE"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            CommonDataHelper.UserName
                            };
                var result =base.ExecScalar("DM_KHACHHANG_NCC_INSERT", paramObj);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool DeleteDM_KHACHHANG_NCC_DIA_DIEM_XUAT_HANG(string pMA_DVIQLY, long pKHACHHANG_NCC_NOI_XUATHANG_ID, string pUser)
        {
            var paramObj = new object[] { pMA_DVIQLY, pKHACHHANG_NCC_NOI_XUATHANG_ID, pUser };
            var result = base.ExecScalar("DM_KHACHHANG_NCC_NOI_XUATHANG_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool DeleteDM_KHACHHANG_NCC_DON_VI_VAN_CHUYEN(string pMA_DVIQLY, long pKHACHHANG_NCC_DONVI_VANCHUYEN_ID, string pUser)
        {
            var paramObj = new object[] {  pMA_DVIQLY, pKHACHHANG_NCC_DONVI_VANCHUYEN_ID , pUser };
            var result = base.ExecScalar("DM_KHACHHANG_NCC_DONVI_VANCHUYEN_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool DeleteDM_KHACHHANG_NCC(long pKHACHHANG_NCC_ID, string pMA_DVIQLY, string pUser)
        {
            var paramObj = new object[] { pKHACHHANG_NCC_ID, pMA_DVIQLY,pUser };
            var result = base.ExecScalar("DM_KHACHHANG_NCC_DELETE", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable GetListDM_KHACHHANG_NCC(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_KHACHHANG_NCC_GET_ALL", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_KHACHHANG_NCC_TINH_THANHPHO()
        {
            var paramObj = new object[] { };
            var result = base.ExecuteDataSet("DM_KHACHHANG_NCC_GET_BY_ID_TINH", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_KHACHHANG_NCC_KHACHHANG(string pMA_DVIQLY)
        {
            var paramObj = new object[] { pMA_DVIQLY };
            var result = base.ExecuteDataSet("DM_KHACHHANG_NCC_GET_BY_ID_LOAI_KHACH_HANG", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_KHACHHANG_NCC_BY_MAKH(string pMA_DVIQLY, string pMA_KH)
        {
            var paramObj = new object[] { pMA_DVIQLY, pMA_KH };
            var result = base.ExecuteDataSet("DM_KHACHHANG_NCC_GET_TEN_BY_MA_KH", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetKHACHHANG_NCC_ID(string pMA_DVIQLY, string pMA_KH)
        {
            var paramObj = new object[] { pMA_DVIQLY, pMA_KH };
            var result = base.ExecuteDataSet("DM_KHACHHANG_NCC_NOI_XUATHANG_GET_ID_BY_MAKH", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_KHACHHANG_NCC_BY_LOAI_KH_NCC(string pMA_DVIQLY, string pLOAI_KH_NCC)
        {
            var paramObj = new object[] { pMA_DVIQLY, pLOAI_KH_NCC };
            var result = base.ExecuteDataSet("DM_KHACHHANG_NCC_GET_BY_LOAIKH_NCC", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_KHACHHANG_NCC_DIA_DIEM_XUAT_HANG(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {
            var paramObj = new object[] { pMA_DVIQLY, pKHACHHANG_NCC_ID };
            var result = base.ExecuteDataSet("DM_KHACHHANG_NCC_NOI_XUATHANG_GET_BY_ID_KHNCC", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetListDM_KHACHHANG_NCC_DON_VI_VAN_CHUYEN(string pMA_DVIQLY, long pKHACHHANG_NCC_ID)
        {
            var paramObj = new object[] { pMA_DVIQLY, pKHACHHANG_NCC_ID };
            var result = base.ExecuteDataSet("DM_KHACHHANG_NCC_DONVI_VANCHUYEN_GET_BY_ID_KHNCC", paramObj);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public bool InsertorUpdateDIA_DIEM_XUAT_HANG(DataTable dst, string pUser, long pKHACHHANG_NCC_ID)
        {
            var paramObj = new object[]{ 
                            ConstCommon.DonViQuanLy,
                            ConstCommon.NVL_NUM_LONG_NEW(dst.Rows[0]["KHACHHANG_NCC_NOI_XUATHANG_ID"]),
                            pKHACHHANG_NCC_ID,
                            dst.Rows[0]["STT"],
                            dst.Rows[0]["DIADIEM_GIAO"],
                            dst.Rows[0]["DIA_CHI"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            pUser
                             };
            var result = base.ExecScalar("DM_KHACHHANG_NCC_NOI_XUATHANG_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool InsertorUpdateDON_VI_VAN_CHUYEN(DataTable dst, string pUser,long pKHACHHANG_NCC_ID)
        {
            var paramObj = new object[]{
                            ConstCommon.DonViQuanLy,
                            ConstCommon.NVL_NUM_LONG_NEW(dst.Rows[0]["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"]),
                            pKHACHHANG_NCC_ID,
                            dst.Rows[0]["STT"],
                            dst.Rows[0]["TEN_DONVI_VANCHUYEN"],
                            dst.Rows[0]["DIA_CHI"],
                            dst.Rows[0]["GHI_CHU"],
                            dst.Rows[0]["TINH_TRANG"],
                            pUser
                             };
            var result = base.ExecScalar("DM_KHACHHANG_NCC_DONVI_VANCHUYEN_INSERT", paramObj);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
    }
}