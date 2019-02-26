using DVS.CS.Domain;
using DVS.Framework.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVS.CS.ApiClient
{
    public interface ISignalClient
    {
        Task<ApiResponse<bool>> InsertCallContact(string FromPhone, string ToPhone, string CallId, decimal CallType, DateTime StartTime, DateTime EndTime);

        Task<ApiResponse<SignalRCallContactInfo>> GetCallContactByPhone(string CallContactPhone);
    }

    public class SignalClient : BaseClient, ISignalClient
    {
        private string _postController = string.Concat(AppHostClient, UrlCommon.C_SignalR);

        public async Task<ApiResponse<bool>> InsertCallContact(string FromPhone, string ToPhone, string CallId, decimal CallType, DateTime StartTime, DateTime EndTime)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SignalR_InsertCallContact);
            dicParams = new Dictionary<string, object>();
            var Obj = new SignalRCallContactReqInfo
            {
                FromPhone = FromPhone,
                ToPhone = ToPhone,
                CallId = CallId,
                CallType = CallType,
                StartTime = StartTime,
                EndTime = EndTime
            };
            return await ExcuteRequestAsync<bool, SignalRCallContactReqInfo>(urlSend, Obj);
        }

        public async Task<ApiResponse<SignalRCallContactInfo>> GetCallContactByPhone(string CallContactPhone)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_SignalR_GetCallContactByPhone);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<SignalRCallContactInfo, string>(urlSend, CallContactPhone);
        }
    }
}