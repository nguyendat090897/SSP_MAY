
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_SanPham_ThungVLBussiness
    {
        int InsertorUpdateDM_SanPham_ThungVL(DataTable dtInput);
        int DeleteDM_SanPham_ThungVL(string MA_DVIQLY, int SANPHAM_THUNG_VATLY_ID, string ModifiledBy);
        DataTable GetListDM_SanPham_ThungVL(string MA_DVIQLY, int SANPHAM_ID);

    }
    public class DM_SanPham_ThungVLBussiness : IDM_SanPham_ThungVLBussiness
    {
        IDM_SanPham_ThungVLProvider _IDM_SanPham_ThungVLProvider;
        public DM_SanPham_ThungVLBussiness(string appId, string userId = "0")
        {
            _IDM_SanPham_ThungVLProvider = new DM_SanPham_ThungVLProvider(appId, userId);
        }
        public int InsertorUpdateDM_SanPham_ThungVL(DataTable dtInput)
        {
            return _IDM_SanPham_ThungVLProvider.InsertorUpdateDM_SanPham_ThungVL(dtInput);
        }
        public int DeleteDM_SanPham_ThungVL(string MA_DVIQLY, int SANPHAM_THUNG_VATLY_ID, string ModifiledBy)
        {
            return _IDM_SanPham_ThungVLProvider.DeleteDM_SanPham_ThungVL(MA_DVIQLY, SANPHAM_THUNG_VATLY_ID, ModifiledBy);
        }
        public DataTable GetListDM_SanPham_ThungVL(string MA_DVIQLY, int SANPHAM_ID)
        {
            return _IDM_SanPham_ThungVLProvider.GetListDM_SanPham_ThungVL(MA_DVIQLY, SANPHAM_ID);
        }

    }
}
