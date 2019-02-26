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

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for dm_donvitinh.xaml
    /// </summary>
    public partial class frm_Menu : ControlBase
    {
        TreeListControlHelper iTreeListControlHelper = null;
        DataTable iGridDataSource = null;
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        IMenuBussiness _menuBussiness;

        public frm_Menu()
        {
            InitializeComponent();
            Initialize_Grid_Menu();
            _menuBussiness = new MenuBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadData();

        }


        private void Initialize_Grid_Menu()
        {
            try
            {

                this.iTreeListControlHelper = new TreeListControlHelper(treeListX, iTreeListView, false);
                Column xColumn;

                xColumn = new Column("ID", "ID");
                xColumn.Visible = false;
                this.iTreeListControlHelper.Add(xColumn);

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_Menu_popup_stt"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iTreeListControlHelper.Add(xColumn);

                xColumn = new Column("MenuCode", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_Menu_popup_code"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iTreeListControlHelper.Add(xColumn);

                xColumn = new Column("ParentID", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_Menu_popup_parent"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iTreeListControlHelper.Add(xColumn);

                xColumn = new Column("MenuParentID", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_Menu_popup_parent"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iTreeListControlHelper.Add(xColumn);


                xColumn = new Column("MenuName", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_Menu_popup_name"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iTreeListControlHelper.Add(xColumn);

                xColumn = new Column("MenuIcon", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_Menu_popup_icon"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iTreeListControlHelper.Add(xColumn);

                xColumn = new Column("MenuClassName", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_Menu_popup_classname"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iTreeListControlHelper.Add(xColumn);

                this.iTreeListControlHelper.Initialize();
                this.iTreeListView.ExpandAllNodes();
                this.iTreeListView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("ID", typeof(int));
                xDicUser.Add("MenuCode", typeof(string));
                xDicUser.Add("MenuName", typeof(string));
                xDicUser.Add("MenuNamespaceClass", typeof(string));
                xDicUser.Add("MenuClassName", typeof(string));
                xDicUser.Add("MenuRemark", typeof(string));
                xDicUser.Add("MenuIcon", typeof(string));
                xDicUser.Add("MenuParentID", typeof(int));
                xDicUser.Add("ParentID", typeof(string));
                xDicUser.Add("IsDelete", typeof(int));
                xDicUser.Add("CreateBy", typeof(int));
                xDicUser.Add("CreateDate", typeof(string));
                xDicUser.Add("ModifiedBy", typeof(int));
                xDicUser.Add("ModifiedDate", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }
        private void LoadData()
        {
            try
            {
                DataSet ds = _menuBussiness.GetAllMenu();
                if (ds != null && ds.Tables.Count > 0)
                {
                    this.iGridDataSource = ds.Tables[0];
                    this.iTreeListControlHelper.BindItemSource(iGridDataSource);
                    this.iTreeListView.ExpandAllNodes();
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "popup_Menu", null);
                LoadData();
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (iTreeListView.Nodes.Count == 0) return;
                DataRow xDr = ((DataRowView)iTreeListView.GetNodeByRowHandle(iTreeListView.FocusedRowHandle).Content).Row;
                bool b = base.ShowMessage(MessageType.OkCancel, UtilLanguage.ApplyLanguage()["Confirm"].ToString());
                if (b)
                {
                    bool result = _menuBussiness.DeleteMenu(xDr["ID"].ToString());
                    if (result == true)
                    {
                        base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frm_Menu_ThongBaoXoaThanhCong"]));
                        LoadData();

                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frm_Menu_ThongBaoXoaKhongThanhCong"]));
                    }
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void iTreeListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //Mouse.OverrideCursor = Cursors.Wait;
                //TableViewHitInfo hi = ((TableView)this.iTreeListView).CalcHitInfo(e.OriginalSource as DependencyObject);
                //if (hi.HitTest.ToString() == "RowCell")
                //{
                //    string id = Convert.ToString(((DataRowView)this.grd.SelectedItem).Row["ID"]);
                //    DataRowView drv = this.grdView.FocusedRow as DataRowView;
                //    object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "popup_Menu", drv);
                //    LoadData();
                //}


                Mouse.OverrideCursor = Cursors.Wait;

                string id = Convert.ToString(((DataRowView)this.treeListX.SelectedItem).Row["ID"]);
                DataRowView drv = ((DataRowView)iTreeListView.GetNodeByRowHandle(iTreeListView.FocusedRowHandle).Content);
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "popup_Menu", drv);
                LoadData();

            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;

            }
        }
    }

}
