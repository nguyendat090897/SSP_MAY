using DSofT.Framework.Client;
using DSofT.Framework.Client.Helpers;

namespace DSofT.ApiClient
{
    public class BaseClient : BaseApiClient
    {
        private static string appIdClient = ClientHelpers.GetAppSetting("MOSMS.AppId");
        private static string appSecretClient = ClientHelpers.GetAppSetting("MOSMS.AppSecret");
        private static string appPublicKeyClient = ClientHelpers.GetAppSetting("MOSMS.AppPublicKey");
        protected static string AppHostClient = ClientHelpers.GetAppSetting("MOSMS.AppHostUri");

        public BaseClient()
            : base(appIdClient, appSecretClient, appPublicKeyClient, AppHostClient)
        {
        }
    }
}