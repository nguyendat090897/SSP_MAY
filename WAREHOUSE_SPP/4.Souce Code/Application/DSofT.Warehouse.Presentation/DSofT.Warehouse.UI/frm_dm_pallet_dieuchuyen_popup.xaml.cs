﻿using DSofT.Warehouse.Business;
using DSofT.Warehouse.Log.UtilHelper;
using DSofT.Warehouse.Resources;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using DevExpress.Xpf.Grid;
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
    ///  Author:	 Ngô Gia Thiên
    ///  Create date: 06/03/2018
    ///  Editor:      Nguyen Van Huynh
    ///  Modify date: 20/3/2018
    ///  Description: Danh muc pallet dieu chuyen
    ///  DEV: Nguyen Van Huynh
    /// </summary>
    public partial class frm_dm_pallet_dieuchuyen_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataTable iGridDataSource = null;
        DataTable xDonViQuanLy = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        public static int pSO_LUONG_CHUYEN = 0;
        public static int pSO_LUONG_CHO_PHEP;
        public static long pPALLET_CHUYEN = 0;
        public static string pSO_PHIEU = String.Empty;
        long pPALLET_CHUYEN_ID = 0;
        int p_so_luong = 0;
        IDM_PALLET_DIEUCHUYEN_CTIETBussiness _DM_PALLET_DIEUCHUYEN_CTIETBussiness;
        public frm_dm_pallet_dieuchuyen_popup()
        {
            InitializeComponent();
            _DM_PALLET_DIEUCHUYEN_CTIETBussiness = new DM_PALLET_DIEUCHUYEN_CTIETBussiness(string.Empty);
            Initialize_Grid();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.iGridDataSource = this.TableSchemaDefineBingding();
            this.iGridDataSource.Clear();
            this.DataContext = this.iDataSource;
            LoadComboBoxDonViQuanLy();
            this.iDataSource.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
            LoadData();
            
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    pSO_PHIEU = this.iDataSource.Rows[0]["SO_PHIEU"].ToString().Trim();
                    pPALLET_CHUYEN_ID  = ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PALLET_DIEUCHUYEN_ID"].ToString().Trim());
                    pPALLET_CHUYEN = pPALLET_CHUYEN_ID;
                    pSO_LUONG_CHUYEN = ConstCommon.NVL_NUM_NT_NEW(this.iDataSource.Rows[0]["SOLUONG_CHUYEN"].ToString().Trim());
                    if(this.iDataSource.Rows[0]["SOLUONG_CHOPHEP"].ToString().Trim() == String.Empty)
                    {
                        pSO_LUONG_CHO_PHEP = 0;
                    }
                    else
                    {
                        pSO_LUONG_CHO_PHEP = ConstCommon.NVL_NUM_NT_NEW(this.iDataSource.Rows[0]["SOLUONG_CHOPHEP"].ToString().Trim());
                    }
                    if (frm_dm_pallet_dieu_chuyen.status == false)
                    {
                        LoadData();
                    }
                    else
                    {
                        this.iGridDataSource = null;
                        LoadData();
                    }
                }
                if (frm_dm_pallet_dieu_chuyen.status == true)
                {
                    btnInphieu.IsEnabled = false;
                }
                else
                    btnInphieu.IsEnabled = true;
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
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("PALLET_DIEUCHUYEN_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("MA_DVIQLY_NHAN", typeof(string));
                xDicUser.Add("SO_PHIEU", typeof(string));
                xDicUser.Add("SOLUONG_CHOPHEP", typeof(decimal));
                xDicUser.Add("SOLUONG_CHUYEN", typeof(decimal));
                xDicUser.Add("NGAY_CHUYEN", typeof(string));
                xDicUser.Add("NGAY_NHAN", typeof(string));
                xDicUser.Add("NGUOI_NHAN", typeof(string));
                xDicUser.Add("LYDO", typeof(string));
                xDicUser.Add("CHON", typeof(bool));
                xDicUser.Add("PALLET_ID", typeof(decimal));
                xDicUser.Add("MA_PALLET", typeof(string));
                xDicUser.Add("TEN_PALLET", typeof(string));
                xDicUser.Add("PALLET_DIEUCHUYEN_CTIET_ID", typeof(decimal));
                xDicUser.Add("TEN_DVIQLY", typeof(string));
                xDicUser.Add("TEN_DVIQLY_GIAO", typeof(string));
                xDicUser.Add("SO_LUONG_PALLET_DIEU_CHUYEN", typeof(int));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
                xDicUser.Add("TINH_TRANG", typeof(bool));
                xDicUser.Add("STT", typeof(int));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }
        private void LoadComboBoxDonViQuanLy()
        {
            try
            {
                DataTable tb = _DM_PALLET_DIEUCHUYEN_CTIETBussiness.GetListDM_PALLET_DIEUCHUYEN_DONVIQUANLY();
                if (tb != null && tb.Rows.Count > 0)
                {
                    this.xDonViQuanLy = tb;
                    ComboBoxUtil.SetComboBoxEdit(this.cboMA_DVIQLY_NHAN, "TEN_DVIQLY", "MA_DVIQLY_NHAN", xDonViQuanLy, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboMA_DVIQLY_NHAN, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["MA_DVIQLY_NHAN"] = cboMA_DVIQLY_NHAN.GetKeyValue(0);
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _DM_PALLET_DIEUCHUYEN_CTIETBussiness.GetListDM_PALLET_DIEUCHUYEN_CTIET(ConstCommon.DonViQuanLy,pPALLET_CHUYEN_ID);
                    });
                    Action<DataTable> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iGridDataSource = dt;
                            this.iGridHelper.BindItemSource(this.iGridDataSource);
                        }));
                    });
                    task.AsyncTaskExecute(action, onComplete);
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadDataTenPallet()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn = new Column();

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 40;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("CHON", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_Chon"]));
                xColumn.Width = 40;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("CHON") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                xColumn.AllowSorting = false;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaPallet"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("PALLET_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaPallet"]));
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("PALLET_DIEUCHUYEN_CTIET_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaPallet"]));
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                this.iGridHelper.Initialize();
                this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private bool CheckSymbolInSoPhieu(string str)
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
        private void SetFocus()
        {
            if (cboMA_DVIQLY_NHAN.SelectedIndex == 0)
            {
                cboMA_DVIQLY_NHAN.Focus();
                base.ShowMessage(MessageType.Information, "Chưa chọn [ĐƠN VỊ QUẢN LÝ NHẬN]." );
            }
            else if (txtSO_PHIEU.Text == String.Empty)
            {
                base.ShowMessage(MessageType.Information, "[SỐ PHIẾU] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                txtSO_PHIEU.Focus();
            }
            else if (dtpNGAY_TRANGTHAI.Text == String.Empty)
            {
                base.ShowMessage(MessageType.Information, "[NGÀY CHUYỂN] PALLET " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                dtpNGAY_TRANGTHAI.Focus();
            }
            else
            {
                base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG CHUYỂN] PALLET " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
            }
        }
        private void SaveData()
        {

            DataSet dsReturn = null;
            dsReturn = _DM_PALLET_DIEUCHUYEN_CTIETBussiness.InsertDM_PALLET_DIEUCHUYEN(iDataSource, iGridDataSource, CommonDataHelper.UserName, ConstCommon.DonViQuanLy);
            if ((dsReturn != null) && (dsReturn.Tables.Count == 2))
            {

                foreach (DataColumn item in dsReturn.Tables[0].Columns)
                {
                    if (this.iDataSource.Columns[item.ColumnName] != null)
                    {
                        this.iDataSource.Rows[0][item.ColumnName] = dsReturn.Tables[0].Rows[0][item.ColumnName];
                    }
                }
                DataTable dt = null;
                dt = this.TableSchemaDefineBingding();
                dt.Clear();
                dt = dsReturn.Tables[1].Copy();
                if (dt != null)
                {
                   // LoadData();
                }
                else
                {
                    grd.ItemsSource = null;
                }
                pSO_PHIEU = txtSO_PHIEU.Text;
                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
            }
            else
            {
                base.ShowMessage(MessageType.Error, "Lưu không thành công");
            }
        }
        /// <summary>
        /// lap phieu dieu chuyen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInphieu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_dm_pallet_tiepnhandieuchuyen", iRowSelGb);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnInphieu.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnTIM_KIEM_PALLET_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_dm_pallet_dieuchuyen_popup_2", null);
                DataTable dt_SPCHON = (DataTable)xReturn;

                if ((dt_SPCHON != null) && (dt_SPCHON.Rows.Count > 0))
                {
                    if (dt_SPCHON.Rows[0]["GHI_CHU"].ToString() == "Y")
                    {
                        for (int i = 0; i < dt_SPCHON.Rows.Count; i++)
                        {
                            if (ContainDataRowInDataTable(iGridDataSource, dt_SPCHON.Rows[i]) == false)
                            {
                                DataRow dr = iGridDataSource.NewRow();
                                dr["MA_PALLET"] = dt_SPCHON.Rows[i]["MA_PALLET"];
                                dr["TEN_PALLET"] = dt_SPCHON.Rows[i]["TEN_PALLET"];
                              //  dr["PALLET_DIEUCHUYEN_CTIET_ID"] = iGridDataSource.Rows[i]["PALLET_DIEUCHUYEN_CTIET_ID"];
                                dr["PALLET_ID"] = dt_SPCHON.Rows[i]["PALLET_ID"];
                                iGridDataSource.Rows.Add(dr);
                            }
                        }
                    }
                    pSO_LUONG_CHUYEN = iGridDataSource.Rows.Count;
                    txtSOLUONG_CHUYEN.EditValue = pSO_LUONG_CHUYEN.ToString();
                }

                if (iGridDataSource != null)
                {
                    this.iGridHelper.BindItemSource(iGridDataSource);
                }
                else
                {
                    grd.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnTIM_KIEM_PALLET.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void Delete()
        {
            bool note = false;
            long k = 0;
            if ((iGridDataSource == null) || (iGridDataSource.Rows.Count == 0))
            {
                return;
            }
            if (this.grd.GetFocusedRow() == null) return;
            bool result = true;
            bool result_so_luong = true;
            for (long i = 0; i < this.iGridDataSource.Rows.Count; i++)
            {
                if (this.iGridDataSource.Rows.Count == 0)
                    return;
                if (this.iGridDataSource.Rows[ConstCommon.NVL_NUM_NT_NEW(i)]["CHON"].ToString() == "1")
                {
                    if (ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[ConstCommon.NVL_NUM_NT_NEW(i)]["PALLET_DIEUCHUYEN_CTIET_ID"]) > 0)
                    {
                        k = ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[ConstCommon.NVL_NUM_NT_NEW(i)]["PALLET_DIEUCHUYEN_CTIET_ID"].ToString());
                        this.iGridDataSource.Rows.Remove(this.iGridDataSource.Rows[ConstCommon.NVL_NUM_NT_NEW(i)]);
                        result = _DM_PALLET_DIEUCHUYEN_CTIETBussiness.DeleteDM_PALLET_DIEUCHUYEN_CTIET(ConstCommon.DonViQuanLy,k, CommonDataHelper.UserName);
                        pSO_LUONG_CHUYEN--;
                    }
                    else
                    {
                        this.iGridDataSource.Rows.Remove(this.iGridDataSource.Rows[ConstCommon.NVL_NUM_NT_NEW(i)]);
                        i--;
                        pSO_LUONG_CHUYEN--;
                        note = true;
                    }
                }
            }
            txtSOLUONG_CHUYEN.EditValue = pSO_LUONG_CHUYEN.ToString();
            result_so_luong = _DM_PALLET_DIEUCHUYEN_CTIETBussiness.UpdateDM_PALLET_DIEUCHUYEN_SO_LUONG(ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[0]["PALLET_DIEUCHUYEN_ID"].ToString()), ConstCommon.DonViQuanLy, pSO_LUONG_CHUYEN);
            if (!result && note == false)
            {
                base.ShowMessage(MessageType.Information,"Hãy chọn pallet cần xóa!");
                return;
            }
            else
            {
                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["tblThongBao"]), Convert.ToString(UtilLanguage.ApplyLanguage()["tblThanhCong"]), notificationService);
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Delete();
            }
            catch (Exception ex)
            {
                return;
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnTIM_KIEM_PALLET.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboMA_DVIQLY_NHAN.Text != String.Empty && txtSO_PHIEU.Text != String.Empty &&
                    dtpNGAY_TRANGTHAI.Text != String.Empty && txtSOLUONG_CHUYEN.Text != String.Empty)
                {
                    if (CheckSymbolInSoPhieu(txtSO_PHIEU.Text) == true)
                    {
                        if (txtSOLUONG_CHOPHEP.Text != string.Empty)
                        {
                            int so_luong_vuot = Convert.ToInt32(txtSOLUONG_CHOPHEP.Text) - Convert.ToInt32(txtSOLUONG_CHUYEN.Text);
                            if (so_luong_vuot >= 0)
                            {
                                SaveData();
                            }
                            else
                            {
                                base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG CHUYỂN] vượt so với [SỐ LƯỢNG CHO PHÉP].", null);
                                return;
                            }
                        }
                        else
                            SaveData();
                    }
                    else
                        base.ShowMessage(MessageType.Information, "[SỐ PHIẾU] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteSYMBOL"]));
                }
                else
                    SetFocus();
            }
            catch (Exception ex)
            {
                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
            }
        }
        private bool ContainDataRowInDataTable(DataTable dt, DataRow dr)
        {
            try
            {
                var k = (from r in dt.Rows.OfType<DataRow>() where r["PALLET_ID"].ToString() == dr["PALLET_ID"].ToString() select r).FirstOrDefault();
                if (k != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
        }

        private void txtSOLUONG_CHOPHEP_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (txtSOLUONG_CHUYEN.Text != String.Empty)
            {
                if (txtSOLUONG_CHOPHEP.Text == String.Empty)
                {
                    //pSO_LUONG_CHO_PHEP = 0; 
                    return;
                }
                else
                {
                    if (ConstCommon.NVL_NUM_NT_NEW(txtSOLUONG_CHOPHEP.EditValue.ToString()) < pSO_LUONG_CHUYEN)
                    {
                        p_so_luong = pSO_LUONG_CHUYEN - ConstCommon.NVL_NUM_NT_NEW(txtSOLUONG_CHOPHEP.EditValue.ToString());
                        base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG CHUYỂN] vượt quá " + p_so_luong + " so với [SỐ LƯỢNG CHO PHÉP].", null);
                    }
                    else
                        pSO_LUONG_CHO_PHEP = ConstCommon.NVL_NUM_NT_NEW(txtSOLUONG_CHOPHEP.EditValue.ToString());
                }
            }
            else
                return;
        }

        private void grdView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //try
            //{
            //    //iRowSelGb = null;
            //    //if (iGridDataSource.Rows.Count == 0)
            //    //    return;
            //    //iRowSelGb = ((DataRowView)this.grd.GetFocusedRow()).Row;
            //    //if (iRowSelGb["CHON"].ToString() == "1")
            //    //{
            //    //    pSO_LUONG_CHUYEN++;
            //    //}
            //    //else
            //    //    pSO_LUONG_CHUYEN--;
            //    //txtSOLUONG_CHUYEN.EditValue = pSO_LUONG_CHUYEN.ToString();
            //}
            //catch (Exception ex)
            //{
            //    UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_MouseDown()");
            //    base.ShowMessage(MessageType.Error, ex.Message, ex);
            //}
        }
    }
}
