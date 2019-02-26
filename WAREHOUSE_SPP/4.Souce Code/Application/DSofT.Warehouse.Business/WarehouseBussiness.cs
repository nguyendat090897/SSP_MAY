
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IWarehouseBussiness
    {
        DataTable GetAllWarehouse(DataTable dtInput);
        bool InsertOrUpdateWarehouse(DataTable dtInput);
        bool DeleteWarehouse(DataTable dtInput);
        DataTable GetListAllWarehouse(DataTable dtInput);
    }
    public class WarehouseBussiness : IWarehouseBussiness
    {
        IWarehouseProvider _WarehouseProvider;
        /// <summary>
        /// Hàm khởi tạo bắt buộc
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        public WarehouseBussiness(string appId, string userId = "0")
        {
            _WarehouseProvider = new WarehouseProvider(appId, userId);
        }
        /// <summary>
        /// GetAllWarehouse
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllWarehouse(DataTable dtInput)
        {
            return _WarehouseProvider.GetAllWarehouse(dtInput);
        }

        /// <summary>
        /// GetListAllWarehouse
        /// </summary>
        /// <param name="dtInput"></param>
        /// <returns></returns>
        public DataTable GetListAllWarehouse(DataTable dtInput)
        {
            return _WarehouseProvider.GetListAllWarehouse(dtInput);
        }

        public bool InsertOrUpdateWarehouse( DataTable dtInput)
        {
            return _WarehouseProvider.InsertOrUpdateWarehouse(dtInput);
        }
        /// <summary>
        /// DeleteWarehouse
        /// </summary>
        /// <param name="dtInput"></param>
        /// <returns></returns>
        public bool DeleteWarehouse(DataTable dtInput)
        {
            return _WarehouseProvider.DeleteWarehouse(dtInput);
        }
    }
}
