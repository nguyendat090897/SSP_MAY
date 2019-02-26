using DSofT.Warehouse.Business;
using DSofT.Warehouse.Log.UtilHelper;
using DSofT.Warehouse.Resources;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_dm_sua_poppup.xaml
    /// </summary>
    public partial class frm_phanquyen_quanlykho_popup : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        public frm_phanquyen_quanlykho_popup()
        {
            InitializeComponent();
            Initialize_Grid_PhanQuyen_QuanLyKho_popup();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void Initialize_Grid_PhanQuyen_QuanLyKho_popup()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.gridControl, this.grdViewPhanQuyen_QuanLyKho_popup, false);
                Column xColumn;

                xColumn = new Column("Chon", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_Chon"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MaKho", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_MaKho"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TenKho", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_TenKho"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                this.iGridHelper.Initialize();
                this.grdViewPhanQuyen_QuanLyKho_popup.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid_PhanQuyen_QuanLyKho_popup()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }
}
