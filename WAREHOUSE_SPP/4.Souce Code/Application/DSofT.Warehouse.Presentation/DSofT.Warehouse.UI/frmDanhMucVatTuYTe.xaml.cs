using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows;
using System.Windows.Input;
using DSofT.Warehouse.Business;
using DSofT.Warehouse.Resources;
using DSofT.Warehouse.Log.UtilHelper;
using DSofT.Framework.UICore;
using DSofT.Framework.UIControl;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 23/01/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Danh muc vat tu y te
    /// </summary>
    public partial class frmDanhMucVatTuYTe : ControlBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        public frmDanhMucVatTuYTe()
        {
            InitializeComponent();
            Initialize_Grid();
          //  this.iDataSource = this.TableSchemaDefineBingding();
           // this.DataContext = this.iDataSource;
        }
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MaVatTu", typeof(string));
                xDicUser.Add("TenVatTu", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }
        
        /// <summary>
        /// Initialize_Grid
        /// </summary>
        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;
                

                xColumn = new Column("Id", "Id");
                xColumn.Visible = false;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MaVatTu", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaVatTu"]));
                xColumn.Width = 60;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                 xColumn = new Column("TenVatTu", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenVatTu"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NhomVatTu", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNhomVatTu"]));
                xColumn.Width = 60;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("DVT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDVT"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("QuyCach", Convert.ToString(UtilLanguage.ApplyLanguage()["lblQuyCach"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NSX", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNSX"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("NhaSX", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNhaSX"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                //xColumn = new Column("GiaMua", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaMua"]));
                //xColumn.Width = 50;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);


                //xColumn = new Column("GhiChu", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGhiChu"]));
                //xColumn.Width = 50;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                this.iGridHelper.Initialize();
                this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frmDanhMucVatTuYTe_popup", null);
                //LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // if (iDataSource.Rows.Count == 0) return;
                // DataRow iRowSelGb = ((DataRowView)this.grd.GetFocusedRow()).Row;
                // object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_dm_vat_tu_y_te_popup", iRowSelGb);
                // LoadData();
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frmDanhMucVatTuYTe_popup", null);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_MouseDoubleClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }
}
