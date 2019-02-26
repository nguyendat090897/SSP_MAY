using DSofT.Warehouse.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSofT.Warehouse.Business
{
    public interface IUserBussiness
    {
        bool User_ChangePassword(DataSet dsInput);
    }
    public class UserBussiness : IUserBussiness
    {
        IUserProvider _UserProvider;
        /// <summary>
        /// Hàm khởi tạo bắt buộc
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        public UserBussiness(string appId, string userId = "0")
        {
            _UserProvider = new UserProvider(appId, userId);
        }
        public bool User_ChangePassword(DataSet dsInput)
        {
            var res = _UserProvider.User_ChangePassword(dsInput);
            return res;
        }
    }
}
