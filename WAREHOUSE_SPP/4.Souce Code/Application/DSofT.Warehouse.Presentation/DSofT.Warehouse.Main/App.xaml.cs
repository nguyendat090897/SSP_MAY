using DevExpress.Xpf.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DSofT.Warehouse.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void SettingDefaultTheme()
        {
            Theme theme = new Theme("Theme1", "DevExpress.Xpf.Themes.Theme1.v14.2");
            theme.AssemblyName = "DevExpress.Xpf.Themes.Theme1.v14.2";
            Theme.RegisterTheme(theme);
            ThemeManager.ApplicationThemeName = "Theme1";
        }

        public ResourceDictionary Resource
        {
            // You could probably get it via its name with some query logic as well.
            get { return Resources.MergedDictionaries[0]; }
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SettingDefaultTheme();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");

            ResourceDictionary dict = new ResourceDictionary();
            //ResourceDictionary dict1 = new ResourceDictionary();
            //dict1.Source = new Uri("pack://application:,,,/DSofT.Warehouse.Resources;component/Vietnamese.xaml");
            //ResourceDictionary dict2 = new ResourceDictionary();
            //dict2.Source = new Uri("pack://application:,,,/DSofT.Warehouse.Resources;component/English.xaml");

            //dict.MergedDictionaries.Add(dict1);
            //dict.MergedDictionaries.Add(dict2);
            ////begin doan sua lai noi dung cua file moi truong
            //string a = System.IO.Directory.GetCurrentDirectory();
            //string rel = string.Empty;
            //try
            //{

            //    //File.WriteAllText(a + @"\logfileerror.txt", "ex.Message");
            //    //string str = File.ReadAllText(a + @"\EnvironmentSettings.xml");
            //    //rel = a.Replace("Deploy", "");
            //    //str = str.Replace(@"D:\PROJECT\SVN\DV.UTC2.TMS\DV.UCT2.TMS\DownloadAssemblies\", rel + @"DownloadAssemblies\");
            //    ////str = str.Replace(@"D:\PROJECT\SVN\DV.UTC2.TMS\DV.UCT2.TMS\DownloadAssemblies\", a + @"\DownloadAssemblies\");
            //    //File.WriteAllText(a + @"\EnvironmentSettings.xml", str);
            //}
            //catch (Exception ex)
            //{
            //    File.WriteAllText(a + @"\logfileerror.txt", ex.Message);
            //}

            //end doan sua lai noi dung cua file moi truong

            var regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\DSofT\\ControlUnion\\Warehouse\\Application");

            if (regKey.GetValue("Language") == null)
                regKey.SetValue("Language", "");

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
            this.Resources.MergedDictionaries.Add(dict);

        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string a = System.IO.Directory.GetCurrentDirectory();
            File.WriteAllText(a + @"\logfileerror.txt", e.Exception.ToString());
        }
    }
}
