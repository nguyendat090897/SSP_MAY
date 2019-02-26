
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_SANPHAM_SLUONG_MINMAX_KHUVUCBussiness
    {
        bool InsertOrUpdateDM_SANPHAM_SLUONG_MINMAX_KHUVUC(DataTable dst);
        DataTable DeleteDM_SANPHAM_SLUONG_MINMAX_KHUVUC(string pMA_DVIQLY, long @pSANPHAM_SLUONG_MINMAX_KHUVUC_ID, string pModifiedBy);
        DataTable GetListDM_SANPHAM_SLUONG_MINMAX_KHUVUC(string pMA_DVIQLY, long pKHO_KHUVUC_ID);

    }
    public class DM_SANPHAM_SLUONG_MINMAX_KHUVUCBussiness : IDM_SANPHAM_SLUONG_MINMAX_KHUVUCBussiness
    {
        IDM_SANPHAM_SLUONG_MINMAX_KHUVUCProvider _DM_SANPHAM_SLUONG_MINMAX_KHUVUCProvider;
        public DM_SANPHAM_SLUONG_MINMAX_KHUVUCBussiness(string appId, string userId = "0")
        {
            _DM_SANPHAM_SLUONG_MINMAX_KHUVUCProvider = new DM_SANPHAM_SLUONG_MINMAX_KHUVUCProvider(appId, userId);
        }
        public bool InsertOrUpdateDM_SANPHAM_SLUONG_MINMAX_KHUVUC(DataTable dst)
        {
            return _DM_SANPHAM_SLUONG_MINMAX_KHUVUCProvider.InsertOrUpdateDM_SANPHAM_SLUONG_MINMAX_KHUVUC(dst);
        }
        public DataTable DeleteDM_SANPHAM_SLUONG_MINMAX_KHUVUC(string pMA_DVIQLY, long @pSANPHAM_SLUONG_MINMAX_KHUVUC_ID, string pModifiedBy)
        {
            return _DM_SANPHAM_SLUONG_MINMAX_KHUVUCProvider.DeleteDM_SANPHAM_SLUONG_MINMAX_KHUVUC(pMA_DVIQLY, @pSANPHAM_SLUONG_MINMAX_KHUVUC_ID, pModifiedBy);
        }
        public DataTable GetListDM_SANPHAM_SLUONG_MINMAX_KHUVUC(string pMA_DVIQLY, long pKHO_KHUVUC_ID)
        {
            return _DM_SANPHAM_SLUONG_MINMAX_KHUVUCProvider.GetListDM_SANPHAM_SLUONG_MINMAX_KHUVUC(pMA_DVIQLY, pKHO_KHUVUC_ID);
        }
    }
}
