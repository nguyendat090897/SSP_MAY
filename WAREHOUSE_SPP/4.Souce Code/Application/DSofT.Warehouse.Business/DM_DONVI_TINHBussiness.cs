using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_DONVI_TINHBussiness
    {
        bool InsertorUpdateDM_DONVI_TINH(int result,DataTable ds);
        bool DeleteDM_DONVI_TINH(string pMA_DONVI_TINH, string pModifiedBy);
        DataTable GetListDM_DONVI_TINH();
        DataTable GetListDM_DONVI_TINH_GET_KEY(string pMA_DONVI_TINH);
        DataTable GetListDM_DONVI_TINH_COMBOBOX();
        DataTable GetListDM_SANPHAM_QUYDOI(string pMA_DVIQLY);
        bool InsertDM_SANPHAM_QUYDOI_DVT(DataTable dst, DataTable qd, DataTable dt, string pUser, string pDONVIQUANLY);
    }
    public class DM_DONVI_TINHBussiness : IDM_DONVI_TINHBussiness
    {
        IDM_DONVI_TINHProvider _DM_DONVI_TINHProvider;
        public DM_DONVI_TINHBussiness(string appId, string userId = "0")
        {
            _DM_DONVI_TINHProvider = new DM_DONVI_TINHProvider(appId, userId);
        }
        public bool InsertorUpdateDM_DONVI_TINH(int result,DataTable ds)
        {
            return _DM_DONVI_TINHProvider.InsertorUpdateDM_DONVI_TINH(result,ds);
        }
        public bool DeleteDM_DONVI_TINH(string pMA_DONVI_TINH, string pModifiedBy)
        {
            return _DM_DONVI_TINHProvider.DeleteDM_DONVI_TINH(pMA_DONVI_TINH, pModifiedBy);
        }
        public DataTable GetListDM_DONVI_TINH()
        {
            return _DM_DONVI_TINHProvider.GetListDM_DONVI_TINH();
        }
        public DataTable GetListDM_DONVI_TINH_GET_KEY(string pMA_DONVI_TINH)
        {
            return _DM_DONVI_TINHProvider.GetListDM_DONVI_TINH_GET_KEY(pMA_DONVI_TINH);
        }

        public DataTable GetListDM_DONVI_TINH_COMBOBOX()
        {
            return _DM_DONVI_TINHProvider.GetListDM_DONVI_TINH_COMBOBOX();
        }

        public bool InsertDM_SANPHAM_QUYDOI_DVT(DataTable dst, DataTable qd, DataTable dt ,string pUser, string pDONVIQUANLY)
        {
            bool result = false;
           
            if (((dst != null) && (dst.Rows.Count > 0)))
            {
                for (int i = 0; i < dst.Rows.Count; i++)
                {
                    bool flat = true;
                    for (int j = 0; j < qd.Rows.Count; j++)
                    {
                        if (dst.Rows[i]["SANPHAM_ID"].ToString() == qd.Rows[j]["SANPHAM_ID"].ToString()
                                && dst.Rows[i]["MA_ITEM_TYPE"].ToString() == qd.Rows[j]["MA_ITEM_TYPE"].ToString())
                        {
                            flat = false;
                            break;
                        }
                    }
                    if (flat == true)
                    {
                        result = _DM_DONVI_TINHProvider.InsertDM_SANPHAM_QUYDOIDVT(dst.Rows[i], dt, pUser, ConstCommon.DonViQuanLy);
                    }
                    else continue;

                }
            }
            return result;
        }
        public DataTable GetListDM_SANPHAM_QUYDOI(string pMA_DVIQLY)
        {
            return _DM_DONVI_TINHProvider.GetListDM_SANPHAM_QUYDOI(pMA_DVIQLY);
        }
    }
}