using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IDM_SanPhamProvider
    {
        int InsertOrUpdateSanPham(string MA_DVIQLY,DataTable dtInput, string User);
        int DeleteSanPham(string MA_DVIQLY,int SANPHAM_ID, string User);

        DataTable GetList_SanPham(string MA_DVQL);

        DataTable GetList_SanPham_By_Loai_SPham_And_ItemType(string MA_DVIQLY, int LOAI_SPHAM_ID, string MA_ITEM_TYPE);

        DataTable GetData_For_Cbo_Item_Type();

        DataTable GetData_ForGrd_Loai_SPham(string MA_DVIQLY, string MA_ITEM_TYPE);

        DataTable GetData_For_Cbo_Loai_SPham(string MA_DVIQLY);

        DataTable GetData_For_Cbo_Nhom_SPham(string MA_DVIQLY);

        DataTable GetData_For_Cbo_Nguon_Nhap(string MA_DVIQLY);

        DataTable GetData_For_Cbo_DKBQ(string MA_DVIQLY);

        DataTable GetData_For_Cbo_NSX(string MA_DVIQLY);

        DataTable GetData_For_Cbo_QuocGia(string MA_DVIQLY);

        DataTable GetData_For_Cbo_KhachHang_Ncc(string MA_DVIQLY);

        DataTable GetData_For_Cbo_LoaiKichThuoc(string MA_DVIQLY);

        DataTable GetData_For_Cbo_DVT();
        int Check_Exist_Ma_SPham(string MA_DVIQLY, string MA_SANPHAM);

        int Check_Exist_Ma_SPham_Update(string MA_DVIQLY, string MA_SANPHAM, int SANPHAM_ID);

        DataTable GetMax_Id_SanPham(string MA_DVQL);
    }
    public class SanPhamProvider : BaseSqlExecute, IDM_SanPhamProvider
    {

        public SanPhamProvider(string appId, string userId)
            : base(DbCommon.WarehouseDbConnstr, appId, userId)
        {
        }
        /// <summary>
        /// Get danh sách trường học
        /// </summary>
        /// <returns></returns>
        
        public DataTable GetList_SanPham(string MA_DVQL)
        {
            var sqlParams = new object[] { MA_DVQL };
            var result = base.ExecuteDataSet("DM_SANPHAM_GET_LIST_ALL", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        

        public DataTable GetMax_Id_SanPham(string MA_DVQL)
        {
            var sqlParams = new object[] { MA_DVQL };
            var result = base.ExecuteDataSet("DM_SPHAM_GET_MAX_IDSP", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetList_SanPham_By_Loai_SPham_And_ItemType(string MA_DVIQLY,int LOAI_SPHAM_ID, string MA_ITEM_TYPE)
        {
            var sqlParams = new object[] {MA_DVIQLY, LOAI_SPHAM_ID, MA_ITEM_TYPE };
            var result = base.ExecuteDataSet("DM_SANPHAM_GET_SPHAM_BY_LOAI_SPHAM_AND_ITEMTYPE", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        public DataTable GetData_For_Cbo_Loai_SPham(string MA_DVIQLY)
        {
            var sqlParams = new object[] { MA_DVIQLY};
            var result = base.ExecuteDataSet("DM_SPHAM_GET_LOAI_SPHAM_Cbo", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        /// <summary>
        /// Insert hoặc Update loại món ăn
        /// </summary>
        /// <returns></returns>
        public int InsertOrUpdateSanPham(string MA_DVIQLY,DataTable dtInput,string User)
        {
            //thuc hien update
            if (NumberHelper.ConvertStringToLong(dtInput.Rows[0]["SANPHAM_ID"].ToString()) > 0)
            {
                var sqlParams = new object[] {
                                               MA_DVIQLY
                                             ,dtInput.Rows[0]["SANPHAM_ID"]
                                             ,dtInput.Rows[0]["MA_ITEM_TYPE"]
                                             , dtInput.Rows[0]["MA_SANPHAM_KH"]
                                             , dtInput.Rows[0]["MA_SANPHAM"]
                                             , dtInput.Rows[0]["TEN_SANPHAM"]
                                             , dtInput.Rows[0]["NHOM_SPHAM_ID"]
                                             , dtInput.Rows[0]["LOAI_SPHAM_ID"]
                                             , dtInput.Rows[0]["MA_VACH"]
                                             , dtInput.Rows[0]["IS_KSDB"]
                                             , dtInput.Rows[0]["IS_QA"]
                                             , dtInput.Rows[0]["HOATCHAT_CHINH"]
                                             , dtInput.Rows[0]["MA_DONVI_TINH"]
                                             , dtInput.Rows[0]["TON_TOITHIEU"]
                                             , dtInput.Rows[0]["SONGAY_CANHBAOTRUOC"]
                                             , dtInput.Rows[0]["NGUON_NHAP_ID"]
                                             , dtInput.Rows[0]["DKIEN_BQUAN_ID"]
                                             , dtInput.Rows[0]["GIABAN_LE"]
                                             , dtInput.Rows[0]["GIABAN_BUON"]
                                             , dtInput.Rows[0]["VITRI_DEXUAT"]
                                             , dtInput.Rows[0]["NHA_SXUAT_ID"]
                                             , dtInput.Rows[0]["QUOCGIA_ID"]
                                             , dtInput.Rows[0]["KHACHHANG_NCC_ID"]
                                             , dtInput.Rows[0]["THUE_VAT"]
                                             , dtInput.Rows[0]["CHIET_KHAU"]
                                             , dtInput.Rows[0]["THUONG_DS"]
                                             , dtInput.Rows[0]["PATH_IMAGE"]
                                             , dtInput.Rows[0]["SOLUONG_THUNG"]
                                             , dtInput.Rows[0]["MA_DONVI_TINH_THUNG"]
                                             , dtInput.Rows[0]["TRONG_LUONG_KG"]
                                             , dtInput.Rows[0]["LOAI_KTHUOC_ID"]
                                             , dtInput.Rows[0]["THE_TICH_M3"]
                                             , dtInput.Rows[0]["SOLUONG_THUNG_PALLET"]
                                             , dtInput.Rows[0]["SO_DANGKY"]
                                             , dtInput.Rows[0]["NGAYKY_DANGKY"]
                                             , dtInput.Rows[0]["NGAYHH_DANGKY"]
                                             , dtInput.Rows[0]["SO_XACNHAN"]
                                             , dtInput.Rows[0]["NGAYKY_XACNHAN"]
                                             , dtInput.Rows[0]["NGAYHH_XACNHAN"]
                                             , dtInput.Rows[0]["GHI_CHU"]
                                             , dtInput.Rows[0]["TINH_TRANG"]
                                             , User
                                             };
                var result = base.ExecScalar("DM_SANPHAM_UPDATE", sqlParams);
                if (int.Parse(result.ToString()) > 0)
                {
                    return 1;
                }
            }
            else
            {
                   var sqlParams = new object[] {
                                             MA_DVIQLY
                                             ,dtInput.Rows[0]["MA_ITEM_TYPE"]
                                             , dtInput.Rows[0]["MA_SANPHAM_KH"]
                                             , dtInput.Rows[0]["MA_SANPHAM"]
                                             , dtInput.Rows[0]["TEN_SANPHAM"]
                                             , dtInput.Rows[0]["NHOM_SPHAM_ID"]
                                             , dtInput.Rows[0]["LOAI_SPHAM_ID"]
                                             , dtInput.Rows[0]["MA_VACH"]
                                             , dtInput.Rows[0]["IS_KSDB"]
                                             , dtInput.Rows[0]["IS_QA"]
                                             , dtInput.Rows[0]["HOATCHAT_CHINH"]
                                             , dtInput.Rows[0]["MA_DONVI_TINH"]
                                             , dtInput.Rows[0]["TON_TOITHIEU"]
                                             , dtInput.Rows[0]["SONGAY_CANHBAOTRUOC"]
                                             , dtInput.Rows[0]["NGUON_NHAP_ID"]
                                             , dtInput.Rows[0]["DKIEN_BQUAN_ID"]
                                             , dtInput.Rows[0]["GIABAN_LE"]
                                             , dtInput.Rows[0]["GIABAN_BUON"]
                                             , dtInput.Rows[0]["VITRI_DEXUAT"]
                                             , dtInput.Rows[0]["NHA_SXUAT_ID"]
                                             , dtInput.Rows[0]["QUOCGIA_ID"]
                                             , dtInput.Rows[0]["KHACHHANG_NCC_ID"]
                                             , dtInput.Rows[0]["THUE_VAT"]
                                             , dtInput.Rows[0]["CHIET_KHAU"]
                                             , dtInput.Rows[0]["THUONG_DS"]
                                             , dtInput.Rows[0]["PATH_IMAGE"]
                                             , dtInput.Rows[0]["SOLUONG_THUNG"]
                                             , dtInput.Rows[0]["MA_DONVI_TINH_THUNG"]
                                             , dtInput.Rows[0]["TRONG_LUONG_KG"]
                                             , dtInput.Rows[0]["LOAI_KTHUOC_ID"]
                                             , dtInput.Rows[0]["THE_TICH_M3"]
                                             , dtInput.Rows[0]["SOLUONG_THUNG_PALLET"]
                                             , dtInput.Rows[0]["SO_DANGKY"]
                                             , dtInput.Rows[0]["NGAYKY_DANGKY"]
                                             , dtInput.Rows[0]["NGAYHH_DANGKY"]
                                             , dtInput.Rows[0]["SO_XACNHAN"]
                                             , dtInput.Rows[0]["NGAYKY_XACNHAN"]
                                             , dtInput.Rows[0]["NGAYHH_XACNHAN"]
                                             , dtInput.Rows[0]["GHI_CHU"]
                                             , dtInput.Rows[0]["TINH_TRANG"]
                                             , User
                                             };
                var result = base.ExecScalar("DM_SANPHAM_INSERT", sqlParams);
                if (int.Parse(result.ToString()) > 0)
                {
                    return 1;
                }
            }
            return 0;
        }
        /// <summary>
        /// DeleteWarehouse
        /// </summary>
        /// <param name="dtInput"></param>
        /// <returns></returns>
        public int DeleteSanPham(string MA_DVIQLY,int SANPHAM_ID, string User)
        {
                var sqlParams = new object[] {
                                             MA_DVIQLY
                                             ,SANPHAM_ID
                                             , User
                                             };
                var result = base.ExecScalar("DM_SANPHAM_DELETE", sqlParams);
                if (int.Parse(result.ToString()) > 0)
                {
                    return 1;
                }
            return 0;
        }
        

        public DataTable GetData_For_Cbo_Item_Type()
        {
            var sqlParams = new object[] { };
            var result = base.ExecuteDataSet("DM_NHOM_SPHAM_GET_ITEM_TYPE", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetData_ForGrd_Loai_SPham(string MA_DVIQLY, string MA_ITEM_TYPE)
        {
            var sqlParams = new object[] { MA_DVIQLY, MA_ITEM_TYPE };
            var result = base.ExecuteDataSet("DM_SPHAM_GET_LOAI_SPHAM", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetData_For_Cbo_Nhom_SPham(string MA_DVIQLY)
        {
            var sqlParams = new object[] { MA_DVIQLY };
            var result = base.ExecuteDataSet("DM_SPHAM_GET_NHOM_SPHAM", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetData_For_Cbo_Nguon_Nhap(string MA_DVIQLY)
        {
            var sqlParams = new object[] { MA_DVIQLY };
            var result = base.ExecuteDataSet("DM_SPHAM_GET_NGUON_NHAP", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetData_For_Cbo_DKBQ(string MA_DVIQLY)
        {
            var sqlParams = new object[] { MA_DVIQLY };
            var result = base.ExecuteDataSet("DM_SPHAM_GET_DKIEN_BQUAN", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetData_For_Cbo_NSX(string MA_DVIQLY)
        {
            var sqlParams = new object[] { MA_DVIQLY };
            var result = base.ExecuteDataSet("DM_SPHAM_GET_NHA_SXUAT", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetData_For_Cbo_QuocGia(string MA_DVIQLY)
        {
            var sqlParams = new object[] { MA_DVIQLY };
            var result = base.ExecuteDataSet("DM_SPHAM_GET_QUOCGIA", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetData_For_Cbo_KhachHang_Ncc(string MA_DVIQLY)
        {
            var sqlParams = new object[] { MA_DVIQLY };
            var result = base.ExecuteDataSet("DM_SPHAM_GET_KHACHHANG_NCC", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetData_For_Cbo_LoaiKichThuoc(string MA_DVIQLY)
        {
            var sqlParams = new object[] { MA_DVIQLY };
            var result = base.ExecuteDataSet("DM_SPHAM_GET_KICHTHUOC", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public DataTable GetData_For_Cbo_DVT()
        {
            var sqlParams = new object[] { };
            var result = base.ExecuteDataSet("DM_SPHAM_GET_DVT", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }

        public int Check_Exist_Ma_SPham(string MA_DVIQLY,string MA_SANPHAM)
        {
            var sqlParams = new object[] { MA_DVIQLY, MA_SANPHAM };
            var result = base.ExecScalar("DM_SAN_PHAM_CHECK_EXIST_MA", sqlParams);
            return int.Parse(result.ToString());
        }

        public int Check_Exist_Ma_SPham_Update(string MA_DVIQLY,string MA_SANPHAM, int SANPHAM_ID)
        {
            var sqlParams = new object[] { MA_DVIQLY, MA_SANPHAM, SANPHAM_ID };
            var result = base.ExecScalar("DM_SAN_PHAM_CHECK_EXIST_MA_FOR_UPDATE", sqlParams);
            return int.Parse(result.ToString());
        }
    }
}
