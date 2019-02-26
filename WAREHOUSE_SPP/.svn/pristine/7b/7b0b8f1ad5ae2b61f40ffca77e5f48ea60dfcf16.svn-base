using DVS.CS.Domain;
using DVS.Framework.Client.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVS.CS.ApiClient
{
    public interface ICancelReasonClient
    {
        #region Cancel Reason

        Task<ApiResponse<List<CancelReasonModel>>> GetAllCancelReason();

        Task<ApiResponse<CancelReasonModel>> GetCancelReasonbyId(CancelReasonModel model);

        Task<ApiResponse<bool>> SaveCancelReason(CancelReasonModel model);

        Task<ApiResponse<bool>> SaveCancelReasonbyStatus(CancelReasonModel model);

        Task<ApiResponse<bool>> RemoveCancelReasonbyId(CancelReasonModel model);

        #endregion Cancel Reason
    }

    public class CancelReasonClient : BaseClient, ICancelReasonClient
    {
        private string controller = string.Concat(AppHostClient, UrlCommon.C_CancelReason);

        public CancelReasonClient()
            : base()
        {
        }

        #region Cancel Reason

        public async Task<ApiResponse<List<CancelReasonModel>>> GetAllCancelReason()
        {
            var urlSend = controller.GetAction(UrlCommon.C_CancelReason_GetAllCancelReason);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<CancelReasonModel>>(urlSend);
        }

        public async Task<ApiResponse<CancelReasonModel>> GetCancelReasonbyId(CancelReasonModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.GetCancelReasonbyId);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<CancelReasonModel, CancelReasonModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> SaveCancelReason(CancelReasonModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.SaveCancelReason);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, CancelReasonModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> SaveCancelReasonbyStatus(CancelReasonModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.SaveCancelReasonbyStatus);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, CancelReasonModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> RemoveCancelReasonbyId(CancelReasonModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.RemoveCancelReasonbyId);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, CancelReasonModel>(urlSend, model);
        }

        #endregion Cancel Reason
    }
}