using Lingo.CS.Domain.ACL;
using Lingo.CS.Domain.Auth;
using Lingo.Framework.Client.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingo.CS.ApiClient
{
    public interface IAuthClient
    {
        Task<ApiResponse<UserLoginSession>> GetUserSession(string sessionId);

        Task<ApiResponse<string>> CheckAdminLogin(UserLogin userInfo);

        Task<ApiResponse<bool>> ClearLoginSession(string sessionId);

        Task<ApiResponse<int>> AdminResetPassword(ResetPass resetPass);

        Task<ApiResponse<List<ACLActionModel>>> GetListActionAllows(decimal userId);
    }

    public class AuthClient : BaseClient, IAuthClient
    {
        private string controller = string.Concat(AppHostClient, UrlCommon.C_Auth);

        public AuthClient()
            : base()
        {
        }

        public async Task<ApiResponse<UserLoginSession>> GetUserSession(string sessionId)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Auth_GetUserSession);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<UserLoginSession, string>(urlSend, sessionId);
        }

        public async Task<ApiResponse<string>> CheckAdminLogin(UserLogin userInfo)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Auth_CheckAdminLogin);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<string, UserLogin>(urlSend, userInfo);
        }

        public async Task<ApiResponse<bool>> ClearLoginSession(string sessionId)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Auth_ClearLoginSession);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, string>(urlSend, sessionId);
        }

        public async Task<ApiResponse<int>> AdminResetPassword(ResetPass resetPass)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Auth_AdminResetPassword);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<int, ResetPass>(urlSend, resetPass);
        }

        public async Task<ApiResponse<List<ACLActionModel>>> GetListActionAllows(decimal userId)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Auth_GetListActionAllows);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<ACLActionModel>, decimal>(urlSend, userId);
        }
    }
}