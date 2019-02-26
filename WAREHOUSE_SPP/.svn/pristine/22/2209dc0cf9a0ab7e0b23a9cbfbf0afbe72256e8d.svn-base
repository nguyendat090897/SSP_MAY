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
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Media;
using DevExpress.Xpf.Editors.Settings;
using DSofT.Framework.UICore.TaskProxy;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_NhapKho_popup.xaml
    /// </summary>
    public partial class frm_NhapKho_popup_nhap_hang_nhieu_lan : PopupBase
    {
        #region Khai báo
        DataTable dtTON_TREN_PHIEU = null;
        DataTable dt_LICH_SU_NHAP = null;
        GridHelper iGridHelper_TON_TREN_PHIEU = null;
        GridHelper iGridHelper_LICH_SU_NHAP = null;
        int so_tong, so_thung, so_le, so_luong_ton = 0;
        IPHIEU_NHAPXUATKHOBussiness _IPHIEU_NHAP_XUAT_KHO;
        long pPHIEU_NHAP_XUAT_KHO_ID = 0;
        #endregion
        #region Method
        public frm_NhapKho_popup_nhap_hang_nhieu_lan()
        {
            InitializeComponent();
            this.dtTON_TREN_PHIEU = this.TableSchemaDefineBingding_TON_TREN_PHIEU();
            this.dt_LICH_SU_NHAP = this.TableSchemaDefineBingding_LICH_SU_NHAP();
            _IPHIEU_NHAP_XUAT_KHO = new PHIEU_NHAPXUATKHOBussiness(string.Empty);
            this.DataContext = this.dtTON_TREN_PHIEU;
            Initialize_Grid_TON_TREN_PHIEU();
            Initialize_Grid_LICH_SU_NHAP();
            this.dtTON_TREN_PHIEU.Rows[0]["TEN_KHO"] = ConstCommon.pTEN_KHO;
            this.dtTON_TREN_PHIEU.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
            pPHIEU_NHAP_XUAT_KHO_ID = frm_NhapKho_popup.pPHIEU_XUATNHAP_KHO_ID;
            LoadData();
        }
        /// <summary>
        /// TableSchemaDefineBingding
        /// </summary>
        /// <returns></returns>
        /// 
        private DataTable TableSchemaDefineBingding_TON_TREN_PHIEU()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEU_NHAPXUATKHO_CTIET_ID", typeof(decimal));
                xDicUser.Add("PHIEU_NHAPXUATKHO_ID", typeof(decimal));
                xDicUser.Add("KHO_ID", typeof(decimal));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SO_PHIEU_NHAP", typeof(string));
                xDicUser.Add("TEN_KHO", typeof(string));
                xDicUser.Add("MA_SANPHAM_KH", typeof(string));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("SOLO", typeof(string));
                xDicUser.Add("HANDUNG", typeof(string));
                xDicUser.Add("QUYCACHDONGGOI", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));
                xDicUser.Add("TEN_DONVI_TINH", typeof(string));
                xDicUser.Add("IS_NHAPNHIEULAN", typeof(bool));
                xDicUser.Add("SO_LUONG_TON", typeof(int));

                xDicUser.Add("SO_LUONG_TONG", typeof(int));
                xDicUser.Add("SO_LUONG_THUNG", typeof(int));
                xDicUser.Add("SO_LUONG_LE", typeof(int));

                xDicUser.Add("SODK", typeof(string));
                xDicUser.Add("MA_TINHTRANG_HANG", typeof(string));
                xDicUser.Add("DONGIA", typeof(decimal));
                xDicUser.Add("THANHTIEN", typeof(decimal));
                xDicUser.Add("PALLET_ID", typeof(decimal));
                xDicUser.Add("TEN_PALLET", typeof(string));
                xDicUser.Add("VITRI", typeof(string));

                // xDicUser.Add("TONGTON", typeof(int));
                // xDicUser.Add("TONTHEOTHUNG", typeof(int));
                // xDicUser.Add("TONLE", typeof(int));
                xDicUser.Add("MA_VACH", typeof(string));
                xDicUser.Add("NGAY_SANXUAT", typeof(string));
                xDicUser.Add("MA_TINHTRANG_CV", typeof(string));
                xDicUser.Add("TEN_TINHTRANG_CV", typeof(string));
                // xDicUser.Add("soluonthucnhap", typeof(int));

                xDicUser.Add("SO_LUONG_TONG_THUCNHAP", typeof(int));
                xDicUser.Add("SO_LUONG_THUNG_THUCNHAP", typeof(int));
                xDicUser.Add("SO_LUONG_LE_THUCNHAP", typeof(int));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }
        private DataTable TableSchemaDefineBingding_LICH_SU_NHAP()
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
                xDicUser.Add("QUYCACHDONGGOI", typeof(string));
                xDicUser.Add("SO_LUONG_TONG", typeof(int));
                xDicUser.Add("SO_LUONG_THUNG", typeof(int));
                xDicUser.Add("SO_LUONG_LE", typeof(int));
                xDicUser.Add("MA_TINHTRANG_HANG", typeof(string));
                xDicUser.Add("TEN_TINHTRANG_HANG", typeof(string));
                xDicUser.Add("SODK", typeof(string));
                xDicUser.Add("DONGIA", typeof(decimal));
                xDicUser.Add("THANHTIEN", typeof(decimal));
                xDicUser.Add("PALLET_ID", typeof(decimal));
                xDicUser.Add("TEN_PALLET", typeof(string));
                xDicUser.Add("VITRI", typeof(string));
                xDicUser.Add("MA_VACH", typeof(string));
                xDicUser.Add("NGAY_SANXUAT", typeof(string));
                xDicUser.Add("MA_TINHTRANG_CV", typeof(string));
                xDicUser.Add("TEN_TINHTRANG_CV", typeof(string));
                xDicUser.Add("IS_NHAPNHIEULAN", typeof(bool));
                xDicUser.Add("NGAY_NHAP", typeof(string));
                xDicUser.Add("KHO_VITRI_ID", typeof(decimal));
                xDicUser.Add("TEN_KHO_KHUVUC", typeof(string));
                xDicUser.Add("MA_SANPHAM_KH", typeof(string));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));
                xDicUser.Add("TEN_DONVI_TINH", typeof(string));
                xDicUser.Add("SO_LUONG", typeof(int));
                xDicUser.Add("SO_LUONG_TONG_THUCNHAP", typeof(int));
                xDicUser.Add("SO_LUONG_THUNG_THUCNHAP", typeof(int));
                xDicUser.Add("SO_LUONG_LE_THUCNHAP", typeof(int));
                
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
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
      
        /// <summary>
        /// Binding gird
        /// </summary>
        private void Initialize_Grid_TON_TREN_PHIEU()
        {
            try
            {
                this.iGridHelper_TON_TREN_PHIEU = new GridHelper(this.grdPHIEU_CON_TON, this.grdViewPHIEU_CON_TON, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_MaThuoc"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuoc1"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("TEN_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);
                //sửa

                xColumn = new Column("SO_LUONG_TON", "Tồn trên phiếu");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.Foreground = Brushes.Red;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"])); 
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; ;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("TEN_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);
                //sưa
                xColumn = new Column("tongton", "Tổng tồn");
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; ;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);
                //sưa
                xColumn = new Column("tonthung", "Tồn theo thùng");
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; ;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);
                //sưa
                xColumn = new Column("tonle", "Tồn lẻ");
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; ;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"])); 
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);//BUTTON

                xColumn = new Column("NGAY_SANXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"])); 
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                xColumn = new Column("TEN_TINHTRANG_CV", Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);
                //doi thanh so luong thuc nhap
                xColumn = new Column("THUC_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThucNhap"])); 
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_TON_TREN_PHIEU.Add(xColumn);

                this.iGridHelper_TON_TREN_PHIEU.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void Initialize_Grid_LICH_SU_NHAP()
        {
            try
            {
                this.iGridHelper_LICH_SU_NHAP = new GridHelper(this.grdLICH_SU_NHAP_HANG, this.grdViewLICH_SU_NHAP_HANG, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("PHIEU_NHAPXUATKHO_CTIET_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_MaThuoc"]));
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_MaThuoc"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuoc1"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("TEN_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("NGAY_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["frmImportWarehousingMaterials_ImplementationDates"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("TEN_KHO_KHUVUC", Convert.ToString(UtilLanguage.ApplyLanguage()["frmImportWarehousingMaterials_ImplementationDates"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);
                
                xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; ;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("TEN_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);
                //sưa
                xColumn = new Column("tongton", "Tổng tồn");
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; ;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);
                //sưa
                xColumn = new Column("tonthung", "Tồn theo thùng");
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; ;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);
                //sưa
                xColumn = new Column("tonle", "Tồn lẻ");
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; ;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);//BUTTON

                xColumn = new Column("NGAY_SANXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                xColumn = new Column("TEN_TINHTRANG_CV", Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);
                //doi thanh so luong thuc nhap
                if (frm_NhapKho_popup.pN_OR_N == "N")
                {
                    xColumn = new Column("THUC_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThucNhap"]));
                    xColumn.Width = 80;
                    xColumn.Visible = true;
                    xColumn.MaskType = MaskType.Numeric;
                    xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                    this.iGridHelper_LICH_SU_NHAP.Add(xColumn);
                }
                else
                {
                    xColumn = new Column("THUC_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThucXuat"]));
                    xColumn.Width = 80;
                    xColumn.Visible = true;
                    xColumn.MaskType = MaskType.Numeric;
                    xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                    this.iGridHelper_LICH_SU_NHAP.Add(xColumn);
                }

                ButtonEditSettings xButton_Remove = new ButtonEditSettings();
                xButton_Remove.DefaultButtonClick += xButton_RemoveClick;
                xColumn = new Column("REMOVE", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_Remove"]));
                xColumn.Width = 50;
                xColumn.CustomEditor = xButton_Remove;
                xColumn.Visible = true;
                xColumn.ColumnType = ColumnControl.Custom;
                xColumn.MaxLenth = 5;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                this.iGridHelper_LICH_SU_NHAP.Add(xColumn);

                this.iGridHelper_LICH_SU_NHAP.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// LoadData
        /// </summary>
        private void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataSet> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_GET_NHAP_NHIEU_LAN(ConstCommon.DonViQuanLy, pPHIEU_NHAP_XUAT_KHO_ID);
                    });
                    Action<DataSet> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.dtTON_TREN_PHIEU = dt.Tables[0];
                            this.dt_LICH_SU_NHAP = dt.Tables[1];
                            this.iGridHelper_TON_TREN_PHIEU.BindItemSource(this.dtTON_TREN_PHIEU);
                            this.iGridHelper_LICH_SU_NHAP.BindItemSource(this.dt_LICH_SU_NHAP);
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
        /// SetIsNull
        /// </summary>
        private bool SetIsNull()
        {
            bool isNULL = false;
            for (int i = 0; i < dtTON_TREN_PHIEU.Rows.Count; i++)
            {
                if (ConstCommon.NVL_NUM_NT_NEW(dtTON_TREN_PHIEU.Rows[i]["SO_LUONG_TONG"]) != 0 || ConstCommon.NVL_NUM_NT_NEW(dtTON_TREN_PHIEU.Rows[i]["SO_LUONG_THUNG"]) != 0
                    || ConstCommon.NVL_NUM_NT_NEW(dtTON_TREN_PHIEU.Rows[i]["SO_LUONG_LE"]) != 0)
                {
                    isNULL = true;
                    break;
                }
               
            }
            return isNULL;
        }
        /// <summary>
        /// SaveData
        /// </summary>
        private void SaveData()
        {
            if (SetIsNull() == false)
            {
                base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG TỔNG,SỐ LƯỢNG THÙNG,SỐ LƯỢNG LẺ] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            bool result = _IPHIEU_NHAP_XUAT_KHO.InsertKO_PHIEU_NHAPXUATKHO_NHAP_NHIEU_LAN(this.dtTON_TREN_PHIEU.Copy(), CommonDataHelper.UserName, pPHIEU_NHAP_XUAT_KHO_ID);
            if (!result)
            {
                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                return;
            }
            else
            {
                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                LoadData();
            }
        }

        #endregion
        #region UI
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (dtTON_TREN_PHIEU.Rows.Count == 0) return;
            if (this.grdPHIEU_CON_TON.GetFocusedRow() == null) return;
            SaveData();
        }

        private void grdViewPHIEU_CON_TON_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {

                if (this.grdPHIEU_CON_TON.GetFocusedRow() == null) return;
                int vitri = this.grdViewPHIEU_CON_TON.FocusedRowHandle;
                if (vitri < 0) return;
                if (ConstCommon.NVL_NUM_LONG_NEW(dtTON_TREN_PHIEU.Rows[vitri]["SOLUONG_QUYDOI"].ToString()) == 0)
                {
                    foreach (Column col in iGridHelper_TON_TREN_PHIEU.GetColumns)
                    {
                        if (col.Name == "SO_LUONG_THUNG".ToString())
                        {
                            iGridHelper_TON_TREN_PHIEU.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                        }
                        if (col.Name == "SO_LUONG_LE".ToString())
                        {
                            iGridHelper_TON_TREN_PHIEU.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                        }
                    }
                }
                else
                {
                    foreach (Column col in iGridHelper_TON_TREN_PHIEU.GetColumns)
                    {
                        if (col.Name == "SO_LUONG_THUNG".ToString())
                        {
                            iGridHelper_TON_TREN_PHIEU.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                        }
                        if (col.Name == "SO_LUONG_LE".ToString())
                        {
                            iGridHelper_TON_TREN_PHIEU.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

        }

        private void grdViewPHIEU_CON_TON_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                int so_luong_quy_doi = 0;
                if (this.grdPHIEU_CON_TON.GetFocusedRow() == null) return;
                DataRowView row = this.grdPHIEU_CON_TON.GetFocusedRow() as DataRowView;
                if (e.Column.FieldName == "SO_LUONG_LE" || e.Column.FieldName == "SO_LUONG_TONG" || e.Column.FieldName == "SO_LUONG_THUNG")
                {
                    so_tong = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]);
                    so_thung = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_THUNG"]);
                    so_le = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_LE"]);
                    so_luong_ton = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TON"]);
                    if (so_tong > so_luong_ton)
                    {
                        base.ShowMessage(MessageType.Information, "Bạn nhập quá [SỐ LƯỢNG NHẬP TỒN LẠI] trong phiếu.");
                        row["SO_LUONG_TONG"] = 0;
                        return;
                    }
                    if (ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]) != 0)
                        row["THANHTIEN"] = ConstCommon.NVL_NUM_DECIMAL_NEW(row["SO_LUONG_TONG"]) * ConstCommon.NVL_NUM_DECIMAL_NEW(row["DONGIA"]);
                    if (ConstCommon.NVL_NUM_NT_NEW(row["SOLUONG_QUYDOI"]) > 0)
                    {
                        so_luong_quy_doi = ConstCommon.NVL_NUM_NT_NEW(row["SOLUONG_QUYDOI"]);
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
                        row["SO_LUONG_THUNG"] = 0;
                        row["SO_LUONG_LE"] = 0;
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// xButton_RemoveClick
        /// </summary>
        void xButton_RemoveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.grdLICH_SU_NHAP_HANG.GetFocusedRow() == null) return;
                DataRowView row = this.grdLICH_SU_NHAP_HANG.GetFocusedRow() as DataRowView;
                if (ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_NHAPXUATKHO_CTIET_ID"]) > 0)
                {
                    if (this.dt_LICH_SU_NHAP != null && this.dt_LICH_SU_NHAP.Rows[(grdLICH_SU_NHAP_HANG.View as TableView).FocusedRowHandle]["PHIEU_NHAPXUATKHO_CTIET_ID"].ToString() != string.Empty)
                    {
                        if (!base.ShowMessage(MessageType.OkCancel, "Bạn muốn xóa không?"))
                            return;
                        bool result = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_NHAP_NHIEU_LAN_DELETE(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.dt_LICH_SU_NHAP.Rows[(grdLICH_SU_NHAP_HANG.View as TableView).FocusedRowHandle]["PHIEU_NHAPXUATKHO_CTIET_ID"].ToString()), CommonDataHelper.UserName);
                        if (!result)
                        {
                            base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                            return;
                        }
                        else
                        {
                            ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["tblThongBao"]), Convert.ToString(UtilLanguage.ApplyLanguage()["tblThanhCong"]), notificationService);
                            LoadData();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                    this.dt_LICH_SU_NHAP.Rows.Remove(row.Row);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        #endregion
    }
}