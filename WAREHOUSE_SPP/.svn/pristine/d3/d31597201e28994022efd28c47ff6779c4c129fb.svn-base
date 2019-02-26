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
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors.Settings;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 02/03/2018
    ///  Editor:   Ngo Gia Thien   
    ///  Modify date: 28/03/2018
    ///  Description: khai bao trang thai khu vuc theo ko
    /// </summary>
    public partial class frm_khai_bao_trang_thai_theo_khuvuc : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGbKV = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        IDM_KHO_KHUVUC_HANGBussiness _DM_KHO_KHUVUC_HANGBussiness;
        DataTable dtKhoChon = new DataTable();
        DataTable dtTT = null;
        public frm_khai_bao_trang_thai_theo_khuvuc()
        {
            InitializeComponent();
            _DM_KHO_KHUVUC_HANGBussiness = new DM_KHO_KHUVUC_HANGBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            BultCombobox();
            Initialize_Grid();
        }

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGbKV = Parameter[0] as DataRow;
                DispalayRequest();
                LoadData();
                if (this.iGridDataSource != null)
                {
                    this.iGridDataSource.Rows.Add();
                }
                if (iRowSelGbKV != null)
                {
                    this.iDataSource.Rows[0]["KHO_ID"] = iRowSelGbKV["KHO_ID"];
                    this.iDataSource.Rows[0]["KHO_KHUVUC_ID"] = iRowSelGbKV["KHO_KHUVUC_ID"];
                }
            }
        }

        public void DispalayRequest()
        {
            try
            {
                foreach (DataColumn item in this.iRowSelGbKV.Table.Columns)
                {
                    if (this.iDataSource.Columns[item.ColumnName] != null)
                    {
                        this.iDataSource.Rows[0][item.ColumnName] = iRowSelGbKV[item.ColumnName];
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
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("KHO_KHUVUC_HANG_ID", typeof(decimal));
                xDicUser.Add("KHO_ID", typeof(long));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("MA_TINHTRANG_HANG", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(Int16));
                xDicUser.Add("RowVersion", typeof(long));
                xDicUser.Add("IsDelete", typeof(string));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
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
                        return _DM_KHO_KHUVUC_HANGBussiness.GetListDM_KHO_KHUVUC_HANG(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGbKV["KHO_ID"].ToString()), ConstCommon.NVL_NUM_LONG_NEW(iRowSelGbKV["KHO_KHUVUC_ID"].ToString()));
                    });
                    Action<DataTable> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iGridDataSource = dt;
                            this.iGridDataSource.Rows.Add();
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

        private void BultCombobox()
        {
            dtTT = _DM_KHO_KHUVUC_HANGBussiness.GetListDM_KHO_KHUVUC_HANG_TENTT(ConstCommon.DonViQuanLy);
        }

        private DataTable TaoDatable_TT()
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
                c_HienThiTT.ColumnName = "HIENTHI_TT";
                c_HienThiTT.DataType = typeof(string);
                dt.Columns.Add(c_HienThiTT);

                dt.Rows.Add();
                dt.Rows.Add();

                dt.Rows[0][0] = true;
                dt.Rows[0][1] = "Sử dụng";
                dt.Rows[1][0] = false;
                dt.Rows[1][1] = "Không sử dụng";


                return dt;
            }
            catch
            {

                return null;
            }
        }

        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 40;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                string[] header = new string[] { "Trạng thái hàng" };
                string[] id = new string[] { "TEN_TINHTRANG_HANG" };
                string[] align = new string[] { "left" };

                xColumn = new Column("MA_TINHTRANG_HANG", "Trạng thái hàng");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.Binding = new System.Windows.Data.Binding("MA_TINHTRANG_HANG") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dtTT;
                xColumn.ValueList.DisplayMember = "TEN_TINHTRANG_HANG";
                xColumn.ValueList.ValueMember = "MA_TINHTRANG_HANG";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dtTT, header, id, align);
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGhiChu"]));
                xColumn.Width = 100;
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
                xColumn.ValueList.DataSource = TaoDatable_TT();
                xColumn.ValueList.DisplayMember = "HIENTHI_TT";
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
                xColumn.HorizontalContentAlignment = EditSettingsHorizontalAlignment.Center;
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
        void xButton_RemoveClick(object sender, RoutedEventArgs e)
        {

            try
            {
                if (this.iGridDataSource != null && this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["KHO_KHUVUC_HANG_ID"].ToString() != string.Empty)
                {
                    if (!base.ShowMessage(MessageType.OkCancel, Convert.ToString(UtilLanguage.ApplyLanguage()["lblcomfrimDelete"])))
                        return;
                    bool result = _DM_KHO_KHUVUC_HANGBussiness.DeleteDM_KHO_KHUVUC_HANG(CommonDataHelper.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["KHO_KHUVUC_HANG_ID"].ToString()), CommonDataHelper.UserName);
                    if (!result)
                    {
                        base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                        OnPopupShown();
                        return;
                    }
                    else
                    {
                        ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                        OnPopupShown();
                        return;

                    }
                }
                else
                {
                    base.ShowMessage(MessageType.Information, "BẠN CHƯA CHỌN KHU VỰC HÀNG ĐỂ XÓA!");
                    return;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnRefesh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadData();
                OnPopupShown();
            }
            catch (Exception ex)
            {

                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            
        }

        private bool checkNull()
        {
            for(int i = 0; i < this.iGridDataSource.Rows.Count - 1; i++)
            {
                if (this.iGridDataSource.Rows[i]["MA_TINHTRANG_HANG"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN TRẠNG THÁI HÀNG");
                    return false;
                }
                if (this.iGridDataSource.Rows[i]["TINH_TRANG"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, " BẠN CHƯA CHỌN TÌNH TRẠNG SỬ DỤNG");
                    return false;
                }
                if(_DM_KHO_KHUVUC_HANGBussiness.DM_KHO_KHUVUC_HANG_CHECKEXISTS(ConstCommon.DonViQuanLy,ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[i]["KHO_KHUVUC_HANG_ID"]), ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["KHO_ID"]), ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["KHO_KHUVUC_ID"]), this.iGridDataSource.Rows[i]["MA_TINHTRANG_HANG"].ToString()) == true)
                {
                    base.ShowMessage(MessageType.Information, " TRẠNG THÁI HÀNG ĐÃ TỒN TẠI ");
                    return false;
                }
            }
            return true;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (checkNull() == false)
                {
                    return;
                }

                bool result = true;
               
                    for(int i = 0; i < iGridDataSource.Rows.Count - 1;i++)
                    {
                        DataTable dtKhoChon = new DataTable();
                        dtKhoChon.Columns.Add("KHO_KHUVUC_HANG_ID");
                        dtKhoChon.Columns.Add("KHO_ID");
                        dtKhoChon.Columns.Add("KHO_KHUVUC_ID");
                        dtKhoChon.Columns.Add("MA_TINHTRANG_HANG");
                        dtKhoChon.Columns.Add("GHI_CHU");
                        dtKhoChon.Columns.Add("TINH_TRANG");
                        dtKhoChon.Columns.Add("RowVersion");

                        DataRow drNew = dtKhoChon.NewRow();
                        drNew["KHO_KHUVUC_HANG_ID"] = this.iGridDataSource.Rows[i]["KHO_KHUVUC_HANG_ID"].ToString();
                        drNew["KHO_ID"] = iRowSelGbKV["KHO_ID"].ToString();
                        drNew["KHO_KHUVUC_ID"] = iRowSelGbKV["KHO_KHUVUC_ID"].ToString();
                        drNew["MA_TINHTRANG_HANG"] = this.iGridDataSource.Rows[i]["MA_TINHTRANG_HANG"].ToString(); 
                        drNew["GHI_CHU"] = this.iGridDataSource.Rows[i]["GHI_CHU"].ToString(); 
                        drNew["TINH_TRANG"] = this.iGridDataSource.Rows[i]["TINH_TRANG"].ToString();
                        dtKhoChon.Rows.Add(drNew);

                        if(dtKhoChon != null)
                        {
                            this.iGridHelper.BindItemSource(dtKhoChon);
                        }
                        else
                        {
                            grd.ItemsSource = null;
                        }

                        result = _DM_KHO_KHUVUC_HANGBussiness.InsertOrUpdateDM_KHO_KHUVUC_HANG(dtKhoChon);
                    }
                    


                    if (!result)
                    {

                        base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                        return;
                    }
                    else
                    {
                        ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                        LoadData();
                        this.iGridDataSource.Rows.Add();
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

                        this.iGridDataSource.Rows.Add();

                    }

                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }

}
