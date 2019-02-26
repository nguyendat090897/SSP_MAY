using DSofT.Warehouse.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSofT.Warehouse.Business
{
    public interface I_ThamSo_Bussiness
    {
        DataTable GetAll(string MaDVQLY);
        DataTable GetGT(DataRow dr);
        bool Update(DataRow dr);
    }

    public class ThamSo_Bussiness: I_ThamSo_Bussiness
    {
        I_ThamSo_Provider _ThamSo_Bussiness;
        public ThamSo_Bussiness(string appId, string userId = "0")
        {
            _ThamSo_Bussiness = new ThamSo_Provider(appId, userId="0");
        }
        public DataTable GetAll(string MaDVQLY)
        {
            return _ThamSo_Bussiness.GetAll(MaDVQLY);
        }
        public bool Update(DataRow dr)
        {
            return _ThamSo_Bussiness.Update(dr);
        }
        public DataTable GetGT(DataRow dr)
        {
            return _ThamSo_Bussiness.GetGT(dr);
        }
    }
}
