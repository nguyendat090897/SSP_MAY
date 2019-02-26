using DSofT.Warehouse.Domain.Model.System;
using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;

namespace DSofT.Warehouse.Provider
{
    public interface ISysLogProvider
    {
        bool InsertSysLog(S_LogModel model);
    }
    public class SysLogProvider : BaseSqlExecute, ISysLogProvider
    {

        public SysLogProvider(string appId, string userId)
            : base(DbCommon.WarehouseDbConnstr, appId, userId)
        {
        }
        /// <summary>
        /// InsertSysLog
        /// </summary>
        /// <returns></returns>
        public bool InsertSysLog(S_LogModel model)
        {
            var sqlParams = new object[] {
                 model.UserName
                ,model.LogDate
                ,model.LogAction
                ,model.LogLevel
                ,model.Logger
                ,model.FormName
                ,model.LogContent
                ,model.Exception};
            var result = base.ExecScalar("S_Log_Insert", sqlParams);
            if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        
    }
}
