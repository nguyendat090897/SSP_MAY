using DSofT.Warehouse.Domain.Model.System;
using DSofT.Framework;
using DSofT.Framework.Internal.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DSofT.Warehouse.Provider
{
    public interface ISystemProvider
    {
        List<MenuDBModel> GetListMenuSystem(MenuFilterModel model);
        DataSet ExecuteDataSet(string storeName, object[] paramObj);
        DataSet ExecuteDataSet(string storeName, List<ParamSQLModel> paramObj);
    }
    public class SystemProvider : BaseSqlExecute, ISystemProvider
    {

        public SystemProvider(string appId, string userId)
            : base(DbCommon.WarehouseDbConnstr, appId, userId)
        {
        }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public DataSet ExecuteDataSet(string storeName, object[] paramObj)
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            DataSet ds = new DataSet();
            ds = base.ExecuteDataSet(storeName, paramObj);
            return ds;
        }

        public new DataSet ExecuteDataSet(string storeName, List<ParamSQLModel> paramObj)
        {
            DataSet ds = new DataSet();
            ds = base.ExecuteDataSet(storeName, paramObj);
            return ds;
        }
        public List<MenuDBModel> GetListMenuSystem(MenuFilterModel model)
        {
            var paramObj = new List<ParamSQLModel>();
            string sqlString = "	SELECT DISTINCT	"
                             + "	  M.*	"
                             + "	FROM U_Menu M	"
                             + "	WHERE (M.IsDelete IS NULL	"
                             + "	OR M.IsDelete = 0)	"
                             + "	AND (EXISTS (SELECT	"
                             + "	  *	"
                             + "	FROM U_UserType UT	"
                             + "	INNER JOIN U_User U	"
                             + "	  ON U.UserGroupID = UT.ID	"
                             + "	  AND U.ID = @UserId	"
                             + "	  AND @UserTypeGroup = UT.UserTypeGroup)	"
                             + "	OR M.ID IN (SELECT	"
                             + "	  UR.MenuID	"
                             + "	FROM U_User_Role UR	"
                             + "	INNER JOIN U_UserType UT	"
                             + "	  ON UT.IsDelete = 0	"
                             + "	  AND UT.ID = UR.UserTypeID	"
                             + "	INNER JOIN U_User U	"
                             + "	  ON U.UserGroupID = UT.ID	"
                             + "	  AND U.ID = @UserId)	"
                             + "	)	";

            paramObj.Add(new ParamSQLModel() { Name = "UserId", Value = model.UserId });
            paramObj.Add(new ParamSQLModel() { Name = "UserTypeGroup", Value = "99" });
            var result = base.ExecSql<MenuDBModel>(sqlString, paramObj.ToArray());
            if (result != null)
            {
                return result.ToList();
            }
            return new List<MenuDBModel>();

        }
    }
}
