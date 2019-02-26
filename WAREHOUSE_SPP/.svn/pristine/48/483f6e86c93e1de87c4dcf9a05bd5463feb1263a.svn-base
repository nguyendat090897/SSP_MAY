using DVS.CS.Domain;
using DVS.Framework.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVS.CS.ApiClient
{
    public interface IAssignKeyClient
    {
        Task<ApiResponse<List<AssignKeyModel>>> GetAllAssignKeyPaging(AssignKeyFilterModel model);
        Task<ApiResponse<int>> InsertAssignKey(AssignKeyDBModel model);
        Task<ApiResponse<bool>> UpdateAssignKey(AssignKeyDBModel model);
        Task<ApiResponse<bool>> DeleteAssignKey(int AssignKeyId);
        Task<ApiResponse<AssignKeyDBModel>> GetAssignKeyById(int AssignKeyId);
    }

    public class AssignKeyClient : BaseClient, IAssignKeyClient
    {
        private string controller = string.Concat(AppHostClient, UrlCommon.C_AssignKey);

        public AssignKeyClient()
            : base()
        {
        }

        public async Task<ApiResponse<bool>> DeleteAssignKey(int AssignKeyId)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_AssignKey_DeleteAssignKey);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<bool, int>(urlSend, AssignKeyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //AssignKeyDBModel GetAssignKeyById(int AssignKeyId);

        public async Task<ApiResponse<List<AssignKeyModel>>> GetAllAssignKeyPaging(AssignKeyFilterModel model)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_AssignKey_GetAllAssignKeyPaging);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<List<AssignKeyModel>, AssignKeyFilterModel>(urlSend, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<AssignKeyDBModel>> GetAssignKeyById(int AssignKeyId)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_AssignKey_GetAssignKeyById);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<AssignKeyDBModel, int>(urlSend, AssignKeyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<int>> InsertAssignKey(AssignKeyDBModel model)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_AssignKey_InsertAssignKey);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<int, AssignKeyDBModel>(urlSend, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<bool>> UpdateAssignKey(AssignKeyDBModel model)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_AssignKey_UpdateAssignKey);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<bool, AssignKeyDBModel>(urlSend, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}