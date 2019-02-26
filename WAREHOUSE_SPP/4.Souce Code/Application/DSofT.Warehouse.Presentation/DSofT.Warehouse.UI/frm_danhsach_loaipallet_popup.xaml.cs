﻿using DSofT.Warehouse.Business;
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
    /// Interaction logic for frm_danhsach_loaipallet_popup.xaml
    /// </summary>
    public partial class frm_danhsach_loaipallet_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataTable xLoaiKichThuoc = null;
        IDM_LOAI_PALLETBussiness _DM_LOAI_PALLETBussiness;
        DataRow iRowSelGb = null;
        string pTEN_LOAI_PALLET = String.Empty;
        long pID_KICH_THUOC = 0;
        public frm_danhsach_loaipallet_popup()
        {
            InitializeComponent();
            _DM_LOAI_PALLETBussiness = new DM_LOAI_PALLETBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadComboBox();
            
        }
        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGb = this.Parameter[0] as DataRow;
                DispalayRequest();
                pTEN_LOAI_PALLET = this.iDataSource.Rows[0]["TEN_LOAI_PALLET"].ToString().Trim();
                pID_KICH_THUOC = ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["LOAI_KTHUOC_ID"].ToString().Trim());
                UpdateTinhTrang();
            }
        }
        /// <summary>
        /// 
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
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("LOAI_PALLET_ID", typeof(decimal));
                xDicUser.Add("TEN_LOAI_PALLET", typeof(string));
                xDicUser.Add("LOAI_KTHUOC_ID", typeof(decimal));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(Int16));
                xDicUser.Add("RowVersion", typeof(long));
                xDicUser.Add("IsDelete", typeof(bool));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));

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
        private void LoadComboBox()
        {
            try
            {

                DataTable dt = _DM_LOAI_PALLETBussiness.GetListDM_LOAI_KICKTHUOC_COMBOBOX(ConstCommon.DonViQuanLy);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.xLoaiKichThuoc = dt;
                    ComboBoxUtil.SetComboBoxEdit(this.cboLOAI_KTHUOC_ID, "MA_SIZE", "LOAI_KTHUOC_ID", xLoaiKichThuoc, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboLOAI_KTHUOC_ID, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["LOAI_KTHUOC_ID"] = cboLOAI_KTHUOC_ID.GetKeyValue(0);
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void KTTinhTrang()
        {
                if (rdbKHONGSUDUNG.IsChecked == true)
                    this.iDataSource.Rows[0]["TINH_TRANG"] = 1;
                else if(rdbSUDUNG.IsChecked == true)
                    this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
                else
                    this.iDataSource.Rows[0]["TINH_TRANG"] = 1;
        }
        private void UpdateTinhTrang()
        {
                if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString().Trim() != String.Empty)
                {
                    if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString() == "1")
                        rdbKHONGSUDUNG.IsChecked = true;
                    else
                        rdbSUDUNG.IsChecked = true;
                }
                else
                    rdbKHONGSUDUNG.IsChecked = true;
        }
        private void SetIsNull()
        {
            this.iDataSource.Rows[0]["TEN_LOAI_PALLET"] = string.Empty;
            this.iDataSource.Rows[0]["GHI_CHU"] = string.Empty;
            this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
            this.cboLOAI_KTHUOC_ID.SelectedIndex = 0;
            rdbSUDUNG.IsChecked = true;
        }
        private void SaveData()
        {
            bool result = _DM_LOAI_PALLETBussiness.InsertorUpdateDM_LOAI_PALLET(this.iDataSource.Copy());
            if (!result)
            {
                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                return;
            }
            else
            {
                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
              //  return;
            }
            //if (frm_danhsach_loaipallet.status == false)
            //    this.Close();
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            frm_danhsach_loaipallet.status = true;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SetIsNull();
                txtTEN_LOAI_PALLET.Focus();
                this.iDataSource.Clear();
                this.iDataSource = this.TableSchemaDefineBingding();
                this.DataContext = this.iDataSource;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtTEN_LOAI_PALLET.Text != String.Empty && cboLOAI_KTHUOC_ID.SelectedIndex != 0)
                {
                    DataTable dts = _DM_LOAI_PALLETBussiness.GetListDM_LOAI_PALLET_KEY(ConstCommon.DonViQuanLy, txtTEN_LOAI_PALLET.Text, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["LOAI_KTHUOC_ID"].ToString()));
                    if (frm_danhsach_loaipallet.status == true && dts != null && dts.Rows.Count > 0)
                    {
                        base.ShowMessage(MessageType.Information, "[TÊN LOẠI],[KÍCH THƯỚC] PALLET " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                    }
                    else
                    {
                        if (pTEN_LOAI_PALLET != this.iDataSource.Rows[0]["TEN_LOAI_PALLET"].ToString() || pID_KICH_THUOC != ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["LOAI_KTHUOC_ID"].ToString().Trim()))
                        {
                            if (dts == null || dts.Rows.Count == 0)
                            {
                                SaveData();
                            }
                            else
                                base.ShowMessage(MessageType.Information, "[TÊN LOẠI],[KÍCH THƯỚC] PALLET " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                        }
                        else
                        {
                            SaveData();
                        }
                    }
                }
                else
                {
                    if (txtTEN_LOAI_PALLET.Text == String.Empty)
                    {
                        base.ShowMessage(MessageType.Information, "[TÊN LOẠI] PALLET " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                        txtTEN_LOAI_PALLET.Focus();
                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, "Chưa chọn [KÍCH THƯỚC] PALLET.");
                        cboLOAI_KTHUOC_ID.Focus();
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rdbSUDUNG_Click(object sender, RoutedEventArgs e)
        {
            KTTinhTrang();
        }

        private void rdbKHONGSUDUNG_Click(object sender, RoutedEventArgs e)
        {
            KTTinhTrang();
        }
    }

}