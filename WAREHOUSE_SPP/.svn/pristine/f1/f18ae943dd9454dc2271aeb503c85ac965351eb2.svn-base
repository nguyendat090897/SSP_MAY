using DSofT.Warehouse.Business;
using DSofT.Warehouse.Resources;
using DevExpress.Xpf.Grid;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using DSofT.Warehouse.Log.UtilHelper;
using DSofT.Framework.UICore.TaskProxy;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_dm_sua.xaml
    /// </summary>
    public partial class frm_phanquyen_quanlykho : ControlBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;

        public frm_phanquyen_quanlykho()
        {
            InitializeComponent();
            Initialize_Grid_PhanQuyen_QuanLyKho();
        }
        /// <summary>
        /// Initialize_Grid_Sua
        /// </summary>
        private void Initialize_Grid_PhanQuyen_QuanLyKho()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.gridControl, this.grdViewPhanQuyen_QuanLyKho, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("User", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_User"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TenUser", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_TenUser"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("KhoQuanLy", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_KhoQuanLy"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);
               
                this.iGridHelper.Initialize();
                this.grdViewPhanQuyen_QuanLyKho.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid_PhanQuyen_QuanLyKho()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// btnNew_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_phanquyen_quanlykho_popup", null);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {

                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadData()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void grdView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //if (iDataSource.Rows.Count == 0) return;
                //DataRow iRowSelGb = ((DataRowView)this.grd.GetFocusedRow()).Row;
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_phanquyen_quanlykho_popup", null);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_MouseDoubleClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }
}
