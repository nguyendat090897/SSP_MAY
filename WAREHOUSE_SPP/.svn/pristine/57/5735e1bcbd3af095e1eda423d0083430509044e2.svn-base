using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IReportBussiness
    {
        DataTable ReportMaterialWeekGetBySchoolId(DataTable dtInput);
    }
    public class ReportBussiness : IReportBussiness
    {
        IReportProvider _reportProvider;
        /// <summary>
        /// Hàm khởi tạo bắt buộc
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        public ReportBussiness(string appId, string userId = "0")
        {
            _reportProvider = new ReportProvider(appId, userId);
        }
        /// <summary>
        /// ReportMaterialWeekGetBySchoolId
        /// </summary>
        /// <param name="dtInput"></param>
        /// <returns></returns>
        public DataTable ReportMaterialWeekGetBySchoolId(DataTable dtInput)
        {
            return _reportProvider.ReportMaterialWeekGetBySchoolId(dtInput);
        }
    }
}
