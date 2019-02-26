using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IWarehouseProvider
    {
        DataTable GetAllWarehouse(DataTable dtInput);
        bool InsertOrUpdateWarehouse(DataTable dtInput);
        bool DeleteWarehouse(DataTable dtInput);

        DataTable GetListAllWarehouse(DataTable dtInput);
    }
    public class WarehouseProvider : BaseSqlExecute, IWarehouseProvider
    {

        public WarehouseProvider(string appId, string userId)
            : base(DbCommon.WarehouseDbConnstr, appId, userId)
        {
        }
        /// <summary>
        /// Get danh sách trường học
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllWarehouse(DataTable dtInput)
        {
            var sqlParams = new object[] { };
            var result = base.ExecuteDataSet("Warehouse_GetAll", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        /// <summary>
        /// GetListAllWarehouse
        /// </summary>
        /// <param name="dtInput"></param>
        /// <returns></returns>
        public DataTable GetListAllWarehouse(DataTable dtInput)
        {
            var sqlParams = new object[] { dtInput.Rows[0]["WarehouseCode"], dtInput.Rows[0]["WarehouseName"] };
            var result = base.ExecuteDataSet("Warehouse_ListGetAll", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        /// <summary>
        /// Insert hoặc Update loại món ăn
        /// </summary>
        /// <returns></returns>
        public bool InsertOrUpdateWarehouse(DataTable dtInput)
        {
            //thuc hien update
            if (NumberHelper.ConvertStringToLong(dtInput.Rows[0]["Id"].ToString()) > 0)
            {
                var sqlParams = new object[] { dtInput.Rows[0]["Id"]
                                             ,dtInput.Rows[0]["WarehouseCode"]
                                             , dtInput.Rows[0]["WarehouseName"]
                                             , dtInput.Rows[0]["Address"]
                                             , dtInput.Rows[0]["Phone"]
                                             , dtInput.Rows[0]["Fax"]
                                             , dtInput.Rows[0]["Email"]
                                             , dtInput.Rows[0]["Representative"]
                                             , dtInput.Rows[0]["Remark"]
                                             , CommonDataHelper.UserName
                                             };
                var result = base.ExecScalar("Warehouse_Update", sqlParams);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
            }
            else
            {
                var sqlParams = new object[] { 
                                              dtInput.Rows[0]["WarehouseCode"]
                                             , dtInput.Rows[0]["WarehouseName"]
                                             , dtInput.Rows[0]["Address"]
                                             , dtInput.Rows[0]["Phone"]
                                             , dtInput.Rows[0]["Fax"]
                                             , dtInput.Rows[0]["Email"]
                                             , dtInput.Rows[0]["Representative"]
                                             , dtInput.Rows[0]["Remark"]
                                             , CommonDataHelper.UserName
                                             };
                var result = base.ExecScalar("Warehouse_Insert", sqlParams);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// DeleteWarehouse
        /// </summary>
        /// <param name="dtInput"></param>
        /// <returns></returns>
        public bool DeleteWarehouse(DataTable dtInput)
        {
                var sqlParams = new object[] { dtInput.Rows[0]["Id"]
                                             , CommonDataHelper.UserName
                                             };
                var result = base.ExecScalar("Warehouse_Delete", sqlParams);
                if (NumberHelper.ConvertStringToLong(result.ToString()) > 0)
                {
                    return true;
                }
            return false;
        }
    }
}
