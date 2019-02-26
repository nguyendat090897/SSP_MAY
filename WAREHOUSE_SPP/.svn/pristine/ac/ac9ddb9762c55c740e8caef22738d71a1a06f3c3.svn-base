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
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors;
using System.Drawing;
using System.Windows.Media;
using DevExpress.Xpf.Editors.Settings;
using System.Globalization;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_NhapLaiVatTuDaXuatRa_popup.xaml
    /// </summary>
    public partial class frm_NhapLaiVatTuDaXuatRa_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper_CT = null;
        DataTable dt_PHIEU_CTIET = null;
        DataTable dt_Tra_NVL_Ton = null;
        DataTable dt_KHO = null;
        DataTable dt_KHO_KHUVUC = null;
        DataTable dt_TRANGTHAI = null;
        DataTable dt_KHUVUC = null;
        DataTable dt_VITRIHANG = null;
        DataTable dt_TINHTRANGCV = null;
        DataTable dt_PALLET = null;
        string pX_OR_N = "N";
        INhap_Lai_Vat_Tu_Dich_VuBussiness _Tra_Lai_VatTu_DV;
        IPHIEU_NHAPXUATKHOBussiness _Nhap_Kho;
        long PHIEU_NHAPXUATKHO_ID = 0;
        int so_tong, so_thung, so_le = 0;
        bool isFirstLoad = false;
        bool isSecondLoad = false;
        bool isEndLoad = false;
        public frm_NhapLaiVatTuDaXuatRa_popup()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            dt_Tra_NVL_Ton = this.TableSchemaDefineBingding_TraNVL_Ton();
            dt_Tra_NVL_Ton.Clear();
            dt_PHIEU_CTIET = this.TableSchemaDefineBingding_Grid();
            dt_PHIEU_CTIET.Clear();
            _Tra_Lai_VatTu_DV = new Nhap_Lai_Vat_Tu_Dich_VuBussiness(string.Empty);
            _Nhap_Kho = new PHIEU_NHAPXUATKHOBussiness(string.Empty);
            this.DataContext = this.iDataSource;
            LoadComboBox();
            Initialize_Grid();
            Initialize_Grid_CTPHIEUNHAP();
            //KiemTraNhapNhieuLan();
            //chkIS_NHAPNHIEULAN.IsEnabled = false;
        }

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                btnChonPhieuYC_DV.IsEnabled = false;
                iRowSelGb = this.Parameter[0] as DataRow;
                PHIEU_NHAPXUATKHO_ID = ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_ID"]);
                DispalayRequest();
                //_Tra_Lai_VatTu_DV = ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"].ToString());
                //isFirstLoad = true;
                if (this.Parameter.Count() > 1)
                {
                    //UpdateNhapNhieuLan();
                    //chkIS_NHAPNHIEULAN.IsEnabled = true;
                    DataRow[] dr_nvl_ton = this.Parameter[1] as DataRow[];
                    //txtkhonhap.Text = dr_chitiet[0]["TEN_KHO"].ToString();
                    dt_Tra_NVL_Ton.Clear();
                    if (dr_nvl_ton.Length > 0)
                    {
                        dt_Tra_NVL_Ton = dr_nvl_ton.CopyToDataTable();
                    }
                    if (dt_Tra_NVL_Ton != null && dt_Tra_NVL_Ton.Rows.Count>0)
                    {
                        this.iGridHelper.BindItemSource(dt_Tra_NVL_Ton);
                        txtPYCDV.Text = dt_Tra_NVL_Ton.Rows[0]["SOPHIEU_DV"].ToString();
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }
                    //-------------------bing grd Chi tiet phieu nhap--------------------

                    DataRow[] dr_chitiet = this.Parameter[2] as DataRow[];
                    //txtkhonhap.Text = dr_chitiet[0]["TEN_KHO"].ToString();
                    
                    dt_PHIEU_CTIET.Clear();
                    if (dr_chitiet.Length > 0)
                    {
                        dt_PHIEU_CTIET = dr_chitiet.CopyToDataTable();
                    }
                    if (dt_PHIEU_CTIET != null && dt_PHIEU_CTIET.Rows.Count>0)
                    {
                        this.iGridHelper_CT.BindItemSource(dt_PHIEU_CTIET);
                        cboKhoNhap.EditValue = dt_PHIEU_CTIET.Rows[0]["KHO_ID"];
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }

                }
            }
            if(cboKhoNhap.Text=="")
            {
                cboKhoNhap.SelectedIndex = 0;
            }
            this.iDataSource.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
            this.iDataSource.Rows[0]["MA_HINHTHU_NHAPXUAT"] = "NHAP_NVL_DV_TRALAI";
            txtHinhThucNhap.Text = "Nhập vật tư trả lại từ sản phẩm dịch vụ";
            txtHinhThucNhap.IsEnabled = false;
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
        /// <summary>
        /// TableSchemaDefineBingding
        /// </summary>
        /// <returns></returns>
        /// 

        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEU_NHAPXUATKHO_ID", typeof(decimal));
                xDicUser.Add("PHIEUYEUCAU_NHAPXUATKHO_ID", typeof(decimal));
                xDicUser.Add("PHIEUYEUCAU_DV_ID", typeof(decimal));
                xDicUser.Add("KHACHHANG_NCC_ID", typeof(decimal));
                xDicUser.Add("TEN_KH", typeof(string));
                xDicUser.Add("SO_HOPDONG", typeof(string));
                xDicUser.Add("TEN_DONVI_VANCHUYEN", typeof(string));
                xDicUser.Add("DIADIEM_GIAO", typeof(string));
                xDicUser.Add("KHACHHANG_NCC_DONVI_VANCHUYEN_ID", typeof(decimal));
                xDicUser.Add("KHACHHANG_NCC_NOI_XUATHANG_ID", typeof(decimal));
                xDicUser.Add("TEN_HINHTHU_NHAPXUAT", typeof(string));
                xDicUser.Add("HOPDONG_ID", typeof(decimal));
                xDicUser.Add("IS_NHAPNHIEULAN", typeof(bool));
                xDicUser.Add("NGUOILIENHE_A", typeof(string));
                xDicUser.Add("MA_KHO", typeof(string));
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
                xDicUser.Add("PHIEU_KIEMKE_ID", typeof(decimal));
                xDicUser.Add("SO_PHIEU_KIEM_KE", typeof(string));
                xDicUser.Add("SO_LUONG_TONG", typeof(int));
                xDicUser.Add("KHO_ID", typeof(decimal));
                
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }
        private DataTable TableSchemaDefineBingding_Grid()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("STT", typeof(int));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEU_NHAPXUATKHO_CTIET_ID", typeof(decimal));
                xDicUser.Add("PHIEU_NHAPXUATKHO_ID", typeof(decimal));
                xDicUser.Add("KHO_ID", typeof(decimal));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("SOLO", typeof(string));
                xDicUser.Add("HANDUNG", typeof(string));
                xDicUser.Add("SO_LUONG_YEU_CAU", typeof(int));
                xDicUser.Add("SO_LUONG_XUAT", typeof(int));
                xDicUser.Add("SO_LUONG_TONG", typeof(int));
                xDicUser.Add("SO_LUONG_THUNG", typeof(int));
                xDicUser.Add("SO_LUONG_LE", typeof(int));
                xDicUser.Add("MA_TINHTRANG_HANG", typeof(string));
                xDicUser.Add("SODK", typeof(string));
                xDicUser.Add("DONGIA", typeof(decimal));
                xDicUser.Add("THANHTIEN", typeof(decimal));
                xDicUser.Add("PALLET_ID", typeof(decimal));
                xDicUser.Add("VITRI", typeof(string));
                xDicUser.Add("MA_VACH", typeof(string));
                xDicUser.Add("NGAY_SANXUAT", typeof(string));
                xDicUser.Add("MA_TINHTRANG_CV", typeof(string));
                xDicUser.Add("IS_NHAPNHIEULAN", typeof(bool));
                xDicUser.Add("QUYCACHDONGGOI", typeof(string));
                xDicUser.Add("KHO_VITRI_ID", typeof(decimal));
                xDicUser.Add("MA_SANPHAM_KH", typeof(string));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));

                xDicUser.Add("SO_LUONG", typeof(int));
                xDicUser.Add("SO_LUONG_TONG_THUCNHAP", typeof(int));
                xDicUser.Add("SO_LUONG_THUNG_THUCNHAP", typeof(int));
                xDicUser.Add("SO_LUONG_LE_THUCNHAP", typeof(int));
                xDicUser.Add("SOLUONG_QUYDOI", typeof(int));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }
        private DataTable TableSchemaDefineBingding_TraNVL_Ton()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("PHIEUYEUCAU_DV_TRANVL_TON_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEUYEUCAU_DV_ID", typeof(decimal));
                xDicUser.Add("PHIEU_NHAPXUATKHO_ID", typeof(decimal));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));
                xDicUser.Add("SO_LUONG_XUAT", typeof(int));
                xDicUser.Add("SO_LUONG_DASUDUNG", typeof(int));
                xDicUser.Add("SO_LUONG_CHUASUDUNG", typeof(int));
                xDicUser.Add("SO_LUONG_HUHONG", typeof(int));
                xDicUser.Add("RowVersion", typeof(long));
                xDicUser.Add("IsDelete", typeof(bool));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));
                xDicUser.Add("SOLUONG_QUYDOI", typeof(int));
                xDicUser.Add("TONG_SO_LUONG_DASUDUNG", typeof(int));
                xDicUser.Add("TONG_SO_LUONG_HUHONG", typeof(int));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
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
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_vattu_dichvu_MaVatTu"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_vattu_dichvu_TenVatTu"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_vattu_dichvu_DonViTinh"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_XUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTongXuat"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TONG_SO_LUONG_DASUDUNG", "Tổng đã sử dụng trước");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_DASUDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTongDaSDung"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_CHUASUDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSLChuaSD"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_HUHONG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSLHuHong"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TONG_SO_LUONG_HUHONG", "Tổng SL hư hỏng trước");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
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

                xColumn = new Column("SOLUONG_QUYDOI", "Qui đổi");
                xColumn.Width = 80;
                xColumn.Visible = false;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                

                this.iGridHelper.Initialize();
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
                this.iGridHelper_CT = new GridHelper(this.grd_2, this.grdView_2, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_MaThuoc"]));
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
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CT.Add(xColumn);


                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_PHIEU_YEU_CAU", "Số lượng phiếu yêu cầu");
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_CT.Add(xColumn);

                string[] header_tt = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblChangeStatus"]) };
                string[] id_tt = new string[] { "TEN_TINHTRANG_HANG" };
                string[] align_tt = new string[] { "left" };
                xColumn = new Column("MA_TINHTRANG_HANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChangeStatus"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("MA_TINHTRANG_HANG") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_TRANGTHAI;
                xColumn.ValueList.DisplayMember = "TEN_TINHTRANG_HANG";
                xColumn.ValueList.ValueMember = "MA_TINHTRANG_HANG";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_TRANGTHAI, header_tt, id_tt, align_tt);
                this.iGridHelper_CT.Add(xColumn);//combobox

                string[] header_kv = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]) };
                string[] id_kv = new string[] { "TEN_KHO_KHUVUC" };
                string[] align_kv = new string[] { "left" };
                xColumn = new Column("KHO_KHUVUC_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("KHO_KHUVUC_ID") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_KHUVUC;
                xColumn.ValueList.DisplayMember = "TEN_KHO_KHUVUC";
                xColumn.ValueList.ValueMember = "KHO_KHUVUC_ID";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_KHUVUC, header_kv, id_kv, align_kv);
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; ;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                string[] header_pl = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]) };
                string[] id_pl = new string[] { "TEN_PALLET" };
                string[] align_pl = new string[] { "left" };
                xColumn = new Column("PALLET_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("PALLET_ID") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_PALLET;
                xColumn.ValueList.DisplayMember = "TEN_PALLET";
                xColumn.ValueList.ValueMember = "PALLET_ID";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_PALLET, header_pl, id_pl, align_pl);
                this.iGridHelper_CT.Add(xColumn);

                string[] header_vth = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]) };
                string[] id_vth = new string[] { "VITRI" };
                string[] align_vth = new string[] { "left" };
                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("VITRI") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_VITRIHANG;
                xColumn.ValueList.DisplayMember = "VITRI";
                xColumn.ValueList.ValueMember = "VITRI";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_VITRIHANG, header_vth, id_vth, align_vth);
                this.iGridHelper_CT.Add(xColumn);//COMBOBOX

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CT.Add(xColumn);//BUTTON

                xColumn = new Column("NGAY_SANXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CT.Add(xColumn);


                string[] header_ttcv = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]) };
                string[] id_ttcv = new string[] { "TEN_TINHTRANG_CV" };
                string[] align_ttcv = new string[] { "left" };
                xColumn = new Column("MA_TINHTRANG_CV", Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("MA_TINHTRANG_CV") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_TINHTRANGCV;
                xColumn.ValueList.DisplayMember = "TEN_TINHTRANG_CV";
                xColumn.ValueList.ValueMember = "MA_TINHTRANG_CV";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_TINHTRANGCV, header_ttcv, id_ttcv, align_ttcv);
                this.iGridHelper_CT.Add(xColumn);//COMBOBOX

                if (pX_OR_N == "N")
                {
                    xColumn = new Column("THUC_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThucNhap"]));
                    xColumn.Width = 80;
                    xColumn.Visible = true;
                    xColumn.MaskType = MaskType.Numeric;
                    xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                    this.iGridHelper_CT.Add(xColumn);

                    xColumn = new Column("IS_NHAPNHIEULAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNhapNhieuLan"]));
                    xColumn.Width = 100;
                    xColumn.Visible = true;
                    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                    xColumn.ColumnType = ColumnControl.Checkbox;
                    xColumn.Binding = new Binding("IS_NHAPNHIEULAN") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                    this.iGridHelper_CT.Add(xColumn);//CHECKBOX
                }

                xColumn = new Column("SOLUONG_QUYDOI", "Qui đổi");
                xColumn.Width = 80;
                xColumn.Visible = false;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_CT.Add(xColumn);

                var converter = new System.Windows.Media.BrushConverter();
                var brush = (System.Windows.Media.Brush)converter.ConvertFromString("#F2F2F2");

                this.grdView_2.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_PHIEU_YEU_CAU",
                    Format = new Format() { Foreground = System.Windows.Media.Brushes.Red },
                    Expression = "[SO_LUONG_PHIEU_YEU_CAU] != null",
                    ApplyToRow = false
                });
                this.grdView_2.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_THUNG",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.grdView_2.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_LE",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.iGridHelper_CT.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid_CTPHIEUNHAP()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void xButton_RemoveClick(object sender, RoutedEventArgs e)
        {

            try
            {
                if (grdView.FocusedRowHandle < 0)
                {
                    return;
                }
                if (dt_Tra_NVL_Ton != null && dt_Tra_NVL_Ton.Rows.Count > 0)
                {
                    DataRowView row = this.grd.GetFocusedRow() as DataRowView;
                    if (this.dt_Tra_NVL_Ton.Rows[(grd.View as TableView).FocusedRowHandle]["PHIEUYEUCAU_DV_TRANVL_TON_ID"].ToString() == string.Empty)
                    {
                        dt_Tra_NVL_Ton.Rows.Remove(row.Row);
                        if (dt_Tra_NVL_Ton.Rows.Count <= 0)
                        {
                            return;
                        }
                        else
                        {
                            grdView.FocusedRowHandle = 0;
                        }
                        this.iGridHelper.BindItemSource(dt_Tra_NVL_Ton);
                        return;
                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, "Không thể xóa sản phẩm, hàng đã tồn tại trong phiếu!");
                        return;
                    }
                }


                
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);

            }
        }

        /// 
        ///-----------------------------------------------Build Combo------------------------
        ///
        private void LoadComboBox()
        {
            try
            {
                dt_KHO = _Tra_Lai_VatTu_DV.GetListDM_KHO(ConstCommon.DonViQuanLy);
                dt_KHO_KHUVUC = _Tra_Lai_VatTu_DV.DM_KHO_KHUVUC_GET_LIST_BY_KHO(ConstCommon.DonViQuanLy, 0);
                dt_PALLET = _Tra_Lai_VatTu_DV.GetListDM_PALLET(ConstCommon.DonViQuanLy);
                dt_TRANGTHAI = _Tra_Lai_VatTu_DV.GetData_For_gird_TINHTRANG_HANG(ConstCommon.DonViQuanLy);
                dt_KHUVUC = _Tra_Lai_VatTu_DV.GetData_For_gird_TENKHO_KHUVUC(ConstCommon.DonViQuanLy);
                dt_TINHTRANGCV = _Tra_Lai_VatTu_DV.GetData_For_gird_TINHTRANG_CV(ConstCommon.DonViQuanLy);
                dt_VITRIHANG = _Tra_Lai_VatTu_DV.GetData_For_gird_VITRI_HANG(ConstCommon.DonViQuanLy, ConstCommon.pKHO_ID);

                if (dt_KHO != null && dt_KHO.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboKhoNhap, "TEN_KHO", "KHO_ID", dt_KHO, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboKhoNhap, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    //this.iDataSource.Rows[0]["KHO_ID"] = cboKhoNhap.GetKeyValue(0);
                    
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadComboBox()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }


        ///---------------------------------end Build Combo-------------------------------
        ///
        private bool SetIsNull()
        {
            try
            {
                if (txtSoPhieu.Text.Trim() == "")
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng nhập [SỐ PHIẾU]!");
                    txtSoPhieu.Focus();
                    return false;
                }
                if (ConstCommon.check_String_Unicode(txtSoPhieu.Text.Trim()) == false)
                {
                    base.ShowMessage(MessageType.Information, "[SỐ PHIẾU] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteSYMBOL"]));
                    txtSoPhieu.Focus();
                    return false;
                }
                if (cboNgayNhap.Text.Trim() == "")
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng chọn ngày lập phiếu!");
                    cboNgayNhap.Focus();
                    return false;
                }
                if(cboKhoNhap.Text=="--Chọn--")
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng chọn kho nhập!");
                    cboKhoNhap.Focus();
                    return false;
                }
                if (_Nhap_Kho.KO_NHAPXUATKHO_CHECKEXISTS(ConstCommon.DonViQuanLy, PHIEU_NHAPXUATKHO_ID, txtSoPhieu.Text.Trim()) == true)
                {
                    base.ShowMessage(MessageType.Information, "Số phiếu đã bị trùng, xin vui lòng nhập số khác ");
                    return false;
                }
                else
                {
                    if (dt_PHIEU_CTIET.Rows.Count == 0)
                    {
                        if (pX_OR_N == "N")
                        {
                            base.ShowMessage(MessageType.Information, "[CHI TIẾT PHIẾU NHẬP HÀNG] " + " không thể rỗng!");
                            return false;
                        }
                    }
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
                return false;
            }
        }
        

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        private void btnChonPhieuYC_DV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_Chon_Phieu_YC_DV_popup", null);
                DataTable dt_SPCHON = (DataTable)xReturn;
                dt_Tra_NVL_Ton.Clear();
                //begin tao phieu chi tiet
                if ((dt_SPCHON != null) && (dt_SPCHON.Rows.Count > 0))
                {
                    txtPYCDV.Text = dt_SPCHON.Rows[0]["SOPHIEU_DV"].ToString();
                    iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"] = dt_SPCHON.Rows[0]["PHIEUYEUCAU_DV_ID"];
                    for (int i = 0; i < dt_SPCHON.Rows.Count; i++)
                    {
                        if (ContainDataRowInDataTable(dt_Tra_NVL_Ton, dt_SPCHON.Rows[i]) == false)
                        {
                            DataRow dr = dt_Tra_NVL_Ton.NewRow();
                            dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                            dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                            dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                            dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                            dr["MA_DONVI_TINH"] = dt_SPCHON.Rows[i]["MA_DONVI_TINH"];
                            dr["PHIEUYEUCAU_DV_ID"] = dt_SPCHON.Rows[i]["PHIEUYEUCAU_DV_ID"];
                            dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                            dr["SO_LUONG_XUAT"] = dt_SPCHON.Rows[i]["SO_LUONG"];
                            dr["SOLUONG_QUYDOI"] = dt_SPCHON.Rows[i]["SOLUONG_QUYDOI"];
                            dr["TONG_SO_LUONG_DASUDUNG"] = dt_SPCHON.Rows[i]["TONG_SO_LUONG_DASUDUNG"];
                            dr["TONG_SO_LUONG_HUHONG"] = dt_SPCHON.Rows[i]["TONG_SO_LUONG_HUHONG"];
                            dt_Tra_NVL_Ton.Rows.Add(dr);
                        }
                    }
                    if (dt_Tra_NVL_Ton != null)
                    {
                        this.iGridHelper.BindItemSource(dt_Tra_NVL_Ton);
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonPhieuYC_DV.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnTaoPhieuNhapVatTu_Click(object sender, RoutedEventArgs e)
        {
            if (dt_Tra_NVL_Ton == null && dt_Tra_NVL_Ton.Rows.Count <= 0)
            {
                return;
            }
            else
            {
                for (int i = 0; i < dt_Tra_NVL_Ton.Rows.Count; i++)
                {
                    if (ContainDataRowInDataTable(dt_PHIEU_CTIET, dt_Tra_NVL_Ton.Rows[i]) == false)
                    {
                        if (ConstCommon.NVL_NUM_LONG_NEW(dt_Tra_NVL_Ton.Rows[i]["SO_LUONG_CHUASUDUNG"]) <= 0)
                        {
                            base.ShowMessage(MessageType.Information, "Số lượng chưa sử dụng phải là số dương > 0.");
                            return ;
                        }
                        DataRow dr = dt_PHIEU_CTIET.NewRow();
                        dr["STT"] = dt_PHIEU_CTIET.Rows.Count + 1;
                        dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                        dr["PHIEU_NHAPXUATKHO_CTIET_ID"] = "0";
                        dr["PHIEU_NHAPXUATKHO_ID"] = "0";
                        if(cboKhoNhap.EditValue!=null && cboKhoNhap.EditValue.ToString()!="--Chọn--")
                        {
                            dr["KHO_ID"] = cboKhoNhap.EditValue;
                        }
                        dr["KHO_KHUVUC_ID"] = "0";
                        dr["SANPHAM_ID"] = dt_Tra_NVL_Ton.Rows[i]["SANPHAM_ID"];
                        dr["MA_SANPHAM"] = dt_Tra_NVL_Ton.Rows[i]["MA_SANPHAM"];
                        dr["TEN_SANPHAM"] = dt_Tra_NVL_Ton.Rows[i]["TEN_SANPHAM"];
                        dr["MA_DONVI_TINH"] = dt_Tra_NVL_Ton.Rows[i]["MA_DONVI_TINH"];

                        dr["MA_ITEM_TYPE"] = dt_Tra_NVL_Ton.Rows[i]["MA_ITEM_TYPE"];
                        dr["SO_LUONG_TONG"] = dt_Tra_NVL_Ton.Rows[i]["SO_LUONG_CHUASUDUNG"];
                        dr["SOLUONG_QUYDOI"] = dt_Tra_NVL_Ton.Rows[i]["SOLUONG_QUYDOI"];
                        dt_PHIEU_CTIET.Rows.Add(dr);
                        //------------------------------------------

                    }
                    if (dt_PHIEU_CTIET != null)
                    {
                        this.iGridHelper_CT.BindItemSource(dt_PHIEU_CTIET);
                    }
                    else
                    {
                        grd_2.ItemsSource = null;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SetIsNull() == false)
                {
                    return;
                }
                if (dt_PHIEU_CTIET.Rows.Count == 0)
                {
                    base.ShowMessage(MessageType.Information, "Chưa có phiếu chi tiết nào.");
                    return;
                }
                /*for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                {
                    if (dt_PHIEU_CTIET.Rows[i]["IS_NHAPNHIEULAN"].ToString() == "1")
                    {
                        this.iDataSource.Rows[0]["IS_NHAPNHIEULAN"] = "True";
                        break;
                    }
                }*/
                
                for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                {
                    if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG"]) == 0)
                    {
                        base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG TỔNG] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                        return;
                    }
                    if (dt_PHIEU_CTIET.Rows[i]["MA_TINHTRANG_HANG"].ToString() == String.Empty)
                    {
                        base.ShowMessage(MessageType.Information, "Chưa chọn [TRẠNG THÁI HÀNG].");
                        return;
                    }
                    if (dt_PHIEU_CTIET.Rows[i]["VITRI"].ToString() == String.Empty)
                    {
                        base.ShowMessage(MessageType.Information, "Chưa chọn [VỊ TRÍ HÀNG].");
                        return;
                    }
                    if (dt_PHIEU_CTIET.Rows[i]["MA_TINHTRANG_CV"].ToString() == String.Empty)
                    {
                        base.ShowMessage(MessageType.Information, "Chưa chọn [TÌNH TRẠNG CV].");
                        return;
                    }
                }

                DataSet dsReturn = null;
                dsReturn = _Tra_Lai_VatTu_DV.InsertKO_PHIEU_NHAPXUATKHO(CommonDataHelper.DonViQuanLy, dt_Tra_NVL_Ton, iDataSource, dt_PHIEU_CTIET, CommonDataHelper.UserName, pX_OR_N);
                if ((dsReturn != null) && (dsReturn.Tables.Count == 3))
                {

                    foreach (DataColumn item in dsReturn.Tables[0].Columns)
                    {
                        if (this.iDataSource.Columns[item.ColumnName] != null)
                        {
                            this.iDataSource.Rows[0][item.ColumnName] = dsReturn.Tables[0].Rows[0][item.ColumnName];
                        }
                    }

                    dt_PHIEU_CTIET.Clear();
                    dt_PHIEU_CTIET = dsReturn.Tables[1].Copy();
                    if (dt_PHIEU_CTIET != null)
                    {
                        this.iGridHelper_CT.BindItemSource(dt_PHIEU_CTIET);
                    }
                    else
                    {
                        grd_2.ItemsSource = null;
                    }
                    dt_Tra_NVL_Ton.Clear();
                    dt_Tra_NVL_Ton = dsReturn.Tables[2].Copy();
                    if (dt_Tra_NVL_Ton != null)
                    {
                        this.iGridHelper.BindItemSource(dt_Tra_NVL_Ton);
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }

                    ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                    PHIEU_NHAPXUATKHO_ID = ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"].ToString());
                    //  IsVisibleSoLuongYeuCau();
                }
                else
                {
                    base.ShowMessage(MessageType.Error, "Lưu không thành công");
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }


        }

        private void grdView_2_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                dt_PHIEU_CTIET.AcceptChanges();
                if (this.grd_2.GetFocusedRow() == null) return;
                int vitri = this.grdView_2.FocusedRowHandle;
                if (vitri < 0) return;

                //begin hoapd them vao
                if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[vitri]["SOLUONG_QUYDOI"].ToString()) == 0)
                {
                    //neu la dao tao

                    foreach (Column col in iGridHelper_CT.GetColumns)
                    {
                        if (col.Name == "SO_LUONG_THUNG".ToString())
                        {
                            iGridHelper_CT.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                        }
                        if (col.Name == "SO_LUONG_LE".ToString())
                        {
                            iGridHelper_CT.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                        }
                    }

                    //end hoapd them vao
                }
                else
                {
                    foreach (Column col in iGridHelper_CT.GetColumns)
                    {
                        if (col.Name == "SO_LUONG_THUNG".ToString())
                        {
                            iGridHelper_CT.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                        }
                        if (col.Name == "SO_LUONG_LE".ToString())
                        {
                            iGridHelper_CT.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_2_FocusedRowChanged()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        

        private void grdView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if (this.grd.GetFocusedRow() == null) return;
                int vitri = this.grdView.FocusedRowHandle;
                if (vitri < 0) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;
                int tongSLSuDung = 0;
                int tongSLHuHong = 0;
                tongSLSuDung = ConstCommon.NVL_NUM_NT_NEW(row["TONG_SO_LUONG_DASUDUNG"]) + ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_DASUDUNG"]);
                tongSLHuHong = ConstCommon.NVL_NUM_NT_NEW(row["TONG_SO_LUONG_HUHONG"]) + ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_HUHONG"]);
                row["SO_LUONG_CHUASUDUNG"] = (ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_XUAT"]) - tongSLSuDung - tongSLHuHong);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_CellValueChanged()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdView_2_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                int so_luong_quy_doi = 0;
                if (this.grd_2.GetFocusedRow() == null) return;
                DataRowView row = this.grd_2.GetFocusedRow() as DataRowView;
                if (ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]) != 0)
                    row["THANHTIEN"] = ConstCommon.NVL_NUM_DECIMAL_NEW(row["SO_LUONG_TONG"]) * ConstCommon.NVL_NUM_DECIMAL_NEW(row["DONGIA"]);
                if (e.Column.FieldName == "SO_LUONG_LE" || e.Column.FieldName == "SO_LUONG_TONG" || e.Column.FieldName == "SO_LUONG_THUNG")
                {
                    so_tong = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]);
                    so_thung = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_THUNG"]);
                    so_le = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_LE"]);
                    if (ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]) != 0)
                        row["THANHTIEN"] = ConstCommon.NVL_NUM_DECIMAL_NEW(row["SO_LUONG_TONG"]) * ConstCommon.NVL_NUM_DECIMAL_NEW(row["DONGIA"]);
                    if (ConstCommon.NVL_NUM_NT_NEW(row["SOLUONG_QUYDOI"]) > 0)
                    {
                        so_luong_quy_doi = ConstCommon.NVL_NUM_NT_NEW(row["SOLUONG_QUYDOI"]);
                        //SetIsNhapNhieuLan();
                        if (e.Column.FieldName == "SO_LUONG_TONG")
                        {
                            if ((so_tong != 0 && so_thung != 0 && so_le != 0) || so_thung == 0 || so_le == 0 || (so_thung == 0 && so_le == 0))
                            {
                                row["SO_LUONG_THUNG"] = so_tong / so_luong_quy_doi;
                                row["SO_LUONG_LE"] = so_tong % so_luong_quy_doi;
                                return;
                            }
                            if (so_tong == 0)
                            {
                                row["SO_LUONG_THUNG"] = 0;
                                row["SO_LUONG_LE"] = 0;
                                return;
                            }
                        }
                        if (e.Column.FieldName == "SO_LUONG_THUNG")
                        {
                            if (so_thung == 0)
                            {
                                row["SO_LUONG_LE"] = so_tong;
                                return;
                            }
                            if ((so_tong != 0 && so_thung != 0 && so_le != 0) || so_tong == 0 || so_tong != 0)
                            {
                                row["SO_LUONG_TONG"] = so_thung * so_luong_quy_doi + so_le;
                                return;
                            }
                        }
                        if (e.Column.FieldName == "SO_LUONG_LE")
                        {
                            if (so_tong != 0 && so_thung != 0 && so_le != 0)
                            {
                                if (so_le > so_luong_quy_doi)
                                {
                                    base.ShowMessage(MessageType.Information, "Bạn nhập quá [SỐ LƯỢNG QUY ĐỔI].");
                                    row["SO_LUONG_LE"] = so_tong - (so_thung * so_luong_quy_doi);
                                    return;
                                }
                                if (so_le == so_luong_quy_doi)
                                {
                                    row["SO_LUONG_THUNG"] = so_thung + (so_le / so_luong_quy_doi);
                                    row["SO_LUONG_LE"] = so_le % so_luong_quy_doi;
                                    row["SO_LUONG_TONG"] = so_thung * so_luong_quy_doi + so_le;
                                    return;
                                }
                                else
                                {
                                    row["SO_LUONG_TONG"] = so_thung * so_luong_quy_doi + so_le;
                                    return;
                                }
                            }
                            if (so_thung == 0)
                            {
                                row["SO_LUONG_TONG"] = so_le;
                                row["SO_LUONG_THUNG"] = so_thung + (so_le / so_luong_quy_doi);
                                row["SO_LUONG_LE"] = so_le % so_luong_quy_doi;
                                return;
                            }
                            if (so_tong == 0)
                            {
                                if (so_le >= so_luong_quy_doi)
                                {
                                    row["SO_LUONG_THUNG"] = so_thung + (so_le / so_luong_quy_doi);
                                    row["SO_LUONG_LE"] = so_le % so_luong_quy_doi;
                                    row["SO_LUONG_TONG"] = so_thung * so_luong_quy_doi + so_le;
                                    return;
                                }
                                else
                                {
                                    row["SO_LUONG_TONG"] = so_thung * so_luong_quy_doi + so_le;
                                    return;
                                }
                            }
                            if (so_le == 0)
                            {
                                row["SO_LUONG_TONG"] = so_thung * so_luong_quy_doi;
                                return;
                            }
                        }
                    }
                    else
                    {
                        //SetIsNhapNhieuLan();
                        row["SO_LUONG_THUNG"] = 0;
                        row["SO_LUONG_LE"] = 0;
                        return;
                    }
                }
                else
                {
                    //SetIsNhapNhieuLan();
                    return;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_2_CellValueChanged()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void cboNgayNhap_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            try
            {
                cboNgayNhap.EditValue = DateTime.ParseExact(cboNgayNhap.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            { }
            
        }
    }

}
