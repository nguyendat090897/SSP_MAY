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
    public interface I_QuanLy_PhieuThu_KH_NCC_Bussiness
    {
        DataTable GetAll(string MaDVQLY, string Loai_phieu);
        DataTable GetKH(string MaDVQLY, string Loai_KHNCC);
        DataTable GetHD(string MaDVQLY, int MA_KH);
        DataTable CheckPT(string MaDVQLY, string MA_PT, string Loai_phieu);
        bool Delete(string MaDVQLY, int ID);
        bool Insert(DataTable dt);
        bool Update(DataTable dt);
    }
    public class QuanLy_PhieuThu_KH_NCC_Bussiness: I_QuanLy_PhieuThu_KH_NCC_Bussiness
    {
        I_QuanLy_PhieuThu_KH_NCC_Provider _QuanLy_PhieuThu_KH_NCC_Bussiness;
        public QuanLy_PhieuThu_KH_NCC_Bussiness(string appId, string userId = "0")
        {
            _QuanLy_PhieuThu_KH_NCC_Bussiness = new QuanLy_PhieuThu_KH_NCC_Provider(appId, userId = "0");
        }
        public DataTable GetAll(string MaDVQLY, string Loai_phieu)
        {
            return _QuanLy_PhieuThu_KH_NCC_Bussiness.GetAll(MaDVQLY, Loai_phieu);
        }
        public DataTable GetKH(string MaDVQLY, string Loai_KHNCC)
        {
            return _QuanLy_PhieuThu_KH_NCC_Bussiness.GetKH(MaDVQLY, Loai_KHNCC);
        }
        public DataTable GetHD(string MaDVQLY, int MA_KH)
        {
            return _QuanLy_PhieuThu_KH_NCC_Bussiness.GetHD(MaDVQLY, MA_KH);
        }
        public bool Delete(string MaDVQLY, int ID)
        {
            return _QuanLy_PhieuThu_KH_NCC_Bussiness.Delete(MaDVQLY,ID);
        }
        public bool Insert(DataTable dt)
        {
            return _QuanLy_PhieuThu_KH_NCC_Bussiness.Insert(dt);
        }
        public bool Update(DataTable dt)
        {
            return _QuanLy_PhieuThu_KH_NCC_Bussiness.Update(dt);
        }
        public DataTable CheckPT(string MaDVQLY, string MA_PT, string Loai_phieu)
        {
            return _QuanLy_PhieuThu_KH_NCC_Bussiness.CheckPT(MaDVQLY, MA_PT, Loai_phieu);
        }
    }
}
