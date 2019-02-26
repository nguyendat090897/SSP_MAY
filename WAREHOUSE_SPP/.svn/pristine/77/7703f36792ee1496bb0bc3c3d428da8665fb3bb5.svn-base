using DVS.CS.Domain;
using DVS.Framework.Client.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVS.CS.ApiClient
{
    public interface ISettingClient
    {
        Task<ApiResponse<List<SettingModel>>> GetSettingList();

        Task<ApiResponse<decimal>> UpdateSettingValue(SettingUpdateModel data);
    }

    public class SettingClient : BaseClient, ISettingClient
    {
        private string controller = string.Concat(AppHostClient, UrlCommon.C_Setting);

        public SettingClient()
            : base()
        {
        }

        public async Task<ApiResponse<List<SettingModel>>> GetSettingList()
        {
            var urlSend = controller.GetAction(UrlCommon.C_Setting_GetListSetting);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<SettingModel>>(urlSend);
        }

        public async Task<ApiResponse<decimal>> UpdateSettingValue(SettingUpdateModel data)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Setting_UpdateValue);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<decimal, SettingUpdateModel>(urlSend, data);
        }
    }
}