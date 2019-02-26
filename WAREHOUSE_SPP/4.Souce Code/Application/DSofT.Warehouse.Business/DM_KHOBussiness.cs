
using DSofT.Warehouse.Provider;
using System.Data;
using System.Transactions;

namespace DSofT.Warehouse.Business
{
    public interface IDM_KHOBussiness
    {
        bool InsertOrUpdateDM_KHO(DataTable dst);
        bool DeleteDM_KHO(string pMA_DVIQLY, long pKHO_ID, string pModifiedBy);
        DataTable GetListDM_KHO(string pMA_DVIQLY);
        DataTable GetListDM_KHO_TENDKBQ(string pMA_DVIQLY);
        DataTable GetListDM_KHO_MA(string pMA_DVIQLY, string pMA_KHO);
        DataTable GetListDM_KHO_ID(string pMA_DVIQLY);
        DataTable GetListUser(int Kho_ID);
        bool Delete_DM_User(int kho_id);
        bool InsertOrUpdateDM_KHO_User(DataTable dst);
    }
    public class DM_KHOBussiness : IDM_KHOBussiness
    {
        IDM_KHOProvider _DM_KHOProvider;
        public DM_KHOBussiness(string appId, string userId = "0")
        {
            _DM_KHOProvider = new DM_KHOProvider(appId, userId);
        }
        public bool InsertOrUpdateDM_KHO(DataTable dst)
        {
            return _DM_KHOProvider.InsertOrUpdateDM_KHO(dst);
        }
        public bool DeleteDM_KHO(string pMA_DVIQLY, long pKHO_ID, string pModifiedBy)
        {
            return _DM_KHOProvider.DeleteDM_KHO(pMA_DVIQLY, pKHO_ID, pModifiedBy);
        }
        public DataTable GetListDM_KHO(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.GetListDM_KHO(pMA_DVIQLY);
        }
        public DataTable GetListDM_KHO_TENDKBQ(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.GetListDM_KHO_TENDKBQ(pMA_DVIQLY);
        }

        public DataTable GetListDM_KHO_MA(string pMA_DVIQLY, string pMA_KHO)
        {
            return _DM_KHOProvider.GetListDM_KHO_MA(pMA_DVIQLY,  pMA_KHO);
        }

        public DataTable GetListDM_KHO_ID(string pMA_DVIQLY)
        {
            return _DM_KHOProvider.GetListDM_KHO_ID(pMA_DVIQLY);
        }
        public DataTable GetListUser(int Kho_ID)
        {
            return _DM_KHOProvider.GetListUser(Kho_ID);
        }
        public bool Delete_DM_User(int kho_id)
        {
            return _DM_KHOProvider.Delete_DM_User(kho_id);
        }
        public bool InsertOrUpdateDM_KHO_User(DataTable dst)
        {
            bool result =false;
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                if (dst.Rows.Count > 0)
                {
                    for (int i = 0; i < dst.Rows.Count; i++)
                    {

                        result = _DM_KHOProvider.InsertOrUpdateDM_KHO_User(dst.Rows[i]);
                    }
                }
                else
                    return false;
                trans.Complete();
            }
            if (result == true)
                return true;
            else
                return false;
        }
    }
}
