using DVS.CS.Domain;
using DVS.CS.Domain.ListOrderAssign;
using DVS.Framework.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVS.CS.ApiClient
{
    public interface IOrderAssignClient
    {
        Task<ApiResponse<List<ListOrderAssignModel>>> GetAllOrderAssignInfo(OrderAssignModel aclVM);
        Task<ApiResponse<int>> InsertAssignOrder(AssignOrderDBModel model);
        Task<ApiResponse<bool>> UpdateAssignOrder(AssignOrderDBModel model);
        Task<ApiResponse<List<UserCSModel>>> GetListUserCS(int UserId);
    }

    public class OrderAssignClient : BaseClient, IOrderAssignClient
    {
        private string controller = string.Concat(AppHostClient, UrlCommon.C_OrderAssign);

        public OrderAssignClient()
            : base()
        {
        }

        public async Task<ApiResponse<List<ListOrderAssignModel>>> GetAllOrderAssignInfo(OrderAssignModel orderAssignVM)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_OrderAssign_GetListOrderAssign);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<List<ListOrderAssignModel>, OrderAssignModel>(urlSend, orderAssignVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<List<UserCSModel>>> GetListUserCS(int UserId)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_OrderAssign_GetListUserCS);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<List<UserCSModel>, int>(urlSend, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<int>> InsertAssignOrder(AssignOrderDBModel model)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_OrderAssign_InsertAssignOrder);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<int, AssignOrderDBModel>(urlSend, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<bool>> UpdateAssignOrder(AssignOrderDBModel model)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_OrderAssign_UpdateAssignOrder);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<bool, AssignOrderDBModel>(urlSend, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}