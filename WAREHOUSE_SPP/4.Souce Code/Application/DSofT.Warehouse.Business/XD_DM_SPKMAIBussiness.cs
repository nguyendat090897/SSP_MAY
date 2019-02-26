
using DSofT.Warehouse.Provider;
using System;
using System.Data;
using System.Transactions;

namespace DSofT.Warehouse.Business
{
    public interface IXD_DM_SPKMAIBussiness
    {

        DataSet InsertHT_DINHMUC_TEMTOA(DataTable dtDinhmuc, DataTable dtNVLThung, DataTable dtNVLSP);
        DataSet getHT_DINHMUC_TEMTOA_ALL(string pMA_DVIQLY);
        bool HT_DINHMUC_TEMTOA_DELETE_BYID(string pMA_DVIQLY, long pDINHMUC_TEMTOA_ID, string pUser);
        DataTable HT_DINHMUC_TEMTOA_NVL_THUNG_DELETE(string pMA_DVIQLY, long pDINHMUC_TEMTOA_NVL_THUNG_ID, string pUser);
        DataTable HT_DINHMUC_TEMTOA_NVL_SP_DELETE(string pMA_DVIQLY, long DINHMUC_TEMTOA_NVL_SP_ID, string pUser);
        DataTable GetListHT_DINHMUC_TEMTOA_ID(string pMA_DVIQLY);
        DataTable GetListHT_DINHMUC_TEMTOA_CHIPHI_DV_GET_TENDV(string pMA_DVIQLY);
        DataTable HT_DINHMUC_TEMTOA_CHIPHI_DV_GET_LIST_ALL(string pMA_DVIQLY, long pDINHMUC_TEMTOA_ID);
        bool InsertOrUpdateHT_DINHMUC_TEMTOA_CHIPHI_DV(DataTable dst);
        DataTable HT_DINHMUC_TEMTOA_CHIPHI_DV_DELETE(string pMA_DVIQLY, long pDINHMUC_TEMTOA_CHIPHI_DV_ID, string pUser);
        bool UpdateHT_DINHMUC_TEMTOA_CPDV(DataTable dst);
        bool HT_DINHMUC_TEMTOA_CHIPHI_DV_CHECKEXISTS(string pMA_DVIQLY, long pDINHMUC_TEMTOA_CHIPHI_DV_ID, long pDINHMUC_TEMTOA_ID, long pDICHVU_TEMTOA_ID);
        bool HT_DINHMUC_TEMTOA_CHECKEXISTS(string pMA_DVIQLY, long pDINHMUC_TEMTOA_ID, long pSANPHAM_ID);
    }
    public class XD_DM_SPKMAIBussiness : IXD_DM_SPKMAIBussiness
    {
        IXD_DM_SPKMAIProvider _XD_DM_SPKMAIProvider;
        public XD_DM_SPKMAIBussiness(string appId, string userId = "0")
        {
            _XD_DM_SPKMAIProvider = new XD_DM_SPKMAIProvider(appId, userId);
        }

        public DataSet InsertHT_DINHMUC_TEMTOA(DataTable dtDinhmuc, DataTable dtNVLThung, DataTable dtNVLSP)
        {
            DataSet ds_return = null;
            long pDINHMUC_TEMTOA_ID = 0;
            long pDINHMUC_TEMTOA_NVL_THUNG_ID = 0;
            long pDINHMUC_TEMTOA_NVL_SP_ID = 0;
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                             new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {


                pDINHMUC_TEMTOA_ID = _XD_DM_SPKMAIProvider.InsertHT_DINHMUC_TEMTOA(dtDinhmuc);
                if (((dtNVLThung != null) && (dtNVLThung.Rows.Count > 0)) && (pDINHMUC_TEMTOA_ID > 0))
                {
                    for (int i = 0; i < dtNVLThung.Rows.Count; i++)
                    {
                        dtNVLThung.Rows[i]["DINHMUC_TEMTOA_ID"] = pDINHMUC_TEMTOA_ID;
                        pDINHMUC_TEMTOA_NVL_THUNG_ID = _XD_DM_SPKMAIProvider.InsertHT_DINHMUC_TEMTOA_NVL_THUNG(dtNVLThung.Rows[i]);
                        if (pDINHMUC_TEMTOA_NVL_THUNG_ID <= 0)
                        {
                            return null;
                        }

                    }
                }
                if(((dtNVLSP != null) && (dtNVLSP.Rows.Count > 0)) && (pDINHMUC_TEMTOA_ID > 0))
                {
                    for (int i = 0; i < dtNVLSP.Rows.Count; i++)
                    {
                        dtNVLSP.Rows[i]["DINHMUC_TEMTOA_ID"] = pDINHMUC_TEMTOA_ID;
                        pDINHMUC_TEMTOA_NVL_SP_ID = _XD_DM_SPKMAIProvider.InsertHT_DINHMUC_TEMTOA_NVL_SP(dtNVLSP.Rows[i]);
                        if (pDINHMUC_TEMTOA_NVL_SP_ID <= 0)
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
            ds_return = _XD_DM_SPKMAIProvider.getHT_DINHMUC_TEMTOA(dtDinhmuc.Rows[0]["MA_DVIQLY"].ToString(), pDINHMUC_TEMTOA_ID);
            return ds_return;
        }

        public DataSet getHT_DINHMUC_TEMTOA_ALL(string pMA_DVIQLY)
        {
            return _XD_DM_SPKMAIProvider.getHT_DINHMUC_TEMTOA_ALL(pMA_DVIQLY);
        }

        public bool HT_DINHMUC_TEMTOA_DELETE_BYID(string pMA_DVIQLY, long pDINHMUC_TEMTOA_ID, string pUser)
        {
            return _XD_DM_SPKMAIProvider.HT_DINHMUC_TEMTOA_DELETE_BYID(pMA_DVIQLY, pDINHMUC_TEMTOA_ID, pUser);
        }
        public DataTable HT_DINHMUC_TEMTOA_NVL_THUNG_DELETE(string pMA_DVIQLY, long pDINHMUC_TEMTOA_NVL_THUNG_ID, string pUser)
        {
            return _XD_DM_SPKMAIProvider.HT_DINHMUC_TEMTOA_NVL_THUNG_DELETE(pMA_DVIQLY,pDINHMUC_TEMTOA_NVL_THUNG_ID,pUser);
        }
        public DataTable HT_DINHMUC_TEMTOA_NVL_SP_DELETE(string pMA_DVIQLY, long pDINHMUC_TEMTOA_NVL_SP_ID, string pUser)
        {
            return _XD_DM_SPKMAIProvider.HT_DINHMUC_TEMTOA_NVL_SP_DELETE(pMA_DVIQLY, pDINHMUC_TEMTOA_NVL_SP_ID,pUser);
        }
        public DataTable GetListHT_DINHMUC_TEMTOA_ID(string pMA_DVIQLY)
        {
            return _XD_DM_SPKMAIProvider.GetListHT_DINHMUC_TEMTOA_ID(pMA_DVIQLY);
        }
        public DataTable GetListHT_DINHMUC_TEMTOA_CHIPHI_DV_GET_TENDV(string pMA_DVIQLY)
        {
            return _XD_DM_SPKMAIProvider.GetListHT_DINHMUC_TEMTOA_CHIPHI_DV_GET_TENDV(pMA_DVIQLY);
        }
        public DataTable HT_DINHMUC_TEMTOA_CHIPHI_DV_GET_LIST_ALL(string pMA_DVIQLY, long pDINHMUC_TEMTOA_ID)
        {
            return _XD_DM_SPKMAIProvider.HT_DINHMUC_TEMTOA_CHIPHI_DV_GET_LIST_ALL(pMA_DVIQLY, pDINHMUC_TEMTOA_ID);
        }
        public bool InsertOrUpdateHT_DINHMUC_TEMTOA_CHIPHI_DV(DataTable dst)
        {
            return _XD_DM_SPKMAIProvider.InsertOrUpdateHT_DINHMUC_TEMTOA_CHIPHI_DV(dst);
        }
        public DataTable HT_DINHMUC_TEMTOA_CHIPHI_DV_DELETE(string pMA_DVIQLY, long pDINHMUC_TEMTOA_CHIPHI_DV_ID, string pUser)
        {
            return _XD_DM_SPKMAIProvider.HT_DINHMUC_TEMTOA_CHIPHI_DV_DELETE(pMA_DVIQLY, pDINHMUC_TEMTOA_CHIPHI_DV_ID, pUser);
        }
        public bool UpdateHT_DINHMUC_TEMTOA_CPDV(DataTable dst)
        {
            return _XD_DM_SPKMAIProvider.UpdateHT_DINHMUC_TEMTOA_CPDV(dst);
        }

        public bool HT_DINHMUC_TEMTOA_CHIPHI_DV_CHECKEXISTS(string pMA_DVIQLY, long pDINHMUC_TEMTOA_CHIPHI_DV_ID, long pDINHMUC_TEMTOA_ID, long pDICHVU_TEMTOA_ID)
        {
            return _XD_DM_SPKMAIProvider.HT_DINHMUC_TEMTOA_CHIPHI_DV_CHECKEXISTS(pMA_DVIQLY, pDINHMUC_TEMTOA_CHIPHI_DV_ID, pDINHMUC_TEMTOA_ID, pDICHVU_TEMTOA_ID);
        }
        public bool HT_DINHMUC_TEMTOA_CHECKEXISTS(string pMA_DVIQLY, long pDINHMUC_TEMTOA_ID, long pSANPHAM_ID)
        {
            return _XD_DM_SPKMAIProvider.HT_DINHMUC_TEMTOA_CHECKEXISTS(pMA_DVIQLY, pDINHMUC_TEMTOA_ID, pSANPHAM_ID);
        }
    }
}
