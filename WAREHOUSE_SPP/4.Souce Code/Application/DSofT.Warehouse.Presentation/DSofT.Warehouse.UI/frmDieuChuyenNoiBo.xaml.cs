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
using System.Windows.Media;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 31/01/2018
    ///  Editor:     HUYNH 
    ///  Modify date: 24/04/2018
    ///  DEV: Huynh
    ///  Description: Quan ly xuat nhap ko - Dieu chuyen noi bo
    /// </summary>
    public partial class frmDieuChuyenNoiBo : ControlBase
    {
        DataTable iDataSource = null;
        DataTable iGridDataSource = null;
        DataTable dt_CTNXKHO = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper_CT = null;
        DataRow iRowSelGb = null;
        DataSet iGridDataSource_PHANLOAI = null;
        IPHIEU_NHAPXUATKHOBussiness _IPHIEU_NHAP_XUAT_KHO;
        string pLOAI_DC, pTEN_LOAIDC = String.Empty;
        bool isFirstLoad = true;
        int k = 0;
        public frmDieuChuyenNoiBo()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            Initialize_Grid();
            Initialize_Grid_CT();
            _IPHIEU_NHAP_XUAT_KHO = new PHIEU_NHAPXUATKHOBussiness(string.Empty);
            this.DataContext = this.iDataSource;
           LoadData();
            LoadComboBox();
        }
        /// <summary>
        /// LoadData
        /// </summary>
        private void LoadComboBox()
        {
            DataTable dtLOAI_CHUYEN = new DataTable();
            dtLOAI_CHUYEN.Columns.Add("IDLOAI_DC", typeof(string));
            dtLOAI_CHUYEN.Columns.Add("TENLOAI_DC", typeof(string));
            ComboBoxUtil.SetComboBoxEdit(this.cboLoaiChuyen, "TENLOAI_DC", "IDLOAI_DC", dtLOAI_CHUYEN, SelectionTypeEnum.Native);
            ComboBoxUtil.InsertItem(this.cboLoaiChuyen, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
            ComboBoxUtil.InsertItem(this.cboLoaiChuyen, "Chuyển cùng địa chỉ", "TRONGDONVI", 1);
            ComboBoxUtil.InsertItem(this.cboLoaiChuyen, "Chuyển khác địa chỉ(chuyển kho khác địa chỉ)", "KHACDONVI", 2);
            this.cboLoaiChuyen.SelectedIndex = 0;
            //pLOAI_DC = "TRONGDONVI";
            //pTEN_LOAIDC = "Chuyển cùng địa chỉ";
        }
        public void LoadData()
        {
            try
            {
                isFirstLoad = true;
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataSet> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_DCNB_GETALL(ConstCommon.DonViQuanLy);
                    });
                    Action<DataSet> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iGridDataSource = dt.Tables[0];
                            this.iGridDataSource_PHANLOAI = dt.Copy();
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
        /// <summary>
        /// Binding data
        /// </summary>
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEU_NHAPXUATKHO_DCNB_ID", typeof(decimal));
                xDicUser.Add("LOAI_DC", typeof(string));
                xDicUser.Add("TENLOAI_DC", typeof(string));
                xDicUser.Add("SOPHIEU_DC", typeof(string));
                xDicUser.Add("NGAY_DC", typeof(string));
                xDicUser.Add("NGUOI_DC", typeof(string));
                xDicUser.Add("SOPHIEU_NHAN", typeof(string));
                xDicUser.Add("NGUOI_NHAN", typeof(string));
                xDicUser.Add("NGAY_NHAN", typeof(string));
                xDicUser.Add("MA_DVIQLY_CHUYEN", typeof(string));
                xDicUser.Add("MA_DVIQLY_NHAN", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboLoaiChuyen.SelectedIndex == 0)
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng chọn loại điều chuyển nội bộ.");
                    return;
                }
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frmDieuChuyenNoiBo_popup", pLOAI_DC,pTEN_LOAIDC);
                LoadData();
                cboLoaiChuyen.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

        }
        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grdDieuChuyenNoiBo, this.grdViewDieuChuyenNoiBo, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TENLOAI_DC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblLoaiC"]));
                xColumn.Width = 180;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SOPHIEU_DC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSPhieuChuyen"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAY_DC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgayDC"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGUOI_DC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNguoiDC"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("SOPHIEU_NHAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSPhieuNhan"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGUOI_NHAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNguoiNhan"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAY_NHAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgayNhan"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("MA_DVIQLY_CHUYEN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDVQLchuyen"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_DVIQLY_NHAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDVQLnhan"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuRemark"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                this.iGridHelper.Initialize();
                //  this.grdViewDieuChuyenNoiBo.AutoWidth = true;
                ConstCommon.setAutoFilterConditionGrid(grdDieuChuyenNoiBo);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void Initialize_Grid_CT()
        {
            try
            {
                this.iGridHelper_CT = new GridHelper(this.grdChiTiet_DieuChuyen, this.grdViewChiTiet_DieuChuyen, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["frmMaterial_SupplierName"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_HOPDONG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblHopDong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_DONVI_VANCHUYEN", Convert.ToString(UtilLanguage.ApplyLanguage()["btnDonViVanChuyen"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("DIADIEM_GIAO", Convert.ToString(UtilLanguage.ApplyLanguage()["btnDiaDiemXuatHang"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;

                xColumn = new Column("NGUOILIENHE_A", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNguoiLHA"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("NGUOILIENHE_B", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNguoiLHB"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_CHUNGTU", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xacnhan_hanghuhong_popup_SoChungTu"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("NGAY_CHUNGTU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNCT"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_X_OR_N", "Dạng phiếu");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_HINHTHU_NHAPXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lbHTNX"]));
                xColumn.Width = 180;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SOPHIEU", Convert.ToString(UtilLanguage.ApplyLanguage()["frmFoodMenu_FoodMenuCode"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("NGAYLAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgayPhieu"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_TAIXE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaiXe"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_DIENTHOAI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoDT"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_XE", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_importexcel_SoXe"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xacnhan_hanghuhong_GhiChu"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_PHIEU_KIEM_KE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoPhieuKiemKe"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                this.iGridHelper_CT.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void cboLoaiChuyen_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
           
        }
        private void grdViewDieuChuyenNoiBo_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (this.grdDieuChuyenNoiBo.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)grdDieuChuyenNoiBo.GetFocusedRow()).Row;
                DispalayRequest();
                DataRow[] iRowSelGb_CT = iGridDataSource_PHANLOAI.Tables[3].Select("PHIEU_NHAPXUATKHO_DCNB_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DCNB_ID"]));
                if (iRowSelGb_CT.Length > 0)
                {
                    dt_CTNXKHO = iRowSelGb_CT.CopyToDataTable();
                    this.iGridHelper_CT.BindItemSource(dt_CTNXKHO);
                }
                else
                {
                    grdChiTiet_DieuChuyen.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnDelete.Tag.ToString());
                return;
            }
        }

        private void cboLoaiChuyen_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cboLoaiChuyen.SelectedIndex == 0)
            {
                if (isFirstLoad == true)
                {
                    LoadData();
                    isFirstLoad = false;
                   // k = 0;
                }
                else
                {
                    this.iGridHelper.BindItemSource(iGridDataSource_PHANLOAI.Tables[0]);
                    btnDSPhieuChuyen.Visibility = Visibility.Collapsed;
                }
            }
            if (cboLoaiChuyen.SelectedIndex == 1)
            {
                this.iGridHelper.BindItemSource(iGridDataSource_PHANLOAI.Tables[1]);
                pLOAI_DC = "TRONGDONVI";
                pTEN_LOAIDC = "Chuyển cùng địa chỉ";
                btnDSPhieuChuyen.Visibility = Visibility.Collapsed;
                k = 1;
            }
            if (cboLoaiChuyen.SelectedIndex == 2)
            {
                this.iGridHelper.BindItemSource(iGridDataSource_PHANLOAI.Tables[2]);
                pLOAI_DC = "KHACDONVI";
                pTEN_LOAIDC = "Chuyển khác địa chỉ(chuyển kho khác địa chỉ)";
                btnDSPhieuChuyen.Visibility = Visibility.Visible;
                k = 2;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            cboLoaiChuyen.SelectedIndex = 0;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                iRowSelGb = null;
                if (!base.ShowMessage(MessageType.OkCancel, Convert.ToString(UtilLanguage.ApplyLanguage()["lblcomfrimDelete"])))
                    return;
                if (this.grdDieuChuyenNoiBo.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)grdDieuChuyenNoiBo.GetFocusedRow()).Row;
                bool resul = false;
                for(int i = 0;i<dt_CTNXKHO.Rows.Count;i++)
                {
                    long pPHIEU_NHAPXUATKHO_ID = ConstCommon.NVL_NUM_LONG_NEW(dt_CTNXKHO.Rows[i]["PHIEU_NHAPXUATKHO_ID"]);
                    if (pPHIEU_NHAPXUATKHO_ID == 0)
                    {
                        base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["tblKhongThanhCong"]));
                        return;
                    }
                    resul = _IPHIEU_NHAP_XUAT_KHO.DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(ConstCommon.DonViQuanLy, pPHIEU_NHAPXUATKHO_ID, CommonDataHelper.UserName);
                }
                long pID_PHIEU_DCNB = ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DCNB_ID"]);
                bool result = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_DCNB_DELETE(ConstCommon.DonViQuanLy,pID_PHIEU_DCNB, CommonDataHelper.UserName);
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
                cboLoaiChuyen.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnDelete.Tag.ToString());
                base.ShowMessage(MessageType.Error, "Vui lòng chọn dữ liệu cần xóa", ex);
            }
        }

        private void btnDSPhieuChuyen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frmDieuChuyenNoiBo_ds_phieu_chuyen_cho_nhan_poup", null);
                LoadData();
                cboLoaiChuyen.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnDSPhieuChuyen.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdViewDieuChuyenNoiBo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string pDA_CHAP_NHAN = String.Empty;
                if(cboLoaiChuyen.SelectedIndex == 0)
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng chọn loại điều chuyển nội bộ.");
                    return;
                }
                if (iDataSource.Rows.Count == 0) return;
                if (this.grdDieuChuyenNoiBo.GetFocusedRow() == null) return;
                DataRow iRowSelGb = ((DataRowView)this.grdDieuChuyenNoiBo.GetFocusedRow()).Row;
                DataRow[] iRowSelGb_PHIEUXUAT = iGridDataSource_PHANLOAI.Tables[3].Select("PHIEU_NHAPXUATKHO_DCNB_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DCNB_ID"]) + "AND X_OR_N = 'X'" );
                DataRow[] iRowSelGb_PHIEUNHAP = iGridDataSource_PHANLOAI.Tables[3].Select("PHIEU_NHAPXUATKHO_DCNB_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DCNB_ID"]) + "AND X_OR_N = 'N'");
                DataRow[] iRowSelGb_CT_PHIEUXKHO = iGridDataSource_PHANLOAI.Tables[4].Select("PHIEU_NHAPXUATKHO_DCNB_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DCNB_ID"]) + "AND X_OR_N = 'X'");
                DataRow[] iRowSelGb_CT_PHIEUNKHO = iGridDataSource_PHANLOAI.Tables[4].Select("PHIEU_NHAPXUATKHO_DCNB_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DCNB_ID"]) + "AND X_OR_N = 'N'");
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frmDieuChuyenNoiBo_popup", pLOAI_DC, pTEN_LOAIDC, iRowSelGb, iRowSelGb_CT_PHIEUXKHO, iRowSelGb_CT_PHIEUNKHO, iRowSelGb_PHIEUXUAT, iRowSelGb_PHIEUNHAP,pDA_CHAP_NHAN);
                LoadData();
                cboLoaiChuyen.SelectedIndex = 0;               
                
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdViewDieuChuyenNoiBo_MouseDoubleClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }
}
