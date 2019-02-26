using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DSofT.Warehouse.Main
{
    public class MenuModel
    {
        public string title;
        public string color;
        public string module;
        public string dynamicContent;
        public string fixedContentType;
        public string icon;
        public string key;
        public string className;
        public string tileSize;
    }

    public class MenuParent
    {
        public string title;
        public string key;
        public MenuModel menumodel;
    }
}
