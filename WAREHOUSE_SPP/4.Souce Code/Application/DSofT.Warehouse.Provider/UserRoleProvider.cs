using DSofT.Framework;
using DSofT.Framework.Internal.Data;
using DSofT.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace DSofT.Warehouse.Provider
{
    public interface IUserTypeProvider
    {
        DataSet Load_Data();
        DataSet Load_Menu_Condition(int UserTypeID);
        bool Update_UserRole(int UserTypeID, DataSet ListMenu);
    }
    public class UserRoleProvider : BaseSqlExecute, IUserTypeProvider
    {
        public UserRoleProvider(string appID, string userID)
            : base(DbCommon.WarehouseDbConnstr, appID, userID)
        {
        }

        /// <summary>
        /// Lấy danh sách nhóm người dùng từ CSDL
        /// </summary>
        /// <returns>Trả về danh sách nhóm thỏa yêu cầu</returns>
        public DataSet Load_Data()
        {
            string sqlString = "SELECT * FROM U_UserType WHERE IsDelete = 0";
            var result = base.ExecuteDataSetBySql(sqlString, null);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Thêm chức năng vào nhóm người dùng
        /// </summary>
        /// <param name="UserTypeID"></param>
        /// <param name="ListMenu"></param>
        /// <returns></returns>
        public bool Update_UserRole(int UserTypeID, DataSet ListMenu)
        {
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.Required,
                  new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                {
                    string sqlString = "	DELETE U_User_Role	"
                     + "	      WHERE UserTypeID = @UserTypeId	"
                     + "	        AND MenuID = @MenuId	";
                    var paramObj = new ParamSQLModel[] {
                        new ParamSQLModel() {  Name = "@UserTypeId", Value = UserTypeID.ToString() }
                    };
                    base.ExecScalar(sqlString, paramObj);
                    sqlString = "INSERT INTO U_User_Role (UserTypeID, MenuID) VALUES ";
                    int idx = 0;
                    List<ParamSQLModel> lstParam = new List<ParamSQLModel>();
                    foreach (DataRow dr in ListMenu.Tables[0].Rows)
                    {
                        sqlString += string.Format("(@UserTypeId{0}, @MenuId{0})", idx);
                        lstParam.Add(new ParamSQLModel() { Name = string.Format("@UserTypeId{0}", idx), Value = UserTypeID.ToString() });
                        lstParam.Add(new ParamSQLModel() { Name = string.Format("@MenuId{0}", idx), Value = dr["ID"].ToString() });
                        CommonLogger.WriteLog(new Exception(sqlString), SystemLogSource.SystemRuntime);
                        base.ExecScalar(sqlString, lstParam.ToArray());
                        //var result = (int)base.ExecScalar("U_USER_ROLE_CHANGE_ROLE", paramObj);
                        //if (result == 0)
                        //{
                        //    //return false;
                        //}
                    }
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                CommonLogger.WriteLog(ex, SystemLogSource.SystemRuntime);
                return false;
            }
            return true;
        }

        public DataSet Load_Menu_Condition(int UserTypyID)
        {
            var paramObj = new ParamSQLModel[] { new ParamSQLModel() { Name= "USERTYPEID", Value = UserTypyID.ToString() } };
            string sqlString = "	SELECT	"
                             + "	  M.ID	"
                             + "	  , M.MENUCODE	"
                             + "	  , M.MENUNAME	"
                             + "	  , CASE 	"
                             + "	    WHEN UR.ID IS NULL 	"
                             + "	      THEN 0 	"
                             + "	    ELSE 1 	"
                             + "	    END STATUS 	"
                             + "	FROM	"
                             + "	  U_Menu M 	"
                             + "	  LEFT JOIN U_User_Role UR 	"
                             + "	    ON M.ID = UR.MenuID 	"
                             + "	    AND UR.UserTypeID ="  + UserTypyID
                             + "	WHERE	"
                             + "	  M.IsDelete = 0	";

            var result = base.ExecuteDataSetBySql(sqlString, paramObj.ToList());
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
