using DSofT.Warehouse.Business;
using DSofT.Warehouse.Resources;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using System;
using System.Data;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_QuanLyNhomQuyen.xaml
    /// </summary>
    public partial class frm_UserRole : ControlBase
    {
        IUserTypeBussiness _UserType_Bussiness;
        GridHelper GridControlHelper = null;
        GridHelper GridControl_grcListRights = null;
        DataTable iGridDatasource = null;
        DataTable iGridDatasource_Right = null;
        public frm_UserRole()
        {
            InitializeComponent();
            _UserType_Bussiness = new UserType_Bussiness(string.Empty);
            InitGridControl_grdUserType();
            InitGridControl_grdMenu();
            Load_UserType();
        }
        /// <summary>
        /// Tạo các cột trong grid control chứa danh sách nhóm
        /// </summary>
        private void InitGridControl_grdUserType()
        {
            try
            {
                GridControlHelper = new GridHelper(frm_UserRole_grdUserType, tableView1, false);
                Column xColumn = new Column();

                xColumn = new Column("ID", "");
                xColumn.Width = 20;
                xColumn.Visible = false;
                this.GridControlHelper.Add(xColumn);

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 20;
                xColumn.Visible = true;
                xColumn.AllowSorting = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.GridControlHelper.Add(xColumn);

                xColumn = new Column("UserTypeCode", Convert.ToString(UtilLanguage.ApplyLanguage()["frmUserRole_TypeCode"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.AllowSorting = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.GridControlHelper.Add(xColumn);

                xColumn = new Column("UserTypeName", Convert.ToString(UtilLanguage.ApplyLanguage()["frmUserRole_UserTypeName"]));
                xColumn.Width = 70;
                xColumn.Visible = true;
                xColumn.AllowSorting = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.GridControlHelper.Add(xColumn);

                this.GridControlHelper.Initialize();
                this.tableView1.AutoWidth = true;
            }
            catch
            { }
        }

        /// <summary>
        /// Tạo các cột trong grid control chức danh sách menu
        /// </summary>
        private void InitGridControl_grdMenu()
        {
            GridControl_grcListRights = new GridHelper(frm_UserRole_grdMenu, tableView2, false);
            Column xColums = new Column();

            xColums = new Column("STATUS", "");
            xColums.ColumnType = ColumnControl.Checkbox;
            xColums.Width = 10;
            xColums.Visible = true;
            xColums.HeaderStyle = ColumnHeaderStyle.CheckBox;
            xColums.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
            xColums.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
            xColums.Binding = new Binding("STATUS") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
            this.GridControl_grcListRights.Add(xColums);


            xColums = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
            xColums.Width = 20;
            xColums.Visible = true;
            //xColums.AllowSorting = false;
            xColums.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
            xColums.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
            this.GridControl_grcListRights.Add(xColums);

            xColums = new Column("ID","");
            xColums.Width = 20;
            xColums.Visible = false;
            this.GridControl_grcListRights.Add(xColums);

            xColums = new Column("MENUCODE", Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuCode"]));
            xColums.Width = 50;
            xColums.Visible = true;
            xColums.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
            xColums.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
            this.GridControl_grcListRights.Add(xColums);

            xColums = new Column("MENUNAME", Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuName"]));
            xColums.Width = 60;
            xColums.Visible = true;
            xColums.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
            xColums.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
            this.GridControl_grcListRights.Add(xColums);

            this.GridControl_grcListRights.Initialize();
            tableView2.AutoWidth = true;
        }

        /// <summary>
        /// Lấy danh sách nhóm người dùng
        /// </summary>
        private void Load_UserType()
        {
            try
            {
                DataSet ds = _UserType_Bussiness.Load_Data();
                if (ds != null && ds.Tables.Count > 0)
                {
                    this.iGridDatasource = ds.Tables[0];
               //     frm_QuanLyNhomQuyen_grdUserType.ItemsSource = dt;
                    GridControlHelper.BindItemSource(this.iGridDatasource);
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách chức năng theo nhóm người dùng
        /// </summary>
        private void Load_Menu_Condition()
        {
            try {
                DataSet ds_MenuAll = _UserType_Bussiness.Load_Menu_Condition(UserTypeID);
                if(ds_MenuAll != null && ds_MenuAll.Tables.Count > 0)
                {
                    this.iGridDatasource_Right = ds_MenuAll.Tables[0];
                    GridControl_grcListRights.BindItemSource(iGridDatasource_Right);
                }
            }
            catch( Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message);
            }
        }

        int UserTypeID;
        private void frm_QuanLyNhomQuyen_grdUserType_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UserTypeID = Convert.ToInt32(((DataRowView)this.frm_UserRole_grdUserType.SelectedItem).Row["ID"]);
            String UserTypeName = Convert.ToString(((DataRowView)this.frm_UserRole_grdUserType.SelectedItem).Row["UserTypeName"]);
            Load_Menu_Condition();
        }


        private void frm_UserRole_btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataSet ListMenu = new DataSet();
                ListMenu.Tables.Add(iGridDatasource_Right.Copy());
                ListMenu.AcceptChanges();
                if (_UserType_Bussiness.Update_UserRole(UserTypeID, ListMenu))
                {
                    base.ShowMessage(MessageType.Information, UtilLanguage.ApplyLanguage()["Successfully"].ToString());
                }
            }
            catch (System.Exception)
            {
                base.ShowMessage(MessageType.Error, UtilLanguage.ApplyLanguage()["Fail"].ToString());
            }
        }

        private void frm_UserType_btnCancel_Click_1(object sender, RoutedEventArgs e)
        {
            Load_Menu_Condition();
        }
    }
}
