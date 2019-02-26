using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVS.Framework.Client.Helpers;
using DVS.CS.Domain;


namespace DVS.CS.ApiClient.Clients
{
    public interface ITicketStatusClient
    {
        Task<ApiResponse<List<ListTicketStatus>>> GetListTicketStatus(ValueFilterTicketStatus valueFilter);
        Task<ApiResponse<int>> InsertTicketStatus(CreateTicketStatusReqModel reqModel);
        Task<ApiResponse<int>> UpdateTicketStatus(UpdateTicketStatusReqModel reqModel);
        Task<ApiResponse<int>> DeleteTicketStatus(decimal ticketStatusId);
        Task<ApiResponse<GetTicketStatusByIdReqModel>> GetTicketStatusById(decimal ticketStatusId);
    }
    public class TicketStatusClient : BaseClient, ITicketStatusClient
    {
        private string _postController = string.Concat(AppHostClient, UrlCommon.C_TicketStatus);

        public TicketStatusClient()
            : base()
        {
        }
        public async Task<ApiResponse<List<ListTicketStatus>>> GetListTicketStatus(ValueFilterTicketStatus valueFilter)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_TicketStatus_GetListTicketStatus);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<ListTicketStatus>, ValueFilterTicketStatus>(urlSend, valueFilter);
        }
        public async Task<ApiResponse<int>> InsertTicketStatus(CreateTicketStatusReqModel reqModel)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_TicketStatus_InsertTicketStatus);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<int, CreateTicketStatusReqModel>(urlSend, reqModel);
        }
        public async Task<ApiResponse<int>> UpdateTicketStatus(UpdateTicketStatusReqModel reqModel)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_TicketStatus_UpdateTicketStatus);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<int, UpdateTicketStatusReqModel>(urlSend, reqModel);
        }

        public async Task<ApiResponse<int>> DeleteTicketStatus(decimal ticketStatusId)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_TicketStatus_DeleteTicketStatus);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<int, decimal>(urlSend, ticketStatusId);
        }
        public async Task<ApiResponse<GetTicketStatusByIdReqModel>> GetTicketStatusById(decimal ticketStatusId)
        {
            var urlSend = _postController.GetAction(UrlCommon.C_TicketStatus_GetTicketStatusById);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<GetTicketStatusByIdReqModel, decimal>(urlSend, ticketStatusId);
        }

    }
}
