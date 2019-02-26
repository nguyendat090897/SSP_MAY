using DVS.CS.Domain;
using DVS.Framework.Client.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVS.CS.ApiClient.Clients
{
    public interface ICreateOrderClient
    {
        Task<ApiResponse<List<CreateSOOrderType>>> GetListOrderType();
    }
    public class CreateOrderClient : BaseClient, ICreateOrderClient
    {
        private string _postController = string.Concat(AppHostClient, UrlCommon.C_CreateOrder);
        public CreateOrderClient()
            : base()
        {
        }
        public async Task<ApiResponse<List<CreateSOOrderType>>> GetListOrderType()
        {
            var urlSend = _postController.GetAction(UrlCommon.C_CreateOrder_GetListOrderType);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<CreateSOOrderType>>(urlSend);
        }
    }
}
