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
    /// Interaction logic for frm_dm_Tinh_popup.xaml
    /// </summary>
    public partial class frm_dm_Tinh_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataTable iGridDataSource = null;
        IDM_TINHBussiness _DM_TINHBussiness;
        DataRow iRowSelGb = null;
        string pMA_TINH = String.Empty;
        public frm_dm_Tinh_popup()
        {
            InitializeComponent();
            _DM_TINHBussiness = new DM_TINHBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            UpdateTinhTrang();

        }

        private DataTable GetIDataSource()
        {
            return this.iDataSource;
        }

        public void DispalayRequest()
        {
            try
            {
                foreach (DataColumn item in this.iRowSelGb.Table.Columns)
                {
                    if (this.iDataSource.Columns[item.ColumnName] != null)
                    {
                        this.iDataSource.Rows[0][item.ColumnName] = iRowSelGb[item.ColumnName];
                    }
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "DispalayRequest()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    pMA_TINH = this.iDataSource.Rows[0]["MA_TINH"].ToString().Trim();
                }
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
                xDicUser.Add("TINH_ID", typeof(decimal));
                xDicUser.Add("MA_TINH", typeof(string));
                xDicUser.Add("TEN_TINH", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(bool));
                xDicUser.Add("RowVersion", typeof(long));
                xDicUser.Add("IsDelete", typeof(bool));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));
                xDicUser.Add("TINH_TRANG_HT", typeof(String));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }

        private bool CheckSymbolInMaPallet(string str)
        {
            if (str == "")
            {
                return false;
            }
            char[] varChar = str.ToCharArray();
            int i = 0;
            while (i < varChar.Length &&
                ((Convert.ToInt32(varChar[i]) >= 97 && Convert.ToInt32(varChar[i]) <= 122) || (Convert.ToInt32(varChar[i]) >= 65 && Convert.ToInt32(varChar[i]) <= 90)
                || (Convert.ToInt32(varChar[i]) >= 48 && Convert.ToInt32(varChar[i]) <= 57)))
            {
                i++;
            }
            if (i < varChar.Length)
            {
                return false;
            }
            return true;
        }

        private void SetIsNull()
        {
            this.iDataSource.Rows[0]["MA_TINH"] = string.Empty;
            this.iDataSource.Rows[0]["TEN_TINH"] = string.Empty;
            this.iDataSource.Rows[0]["GHI_CHU"] = string.Empty;
            this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            frm_dm_Tinh.status = true;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SetIsNull();
                txtMaTinh.Focus();
                rdbKSD.IsChecked = true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private void KTTinhTrang()
        {
            if (rdbSD.IsChecked == true)
                this.iDataSource.Rows[0]["TINH_TRANG"] = 1;
            else
                this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
        }
        private void UpdateTinhTrang()
        {
            try
            {
                if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString().Trim() != null)
                {
                    if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString().Trim() == "1")
                        rdbSD.IsChecked = true;
                    else
                        rdbKSD.IsChecked = true;
                }
                else
                    rdbKSD.IsChecked = true;
            }
            catch (Exception)
            { }
        }

        private void SaveData()
        {
            bool result = _DM_TINHBussiness.InsertorUpdateDM_TINH(this.iDataSource.Copy());
            if (!result)
            {

                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                return;
            }
            else
            {
                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                return;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtMaTinh.Text != String.Empty && txtTenTinh.Text != String.Empty)
                {
                    DataTable dts = _DM_TINHBussiness.GetListDM_TINH_GET_KEY(txtMaTinh.Text);
                    if (frm_dm_Tinh.status == true && dts != null && dts.Rows.Count > 0)
                    {
                        base.ShowMessage(MessageType.Information, "[MÃ] TỈNH " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                    }
                    else
                    {
                        if (CheckSymbolInMaPallet(txtMaTinh.Text) == true)
                        {
                            KTTinhTrang();
                            if (pMA_TINH != this.iDataSource.Rows[0]["MA_TINH"].ToString())
                            {
                                if (dts == null || dts.Rows.Count == 0)
                                {
                                    SaveData();
                                }
                                else
                                {
                                    base.ShowMessage(MessageType.Information, "[MÃ] TỈNH " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                                    return;
                                }
                            }
                            else
                            {
                                SaveData();
                            }
                        }
                        else
                        { base.ShowMessage(MessageType.Information, "[MÃ] TỈNH " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteSYMBOL"])); }
                    }
                }
                else
                { SetFocus(); }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void SetFocus()
        {
            if (txtMaTinh.Text == String.Empty)
            {
                txtMaTinh.Focus();
                base.ShowMessage(MessageType.Information, "[MÃ] TỈNH " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
            }
            else if (txtTenTinh.Text == String.Empty)
            {
                base.ShowMessage(MessageType.Information, "[TÊN] TỈNH " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                txtTenTinh.Focus();
            }

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
