using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface IDM_QUOCGIABussiness
    {
        bool InsertorUpdateDM_QUOCGIA(DataTable dst);
        bool DeleteDM_QUOCGIA(string pMA_DVIQLY, long pQUOCGIA_ID, string pModifiedBy);
        DataTable GetListDM_QUOCGIA(string pMA_DVIQLY);
        DataTable GetListDM_QUOCGIA_KEY(string pMA_DVIQLY, string pMA_QUOCGIA);

    }
    public class DM_QUOCGIABussiness : IDM_QUOCGIABussiness
    {
        IDM_QUOCGIAProvider _DM_QUOCGIAProvider;
        public DM_QUOCGIABussiness(string appId, string userId = "0")
        {
            _DM_QUOCGIAProvider = new DM_QUOCGIAProvider(appId, userId);
        }
        public bool InsertorUpdateDM_QUOCGIA(DataTable dst)
        {
            return _DM_QUOCGIAProvider.InsertorUpdateDM_QUOCGIA(dst);
        }

        public bool DeleteDM_QUOCGIA(string pMA_DVIQLY, long pQUOCGIA_ID, string pModifiedBy)
        {
            return _DM_QUOCGIAProvider.DeleteDM_QUOCGIA(pMA_DVIQLY, pQUOCGIA_ID, pModifiedBy);
        }
        public DataTable GetListDM_QUOCGIA(string pMA_DVIQLY)
        {
            return _DM_QUOCGIAProvider.GetListDM_QUOCGIA(pMA_DVIQLY);
        }
        public DataTable GetListDM_QUOCGIA_KEY(string pMA_DVIQLY, string pMA_QUOCGIA)
        {
            return _DM_QUOCGIAProvider.GetListDM_QUOCGIA_KEY(pMA_DVIQLY, pMA_QUOCGIA);
        }
    }
}