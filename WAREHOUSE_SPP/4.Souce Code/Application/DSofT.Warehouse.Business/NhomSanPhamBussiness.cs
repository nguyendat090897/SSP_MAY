
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_NHOM_SAN_PHAMBussiness
    {
        int InsertorUpdateDM_NHOM_SAN_PHAM(DataTable dt);
        int DeleteDM_NHOM_SAN_PHAM(string MA_DVIQLY, int NHOM_SPHAM_ID, string ModifiledBy);
        DataTable GetListDM_NHOM_SAN_PHAM(string MA_DVIQLY);

        int Check_Exist_MA_NHOM_SAN_PHAM(string MA_DVIQLY, string MA_NHOM_SPHAM);

        int Check_Exist_MA_NHOM_SAN_PHAM_For_Update(string MA_NHOM_SPHAM, int NHOM_SPHAM_ID);

        DataTable GetList_ITEM_TYPE();

    }
    public class DM_NHOM_SAN_PHAMBussiness : IDM_NHOM_SAN_PHAMBussiness
    {
        IDM_NHOM_SAN_PHAM_Provider _DM_NHOM_SAN_PHAMProvider;
        public DM_NHOM_SAN_PHAMBussiness(string appId, string userId = "0")
        {
            _DM_NHOM_SAN_PHAMProvider = new DM_NHOM_SAN_PHAM_Provider(appId, userId);
        }
        public int InsertorUpdateDM_NHOM_SAN_PHAM(DataTable dt)
        {
            return _DM_NHOM_SAN_PHAMProvider.InsertorUpdateDM_NHOM_SAN_PHAM(dt);
        }
        public int DeleteDM_NHOM_SAN_PHAM(string MA_DVIQLY, int NHOM_SPHAM_ID, string ModifiledBy)
        {
            return _DM_NHOM_SAN_PHAMProvider.DeleteDM_NHOM_SAN_PHAM(MA_DVIQLY, NHOM_SPHAM_ID, ModifiledBy);
        }
        public DataTable GetListDM_NHOM_SAN_PHAM(string MA_DVIQLY)
        {
            return _DM_NHOM_SAN_PHAMProvider.GetListDM_NHOM_SAN_PHAM(MA_DVIQLY);
        }

        public int Check_Exist_MA_NHOM_SAN_PHAM(string MA_DVIQLY, string MA_NHOM_SPHAM)
        {
            return _DM_NHOM_SAN_PHAMProvider.Check_Exist_MA_NHOM_SAN_PHAM(MA_DVIQLY, MA_NHOM_SPHAM);
        }

        public int Check_Exist_MA_NHOM_SAN_PHAM_For_Update(string MA_NHOM_SPHAM, int NHOM_SPHAM_ID)
        {
            return _DM_NHOM_SAN_PHAMProvider.Check_Exist_MA_NHOM_SAN_PHAM_For_Update(MA_NHOM_SPHAM, NHOM_SPHAM_ID);
        }
        public DataTable GetList_ITEM_TYPE()
        {
            return _DM_NHOM_SAN_PHAMProvider.GetList_ITEM_TYPE();
        }
    }
}
