using DVS.CS.Domain;
using DVS.Framework.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVS.CS.ApiClient
{
    public interface IACLClient
    {
        Task<ApiResponse<List<GetActionByControllerResModel>>> GetActionByControllerId(GetActionByControllerReqModel reqModel);

        Task<ApiResponse<bool>> AddActionToRole(AddActionToRoleReqModel reqModel);

        #region Permissions

        //Task<ApiResponse<List<AdminUserTitle>>> GetListAdminUserTitle();

        Task<ApiResponse<List<GetGroupRoleByTitleResModel>>> GetGroupRoleByTitle(int titleId);

        //Task<int> CreateGroupRole(ACLGroupRoleModel model);

        Task<ApiResponse<int>> AddRemoveTitleGroupRole(AddRemoveTileGroupRoleReqModel reqModel);

        #endregion Permissions

        #region Group Role

        Task<ApiResponse<CUDReturnMessage>> CreateUpdateGroupRole(CreateUpdateGroupRoleReqModel reqModel);

        Task<ApiResponse<GroupRoleInfoResModel>> GetGroupRoleById(int groupRoleId);

        //NEW

        Task<ApiResponse<List<GetListRoleShowResModel>>> GetRoleByGroupRole(GetListRoleShowReqModel reqModel);

        //Task<ApiResponse<List<ListGroupRole>>> GetListGroupRoleHide(GetListRoleShowReqModel reqModel);

        //Task<ApiResponse<int>> RemoveGroupRole(GroupRoleModelInfo model);

        Task<ApiResponse<int>> AddRemoveRoleGroup(AddRemoveRoleFromGroupReqModel reqModel);

        #endregion Group Role

        #region Role

        /// <summary>
        ///
        /// </summary>
        /// <param name="controllerId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="numberRow"></param>
        /// <returns></returns>
        Task<ApiResponse<List<GetRoleByControllerResModel>>> GetRoleByController(int controllerId, int pageNumber, int numberRow);

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ApiResponse<GetRoleByIdResModel>> GetRoleById(int roleId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResponse<int>> InsertRole(CreateUpdateRoleReqModel model);

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResponse<bool>> UpdateRole(CreateUpdateRoleReqModel model);

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ApiResponse<bool>> DeleteRole(int roleId);

        #endregion Role

        #region Controllers

        Task<ApiResponse<bool>> CreateUpdateController(CreateUpdateControllerReqModel reqModel);

        Task<ApiResponse<List<ControllerList>>> GetListController(GetControllerReqModel reqModel);

        Task<ApiResponse<GetDetailControllerResModel>> GetDetailController(decimal controllerId);

        Task<ApiResponse<CUDReturnMessage>> DeleteController(decimal controllerId);

        #endregion Controllers

        #region Actions

        Task<ApiResponse<List<GetActionResModel>>> GetActionList(GetActionReqModel reqModel);

        Task<ApiResponse<GetActionByIdResModel>> GetActionById(decimal actionId);

        Task<ApiResponse<CUDReturnMessage>> CreateUpdateAction(CreateUpdateActionReqModel reqModel);

        //Task<ApiResponse<CUDReturnMessage>> UpdateActionbyStatus(UpdateActionStatusReqModel model);

        Task<ApiResponse<CUDReturnMessage>> DeleteAction(decimal actionId);

        #endregion Actions
    }

    public class ACLClient : BaseClient, IACLClient
    {
        private string controller = string.Concat(AppHostClient, UrlCommon.C_ACL);

        public ACLClient()
            : base()
        {
        }

        public async Task<ApiResponse<List<GetActionByControllerResModel>>> GetActionByControllerId(GetActionByControllerReqModel reqModel)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_ACL_GetActionByControllerId);
                dicParams = new Dictionary<string, object>();
                //dicParams.Add("ControllerId", ControllerId);
                //dicParams.Add("flgShow", flgShow);
                return await base.ExcuteRequestAsync<List<GetActionByControllerResModel>, GetActionByControllerReqModel>(urlSend, reqModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<bool>> AddActionToRole(AddActionToRoleReqModel reqModel)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_AddActionToRole);
            dicParams = new Dictionary<string, object>();
            return await base.ExcuteRequestAsync<bool, AddActionToRoleReqModel>(urlSend, reqModel);
        }

        #region Permissions

        //public async Task<ApiResponse<List<AdminUserTitle>>> GetListAdminUserTitle()
        //{
        //    var urlSend = controller.GetAction(UrlCommon.C_ACL_GetListAdminUserTitle);
        //    dicParams = new Dictionary<string, object>();
        //    return await ExcuteRequestAsync<List<AdminUserTitle>>(urlSend);
        //}

        public async Task<ApiResponse<List<GetGroupRoleByTitleResModel>>> GetGroupRoleByTitle(int titleId)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_GroupRole_GetGroupRoleByTitle);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<GetGroupRoleByTitleResModel>, int>(urlSend, titleId);
        }

        //public async Task<int> CreateGroupRole(ACLGroupRoleModel model)
        //{
        //    var urlSend = controller.GetAction(UrlCommon.C_ACL_GroupRole_CreateGroupRole);
        //    dicParams = new Dictionary<string, object>();
        //    return await ExcuteRequestAsync<int, ACLGroupRoleModel>(urlSend, model);
        //}

        public async Task<ApiResponse<int>> AddRemoveTitleGroupRole(AddRemoveTileGroupRoleReqModel reqModel)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_GroupRole_AddRemoveTitleGroupRole);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<int, AddRemoveTileGroupRoleReqModel>(urlSend, reqModel);
        }

        #endregion Permissions

        #region Group Role

        public async Task<ApiResponse<CUDReturnMessage>> CreateUpdateGroupRole(CreateUpdateGroupRoleReqModel reqModel)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_GroupRole_CreateUpdateGroupRole);
            dicParams = new Dictionary<string, object>();
            return await base.ExcuteRequestAsync<CUDReturnMessage, CreateUpdateGroupRoleReqModel>(urlSend, reqModel);
        }

        public async Task<ApiResponse<GroupRoleInfoResModel>> GetGroupRoleById(int groupRoleId)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_GroupRole_GetGroupRoleById);
            dicParams = new Dictionary<string, object>();
            return await base.ExcuteRequestAsync<GroupRoleInfoResModel, int>(urlSend, groupRoleId);
        }

        //NEW

        public async Task<ApiResponse<List<GetListRoleShowResModel>>> GetRoleByGroupRole(GetListRoleShowReqModel reqModel)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_GetRoleByGroupRole);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<GetListRoleShowResModel>, GetListRoleShowReqModel>(urlSend, reqModel);
        }

        //public async Task<ApiResponse<List<ListGroupRole>>> GetListGroupRoleHide(GetListRoleShowReqModel reqModel)
        //{
        //    var urlSend = controller.GetAction(UrlCommon.C_ACL_ListGroupRoleHide);
        //    dicParams = new Dictionary<string, object>();
        //    return await ExcuteRequestAsync<List<ListGroupRole>, GetListRoleShowReqModel>(urlSend, reqModel);
        //}

        //public async Task<ApiResponse<int>> RemoveGroupRole(GroupRoleModelInfo model)
        //{
        //    var urlSend = controller.GetAction(UrlCommon.C_ACL_RemoveGroupRole);
        //    dicParams = new Dictionary<string, object>();
        //    return await ExcuteRequestAsync<int, GroupRoleModelInfo>(urlSend, model);
        //}

        public async Task<ApiResponse<int>> AddRemoveRoleGroup(AddRemoveRoleFromGroupReqModel reqModel)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_AddRemoveRoleGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<int, AddRemoveRoleFromGroupReqModel>(urlSend, reqModel);
        }

        #endregion Group Role

        #region Role

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> DeleteRole(int roleId)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_DeleteRole);
            dicParams = new Dictionary<string, object>();
            //dicParams.Add("roleId", roleId);
            return await base.ExcuteRequestAsync<bool, int>(urlSend, roleId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controllerId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="numberRow"></param>
        /// <returns></returns>
        public async Task<ApiResponse<List<GetRoleByControllerResModel>>> GetRoleByController(int controllerId, int pageNumber, int numberRow)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_GetRoleByController);
            dicParams = new Dictionary<string, object>();
            var reqModel = new GetRoleByControllerReqModel()
            {
                ControllerId = controllerId,
                NumberRow = numberRow,
                PageNumber = pageNumber
            };
            return await base.ExcuteRequestAsync<List<GetRoleByControllerResModel>, GetRoleByControllerReqModel>(urlSend, reqModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<ApiResponse<GetRoleByIdResModel>> GetRoleById(int roleId)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_GetRoleById);
            dicParams = new Dictionary<string, object>();
            //dicParams.Add("roleId", roleId);
            return await base.ExcuteRequestAsync<GetRoleByIdResModel, int>(urlSend, roleId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse<int>> InsertRole(CreateUpdateRoleReqModel reqModel)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_InsertRole);
            dicParams = new Dictionary<string, object>();
            return await base.ExcuteRequestAsync<int, CreateUpdateRoleReqModel>(urlSend, reqModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> UpdateRole(CreateUpdateRoleReqModel reqModel)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_UpdateRole);
            dicParams = new Dictionary<string, object>();
            return await base.ExcuteRequestAsync<bool, CreateUpdateRoleReqModel>(urlSend, reqModel);
        }

        #endregion Role

        #region Controllers

        public async Task<ApiResponse<List<ControllerList>>> GetListController(GetControllerReqModel reqModel)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_Controller_GetListController);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<ControllerList>, GetControllerReqModel>(urlSend, reqModel);
        }

        public async Task<ApiResponse<bool>> CreateUpdateController(CreateUpdateControllerReqModel reqModel)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_Controller_CreateUpdateController);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, CreateUpdateControllerReqModel>(urlSend, reqModel);
        }

        public async Task<ApiResponse<GetDetailControllerResModel>> GetDetailController(decimal controllerId)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_Controller_GetDetailController);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<GetDetailControllerResModel, decimal>(urlSend, controllerId);
        }

        public async Task<ApiResponse<CUDReturnMessage>> DeleteController(decimal controllerId)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_Controller_DeleteController);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<CUDReturnMessage, decimal>(urlSend, controllerId);
        }

        #endregion Controllers

        #region Actions

        public async Task<ApiResponse<List<GetActionResModel>>> GetActionList(GetActionReqModel reqModel)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_Action_ActionList);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<GetActionResModel>, GetActionReqModel>(urlSend, reqModel);
        }

        public async Task<ApiResponse<GetActionByIdResModel>> GetActionById(decimal actionId)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_Action_GetActionById);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<GetActionByIdResModel, decimal>(urlSend, actionId);
        }

        public async Task<ApiResponse<CUDReturnMessage>> CreateUpdateAction(CreateUpdateActionReqModel reqModel)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_Action_CreateUpdateAction);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<CUDReturnMessage, CreateUpdateActionReqModel>(urlSend, reqModel);
        }

        //public async Task<ApiResponse<CUDReturnMessage>> UpdateActionbyStatus(UpdateActionStatusReqModel model)
        //{
        //    var urlSend = controller.GetAction(UrlCommon.C_ACL_Action_UpdateActionbyStatus);
        //    dicParams = new Dictionary<string, object>();
        //    return await ExcuteRequestAsync<CUDReturnMessage, UpdateActionStatusReqModel>(urlSend, model);
        //}

        public async Task<ApiResponse<CUDReturnMessage>> DeleteAction(decimal actionId)
        {
            var urlSend = controller.GetAction(UrlCommon.C_ACL_Action_DeleteAction);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<CUDReturnMessage, decimal>(urlSend, actionId);
        }

        #endregion Actions
    }
}