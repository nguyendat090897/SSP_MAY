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
    /// Interaction logic for frm_PhieuXuat_DieuChinh.xaml
    /// </summary>
    public partial class frm_PhieuXuat_DieuChinh : ControlBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper_PHIEU_DIEU_CHINH = null;
        GridHelper iGridHelper_CTPHAN_THUC_XUAT = null;
        GridHelper iGridHelper_CTPHAN_TRA_LAI = null;
        GridHelper iGridHelper_CTPHAN_TRA_LAI_2 = null;
        DataTable dt_PHIEU_DIEU_CHINH = null;
        DataSet dt_CT_PHIEU_DIEU_CHINH = null;
        DataRow iRowSelGb = null;
        IPHIEU_NHAPXUATKHOBussiness _IPHIEU_NHAP_XUAT_KHO;
        public frm_PhieuXuat_DieuChinh()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            Initialize_Grid();
            Initialize_Grid_CTPHAN_THUC_XUAT();
            Initialize_Grid_CTPHAN_TRA_LAI();
            Initialize_Grid_CTPHAN_TRA_LAI_2();
            _IPHIEU_NHAP_XUAT_KHO = new PHIEU_NHAPXUATKHOBussiness(string.Empty);
            this.DataContext = this.iDataSource;
            LoadData();
        }
        #region Method
        public void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataSet> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_DC_GET_ALL(ConstCommon.DonViQuanLy);
                    });
                    Action<DataSet> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.dt_PHIEU_DIEU_CHINH = dt.Tables[0];
                            this.dt_CT_PHIEU_DIEU_CHINH = dt.Copy();
                            this.iGridHelper_PHIEU_DIEU_CHINH.BindItemSource(this.dt_PHIEU_DIEU_CHINH);
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
                xDicUser.Add("PHIEU_NHAPXUATKHO_ID", typeof(decimal));
                xDicUser.Add("PHIEU_NHAPXUATKHO_DC_ID", typeof(decimal));
                xDicUser.Add("LOAI_DC", typeof(string));
                xDicUser.Add("TEN_LOAI_DC", typeof(string));



                xDicUser.Add("KHACHHANG_NCC_ID", typeof(decimal));
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

        /// <summary>
        /// Initialize_Grid
        /// </summary>
        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper_PHIEU_DIEU_CHINH = new GridHelper(this.grdPHIEU_DIEU_CHINH, this.grdViewPHIEU_DIEU_CHINH, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHIEU_DIEU_CHINH.Add(xColumn);

                xColumn = new Column("SOPHIEU", "Số phiếu xuất");
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHIEU_DIEU_CHINH.Add(xColumn);

                xColumn = new Column("TEN_LOAI_DC", Convert.ToString(UtilLanguage.ApplyLanguage()["btnLOAI_DC"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHIEU_DIEU_CHINH.Add(xColumn);

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_dm_sua_GhiChu"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHIEU_DIEU_CHINH.Add(xColumn);

                this.grdViewPHIEU_DIEU_CHINH.AutoWidth = true;
                this.iGridHelper_PHIEU_DIEU_CHINH.Initialize();
                ConstCommon.setAutoFilterConditionGrid(grdPHIEU_DIEU_CHINH);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void Initialize_Grid_CTPHAN_THUC_XUAT()
        {
            try
            {
                this.iGridHelper_CTPHAN_THUC_XUAT = new GridHelper(this.grdChiTiet_ThucXuat, this.grdViewChiTiet_ThucXuat, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("TEN_KHO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKho"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("TEN_KHO_KHUVUC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);//combobox

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);


                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);


                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.Foreground = Brushes.Red;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("TEN_TINHTRANG_HANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChangeStatus"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("GIABAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaBan"])); 
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("TEN_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);//COMBOBOX

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"])); 
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);//BUTTON

                xColumn = new Column("NGAY_SANXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);


                xColumn = new Column("TEN_TINHTRANG_CV", Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);//COMBOBOX

                xColumn = new Column("IS_NHAPNHIEULAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblXuatNhieuLan"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("IS_NHAPNHIEULAN") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                this.iGridHelper_CTPHAN_THUC_XUAT.Add(xColumn);//CHECKBOX

                this.iGridHelper_CTPHAN_THUC_XUAT.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void Initialize_Grid_CTPHAN_TRA_LAI()
        {
            try
            {
                this.iGridHelper_CTPHAN_TRA_LAI = new GridHelper(this.grdChiTiet_TraLai, this.grdViewChiTiet_TraLai, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("TEN_KHO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKho"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("TEN_KHO_KHUVUC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);//combobox

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);


                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);


                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.Foreground = Brushes.Red;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);


                xColumn = new Column("SO_LUONG_TRA_LAI", "Số lượng tổng trả lại");
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.Foreground = Brushes.Red;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("TEN_TINHTRANG_HANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChangeStatus"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("GIABAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaBan"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("TEN_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);//COMBOBOX

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);//BUTTON

                xColumn = new Column("NGAY_SANXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);


                xColumn = new Column("TEN_TINHTRANG_CV", Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);//COMBOBOX

                xColumn = new Column("IS_NHAPNHIEULAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblXuatNhieuLan"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("IS_NHAPNHIEULAN") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                this.iGridHelper_CTPHAN_TRA_LAI.Add(xColumn);//CHECKBOX

                this.iGridHelper_CTPHAN_TRA_LAI.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void Initialize_Grid_CTPHAN_TRA_LAI_2()
        {
            try
            {
                this.iGridHelper_CTPHAN_TRA_LAI_2 = new GridHelper(this.grdChiTiet_TraLai_2, this.grdViewChiTiet_TraLai_2, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("TEN_KHO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKho"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("TEN_KHO_KHUVUC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);//combobox

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);


                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);


                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("SO_LUONG_TRA_LAI", "Số lượng trả lại");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.Foreground = Brushes.Red;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("TEN_TINHTRANG_HANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChangeStatus"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("TEN_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);

                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);//COMBOBOX

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);//BUTTON

                xColumn = new Column("NGAY_SANXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);


                xColumn = new Column("TEN_TINHTRANG_CV", Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHAN_TRA_LAI_2.Add(xColumn);//COMBOBOX


                this.iGridHelper_CTPHAN_TRA_LAI_2.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        #endregion
        #region UI
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_PhieuXuat_DieuChinh_chon_phieu_xuat_popup", null,"X");
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XuatKho_popup_huy_dieuchinh_phieuxuat", null);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdViewPHIEU_DIEU_CHINH_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (this.grdPHIEU_DIEU_CHINH.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)grdPHIEU_DIEU_CHINH.GetFocusedRow()).Row;
                DispalayRequest();
                if(iRowSelGb["LOAI_DC"].ToString() == "HUY")
                {
                    tabChiTiet_ThucXuat.Visibility = Visibility.Collapsed;
                    tabChiTiet_TraLai_2.Visibility = Visibility.Collapsed;
                    tabChiTiet_TraLai.IsSelected = true;
                    this.grdChiTiet_TraLai.Columns["SO_LUONG_TRA_LAI"].Visible = false;
                    this.grdChiTiet_TraLai.Columns["SO_LUONG_TONG"].Visible = true;
                }
                else
                {
                    tabChiTiet_ThucXuat.Visibility = Visibility.Visible;
                    tabChiTiet_TraLai_2.Visibility = Visibility.Collapsed;
                    this.grdChiTiet_TraLai.Columns["SO_LUONG_TRA_LAI"].Visible = true;
                    this.grdChiTiet_TraLai.Columns["SO_LUONG_TONG"].Visible = false;
                }
                DataRow[] iRowSelGb_CTPHAN_THUC_XUAT = dt_CT_PHIEU_DIEU_CHINH.Tables[1].Select("PHIEU_NHAPXUATKHO_DC_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DC_ID"]));
                DataRow[] iRowSelGb_CTPHAN_TRA_LAI = dt_CT_PHIEU_DIEU_CHINH.Tables[2].Select("PHIEU_NHAPXUATKHO_DC_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DC_ID"]));
                DataRow[] iRowSelGb_CTPHAN_TRA_LAI_2 = dt_CT_PHIEU_DIEU_CHINH.Tables[3].Select("PHIEU_NHAPXUATKHO_DC_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DC_ID"]));
                if (iRowSelGb["LOAI_DC"].ToString() == "DC")
                {
                    if (iRowSelGb_CTPHAN_THUC_XUAT.Length > 0 && iRowSelGb_CTPHAN_TRA_LAI_2.Length > 0)
                    {
                        this.iGridHelper_CTPHAN_THUC_XUAT.BindItemSource(iRowSelGb_CTPHAN_THUC_XUAT.CopyToDataTable());
                        if (iRowSelGb_CTPHAN_TRA_LAI_2.Length > 0)
                        {
                            this.iGridHelper_CTPHAN_TRA_LAI.BindItemSource(iRowSelGb_CTPHAN_TRA_LAI_2.CopyToDataTable());
                        }
                    }
                    else
                    {
                        grdChiTiet_ThucXuat.ItemsSource = null;
                    }
                }
                else
                {
                    if (iRowSelGb_CTPHAN_TRA_LAI.Length > 0)
                    {
                        DataTable dt_temp = null;
                        dt_temp = iRowSelGb_CTPHAN_TRA_LAI.CopyToDataTable();
                        for (int i = 0; i < dt_temp.Rows.Count; i++)
                        {
                            dt_temp.Rows[i]["SO_LUONG_TONG"] = ConstCommon.NVL_NUM_NT_NEW(dt_temp.Rows[i]["SO_LUONG_TONG"]) * -1;
                            dt_temp.Rows[i]["SO_LUONG_THUNG"] = ConstCommon.NVL_NUM_NT_NEW(dt_temp.Rows[i]["SO_LUONG_THUNG"]) * -1;
                            dt_temp.Rows[i]["SO_LUONG_LE"] = ConstCommon.NVL_NUM_NT_NEW(dt_temp.Rows[i]["SO_LUONG_LE"]) * -1;
                            dt_temp.Rows[i]["DONGIA"] = ConstCommon.NVL_NUM_NT_NEW(dt_temp.Rows[i]["DONGIA"]) * -1;
                            dt_temp.Rows[i]["THANHTIEN"] = ConstCommon.NVL_NUM_NT_NEW(dt_temp.Rows[i]["THANHTIEN"]) * -1;
                        }
                        this.iGridHelper_CTPHAN_TRA_LAI.BindItemSource(dt_temp);
                    }
                    else
                    {
                        grdChiTiet_TraLai.ItemsSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnDelete.Tag.ToString());
                // base.ShowMessage(MessageType.Error, "Vui lòng chọn dữ liệu cần xóa", ex);
                return;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            tabThongTinChung.IsSelected = true;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                iRowSelGb = null;
                if (!base.ShowMessage(MessageType.OkCancel, Convert.ToString(UtilLanguage.ApplyLanguage()["lblcomfrimDelete"])))
                    return;
                if (this.grdPHIEU_DIEU_CHINH.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)grdPHIEU_DIEU_CHINH.GetFocusedRow()).Row;

                bool result = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_DC_DELETE(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DC_ID"].ToString()), CommonDataHelper.UserName);
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

        private void grdViewPHIEU_DIEU_CHINH_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                object xReturn = null;
                DataRow[] iRowSelGb_CTPHANTHUCXUAT = null;
                DataRow[] iRowSelGb_CTPHANTRALAI = null;
                DataRow[] iRowSelGb_CTPHANTRALAI_2 = null;
                if (iDataSource.Rows.Count == 0) return;
                if (this.grdPHIEU_DIEU_CHINH.GetFocusedRow() == null) return;
                DataRow iRowSelGb = ((DataRowView)this.grdPHIEU_DIEU_CHINH.GetFocusedRow()).Row;
                if (iRowSelGb["LOAI_DC"].ToString() == "DC")
                {
                    iRowSelGb_CTPHANTHUCXUAT = dt_CT_PHIEU_DIEU_CHINH.Tables[1].Select("PHIEU_NHAPXUATKHO_DC_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DC_ID"]));
                    iRowSelGb_CTPHANTRALAI = dt_CT_PHIEU_DIEU_CHINH.Tables[2].Select("PHIEU_NHAPXUATKHO_DC_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DC_ID"]));
                    iRowSelGb_CTPHANTRALAI_2 = dt_CT_PHIEU_DIEU_CHINH.Tables[3].Select("PHIEU_NHAPXUATKHO_DC_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DC_ID"]));
                }
                else
                {
                    iRowSelGb_CTPHANTRALAI = dt_CT_PHIEU_DIEU_CHINH.Tables[2].Select("PHIEU_NHAPXUATKHO_DC_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_DC_ID"]));
                }
                xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XuatKho_popup_huy_dieuchinh_phieuxuat", iRowSelGb, iRowSelGb_CTPHANTHUCXUAT, iRowSelGb_CTPHANTRALAI, iRowSelGb_CTPHANTRALAI_2);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_MouseDoubleClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        #endregion


    }
}
