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
    public interface I_ThietLapTonKhoTT_Bussiness
    {
        DataTable GetList(string MaDVQLY);
        bool HT_THIETLAPTONKHO_TT_Del(int ma_QUITAC_BANCHAM_ID);
        DataTable GetKho(string MaDVQLY);
        bool HT_THIETLAPTONKHO_TT_Insert(DataRow dst);
        DataTable GetKey(string MaDVQLY, int KHO_ID, int SP_ID);
    }

    public class ThietLapTonKhoTT_Bussiness : I_ThietLapTonKhoTT_Bussiness
    {
        I_ThietLapTonKhoTT_Provider _ThietLapTonKhoTT_Provider;
        public ThietLapTonKhoTT_Bussiness(string appId, string userId = "0")
        {
            _ThietLapTonKhoTT_Provider = new ThietLapTonKhoTT_Provider(appId, userId = "0");
        }
        public DataTable GetList(string MaDVQLY)
        {
            return _ThietLapTonKhoTT_Provider.GetList(MaDVQLY);
        }
        public bool HT_THIETLAPTONKHO_TT_Del(int ma_QUITAC_BANCHAM_ID)
        {
            return _ThietLapTonKhoTT_Provider.HT_THIETLAPTONKHO_TT_Del(ma_QUITAC_BANCHAM_ID);
        }
        public DataTable GetKho(string MaDVQLY)
        {
            return _ThietLapTonKhoTT_Provider.GetKho(MaDVQLY);
        }
        public bool HT_THIETLAPTONKHO_TT_Insert(DataRow dst)
        {
            return _ThietLapTonKhoTT_Provider.HT_THIETLAPTONKHO_TT_Insert(dst);
        }
        public DataTable GetKey(string MaDVQLY, int KHO_ID, int SP_ID)
        {
            return _ThietLapTonKhoTT_Provider.GetKey(MaDVQLY, KHO_ID, SP_ID);
        }
    }
}
