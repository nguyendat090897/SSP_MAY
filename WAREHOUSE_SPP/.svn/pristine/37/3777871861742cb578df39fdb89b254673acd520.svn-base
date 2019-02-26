using System;
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
using DSofT.Framework.UICore.TaskProxy;
using System.Windows.Data;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Ngo Gia Thien
    ///  Create date: 22/03/2018
    ///  Editor:
    ///  Modify date:
    ///  Description: Xay dung dinh muc cho san pham khuyen mai
    /// </summary>
    public partial class frm_XDDinhMuc_ChoSP_KhuyenMai : ControlBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper_ctiet_nvl_sp = null;
        GridHelper iGridHelper_ctiet_sp_dg = null;
        DataTable iGridDataSource = null;
        DataSet iGridDataSource_dataset = null;
        DataRow iRowSelGb = null;
        IXD_DM_SPKMAIBussiness XD_DM_SPKMAIBussiness;
        IXD_DM_TTTVIETBussiness _XD_DM_TTTVIETBussiness;
        public static bool status = false;
        public frm_XDDinhMuc_ChoSP_KhuyenMai()
        {
            InitializeComponent();
            XD_DM_SPKMAIBussiness = new XD_DM_SPKMAIBussiness(string.Empty);
            _XD_DM_TTTVIETBussiness = new XD_DM_TTTVIETBussiness(string.Empty);
            Initialize_Grid();
            Initialize_Grid_ctiet_sp_dg();
            Initialize_Grid_ctiet_NVL_SP();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadData();
        }
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("DINHMUC_TEMTOA_ID", typeof(decimal));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
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
                    ServiceAction<DataSet> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return XD_DM_SPKMAIBussiness.getHT_DINHMUC_TEMTOA_ALL(ConstCommon.DonViQuanLy);
                    });
                    Action<DataSet> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iGridDataSource = dt.Tables[0];
                            this.iGridDataSource_dataset = dt.Copy();
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

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuoc"]));
                xColumn.Width = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenThuoc"]));
                xColumn.Width = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SOLUONG_DONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSLDThung"]));
                xColumn.Width = 150;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH_DONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["frmQuantitativeFood_UnitName"]));
                xColumn.Width = 150;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("CHIPHI_DICHVU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChiPhiDichVu"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NHANTHEM_TYLE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTyLe"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GIANHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieuxuat_tuPYC_GiaNhap"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GIABAN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaBan"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGhiChu"]));
                xColumn.Width = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;

                this.iGridHelper.Add(xColumn);
                this.iGridHelper.Initialize();
                //this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        /// <summary>
        /// Initialize_Grid_ctiet_sp_dg
        /// </summary>
        private void Initialize_Grid_ctiet_sp_dg()
        {
            try
            {
                this.iGridHelper_ctiet_sp_dg = new GridHelper(this.grdChiTiet, this.grdViewChiTiet, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_sp_dg.Add(xColumn);


                xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuocKH"]));
                xColumn.Width = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_sp_dg.Add(xColumn);


                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xacnhan_hanghuhong_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_sp_dg.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_sanpham_dichvu_DonViTinh"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_sp_dg.Add(xColumn);

                xColumn = new Column("SOLUONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_vattu_dichvu_SoLuong"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_sp_dg.Add(xColumn);

                xColumn = new Column("GIANHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaNhap"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_sp_dg.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThanhTien"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_sp_dg.Add(xColumn);

                xColumn = new Column("TIEU_HAO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblHaoHut"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_sp_dg.Add(xColumn);


                this.iGridHelper_ctiet_sp_dg.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// Initialize_Grid_ctiet_NVL_SP
        /// </summary>
        private void Initialize_Grid_ctiet_NVL_SP()
        {
            try
            {
                this.iGridHelper_ctiet_nvl_sp = new GridHelper(this.grdChiTiet1, this.grdViewChiTiet1, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_nvl_sp.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieuxuat_tuPYC_MaNL"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_nvl_sp.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieuxuat_tuPYC_TenNL"]));
                xColumn.Width = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_nvl_sp.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_sanpham_dichvu_DonViTinh"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_nvl_sp.Add(xColumn);

                xColumn = new Column("SOLUONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_vattu_dichvu_SoLuong"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_ctiet_nvl_sp.Add(xColumn);

                xColumn = new Column("GIANHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaNhap"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_ctiet_nvl_sp.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThanhTien"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet_nvl_sp.Add(xColumn);

                xColumn = new Column("TIEU_HAO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblHaoHut"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_ctiet_nvl_sp.Add(xColumn);


                this.iGridHelper_ctiet_nvl_sp.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void grdView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            status = true;
            try
            {
                if (this.grd.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)grd.GetFocusedRow()).Row;
                DispalayRequest();
                DataRow[] iRowSelGb_ctiet_SP_DG = iGridDataSource_dataset.Tables[1].Select("DINHMUC_TEMTOA_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["DINHMUC_TEMTOA_ID"]));
                DataRow[] iRowSelGb_ctiet_NVL_SP = iGridDataSource_dataset.Tables[2].Select("DINHMUC_TEMTOA_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["DINHMUC_TEMTOA_ID"]));
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_ChoSP_KhuyenMai_Popup", iRowSelGb, iRowSelGb_ctiet_SP_DG, iRowSelGb_ctiet_NVL_SP);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_MouseDoubleClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            status = false;
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_ChoSP_KhuyenMai_Popup", null);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(checkSanPham() == false)
                {
                    return;
                }

                iRowSelGb = null;
                if (!base.ShowMessage(MessageType.OkCancel, Convert.ToString(UtilLanguage.ApplyLanguage()["lblcomfrimDelete"])))
                    return;
                if (this.grd.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)grd.GetFocusedRow()).Row;

                bool result = XD_DM_SPKMAIBussiness.HT_DINHMUC_TEMTOA_DELETE_BYID(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["DINHMUC_TEMTOA_ID"].ToString()), CommonDataHelper.UserName);
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

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void grdView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (this.grd.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)grd.GetFocusedRow()).Row;
                DispalayRequest();
                DataRow[] iRowSelGb_ctiet_SP_DG = iGridDataSource_dataset.Tables[1].Select("DINHMUC_TEMTOA_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["DINHMUC_TEMTOA_ID"]));
                DataRow[] iRowSelGb_ctiet_NVL_SP = iGridDataSource_dataset.Tables[2].Select("DINHMUC_TEMTOA_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["DINHMUC_TEMTOA_ID"]));
                if (iRowSelGb_ctiet_SP_DG.Length > 0 && iRowSelGb_ctiet_NVL_SP.Length > 0)
                {
                    this.iGridHelper_ctiet_sp_dg.BindItemSource(iRowSelGb_ctiet_SP_DG.CopyToDataTable());
                    this.iGridHelper_ctiet_nvl_sp.BindItemSource(iRowSelGb_ctiet_NVL_SP.CopyToDataTable());
                }
                else
                {
                    grdChiTiet.ItemsSource = null;
                    grdChiTiet1.ItemsSource = null;
                }


            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_FocusedRowChanged()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private bool checkSanPham()
        {
            if (_XD_DM_TTTVIETBussiness.HT_DINHMUC_TEMTOA_CHECKEXISTS_YCDV(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(grd.GetFocusedRowCellValue("SANPHAM_ID"))) == true)
            {
                base.ShowMessage(MessageType.Information, "THẤT BẠI. SẢN PHẨM ĐÃ ĐƯỢC SỬ DỤNG TRONG PHIẾU YÊU CẦU");
                return false;
            }
            return true;
        }
    }
}
