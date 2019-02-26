using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DSofT.Warehouse.Domain
{
    [Serializable]
    public class InfoFileModel
    {
        public string UploadMessage { get; set; }

        public bool IsSuccess { get; set; }

        public string FileUrl { get; set; }

        public string FileName { get; set; }

        public long Length { get; set; }

        public string FileType { get; set; }
    }
}
