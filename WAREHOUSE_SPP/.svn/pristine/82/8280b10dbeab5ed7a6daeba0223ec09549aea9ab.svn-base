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
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DSofT.Framework.UICore.TaskProxy;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 2/03/2018
    ///  Editor:      Ngo Gia Thien
    ///  Modify date: 29/03/2018
    ///  Description: Khai bao so luong cho vi tri
    /// </summary>
    public partial class frm_khai_bao_so_luong_cho_vi_tri : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        DataTable dt_PHIEU_CTIET = null;
        DataTable iGridDataSource = null;
        IDM_SANPHAM_SLUONG_MINMAX_KHUVUCBussiness _DM_SANPHAM_SLUONG_MINMAX_KHUVUCBussiness;
        public frm_khai_bao_so_luong_cho_vi_tri()
        {
            InitializeComponent();
            _DM_SANPHAM_SLUONG_MINMAX_KHUVUCBussiness = new DM_SANPHAM_SLUONG_MINMAX_KHUVUCBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.iGridDataSource = this.TableSchemaDefineBingding();
            this.iGridDataSource.Clear();
            Initialize_Grid();
            this.DataContext = this.iDataSource;
        }

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGb = this.Parameter[0] as DataRow;
                DispalayRequest();
                LoadData();
                if (iRowSelGb != null)
                {
                    this.iDataSource.Rows[0]["KHO_KHUVUC_ID"] = iRowSelGb["KHO_KHUVUC_ID"];
                }

                if (this.Parameter.Count() > 1)
                {
                    DataRow[] dr_chitiet = this.Parameter[1] as DataRow[];

                    dt_PHIEU_CTIET.Clear();
                    dt_PHIEU_CTIET = dr_chitiet.CopyToDataTable();
                    if (dt_PHIEU_CTIET != null)
                    {
                        this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }
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

        private void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _DM_SANPHAM_SLUONG_MINMAX_KHUVUCBussiness.GetListDM_SANPHAM_SLUONG_MINMAX_KHUVUC(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["KHO_KHUVUC_ID"].ToString())); ;
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

        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("SANPHAM_SLUONG_MINMAX_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));

                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));

                xDicUser.Add("SL_MIN", typeof(decimal));
                xDicUser.Add("SL_MAX", typeof(decimal));
                xDicUser.Add("VITRI", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(bool));
;

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


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaSanPham"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenSanPham"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDVT"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SL_MIN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongMIN"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SL_MAX", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongMAX"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTri"]));
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


                this.iGridHelper.Initialize();
                this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private bool ContainDataRowInDataTable(DataTable dt, DataRow dr)
        {
            try
            {
                var k = (from r in dt.Rows.OfType<DataRow>() where r["SANPHAM_ID"].ToString() == dr["SANPHAM_ID"].ToString() select r).FirstOrDefault();
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

        private void btnXoadong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool msg = false;
                if ((this.iGridDataSource == null) || (this.iGridDataSource.Rows.Count == 0))
                {
                    return;
                }

                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;

                if (ConstCommon.NVL_NUM_LONG_NEW(row["SANPHAM_SLUONG_MINMAX_KHUVUC_ID"]) > 0)
                {
                    msg = (bool)base.ShowMessage(MessageType.OkCancel, "BẠN CÓ MUỐN XÓA DÒNG ĐANG CHỌN KHÔNG???");
                    if (msg == true)
                    {
                        DataTable dsReturn = _DM_SANPHAM_SLUONG_MINMAX_KHUVUCBussiness.DeleteDM_SANPHAM_SLUONG_MINMAX_KHUVUC(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(row.Row["SANPHAM_SLUONG_MINMAX_KHUVUC_ID"]), CommonDataHelper.UserName);
                        if ((dsReturn == null) || dsReturn.Rows.Count == 0 || dsReturn.Rows[0][0].ToString() == "0")
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                iGridDataSource.Rows.Remove(row.Row);


                this.iGridHelper.BindItemSource(this.iGridDataSource);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSP.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnChonSP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP", null);
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
                                dr["SANPHAM_SLUONG_MINMAX_KHUVUC_ID"] = "0";
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];                               
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                dr["MA_DONVI_TINH"] = dt_SPCHON.Rows[i]["MA_DONVI_TINH"];
                                iGridDataSource.Rows.Add(dr);
                            }
                        }
                    }
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSP.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        /// <summary>
        /// Kiem tra grd null 
        /// </summary>
        /// <returns></returns>
        private bool checkNull()
        {
            if (this.iGridDataSource.Rows.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < this.iGridDataSource.Rows.Count; i++)
            {
                if (this.iGridDataSource.Rows[i]["SL_MIN"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, " SÔ LƯỢNG MIN KHÔNG ĐƯỢC BỎ TRỐNG!!! ");
                    return false;
                }

                if (this.iGridDataSource.Rows[i]["SL_MAX"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, " SÔ LƯỢNG MAX KHÔNG ĐƯỢC BỎ TRỐNG!!! ");
                    return false;
                }

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
                if (ConstCommon.NVL_NUM_DECIMAL_NEW(this.iGridDataSource.Rows[i]["SL_MIN"].ToString()) > ConstCommon.NVL_NUM_DECIMAL_NEW(this.iGridDataSource.Rows[i]["SL_MAX"].ToString()))
                {
                    base.ShowMessage(MessageType.Information, "SỐ LƯỢNG MIN PHẢI NHỎ HƠN SỐ LƯỢNG MAX!!!");
                    return false;
                }
            }
            return true;
        }


        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if(!checkNull())
                {
                    return;
                }

                bool result = true;

                for (int i = 0; i < this.iGridDataSource.Rows.Count; i++)
                {
                    DataTable dtKhoChon = new DataTable();

                    dtKhoChon.Columns.Add("SANPHAM_SLUONG_MINMAX_KHUVUC_ID");
                    dtKhoChon.Columns.Add("KHO_KHUVUC_ID");
                    dtKhoChon.Columns.Add("SANPHAM_ID");
                    dtKhoChon.Columns.Add("SL_MIN");
                    dtKhoChon.Columns.Add("SL_MAX");
                    dtKhoChon.Columns.Add("VITRI");
                    dtKhoChon.Columns.Add("GHI_CHU");
                    dtKhoChon.Columns.Add("TINH_TRANG");

                    DataRow drNew = dtKhoChon.NewRow();
                    drNew["SANPHAM_SLUONG_MINMAX_KHUVUC_ID"] = this.iGridDataSource.Rows[i]["SANPHAM_SLUONG_MINMAX_KHUVUC_ID"];
                    drNew["KHO_KHUVUC_ID"] = iRowSelGb["KHO_KHUVUC_ID"].ToString();
                    drNew["SANPHAM_ID"] = this.iGridDataSource.Rows[i]["SANPHAM_ID"];
                    drNew["SL_MIN"] = this.iGridDataSource.Rows[i]["SL_MIN"];
                    drNew["SL_MAX"] = this.iGridDataSource.Rows[i]["SL_MAX"];
                    drNew["VITRI"] = this.iGridDataSource.Rows[i]["VITRI"];
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

                    result = _DM_SANPHAM_SLUONG_MINMAX_KHUVUCBussiness.InsertOrUpdateDM_SANPHAM_SLUONG_MINMAX_KHUVUC(dtKhoChon);
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
                    return;
                }


            }
            catch (Exception ex)
            {

                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

    }
}
