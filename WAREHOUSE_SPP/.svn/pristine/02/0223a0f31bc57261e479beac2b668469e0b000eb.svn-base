using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DSofT.Warehouse.Resources
{
    public class UtilLanguage
    {
        public static ResourceDictionary ApplyLanguage(string cultureName = null)
        {
            var regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\DSofT\\ControlUnion\\Warehouse\\Application");

            if (cultureName != null)
            {
                regKey.SetValue("Language", cultureName);
            }

            ResourceDictionary dict = new ResourceDictionary();

            switch (regKey.GetValue("Language").ToString())//Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "vi-VN":
                    dict.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/DSofT.Warehouse.Resources;component/Vietnamese.xaml") });
                    break;
                case "en-US":
                    dict.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/DSofT.Warehouse.Resources;component/English.xaml") });
                    break;
                // ...
                default:
                    dict.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/DSofT.Warehouse.Resources;component/English.xaml") });
                    break;
            }
            return dict;
        }
    }
}
