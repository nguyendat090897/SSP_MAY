using DVS.CS.Domain;
using DVS.Framework.Client.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVS.CS.ApiClient
{
    public interface ICallContactClient
    {
        Task<ApiResponse<List<CallContactInfo>>> ListCallContact(string KEY, decimal PAGESIZE, decimal PAGEINDEX);

        Task<ApiResponse<decimal>> GetCustomerIdByPhoneNumber(string PhoneNumber);

        Task<ApiResponse<int>> UpdateCustIdByPhoneNumber(UpdateCustomerIdReq updateCustomerIdReq);
    }

    public class CallContactClient : BaseClient, ICallContactClient
    {
        private string _postController = string.Concat(AppHostClient, UrlCommon.C_CallContact);

        public CallContactClient()
            : base()
        {
        }

        public async Task<ApiResponse<List<CallContactInfo>>> ListCallContact(string KEY, decimal PAGESIZE, decimal PAGEINDEX)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_CallContact_ListCallContact);
            dicParams = new Dictionary<string, object>();
            var Obj = new CallContactReq
            {
                KEY = KEY,
                PAGESIZE = PAGESIZE,
                PAGEINDEX = PAGEINDEX
            };
            return await ExcuteRequestAsync<List<CallContactInfo>, CallContactReq>(urlSend, Obj);
        }

        public async Task<ApiResponse<decimal>> GetCustomerIdByPhoneNumber(string PhoneNumber)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_CallContact_GetCustomerIdByPhoneNumber);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<decimal, string>(urlSend, PhoneNumber);
        }

        public async Task<ApiResponse<int>> UpdateCustIdByPhoneNumber(UpdateCustomerIdReq updateCustomerIdReq)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_CallContact_UpdateCustIdByPhoneNumber);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<int, UpdateCustomerIdReq>(urlSend, updateCustomerIdReq);
        }
    }
}