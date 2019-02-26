
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_NHA_SXUATBussiness
    {
        int InsertorUpdateDM_NHA_SXUAT(DataTable dt);
        int DeleteDM_NHA_SXUAT(string MA_DVIQLY, int DM_NHA_SXUATID, string ModifiledBy);
        DataTable GetListDM_NHA_SXUAT(string MA_DVIQLY);

        int Check_Exist_MA_NHA_SXUAT(string MA_DVIQLY, string MA_NHA_SXUAT);

        int check_Exist_MA_NHA_SXUAT_FOR_UPDATE(string MA_DVIQLY, string MA_NHA_SXUAT, int DM_NHA_SXUATID);

        DataTable GetListDM_QUOCGIA(string MA_DVIQLY);

    }
    public class DM_NHA_SXUATBussiness : IDM_NHA_SXUATBussiness
    {
        IDM_NHA_SXUATProvider _DM_NHA_SXUATProvider;
        public DM_NHA_SXUATBussiness(string appId, string userId = "0")
        {
            _DM_NHA_SXUATProvider = new DM_NHA_SXUATProvider(appId, userId);
        }

        public DataTable GetListDM_QUOCGIA(string MA_DVIQLY)
        {
            return _DM_NHA_SXUATProvider.GetListDM_QUOCGIA(MA_DVIQLY);
        }

        public int check_Exist_MA_NHA_SXUAT_FOR_UPDATE(string MA_DVIQLY, string MA_NHA_SXUAT, int DM_NHA_SXUATID)
        {
            return _DM_NHA_SXUATProvider.check_Exist_MA_NHA_SXUAT_FOR_UPDATE(MA_DVIQLY, MA_NHA_SXUAT, DM_NHA_SXUATID);
        }
        public int Check_Exist_MA_NHA_SXUAT(string MA_DVIQLY, string MA_NHA_SXUAT)
        {
            return _DM_NHA_SXUATProvider.Check_Exist_MA_NHA_SXUAT(MA_DVIQLY, MA_NHA_SXUAT);
        }
        public int InsertorUpdateDM_NHA_SXUAT(DataTable dt)
        {
            return _DM_NHA_SXUATProvider.InsertorUpdateDM_NHA_SXUAT(dt);
        }
        
        public int DeleteDM_NHA_SXUAT(string MA_DVIQLY, int DM_NHA_SXUATID, string ModifiledBy)
        {
            return _DM_NHA_SXUATProvider.DeleteDM_NHA_SXUAT(MA_DVIQLY, DM_NHA_SXUATID, ModifiledBy);
        }
        public DataTable GetListDM_NHA_SXUAT(string MA_DVIQLY)
        {
            return _DM_NHA_SXUATProvider.GetListDM_NHA_SXUAT(MA_DVIQLY);
        }
    }
}
