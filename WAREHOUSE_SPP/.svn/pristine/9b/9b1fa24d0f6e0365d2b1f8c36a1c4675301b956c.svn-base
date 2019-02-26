using DSofT.Warehouse.Business;
using DSofT.Warehouse.Resources;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for popup_Menu.xaml
    /// </summary>
    public partial class popup_Menu : PopupBase
    {
        DataTable iDataSource = null;
        DataTable iGridDataSource = null;
        GridHelper iGridHelperMenu = null;
        DataRow iRowSelGb = null;
        DataRowView dr;
        IMenuBussiness _menuBussiness;
        bool iFlgSave=false;
        
        public popup_Menu()
        {
            InitializeComponent();
            _menuBussiness = new MenuBussiness(string.Empty);
            
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            loadDataForCombobox(); 
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

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                dr = this.Parameter[0] as DataRowView;
                Dispalay_request();
            }
            else
            {

                iFlgSave = true;
            }
        }
        public void Dispalay_request()
        {
            try
            {
                foreach (DataColumn item in this.dr.DataView.Table.Columns)
                {
                    if (this.iDataSource.Columns[item.ColumnName] != null)
                    {
                        this.iDataSource.Rows[0][item.ColumnName] = dr[item.ColumnName];
                    }
                }
               
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
        private void loadDataForCombobox()
        {
            try
            {
              
                DataSet ds = _menuBussiness.GetAllMenuParent();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    this.iGridDataSource = ds.Tables[0];
                    ComboBoxUtil.SetComboBoxEdit(this.cboMenuParent, "MenuName", "MenuParentID", iGridDataSource, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(this.cboMenuParent, "*", "0", 0,true);

                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private bool Validate()
        {
            try
            {
                if (string.IsNullOrEmpty(this.iDataSource.Rows[0]["MenuCode"].ToString()))
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuCode"]));
                    this.txtMenuCode.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(this.iDataSource.Rows[0]["MenuName"].ToString()))
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuName"]));
                    this.txtMenuName.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(this.iDataSource.Rows[0]["MenuNamespaceClass"].ToString()))
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuClassNamespace"]));
                    this.txtMenuClassNamespace.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(this.iDataSource.Rows[0]["MenuIcon"].ToString()))
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuIcon"]));
                    this.txtMenuIcon.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(this.iDataSource.Rows[0]["MenuRemark"].ToString()))
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuRemark"]));
                    this.txtMenuRemark.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(this.iDataSource.Rows[0]["MenuClassName"].ToString()))
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuClassName"]));
                    this.txtMenuClassName.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(this.iDataSource.Rows[0]["MenuParentID"].ToString()))
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuParent"]));
                    this.cboMenuParent.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertMenuInDB()
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(iDataSource.Copy());
                bool result = _menuBussiness.InsertMenu(ds);
                if (result==true)
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]));
                }
                else
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
         
        }
        public void UpdateMenuInDB()
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                DataSet ds = new DataSet();
                ds.Tables.Add(iDataSource.Copy());
                bool result = _menuBussiness.UpdateMenu(ds);
                if (result == true)
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]));
                }
                else
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

        }
        private void ResetDefaultInputControl()
        {
            this.iDataSource = this.TableSchemaDefineBingding();
            this.iFlgSave = true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
              try
            {
                //if (Validate())
                //{
                    if (iFlgSave)
                       
                    {
                        InsertMenuInDB();
                        //this.Close();
                    }
                    else
                    {
                        UpdateMenuInDB();
                        //this.Close();
                    } 
              //}
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                this.Close();
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

        private void cboMenuParent_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {

        }

        private void cboMenuParent_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

        }
    }
}
