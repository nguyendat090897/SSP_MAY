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
    ///  Editor:      Ngo Gia Thien
    ///  Modify date: 28/03/2018
    ///  Description: khai bao khu vuc theo kho
    /// </summary>
    public partial class frm_khai_bao_khuvuc_theo_kho : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        IDM_KHO_KHUVUCBussiness _DM_KHO_KHUVUCBussiness;
        DataTable dtKhoChon = new DataTable();
        DataRow iRowSelGbKV;
        public frm_khai_bao_khuvuc_theo_kho()
        {
            InitializeComponent();
            _DM_KHO_KHUVUCBussiness = new DM_KHO_KHUVUCBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            Initialize_Grid();
        }

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGb = this.Parameter[0] as DataRow;
                DispalayRequest();
                LoadData();
                if (this.iGridDataSource != null)
                {
                    this.iGridDataSource.Rows.Add();
                }
                if (iRowSelGb != null)
                {
                    this.iDataSource.Rows[0]["KHO_ID"] = this.iRowSelGb["KHO_ID"];
                }
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
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("KHO_ID", typeof(long));
                xDicUser.Add("MA_KHO_KHUVUC", typeof(string));
                xDicUser.Add("TEN_KHO_KHUVUC", typeof(string));
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
                //this.iGridDataSource = _DM_KHO_KHUVUCBussiness.GetListDM_KHO_KHUVUC(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["KHO_ID"].ToString()));
                //this.iGridHelper.BindItemSource(this.iGridDataSource);

                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _DM_KHO_KHUVUCBussiness.GetListDM_KHO_KHUVUC(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["KHO_ID"].ToString()));
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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

                xColumn = new Column("MA_KHO_KHUVUC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaKhoKV"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_KHO_KHUVUC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenKhoKV"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
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
                if (this.iGridDataSource != null && this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["KHO_KHUVUC_ID"].ToString() != string.Empty)
                {
                    if (!base.ShowMessage(MessageType.OkCancel, Convert.ToString(UtilLanguage.ApplyLanguage()["lblcomfrimDelete"])))
                        return;
                    bool result = _DM_KHO_KHUVUCBussiness.DeleteDM_KHO_KHUVUC(CommonDataHelper.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["KHO_KHUVUC_ID"].ToString()), CommonDataHelper.UserName);
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
                else
                {
                    base.ShowMessage(MessageType.Information, "BẠN CHƯA CHỌN KHU VỰC ĐỂ XÓA!");
                    return;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }


        private void btnTTHang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["KHO_KHUVUC_ID"].ToString() == string.Empty)
                {
                    base.ShowMessage(MessageType.Information, "MỜI BẠN CHỌN VÀO VÙNG CÓ GIÁ TRỊ!!!");
                }
                else
                {
                    if (iDataSource.Rows.Count == 0) return;
                    iRowSelGbKV = ((DataRowView)this.grd.GetFocusedRow()).Row;
                    object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_khai_bao_trang_thai_theo_khuvuc", iRowSelGbKV);

                }


            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnTTHang.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnKhachHang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["KHO_KHUVUC_ID"].ToString() == string.Empty)
                {
                    base.ShowMessage(MessageType.Information, "MỜI BẠN CHỌN VÀO VÙNG CÓ GIÁ TRỊ!!!");
                }
                else
                {
                    if (iDataSource.Rows.Count == 0) return;
                    iRowSelGbKV = ((DataRowView)this.grd.GetFocusedRow()).Row;
                    object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_khai_bao_khach_hang_theo_khuvuc", iRowSelGbKV);

                }
                
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnKhachHang.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnSLHang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["KHO_KHUVUC_ID"].ToString() == string.Empty)
                {
                    base.ShowMessage(MessageType.Information, "MỜI BẠN CHỌN VÀO VÙNG CÓ GIÁ TRỊ!!!");
                }
                else
                {
                    if (iDataSource.Rows.Count == 0) return;
                    iRowSelGbKV = ((DataRowView)this.grd.GetFocusedRow()).Row;
                    object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_khai_bao_so_luong_cho_vi_tri", iRowSelGbKV);

                }
                
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSLHang.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
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
               
                    for (int i = 0; i < this.iGridDataSource.Rows.Count - 1; i++)
                    {
                        DataTable dtKhoChon = new DataTable();
                        dtKhoChon.Columns.Add("KHO_KHUVUC_ID");
                        dtKhoChon.Columns.Add("KHO_ID");
                        dtKhoChon.Columns.Add("MA_KHO_KHUVUC");
                        dtKhoChon.Columns.Add("TEN_KHO_KHUVUC");
                        dtKhoChon.Columns.Add("GHI_CHU");
                        dtKhoChon.Columns.Add("TINH_TRANG");
                        dtKhoChon.Columns.Add("RowVersion");

                        DataRow drNew = dtKhoChon.NewRow();
                        drNew["KHO_KHUVUC_ID"] = this.iGridDataSource.Rows[i]["KHO_KHUVUC_ID"];
                        drNew["KHO_ID"] = iRowSelGb["KHO_ID"].ToString();
                        drNew["MA_KHO_KHUVUC"] = this.iGridDataSource.Rows[i]["MA_KHO_KHUVUC"];
                        drNew["TEN_KHO_KHUVUC"] = this.iGridDataSource.Rows[i]["TEN_KHO_KHUVUC"];
                        drNew["GHI_CHU"] = this.iGridDataSource.Rows[i]["GHI_CHU"];
                        drNew["TINH_TRANG"] = this.iGridDataSource.Rows[i]["TINH_TRANG"];

                        dtKhoChon.Rows.Add(drNew);

                        if (dtKhoChon != null)
                        {
                            this.iGridHelper.BindItemSource(dtKhoChon);
                        }
                        else
                        {
                            grd.ItemsSource = null;
                        }

                        result = _DM_KHO_KHUVUCBussiness.InsertOrUpdateDM_KHO_KHUVUC(dtKhoChon);
                    }
                    if (!result)
                    {

                        base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                        return;
                    }
                    else
                    {
                        ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                        OnPopupShown();
                        return;
                    }
            }
            catch (Exception ex)
            {

                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdView_CellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
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

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
                LoadData();
        }

        private bool checkNull()
        {
            for (int i = 0; i < this.iGridDataSource.Rows.Count - 1; i++)
            {
                if (this.iGridDataSource.Rows[i]["MA_KHO_KHUVUC"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, " MÃ KHU VỰC KHÔNG ĐƯỢC BỎ TRỐNG!!! ");
                    return false;
                }
                if (this.iGridDataSource.Rows[i]["TEN_KHO_KHUVUC"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, " TÊN KHU VỰC KHÔNG ĐƯỢC BỎ TRỐNG!!! ");
                    return false;
                }
                if (this.iGridDataSource.Rows[i]["TINH_TRANG"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, " BẠN CHƯA CHỌN TÌNH TRẠNG SỬ DỤNG!!! ");
                    return false;
                }
                if(ConstCommon.check_String_Unicode(this.iGridDataSource.Rows[i]["MA_KHO_KHUVUC"].ToString()) == false)
                {
                    base.ShowMessage(MessageType.Information, "VUI LÒNG KHÔNG NHẬP KÝ TỰ ĐẶC BIỆT VÀO Ô MÃ KHU VỰC");
                    return false;
                }
                if(_DM_KHO_KHUVUCBussiness.DM_KHO_KHUVUC_CHECKEXISTS(ConstCommon.DonViQuanLy,ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[i]["KHO_KHUVUC_ID"]), ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["KHO_ID"]), this.iGridDataSource.Rows[i]["MA_KHO_KHUVUC"].ToString()) == true)
                {
                    base.ShowMessage(MessageType.Information, "MÃ KHU VỰC ĐÃ TỒN TẠI. VUI LÒNG NHẬP MÃ KHU VỰC KHÁC");
                    return false;
                }
            }
            return true;
        }
    }

}
