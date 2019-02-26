using DSofT.Framework;
using DSofT.Framework.Internal.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSofT.Warehouse.Provider
{
    public interface IUserProvider
    {
        //UserModel CheckLogin(LoginModel model);
        /// <summary>
        /// Hàm đăng nhâp
        /// Xư lý
        /// Hàm đăng nhâp
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool User_ChangePassword(DataSet dsInput);
    }
    public class UserProvider : BaseSqlExecute, IUserProvider
    {
        public UserProvider(string appId, string userId)
            : base(DbCommon.WarehouseDbConnstr, appId, userId)
        {
        }

        public bool User_ChangePassword(DataSet dsInput)
        {
            DataTable dst = new DataTable();
            dst = dsInput.Tables[0];
            var paramObj = new ParamSQLModel[] {
                new ParamSQLModel() { Name = "@ID", Value = dst.Rows[0]["ID"].ToString()  },
                new ParamSQLModel() { Name = "@Passwordold", Value = dst.Rows[0]["Passwordold"].ToString()  },
                new ParamSQLModel() { Name = "@Passwordnew", Value = dst.Rows[0]["Passwordnew"].ToString() }
            };
            string sqlString = "	UPDATE U_User 	"
                             + "	SET	"
                             + "	  Password = @Passwordnew 	"
                             + "	WHERE	"
                             + "	  ID = @ID  AND Password = @Passwordold	";
            var result = (int)base.ExecScalar(sqlString, paramObj);
            return result == 1;
        }
    }
}
