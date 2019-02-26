using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSofT.Warehouse.Domain
{
    [Serializable]
    public class FilterPagingModel
    {
        public string KeySearch { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
