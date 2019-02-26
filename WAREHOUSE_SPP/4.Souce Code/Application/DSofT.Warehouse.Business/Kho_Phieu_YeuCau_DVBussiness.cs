
using DSofT.Warehouse.Provider;
using System;
using System.Data;
using System.Linq;
using System.Transactions;

namespace DSofT.Warehouse.Business
{
    public interface IKho_Phieu_YeuCau_DVBussiness
    {
        DataSet InsertorUpdateKho_Lam_Phieu_DV_And_CTiet(string MA_DVIQLY, DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string User);
        int DeleteKho_Lam_Phieu_DV(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID, string ModifiledBy);
        DataTable GetListKho_Lam_Phieu_DV(string MA_DVIQLY);
        DataSet GetListKho_Lam_Phieu_DV_By_ID(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID);

        DataTable GetData_Loai_DV(string MA_DVIQLY);
        DataTable GetListDM_KHO(string pMA_DVIQLY);
        DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID);
        int DeleteKho_Lam_Phieu_DV_CT(string MA_DVIQLY, int PHIEUYEUCAU_DV_CTIET_ID, string ModifiledBy);

        int CheckExist(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID, string SoPhieu);
        DataTable GetList_LamPhieuDV_CTiet_NVL(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID);
        DataTable GetList_LamPhieuDV_CTiet_NVL_NEW(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID);
        DataSet Get_DinhMuc_SP_All(string MA_DVIQLY);
        int KO_PHIEUYEUCAU_DV_CTIET_NVL_DELETE_NEW(string pMA_DVIQLY, long ppPHIEUYEUCAU_DV_ID, string ModifiledBy);

    }
    public class Kho_Phieu_YeuCau_DVBussiness : IKho_Phieu_YeuCau_DVBussiness
    {
        IKho_Lam_Phieu_DVProvider _IKho_Lam_Phieu_DVProvider;
        public Kho_Phieu_YeuCau_DVBussiness(string appId, string userId = "0")
        {
            _IKho_Lam_Phieu_DVProvider = new Kho_Lam_Phieu_DVProvider(appId, userId);
        }
        public DataTable GetList_LamPhieuDV_CTiet_NVL_NEW(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID)
        {
            return _IKho_Lam_Phieu_DVProvider.GetList_LamPhieuDV_CTiet_NVL_NEW(MA_DVIQLY, PHIEUYEUCAU_DV_ID);
        }
        public int KO_PHIEUYEUCAU_DV_CTIET_NVL_DELETE_NEW(string pMA_DVIQLY, long ppPHIEUYEUCAU_DV_ID, string ModifiledBy)
        {
            return _IKho_Lam_Phieu_DVProvider.KO_PHIEUYEUCAU_DV_CTIET_NVL_DELETE_NEW(pMA_DVIQLY, ppPHIEUYEUCAU_DV_ID, ModifiledBy);
        }
        public DataSet Get_DinhMuc_SP_All(string MA_DVIQLY)
        {
            return _IKho_Lam_Phieu_DVProvider.getDINHMUC_SANPHAM_ALL(MA_DVIQLY);
        }
        public DataTable GetList_LamPhieuDV_CTiet_NVL(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID)
        {
            return _IKho_Lam_Phieu_DVProvider.GetList_LamPhieuDV_CTiet_NVL(MA_DVIQLY, PHIEUYEUCAU_DV_ID);
        }
        public int CheckExist(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID, string SoPhieu)
        {
            return _IKho_Lam_Phieu_DVProvider.CheckExist(MA_DVIQLY, PHIEUYEUCAU_DV_ID, SoPhieu);
        }
        public int DeleteKho_Lam_Phieu_DV_CT(string MA_DVIQLY, int PHIEUYEUCAU_DV_CTIET_ID, string ModifiledBy)
        {
            return _IKho_Lam_Phieu_DVProvider.DeleteKho_Lam_Phieu_DV_CT(MA_DVIQLY, PHIEUYEUCAU_DV_CTIET_ID, ModifiledBy);
        }
        public DataTable DM_KHO_KHUVUC_GET_LIST_BY_KHO(string pMA_DVIQLY, long pKHO_ID)
        {
            return _IKho_Lam_Phieu_DVProvider.DM_KHO_KHUVUC_GET_LIST_BY_KHO(pMA_DVIQLY, pKHO_ID);
        }
        public DataTable GetListDM_KHO(string pMA_DVIQLY)
        {
            return _IKho_Lam_Phieu_DVProvider.GetListDM_KHO(pMA_DVIQLY);
        }
        public DataSet InsertorUpdateKho_Lam_Phieu_DV_And_CTiet(string MA_DVIQLY, DataTable dtDauphieu, DataTable dtPhieu_Ctiet, string User)
        {
            DataSet ds_return = null;
            int PHIEUYEUCAU_DV_CTIET_ID = 0;
            int PHIEUYEUCAU_DV_ID = 0;
            DataSet dsDinhmuc_all = null;

            dsDinhmuc_all = _IKho_Lam_Phieu_DVProvider.getDINHMUC_SANPHAM_ALL(MA_DVIQLY);
            DataTable dt_NVLKEM = new DataTable();
            DataRow[] dr_dinhmuc = null;
            DataRow[] dr_dinhmuc_nvlkem = null;
            dt_NVLKEM.Columns.Add("MA_ITEM_TYPE", typeof(string));
            dt_NVLKEM.Columns.Add("SANPHAM_ID", typeof(decimal));
            dt_NVLKEM.Columns.Add("SO_LUONG", typeof(int));
            dt_NVLKEM.Columns.Add("DONGIA", typeof(decimal));
            dt_NVLKEM.Columns.Add("THANHTIEN", typeof(decimal));
            dt_NVLKEM.Columns.Add("IS_SPKM", typeof(int));

            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {


                PHIEUYEUCAU_DV_ID = _IKho_Lam_Phieu_DVProvider.InsertorUpdateKho_Lam_Phieu_DV(MA_DVIQLY, dtDauphieu, User);
                if (((dtPhieu_Ctiet != null) && (dtPhieu_Ctiet.Rows.Count > 0)) && (PHIEUYEUCAU_DV_ID > 0))
                {
                    for (int i = 0; i < dtPhieu_Ctiet.Rows.Count; i++)
                    {
                        dtPhieu_Ctiet.Rows[i]["PHIEUYEUCAU_DV_ID"] = PHIEUYEUCAU_DV_ID;
                        PHIEUYEUCAU_DV_CTIET_ID = _IKho_Lam_Phieu_DVProvider.InsertorUpdateKho_Lam_Phieu_DV_CT(MA_DVIQLY, dtPhieu_Ctiet.Rows[i], User);
                        if (PHIEUYEUCAU_DV_CTIET_ID <= 0)
                        {
                            return null;
                        }

                        //begin hoapd thuc hien tinh toan NVL va san pham khuyen mai kem theo
                        if (dsDinhmuc_all != null && dsDinhmuc_all.Tables[0].Rows.Count > 0)
                        {
                            dr_dinhmuc = dsDinhmuc_all.Tables[0].Select("SANPHAM_ID=" + ConstCommon.NVL_NUM_LONG_NEW(dtPhieu_Ctiet.Rows[i]["SANPHAM_ID"]));
                            if (dr_dinhmuc.Length > 0)
                            {
                                if (dr_dinhmuc[0]["LOAI_DINHMUC"].ToString() == "TEM")
                                {
                                    //begin tinh NVL cho san pham
                                    dr_dinhmuc_nvlkem = dsDinhmuc_all.Tables[2].Select("DINHMUC_TEMTOA_ID=" + ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc[0]["DINHMUC_TEMTOA_ID"]));
                                    if (dr_dinhmuc_nvlkem.Length > 0)
                                    {
                                        for (int idm = 0; idm < dr_dinhmuc_nvlkem.Length; idm++)
                                        {

                                            DataRow newCustomersRow = dt_NVLKEM.NewRow();

                                            newCustomersRow["MA_ITEM_TYPE"] = dr_dinhmuc_nvlkem[idm]["MA_ITEM_TYPE"];
                                            newCustomersRow["SANPHAM_ID"] = dr_dinhmuc_nvlkem[idm]["SANPHAM_ID"];
                                            newCustomersRow["SO_LUONG"] =
                                                (ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc_nvlkem[idm]["SOLUONG"]) * ConstCommon.NVL_NUM_LONG_NEW(dtPhieu_Ctiet.Rows[i]["SO_LUONG_TONG"]))
                                                + ((ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc_nvlkem[idm]["SOLUONG"]) * ConstCommon.NVL_NUM_LONG_NEW(dtPhieu_Ctiet.Rows[i]["SO_LUONG_TONG"])) / 100) * ConstCommon.NVL_NUM_DECIMAL_NEW(dr_dinhmuc_nvlkem[idm]["TIEU_HAO"])
                                                ;
                                            newCustomersRow["DONGIA"] = dr_dinhmuc_nvlkem[idm]["GIANHAP"];
                                            newCustomersRow["THANHTIEN"] = ConstCommon.NVL_NUM_LONG_NEW(newCustomersRow["DONGIA"]) * ConstCommon.NVL_NUM_LONG_NEW(newCustomersRow["SO_LUONG"]);
                                            newCustomersRow["IS_SPKM"] = 0;
                                            dt_NVLKEM.Rows.Add(newCustomersRow);
                                        }
                                    }
                                    //end tinh NVL cho san pham
                                    //begin tinh NVL cho thung

                                    dr_dinhmuc_nvlkem = dsDinhmuc_all.Tables[3].Select("DINHMUC_TEMTOA_ID=" + ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc[0]["DINHMUC_TEMTOA_ID"]));
                                    if (dr_dinhmuc_nvlkem.Length > 0)
                                    {
                                        for (int idm = 0; idm < dr_dinhmuc_nvlkem.Length; idm++)
                                        {

                                            DataRow newCustomersRow = dt_NVLKEM.NewRow();

                                            newCustomersRow["MA_ITEM_TYPE"] = dr_dinhmuc_nvlkem[idm]["MA_ITEM_TYPE"];
                                            newCustomersRow["SANPHAM_ID"] = dr_dinhmuc_nvlkem[idm]["SANPHAM_ID"];
                                            newCustomersRow["SO_LUONG"] =
                                               ((ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc_nvlkem[idm]["SOLUONG"]) * ConstCommon.NVL_NUM_LONG_NEW(dtPhieu_Ctiet.Rows[i]["SO_LUONG_TONG"]))
                                                + ((ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc_nvlkem[idm]["SOLUONG"]) * ConstCommon.NVL_NUM_LONG_NEW(dtPhieu_Ctiet.Rows[i]["SO_LUONG_TONG"])) / 100) * ConstCommon.NVL_NUM_DECIMAL_NEW(dr_dinhmuc_nvlkem[idm]["TIEU_HAO"])
                                                ) / ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc[0]["SOLUONG_DONGGOI"]);
                                            newCustomersRow["DONGIA"] = dr_dinhmuc_nvlkem[idm]["GIANHAP"];
                                            newCustomersRow["THANHTIEN"] = ConstCommon.NVL_NUM_LONG_NEW(newCustomersRow["DONGIA"]) * ConstCommon.NVL_NUM_LONG_NEW(newCustomersRow["SO_LUONG"]);
                                            newCustomersRow["IS_SPKM"] = 0;
                                            dt_NVLKEM.Rows.Add(newCustomersRow);
                                        }
                                    }
                                    //end tinh NVL cho thung

                                }
                                else//loai dinh muc cho san pham dong goi
                                {
                                    //begin tinh NVL cho san phamdf 
                                    dr_dinhmuc_nvlkem = dsDinhmuc_all.Tables[2].Select("DINHMUC_TEMTOA_ID=" + ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc[0]["DINHMUC_TEMTOA_ID"]));
                                    if (dr_dinhmuc_nvlkem.Length > 0)
                                    {
                                        for (int idm = 0; idm < dr_dinhmuc_nvlkem.Length; idm++)
                                        {

                                            DataRow newCustomersRow = dt_NVLKEM.NewRow();

                                            newCustomersRow["MA_ITEM_TYPE"] = dr_dinhmuc_nvlkem[idm]["MA_ITEM_TYPE"];
                                            newCustomersRow["SANPHAM_ID"] = dr_dinhmuc_nvlkem[idm]["SANPHAM_ID"];
                                            newCustomersRow["SO_LUONG"] =
                                                (ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc_nvlkem[idm]["SOLUONG"]) * ConstCommon.NVL_NUM_LONG_NEW(dtPhieu_Ctiet.Rows[i]["SO_LUONG_TONG"]))
                                                + ((ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc_nvlkem[idm]["SOLUONG"]) * ConstCommon.NVL_NUM_LONG_NEW(dtPhieu_Ctiet.Rows[i]["SO_LUONG_TONG"])) / 100) * ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc_nvlkem[idm]["TIEU_HAO"])
                                                ;
                                            newCustomersRow["DONGIA"] = dr_dinhmuc_nvlkem[idm]["GIANHAP"];
                                            newCustomersRow["THANHTIEN"] = ConstCommon.NVL_NUM_LONG_NEW(newCustomersRow["DONGIA"]) * ConstCommon.NVL_NUM_LONG_NEW(newCustomersRow["SO_LUONG"]);
                                            newCustomersRow["IS_SPKM"] = 0;
                                            dt_NVLKEM.Rows.Add(newCustomersRow);
                                        }
                                    }
                                    //end tinh NVL cho san pham
                                    //begin tinh NVL cho thung

                                    dr_dinhmuc_nvlkem = dsDinhmuc_all.Tables[3].Select("DINHMUC_TEMTOA_ID=" + ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc[0]["DINHMUC_TEMTOA_ID"]));
                                    if (dr_dinhmuc_nvlkem.Length > 0)
                                    {
                                        for (int idm = 0; idm < dr_dinhmuc_nvlkem.Length; idm++)
                                        {

                                            DataRow newCustomersRow = dt_NVLKEM.NewRow();

                                            newCustomersRow["MA_ITEM_TYPE"] = dr_dinhmuc_nvlkem[idm]["MA_ITEM_TYPE"];
                                            newCustomersRow["SANPHAM_ID"] = dr_dinhmuc_nvlkem[idm]["SANPHAM_ID"];
                                            newCustomersRow["SO_LUONG"] =
                                               (ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc_nvlkem[idm]["SOLUONG"]) * ConstCommon.NVL_NUM_LONG_NEW(dtPhieu_Ctiet.Rows[i]["SO_LUONG_TONG"]))
                                                + ((ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc_nvlkem[idm]["SOLUONG"]) * ConstCommon.NVL_NUM_LONG_NEW(dtPhieu_Ctiet.Rows[i]["SO_LUONG_TONG"])) / 100) * ConstCommon.NVL_NUM_LONG_NEW(dr_dinhmuc_nvlkem[idm]["TIEU_HAO"])
                                                ;
                                            newCustomersRow["DONGIA"] = dr_dinhmuc_nvlkem[idm]["GIANHAP"];
                                            newCustomersRow["THANHTIEN"] = ConstCommon.NVL_NUM_LONG_NEW(newCustomersRow["DONGIA"]) * ConstCommon.NVL_NUM_LONG_NEW(newCustomersRow["SO_LUONG"]);
                                            newCustomersRow["IS_SPKM"] = 1;
                                            dt_NVLKEM.Rows.Add(newCustomersRow);
                                        }
                                    }
                                    //end tinh NVL cho thung

                                }
                            }
                        }
                        //end hoapd thuc hien tinh toan NVL va san pham khuyen mai kem theo

                        //begin groupby lai cac san pham ID giong nhau
                        if (dt_NVLKEM != null && dt_NVLKEM.Rows.Count > 0)
                        {
                            dt_NVLKEM = dt_NVLKEM.AsEnumerable()
                             .GroupBy(r => new { Col1 = r["MA_ITEM_TYPE"], Col2 = r["SANPHAM_ID"], Col3 = r["IS_SPKM"] })
                             .Select(g =>
                             {
                                 var row = dt_NVLKEM.NewRow();
                                 row["MA_ITEM_TYPE"] = g.Key.Col1;
                                 row["SANPHAM_ID"] = g.Key.Col2;
                                 row["IS_SPKM"] = g.Key.Col3;
                                 row["SO_LUONG"] = g.Sum(r => r.Field<int>("SO_LUONG"));
                                 row["DONGIA"] = g.Min(r => r.Field<decimal>("DONGIA"));
                                 row["THANHTIEN"] = g.Sum(r => r.Field<decimal>("THANHTIEN"));
                                 return row;

                             })
                             .CopyToDataTable();

                            //insert truoc khi insert thi xoa
                            int pD = _IKho_Lam_Phieu_DVProvider.KO_PHIEUYEUCAU_DV_CTIET_NVL_DELETE_NEW(MA_DVIQLY, PHIEUYEUCAU_DV_ID, User);
                            if (pD <= 0)
                            {
                                return null;
                            }

                            for (int j = 0; j < dt_NVLKEM.Rows.Count; j++)
                            {
                                pD = _IKho_Lam_Phieu_DVProvider.KO_PHIEUYEUCAU_DV_CTIET_NVL_INSERT_NEW(MA_DVIQLY, PHIEUYEUCAU_DV_ID, dt_NVLKEM.Rows[j]["MA_ITEM_TYPE"].ToString(), ConstCommon.NVL_NUM_LONG_NEW(dt_NVLKEM.Rows[j]["SANPHAM_ID"]), ConstCommon.NVL_NUM_NT_NEW(dt_NVLKEM.Rows[j]["SO_LUONG"]), ConstCommon.NVL_NUM_LONG_NEW(dt_NVLKEM.Rows[j]["DONGIA"]), ConstCommon.NVL_NUM_LONG_NEW(dt_NVLKEM.Rows[j]["THANHTIEN"]), ConstCommon.NVL_NUM_NT_NEW(dt_NVLKEM.Rows[j]["IS_SPKM"]), User);
                                if (pD <= 0)
                                {
                                    return null;
                                }

                            }
                        }
                        //end groupby lai cac san pham ID giong nhau

                    }
                }
                else
                {
                    return null;
                }
                trans.Complete();

            }
            ds_return = _IKho_Lam_Phieu_DVProvider.GetListKho_Lam_Phieu_DV_By_ID(MA_DVIQLY, PHIEUYEUCAU_DV_ID);
            return ds_return;
        }
        public int DeleteKho_Lam_Phieu_DV(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID, string ModifiledBy)
        {
            return _IKho_Lam_Phieu_DVProvider.DeleteKho_Lam_Phieu_DV(MA_DVIQLY, PHIEUYEUCAU_DV_ID, ModifiledBy);
        }
        public DataTable GetListKho_Lam_Phieu_DV(string MA_DVIQLY)
        {
            return _IKho_Lam_Phieu_DVProvider.GetListKho_Lam_Phieu_DV(MA_DVIQLY);
        }
        public DataSet GetListKho_Lam_Phieu_DV_By_ID(string MA_DVIQLY, int PHIEUYEUCAU_DV_ID)
        {
            return _IKho_Lam_Phieu_DVProvider.GetListKho_Lam_Phieu_DV_By_ID(MA_DVIQLY, PHIEUYEUCAU_DV_ID);
        }
        public DataTable GetData_Loai_DV(string MA_DVIQLY)
        {
            return _IKho_Lam_Phieu_DVProvider.GetData_Loai_DV(MA_DVIQLY);
        }
    }
}
