using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors.Settings;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Threading;
using DevExpress.Xpf.Ribbon;
//using DevExpress.Xpf.Core;


using DSofT.Warehouse.Business;
using DSofT.Warehouse.Domain.Model.System;
using DevExpress.Xpf.Docking;
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

namespace DSofT.Warehouse.Main.Menu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Menu_ribbonCopy : ControlBase
    {

        public List<MenuDBModel> _ComboboxMenu;
        public List<MenuDBModel> _AllMenu;
        ISystemBussiness _systemBussiness;
        public Menu_ribbonCopy()
        {
            InitializeComponent();
            _systemBussiness = new SystemBussiness(string.Empty);
            _AllMenu = _systemBussiness.GetListMenuSystem(new MenuFilterModel() { UserId = CommonDataHelper.UserId });

        }




        FormattedText fmtText = null;
        FormattedText createFormattedText(FontFamily fontFamily)
        {
            return new FormattedText("Aa", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(fontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal), 18, Brushes.Black, null, TextFormattingMode.Ideal);
        }

        ImageSource CreateImage(FontFamily fontFamily)
        {
            const double DimensionSize = 32;
            const double HalfDimensionSize = DimensionSize / 2d;
            DrawingVisual v = new DrawingVisual();
            DrawingContext c = v.RenderOpen();
            c.DrawRectangle(Brushes.White, null, new Rect(0, 0, DimensionSize, DimensionSize));
            if (fmtText == null)
                fmtText = createFormattedText(fontFamily);
            fmtText.SetFontFamily(fontFamily);
            fmtText.TextAlignment = TextAlignment.Center;
            double verticalOffset = (DimensionSize - fmtText.Baseline) / 2d;
            c.DrawText(fmtText, new Point(HalfDimensionSize, verticalOffset));
            c.Close();

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)DimensionSize, (int)DimensionSize, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(v);
            return rtb;
        }

        GalleryItem CreateItem(FontFamily fontFamily, ImageSource image)
        {
            GalleryItem item = new GalleryItem();
            item.Glyph = image;
            item.Caption = fontFamily.ToString();
            item.Tag = fontFamily;
            return item;
        }

        void textEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ShowHideSelectionCategory();
            InvokeUpdateFormat();
        }

        bool isInvokePending = false;

        void InvokeUpdateFormat()
        {
            if (!isInvokePending)
            {
               // Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(UpdateFormat));
                isInvokePending = true;
            }
            UpdateFormat();
        }

        protected void UpdateFormat()
        {
        }

        void ShowHideSelectionCategory()
        {
           
        }


    

        void FontFamilyGallery_ItemChecked(object sender, GalleryItemEventArgs e)
        {
            FontFamily newFontFamily = (FontFamily)e.Item.Tag;
            ApplyPropertyValueToSelectedText(TextElement.FontFamilyProperty, newFontFamily);
        }

        void eFontSize_EditValueChanged(object sender, RoutedEventArgs e)
        {
            ApplyPropertyValueToSelectedText(TextElement.FontSizeProperty, eFontSize.EditValue);
        }

        void ApplyPropertyValueToSelectedText(DependencyProperty formattingProperty, object value)
        {
            if (value == null) return;
            //InvokeUpdateFormat();
            //InvokeFocusEdit();
        }

        void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            (RibbonControl.ApplicationMenu as ApplicationMenu).ClosePopup();
        }
        void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            (RibbonControl.ApplicationMenu as ApplicationMenu).ClosePopup();
        }



        void groupEdit_CaptionButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("DevExpress Ribbon Control", "Edit Settings Dialog");
        }

        private void bAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageBox.Show("DevExpress Ribbon Control", "About Window");
        }

        private void groupFile_CaptionButtonClick(object sender, RibbonCaptionButtonClickEventArgs e)
        {
            MessageBox.Show("DevExpress Ribbon Control", "File Settings Dialog");

        }

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
                    xMenu = xMenu.Where(m => m.MenuParentID == ParentId.TryParseToInt() || ParentId == "0" || ParentId == "*").ToList();
                }
                var xMenuChild = _AllMenu.Where(m => m.MenuParentID != 0).Select(m => m.MenuParentID).ToList();
                xMenu = xMenu.Where(m => !xMenuChild.Contains(m.ID)).ToList();

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




        private void frmUser_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmUser"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmUser.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }

        }

        private void frm_UserRole_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_UserRole"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_UserRole.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frm_UserType_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_UserType"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_UserType.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmThietLapTonKhoTT_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmThietLapTonKhoTT"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmThietLapTonKhoTT.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmThietLapQuyTacHangBanCham_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmThietLapQuyTacHangBanCham"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmThietLapQuyTacHangBanCham.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmThietLapCanhBaoHSDThuoc_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmThietLapCanhBaoHSDThuoc"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmThietLapCanhBaoHSDThuoc.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmKhoiTaoTonkho_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmKhoiTaoTonkho"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmKhoiTaoTonkho.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frm_quytac_makytudau_sanpham_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_quytac_makytudau_sanpham"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_quytac_makytudau_sanpham.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frm_XDDinhMuc_BoSung_TTTViet_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_XDDinhMuc_BoSung_TTTViet"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_XDDinhMuc_BoSung_TTTViet.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frm_XDDinhMuc_ChoSP_KhuyenMai_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_XDDinhMuc_ChoSP_KhuyenMai"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_XDDinhMuc_ChoSP_KhuyenMai.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frm_ThamSo_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_ThamSo"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_ThamSo.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmNhapKho_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmNhapKho"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmNhapKho.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frm_lapphieu_yeucau_xuatkho_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_lapphieu_yeucau_xuatkho"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_lapphieu_yeucau_xuatkho.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmDieuChuyenNoiBo_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmDieuChuyenNoiBo"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDieuChuyenNoiBo.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmDCNBTaiKhoNhan_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmDCNBTaiKhoNhan"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDCNBTaiKhoNhan.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmLapPhieuYeuCauNhapKho_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmLapPhieuYeuCauNhapKho"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDCNBTaiKhoNhan.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmPhanCongThietBi_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                

                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmPhanCongThietBi"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmPhanCongThietBi.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frm_XuatKho_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_XuatKho"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_XuatKho.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmDSPLDV_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmDSPLDV"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDSPLDV.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmDSSPDongGoi_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmDSSPDongGoi"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDSSPDongGoi.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmLapYeuCauDichVu_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmLapYeuCauDichVu"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmLapYeuCauDichVu.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmPhieuNhapVatTuDichVu_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmPhieuNhapVatTuDichVu"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmPhieuNhapVatTuDichVu.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmPhieuNhapSPDV_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmPhieuNhapSPDV"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmPhieuNhapSPDV.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frm_NhapLaiVatTuDaXuatRa_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_NhapLaiVatTuDaXuatRa"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_NhapLaiVatTuDaXuatRa.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frm_lap_phieu_xuat_kho_vattu_dich_vu_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_lap_phieu_xuat_kho_vattu_dich_vu"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_lap_phieu_xuat_kho_vattu_dich_vu.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmDSBTDPXuat_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmDSBTDPXuat"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDSBTDPXuat.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmLapPhieuXacNhanHangHuHong_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmLapPhieuXacNhanHangHuHong"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmLapPhieuXacNhanHangHuHong.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmCanhBaoHangBanCham_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmCanhBaoHangBanCham"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmCanhBaoHangBanCham.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frmCanhBaoHSDThuoc_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmCanhBaoHSDThuoc"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmCanhBaoHSDThuoc.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmLapPhieuXuatTuPYC_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmLapPhieuXuatTuPYC"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmLapPhieuXuatTuPYC.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frm_dm_pallet_dieu_chuyen_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_dm_pallet_dieu_chuyen"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_dm_pallet_dieu_chuyen.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmKhachHangHopDong_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmKhachHangHopDong"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmKhachHangHopDong.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmKiemKeHangHoa_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmKiemKeHangHoa"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmKiemKeHangHoa.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmQuanLyThu_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmQuanLyThu"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmQuanLyThu.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmQuanLyChi_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmQuanLyChi"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmQuanLyChi.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frm_dm_pallet_cap_nhat_trang_thai_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_dm_pallet_cap_nhat_trang_thai"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_dm_pallet_cap_nhat_trang_thai.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frm_PhieuXuat_DieuChinh_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_PhieuXuat_DieuChinh"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_PhieuXuat_DieuChinh.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmDMKHNCC_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmDMKHNCC"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDMKHNCC.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmDMKho_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmDMKho"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDMKho.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmThietBi_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmThietBi"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmThietBi.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmDVT_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmDVT"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDVT.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmPallet_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmPallet"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmPallet.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmDMTTBYT_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmDMTTBYT"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDMTTBYT.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmQuocGia_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmQuocGia"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmQuocGia.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmNhaSX_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmNhaSX"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmNhaSX.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmDanhMucSanPham_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmDanhMucSanPham"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDanhMucSanPham.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmDanhSachLoaiPallet_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmDanhSachLoaiPallet"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDanhSachLoaiPallet.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

        private void frm_dm_LoaiSanPham_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_dm_LoaiSanPham "
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_dm_LoaiSanPham.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frm_dm_LoaiKichThuoc_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_dm_LoaiKichThuoc"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_dm_LoaiKichThuoc.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frm_DanhMucLoaiKho_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_DanhMucLoaiKho"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_DanhMucLoaiKho.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frm_dm_nhom_pallet_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_dm_nhom_pallet"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_dm_nhom_pallet.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frmDanhMucNhomSanPham_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frmDanhMucNhomSanPham"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frmDanhMucNhomSanPham.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }
        private void frm_dm_Tinh_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var mnuModel = (from p in _AllMenu
                                where p.MenuCode == "frm_dm_Tinh"
                                select p);

                if (mnuModel.Count() > 0)
                {
                    this.CallTabMenu(mnuModel.ElementAt(0));

                }
                else
                {
                    frm_dm_Tinh.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Information, ex.Message);
            }
        }

}



public class RecentItem
    {
        public int Number { get; set; }
        public string FileName { get; set; }
    }

    public class ButtonWithImageContent
    {
        public string ImageSource { get; set; }
        public object Content { get; set; }
    }

    public class FontSizes
    {
        public double[] Items
        {
            get
            {
                return new double[] {
            3.0, 4.0, 5.0, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5,
            10.0, 10.5, 11.0, 11.5, 12.0, 12.5, 13.0, 13.5, 14.0, 15.0,
            16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0,
            32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0, 76.0,
            80.0, 88.0, 96.0, 104.0, 112.0, 120.0, 128.0, 136.0, 144.0
            };
            }
        }
    }

    public class DecimatedFontFamilies : FontFamilies
    {
        const int DecimationFactor = 5;

        public override ObservableCollection<FontFamily> Items
        {
            get
            {
                ObservableCollection<FontFamily> res = new ObservableCollection<FontFamily>();
                for (int i = 0; i < ItemsCore.Count; i++)
                {
                    if (i % DecimationFactor == 0)
                        res.Add(ItemsCore[i]);
                }
                return res;
            }
        }
    }

    public class FontFamilies
    {
        static ObservableCollection<FontFamily> items;
        protected static ObservableCollection<FontFamily> ItemsCore
        {
            get
            {
                if (items == null)
                {
                    items = new ObservableCollection<FontFamily>();
                    foreach (FontFamily fam in Fonts.SystemFontFamilies)
                    {
                        if (!IsValidFamily(fam)) continue;
                        items.Add(fam);
                    }
                }
                return items;
            }
        }
        public static bool IsValidFamily(FontFamily fam)
        {
            foreach (Typeface f in fam.GetTypefaces())
            {
                GlyphTypeface g = null;
                try
                {
                    if (f.TryGetGlyphTypeface(out g))
                        if (g.Symbol) return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
        public virtual ObservableCollection<FontFamily> Items
        {
            get
            {
                ObservableCollection<FontFamily> res = new ObservableCollection<FontFamily>();
                foreach (FontFamily fm in ItemsCore)
                {
                    res.Add(fm);
                }
                return res;
            }
        }
    }
}
