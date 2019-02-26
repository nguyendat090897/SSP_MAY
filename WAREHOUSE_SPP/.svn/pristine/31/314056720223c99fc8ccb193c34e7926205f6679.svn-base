using DSofT.Framework.Helpers;
using DSofT.Framework.Internal.ApiListener.Controllers;
using DSofT.Warehouse.Business;
using DSofT.Warehouse.Domain.Model;
using System.Web.Http;

namespace DSofT.Warehouse.ApiListener.Controllers
{
    [RoutePrefix(Domain.UrlCommon.U_UserManager)]
    public class UserAPIController : BaseApiController
    {
        private readonly IUserManagerBussiness _userManagerBussiness;

        //private readonly ISystemBussiness _systemBussiness ;
        public UserAPIController()
        {
            _userManagerBussiness = new UserManagerBussiness(AppId);
        }

        //#region Publish API

        [HttpPost, Route(Domain.UrlCommon.U_UserManager_CheckLogin)]
        public IHttpActionResult CheckLoginForAPI()
        {
            var userRequestModel = GetRequestData<LoginModel>();
            userRequestModel.PASSWORD = userRequestModel.PASSWORD.EncryptPassword();
            var result = _userManagerBussiness.CheckLogin(userRequestModel);
            return DSofTResult(result);
        }


    }
}