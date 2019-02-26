using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSofT.Framework.Internal.Data;
using System.Data;
using DSofT.Framework;

namespace DSofT.Warehouse.Provider
{
    public interface IUserTypeManagerProvider
    {
        DataSet GetList();
        bool InsertUserType(DataSet ds);
        bool DeleteUserType(DataSet ds);
        bool UpdateUserType(DataSet ds);
    }
    public class UserTypeManagerProvider : BaseSqlExecute, IUserTypeManagerProvider
    {

        public UserTypeManagerProvider(string appId, string userId)
            : base(DbCommon.WarehouseDbConnstr, appId, userId)
        {
        }

        public DataSet GetList()
        {
            var paramObj = new ParamSQLModel[] { };
            string sqlString = "	SELECT	"
                             + "	  * 	"
                             + "	FROM	"
                             + "	  U_UserType 	"
                             + "	WHERE	"
                             + "	  IsDelete = 0	";
            var result = base.ExecuteDataSetBySql(sqlString, null);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public bool InsertUserType(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            //var paramObj = new object[4] { dt.Rows[0]["UserTypeCode"], dt.Rows[0]["UserTypeName"], dt.Rows[0]["UserTypeGroup"], dt.Rows[0]["UserTypeDescription"] };
            //var result = base.ExecScalar("U_USERTYPE_INSERT", paramObj);
            base.InsertTable("U_UserType", dt, null);
            return true;
            //if ((int)result == 1)
            //    return true;
            //return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public bool DeleteUserType(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            var paramObj = new ParamSQLModel[1] { new ParamSQLModel() { Name = "@Id", Value = dt.Rows[0]["ID"].ToString() } };
            string sqlString = "	UPDATE U_UserType 	"
                 + "	SET	"
                 + "	  IsDelete = 1 	"
                 + "	WHERE	"
                 + "	  ID = @Id 	";
            var result = base.ExecuteNonQuery(sqlString, paramObj);
            return true;
        }

        public bool UpdateUserType(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            //var paramObj = new object[4] { dt.Rows[0]["UserTypeCode"], dt.Rows[0]["UserTypeName"], dt.Rows[0]["UserTypeGroup"], dt.Rows[0]["UserTypeDescription"] };
            //var result = base.ExecScalar("U_USERTYPE_INSERT", paramObj);
            base.UpdateTable("U_UserType", dt, null);
            return true;
            //if ((int)result == 1)
            //    return true;
            //return false;

            //DataTable dt = new DataTable();
            //dt = ds.Tables[0];
            //var paramObj = new object[5] { dt.Rows[0]["ID"], dt.Rows[0]["UserTypeCode"], dt.Rows[0]["UserTypeName"], dt.Rows[0]["UserTypeGroup"], dt.Rows[0]["UserTypeDescription"] };
            //var result = base.ExecScalar("U_USERTYPE_UPDATE", paramObj);
            //if ((int)result == 1)
            //    return true;
            //return false;
        }
    }
}
