
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_KHO_KHUVUC_HANGBussiness
    {
        bool InsertOrUpdateDM_KHO_KHUVUC_HANG(DataTable dst);
        bool DeleteDM_KHO_KHUVUC_HANG(string pMA_DVIQLY, long pKHO_KHUVUC_HANG_ID, string pModifiedBy);
        DataTable GetListDM_KHO_KHUVUC_HANG(string pMA_DVIQLY, long pKHO_ID, long pKHO_KHUVUC_ID);
        DataTable GetListDM_KHO_KHUVUC_HANG_TENTT(string pMA_DVIQLY);
        bool DM_KHO_KHUVUC_HANG_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_HANG_ID, long pKHO_ID, long pKHO_KHUVUC_ID, string pMA_TINHTRANG_HANG);
    }
    public class DM_KHO_KHUVUC_HANGBussiness : IDM_KHO_KHUVUC_HANGBussiness
    {
        IDM_KHO_KHUVUC_HANGProvider _DM_KHO_KHUVUC_HANGProvider;
        public DM_KHO_KHUVUC_HANGBussiness(string appId, string userId = "0")
        {
            _DM_KHO_KHUVUC_HANGProvider = new DM_KHO_KHUVUC_HANGProvider(appId, userId);
        }
        public bool InsertOrUpdateDM_KHO_KHUVUC_HANG(DataTable dst)
        {
            return _DM_KHO_KHUVUC_HANGProvider.InsertOrUpdateDM_KHO_KHUVUC_HANG(dst);
        }
        public bool DeleteDM_KHO_KHUVUC_HANG(string pMA_DVIQLY, long pKHO_KHUVUC_HANG_ID, string pModifiedBy)
        {
            return _DM_KHO_KHUVUC_HANGProvider.DeleteDM_KHO_KHUVUC_HANG(pMA_DVIQLY, pKHO_KHUVUC_HANG_ID, pModifiedBy);
        }
        public DataTable GetListDM_KHO_KHUVUC_HANG(string pMA_DVIQLY, long pKHO_ID, long pKHO_KHUVUC_ID)
        {
            return _DM_KHO_KHUVUC_HANGProvider.GetListDM_KHO_KHUVUC_HANG(pMA_DVIQLY, pKHO_ID, pKHO_KHUVUC_ID);
        }
        public DataTable GetListDM_KHO_KHUVUC_HANG_TENTT(string pMA_DVIQLY)
        {
            return _DM_KHO_KHUVUC_HANGProvider.GetListDM_KHO_KHUVUC_HANG_TENTT(pMA_DVIQLY);
        }
        public bool DM_KHO_KHUVUC_HANG_CHECKEXISTS(string pMA_DVIQLY, long pKHO_KHUVUC_HANG_ID, long pKHO_ID, long pKHO_KHUVUC_ID, string pMA_TINHTRANG_HANG)
        {
            return _DM_KHO_KHUVUC_HANGProvider.DM_KHO_KHUVUC_HANG_CHECKEXISTS(pMA_DVIQLY, pKHO_KHUVUC_HANG_ID, pKHO_ID, pKHO_KHUVUC_ID, pMA_TINHTRANG_HANG);
        }
    }
}
