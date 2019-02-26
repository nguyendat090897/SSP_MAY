
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_SanPhamBussiness
    {
        int InsertOrUpdateSanPham(string MA_DVIQLY, DataTable dtInput, string User);
        int DeleteSanPham(string MA_DVIQLY, int SANPHAM_ID, string User);

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
    public class SanPhamBussiness : IDM_SanPhamBussiness
    {
        IDM_SanPhamProvider _DM_SAN_PHAMProvider;
        public SanPhamBussiness(string appId, string userId = "0")
        {
            _DM_SAN_PHAMProvider = new SanPhamProvider(appId, userId);
        }

        public DataTable GetData_For_Cbo_Loai_SPham(string MA_DVIQLY)
        {
            return _DM_SAN_PHAMProvider.GetData_For_Cbo_Loai_SPham(MA_DVIQLY);
        }
        public DataTable GetMax_Id_SanPham(string MA_DVQL)
        {
            return _DM_SAN_PHAMProvider.GetMax_Id_SanPham(MA_DVQL);
        }
        public int InsertOrUpdateSanPham(string MA_DVIQLY,DataTable dt, string User)
        {
            return _DM_SAN_PHAMProvider.InsertOrUpdateSanPham(MA_DVIQLY,dt, User);
        }
        
        public int DeleteSanPham(string MA_DVIQLY,int SANPHAM_ID, string User)
        {
            return _DM_SAN_PHAMProvider.DeleteSanPham(MA_DVIQLY,SANPHAM_ID, User);
        }
        public DataTable GetList_SanPham(string MA_DVIQLY)
        {
            return _DM_SAN_PHAMProvider.GetList_SanPham(MA_DVIQLY);
        }

        public DataTable GetList_SanPham_By_Loai_SPham_And_ItemType(string MA_DVIQLY, int LOAI_SPHAM_ID, string MA_ITEM_TYPE)
        {
            return _DM_SAN_PHAMProvider.GetList_SanPham_By_Loai_SPham_And_ItemType(MA_DVIQLY,LOAI_SPHAM_ID, MA_ITEM_TYPE);
        }
        

        public DataTable GetData_For_Cbo_Item_Type()
        {
            return _DM_SAN_PHAMProvider.GetData_For_Cbo_Item_Type();
        }

        public DataTable GetData_ForGrd_Loai_SPham(string MA_DVIQLY, string MA_ITEM_TYPE)
        {
            return _DM_SAN_PHAMProvider.GetData_ForGrd_Loai_SPham(MA_DVIQLY, MA_ITEM_TYPE);
        }

        public DataTable GetData_For_Cbo_Nhom_SPham(string MA_DVIQLY)
        {
            return _DM_SAN_PHAMProvider.GetData_For_Cbo_Nhom_SPham(MA_DVIQLY);
        }

        public DataTable GetData_For_Cbo_Nguon_Nhap(string MA_DVIQLY)
        {
            return _DM_SAN_PHAMProvider.GetData_For_Cbo_Nguon_Nhap(MA_DVIQLY);
        }

        public DataTable GetData_For_Cbo_DKBQ(string MA_DVIQLY)
        {
            return _DM_SAN_PHAMProvider.GetData_For_Cbo_DKBQ(MA_DVIQLY);
        }

        public DataTable GetData_For_Cbo_NSX(string MA_DVIQLY)
        {
            return _DM_SAN_PHAMProvider.GetData_For_Cbo_NSX(MA_DVIQLY);
        }

        public DataTable GetData_For_Cbo_QuocGia(string MA_DVIQLY)
        {
            return _DM_SAN_PHAMProvider.GetData_For_Cbo_QuocGia(MA_DVIQLY);
        }

        public DataTable GetData_For_Cbo_KhachHang_Ncc(string MA_DVIQLY)
        {
            return _DM_SAN_PHAMProvider.GetData_For_Cbo_KhachHang_Ncc(MA_DVIQLY);
        }

        public DataTable GetData_For_Cbo_LoaiKichThuoc(string MA_DVIQLY)
        {
            return _DM_SAN_PHAMProvider.GetData_For_Cbo_LoaiKichThuoc(MA_DVIQLY);
        }

        public DataTable GetData_For_Cbo_DVT()
        {
            return _DM_SAN_PHAMProvider.GetData_For_Cbo_DVT();
        }

        public int Check_Exist_Ma_SPham(string MA_DVIQLY,string MA_SANPHAM)
        {
            return _DM_SAN_PHAMProvider.Check_Exist_Ma_SPham(MA_DVIQLY,MA_SANPHAM);
        }

        public int Check_Exist_Ma_SPham_Update(string MA_DVIQLY, string MA_SANPHAM, int SANPHAM_ID)
        {
            return _DM_SAN_PHAMProvider.Check_Exist_Ma_SPham_Update(MA_DVIQLY,MA_SANPHAM, SANPHAM_ID);
        }
    }
}
