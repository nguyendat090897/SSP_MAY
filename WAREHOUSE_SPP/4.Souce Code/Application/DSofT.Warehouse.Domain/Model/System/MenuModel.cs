using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSofT.Warehouse.Domain.Model.System
{
    //U_MenuModel
    public class MenuFilterModel : MenuDBModel
    {
        public string UserId { get; set; }
    }
    public class MenuDBModel
    {
        public MenuDBModel() {
            ListMenuChildren = new List<MenuDBModel>();
        }
        public List<MenuDBModel> ListMenuChildren { get; set; }
        public int ID { get; set; }
        public string MenuCode { get; set; }
        public string MenuName { get; set; }
        public string MenuNamespaceClass { get; set; }
        public string MenuClassName { get; set; }
        public string MenuRemark { get; set; }
        public string MenuIcon { get; set; }
        public int MenuParentID { get; set; }
        public bool IsDelete { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
