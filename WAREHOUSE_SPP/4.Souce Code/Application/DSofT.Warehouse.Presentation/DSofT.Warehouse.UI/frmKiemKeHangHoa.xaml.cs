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
    ///  Create date: 24/01/2018
    ///  Editor:      
    ///  Modify date: 03/05/2018
    ///  Description: kiem ke hang hoa
    /// </summary>
    public partial class frmKiemKeHangHoa : ControlBase
    {
        DataTable iDataSource = null;
        DataTable iGridDataSource = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper_CT = null;
        DataSet iGridDataSource_CT = null;
        DataTable dt_PHIEUNXKHO = null;
        IPHIEU_NHAPXUATKHOBussiness _IPHIEU_NHAP_XUAT_KHO;
        DataRow iRowSelGb = null;
        public frmKiemKeHangHoa()
        {
            InitializeComponent();
            this.iDataSource = TableSchemaDefineBingding();
            Initialize_Grid();
            Initialize_Grid_CT();
            _IPHIEU_NHAP_XUAT_KHO = new PHIEU_NHAPXUATKHOBussiness(string.Empty);
            this.DataContext = this.iDataSource;
            LoadData();
        }
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("SOPHIEU", typeof(string));
                xDicUser.Add("NGAY_KIEMKE", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("PHIEU_KIEMKE_ID", typeof(decimal));
                xDicUser.Add("TEN_KH", typeof(string));
                xDicUser.Add("KHACHHANG_NCC_ID", typeof(decimal));
                xDicUser.Add("NGUOI_NHAP", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
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
                        return _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_KIEMKE_GET_ALL(ConstCommon.DonViQuanLy);
                    });
                    Action<DataSet> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iGridDataSource = dt.Tables[0];
                            this.iGridDataSource_CT = dt.Copy();
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
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frmKiemKeHangHoa_popup", null);
                LoadData();
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
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("SOPHIEU", Convert.ToString(UtilLanguage.ApplyLanguage()["frmFoodMenu_FoodMenuCode"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAY_KIEMKE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgayKK"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblLyDo"]));
                xColumn.Width = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGUOI_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNguoiNhap"]));
                xColumn.Width = 250;
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
        private void Initialize_Grid_CT()
        {
            try
            {
                this.iGridHelper_CT = new GridHelper(this.grdChiTiet, this.grdViewChiTiet, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_KHO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKho"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_KHO_KHUVUC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);//combobox

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_MaThuoc"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("MA_ITEM_TYPE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblItemType"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuoc1"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CT.Add(xColumn);


                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);


                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG_TON", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongTongTon"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG_TON", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongThungTon"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE_TON", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongLeTon"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG_THUCTE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongTongThucTe"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG_THUCTE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongThungThucTe"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE_THUCTE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongLeThucTe"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG_LECH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongTongLech"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG_LECH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongThungLech"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE_LECH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongLeLech"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_TINHTRANG_HANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChangeStatus"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("CHATLUONG_HANGHOA", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChatLHHOA"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("CHATLUONG_BAOBI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChatLBBI"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);//COMBOBOX

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);//BUTTON

                xColumn = new Column("TEN_TINHTRANG_CV", Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);//COMBOBOX

                xColumn = new Column("SOLUONG_QUYDOI", "Qui đổi");
                xColumn.Width = 80;
                xColumn.Visible = false;
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
        private void grdView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (this.grd.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)grd.GetFocusedRow()).Row;
                DispalayRequest();
                DataRow[] iRowSelGb_CT = iGridDataSource_CT.Tables[1].Select("PHIEU_KIEMKE_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_KIEMKE_ID"]));
                DataRow[] iRowSelGb_phieu_nhapxuat_kho = iGridDataSource_CT.Tables[2].Select("PHIEU_KIEMKE_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_KIEMKE_ID"]));
                if(iRowSelGb_phieu_nhapxuat_kho.Length > 0)
                {
                    dt_PHIEUNXKHO = iRowSelGb_phieu_nhapxuat_kho.CopyToDataTable();
                }
                if (iRowSelGb_CT.Length > 0)
                {
                    this.iGridHelper_CT.BindItemSource(iRowSelGb_CT.CopyToDataTable());
                }
                else
                {
                    grdChiTiet.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnDelete.Tag.ToString());
                base.ShowMessage(MessageType.Error, "Vui lòng chọn dữ liệu cần xóa", ex);
                //return;
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
                bool resul = false;
                for (int i = 0; i < dt_PHIEUNXKHO.Rows.Count; i++)
                {
                    long pPHIEU_NHAPXUATKHO_ID = ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEUNXKHO.Rows[i]["PHIEU_NHAPXUATKHO_ID"]);
                    if (pPHIEU_NHAPXUATKHO_ID == 0)
                    {
                        base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["tblKhongThanhCong"]));
                        return;
                    }
                    resul = _IPHIEU_NHAP_XUAT_KHO.DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(ConstCommon.DonViQuanLy, pPHIEU_NHAPXUATKHO_ID, CommonDataHelper.UserName);
                }
                if (resul == false)
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["tblKhongThanhCong"]));
                    return;
                }
                bool result = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_KIEMKE_DELETE(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_KIEMKE_ID"]), CommonDataHelper.UserName);
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
        private void grdView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                object xReturn = null;
                if (iDataSource.Rows.Count == 0) return;
                if (this.grd.GetFocusedRow() == null) return;
                DataRow iRowSelGb = ((DataRowView)this.grd.GetFocusedRow()).Row;

                DataRow[] iRowSelGb_ctiet = iGridDataSource_CT.Tables[1].Select("PHIEU_KIEMKE_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_KIEMKE_ID"]));
                xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frmKiemKeHangHoa_popup", iRowSelGb, iRowSelGb_ctiet);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_MouseDoubleClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }
}
