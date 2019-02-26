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
using DSofT.Framework.UICore.TaskProxy;
namespace DSofT.Warehouse.UI
{//
    /// <summary>
    /// </summary>
    public partial class frm_dm_LoaiSanPham_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataTable xNhomSP = null;
        DataTable iGridDataSource = null;
        IDM_LOAI_SPHAMBussiness _DM_LOAI_SANPHAMBussiness;
        DataRow iRowSelGb = null;
        string pMA_LOAI_SPHAM = String.Empty;
        public frm_dm_LoaiSanPham_popup()
        {
            InitializeComponent();
            _DM_LOAI_SANPHAMBussiness = new DM_LOAI_SPHAMBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            // txtMA_DVIQLY.Text = ConstCommon.DonViQuanLy;
            LoadComboBox();
            UpdateTinhTrang();
        }
        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    pMA_LOAI_SPHAM = this.iDataSource.Rows[0]["MA_LOAI_SPHAM"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetIDataSource()
        {
            return this.iDataSource;
        }

        /// <summary>
        public bool check_String_Unicode(string str)
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
        /// </summary>
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
                xDicUser.Add("LOAI_SPHAM_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("TEN_NHOM_SPHAM", typeof(string));
                xDicUser.Add("MA_LOAI_SPHAM", typeof(string));
                xDicUser.Add("TEN_LOAI_SPHAM", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(bool));
                xDicUser.Add("NHOM_SPHAM_ID", typeof(decimal));
                xDicUser.Add("RowVersion", typeof(long));
                xDicUser.Add("IsDelete", typeof(bool));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));
                // xDicUser.Add("TEN_NHOM_SPHAM", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }


        /// <summary>
        /// LoadComboBox
        /// </summary>
        /// <returns></returns>
        private void SetIsNull()
        {
            this.iDataSource.Rows[0]["MA_LOAI_SPHAM"] = string.Empty;
            this.iDataSource.Rows[0]["TEN_LOAI_SPHAM"] = string.Empty;
            this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            frm_dm_LoaiSanPham.status = true;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SetIsNull();
                txtMaLoaiSanPham.Focus();
                cboNhomSanPham.SelectedIndex = 0;
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
        private void LoadComboBox()
        {
            try
            {

                DataTable dt = _DM_LOAI_SANPHAMBussiness.GetListDM_NHOM_SPHAM_COMBOBOX(ConstCommon.DonViQuanLy);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.xNhomSP = dt;
                    ComboBoxUtil.SetComboBoxEdit(this.cboNhomSanPham, "TEN_NHOM_SPHAM", "NHOM_SPHAM_ID", xNhomSP, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboNhomSanPham, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["NHOM_SPHAM_ID"] = cboNhomSanPham.GetKeyValue(0);
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
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
            bool result = _DM_LOAI_SANPHAMBussiness.InsertorUpdateDM_LOAI_SPHAM(this.iDataSource.Copy());
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
                if (txtMaLoaiSanPham.Text != String.Empty && txtTenLoaiSanPham.Text != String.Empty && cboNhomSanPham.SelectedIndex != 0)
                {
                    DataTable dts = _DM_LOAI_SANPHAMBussiness.GetListDM_LOAI_SPHAM_KEY(ConstCommon.DonViQuanLy,
                           txtMaLoaiSanPham.Text);
                    if (frm_dm_LoaiSanPham.status == true && dts != null && dts.Rows.Count > 0)
                    {
                        base.ShowMessage(MessageType.Information, "[MÃ] SẢN PHẨM " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                    }
                    else
                    {
                        if (CheckSymbolInMaPallet(txtMaLoaiSanPham.Text) == true)
                        {
                            KTTinhTrang();
                            if (pMA_LOAI_SPHAM != this.iDataSource.Rows[0]["MA_LOAI_SPHAM"].ToString())
                            {
                                if (dts == null || dts.Rows.Count == 0)
                                {
                                    SaveData();
                                }
                                else
                                {
                                    base.ShowMessage(MessageType.Information, "[MÃ] LOẠI SẢN PHẨM " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                                    return;
                                }
                            }
                            else
                            {
                                SaveData();
                            }
                        }
                        else
                        { base.ShowMessage(MessageType.Information, "[MÃ] SẢN PHẨM " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteSYMBOL"])); }
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
            if (txtMaLoaiSanPham.Text == String.Empty)
            {
                txtMaLoaiSanPham.Focus();
                base.ShowMessage(MessageType.Information, "[MÃ] SẢN PHẨM " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
            }
            else if (txtTenLoaiSanPham.Text == String.Empty)
            {
                base.ShowMessage(MessageType.Information, "[TÊN] SẢN PHẨM " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                txtTenLoaiSanPham.Focus();
            }
            else if (cboNhomSanPham.SelectedIndex == 0)
            {
                base.ShowMessage(MessageType.Information, "Chưa chọn [NHÓM] SẢN PHẨM. ");
                cboNhomSanPham.Focus();
            }
           
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}