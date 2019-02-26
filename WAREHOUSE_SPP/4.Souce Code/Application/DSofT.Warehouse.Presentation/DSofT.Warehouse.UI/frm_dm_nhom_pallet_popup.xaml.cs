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
using DSofT.Framework.UICore.TaskProxy;
namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_dm_nhom_pallet_popup.xaml
    /// </summary>
    public partial class frm_dm_nhom_pallet_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataTable xLoaiPallet = null;
        DataTable xNhaSanXuat = null;
        IDM_NHOM_PALLETBussiness _DM_NHOM_PALLETBussiness;
        DataRow iRowSelGb = null;
        string pTEN_NHOM_PALLET = String.Empty;
        int flag = 0;
        public frm_dm_nhom_pallet_popup()
        {
            InitializeComponent();
            _DM_NHOM_PALLETBussiness = new DM_NHOM_PALLETBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadComboBox();
            rdbSUDUNG.IsChecked = true;
        }
        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    pTEN_NHOM_PALLET = this.iDataSource.Rows[0]["TEN_NHOM_PALLET"].ToString().Trim();
                    UpdateTinhTrang();
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
                xDicUser.Add("NHOM_PALLET_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("TEN_NHOM_PALLET", typeof(string));
                xDicUser.Add("LOAI_KTHUOC_ID", typeof(decimal));
                xDicUser.Add("LOAI_PALLET_ID", typeof(decimal));
                xDicUser.Add("NHA_SXUAT_ID", typeof(decimal));
                xDicUser.Add("MA_PL_THEO_NSX", typeof(string));
                xDicUser.Add("TEN_PL_THEO_NSX", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(bool));
                xDicUser.Add("RowVersion", typeof(long));
                xDicUser.Add("IsDelete", typeof(bool));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));
                xDicUser.Add("MA_SIZE", typeof(string));
                xDicUser.Add("TEN_LOAI_PALLET", typeof(string));
                xDicUser.Add("TEN_NHA_SXUAT", typeof(string));
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
                DataTable dtlp = _DM_NHOM_PALLETBussiness.GetListDM_NHOM_PALLET_LOAI_PALLET(ConstCommon.DonViQuanLy);
                DataTable dtnsx = _DM_NHOM_PALLETBussiness.GetListDM_NHOM_PALLE_NHA_SAN_XUAT(ConstCommon.DonViQuanLy);
                if (dtlp != null && dtlp.Rows.Count > 0)
                {
                    this.xLoaiPallet = dtlp;
                    ComboBoxUtil.SetComboBoxEdit(this.cboLOAI_PALLET_ID, "TEN_LOAI_PALLET", "LOAI_PALLET_ID", xLoaiPallet, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboLOAI_PALLET_ID, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["LOAI_PALLET_ID"] = cboLOAI_PALLET_ID.GetKeyValue(0);
                }
                if (dtnsx != null && dtnsx.Rows.Count > 0)
                {
                    this.xNhaSanXuat = dtnsx;
                    ComboBoxUtil.SetComboBoxEdit(this.cboNHA_SXUAT_ID, "TEN_NHA_SXUAT", "NHA_SXUAT_ID", xNhaSanXuat, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboNHA_SXUAT_ID, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["NHA_SXUAT_ID"] = cboNHA_SXUAT_ID.GetKeyValue(0);
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
                else if (rdbSUDUNG.IsChecked == true)
                    this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
                else
                    this.iDataSource.Rows[0]["TINH_TRANG"] = 1;
        }
        private void UpdateTinhTrang()
        {
                if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString().Trim() != String.Empty)
                {
                    if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString() == "False")
                        rdbSUDUNG.IsChecked = true; 
                    else
                        rdbKHONGSUDUNG.IsChecked = true;
                }
        }
        private void SetIsNull()
        {
            this.iDataSource.Rows[0]["TEN_NHOM_PALLET"] = string.Empty;
            this.iDataSource.Rows[0]["MA_PL_THEO_NSX"] = string.Empty;
            this.iDataSource.Rows[0]["TEN_PL_THEO_NSX"] = string.Empty;
            this.iDataSource.Rows[0]["GHI_CHU"] = string.Empty;
            this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
            this.cboLOAI_KTHUOC_ID.Text = String.Empty;
            this.cboLOAI_PALLET_ID.Text = this.iDataSource.Rows[0]["TEN_LOAI_PALLET"].ToString();
            this.cboNHA_SXUAT_ID.Text = this.iDataSource.Rows[0]["TEN_NHA_SXUAT"].ToString();
            this.cboLOAI_PALLET_ID.SelectedIndex = 0;
            this.cboNHA_SXUAT_ID.SelectedIndex = 0;
            rdbSUDUNG.IsChecked = true;
        }
        private void SetFocus()
        {
                if (txtTEN_NHOM_PALLET.Text == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "[TÊN NHÓM] PALLET " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    txtTEN_NHOM_PALLET.Focus();
                }
                else if (cboLOAI_PALLET_ID.SelectedIndex == 0)
                {
                    base.ShowMessage(MessageType.Information, "Chưa chọn [TÊN LOẠI] PALLET.");
                    cboLOAI_PALLET_ID.Focus();
                }
                else if (cboLOAI_KTHUOC_ID.Text == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "[KÍCH THƯỚC] PALLET " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    cboLOAI_KTHUOC_ID.Focus();
                }
                else if (cboNHA_SXUAT_ID.SelectedIndex == 0)
                {
                    base.ShowMessage(MessageType.Information, "Chưa chọn [NHÀ SẢN XUẤT] PALLET.");
                    cboNHA_SXUAT_ID.Focus();
                }
                else if (txtMA_PL_THEO_NSX.Text == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "[MÃ PALLET THEO NSX] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    txtMA_PL_THEO_NSX.Focus();
                }
                else
                {
                    base.ShowMessage(MessageType.Information, "[TÊN PALLET THEO NSX] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    txtTEN_PL_THEO_NSX.Focus();
                }
        }
        private void SaveData()
        {
            bool result = _DM_NHOM_PALLETBussiness.InsertorUpdateDM_NHOM_PALLET(this.iDataSource.Copy());
            if (!result)
            {
                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                return;
            }
            else
            {
                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
            }
            //if (frm_dm_nhom_pallet.status == false)
            //    this.Close();
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            flag = 1;
            frm_dm_nhom_pallet.status = true;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SetIsNull();
                txtTEN_NHOM_PALLET.Focus();
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
                DataTable dts = _DM_NHOM_PALLETBussiness.GetListDM_NHOM_PALLET_KEY(ConstCommon.DonViQuanLy, txtTEN_NHOM_PALLET.Text);
                if (frm_dm_nhom_pallet.status == true && dts != null && dts.Rows.Count > 0)
                {
                    base.ShowMessage(MessageType.Information, "[TÊN NHÓM] PALLET " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                }
                else
                {
                    if (txtTEN_NHOM_PALLET.Text != String.Empty && cboLOAI_KTHUOC_ID.Text != String.Empty
                    && cboLOAI_PALLET_ID.Text != String.Empty && cboNHA_SXUAT_ID.Text != String.Empty
                    && txtMA_PL_THEO_NSX.Text != String.Empty && txtTEN_PL_THEO_NSX.Text != String.Empty)
                    {
                        if (pTEN_NHOM_PALLET != this.iDataSource.Rows[0]["TEN_NHOM_PALLET"].ToString())
                        {
                            if (dts == null || dts.Rows.Count == 0)
                            {
                                SaveData();
                            }
                            else
                                base.ShowMessage(MessageType.Information, "[TÊN NHÓM] PALLET " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                        }
                        else
                        {
                            SaveData();
                        }
                    }
                    else
                    {
                        SetFocus();
                        return;
                    }
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

        private void cboLOAI_PALLET_ID_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        { 
            DataTable dt;
            if (flag == 1)
                dt = null;
            else
            {
                dt = _DM_NHOM_PALLETBussiness.GetListDM_LOAI_PALLET_GET_ALL_KICH_THUOC_BY_ID(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(cboLOAI_PALLET_ID.EditValue.ToString()));
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtLOAI_KICH_THUOC_ID.EditValue = dt.Rows[0]["LOAI_KTHUOC_ID"].ToString().Trim();
                    cboLOAI_KTHUOC_ID.Text = dt.Rows[0]["MA_SIZE"].ToString();
                }
                else
                {
                    cboLOAI_KTHUOC_ID.Text = String.Empty;
                    return;
                }
            }
        }
        private void rdbKHONGSUDUNG_Click(object sender, RoutedEventArgs e)
        {
            this.iDataSource.Rows[0]["TINH_TRANG"] = 1;
        }

        private void rdbSUDUNG_Click(object sender, RoutedEventArgs e)
        {
            this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
        }
    }

}