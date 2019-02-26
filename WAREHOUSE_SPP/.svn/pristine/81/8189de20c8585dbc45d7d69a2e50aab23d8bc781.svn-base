using DSofT.Warehouse.Business;
using DSofT.Warehouse.Domain.Model;
using DSofT.Warehouse.Resources;
using DSofT.Framework.Helpers;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;



namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frmUser.xaml
    /// </summary>
    public partial class frmUser : ControlBase
    {
        GridHelper helper = null;
        DataTable iDataSource = null;
        IUserManagerBussiness _UserManagerBussiness = null;
        ISystemBussiness _systemBussiness = null;
        int IsSave = 0;
        string currentPassword = "";
        public frmUser()
        {
            InitializeComponent();
            _systemBussiness = new SystemBussiness(string.Empty);
            _UserManagerBussiness = new UserManagerBussiness();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            Initialize_Grid_Menu();
            LoadDataUser();
            LoadDataUserType();
            on_off_control();
        }
        public void LoadDataUser()
        {
            try
            {
                DataSet ds = _UserManagerBussiness.GetListUser();
                if (ds != null && ds.Tables.Count > 0)
                {
                    var dt = ds.Tables[0];
                    grdMenu.ItemsSource = dt;

                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message);
            }

        }
        

        public void LoadDataUserType()
        {
            try
            {
                DataSet ds = _UserManagerBussiness.GetListUserType();
                if (ds != null && ds.Tables.Count > 0)
                {
                    var dt = ds.Tables[0];
                    ComboBoxUtil.SetComboBoxEdit(this.cboGroup, "UserTypeName", "ID", dt, SelectionTypeEnum.Native);
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (grdMenu.SelectedItem != null)
            {
                DataRowView drw = (DataRowView)grdMenu.SelectedItem;
                var dr = drw.Row;
                UserModel user = new UserModel();
                user.USER_ID = int.Parse(dr[1].ToString());
                bool r = base.ShowMessage(MessageType.OkCancel, Convert.ToString(UtilLanguage.ApplyLanguage()["frmNguoiDung_tblYouWantDelete"]));
                if (r)
                {
                    try
                    {
                        if (_UserManagerBussiness.DeleteUser(user))
                        {
                            base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmNguoiDung_tblDeleteSuccess"]));
                            LoadDataUser();
                        }
                    }
                    catch (Exception ex)
                    {

                        base.ShowMessage(MessageType.Error, ex.Message);
                    }
                }
            }

        }
        /// <summary>
        /// Load
        /// </summary>
        private void Initialize_Grid_Menu()
        {
            try
            {
                this.helper = new GridHelper(grdMenu, grdViewMenu, false);
                Column xColumn;
                xColumn = new Column("ID", "ID");
                xColumn.Visible = false;
                this.helper.Add(xColumn);


                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.helper.Add(xColumn);

                xColumn = new Column("AreaID", "AreaID");
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.helper.Add(xColumn);

                xColumn = new Column("UserGroupID", "UserGroupID");
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.helper.Add(xColumn);


                xColumn = new Column("UserName", Convert.ToString(UtilLanguage.ApplyLanguage()["frmUser_UserName"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.helper.Add(xColumn);

                xColumn = new Column("FullName", Convert.ToString(UtilLanguage.ApplyLanguage()["frmUser_FullName"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.helper.Add(xColumn);

                xColumn = new Column("UserTypeName", Convert.ToString(UtilLanguage.ApplyLanguage()["frmUser_GroupUser"]));
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.helper.Add(xColumn);

                this.helper.Initialize();
                this.grdViewMenu.AutoWidth = true;
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
                xDicUser.Add("UserName", typeof(string));
                xDicUser.Add("Password", typeof(string));
                xDicUser.Add("FullName", typeof(string));
                xDicUser.Add("AreaName", typeof(string));
                xDicUser.Add("AreaID", typeof(int));
                xDicUser.Add("UserGroupID", typeof(int));
                xDicUser.Add("ChoPhepNguoiSuDung", typeof(bool));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }
        /// <summary>
        /// Code xử lý cho button thêm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            txtTenDangNhap.IsEnabled = true;
            txtMatKhau.IsEnabled = true;
            this.iDataSource.Rows[0]["UserName"] = string.Empty;
            this.iDataSource.Rows[0]["Password"] = string.Empty;
            this.iDataSource.Rows[0]["FullName"] = string.Empty;
            this.iDataSource.Rows[0]["AreaID"] = 0;
            this.iDataSource.Rows[0]["UserGroupID"] = 0;
            this.iDataSource.Rows[0]["ChoPhepNguoiSuDung"] = false;
            txtTenDangNhap.Focus();
            IsSave = 1;
        }
        /// <summary>
        /// Error rỗng
        /// </summary>
        /// <returns></returns>
        public bool validate()
        {

            if (string.IsNullOrEmpty(this.iDataSource.Rows[0]["UserName"].ToString()))
            {
                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmUser_UserNameIsNotEmpty"]));
                this.txtTenDangNhap.Focus();
                return false;
            }


            if (string.IsNullOrEmpty(this.iDataSource.Rows[0]["Password"].ToString()))
            {
                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmUser_PasswordIsNotEmpty"]));
                this.txtMatKhau.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.iDataSource.Rows[0]["FullName"].ToString()))
            {
                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmUser_FullNameIsNotEmpty"]));
                this.txtTenNguoiSuDung.Focus();
                return false;
            }
            if (this.iDataSource.Rows[0]["UserGroupID"].ToString() == "0")
            {
                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmUser_GroupUserNotEmpty"]));
                this.cboGroup.Focus();
                return false;
            }
            if (txtTenDangNhap.IsEnabled)
            {
                this.txtTenDangNhap.Focus();
                string username = iDataSource.Rows[0]["UserName"].ToString();
                //this.txtMatKhau.Focus();
                if (username != string.Empty)
                {
                    try
                    {
                        UserModel user = new UserModel();
                        user.USER_NAME = username;
                        if (_UserManagerBussiness.CheckUserName(user))
                        {
                            base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmUser_UserNameAvailable"]));
                            this.txtTenDangNhap.Focus();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {

                        base.ShowMessage(MessageType.Error, ex.Message);
                    }

                }

            }
            return true;
        }
        /// <summary>
        /// code xử lý cho button lưu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                if (IsSave == 1)
                {
                    Them();
                    txtTenDangNhap.IsEnabled = false;
                }
                else
                {
                    Sua();
                }
            }
        }
        /// <summary>
        /// code sử lý cho button thêm
        /// </summary>
        public void Them()
        {
            UserModel user = new UserModel();
            user.USER_NAME = this.iDataSource.Rows[0]["UserName"].ToString();
            user.FULL_NAME = this.iDataSource.Rows[0]["FullName"].ToString();
            user.PASSWORD = this.iDataSource.Rows[0]["Password"].ToString().EncryptPassword();
            user.AreaID = int.Parse(this.iDataSource.Rows[0]["AreaID"].ToString());
            user.UserTypeID = int.Parse(this.iDataSource.Rows[0]["UserGroupID"].ToString());
            user.IS_DELETE = false;
            try
            {
                if (_UserManagerBussiness.InsertUser(user))
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]));
                    LoadDataUser();
                }
                else
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message);
            }
        }
        /// <summary>
        /// code xử lý cho button sửa
        /// </summary>
        public void Sua()
        {
            if (grdMenu.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)grdMenu.SelectedItem;
                var row = drv.Row;
                if (validate())
                {
                    UserModel user = new UserModel();
                    user.USER_ID = int.Parse(row["ID"].ToString());
                    user.AreaID = int.Parse(this.iDataSource.Rows[0]["AreaID"].ToString());
                    user.UserTypeID = int.Parse(this.iDataSource.Rows[0]["UserGroupID"].ToString());
                    user.USER_NAME = this.iDataSource.Rows[0]["UserName"].ToString();
                    user.FULL_NAME = this.iDataSource.Rows[0]["FullName"].ToString();

                    try
                    {
                        if (this.currentPassword.IsNotNullAndNotEmpty())
                            if (this.currentPassword != this.iDataSource.Rows[0]["Password"].ToString())
                            {
                                user.PASSWORD = this.iDataSource.Rows[0]["Password"].ToString().EncryptPassword();
                            }
                            else
                            {
                                user.PASSWORD = this.currentPassword;
                            }

                        if (_UserManagerBussiness.UpdateUser(user))
                        {
                            base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]));
                            LoadDataUser();
                        }
                    }
                    catch (Exception ex)
                    {

                        base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                    }


                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(this.iDataSource.Rows[0]["Password"].ToString());
        }

        private void grdMenu_CurrentItemChanged(object sender, DevExpress.Xpf.Grid.CurrentItemChangedEventArgs e)
        {


        }
        public void on_off_control()
        {
            txtTenDangNhap.IsEnabled = false;
            txtMatKhau.IsEnabled = true;
        }
        /// <summary>
        /// Button refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Refresh();

        }
        public void Refresh()
        {
            txtTenDangNhap.IsEnabled = false;
            txtMatKhau.IsEnabled = true;
            LoadDataUser();
            IsSave = 0;
            txtTenDangNhap.Focus();
            this.iDataSource.Rows[0]["UserName"] = string.Empty;
            this.iDataSource.Rows[0]["FullName"] = string.Empty;
            this.iDataSource.Rows[0]["Password"] = string.Empty;
            this.iDataSource.Rows[0]["AreaID"] = 0;
            this.iDataSource.Rows[0]["UserGroupID"] = 0;
        }

        private void grdMenu_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (grdMenu.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)grdMenu.SelectedItem;
                var row = drv.Row;
                foreach (DataColumn item in iDataSource.Columns)
                {
                    if (drv.DataView.Table.Columns.Contains(item.ColumnName))
                    {
                        this.iDataSource.Rows[0][item.ColumnName] = row[item.ColumnName];
                    }
                }

                this.currentPassword = this.iDataSource.Rows[0]["Password"].ToString();

                txtTenDangNhap.IsEnabled = false;
                IsSave = 0;
            }
            else
            {
                this.currentPassword = string.Empty;
            }
        }
        /// <summary>
        /// code kiểm tra username có trùng không
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTenDangNhap_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {


        }

    }
}
