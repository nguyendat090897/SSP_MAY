
using DSofT.Warehouse.Provider;
using System.Data;
using System.Transactions;
namespace DSofT.Warehouse.Business
{
    public interface IDM_PALLET_DIEUCHUYEN_CTIETBussiness
    {
        bool UpdateDM_PALLET_DIEUCHUYEN_SO_LUONG(long pPALLET_DIEUCHUYEN_ID, string pMA_DVQLY, int pSO_LUONG_CHUYEN);
        bool UpdateDM_PHIEU_PALLET_DIEU_CHUYEN(DataTable dst, string pUser, string pDONVIQUANLY);
        bool DeleteDM_PALLET_DIEUCHUYEN_CTIET(string pMA_DVIQLY, long PALLET_DIEUCHUYEN_CTIET_ID, string pModifiedBy);
        DataTable GetListDM_PALLET_DIEUCHUYEN_CTIET(string pMA_DVIQLY, long pPALLET_DIEUCHUYEN_ID);
        DataTable GetListDM_PALLET_DIEUCHUYEN_DONVIQUANLY();
        DataTable GetDataBySO_PHIEU(string pMA_DVIQLY, string pSO_PHIEU);
        DataSet InsertDM_PALLET_DIEUCHUYEN(DataTable dtDIEUCHUYEN, DataTable dtDIEUCHUYENCTIET, string pUser, string pDONVIQUANLY);
        DataSet GetListDM_PALLET_DIEU_CHUYEN_CTIET(string pMA_DVIQLY, long pPALLET_DIEUCHUYEN_ID);
    }
    public class DM_PALLET_DIEUCHUYEN_CTIETBussiness : IDM_PALLET_DIEUCHUYEN_CTIETBussiness
    {
        IDM_PALLET_DIEUCHUYEN_CTIETProvider _DM_PALLET_DIEUCHUYEN_CTIETProvider;
        public DM_PALLET_DIEUCHUYEN_CTIETBussiness(string appId, string userId = "0")
        {
            _DM_PALLET_DIEUCHUYEN_CTIETProvider = new DM_PALLET_DIEUCHUYEN_CTIETProvider(appId, userId);
        }
        public bool UpdateDM_PALLET_DIEUCHUYEN_SO_LUONG(long pPALLET_DIEUCHUYEN_ID, string pMA_DVQLY, int pSO_LUONG_CHUYEN)
        {
            return _DM_PALLET_DIEUCHUYEN_CTIETProvider.UpdateDM_PALLET_DIEUCHUYEN_SO_LUONG(pPALLET_DIEUCHUYEN_ID, pMA_DVQLY, pSO_LUONG_CHUYEN);
        }
        public bool UpdateDM_PHIEU_PALLET_DIEU_CHUYEN(DataTable dst, string pUser, string pDONVIQUANLY)
        {
            return _DM_PALLET_DIEUCHUYEN_CTIETProvider.UpdateDM_PHIEU_PALLET_DIEU_CHUYEN(dst, pUser,
             pDONVIQUANLY);
        }
        public bool DeleteDM_PALLET_DIEUCHUYEN_CTIET(string pMA_DVIQLY, long PALLET_DIEUCHUYEN_CTIET_ID, string pModifiedBy)
        {
            return _DM_PALLET_DIEUCHUYEN_CTIETProvider.DeleteDM_PALLET_DIEUCHUYEN_CTIET(pMA_DVIQLY, PALLET_DIEUCHUYEN_CTIET_ID, pModifiedBy);
        }
        public DataTable GetListDM_PALLET_DIEUCHUYEN_CTIET(string pMA_DVIQLY, long pPALLET_DIEUCHUYEN_ID)
        {
            return _DM_PALLET_DIEUCHUYEN_CTIETProvider.GetListDM_PALLET_DIEUCHUYEN_CTIET(pMA_DVIQLY, pPALLET_DIEUCHUYEN_ID);
        }
        public DataTable GetDataBySO_PHIEU(string pMA_DVIQLY, string pSO_PHIEU)
        {
            return _DM_PALLET_DIEUCHUYEN_CTIETProvider.GetDataBySO_PHIEU(pMA_DVIQLY, pSO_PHIEU);
        }
        public DataTable GetListDM_PALLET_DIEUCHUYEN_DONVIQUANLY()
        {
            return _DM_PALLET_DIEUCHUYEN_CTIETProvider.GetListDM_PALLET_DIEUCHUYEN_DONVIQUANLY();
        }
        public DataSet InsertDM_PALLET_DIEUCHUYEN(DataTable dtDIEUCHUYEN, DataTable dtDIEUCHUYENCTIET, string pUser, string pDONVIQUANLY)
        {

            DataSet ds_return = null;
            long pPALLET_DIEUCHUYEN_ID = 0;
            long pPALLET_DIEUCHUYEN_CTIET_ID = 0;
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {


                pPALLET_DIEUCHUYEN_ID = _DM_PALLET_DIEUCHUYEN_CTIETProvider.InsertDM_PALLET_DIEUCHUYEN(dtDIEUCHUYEN, pUser, pDONVIQUANLY);
                if (((dtDIEUCHUYENCTIET != null) && (dtDIEUCHUYENCTIET.Rows.Count > 0)) && (pPALLET_DIEUCHUYEN_ID > 0))
                {
                    for (int i = 0; i < dtDIEUCHUYENCTIET.Rows.Count; i++)
                    {
                        dtDIEUCHUYENCTIET.Rows[i]["PALLET_DIEUCHUYEN_ID"] = pPALLET_DIEUCHUYEN_ID;
                        pPALLET_DIEUCHUYEN_CTIET_ID = _DM_PALLET_DIEUCHUYEN_CTIETProvider.InsertDM_PALLET_DIEUCHUYEN_CTIET(dtDIEUCHUYENCTIET.Rows[i], pUser, pDONVIQUANLY);
                        if (pPALLET_DIEUCHUYEN_CTIET_ID <= 0)
                        {
                            return null;
                        }

                    }
                }
                else
                {
                    return null;
                }
                trans.Complete();

            }
            ds_return = _DM_PALLET_DIEUCHUYEN_CTIETProvider.getListDM_PALLET_DIEUCHUYEN(dtDIEUCHUYEN.Rows[0]["MA_DVIQLY"].ToString(), pPALLET_DIEUCHUYEN_ID);
            return ds_return;
        }
        public DataSet GetListDM_PALLET_DIEU_CHUYEN_CTIET(string pMA_DVIQLY, long pPALLET_DIEUCHUYEN_ID)
        {
            DataSet ds_return = null;
            ds_return = _DM_PALLET_DIEUCHUYEN_CTIETProvider.getListDM_PALLET_DIEUCHUYEN(pMA_DVIQLY, pPALLET_DIEUCHUYEN_ID);
            return ds_return;
        }
    }
}
