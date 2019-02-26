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
using DevExpress.Xpf.Editors;
using System.Globalization;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Data;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_PhieuXuat_DieuChinh_chon_phieu_xuat_popup.xaml
    /// </summary>
    public partial class frm_PhieuXuat_DieuChinh_chon_phieu_xuat_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataTable iGridDataSource = null;
        DataSet iGridDataSource_dataset = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper_ctiet = null;
        IPHIEU_NHAPXUATKHOBussiness _IPHIEU_NHAP_XUAT_KHO;
        bool isFirstLoad = false;
        string mbChon = "N";
        DataTable dt_CTPHIEUXUAT_Return = null;
        DataTable dt_CTPHIEUXUAT = new DataTable();
        public frm_PhieuXuat_DieuChinh_chon_phieu_xuat_popup()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            _IPHIEU_NHAP_XUAT_KHO = new PHIEU_NHAPXUATKHOBussiness(string.Empty);
            Initialize_Grid_PHIEU_XUAT();
            Initialize_Grid_CT_PHIEUXUAT();
            this.iDataSource.Rows[0]["TEN_KHO"] = ConstCommon.pTEN_KHO;
            this.iDataSource.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
            this.DataContext = this.iDataSource;
            LoadData();
        }
        #region Method
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
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEU_NHAPXUATKHO_ID", typeof(decimal));
                xDicUser.Add("KHACHHANG_NCC_ID", typeof(decimal));
                xDicUser.Add("KHO_ID", typeof(decimal));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("TEN_KH", typeof(string));
                xDicUser.Add("SO_HOPDONG", typeof(string));
                xDicUser.Add("TEN_DONVI_VANCHUYEN", typeof(string));
                xDicUser.Add("DIADIEM_GIAO", typeof(string));
                xDicUser.Add("TEN_HINHTHU_NHAPXUAT", typeof(string));
                xDicUser.Add("HOPDONG_ID", typeof(decimal));
                xDicUser.Add("KHACHHANG_NCC_DONVI_VANCHUYEN_ID", typeof(decimal));
                xDicUser.Add("KHACHHANG_NCC_NOI_XUATHANG_ID", typeof(decimal));
                xDicUser.Add("NGUOILIENHE_A", typeof(string));
                xDicUser.Add("TEN_KHO", typeof(string));
                xDicUser.Add("NGUOILIENHE_B", typeof(string));
                xDicUser.Add("SO_CHUNGTU", typeof(string));
                xDicUser.Add("NGAY_CHUNGTU", typeof(string));
                xDicUser.Add("MA_HINHTHU_NHAPXUAT", typeof(string));
                xDicUser.Add("SOPHIEU", typeof(string));
                xDicUser.Add("NGAYLAP", typeof(string));
                xDicUser.Add("TEN_TAIXE", typeof(string));
                xDicUser.Add("SO_DIENTHOAI", typeof(string));
                xDicUser.Add("SO_XE", typeof(string));
                xDicUser.Add("SO_CONT", typeof(string));
                xDicUser.Add("IS_NHAPNHIEULAN", typeof(bool));
                xDicUser.Add("GHI_CHU_RETURN", typeof(string));
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
                        return _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_GET_BY_DC(ConstCommon.DonViQuanLy, "X");
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
        private void Initialize_Grid_PHIEU_XUAT()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.gridControl, this.grdViewLapPhieuYeuCauNhapKho, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["frmMaterial_SupplierName"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_HOPDONG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblHopDong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_DONVI_VANCHUYEN", Convert.ToString(UtilLanguage.ApplyLanguage()["btnDonViVanChuyen"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("DIADIEM_GIAO", Convert.ToString(UtilLanguage.ApplyLanguage()["btnDiaDiemXuatHang"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGUOILIENHE_A", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNguoiLHA"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGUOILIENHE_B", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNguoiLHB"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_CHUNGTU", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xacnhan_hanghuhong_popup_SoChungTu"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAY_CHUNGTU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNCT"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_HINHTHU_NHAPXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lbHTNX"]));
                xColumn.Width = 180;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SOPHIEU", Convert.ToString(UtilLanguage.ApplyLanguage()["frmFoodMenu_FoodMenuCode"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAYLAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgayPhieu"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_TAIXE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaiXe"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_DIENTHOAI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoDT"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_XE", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_importexcel_SoXe"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("IS_NHAPNHIEULAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblXuatNhieuLan"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("IS_NHAPNHIEULAN") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                this.iGridHelper.Add(xColumn);//CHECKBOX

                this.iGridHelper.Initialize();
                ConstCommon.setAutoFilterConditionGrid(this.gridControl);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid_PHIEU_XUAT()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void Initialize_Grid_CT_PHIEUXUAT()
        {
            try
            {
                this.iGridHelper_ctiet = new GridHelper(this.grdChiTiet, this.grdViewChiTiet, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("TEN_KHO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKho"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_MaThuoc"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuoc1"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);


                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("TEN_TINHTRANG_HANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChangeStatus"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_KHO_KHUVUC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);//combobox

                xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("GIABAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaBan"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("TEN_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);

                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);//COMBOBOX

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);//BUTTON

                xColumn = new Column("NGAY_SANXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);


                xColumn = new Column("TEN_TINHTRANG_CV", Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_ctiet.Add(xColumn);//COMBOBOX

                xColumn = new Column("IS_NHAPNHIEULAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblXuatNhieuLan"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("IS_NHAPNHIEULAN") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                this.iGridHelper_ctiet.Add(xColumn);//CHECKBOX

                this.iGridHelper_ctiet.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        public override object UIReturn(string source)
        {
            try
            {
                DataSet ds_Return = new DataSet();
                if (iDataSource.Rows.Count == 1 && dt_CTPHIEUXUAT_Return == null)
                    ds_Return = null;
                else
                {
                    if ((dt_CTPHIEUXUAT_Return != null) && (dt_CTPHIEUXUAT_Return.Rows.Count > 0))
                    {
                        iDataSource.Rows[0]["GHI_CHU_RETURN"] = mbChon;
                    }
                    ds_Return.Tables.Add(iDataSource);
                    ds_Return.Tables.Add(dt_CTPHIEUXUAT_Return);
                }
                return ds_Return;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid_CT_PHIEUXUAT()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
                return null;
            }
        }
        #endregion
        #region UI   
        private void grdViewLapPhieuYeuCauNhapKho_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (this.gridControl.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)gridControl.GetFocusedRow()).Row;
                DispalayRequest();
                DataRow[] iRowSelGb_CTPHIEUXUAT = iGridDataSource_dataset.Tables[1].Select("PHIEU_NHAPXUATKHO_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_ID"]));
                if (iRowSelGb_CTPHIEUXUAT.Length > 0)
                {
                    dt_CTPHIEUXUAT = iRowSelGb_CTPHIEUXUAT.CopyToDataTable();
                    dt_CTPHIEUXUAT_Return = dt_CTPHIEUXUAT.Copy();
                    this.iGridHelper_ctiet.BindItemSource(iRowSelGb_CTPHIEUXUAT.CopyToDataTable());
                }
                else
                {
                    grdChiTiet.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, btnChon.Tag.ToString());
                return;
            }
        }    

        #endregion

        private void btnChon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (iDataSource.Rows.Count == 0) return;
                if (this.gridControl.GetFocusedRow() == null) return;
                mbChon = "Y";
                this.Close();
            }
            catch (Exception ex)
            {
                mbChon = "N";
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, btnChon.Tag.ToString());
                return;
            }
        }
    }
}
