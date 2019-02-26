﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows;
using System.Windows.Input;
using DSofT.Warehouse.Business;
using DSofT.Warehouse.Resources;
using DSofT.Warehouse.Log.UtilHelper;
using DSofT.Framework.UICore;
using DSofT.Framework.UIControl;
using System.Windows.Data;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using Aspose.Cells.Drawing;
using DSofT.Framework.UICore.TaskProxy;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_DonViVanChuyen.xaml
    /// </summary>
    public partial class frm_DonViVanChuyen : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        DataRow iRowSelGb = null;
        IDM_KHACHHANG_NCCBussiness _DM_KHACHHANG_NCCBussiness;
        bool newrow = false;
        public frm_DonViVanChuyen()
        {
            InitializeComponent();
            _DM_KHACHHANG_NCCBussiness = new DM_KHACHHANG_NCCBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.iGridDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            Initialize_Grid();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                //frm_DiaDiemXuatHang a = new frm_DiaDiemXuatHang();
                //if (frm_dm_khachhang_ncc_popup.pLOAIKH_NCC == "KH")
                //{
                //    a.Title = "Nơi xuất hàng của khách hàng " + frm_dm_khachhang_ncc_popup.pTEN_KHNCC;
                //}
                //else
                //    a.Title = "Nơi xuất hàng của nhà cung cấp " + frm_dm_khachhang_ncc_popup.pTEN_KHNCC;
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _DM_KHACHHANG_NCCBussiness.GetListDM_KHACHHANG_NCC_DON_VI_VAN_CHUYEN(ConstCommon.DonViQuanLy, frm_dm_khachhang_ncc_popup.pKHACHHANG_NCC_ID);
                    });
                    Action<DataTable> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iGridDataSource = dt;
                            this.iGridDataSource.Rows.Add();
                            newrow = true;
                            this.iGridHelper.BindItemSource(this.iGridDataSource);
                        }));
                    });
                    task.AsyncTaskExecute(action, onComplete);
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadData()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        public override void OnPopupShown()
        {
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
                xDicUser.Add("KHACHHANG_NCC_DONVI_VANCHUYEN_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("KHACHHANG_NCC_ID", typeof(decimal));
                xDicUser.Add("STT", typeof(int));
                xDicUser.Add("TEN_DONVI_VANCHUYEN", typeof(string));
                xDicUser.Add("DIA_CHI", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(bool));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }

        /// <summary>
        /// Initialize_Grid
        /// </summary>

        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_DONVI_VANCHUYEN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenDonVi"]));
                xColumn.Width = 150;
                xColumn.MaxLenth = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("DIA_CHI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDiaChi"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaxLenth = 250;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuRemark"]));
                xColumn.Width = 200;
                xColumn.MaxLenth = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TINH_TRANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTinhTrang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Combo;
                xColumn.Binding = new System.Windows.Data.Binding("TINH_TRANG") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = InsertTableTinhTrang();
                xColumn.ValueList.DisplayMember = "HIEN_THI_TINH_TRANG";
                xColumn.ValueList.ValueMember = "TINH_TRANG";
                this.iGridHelper.Add(xColumn);

                ButtonEditSettings xButton_Remove = new ButtonEditSettings();
                xButton_Remove.DefaultButtonClick += xButton_RemoveClick;
                xColumn = new Column("REMOVE", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_Remove"]));
                xColumn.Width = 50;
                xColumn.CustomEditor = xButton_Remove;
                xColumn.Visible = true;
                xColumn.ColumnType = ColumnControl.Custom;
                xColumn.MaxLenth = 5;
               // xColumn.HorizontalContentAlignment = EditSettingsHorizontalAlignment.Center;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
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
        private DataTable InsertTableTinhTrang()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.TableName = "TT";
                DataColumn cTT = new DataColumn();
                cTT.ColumnName = "TINH_TRANG";
                cTT.DataType = typeof(bool);
                dt.Columns.Add(cTT);
                DataColumn c_HienThiTT = new DataColumn();
                c_HienThiTT.ColumnName = "HIEN_THI_TINH_TRANG";
                c_HienThiTT.DataType = typeof(string);
                dt.Columns.Add(c_HienThiTT);
                dt.Rows.Add();
                dt.Rows.Add();
                dt.Rows[0][0] = true;
                dt.Rows[0][1] = "Đang hoạt động";
                dt.Rows[1][0] = false;
                dt.Rows[1][1] = "Ngừng hoạt động";
                return dt;
            }
            catch
            {
                return null;
            }
        }
        void xButton_RemoveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;
                if (ConstCommon.NVL_NUM_LONG_NEW(row.Row["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"]) > 0)
                {
                    if (this.iGridDataSource != null && this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"].ToString() != string.Empty)
                    {
                        if (!base.ShowMessage(MessageType.OkCancel, "Bạn muốn xóa không?"))
                            return;
                        bool result = _DM_KHACHHANG_NCCBussiness.DeleteDM_KHACHHANG_NCC_DON_VI_VAN_CHUYEN(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"].ToString()), CommonDataHelper.UserName);
                        if (!result)
                        {
                            base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                            return;
                        }
                        else
                        {
                            ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["tblThongBao"]), Convert.ToString(UtilLanguage.ApplyLanguage()["tblThanhCong"]), notificationService);
                            LoadData();
                            this.iGridDataSource.Rows.Add();
                            return;
                        }
                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, "Chưa có nơi xuất hàng nào được chọn!");
                        return;
                    }
                }
                else
                    this.iGridDataSource.Rows.Remove(row.Row);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool result = true;
                bool flag = true;
                string note = String.Empty;
                if (newrow == true)
                {
                    for (int i = 0; i < this.iGridDataSource.Rows.Count - 1; i++)
                    {
                        if (this.iGridDataSource.Rows[i]["TEN_DONVI_VANCHUYEN"].ToString() == String.Empty || this.iGridDataSource.Rows[i]["TINH_TRANG"].ToString() == String.Empty)
                        {
                            flag = false;
                            if (this.iGridDataSource.Rows[i]["TEN_DONVI_VANCHUYEN"].ToString() == String.Empty)
                            {
                                note = "[TÊN ĐƠN VỊ] ";
                                break;
                            }
                            else
                            {
                                note = "[TÌNH TRẠNG HOẠT ĐỘNG] ";
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < this.iGridDataSource.Rows.Count; i++)
                    {
                        if (this.iGridDataSource.Rows[i]["TEN_DONVI_VANCHUYEN"].ToString() == String.Empty || this.iGridDataSource.Rows[i]["TINH_TRANG"].ToString() == String.Empty)
                        {
                            flag = false;
                            if (this.iGridDataSource.Rows[i]["TEN_DONVI_VANCHUYEN"].ToString() == String.Empty)
                            {
                                note = "[TÊN ĐƠN VỊ] ";
                                break;
                            }
                            else
                            {
                                note = "[TÌNH TRẠNG HOẠT ĐỘNG] ";
                                break;
                            }
                        }
                    }
                }
                if (flag == true)
                {
                    for (int i = 0; i < this.iGridDataSource.Rows.Count; i++)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("KHACHHANG_NCC_DONVI_VANCHUYEN_ID");
                        dt.Columns.Add("STT");
                        dt.Columns.Add("TEN_DONVI_VANCHUYEN");
                        dt.Columns.Add("DIA_CHI");
                        dt.Columns.Add("GHI_CHU");
                        dt.Columns.Add("TINH_TRANG");
                        if (this.iGridDataSource.Rows[i]["TEN_DONVI_VANCHUYEN"].ToString() != String.Empty)
                        {
                            dt.Rows.Add(this.iGridDataSource.Rows[i]["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"].ToString(), (i + 1),
                                this.iGridDataSource.Rows[i]["TEN_DONVI_VANCHUYEN"].ToString(), this.iGridDataSource.Rows[i]["DIA_CHI"].ToString(),
                                this.iGridDataSource.Rows[i]["GHI_CHU"].ToString(), this.iGridDataSource.Rows[i]["TINH_TRANG"].ToString());
                            result = _DM_KHACHHANG_NCCBussiness.InsertorUpdateDON_VI_VAN_CHUYEN(dt, CommonDataHelper.UserName, frm_dm_khachhang_ncc_popup.pKHACHHANG_NCC_ID);
                        }
                    }
                    if (!result)
                    {
                        base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                        return;
                    }
                    else
                    {
                        if (this.iGridDataSource.Rows[0]["TEN_DONVI_VANCHUYEN"].ToString() == String.Empty && this.iGridDataSource.Rows[0]["GHI_CHU"].ToString() == string.Empty&& this.iGridDataSource.Rows[0]["TINH_TRANG"].ToString() == String.Empty)
                            return;
                        if (this.iGridDataSource.Rows[0]["TEN_DONVI_VANCHUYEN"].ToString() == String.Empty && this.iGridDataSource.Rows[0]["TINH_TRANG"].ToString() == String.Empty && this.iGridDataSource.Rows[0]["GHI_CHU"].ToString() != string.Empty)
                            base.ShowMessage(MessageType.Information, "[TÊN ĐƠN VỊ],[TÌNH TRẠNG HOẠT ĐỘNG] chưa chọn.");
                        else
                        {
                            ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                            LoadData();
                        }
                    }
                }
                else
                {
                    base.ShowMessage(MessageType.Information, note + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    return;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if (this.iGridDataSource != null)
                {
                    if ((grd.View as TableView).FocusedRowHandle == this.iGridDataSource.Rows.Count - 1)
                    {

                            if (grd.GetFocusedRowCellValue("TEN_DONVI_VANCHUYEN").ToString() != string.Empty && grd.GetFocusedRowCellValue("TINH_TRANG").ToString() != string.Empty)
                            {
                                this.iGridDataSource.Rows.Add();
                                newrow = true;
                            }
                            else
                            {
                                newrow = false;
                                return;
                            }
                    }

                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_CellValueChanged()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

    }
}