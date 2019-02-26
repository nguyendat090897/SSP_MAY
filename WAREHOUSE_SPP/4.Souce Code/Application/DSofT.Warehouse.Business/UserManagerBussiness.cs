using DSofT.Warehouse.Domain.Model;
using DSofT.Warehouse.Provider;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IUserManagerBussiness
    {
        //UserModel CheckLogin(LoginModel model);
        DataSet CheckLogin(LoginModel model);
        DataSet GetListUser();
        DataSet GetListArea();

        DataSet GetListUserType();
        bool InsertUser(UserModel user);
        bool DeleteUser(UserModel user);
        bool UpdateUser(UserModel user);
        bool CheckUserName(UserModel user);
        //string Get_Password(UserModel user);

    }
    public class UserManagerBussiness : IUserManagerBussiness
    {
        IUserManagerProvider _userManagerProvider;
        /// <summary>
        /// Hàm khởi tạo bắt buộc
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        public UserManagerBussiness(string appId="0", string userId = "0")
        {
            _userManagerProvider = new UserManagerProvider(appId, userId);
        }
        public DataSet CheckLogin(LoginModel model)
        {
            var res = _userManagerProvider.CheckLogin(model);
            return res;
        }
        public DataSet GetListUser()
        {
            DataSet res = _userManagerProvider.GetListUser();
            return res;
        }
        public DataSet GetListArea()
        {
            DataSet res = _userManagerProvider.GetListArea();
            return res;
        }
        public DataSet GetListUserType()
        {
            DataSet res = _userManagerProvider.GetListUserType();
            return res;
        }
        public bool InsertUser(UserModel user)
        {
            return _userManagerProvider.InsertUser(user);
        }
        public bool DeleteUser(UserModel user)
        {
            return _userManagerProvider.DeleteUser(user);
        }
        public bool UpdateUser(UserModel user)
        {
            return _userManagerProvider.UpdateUser(user);
        }
        public bool CheckUserName(UserModel user)
        {
            return _userManagerProvider.CheckUserName(user);
        }
        //public string Get_Password(UserModel user)
        //{
        //    return _userManagerProvider.Get_Password(user);
        //}
    }
   
}
