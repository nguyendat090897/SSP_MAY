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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using DSofT.Framework.UICore.TaskProxy;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors;
using System.Configuration;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;
using System.Drawing;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 02/03/2018
    ///  Editor:      Ngo Gia Thien
    ///  Modify date: 20/03/2108
    ///  Description: khai bao vi tri kho
    /// </summary>
    public partial class frm_khai_bao_vi_tri_kho : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        IDM_KHO_VITRIBussiness _DM_KHO_VITRIBussiness;
        DataTable dtKhoChon = new DataTable();
        public static DataTable tbl = new DataTable();

        public frm_khai_bao_vi_tri_kho()
        {
            InitializeComponent();
            _DM_KHO_VITRIBussiness = new DM_KHO_VITRIBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            tbl = this.MA_VACH();
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
                xDicUser.Add("CHON", typeof(string));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("KHO_VITRI_ID", typeof(decimal));
                xDicUser.Add("KHO_ID", typeof(long));
                xDicUser.Add("VITRI", typeof(string));
                xDicUser.Add("MA_VACH", typeof(string));
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
                //this.iGridDataSource = _DM_KHO_VITRIBussiness.GetListDM_KHO_VITRI(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["KHO_ID"].ToString()));
                //this.iGridHelper.BindItemSource(this.iGridDataSource);
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _DM_KHO_VITRIBussiness.GetListDM_KHO_VITRI(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["KHO_ID"].ToString()));
                    });
                    Action<DataTable> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            dt.Columns.Add("CHON");
                            for (int i = 0; i < dt.Rows.Count - 1; i++)
                            {
                                dt.Rows[i]["CHON"] = "0";
                            }
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


        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;

                xColumn = new Column("CHON", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_Chon"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("CHON") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                xColumn.AllowSorting = false;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTri"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaVach"]));
                xColumn.Width = 200;
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

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void xButton_RemoveClick(object sender, RoutedEventArgs e)
        {

            try
            {
                if (this.iGridDataSource != null && this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["KHO_VITRI_ID"].ToString() != string.Empty)
                {
                    if (!base.ShowMessage(MessageType.OkCancel, Convert.ToString(UtilLanguage.ApplyLanguage()["lblcomfrimDelete"])))
                        return;
                    bool result = _DM_KHO_VITRIBussiness.DeleteDM_KHO_VITRI(CommonDataHelper.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["KHO_VITRI_ID"].ToString()), CommonDataHelper.UserName);
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
                    base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN VỊ TRÍ ĐỂ XÓA!");
                    return;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private bool checkNull()
        {
            for (int i = 0; i < this.iGridDataSource.Rows.Count - 1; i++)
            {
                if (this.iGridDataSource.Rows[i]["VITRI"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, " VỊ TRÍ KHÔNG ĐƯỢC BỎ TRỐNG!!! ");
                    return false;
                }
                if (this.iGridDataSource.Rows[i]["TINH_TRANG"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, " BẠN CHƯA CHỌN TÌNH TRẠNG SỬ DỤNG!!! ");
                    return false;
                }
                if (_DM_KHO_VITRIBussiness.DM_KHO_VITRI_CHECKEXISTS(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[i]["KHO_VITRI_ID"]), ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["KHO_ID"]), this.iGridDataSource.Rows[i]["VITRI"].ToString()) == true)
                {
                    base.ShowMessage(MessageType.Information, " VỊ TRÍ ĐÃ TỒN TẠI. VUI LÒNG NHẬP VỊ TRÍ KHÁC");
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    dtKhoChon.Columns.Add("KHO_VITRI_ID");
                    dtKhoChon.Columns.Add("KHO_ID");
                    dtKhoChon.Columns.Add("VITRI");
                    dtKhoChon.Columns.Add("MA_VACH");
                    dtKhoChon.Columns.Add("TINH_TRANG");
                    dtKhoChon.Columns.Add("RowVersion");

                    DataRow drNew = dtKhoChon.NewRow();
                    drNew["KHO_VITRI_ID"] = this.iGridDataSource.Rows[i]["KHO_VITRI_ID"].ToString();
                    drNew["KHO_ID"] = iRowSelGb["KHO_ID"].ToString();
                    drNew["VITRI"] = this.iGridDataSource.Rows[i]["VITRI"];
                    drNew["MA_VACH"] = this.iGridDataSource.Rows[i]["MA_VACH"];
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

                    result = _DM_KHO_VITRIBussiness.InsertOrUpdateDM_KHO_VITRI(dtKhoChon);
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
                if (iGridDataSource != null)
                {

                    if ((grd.View as TableView).FocusedRowHandle == iGridDataSource.Rows.Count - 1 && e.Column.Name!="CHON")
                    {

                        iGridDataSource.Rows.Add();

                    }

                    for (int i = 0; i < iGridDataSource.Rows.Count - 1; i++)
                    {
                        this.iGridDataSource.Rows[i]["MA_VACH"] = this.iGridDataSource.Rows[i]["VITRI"];
                    }
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            OnPopupShown();
        }

        //Trần Lê Minh
        public static XRBarCode CreateCode128BarCode(string BarCodeText, int i)
        {
            // Create a bar code control.
            XRBarCode barCode = new XRBarCode();

            // Set the bar code's type to Code 128.
            barCode.Symbology = new Code128Generator();

            // Adjust the bar code's main properties.
            barCode.Text = BarCodeText;
            barCode.Width = 400;
            barCode.Height = 100;
            barCode.LocationF = new PointF(200F, 150F * i);
            barCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            // Adjust the properties specific to the bar code type.
            ((Code128Generator)barCode.Symbology).CharacterSet = Code128Charset.CharsetAuto;

            return barCode;
        }

        private DataTable MA_VACH()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("pBarCodeVitri", typeof(string));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }

        private void btnMacvach_Click(object sender, RoutedEventArgs e)
        {
            tbl.Clear();
            try
            {

                for (int i = 0; i < iGridDataSource.Rows.Count; i++)
                {
                    if (iGridDataSource.Rows[i]["CHON"].ToString() == "1")
                    {
                        tbl.Rows.Add(iGridDataSource.Rows[i]["MA_VACH"].ToString());
                    }
                }
                Rpt_NhapXuat.tbl = tbl;
                Rpt_NhapXuat rpt = new Rpt_NhapXuat();
                //rpt.DataSource = tbl;
                rpt.Parameters["txtTieude"].Value = "Danh sách mã vạch";
                XtraReportPreviewModel model = new XtraReportPreviewModel(rpt);
                rpt.CreateDocument(true);
                model.IsParametersPanelVisible = false;
                //DocumentPreview1.Model = model;


                model.ParametersModel.SubmitParameters();
                DocumentPreviewWindow dpw = new DocumentPreviewWindow();
                dpw.Model = model;
                dpw.ShowDialog();
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

        }
    }

}
