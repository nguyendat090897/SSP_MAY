using DSofT.FoodWarehouse.Business;
using DSofT.FoodWarehouse.Domain.Model.System;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.NavBar;
using DSofT.Framework.Environments;
using DSofT.Framework.Helpers;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml;

namespace DSofT.FoodWarehouse.UIMenu
{
    /// <summary>
    /// Interaction logic for Menu_Navgation.xaml
    /// </summary>
    public partial class Menu_Navgation : ControlBase
    {
        ISystemBussiness _systemBussiness;
        private const string DEFAULT_MENU_ICON = "Images/Menu/";
        string strSearch = string.Empty;
        RegistryKey regKey = null;
        public List<MenuDBModel> _ComboboxMenu;
        public List<MenuDBModel> _AllMenu;
        public List<MenuDBModel> _MenuFilter;
        public MenuDBModel mnuGlobal;

        private static string CST_APP_BASE_PATH = EnvironmentSettings.CST_APP_BASE_PATH;

        //private string strimage = AppHelpers.GetAppSetting("AppBasePath");/* + "Images/Menu/clt_ico_administer.png";*/

        public Menu_Navgation()
        {
            InitializeComponent();
            _systemBussiness = new SystemBussiness(string.Empty);
            regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\DSofT\\MENUOP");
            string valueDefault = regKey.GetValue("MENU") == null ? "" : regKey.GetValue("MENU").ToString();
            string valueDefaultFind = regKey.GetValue("FIND") == null ? "" : regKey.GetValue("FIND").ToString();
            if (valueDefault == "")
            {
                regKey.SetValue("MENU", "AA");
            }
            if (valueDefaultFind == "")
            {
                regKey.SetValue("FIND", "T1D");
            }

            valueDefaultFind = regKey.GetValue("FIND") == null ? "" : regKey.GetValue("FIND").ToString();
            if (valueDefaultFind == "T1D")
            {
                rdoTuyetDoi.IsChecked = true;
            }
            else
            {
                rdoTuongDoi.IsChecked = true;
            }
            _AllMenu = _systemBussiness.GetListMenuSystem(new MenuFilterModel() { UserId = CommonDataHelper.IdUser });
            foreach (var itemP in _AllMenu)
            {
                itemP.MenuIcon = CST_APP_BASE_PATH + DEFAULT_MENU_ICON + itemP.MenuIcon;
            }
            LoadComboboxMenu();
            LoadMenu("*", "");
        }

        public override void InitializeControls()
        {
            base.InitializeControls();
        }

        private void LoadComboboxMenu()
        {
            imgSearch.Source = new BitmapImage(new Uri(CST_APP_BASE_PATH + DEFAULT_MENU_ICON + "search.png", UriKind.Absolute));
            //ChangeStyleMenu();
            LoadDataInComboboxMenu();
            lstSearch.ItemsSource = _ComboboxMenu;
            lstSearch.DisplayMember = "MenuName";
            lstSearch.ValueMember = "ID";
            lstSearch.SelectedIndex = 0;
        }
        private void LoadMenu(string ParentId, string textSearch)
        {
            _MenuFilter = LoadDataMenuInList(ParentId, textSearch);
            lstTopMenuList.ItemsSource = _MenuFilter;
            CallTabMenu(mnuGlobal);
        }

        //void navView_GroupAdding(object sender, DevExpress.Xpf.NavBar.GroupAddingEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Group == null)
        //            return;

        //        Menu lv2 = e.Group.DataContext as Menu;

        //        if (_topLevelMenu.Exists(x => x.ID == lv2.ParentID))
        //        {
        //            e.Group.Header = lv2.Text;
        //            e.Group.Style = Application.Current.FindResource("Dx.NavBar.Lbl") as Style;
        //            e.Group.ImageSettings = new ImageSettings() { Height = 16, Width = 16 };

        //            if (lv2.Children != null)
        //            {
        //                //this.InitTreeList_Lv3(e.Group, lv2.Children);
        //            }
        //            else if (isFromFile == false)
        //            {
        //                List<Menu> childMenu = new List<Menu>();
        //            }
        //            e.Group.Click += (clSender, clArgs) =>
        //            {
        //                NavBarGroup nbg = clSender as NavBarGroup;
        //                this.CallTabMenu(nbg.DataContext as Menu);
        //            };

        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        private MenuDBModel CallTabMenu(MenuDBModel xMenu)
        {
            if (xMenu != null)
            {
                if (xMenu.MenuNamespaceClass != "" && xMenu.MenuClassName != "")
                {
                    DocumentPanel xPanel = UIAgent.LoadUIControl(xMenu.MenuNamespaceClass, xMenu.MenuClassName, xMenu.MenuName);
                    xPanel.Description = xMenu.ID.ToString();

                    VisualEffects xVisualEffects = new VisualEffects();
                    xVisualEffects.Zoom((System.Windows.Controls.UserControl)xPanel.Content);

                    Stack<string> stk = new Stack<string>(5);
                    string allPath = string.Empty;
                    string pathMark = " > ";

                    do
                    {
                        stk.Push(xMenu.MenuName);

                        xMenu = _AllMenu.Find(t => t.ID == xMenu.MenuParentID);

                    } while (xMenu != null);

                    foreach (string loc in stk)
                    {
                        allPath += loc + pathMark;
                    }

                    allPath = allPath.Remove(allPath.LastIndexOf(pathMark), pathMark.Length);
                    xPanel.ToolTip = allPath;
                }
            }
            return xMenu;

        }

        private void LoadDataInComboboxMenu()
        {
            _ComboboxMenu = new List<MenuDBModel>();
            List<MenuDBModel> ListMenu = new List<MenuDBModel>();
            try
            {
                var allMenu = _AllMenu;
                _ComboboxMenu = allMenu.Where(n => n.MenuParentID == 0 || n.MenuParentID.IsNull()).ToList();
                _ComboboxMenu.Insert(0, new MenuDBModel() { ID = 0, MenuName = "*" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<MenuDBModel> LoadDataMenuInList(string ParentId, string textSearch)
        {
            List<MenuDBModel> xMenu = new List<MenuDBModel>();

            try
            {
                xMenu = _AllMenu;

                if (ParentId.IsNotNullAndNotEmpty())
                {
                    xMenu = xMenu.Where(m => m.MenuParentID == ParentId.TryParseToInt()).ToList();
                }

                if (textSearch.IsNotNullAndNotEmpty())
                {
                    xMenu = xMenu.Where(m => m.MenuName.UnicodeToPlain().ToUpper().Contains(textSearch.UnicodeToPlain())).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xMenu;
        }

        private void lstTopMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            var menuItem = (MenuDBModel)e.AddedItems[0];
            try
            {
                this.CallTabMenu(menuItem as MenuDBModel);
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void navView_ActiveGroupChanged(object sender, DevExpress.Xpf.NavBar.NavBarActiveGroupChangedEventArgs e)
        {

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstSearch_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            LoadMenu(this.lstSearch.EditValue.ToString(), "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTietHoc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (rdoTuyetDoi.IsChecked == true)
            {
                LoadMenu(this.lstSearch.EditValue.ToString(), this.txtTietHoc.Text.ToUpper() + e.Text.ToUpper());
            }
            else
            {
                LoadMenu(this.lstSearch.EditValue.ToString(), e.Text.ToUpper());
            }
        }

        #region VietnameseSigns
        private readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"

        };


        /// <summary>
        /// Remove Sign Vietnamese String
        /// </summary>
        /// <param name="str"> string</param>
        /// <returns> string </returns>
        public string RemoveSignVietnameseString(string str)
        {
            try
            {
                for (int i = 1; i < VietnameseSigns.Length; i++)
                {
                    for (int j = 0; j < VietnameseSigns[i].Length; j++)
                        str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
                }
                return str;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTietHoc_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (this.txtTietHoc.Text == "")
            {
               LoadMenu(this.lstSearch.EditValue.ToString(), this.txtTietHoc.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoTuyetDoi_Checked(object sender, RoutedEventArgs e)
        {
            this.txtTietHoc.Text = "";
            System.Windows.Controls.RadioButton rdo = sender as System.Windows.Controls.RadioButton;
            if (rdo.Name == "rdotuyetdoi")
            {
                regKey.SetValue("FIND", "T1D");
            }
            else
            {
                regKey.SetValue("FIND", "T2D");
            }
        }


        /// <summary>
        /// Click menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstTopMenuList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            var menuItem = (MenuDBModel)e.AddedItems[0];
            try
            {
                this.CallTabMenu(menuItem as MenuDBModel);
                lstTopMenuList.SelectedItem = null;
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        //private void btnMenu_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (FlgChangeMenu == false)
        //    {
        //        regKey.SetValue("MENU", "AA");
        //    }
        //    else
        //    {
        //        regKey.SetValue("MENU", "BB");
        //    }
        //    //ChangeStyleMenu();
        //}

        //public void ChangeStyleMenu()
        //{
        //    string valueDefault = regKey.GetValue("MENU") == null ? "" : regKey.GetValue("MENU").ToString();
        //    if (valueDefault == "AA")
        //    {
        //        //this.btnMenu.Source = new BitmapImage(new Uri(EnvironmentSettings.CST_APP_BASE_PATH + "/Images/menu2.png", UriKind.Absolute));
        //        FlgChangeMenu = true;
        //        //GrdMenu.Visibility = Visibility.Visible;
        //        GrdMenuList.Visibility = Visibility.Hidden;

        //    }
        //    else
        //    {
        //        this.btnMenu.Source = new BitmapImage(new Uri(EnvironmentSettings.CST_APP_BASE_PATH + "/Images/menu1.png", UriKind.Absolute));
        //        FlgChangeMenu = false;
        //        GrdMenuList.Visibility = Visibility.Visible;
        //        //GrdMenu.Visibility = Visibility.Hidden;

        //    }
        //}
    }
}
