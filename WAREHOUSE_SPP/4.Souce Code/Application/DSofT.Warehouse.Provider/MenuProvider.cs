using DSofT.Warehouse.Domain.Model;
using DSofT.Framework;
using DSofT.Framework.Internal.Data;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IMenuProvider
    {
        DataSet CheckLogin(LoginModel model);
        DataSet GetAllMenu();
        DataSet GetAllMenuParent();
        bool InsertMenu(DataSet dsInput);
        bool UpdateMenu(DataSet dsInput);
        bool DeleteMenu(string strID);
    }
    public class MenuProvider : BaseSqlExecute, IMenuProvider
    {

        public MenuProvider(string appId, string userId)
            : base(DbCommon.WarehouseDbConnstr, appId, userId)
        {
        }
        public DataSet CheckLogin(LoginModel model)
        {
            var paramObj = new object[] {
                model.USER_NAME,
                model.PASSWORD
            };

            var result = base.ExecuteDataSet("BLOG_GET_CHECKLOGIN", paramObj);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public DataSet GetAllMenu()
        {
            var paramObj = new List<ParamSQLModel>();
            string sqlString = "	SELECT	"
                             + "	  ID	"
                             + "	  , MenuCode	"
                             + "	  , MenuName	"
                             + "	  , MenuNamespaceClass	"
                             + "	  , MenuClassName	"
                             + "	  , MenuRemark	"
                             + "	  , MenuIcon	"
                             + "	  , MenuParentID	"
                             + "	  , ParentID = ( 	"
                             + "	    select	"
                             + "	      MenuName 	"
                             + "	    from	"
                             + "	      U_Menu 	"
                             + "	    where	"
                             + "	      ID = m.MenuParentID	"
                             + "	  ) 	"
                             + "	FROM	"
                             + "	  U_Menu m 	"
                             + "	WHERE	"
                             + "	  IsDelete = 0	";
            var result = base.ExecuteDataSetBySql(sqlString, paramObj);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllMenuParent()
        {
            string sqlString = "	SELECT	"
                             + "	  MenuName	"
                             + "	  , ID as MenuParentID 	"
                             + "	FROM	"
                             + "	  U_Menu 	"
                             + "	WHERE	"
                             + "	  IsDelete = 0 	"
                             + "	  And MenuParentID = 0; 	";
            var result = base.ExecuteDataSetBySql(sqlString, new List<ParamSQLModel>());
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public bool InsertMenu(DataSet dsInput)
        {
            DataTable dst = dsInput.Tables[0];
            return base.InsertTable("U_MENU", dst, null);
        }

        public bool UpdateMenu(DataSet dsInput)
        {
            DataTable dst = new DataTable();
            dst = dsInput.Tables[0];
            //var paramObj = new object[8] { dst.Rows[0]["MenuCode"], dst.Rows[0]["MenuName"], dst.Rows[0]["MenuNamespaceClass"], dst.Rows[0]["MenuClassName"], dst.Rows[0]["MenuRemark"], dst.Rows[0]["MenuIcon"], dst.Rows[0]["MenuParentID"], dst.Rows[0]["ID"] };
            var result = base.UpdateTable("U_MENU", dst, null);// (int)base.ExecScalar("U_MENU_UPDATE", paramObj);
            return result;
        }
        public bool DeleteMenu(string strID)
        {
            DataTable dst = new DataTable();
            var paramObj = new ParamSQLModel[1] { new ParamSQLModel() { Name = "@ID", Value = strID } };
            string sqlString = "	UPDATE U_Menu 	"
                             + "	SET	"
                             + "	  IsDelete = 1 	"
                             + "	WHERE	"
                             + "	  ID = @ID 	";
            var result = base.ExecuteNonQuery(sqlString, paramObj);
            return result == 1;
        }
    }
}
