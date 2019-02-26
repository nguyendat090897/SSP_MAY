using DSofT.Warehouse.Domain.Model.System;
using DSofT.Warehouse.Provider;
using DSofT.Framework;
using DSofT.Framework.Internal.Data;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface ISystemBussiness
    {
        List<MenuDBModel> GetListMenuSystem(MenuFilterModel model);
        DataSet ExecuteDataSet(string storeName, object[] paramObj);
        DataSet ExecuteDataSet(string storeName, List<ParamSQLModel> paramObj);

    }
    public class SystemBussiness : ISystemBussiness
    {
        
        ISystemProvider _systemProvider;
        /// <summary>
        /// Hàm khởi tạo bắt buộc
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        public SystemBussiness(string appId, string userId = "0")
        {
            _systemProvider = new SystemProvider(appId, userId);
        }
        
        public DataSet ExecuteDataSet(string storeName, object[] paramObj)
        {
            return _systemProvider.ExecuteDataSet(storeName, paramObj);
        }
        public DataSet ExecuteDataSet(string storeName, List<ParamSQLModel> paramObj)
        {
            return _systemProvider.ExecuteDataSet(storeName, paramObj);
        }
        public List<MenuDBModel> GetListMenuSystem(MenuFilterModel model)
        {
            return _systemProvider.GetListMenuSystem(model);
        }

    }
}
