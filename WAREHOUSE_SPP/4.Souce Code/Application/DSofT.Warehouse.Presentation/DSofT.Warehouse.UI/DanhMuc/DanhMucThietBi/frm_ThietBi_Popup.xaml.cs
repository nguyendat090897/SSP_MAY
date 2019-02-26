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
    ///  Author:	 Ngô Gia Thiên
    ///  Create date: 24/01/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Danh muc thiet bi popup
    /// </summary>
    public partial class frm_ThietBi_Popup : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        //
        //
        DataTable iGridDataSource = null;
        I_DM_THIETBI_BUSSINESS _DM_THIETBI_BUSSINESS;
        string pMA_TB = String.Empty;
        public frm_ThietBi_Popup()
        {
            InitializeComponent();
            //
            //
            _DM_THIETBI_BUSSINESS = new DM_THIETBI_BUSSINESS(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            loadComboBox();
        }
        //
        //
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
                    pMA_TB   = this.iDataSource.Rows[0]["MA_TBI"].ToString().Trim();
                    UpdateTinhTrang();
                    updateLoaiThietBi();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        private void SetIsNull()
        {
            this.iDataSource.Rows[0]["MA_TBI"] = string.Empty;
            this.iDataSource.Rows[0]["TEN_TBI"] = string.Empty;
            this.iDataSource.Rows[0]["THONG_SO"] = string.Empty;
            this.iDataSource.Rows[0]["NUOC_SAN_XUAT"] = string.Empty;
            this.iDataSource.Rows[0]["GHI_CHU"] = string.Empty;
        }

        private void KTTinhTrang()
        {
            if (rdbSU_DUNG.IsChecked == true)
                this.iDataSource.Rows[0]["TINH_TRANG"] = 1;
            else
                this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
        }

        private void KTLoaiTB()
        {
            if (rdbPDA.IsChecked == true)
                this.iDataSource.Rows[0]["LOAI_TBI"] = "PDA";
            if (rdbXenang.IsChecked == true)
                this.iDataSource.Rows[0]["LOAI_TBI"] = "XNA";
        }

        public void Luu_TINHTRANG()
        {
            try
            {
                if (rdbSU_DUNG.IsChecked == true)
                {
                    this.iDataSource.Rows[0]["TINH_TRANG"] = "1";
                }
                if (rdbKHONG_SU_DUNG.IsChecked == true)
                {
                    this.iDataSource.Rows[0]["TINH_TRANG"] = "0";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void UpdateTinhTrang()
        {
            try
            {
                if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString().Trim() != string.Empty)
                {
                    if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString().Trim() == "1")
                        rdbSU_DUNG.IsChecked = true;
                    if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString().Trim() == "0")
                        rdbKHONG_SU_DUNG.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private void updateLoaiThietBi()
        {
            try
            {
                if (this.iDataSource.Rows[0]["LOAI_TBI"].ToString().Trim() != string.Empty)
                {
                    if (this.iDataSource.Rows[0]["LOAI_TBI"].ToString().Trim() == "XNA")
                        rdbXenang.IsChecked = true;
                    if (this.iDataSource.Rows[0]["LOAI_TBI"].ToString().Trim() == "PDA")
                        rdbPDA.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void loadComboBox()
        {
            try
            {
                DataTable DMQG = _DM_THIETBI_BUSSINESS.GetListDM_THIETBI_QG(ConstCommon.DonViQuanLy);
                DataTable DMNSX = _DM_THIETBI_BUSSINESS.GetListDM_THIETBI_NSX(ConstCommon.DonViQuanLy);
                if (DMQG != null && DMQG.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(this.cbbNuocSX, "TEN_QUOCGIA", "QUOCGIA_ID", DMQG, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cbbNuocSX, Convert.ToString(UtilLanguage.ApplyLanguage()[/*"lblNSX"*/"lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["QUOCGIA_ID"] = cbbNuocSX.GetKeyValue(0);
                }
                if (DMNSX != null && DMNSX.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(this.cbbNhaSX, "TEN_NHA_SXUAT", "NHA_SXUAT_ID",DMNSX,SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cbbNhaSX, Convert.ToString(UtilLanguage.ApplyLanguage()[/*"lblNhaSX"]*/"lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["NHA_SXUAT_ID"] = cbbNuocSX.GetKeyValue(0);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            frm_ThietBi.status = true;
            //frm_ThietBi_Popup.status = true;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SetIsNull();
                txtMaThietBi.Text = null;
                txtTenThietBi = null;
                txtThongSo = null;
                txtGhiChu = null;
                rdbXenang.IsChecked = true;
                rdbSU_DUNG.IsChecked = true;
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

        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("THIETBI_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("LOAI_TBI", typeof(string));
                xDicUser.Add("MA_TBI", typeof(string));
                xDicUser.Add("TEN_TBI", typeof(string));
                xDicUser.Add("THONG_SO", typeof(string));
                xDicUser.Add("QUOCGIA_ID", typeof(decimal));
                xDicUser.Add("NHA_SXUAT_ID", typeof(decimal));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(Int16));
                xDicUser.Add("RowVersion", typeof(int));
                xDicUser.Add("IsDelete", typeof(Boolean));
                xDicUser.Add("CreateBy", typeof(String));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(String));
                xDicUser.Add("TEN_QUOCGIA", typeof(string));
                xDicUser.Add("TEN_NHA_SXUAT", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }

        private void SetFocus()
        {
            if (txtMaThietBi.Text == String.Empty)
            {
                txtMaThietBi.Focus();
                base.ShowMessage(MessageType.Information, "[MÃ] THIẾT BỊ " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
            }
            else if (txtTenThietBi.Text == String.Empty)
            {
                base.ShowMessage(MessageType.Information, "[TÊN] THIẾT BỊ " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                txtTenThietBi.Focus();
            }

        }

        private void SaveData()
        {
            bool result = _DM_THIETBI_BUSSINESS.InsertorUpdateDM_THIETBI(this.iDataSource.Copy());
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
        //
        //
        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtMaThietBi.Text != String.Empty && txtTenThietBi.Text != String.Empty)
                {
                    DataTable dts = _DM_THIETBI_BUSSINESS.GetListDM_THIETBI_GET_KEY(txtMaThietBi.Text);
                    if (frm_ThietBi.status == true && dts != null && dts.Rows.Count > 0)
                    {
                        base.ShowMessage(MessageType.Information, "[MÃ] THIẾT BỊ ĐÃ TỒN TẠI " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                    }
                    else
                    {
                        if (CheckSymbolInMaPallet(txtMaThietBi.Text) == true)
                        {
                            KTTinhTrang();
                            KTLoaiTB();
                            if (pMA_TB != this.iDataSource.Rows[0]["MA_TBI"].ToString())
                            {
                                if (dts == null || dts.Rows.Count == 0)
                                {
                                    SaveData();
                                }
                                else
                                {
                                    base.ShowMessage(MessageType.Information, "[MÃ] THIẾT BỊ " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                                    return;
                                }
                            }
                            else
                            {
                                SaveData();
                            }
                        }
                        else
                        { base.ShowMessage(MessageType.Information, "[MÃ] THIẾT BỊ" + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteSYMBOL"])); }
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
        /// <summary>
        /// Đóng Popup 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
