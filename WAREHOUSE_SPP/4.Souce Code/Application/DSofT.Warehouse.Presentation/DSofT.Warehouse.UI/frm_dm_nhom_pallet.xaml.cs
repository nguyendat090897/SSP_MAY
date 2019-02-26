using DSofT.Warehouse.Business;
using DSofT.Warehouse.Resources;
using DevExpress.Xpf.Grid;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using DSofT.Warehouse.Log.UtilHelper;
using DSofT.Framework.UICore.TaskProxy;
using System.Windows.Data;
namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 16/03/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Danh muc nhom pallet
    ///  DEV: Nguyen Van Huynh
    /// </summary>
    public partial class frm_dm_nhom_pallet : ControlBase
    {
        DataRow iRowSelGb = null;
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        IDM_NHOM_PALLETBussiness _DM_NHOM_PALLETBussiness;
        public static bool status = true;
        public frm_dm_nhom_pallet()
        {
            InitializeComponent();
            _DM_NHOM_PALLETBussiness = new DM_NHOM_PALLETBussiness(string.Empty);
            Initialize_Grid();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadData();
        }
        /// <summary>
        /// TableSchemaDefineBingding
        /// </summary>
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
                xDicUser.Add("HIEN_THI_TINH_TRANG", typeof(string));
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

                xColumn = new Column("TEN_NHOM_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTEN_NHOM_PALLET"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("LOAI_KTHUOC_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKichThuoc"]));
                xColumn.Width = 300;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_SIZE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKichThuoc"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("LOAI_PALLET_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenLoaiPallet"]));
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_LOAI_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenLoaiPallet"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NHA_SXUAT_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenNhaSX"]));
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_NHA_SXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenNhaSX"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_PL_THEO_NSX", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMA_PL_THEO_NSX"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_PL_THEO_NSX", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTEN_PL_THEO_NSX"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuRemark"]));
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("HIEN_THI_TINH_TRANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTinhTrang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TINH_TRANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTinhTrang"]));
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                this.iGridHelper.Initialize();
                this.grdView.AutoWidth = true;
                setAutoFilterConditionGrid(grd);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void setAutoFilterConditionGrid(GridControl grd)
        {
            try
            {
                for (int i = 0; i < grd.Columns.Count; i++)
                {
                    grd.Columns[i].ColumnFilterMode = ColumnFilterMode.DisplayText;
                    grd.Columns[i].AutoFilterCondition = AutoFilterCondition.Contains;

                }
            }
            catch
            {

            }
        }
        public void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _DM_NHOM_PALLETBussiness.GetListDM_NHOM_PALLET(ConstCommon.DonViQuanLy);
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadData()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            status = true;
            object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_dm_nhom_pallet_popup", null);
            LoadData();
        }

        private void grdView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            status = false;
            try
            {
                if (iDataSource.Rows.Count == 0) return;
                iRowSelGb = ((DataRowView)this.grd.GetFocusedRow()).Row;
                if (iRowSelGb == null) return;
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_dm_nhom_pallet_popup", iRowSelGb);
                LoadData();
            }
            catch 
            {
                return;
            }
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
                bool result = _DM_NHOM_PALLETBussiness.DeleteDM_NHOM_PALLET(ConstCommon.DonViQuanLy, Convert.ToInt32(iRowSelGb["NHOM_PALLET_ID"].ToString()), CommonDataHelper.UserName);
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
            if (this.grd.GetFocusedRow() == null) return;
        }

        private void grdView_FocusedRowHandleChanged(object sender, FocusedRowHandleChangedEventArgs e)
        {
        }
    }
}
