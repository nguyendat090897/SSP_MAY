using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface I_QuanLy_CongNo_KH_NCC_Bussiness
    {
        DataTable GetAll(string MaDVQLY, string loai);
        DataTable GetHD(string MaDVQLY, int KH_NCC_ID);
        DataTable ChekHD(string MaDVQLY, string So_HD);
        bool Insert(DataTable dt);
        bool Update(DataTable dt);
        bool Delete(string MaDVQLY, int HOPDONG_ID);
    }

    public class QuanLy_CongNo_KH_NCC_Bussiness: I_QuanLy_CongNo_KH_NCC_Bussiness
    {
        I_QuanLy_CongNo_KH_NCC_Provider _QuanLy_CongNo_KH_NCC_Provider;
        public QuanLy_CongNo_KH_NCC_Bussiness(string appId, string userId = "0")
        {
            _QuanLy_CongNo_KH_NCC_Provider = new QuanLy_CongNo_KH_NCC_Provider(appId, userId = "0");
        }
        public DataTable GetAll(string MaDVQLY, string loai)
        {
            return _QuanLy_CongNo_KH_NCC_Provider.GetAll(MaDVQLY, loai);
        }
        public DataTable GetHD(string MaDVQLY, int KH_NCC_ID)
        {
            return _QuanLy_CongNo_KH_NCC_Provider.GetHD(MaDVQLY, KH_NCC_ID);
        }
        public bool Insert(DataTable dt)
        {
            return _QuanLy_CongNo_KH_NCC_Provider.Insert(dt);
        }
        public bool Update(DataTable dt)
        {
            return _QuanLy_CongNo_KH_NCC_Provider.Update(dt);
        }
        public DataTable ChekHD(string MaDVQLY, string So_HD)
        {
            return _QuanLy_CongNo_KH_NCC_Provider.ChekHD(MaDVQLY, So_HD);
        }
        public bool Delete(string MaDVQLY, int HOPDONG_ID)
        {
            return _QuanLy_CongNo_KH_NCC_Provider.Delete(MaDVQLY, HOPDONG_ID);
        }
    }
}
