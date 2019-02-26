using DSofT.Warehouse.Domain.Model;
using DSofT.Framework;
using DSofT.Framework.Internal.Data;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IUserManagerProvider
    {
        //UserModel CheckLogin(LoginModel model);
        /// <summary>
        /// Hàm đăng nhâp
        /// Xư lý
        /// Hàm đăng nhâp
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        DataSet CheckLogin(LoginModel model);
        /// <summary>
        /// hàm lấy bảng user
        /// </summary>
        /// <returns></returns>
        DataSet GetListUser();
        DataSet GetListArea();
        DataSet GetListUserType();
        bool InsertUser(UserModel user);
        bool DeleteUser(UserModel user);
        bool UpdateUser(UserModel user);
        bool CheckUserName(UserModel user);
        //string Get_Password(UserModel user);
    }
    public class UserManagerProvider : BaseSqlExecute, IUserManagerProvider
    {

        public UserManagerProvider(string appId, string userId)
            : base(DbCommon.WarehouseDbConnstr, appId, userId)
        {
        }

        public DataSet CheckLogin(LoginModel model)
        {
            string sql = "	SELECT	"
                     + "	      * 	"
                     + "	    FROM	"
                     + "	      U_User U 	"
                     + "	    WHERE	"
                     + "	      U.UserName = @USERNAME 	"
                     + "	      AND U.[Password] = @PASSWORD 	"
                     + "	      AND IsDelete = 0	";
            var result = base.ExecuteDataSetBySql(sql, new System.Collections.Generic.List<ParamSQLModel>() {
                new ParamSQLModel() { Name = "@USERNAME", Value = model.USER_NAME},
                new ParamSQLModel() { Name = "@PASSWORD", Value = model.PASSWORD}
            });
            if (result != null)
            {
                return result;
            }
            return null;
        }
        /// <summary>
        /// Lista Table User
        /// </summary>
        /// <returns></returns>
        public DataSet GetListUser()
        {
            var paramObj = new object[] { };
            string sqlString = "	SELECT	"
                             + "	  Row_number() over (order by U.UserName) STT	"
                             + "	  , U.*	"
                             + "	  , UT.UserTypeName UserTypeName	"
                             + "	  , '' AreaName 	"
                             + "	FROM	"
                             + "	  U_User U 	"
                             + "	  LEFT JOIN U_UserType UT 	"
                             + "	    ON UT.ID = U.UserGroupID 	"
                             + "	WHERE	"
                             + "	  U.IsDelete = 0	";
            var result = base.ExecuteDataSetBySql(sqlString, null);
            return result;
        }

        public bool InsertUser(UserModel user)
        {
            var paramObj = new object[] { user.USER_NAME, user.FULL_NAME, user.PASSWORD, user.AreaID, user.UserTypeID };
            //base.InsertTable("U_User", user, null);
            base.ExecScalar("U_USER_INSERT", paramObj);
            return true;
        }
        public bool DeleteUser(UserModel user)
        {
            var paramObj = new ParamSQLModel[] { new ParamSQLModel() { Name = "@ID", Value = user.USER_ID.ToString() } };
            string sqlString = "	UPDATE U_User 	"
                             + "	SET	"
                             + "	  IsDelete = 1 	"
                             + "	where	"
                             + "	  ID = @ID	";
            base.ExecScalar(sqlString, paramObj);
            return true;
        }

        public bool CheckUserName(UserModel user)
        {
            var paramObj = new ParamSQLModel[] {
                new ParamSQLModel() { Name = "@UserName", Value = user.USER_NAME}
            };
            string sqlString = "	 SELECT	"
                             + "	      COUNT(UserName) 	"
                             + "	    FROM	"
                             + "	      U_User 	"
                             + "	    WHERE	"
                             + "	      UserName = @UserName 	"
                             + "	      AND (IsDelete IS NULL OR IsDelete = 0)	";
            var result = base.ExecScalar(sqlString, paramObj);
            if ((int)result > 1)
                return true;
            return false;
        }
        public bool UpdateUser(UserModel user)
        {
            //var paramObj = new object[] { user.USER_ID, user.USER_NAME, user.FULL_NAME, user.PASSWORD, user.AreaID, user.UserTypeID };
            base.UpdateTable("U_User", user, null);
            //base.ExecScalar("U_USER_UPDATE", paramObj);
            return true;
        }
        public DataSet GetListArea()
        {


            var paramObj = new object[] { };
            var result = base.ExecuteDataSet("C_Area_GET_LIST", paramObj);
            return result;
        }
        public DataSet GetListUserType()
        {

            var paramObj = new object[] { };
            var result = base.ExecuteDataSet("U_USER_ROLE_GET_USERTYPE", paramObj);
            return result;
        }

        //public string Get_Password(UserModel user)
        //{

        //    var paramObj = new object[] { user.USER_ID };
        //    return (string)base.ExecScalar("U_USER_GET_PASSWORD", paramObj);

        //}

        /// <summary>
        /// check login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public UserModel CheckLogin(LoginModel model)
        //{
        //var paramObj = new object[] {
        //model.USER_NAME,
        //model.PASSWORD
        //};

        //var result = base.ExecStoredProc<UserModel>("BLOG_GET_CHECKLOGIN", paramObj);
        //if (result != null)
        //{
        //return result.FirstOrDefault();
        //}
        //return null;
        //}
    }
}
