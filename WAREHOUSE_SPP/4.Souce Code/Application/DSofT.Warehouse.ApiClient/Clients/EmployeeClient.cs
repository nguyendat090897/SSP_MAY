using System.Collections.Generic;
using System.Threading.Tasks;
using DVS.CS.Domain.Employee;
using DVS.Framework.Client.Helpers;

namespace DVS.CS.ApiClient
{
    public interface IEmployeeClient
    {

        #region Employee
        Task<ApiResponse<List<EmployeeModel>>> GetAllEmployee();
        Task<ApiResponse<List<EmployeeModel>>> GetNonEmployeebyGroup(EmployeeGroupModel model);
        Task<ApiResponse<List<EmployeeModel>>> GetEmployeebyGroup(EmployeeGroupModel model);
        Task<ApiResponse<bool>> AddEmployeeToGroup(EmployeeGroupModel model);
        Task<ApiResponse<bool>> RemoveEmployeeInGroup(EmployeeGroupModel model);
        #endregion

        #region Employee Group

        Task<ApiResponse<List<EmployeeGroupModel>>> GetAllEmployeeGroup();

        Task<ApiResponse<EmployeeGroupModel>> GetEmployeeGroupbyId(EmployeeGroupModel model);

        Task<ApiResponse<bool>> SaveEmployeeGroup(EmployeeGroupModel model);

        Task<ApiResponse<bool>> SaveEmployeeGroupbyStatus(EmployeeGroupModel model);

        Task<ApiResponse<bool>> RemoveEmployeeGroupbyId(EmployeeGroupModel model);

        #endregion 
    }

    public class EmployeeClient : BaseClient, IEmployeeClient
    {
        private string controller = string.Concat(AppHostClient, UrlCommon.C_Employee);

        public EmployeeClient(): base(){}

        #region Employee
        public async Task<ApiResponse<List<EmployeeModel>>> GetAllEmployee()
        {
            var urlSend = controller.GetAction(UrlCommon.C_Employee_GetAllEmployee);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<EmployeeModel>>(urlSend);
        }
        public async Task<ApiResponse<List<EmployeeModel>>> GetNonEmployeebyGroup(EmployeeGroupModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Employee_GetNonEmployeebyGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<EmployeeModel>, EmployeeGroupModel>(urlSend, model);
        }
        public async Task<ApiResponse<List<EmployeeModel>>> GetEmployeebyGroup(EmployeeGroupModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Employee_GetEmployeebyGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<EmployeeModel>, EmployeeGroupModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> AddEmployeeToGroup(EmployeeGroupModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Employee_AddEmployeeToGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, EmployeeGroupModel>(urlSend, model);
        }
        public async Task<ApiResponse<bool>> RemoveEmployeeInGroup(EmployeeGroupModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Employee_RemoveEmployeeInGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, EmployeeGroupModel>(urlSend, model);
        }
        #endregion

        #region Employee Group

        public async Task<ApiResponse<List<EmployeeGroupModel>>> GetAllEmployeeGroup()
        {
            var urlSend = controller.GetAction(UrlCommon.C_Employee_GetAllEmployeeGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<List<EmployeeGroupModel>>(urlSend);
        }

        public async Task<ApiResponse<EmployeeGroupModel>> GetEmployeeGroupbyId(EmployeeGroupModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Employee_GetEmployeeGroupById);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<EmployeeGroupModel, EmployeeGroupModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> SaveEmployeeGroup(EmployeeGroupModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Employee_SaveEmployeeGroup);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, EmployeeGroupModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> SaveEmployeeGroupbyStatus(EmployeeGroupModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Employee_SaveEmployeeGroupbyStatus);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, EmployeeGroupModel>(urlSend, model);
        }

        public async Task<ApiResponse<bool>> RemoveEmployeeGroupbyId(EmployeeGroupModel model)
        {
            var urlSend = controller.GetAction(UrlCommon.C_Employee_RemoveEmployeeGroupById);
            dicParams = new Dictionary<string, object>();
            return await ExcuteRequestAsync<bool, EmployeeGroupModel>(urlSend, model);
        }

        #endregion 
    }
}