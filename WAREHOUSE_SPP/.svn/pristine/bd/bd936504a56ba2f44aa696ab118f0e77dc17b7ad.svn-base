using DVS.CS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVS.Framework.Client.Helpers;

namespace DVS.CS.ApiClient
{
    public interface ICSInfoClient
    {
        Task<ApiResponse<List<CustomerServiceInfo>>> GetListCSInfo(SearchCSReq _SearchCSReq);
        Task<ApiResponse<List<CustomerServiceGroup>>> GetListCSGroup();
        Task<ApiResponse<List<CustomerServiceShift>>> GetListCSShift();
        Task<ApiResponse<int>> UpdateCSInfo(UpdateCSReq _UpdateCSReq);
        Task<ApiResponse<CustomerServiceInfo>> GetCustomerServiceInfo(int USER_ID);
    }

    public class CSInfoClient : BaseClient, ICSInfoClient
    {
        private string controller = string.Concat(AppHostClient, UrlCommon.C_CustomerService);
        public CSInfoClient() : base() { }

        public async Task<ApiResponse<List<CustomerServiceInfo>>> GetListCSInfo(SearchCSReq _SearchCSReq)
        {
            var urlSend = controller.GetAction(UrlCommon.C_CustomerService_GetListCSInfo);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<CustomerServiceInfo>, SearchCSReq>(urlSend, _SearchCSReq);
        }

        public async Task<ApiResponse<List<CustomerServiceGroup>>> GetListCSGroup()
        {
            var urlSend = controller.GetAction(UrlCommon.C_CustomerService_GetListCSGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<CustomerServiceGroup>>(urlSend);
        }

        public async Task<ApiResponse<List<CustomerServiceShift>>> GetListCSShift()
        {
            var urlSend = controller.GetAction(UrlCommon.C_CustomerService_GetListCSShift);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<CustomerServiceShift>>(urlSend);
        }

        public async Task<ApiResponse<int>> UpdateCSInfo(UpdateCSReq _UpdateCSReq)
        {
            var urlSend = controller.GetAction(UrlCommon.C_CustomerService_UpdateCSInfo);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<int, UpdateCSReq>(urlSend, _UpdateCSReq);
        }


        public async Task<ApiResponse<CustomerServiceInfo>> GetCustomerServiceInfo(int USER_ID)
        {
            var urlSend = controller.GetAction(UrlCommon.C_CustomerService_GetCustomerServiceInfo);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<CustomerServiceInfo, int>(urlSend, USER_ID);
        }
    }
}
