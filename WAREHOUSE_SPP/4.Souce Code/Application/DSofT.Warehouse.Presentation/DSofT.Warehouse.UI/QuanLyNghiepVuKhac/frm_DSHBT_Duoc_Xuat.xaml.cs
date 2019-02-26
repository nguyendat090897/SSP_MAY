using DSofT.Warehouse.Business;
using DSofT.Warehouse.Resources;
using DevExpress.Xpf.Grid;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DSofT.Warehouse.Log.UtilHelper;
using DSofT.Framework.UICore.TaskProxy;
using DevExpress.Xpf.Editors;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 06/02/2018
    ///  Editor:   Nguyen Van Huynh   
    ///  Modify date: 10/05/2018
    ///  Description: Danh sach hang biet tru duoc phep xuat
    /// </summary>
    public partial class frm_DSHBT_Duoc_Xuat : ControlBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper_CTPHIEUNHAP = null;
        DataTable iGridDataSource = null;
        DataRow iRowSelGb = null;
        IKO_PHIEU_BT_CHOPHEPXUATBussiness _KO_PHIEU_BT_CHOPHEPXUATBussiness;
        DataSet iGridDataSource_CTPHIEUNHAP = null;
        public frm_DSHBT_Duoc_Xuat()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            Initialize_Grid();
            Initialize_Grid_CTPHIEUNHAP();
            this.DataContext = this.iDataSource;
            _KO_PHIEU_BT_CHOPHEPXUATBussiness = new KO_PHIEU_BT_CHOPHEPXUATBussiness(string.Empty);
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataSet> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _KO_PHIEU_BT_CHOPHEPXUATBussiness.KO_PHIEU_BT_CHOPHEPXUAT_GET_ALL(ConstCommon.DonViQuanLy);
                    });
                    Action<DataSet> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iGridDataSource = dt.Tables[0];
                            this.iGridDataSource_CTPHIEUNHAP = dt.Copy();
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
                xDicUser.Add("PHIEU_BT_CHOPHEPXUAT_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("SOPHIEU", typeof(string));
                xDicUser.Add("NGAY_CHOPHEP", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TEN_KHO", typeof(string));
                xDicUser.Add("KHO_ID", typeof(decimal));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("NGUOI_THUC_HIEN", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
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

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SOPHIEU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoPhieu"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAY_CHOPHEP", Convert.ToString(UtilLanguage.ApplyLanguage()["frmReportMaterialWeek_DateMenu"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_KHO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhoBietTru"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                this.iGridHelper.Initialize();
                this.grdView.AutoWidth = true;
                ConstCommon.setAutoFilterConditionGrid(grd);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void Initialize_Grid_CTPHIEUNHAP()
        {
            try
            {
                this.iGridHelper_CTPHIEUNHAP = new GridHelper(this.grdChiTiet, this.grdViewChiTiet, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("TEN_KHO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKho"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("TEN_KHO_KHUVUC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);//combobox

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_MaThuoc"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuoc1"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);


                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);


                xColumn = new Column("TU_NGAY", "Từ ngày");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("DEN_NGAY", "Đến ngày");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("TEN_TINHTRANG_HANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChangeStatus"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("IS_CHOPHEP", "Cho phép xuất");
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("IS_CHOPHEP") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);//CHECKBOX

                this.iGridHelper_CTPHIEUNHAP.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_DSHBT_Duoc_Xuat_popup", null);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void grdView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                object xReturn = null;
                if (iDataSource.Rows.Count == 0) return;
                if (this.grd.GetFocusedRow() == null) return;
                DataRow[] iRowSelGb_ctiet = iGridDataSource_CTPHIEUNHAP.Tables[1].Select("PHIEU_BT_CHOPHEPXUAT_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_BT_CHOPHEPXUAT_ID"]));
                xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_DSHBT_Duoc_Xuat_popup", iRowSelGb, iRowSelGb_ctiet);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_MouseDoubleClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnInPhieu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                iRowSelGb = null;
                if (!base.ShowMessage(MessageType.OkCancel, Convert.ToString(UtilLanguage.ApplyLanguage()["lblcomfrimDelete"])))
                    return;
                if (this.grd.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)grd.GetFocusedRow()).Row;

                bool result = _KO_PHIEU_BT_CHOPHEPXUATBussiness.KO_PHIEU_BT_CHOPHEPXUAT_DELETE(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_BT_CHOPHEPXUAT_ID"].ToString()), CommonDataHelper.UserName);
                if (!result)
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["tblKhongThanhCong"]));
                    return;
                }
                else
                {
                    ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["tblThongBao"]), Convert.ToString(UtilLanguage.ApplyLanguage()["tblThanhCong"]), notificationService);
                }
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnDelete.Tag.ToString());
                base.ShowMessage(MessageType.Error, "Vui lòng chọn dữ liệu cần xóa", ex);
            }
        }

        private void grdView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (this.grd.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)grd.GetFocusedRow()).Row;
                DispalayRequest();
                DataRow[] iRowSelGb_CTPHIEUNHAP = iGridDataSource_CTPHIEUNHAP.Tables[1].Select("PHIEU_BT_CHOPHEPXUAT_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_BT_CHOPHEPXUAT_ID"]));
                if (iRowSelGb_CTPHIEUNHAP.Length > 0)
                {
                    this.iGridHelper_CTPHIEUNHAP.BindItemSource(iRowSelGb_CTPHIEUNHAP.CopyToDataTable());
                }
                else
                {
                    grdChiTiet.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnDelete.Tag.ToString());
                // base.ShowMessage(MessageType.Error, "Vui lòng chọn dữ liệu cần xóa", ex);
                return;
            }
        }
    }
}
