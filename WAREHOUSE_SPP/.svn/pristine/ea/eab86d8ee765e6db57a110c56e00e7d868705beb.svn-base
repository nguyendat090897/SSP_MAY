using DVS.CS.Domain;
using DVS.Framework.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVS.CS.ApiClient
{
    public interface IShiftClient
    {
        Task<ApiResponse<List<ShiftModel>>> GetAllShiftPaging(ShiftFilterModel model);
        Task<ApiResponse<int>> InsertShift(ShiftDBModel model);
        Task<ApiResponse<bool>> UpdateShift(ShiftDBModel model);
        Task<ApiResponse<bool>> DeleteShift(int ShiftId);
        Task<ApiResponse<int>> CheckShiftTime(ShiftDBModel model);
        Task<ApiResponse<ShiftDBModel>> GetShiftById(int ShiftId);
    }

    public class ShiftClient : BaseClient, IShiftClient
    {
        private string controller = string.Concat(AppHostClient, UrlCommon.C_Shift);

        public ShiftClient()
            : base()
        {
        }

        public async Task<ApiResponse<int>> CheckShiftTime(ShiftDBModel model)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_Shift_CheckShiftTime);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<int, ShiftDBModel>(urlSend, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<bool>> DeleteShift(int ShiftId)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_Shift_DeleteShift);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<bool, int>(urlSend, ShiftId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //ShiftModel GetShiftById(int ShiftId);

        public async Task<ApiResponse<List<ShiftModel>>> GetAllShiftPaging(ShiftFilterModel model)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_Shift_GetAllShiftPaging);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<List<ShiftModel>, ShiftFilterModel>(urlSend, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<ShiftDBModel>> GetShiftById(int ShiftId)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_Shift_GetShiftById);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<ShiftDBModel, int>(urlSend, ShiftId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<int>> InsertShift(ShiftDBModel model)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_Shift_InsertShift);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<int, ShiftDBModel>(urlSend, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<bool>> UpdateShift(ShiftDBModel model)
        {
            try
            {
                var urlSend = controller.GetAction(UrlCommon.C_Shift_UpdateShift);
                dicParams = new Dictionary<string, object>();
                return await base.ExcuteRequestAsync<bool, ShiftDBModel>(urlSend, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}