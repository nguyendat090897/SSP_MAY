using DVS.CS.Domain;
using DVS.Framework.Client.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVS.CS.ApiClient
{
    public interface ICallRecordClient
    {
        Task<ApiResponse<List<CallRecordElement>>> GetCallRecordByCustomerId(decimal CUSTOMER_ID, decimal PAGESIZE, decimal PAGEINDEX);
        Task<ApiResponse<int>> InsertCallOut(CallOutInsertReq _CallOutInsertReq);
    }

    public class CallRecordClient : BaseClient, ICallRecordClient
    {
        private string _postController = string.Concat(AppHostClient, UrlCommon.C_CallRecord);

        public CallRecordClient()
            : base()
        {
        }

        public async Task<ApiResponse<List<CallRecordElement>>> GetCallRecordByCustomerId(decimal CUSTOMER_ID, decimal PAGESIZE, decimal PAGEINDEX)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_CallRecord_GetCallRecordByCustomerId);
            dicParams = new Dictionary<string, object>();
            var Obj = new ListCallRecordByCustomerReq
            {
                CUSTOMER_ID = CUSTOMER_ID,
                PAGESIZE = PAGESIZE,
                PAGEINDEX = PAGEINDEX
            };
            return await ExcuteRequestAsync<List<CallRecordElement>, ListCallRecordByCustomerReq>(urlSend, Obj);
        }

        public async Task<ApiResponse<int>> InsertCallOut(CallOutInsertReq _CallOutInsertReq)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_CallRecord_InsertCallOut);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<int, CallOutInsertReq>(urlSend, _CallOutInsertReq);
        }
    }
}