using DSofT.Warehouse.Domain.Model;
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IMenuBussiness
    {
        DataSet CheckLogin(LoginModel model);
        DataSet GetAllMenu();
        DataSet GetAllMenuParent();
        bool InsertMenu(DataSet dsInput);
        bool UpdateMenu(DataSet dsInput);
        bool DeleteMenu(string strID);

    }
    public class MenuBussiness : IMenuBussiness
    {
        IMenuProvider _userManagerProvider;
        /// <summary>
        /// Hàm khởi tạo bắt buộc
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        public MenuBussiness(string appId, string userId = "0")
        {
            _userManagerProvider = new MenuProvider(appId, userId);
        }
        public DataSet CheckLogin(LoginModel model)
        {
            var res = _userManagerProvider.CheckLogin(model);
            return res;
        }
        public DataSet GetAllMenu()
        {
            var res = _userManagerProvider.GetAllMenu();
            return res;
        }
        public DataSet GetAllMenuParent()
        {
            var res = _userManagerProvider.GetAllMenuParent();
            return res;
        }
        public bool InsertMenu(DataSet dsInput)
        {
            var res = _userManagerProvider.InsertMenu(dsInput);
            return res;
        }
        public bool UpdateMenu(DataSet dsInput)
        {
            var res = _userManagerProvider.UpdateMenu(dsInput);
            return res;
        }
        public bool DeleteMenu(string strID)
        {
            var res = _userManagerProvider.DeleteMenu(strID);
            return res;
        }
    }
}
