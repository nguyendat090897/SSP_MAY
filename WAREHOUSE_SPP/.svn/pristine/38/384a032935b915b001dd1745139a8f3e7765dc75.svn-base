using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSofT.Warehouse.Provider;
using System.Data;

namespace DSofT.Warehouse.Business
{
    public interface I_DM_THIETBI_BUSSINESS
    {
        DataTable GetListDM_THIETBI();
        bool InsertorUpdateDM_THIETBI(DataTable ds);
        bool DeleteDM_THIETBI(string MaTB, string pModifiedBy);
        DataTable GetListDM_THIETBI_GET_KEY(string pMA_THIETBI);
        DataTable GetListDM_THIETBI_QG(string MaDVQL);
        DataTable GetListDM_THIETBI_NSX(string MaDVQL);
    }

    public class DM_THIETBI_BUSSINESS : I_DM_THIETBI_BUSSINESS
    {
        I_DM_ThietBi_Provider _DM_ThietBi_Provider;
        public DM_THIETBI_BUSSINESS(string appId, string userId = "0")
        {
            _DM_ThietBi_Provider = new DM_ThietBi_Provider(appId, userId = "0");
        }
        public DataTable GetListDM_THIETBI()
        {
            return _DM_ThietBi_Provider.GetListDM_THIETBI();
        }
        public bool InsertorUpdateDM_THIETBI(DataTable ds)
        {
            return _DM_ThietBi_Provider.InsertorUpdateDM_THIETBI(ds);
        }
        public DataTable GetListDM_THIETBI_GET_KEY(string pMA_THIETBI)
        {
            return _DM_ThietBi_Provider.GetListDM_THIETBI_GET_KEY(pMA_THIETBI);
        }
        public bool DeleteDM_THIETBI(string MaTB, string pModifiedBy)
        {
            return _DM_ThietBi_Provider.DeleteDM_THIETBI(MaTB, pModifiedBy);
        }
        public DataTable GetListDM_THIETBI_QG(string MaDVQL)
        {
            return _DM_ThietBi_Provider.GetListDM_THIETBI_QG(MaDVQL);
        }
        public DataTable GetListDM_THIETBI_NSX(string MaDVQL)
        {
            return _DM_ThietBi_Provider.GetListDM_THIETBI_NSX(MaDVQL);
        }
    }
}
