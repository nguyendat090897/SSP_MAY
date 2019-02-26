using DSofT.Warehouse.Domain.Model.System;
using DSofT.Warehouse.Provider;

namespace DSofT.Warehouse.Business
{
    public interface ISysLogsBussiness
    {
        bool InsertSysLog(S_LogModel model);
    }
    public class SysLogsBussiness : ISysLogsBussiness
    {
        ISysLogProvider _SysLogProvider;
        /// <summary>
        /// Hàm khởi tạo bắt buộc
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        public SysLogsBussiness(string appId, string userId = "0")
        {
            _SysLogProvider = new SysLogProvider(appId, userId);
        }
        /// <summary>
        /// InsertSysLog
        /// </summary>
        /// <returns></returns>
        public bool InsertSysLog(S_LogModel model)
        {
            return _SysLogProvider.InsertSysLog(model);
        }
    }
}
