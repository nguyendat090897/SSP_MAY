using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSofT.Warehouse.Provider;
using System.Data;
using System.Transactions;

namespace DSofT.Warehouse.Business
{
    public interface I_PHANCONGTHIETBIBussiness
    {
       DataTable GetListPhanCongThietBi(string MaDVQL);
       bool DeleteHT_PHANCONGTHIETBI(int MaPCTB_ID, string pModifiedBy);
        //bool InsertorUpdatePC_THIETBI(DataTable dst);
       DataTable GetListPhanCongThietBi_CheckDate(string ThietBi_ID, string NgayBD,string NgayKT);
       DataTable GetListLoaiTB(int PCONG_TBI_ID, string MaDVQL, string LoaiTbi);
       DataTable GetList_TBI(string MaDVQL, string Loai_TBI);
       bool Insert_Update_PhangCongThietBi(DataTable dst);
       DataTable GetList_USER();
    }

    public class PHANCONGTHIETBIBussiness : I_PHANCONGTHIETBIBussiness
    {
        I_PhanCongThietBi _PhanCongThietBiProvider;
        public PHANCONGTHIETBIBussiness(string appId, string userId = "0")
        {
            _PhanCongThietBiProvider = new PHANCONGTHIETBIProvider(appId, userId = "0");
        }
        public DataTable GetListPhanCongThietBi(string MaDVQL)
        {
            return _PhanCongThietBiProvider.GetListPhanCongThietBi(MaDVQL);
        }
        public bool DeleteHT_PHANCONGTHIETBI(int MaPCTB_ID, string pModifiedBy)
        {
            return _PhanCongThietBiProvider.DeleteHT_PHANCONGTHIETBI(MaPCTB_ID, pModifiedBy);
        }
        public DataTable GetListPhanCongThietBi_CheckDate(string ThietBi_ID, string NgayBD,string NgayKT)
        {
            return _PhanCongThietBiProvider.GetListPhanCongThietBi_CheckDate(ThietBi_ID, NgayBD, NgayKT);
        }
        public DataTable GetListLoaiTB(int PCONG_TBI_ID, string MaDVQL, string LoaiTbi)
        {
            return _PhanCongThietBiProvider.GetListLoaiTB(PCONG_TBI_ID, MaDVQL, LoaiTbi);
        }
        public DataTable GetList_USER()
        {
            return _PhanCongThietBiProvider.GetList_USER();
        }
        /*public bool InsertorUpdatePC_THIETBI(DataTable dst)
        {
            return _PhanCongThietBiProvider.InsertorUpdatePC_THIETBI(dst);
        }*/
        public DataTable GetList_TBI(string MaDVQL, string Loai_TBI)
        {
            return _PhanCongThietBiProvider.GetList_TBI(MaDVQL, Loai_TBI);
        }
        public bool Insert_Update_PhangCongThietBi(DataTable dst)
        {
            long PC_TBI_ID = 0;
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                if (dst.Rows.Count > 0)
                {
                    for (int i = 0; i < dst.Rows.Count; i++)
                    {

                            PC_TBI_ID = _PhanCongThietBiProvider.Insert_Update_PhangCongThietBi(dst.Rows[i]);
                            if (PC_TBI_ID <= 0)
                            {
                                return false;
                            }
                    }
                }
                else
                    return false;
                trans.Complete();
            }
            if (PC_TBI_ID > 0)
                return true;
            else
                return false;
        }
    }
 }   
