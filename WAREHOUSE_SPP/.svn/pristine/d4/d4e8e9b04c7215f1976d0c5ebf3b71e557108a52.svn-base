using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DSofT.Warehouse.Domain.Model;
using DSofT.Warehouse.Provider;


namespace DSofT.Warehouse.Business
{
    public interface IUserTypeBussiness
    {
        DataSet Load_Data();
        DataSet Load_Menu_Condition(int UserTypeID);
        bool Update_UserRole(int UserTypeID, DataSet ListMenu);
    }
    public class UserType_Bussiness : IUserTypeBussiness
    {
        IUserTypeProvider _UserType_Provider;
        public UserType_Bussiness(string appID,string userID = "0")
        {
            _UserType_Provider = new UserRoleProvider(appID,userID);
        }
        public DataSet Load_Data()
        {
            var result = _UserType_Provider.Load_Data();
            return result;
        }
        public DataSet Load_Menu_Condition(int UserTypeID)
        {
            var result = _UserType_Provider.Load_Menu_Condition(UserTypeID);
            return result;
        }
        public bool Update_UserRole(int UserTypeID, DataSet ListMenu)
        {
            return _UserType_Provider.Update_UserRole(UserTypeID, ListMenu);
        }
    }
}
