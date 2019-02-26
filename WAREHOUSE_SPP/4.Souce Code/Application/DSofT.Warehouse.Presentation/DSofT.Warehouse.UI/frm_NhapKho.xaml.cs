﻿using DSofT.Warehouse.Business;
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
using DevExpress.Xpf.Editors.Settings;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_NhapKho.xaml
    /// </summary>
    public partial class frm_NhapKho : ControlBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper_CTPHIEUNHAP = null;
        DataTable iGridDataSource = null;
        DataSet iGridDataSource_CTPHIEUNHAP = null;
        DataRow iRowSelGb = null;
        IPHIEU_NHAPXUATKHOBussiness _IPHIEU_NHAP_XUAT_KHO;
        public frm_NhapKho()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            Initialize_Grid();
            Initialize_Grid_CTPHIEUNHAP();
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
                        return _IPHIEU_NHAP_XUAT_KHO.getKO_PHIEU_NHAPXUATKHO_ALL(ConstCommon.DonViQuanLy, "N");
                    });
                    Action<DataSet> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iGridDataSource = dt.Tables[0];
                            this.iGridDataSource_CTPHIEUNHAP = dt.Copy();
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
                xDicUser.Add("PHIEU_NHAPXUATKHO_ID", typeof(decimal));
                xDicUser.Add("KHACHHANG_NCC_ID", typeof(decimal));
                xDicUser.Add("TEN_KH", typeof(string));
                xDicUser.Add("SO_HOPDONG", typeof(string));
                xDicUser.Add("TEN_DONVI_VANCHUYEN", typeof(string));
                xDicUser.Add("DIADIEM_GIAO", typeof(string));
                xDicUser.Add("TEN_HINHTHU_NHAPXUAT", typeof(string));
                xDicUser.Add("HOPDONG_ID", typeof(string));
                xDicUser.Add("KHACHHANG_NCC_DONVI_VANCHUYEN_ID", typeof(decimal));
                xDicUser.Add("KHACHHANG_NCC_NOI_XUATHANG_ID", typeof(decimal));
                xDicUser.Add("NGUOILIENHE_A", typeof(string));
                xDicUser.Add("TEN_KHO", typeof(string));
                xDicUser.Add("NGUOILIENHE_B", typeof(string));
                xDicUser.Add("SO_CHUNGTU", typeof(string));
                xDicUser.Add("NGAY_CHUNGTU", typeof(string));
                xDicUser.Add("MA_HINHTHU_NHAPXUAT", typeof(string));
                xDicUser.Add("SOLO", typeof(string));
                xDicUser.Add("SOPHIEU", typeof(string));
                xDicUser.Add("NGAYLAP", typeof(string));
                xDicUser.Add("TEN_TAIXE", typeof(string));
                xDicUser.Add("SO_DIENTHOAI", typeof(string));
                xDicUser.Add("SO_XE", typeof(string));
                xDicUser.Add("SO_CONT", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("PHIEU_KIEMKE_ID", typeof(decimal));
                xDicUser.Add("SO_PHIEU_KIEM_KE", typeof(string));
                xDicUser.Add("SOPHIEU_YEUCAU", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
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
                this.iGridHelper = new GridHelper(this.gridControl, this.grdViewLapPhieuNhapKho, false);
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

                //xColumn = new Column("SO_HOPDONG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblHopDong"]));
                //xColumn.Width = 100;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("TEN_DONVI_VANCHUYEN", Convert.ToString(UtilLanguage.ApplyLanguage()["btnDonViVanChuyen"]));
                //xColumn.Width = 150;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);
                //xColumn = new Column("KHACHHANG_NCC_DONVI_VANCHUYEN_ID", "Visible");
                //xColumn.Width = 150;
                //xColumn.Visible = false;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("IS_NHAPNHIEULAN", "Visable");
                //xColumn.Width = 150;
                //xColumn.Visible = false;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("DIADIEM_GIAO", Convert.ToString(UtilLanguage.ApplyLanguage()["btnDiaDiemXuatHang"]));
                //xColumn.Width = 150;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);
                //xColumn = new Column("KHACHHANG_NCC_NOI_XUATHANG_ID", "visible");
                //xColumn.Width = 150;
                //xColumn.Visible = false;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("NGUOILIENHE_A", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNguoiLHA"]));
                //xColumn.Width = 100;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("NGUOILIENHE_B", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNguoiLHB"]));
                //xColumn.Width = 100;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("SO_CHUNGTU", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xacnhan_hanghuhong_popup_SoChungTu"]));
                //xColumn.Width = 100;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("NGAY_CHUNGTU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNCT"]));
                //xColumn.Width = 100;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

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

                //xColumn = new Column("TEN_TAIXE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaiXe"]));
                //xColumn.Width = 100;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("SO_DIENTHOAI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoDT"]));
                //xColumn.Width = 100;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("SO_XE", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_importexcel_SoXe"]));
                //xColumn.Width = 100;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xacnhan_hanghuhong_GhiChu"]));
                //xColumn.Width = 150;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("SO_PHIEU_KIEM_KE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoPhieuKiemKe"])); 
                //xColumn.Width = 100;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //----------------------- create button
                //----------------------- create button
                ButtonEditSettings btnXepHangVaoKho = new ButtonEditSettings();
                btnXepHangVaoKho.AllowDefaultButton = false;
                ButtonInfo btnXepHang = new ButtonInfo();
                btnXepHangVaoKho.Buttons.Add(btnXepHang);
                btnXepHang.Click += btnXepHangVaoKho_Click;
                btnXepHang.Content = "Xếp hàng";

                xColumn = new Column("XEP_HANG", "Xếp hàng vào kho");
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.CustomEditor = btnXepHangVaoKho;
                xColumn.ColumnType = ColumnControl.Custom;
                this.iGridHelper.Add(xColumn);

             

                this.iGridHelper.Initialize();
                ConstCommon.setAutoFilterConditionGrid(gridControl);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void Initialize_Grid_CTPHIEUNHAP()
        {
            try
            {
                this.iGridHelper_CTPHIEUNHAP = new GridHelper(this.grdChiTiet, this.grdViewChiTiet, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("TEN_KHO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKho"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                //xColumn = new Column("TEN_KHO_KHUVUC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_MaThuoc"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                //xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuoc1"]));
                //xColumn.Width = 150;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);


                //xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                //xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                //xColumn.Width = 150;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);


                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                //xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.MaskType = MaskType.Numeric;
                //xColumn.EditMask = "n0";
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                //xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.MaskType = MaskType.Numeric;
                //xColumn.EditMask = "n0";
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("TEN_TINHTRANG_HANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChangeStatus"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                //xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                //xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                //xColumn.Width = 120;
                //xColumn.Visible = true;
                //xColumn.MaskType = MaskType.Numeric;
                //xColumn.EditMask = "n0";
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                //xColumn = new Column("GIABAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaBan"])); 
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.MaskType = MaskType.Numeric;
                //xColumn.EditMask = "n0";
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                //xColumn = new Column("TEN_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CTPHIEUNHAP.Add(xColumn);//COMBOBOX

                //xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"])); 
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);//BUTTON

                //xColumn = new Column("NGAY_SANXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"]));
                //xColumn.Width = 100;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);


                //xColumn = new Column("TEN_TINHTRANG_CV", Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]));
                //xColumn.Width = 150;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);//COMBOBOX

                //xColumn = new Column("IS_NHAPNHIEULAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNhapNhieuLan"]));
                //xColumn.Width = 100;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //xColumn.ColumnType = ColumnControl.Checkbox;
                //xColumn.Binding = new Binding("IS_NHAPNHIEULAN") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);//CHECKBOX

                //xColumn = new Column("THUC_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThucNhap"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                //xColumn = new Column("SOLUONG_QUYDOI", "Qui đổi");
                //xColumn.Width = 80;
                //xColumn.Visible = false;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper_CTPHIEUNHAP.Add(xColumn);

                this.iGridHelper_CTPHIEUNHAP.Initialize();
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
                frm_NhapKho_popup.status = true;
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_NhapKho_popup"/*, null,"N"*/);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdViewLapPhieuNhapKho_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (this.gridControl.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)gridControl.GetFocusedRow()).Row;
                DispalayRequest();
                //if(iRowSelGb["IS_NHAPNHIEULAN"].ToString() == "True")
                //{
                //    tblIS_NHAP_NHIEU_LAN.Visibility = Visibility.Visible;
                //    txtIS_NHAP_NHIEU_LAN.Visibility = Visibility.Visible;
                //    txtIS_NHAP_NHIEU_LAN.Text = "Nhập nhiều lần";
                //}
                //else
                //{
                //    tblIS_NHAP_NHIEU_LAN.Visibility = Visibility.Hidden;
                //    txtIS_NHAP_NHIEU_LAN.Visibility = Visibility.Hidden;
                //}
                DataRow[] iRowSelGb_CTPHIEUNHAP = iGridDataSource_CTPHIEUNHAP.Tables[1].Select("PHIEU_NHAPXUATKHO_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_ID"]));
                if (iRowSelGb_CTPHIEUNHAP.Length > 0)
                {
                    this.iGridHelper_CTPHIEUNHAP.BindItemSource(iRowSelGb_CTPHIEUNHAP.CopyToDataTable());
                }
                else
                {
                    grdChiTiet.ItemsSource = null;
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
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                iRowSelGb = null;
                if (!base.ShowMessage(MessageType.OkCancel, Convert.ToString(UtilLanguage.ApplyLanguage()["lblcomfrimDelete"])))
                    return;
                if (this.gridControl.GetFocusedRow() == null) return;
                iRowSelGb = ((DataRowView)gridControl.GetFocusedRow()).Row;

                bool result = _IPHIEU_NHAP_XUAT_KHO.DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_ID"].ToString()), CommonDataHelper.UserName);
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

        private void grdViewLapPhieuNhapKho_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                frm_NhapKho_popup.status = true;
                frm_NhapKho_popup.pN_OR_N = "N";
                object xReturn = null;
                if (iDataSource.Rows.Count == 0) return;
                if (this.gridControl.GetFocusedRow() == null) return;
                DataRow iRowSelGb = ((DataRowView)this.gridControl.GetFocusedRow()).Row;
                string pLA_KIEM_KIEM = "KKK";
                DataRow[] iRowSelGb_ctiet = iGridDataSource_CTPHIEUNHAP.Tables[1].Select("PHIEU_NHAPXUATKHO_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_ID"]));
                xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_NhapKho_popup", iRowSelGb, iRowSelGb_ctiet/*, "N",pLA_KIEM_KIEM*/);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_MouseDoubleClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnXepHangVaoKho_Click(object sender, RoutedEventArgs e)
        {
            try
            {        
                object xReturn = null;
                if (iDataSource.Rows.Count == 0) return;
                if (this.gridControl.GetFocusedRow() == null) return;
                DataRow iRowSelGb = ((DataRowView)this.gridControl.GetFocusedRow()).Row;
                DataRow[] iRowSelGb_ctiet = iGridDataSource_CTPHIEUNHAP.Tables[1].Select("PHIEU_NHAPXUATKHO_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_ID"]));
                xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_NhapKho_popup_xep_hang_vao_kho", iRowSelGb, iRowSelGb_ctiet, "N");
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        #endregion
    }
}