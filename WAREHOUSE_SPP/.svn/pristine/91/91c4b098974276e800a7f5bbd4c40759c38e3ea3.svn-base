using DSofT.Framework.Client.Helpers;
using DSofT.Domain;
using DSofT.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSofT.ApiClient
{
    public interface IUserManagerClient
    {
        Task<ApiResponse<UserModel>> CheckLogin(LoginModel loginModel);
    }

    public class UserManagerClient : BaseClient, IUserManagerClient
    {
        private string controller = string.Concat(AppHostClient, UrlCommon.U_UserManager);

        public UserManagerClient()
            : base()
        {
        }
        public async Task<ApiResponse<UserModel>> CheckLogin(LoginModel loginModel)
        {
            var urlSend = controller.GetAction(UrlCommon.U_UserManager_CheckLogin);
            dicParams = new Dictionary<string, object>();
            return await base.ExcuteRequestAsync<UserModel, LoginModel>(urlSend, loginModel);
        }
    }
}