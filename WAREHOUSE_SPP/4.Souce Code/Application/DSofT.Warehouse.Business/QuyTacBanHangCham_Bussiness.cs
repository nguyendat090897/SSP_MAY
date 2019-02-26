using DSofT.Warehouse.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSofT.Warehouse.Business
{

    public interface I_QuyTacBanHangCham_Bussiness
    {
        DataTable GetAll(string MaDVQLY);
        DataTable GetKey(string MaDVQLY, int SP_ID);
        bool Delete(string MaDVQLY, int QuitacHSD_ID);
        bool Insert_Udate(DataRow dtr);
    }

    public class QuyTacBanHangCham_Bussiness : I_QuyTacBanHangCham_Bussiness
    {
        I_QuyTacBanHangCham_Provider _QuyTacBanHangCham_Bussiness;
        public QuyTacBanHangCham_Bussiness(string appId, string userId = "0")
        {
            _QuyTacBanHangCham_Bussiness = new QuyTacBanHangCham_Provider(appId, userId = "0");
        }

        public DataTable GetAll(string MaDVQLY)
        {
            return _QuyTacBanHangCham_Bussiness.GetAll(MaDVQLY);
        }
        public bool Delete(string MaDVQLY, int QuitacHSD_ID)
        {
            return _QuyTacBanHangCham_Bussiness.Delete(MaDVQLY, QuitacHSD_ID);
        }
        public bool Insert_Udate(DataRow dtr)
        {
            return _QuyTacBanHangCham_Bussiness.Insert_Udate(dtr);
        }
        public DataTable GetKey(string MaDVQLY, int SP_ID)
        {
            return _QuyTacBanHangCham_Bussiness.GetKey(MaDVQLY, SP_ID);
        }
    }
}
