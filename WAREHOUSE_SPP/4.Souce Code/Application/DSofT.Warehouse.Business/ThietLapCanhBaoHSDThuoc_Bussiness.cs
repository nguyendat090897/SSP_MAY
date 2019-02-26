using DSofT.Warehouse.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSofT.Warehouse.Business
{
    public interface I_ThietLapCanhBaoHSDThuoc_Bussiness
    {
        DataTable GetAll(string MaDVQLY);
        DataTable GetKey(string MaDVQLY, int SP_ID);
        bool Delete(string MaDVQLY, int QuitacHSD_ID);
        bool Insert_Udate(DataRow dtr);
    }

    public class ThietLapCanhBaoHSDThuoc_Bussiness: I_ThietLapCanhBaoHSDThuoc_Bussiness
    {
        I_ThietLapCanhBaoHSDThuoc_Provider _ThietLapCanhBaoHSDThuoc;
        public ThietLapCanhBaoHSDThuoc_Bussiness(string appId, string userId = "0")
        {
            _ThietLapCanhBaoHSDThuoc = new ThietLapCanhBaoHSDThuoc_Provider(appId, userId = "0");
        }

        public DataTable GetAll(string MaDVQLY)
        {
            return _ThietLapCanhBaoHSDThuoc.GetAll(MaDVQLY);
        }
        public bool Delete(string MaDVQLY, int QuitacHSD_ID)
        {
            return _ThietLapCanhBaoHSDThuoc.Delete(MaDVQLY, QuitacHSD_ID);
        }
        public bool Insert_Udate(DataRow dtr)
        {
            return _ThietLapCanhBaoHSDThuoc.Insert_Udate(dtr);
        }
        public DataTable GetKey(string MaDVQLY, int SP_ID)
        {
            return _ThietLapCanhBaoHSDThuoc.GetKey(MaDVQLY, SP_ID);
        }
    }
}
