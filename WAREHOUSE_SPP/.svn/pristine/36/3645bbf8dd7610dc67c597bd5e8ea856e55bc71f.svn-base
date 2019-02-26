using DSofT.Framework.Internal.Data;
using DSofT.Framework.UIControl;
using System;
using System.Collections.Generic;
using System.Data;

namespace DSofT.Warehouse.Provider
{
    public interface IclsCommonProvider
    {
        DataTable GetList_ITEM_TYPE();
    }
    public  class clsCommonProvider : BaseSqlExecute, IclsCommonProvider
    {
        public clsCommonProvider(string appId, string userId) :
base(DbCommon.WarehouseDbConnstr, appId, userId)
        { }


        public  DataTable GetList_ITEM_TYPE()
        {
            try
            {
                var sqlParams = new object[] { };
                var result = base.ExecuteDataSet("DM_NHOM_SPHAM_GET_ITEM_TYPE", sqlParams);
                if (result != null)
                    return result.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
