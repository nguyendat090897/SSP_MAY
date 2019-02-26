using System.Collections.Generic;
using System.Threading.Tasks;
using DVS.CS.Domain.Order;
using DVS.Framework.Client.Helpers;

namespace DVS.CS.ApiClient.Clients
{
    public interface IOrderGroupClient
    {
        #region Assign Key
        Task<ApiResponse<List<AssignKeyModel>>> GetAllAssignKey();
        Task<ApiResponse<List<AssignKeyModel>>> GetAllAssignKeybyOrderGroup(OrderGroupKeyModel model);
        Task<ApiResponse<List<OrderGroupKeyModel>>> GetListOrderGrRuleById(OrderGroupKeyModel model);
        Task<ApiResponse<bool>> AddKeyToOrderGroup(OrderGroupKeyModel model);
        Task<ApiResponse<bool>> UpdateKeyToOrderGroup(OrderGroupKeyModel model);
        Task<ApiResponse<bool>> RemoveKeyFromOrderGroup(OrderGroupKeyModel model);
        #endregion

        #region Order Group

        Task<ApiResponse<List<OrderGroupModel>>> GetAllOrderGroup();

        Task<ApiResponse<OrderGroupModel>> GetOrderGroupById(OrderGroupModel model);

        Task<ApiResponse<bool>> SaveOrderGroup(OrderGroupModel model);

        Task<ApiResponse<bool>> SaveOrderGroupByStatus(OrderGroupModel model);

        Task<ApiResponse<bool>> RemoveOrderGroupById(OrderGroupModel model);

        #endregion 
    }

    public class OrderGroupClient : BaseClient, IOrderGroupClient
    {
        private string controller = string.Concat(AppHostClient, UrlCommon.C_OrderGroup);

        public OrderGroupClient()
            : base()
        {
        }

        #region Assign Key
        public async Task<ApiResponse<List<AssignKeyModel>>> GetAllAssignKey()
        {
            var urlSend = controller.GetAction(UrlCommon.C_OrderGroup_GetAllAssignKey);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<AssignKeyModel>>(urlSend);
        }
        public async Task<ApiResponse<List<AssignKeyModel>>> GetAllAssignKeybyOrderGroup(OrderGroupKeyModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.GetAllAssignKeybyOrderGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<AssignKeyModel>,OrderGroupKeyModel>(urlSend,model);
        }
        public async Task<ApiResponse<List<OrderGroupKeyModel>>> GetListOrderGrRuleById(OrderGroupKeyModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.GetListOrderGrRuleById);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<OrderGroupKeyModel>, OrderGroupKeyModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> AddKeyToOrderGroup(OrderGroupKeyModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.AddKeyToOrderGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, OrderGroupKeyModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> UpdateKeyToOrderGroup(OrderGroupKeyModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.UpdateKeyToOrderGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, OrderGroupKeyModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> RemoveKeyFromOrderGroup(OrderGroupKeyModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.RemoveKeyFromOrderGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, OrderGroupKeyModel>(urlSend, model);
        }
        #endregion

        #region Order Group

        public async Task<ApiResponse<List<OrderGroupModel>>> GetAllOrderGroup()
        {
            var urlSend = controller.GetAction(UrlCommon.C_OrderGroup_GetAllOrderGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<OrderGroupModel>>(urlSend);
        }

        public async Task<ApiResponse<OrderGroupModel>> GetOrderGroupById(OrderGroupModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.C_OrderGroup_GetOrderGroupById);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<OrderGroupModel, OrderGroupModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> SaveOrderGroup(OrderGroupModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.C_OrderGroup_SaveOrderGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, OrderGroupModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> SaveOrderGroupByStatus(OrderGroupModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.C_OrderGroup_SaveOrderGroupByStatus);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, OrderGroupModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> RemoveOrderGroupById(OrderGroupModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.C_OrderGroup_RemoveOrderGroupById);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, OrderGroupModel>(urlSend, model);
        }

        #endregion 
    }
}