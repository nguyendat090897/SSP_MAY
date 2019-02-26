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
    /// Interaction logic for frm_dm_donvitinh_popup.xaml
    /// </summary>
    public partial class frm_dm_donvitinh_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataTable iGridDataSource = null;
        IDM_DONVI_TINHBussiness _DM_DONVI_TINHBussiness;
        DataRow iRowSelGb = null;
        string pMA_DONVI_TINH = String.Empty;
        public frm_dm_donvitinh_popup()
        {
            InitializeComponent();
            _DM_DONVI_TINHBussiness = new DM_DONVI_TINHBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            UpdateTinhTrang();

        }
        /// <summary>
        /// 
        /// </summary>
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
                    pMA_DONVI_TINH = this.iDataSource.Rows[0]["MA_DONVI_TINH"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// TableSchemaDefineBingding
        /// </summary>
        /// <returns></returns>
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DONVI_TINH", typeof(string));
                xDicUser.Add("TEN_DONVI_TINH", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(bool));
                xDicUser.Add("TINH_TRANG_HT", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }


        /// <summary>
        /// btnNew_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            frm_dm_donvitinh.status = true;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SetIsNull();
                txtMaDonViTinh.Focus();
                rdbKhongSuDung.IsChecked = true;
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
        private void SetIsNull()
        {
            this.iDataSource.Rows[0]["MA_DONVI_TINH"] = string.Empty;
            this.iDataSource.Rows[0]["TEN_DONVI_TINH"] = string.Empty;
            this.iDataSource.Rows[0]["GHI_CHU"] = string.Empty;
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

        private void KTTinhTrang()
        {
            if (rdbConSuDung.IsChecked == true)
                this.iDataSource.Rows[0]["TINH_TRANG"] = "True";
            else
                this.iDataSource.Rows[0]["TINH_TRANG"] = "False";
        }

        private void UpdateTinhTrang()
        {
            try
            {
                if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString().Trim() != null)
                {
                    if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString() == "True")
                        rdbConSuDung.IsChecked = true;
                    else
                        rdbKhongSuDung.IsChecked = true;
                }
                else
                    rdbKhongSuDung.IsChecked = true;
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void SaveData(int existMa)
        {
            bool result = _DM_DONVI_TINHBussiness.InsertorUpdateDM_DONVI_TINH(existMa, this.iDataSource.Copy());
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
                int existMa = 0;
                if (txtMaDonViTinh.Text != String.Empty && txtTenDonViTinh.Text != String.Empty)
                {
                    DataTable dts = _DM_DONVI_TINHBussiness.GetListDM_DONVI_TINH_GET_KEY(txtMaDonViTinh.Text);
                    if(frm_dm_donvitinh.status == false)
                        existMa = 1;
                    if (frm_dm_donvitinh.status == true && dts != null && dts.Rows.Count > 0)
                    {
                        existMa = 0;
                        base.ShowMessage(MessageType.Information, "[MÃ] ĐƠN VỊ TÍNH " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                    }
                    else
                    {
                        if (CheckSymbolInMaPallet(txtMaDonViTinh.Text) == true)
                        {
                            KTTinhTrang();
                            if (pMA_DONVI_TINH != this.iDataSource.Rows[0]["MA_DONVI_TINH"].ToString())
                            {
                                if (dts == null || dts.Rows.Count == 0)
                                {
                                    SaveData(existMa);
                                }
                                else
                                {
                                    base.ShowMessage(MessageType.Information, "[MÃ] ĐƠN VỊ TÍNH " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                                    return;
                                }
                            }
                            else
                            {
                                SaveData(existMa);
                            }
                        }
                        else
                        { base.ShowMessage(MessageType.Information, "[MÃ] ĐƠN VỊ TÍNH " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteSYMBOL"])); }
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
            if (txtMaDonViTinh.Text == String.Empty)
            {
                txtMaDonViTinh.Focus();
                base.ShowMessage(MessageType.Information, "[MÃ] ĐƠN VỊ TÍNH " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
            }
            else if (txtTenDonViTinh.Text == String.Empty)
            {
                base.ShowMessage(MessageType.Information, "[TÊN] ĐƠN VỊ TÍNH " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                txtTenDonViTinh.Focus();
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
