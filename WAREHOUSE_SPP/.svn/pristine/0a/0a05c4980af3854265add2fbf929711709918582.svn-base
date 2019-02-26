using DVS.CS.Domain;
using DVS.Framework.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVS.CS.ApiClient
{
    public interface IConfigGroupUserClient
    {
        Task<ApiResponse<List<ConfigUserModel>>> GetListUser();
        Task<ApiResponse<List<ConfigGroupModel>>> GetListGroup();
        Task<ApiResponse<List<ConfigShiftModel>>> GetListShift();
        Task<ApiResponse<List<ConfigInfoModel>>> GetListUserOfShift();
        Task<ApiResponse<List<ConfigInfoModel>>> GetListUserOfGroup();
        Task<ApiResponse<ConfigGroupUserModel>> GetConfigGroupUser();
        Task<ApiResponse<bool>> UpdateConfigGroupUser(ConfigUpdateModel model);
        Task<ApiResponse<bool>> UpdateConfigShiftUser(ConfigUpdateModel model);
    }

    public class ConfigGroupUserClient : BaseClient, IConfigGroupUserClient
    {
        private string controller = string.Concat(AppHostClient, UrlCommon.C_ConfigGroupUser);

        public ConfigGroupUserClient()
            : base()
        {
        }
        
        public async Task<ApiResponse<ConfigGroupUserModel>> GetConfigGroupUser()
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_ConfigGroupUser_GetConfigGroupUser);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<ConfigGroupUserModel>(urlSend);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<List<ConfigGroupModel>>> GetListGroup()
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_ConfigGroupUser_GetListGroup);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<List<ConfigGroupModel>>(urlSend);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<List<ConfigShiftModel>>> GetListShift()
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_ConfigGroupUser_GetListShift);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<List<ConfigShiftModel>>(urlSend);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<List<ConfigUserModel>>> GetListUser()
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_Account_GetListUser);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<List<ConfigUserModel>>(urlSend);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<List<ConfigInfoModel>>> GetListUserOfGroup()
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_ConfigGroupUser_GetListUserOfGroup);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<List<ConfigInfoModel>>(urlSend);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<List<ConfigInfoModel>>> GetListUserOfShift()
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_ConfigGroupUser_GetListUserOfShift);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<List<ConfigInfoModel>>(urlSend);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<bool>> UpdateConfigGroupUser(ConfigUpdateModel model)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_ConfigGroupUser_UpdateConfigGroupUser);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<bool, ConfigUpdateModel>(urlSend, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<bool>> UpdateConfigShiftUser(ConfigUpdateModel model)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_ConfigGroupUser_UpdateConfigShiftUser);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<bool, ConfigUpdateModel>(urlSend, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}