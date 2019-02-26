
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IGPNKBussiness
    {
        int InsertorUpdateGPNK(DataTable dtInput);
        int DeleteGPNK(string MA_DVIQLY, int SANPHAM_GPNK_ID, string ModifiledBy);
        DataTable GetList_GPNK(string MA_DVIQLY, int SANPHAM_ID);

    }
    public class GPNKBussiness : IGPNKBussiness
    {
        IGPNKProvider _GPNKProvider;
        public GPNKBussiness(string appId, string userId = "0")
        {
            _GPNKProvider = new GPNKProvider(appId, userId);
        }

        public int InsertorUpdateGPNK(DataTable dtInput)
        {
            return _GPNKProvider.InsertorUpdateGPNK(dtInput);
        }
        public int DeleteGPNK(string MA_DVIQLY, int SANPHAM_GPNK_ID, string ModifiledBy)
        {
            return _GPNKProvider.DeleteGPNK(MA_DVIQLY, SANPHAM_GPNK_ID, ModifiledBy);
        }
        public DataTable GetList_GPNK(string MA_DVIQLY, int SANPHAM_ID)
        {
            return _GPNKProvider.GetList_GPNK(MA_DVIQLY, SANPHAM_ID);
        }
        
    }
}
