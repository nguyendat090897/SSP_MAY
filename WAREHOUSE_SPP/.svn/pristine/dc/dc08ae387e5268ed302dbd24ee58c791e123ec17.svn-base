using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System;
using System.Data;
using System.Transactions;

namespace DSofT.Warehouse.Provider
{
    public interface IQuantitativeFoodProvider
    {
        DataTable GetAllQuantitativeFood(DataTable dtInput);
        bool InsertOrUpdateQuantitativeFood(DataTable dtInput,string schoolId);
    }
    public class QuantitativeFoodProvider : BaseSqlExecute, IQuantitativeFoodProvider
    {

        public QuantitativeFoodProvider(string appId, string userId)
            : base(DbCommon.WarehouseDbConnstr, appId, userId)
        {
        }
        /// <summary>
        /// Get danh sách định lượng
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllQuantitativeFood(DataTable dtInput)
        {
            var sqlParams = new object[]
                                {
                                 string.IsNullOrWhiteSpace(dtInput.Rows[0]["foodId"].ToString())==true?"0":dtInput.Rows[0]["foodId"],
                                 string.IsNullOrWhiteSpace(dtInput.Rows[0]["foodTypeId"].ToString())==true?"0":dtInput.Rows[0]["foodTypeId"],
                                 string.IsNullOrWhiteSpace(dtInput.Rows[0]["schoolId"].ToString())==true?"0":dtInput.Rows[0]["schoolId"]                                     
                                };
            var result =base.ExecuteDataSet("QuantitativeFood_GetAll", sqlParams);
            if (result != null)
            {
                return result.Tables[0];
            }
            return null;
        }
        /// <summary>
        /// Insert Quantitative Food
        /// </summary>
        /// <param name="dsInput"></param>
        /// <returns></returns>
        public bool InsertOrUpdateQuantitativeFood(DataTable dtInput,string schoolId)
        {
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
              new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                foreach (DataRow item in dtInput.Rows)
                {
                    //update
                    if (NumberHelper.ConvertStringToLong(item["QuantitativeFoodId"].ToString()) > 0)
                    {
                        var sqlParams = new object[]
                                {
                                         item["QuantitativeFoodId"]
                                        ,item["FoodId"]
                                        ,item["MaterialId"]
                                        ,item["QuantitativeOne"]
                                        ,item["QuantitativeTwo"]
                                        ,item["QuantitativeOrther"]
                                        ,item["Remark"]
                                        ,CommonDataHelper.UserName
                                        ,item["Price"]
                                        ,schoolId
                                };
                        var resultbalance = Convert.ToInt32(base.ExecScalar("QuantitativeFood_Update", sqlParams));
                        if (resultbalance <= 0)
                        {
                            return false;
                        }
                    }
                   // insert
                    else
                    {
                        var sqlParams = new object[]
                               {
                                         item["FoodId"]
                                        ,item["MaterialId"]
                                        ,item["QuantitativeOne"]
                                        ,item["QuantitativeTwo"]
                                        ,item["QuantitativeOrther"]
                                        ,item["Remark"]
                                        ,CommonDataHelper.UserName
                                        ,item["Price"]
                                        ,schoolId
                               };
                        var resultbalance = Convert.ToInt32(base.ExecScalar("QuantitativeFood_Insert", sqlParams));
                        if (resultbalance <= 0)
                        {
                            return false;
                        }
                    }
                    
                }
                trans.Complete();
            }
            return true;
        }
    }
}

