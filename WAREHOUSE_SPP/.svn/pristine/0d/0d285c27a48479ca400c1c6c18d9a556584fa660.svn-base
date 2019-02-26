using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSofT.Warehouse.Provider;

namespace DSofT.Warehouse.Business
{
    public interface IUserTypeManagerBussiness
    {
        DataSet GetList();
        bool InsertUserType(DataSet ds);
        bool DeleteUserType(DataSet ds);
        bool UpdateUserType(DataSet ds);
    }
    
    public class UserTypeManagerBussiness: IUserTypeManagerBussiness
    {
        UserTypeManagerProvider _usertypeManagerProvider;
        public UserTypeManagerBussiness(string appId, string userId = "0")
        {
            _usertypeManagerProvider = new UserTypeManagerProvider(appId, userId);
        }

        public DataSet GetList()
        {
            var res = _usertypeManagerProvider.GetList();
            return res;
        }

        public bool InsertUserType(DataSet ds)
        {
            return _usertypeManagerProvider.InsertUserType(ds);
        }

        public bool DeleteUserType(DataSet ds)
        {
            return _usertypeManagerProvider.DeleteUserType(ds);
        }

        public bool UpdateUserType(DataSet ds)
        {
            return _usertypeManagerProvider.UpdateUserType(ds);
        }
    }
}
