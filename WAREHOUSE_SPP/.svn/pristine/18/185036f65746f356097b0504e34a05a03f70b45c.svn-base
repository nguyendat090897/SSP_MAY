using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace DSofT.Warehouse.Provider
{
    public interface IReportProvider
    {
        DataTable ReportMaterialWeekGetBySchoolId(DataTable dtInput);
    }
    public class ReportProvider : BaseSqlExecute, IReportProvider
    {

        public ReportProvider(string appId, string userId)
            : base(DbCommon.WarehouseDbConnstr, appId, userId)
        {
        }
        /// <summary>
        /// ReportMaterialWeekGetBySchoolId
        /// </summary>
        /// <param name="dtInput"></param>
        /// <returns></returns>
        public DataTable ReportMaterialWeekGetBySchoolId(DataTable dtInput)
        {
            var sqlParams = new object[] { DateTime.ParseExact(dtInput.Rows[0]["FromDay"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                , DateTime.ParseExact(dtInput.Rows[0]["ToDay"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
            ,dtInput.Rows[0]["SchoolsId"]};
            var result = base.ExecuteDataSet("ReportMaterialWeek_GetBySchoolId", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
       
    }
}
