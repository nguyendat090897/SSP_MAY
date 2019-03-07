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
using DSofT.Framework.UICore.TaskProxy;
using DevExpress.Xpf.Editors.Settings;
using System.Windows.Documents;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frmKiemKeHangHoa_popup.xaml
    /// </summary>
    public partial class frmKiemKeHangHoa_popup : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataRow iRowSelGb = null;
        DataTable dt_DMKHO = null;
        DataTable dt_PHIEU_CTIET = null;
        DataTable dt_NHAPKHO_DIEUCHINH = null;
        DataTable dt_XUATKHO_DIEUCHINH = null;
        DataTable dt_KHO_KHUVUC = null;
        DataTable dt_TRANGTHAI = null;
        DataTable dt_KHUVUC = null;
        DataTable dt_VITRIHANG = null;
        DataTable dt_TINHTRANGCV = null;
        DataTable dt_PALLET = null;
        DataSet dsReturn = null;
        IPHIEU_NHAPXUATKHOBussiness _IPHIEU_NHAP_XUAT_KHO;
        int so_tong_thuc_te, so_thung_thuc_te, so_le_thuc_te = 0;
        int so_tong_ton, so_thung_ton, so_le_ton = 0;
        int so_tong_lech, so_thung_lech, so_le_lech = 0;
        long pKHO_ID = 0;
        bool isFirstLoad = true;
        public frmKiemKeHangHoa_popup()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            dt_PHIEU_CTIET = this.TableSchemaDefineBingding_Grid();
            dt_NHAPKHO_DIEUCHINH = this.TableSchemaDefineBingding_NHAPXUATKHO();
            dt_XUATKHO_DIEUCHINH = this.TableSchemaDefineBingding_NHAPXUATKHO();
            dt_PHIEU_CTIET.Clear();
            _IPHIEU_NHAP_XUAT_KHO = new PHIEU_NHAPXUATKHOBussiness(string.Empty);
            this.DataContext = this.iDataSource;
            LoadComboBox();
            Initialize_Grid();
            this.iDataSource.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
        }
        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                btnXuatDieuChinh.Visibility = Visibility.Collapsed;
                btnNhapDieuChinh.Visibility = Visibility.Collapsed;
                iRowSelGb = this.Parameter[0] as DataRow;
                LoadComboBox();
                Initialize_Grid();
                if (iRowSelGb == null)
                { return; }
                DispalayRequest();
                if (this.Parameter.Count() > 1)
                {
                    DataRow[] dr_chitiet = this.Parameter[1] as DataRow[];
                    dt_PHIEU_CTIET.Clear();
                    if (dr_chitiet.Length > 0)
                    {
                        dt_PHIEU_CTIET = dr_chitiet.CopyToDataTable();
                    }
                    if (dt_PHIEU_CTIET != null)
                    {
                        isFirstLoad = false;
                        this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                        for(int i = 0;i < dt_PHIEU_CTIET.Rows.Count; i++)
                        {
                            if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG_LECH"]) != 0 && dt_PHIEU_CTIET.Rows[i]["IS_DADIEUCHINH"].ToString() == "0")
                            {
                                btnNhapDieuChinh.Visibility = Visibility.Visible;
                                btnXuatDieuChinh.Visibility = Visibility.Visible;
                                break;
                            }
                        }
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }
                }
            }
            else
            {
                btnNhapDieuChinh.Visibility = Visibility.Collapsed;
                btnXuatDieuChinh.Visibility = Visibility.Collapsed;
                this.iDataSource.Rows[0]["NGUOI_NHAP"] = CommonDataHelper.UserName;
                btnChonSP.Visibility = Visibility.Collapsed;
                isFirstLoad = true;
            }
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
                        return _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_KIEMKE_GET_ID(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_KIEMKE_ID"]));
                    });
                    Action<DataSet> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.dt_PHIEU_CTIET = dt.Tables[1];
                            this.iGridHelper.BindItemSource(this.dt_PHIEU_CTIET);
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
                xDicUser.Add("SOPHIEU", typeof(string));
                xDicUser.Add("NGAY_KIEMKE", typeof(string));
                xDicUser.Add("NGUOI_NHAP", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("PHIEU_KIEMKE_ID", typeof(decimal));
                xDicUser.Add("TEN_KH", typeof(string));
                xDicUser.Add("KHACHHANG_NCC_ID", typeof(decimal));
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
                xDicUser.Add("PHIEU_KIEMKE_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEU_KIEMKE_CTIET_ID", typeof(decimal));
                xDicUser.Add("KHO_ID", typeof(decimal));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));

                xDicUser.Add("SOLO", typeof(string));
                xDicUser.Add("HANDUNG", typeof(string));
                xDicUser.Add("QUYCACHDONGGOI", typeof(string));

                xDicUser.Add("SO_LUONG_TONG_LECH", typeof(int));
                xDicUser.Add("SO_LUONG_THUNG_LECH", typeof(int));
                xDicUser.Add("SO_LUONG_LE_LECH", typeof(int));
                xDicUser.Add("SO_LUONG_TONG_TON", typeof(int));
                xDicUser.Add("SO_LUONG_THUNG_TON", typeof(int));
                xDicUser.Add("SO_LUONG_LE_TON", typeof(int));
                xDicUser.Add("SO_LUONG_TONG_THUCTE", typeof(int));
                xDicUser.Add("SO_LUONG_THUNG_THUCTE", typeof(int));
                xDicUser.Add("SO_LUONG_LE_THUCTE", typeof(int));
                xDicUser.Add("SOLUONG_QUYDOI", typeof(int)); 

                xDicUser.Add("MA_TINHTRANG_HANG", typeof(string));
                xDicUser.Add("CHATLUONG_HANGHOA", typeof(string));
                xDicUser.Add("CHATLUONG_BAOBI", typeof(string));
                xDicUser.Add("PALLET_ID", typeof(decimal));
                xDicUser.Add("VITRI", typeof(string));
                xDicUser.Add("MA_VACH", typeof(string));
                xDicUser.Add("MA_TINHTRANG_CV", typeof(string));
                xDicUser.Add("IS_DADIEUCHINH", typeof(bool));

                xDicUser.Add("MA_SANPHAM_KH", typeof(string));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("TEN_KHO", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }
        private DataTable TableSchemaDefineBingding_NHAPXUATKHO()
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

                xDicUser.Add("SO_LUONG_PHIEU_YEU_CAU", typeof(int));

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
        /// <summary>
        /// Initialize_Grid
        /// </summary>
        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_KHO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKho"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


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
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_MaThuoc"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_ITEM_TYPE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblItemType"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuoc1"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                string[] header_vth = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]) };
                string[] id_vth = new string[] { "VITRI" };
                string[] align_vth = new string[] { "left" };
                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriKiemKe"]));
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
                this.iGridHelper.Add(xColumn);//COMBOBOX

                xColumn = new Column("SO_LUONG_TONG_TON", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongTongTonVT"]));
                xColumn.Width = 180;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "n0";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG_TON", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongThungTonVT"]));
                xColumn.Width = 180;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE_TON", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongLeTonVT"]));
                xColumn.Width = 180;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG_THUCTE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongTongThucTeVT"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";       
                xColumn.Foreground = Brushes.Red;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG_THUCTE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongThungThucTeVT"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.Foreground = Brushes.Red;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE_THUCTE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongLeThucTeVT"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.Foreground = Brushes.Red;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG_LECH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongTongLechVT"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG_LECH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongThungLechVT"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE_LECH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongLeLechVT"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("CHATLUONG_HANGHOA", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChatLHHOAKT"]));
                xColumn.Width = 180;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("CHATLUONG_BAOBI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChatLBBIKT"]));
                xColumn.Width = 180;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

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
                this.iGridHelper.Add(xColumn);//combobox

                string[] header_pl = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]) };
                string[] id_pl = new string[] { "TEN_PALLET" };
                string[] align_pl = new string[] { "left" };
                xColumn = new Column("PALLET_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 100;
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
                this.iGridHelper.Add(xColumn);

              

                xColumn = new Column("MA_VACH", "Mã vạch");
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);//BUTTON

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
                this.iGridHelper.Add(xColumn);//COMBOBOX

                xColumn = new Column("IS_DADIEUCHINH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDaDieuChinh"]));
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("IS_DADIEUCHINH") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                this.iGridHelper.Add(xColumn);//CHECKBOX

                xColumn = new Column("SOLUONG_QUYDOI", "Qui đổi");
                xColumn.Width = 80;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                var converter = new System.Windows.Media.BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#F2F2F2");

                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_THUNG_TON",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_LE_TON",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_THUNG_THUCTE",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_LE_THUCTE",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });
                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_THUNG_LECH",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_LE_LECH",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.iGridHelper.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void LoadComboBox()
        {
            try
            {
                dt_KHO_KHUVUC = _IPHIEU_NHAP_XUAT_KHO.DM_KHO_KHUVUC_GET_LIST_BY_KHO(ConstCommon.DonViQuanLy, 0);
                dt_PALLET = _IPHIEU_NHAP_XUAT_KHO.GetListDM_PALLET(ConstCommon.DonViQuanLy);
                dt_TRANGTHAI = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TINHTRANG_HANG(ConstCommon.DonViQuanLy, ConstCommon.pMA_HINHTHU_NHAPXUAT);
                dt_KHUVUC = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TENKHO_KHUVUC(ConstCommon.DonViQuanLy);
                dt_TINHTRANGCV = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TINHTRANG_CV(ConstCommon.DonViQuanLy);
                dt_VITRIHANG = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_VITRI_HANG(ConstCommon.DonViQuanLy, ConstCommon.pKHO_ID);


                DataTable dtNCC = _IPHIEU_NHAP_XUAT_KHO.GetData_For_Cbo_KhachHang_Ncc(ConstCommon.DonViQuanLy);
                if (dtNCC != null && dtNCC.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboNCC, "TEN_KH", "KHACHHANG_NCC_ID", dtNCC, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboNCC, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["KHACHHANG_NCC_ID"] = cboNCC.GetKeyValue(0);
                }
                dt_DMKHO = _IPHIEU_NHAP_XUAT_KHO.GetListDM_KHO(ConstCommon.DonViQuanLy);
                if (dt_DMKHO != null && dt_DMKHO.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboKHO_ID, "TEN_KHO", "KHO_ID", dt_DMKHO, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboKHO_ID, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["KHO_ID"] = cboKHO_ID.GetKeyValue(0);
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadComboBox()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private bool SetIsNull()
        {
            try
            {
                if (txtSOPHIEU.Text.Trim() == "")
                {
                    base.ShowMessage(MessageType.Information, "[SỐ PHIẾU] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    txtSOPHIEU.Focus();
                    return false;
                }
                if (ConstCommon.check_String_Unicode(txtSOPHIEU.Text.Trim()) == false)
                {
                    base.ShowMessage(MessageType.Information, "[SỐ PHIẾU] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteSYMBOL"]));
                    txtSOPHIEU.Focus();
                    return false;
                }
                if (dtNgayKiemKe.Text.Trim() == "")
                {
                    base.ShowMessage(MessageType.Information, "[NGÀY KIỂM KÊ] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    dtNgayKiemKe.Focus();
                    return false;
                }
                if (cboKHO_ID.Text == "--Chọn--")
                {
                    base.ShowMessage(MessageType.Information, "Chưa chọn [KHO] kiểm kê.");
                    cboKHO_ID.Focus();
                    return false;
                }
                if (_IPHIEU_NHAP_XUAT_KHO.KO_NHAPXUATKHO_CHECKEXISTS(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_KIEMKE_ID"]), txtSOPHIEU.Text.Trim()) == true)
                {
                    base.ShowMessage(MessageType.Information, "Số phiếu đã bị trùng, xin vui lòng nhập số khác ");
                    return false;
                }
                else
                {
                    if (dt_PHIEU_CTIET.Rows.Count == 0)
                    {

                        base.ShowMessage(MessageType.Information, "[CHI TIẾT PHIẾU KIỂM KÊ] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                        return false;
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
        private bool ContainDataRowInDataTable(DataTable dt, DataRow dr)
        {
            try
            {
                var k = (from r in dt.Rows.OfType<DataRow>() where r["SANPHAM_ID"].ToString() == dr["SANPHAM_ID"].ToString() select r).FirstOrDefault();
                var h = (from r in dt.Rows.OfType<DataRow>()
                         where r["VITRI"].ToString() == dr["VITRI"].ToString()
                         select r).FirstOrDefault();
                if (k != null && h != null)
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
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dt_PHIEU_CTIET.Rows.Count == 0)
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng bổ sung chi tiết kiểm kê.");
                    return;
                }
                if (SetIsNull() == false)
                {
                    return;
                }
                for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                {
                    if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG_TON"]) == 0)
                    {
                        base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG TỔNG TỒN] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                        return;
                    }
                    //if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG_THUCTE"]) == 0)
                    //{
                    //    base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG TỔNG THỰC TẾ] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    //    return;
                    //}
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

               // dsReturn = null;
                dsReturn = _IPHIEU_NHAP_XUAT_KHO.InsertorUpdateKO_PHIEU_KIEMKE_AND_CTIET(iDataSource, dt_PHIEU_CTIET, CommonDataHelper.UserName);
                if ((dsReturn != null) && (dsReturn.Tables.Count >= 2))
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
                        this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                        bool nhap = false;
                        bool xuat = false;
                        for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                        {
                            if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG_LECH"]) > 0)
                                nhap = true;
                            if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG_LECH"]) < 0)
                                xuat = true;
                        }
                        if (nhap == true && xuat == true)
                        {
                            btnNhapDieuChinh.Visibility = Visibility.Visible;
                            btnXuatDieuChinh.Visibility = Visibility.Visible;
                        }
                        else if (xuat == true)
                            btnXuatDieuChinh.Visibility = Visibility.Visible;
                        else if (nhap == true)
                        {
                            btnNhapDieuChinh.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            btnNhapDieuChinh.Visibility = Visibility.Collapsed;
                            btnXuatDieuChinh.Visibility = Visibility.Collapsed;
                        }
                        if (isFirstLoad == false)
                        {
                            bool result = true;
                            int tong, thung, le = 0;
                            string px_or_n = String.Empty;
                            for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                            {
                                if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG_LECH"]) != 0)
                                {
                                    tong = ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG_LECH"]);
                                    thung = ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_THUNG_LECH"]);
                                    le = ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_LE_LECH"]);
                                    if (tong < 0 || thung < 0 || le < 0)
                                    {
                                        px_or_n = "X";
                                        tong = tong * -1;
                                        thung = thung * -1;
                                        le = le * -1;
                                    }
                                    else
                                    {
                                        px_or_n = "N";
                                    }
                                    result = _IPHIEU_NHAP_XUAT_KHO.UpdateKO_PHIEU_NHAPXUATKHO_CTIET(dt_PHIEU_CTIET.Rows[i], ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_KIEMKE_ID"]),
                                        tong, thung, le, px_or_n, CommonDataHelper.UserName);
                                }
                                else
                                {
                                    DataRow[] iRowSelGb_phieu_nhapxuat_kho = dsReturn.Tables[3].Select("PHIEU_KIEMKE_ID=" + ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_KIEMKE_ID"]));
                                }
                            }
                            if (result == false)
                            {
                                base.ShowMessage(MessageType.Error, "Lưu không thành công");
                                return;
                            }
                        }
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }

                    ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
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

        private void dtNgayKiemKe_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            try
            {
                dtNgayKiemKe.EditValue = DateTime.ParseExact(dtNgayKiemKe.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {

            }
        }

        private void cboKHO_ID_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (cboKHO_ID.Text == "--Chọn--")
            {
                btnChonSP.Visibility = Visibility.Collapsed;
                pKHO_ID = 0;
            }
            else
            {
                btnChonSP.Visibility = Visibility.Visible;
                pKHO_ID = ConstCommon.NVL_NUM_LONG_NEW(cboKHO_ID.EditValue.ToString());
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnXuatDieuChinh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string pLA_KIEM_KIEM = "KK";
                long pID_PHIEU_KIEMKE = ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_KIEMKE_ID"]);
                DataRow[] iRowSelGb_CTXUAT = dsReturn.Tables[2].Select("PHIEU_KIEMKE_ID=" + pID_PHIEU_KIEMKE + "AND SO_LUONG_TONG_LECH < 0");
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_NhapKho_popup", iRowSelGb_CTXUAT, pID_PHIEU_KIEMKE, 'X', pLA_KIEM_KIEM);
                LoadData();
                btnXuatDieuChinh.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnXuatDieuChinh.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnNhapDieuChinh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string pLA_KIEM_KIEM = "KK";
                long pID_PHIEU_KIEMKE = ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_KIEMKE_ID"]);
                DataRow[] iRowSelGb_CTNHAP = dsReturn.Tables[2].Select("PHIEU_KIEMKE_ID=" + pID_PHIEU_KIEMKE + "AND SO_LUONG_TONG_LECH > 0");
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_NhapKho_popup", iRowSelGb_CTNHAP,pID_PHIEU_KIEMKE, 'N', pLA_KIEM_KIEM);
                LoadData();
                btnNhapDieuChinh.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNhapDieuChinh.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnChonSP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //object xReturn = null;
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_KiemKeHangHoa_ChonSP_popup");
                DataTable dt_SPCHON = (DataTable)xReturn;
                if ((dt_SPCHON != null) && (dt_SPCHON.Rows.Count > 0))
                {
                    //isSecondLoad = false;
                    if (dt_SPCHON.Rows[0]["GHI_CHU"].ToString() == "Y")
                    {
                        for (int i = 0; i < dt_SPCHON.Rows.Count; i++)
                        {
                            if (ContainDataRowInDataTable(dt_PHIEU_CTIET, dt_SPCHON.Rows[i]) == false)
                            {
                                DataRow dr = dt_PHIEU_CTIET.NewRow();
                                dr["STT"] = dt_PHIEU_CTIET.Rows.Count + 1;
                                dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr["PHIEU_KIEMKE_ID"] = "0";
                                dr["KHO_ID"] = dt_SPCHON.Rows[i]["KHO_ID"];
                                dr["VITRI"] = dt_SPCHON.Rows[i]["VITRI"];
                                dr["TEN_KHO"] = dt_SPCHON.Rows[i]["TEN_KHO"];
                                //dr["KHO_KHUVUC_ID"] = dt_SPCHON.Rows[i]["KHO_KHUVUC_ID"];
                                dr["MA_DONVI_TINH"] = dt_SPCHON.Rows[i]["MA_DONVI_TINH"];
                                dr["SOLO"] = dt_SPCHON.Rows[i]["SOLO"];
                                dr["HANDUNG"] = dt_SPCHON.Rows[i]["HANDUNG"];
                                dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM_KH"] = dt_SPCHON.Rows[i]["MA_SANPHAM_KH"];
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                dr["QUYCACHDONGGOI"] = dt_SPCHON.Rows[i]["QUYCACHDONGGOI"];

                                long kho_id = ConstCommon.NVL_NUM_LONG_NEW(dt_SPCHON.Rows[i]["KHO_ID"]);
                                long sanpham_id = ConstCommon.NVL_NUM_LONG_NEW(dt_SPCHON.Rows[i]["SANPHAM_ID"]);
                                string so_lo = dt_SPCHON.Rows[i]["SOLO"].ToString();
                                string han_dung = dt_SPCHON.Rows[i]["HANDUNG"].ToString();
                                string vi_tri = dt_SPCHON.Rows[i]["VITRI"].ToString();

                                int so_luong_nhap = _IPHIEU_NHAP_XUAT_KHO.GET_SO_LUONG_SAN_PHAM_NHAP(ConstCommon.DonViQuanLy, sanpham_id, kho_id, so_lo, han_dung, vi_tri);
                                int so_luong_xuat = _IPHIEU_NHAP_XUAT_KHO.GET_SO_LUONG_SAN_PHAM_XUAT(ConstCommon.DonViQuanLy, sanpham_id, kho_id, so_lo, han_dung, vi_tri);
                                int so_luong_ton_that = so_luong_nhap - so_luong_xuat;
                                dr["SO_LUONG_TONG_TON"] = so_luong_ton_that;
                                dr["SOLUONG_QUYDOI"] = dt_SPCHON.Rows[i]["SOLUONG_QUYDOI"];
                                int sl_quydoi = ConstCommon.NVL_NUM_NT_NEW(dt_SPCHON.Rows[i]["SOLUONG_QUYDOI"]);
                                int sl_tongton = so_luong_ton_that;
                                if (sl_quydoi > 0)
                                {
                                    dr["SO_LUONG_THUNG_TON"] = sl_tongton / sl_quydoi;
                                    dr["SO_LUONG_LE_TON"] = sl_tongton % sl_quydoi;
                                }
                                //<<<<<<< .mine
                                dr["HANDUNG"] = dt_SPCHON.Rows[i]["HANDUNG"];
                                dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM_KH"] = dt_SPCHON.Rows[i]["MA_SANPHAM_KH"];
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                dr["QUYCACHDONGGOI"] = dt_SPCHON.Rows[i]["QUYCACHDONGGOI"];
                                dr["MA_VACH"] = dt_SPCHON.Rows[i]["MA_VACH"];
//||||||| .r1311
                                dr["HANDUNG"] = dt_SPCHON.Rows[i]["HANDUNG"];
                                dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM_KH"] = dt_SPCHON.Rows[i]["MA_SANPHAM_KH"];
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                dr["QUYCACHDONGGOI"] = dt_SPCHON.Rows[i]["QUYCACHDONGGOI"]; 
//=======
//>>>>>>> .r1327
                                dt_PHIEU_CTIET.Rows.Add(dr);
                            }
                        }
                    }

                }
                if (dt_PHIEU_CTIET != null)
                {
                    grd.Columns["KHO_KHUVUC_ID"].Visible = false;
                    this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
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

        private void grdView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                int so_luong_quy_doi = 0;
                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;
                //--------------------Set số lượng kiểm kê thực tế
                if (e.Column.FieldName == "SO_LUONG_LE_THUCTE" || e.Column.FieldName == "SO_LUONG_TONG_THUCTE" || e.Column.FieldName == "SO_LUONG_THUNG_THUCTE")
                {
                    so_tong_thuc_te = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG_THUCTE"]);
                    so_thung_thuc_te = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_THUNG_THUCTE"]);
                    so_le_thuc_te = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_LE_THUCTE"]);
                    so_tong_ton  = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG_TON"]);
                    so_tong_lech = so_tong_thuc_te - so_tong_ton;
                    row["SO_LUONG_TONG_LECH"] = so_tong_lech;
                    if (ConstCommon.NVL_NUM_NT_NEW(row["SOLUONG_QUYDOI"]) > 0)
                    {
                        so_luong_quy_doi = ConstCommon.NVL_NUM_NT_NEW(row["SOLUONG_QUYDOI"]);
                        if(so_tong_lech > 0)
                        {
                            row["SO_LUONG_THUNG_LECH"] = so_tong_lech / so_luong_quy_doi;
                            row["SO_LUONG_LE_LECH"] = so_tong_lech % so_luong_quy_doi;
                            btnNhapDieuChinh.Visibility = Visibility.Visible;
                        }
                        if(so_tong_lech < 0)
                        {
                            row["SO_LUONG_THUNG_LECH"] = so_tong_lech / so_luong_quy_doi;
                            row["SO_LUONG_LE_LECH"] = (( -1 * so_tong_lech) % so_luong_quy_doi) * -1;
                            btnXuatDieuChinh.Visibility = Visibility.Visible;
                        }
                        if (e.Column.FieldName == "SO_LUONG_TONG_THUCTE")
                        {
                            if ((so_tong_thuc_te != 0 && so_thung_thuc_te != 0 && so_le_thuc_te != 0) || so_thung_thuc_te == 0 || so_le_thuc_te == 0 || (so_thung_thuc_te == 0 && so_le_thuc_te == 0))
                            {
                                row["SO_LUONG_THUNG_THUCTE"] = so_tong_thuc_te / so_luong_quy_doi;
                                row["SO_LUONG_LE_THUCTE"] = so_tong_thuc_te % so_luong_quy_doi;
                                return;
                            }
                            if (so_tong_thuc_te == 0)
                            {
                                row["SO_LUONG_THUNG_THUCTE"] = 0;
                                row["SO_LUONG_LE_THUCTE"] = 0;
                                return;
                            }
                        }
                        if (e.Column.FieldName == "SO_LUONG_THUNG_THUCTE")
                        {
                            if (so_thung_thuc_te == 0)
                            {
                                row["SO_LUONG_LE_THUCTE"] = so_tong_thuc_te;
                                return;
                            }
                            if ((so_tong_thuc_te != 0 && so_thung_thuc_te != 0 && so_le_thuc_te != 0) || so_tong_thuc_te == 0 || so_tong_thuc_te != 0)
                            {
                                row["SO_LUONG_TONG_THUCTE"] = so_thung_thuc_te * so_luong_quy_doi + so_le_thuc_te;
                                return;
                            }
                        }
                        if (e.Column.FieldName == "SO_LUONG_LE_THUCTE")
                        {
                            if (so_tong_thuc_te != 0 && so_thung_thuc_te != 0 && so_le_thuc_te != 0)
                            {
                                if (so_le_thuc_te > so_luong_quy_doi)
                                {
                                    base.ShowMessage(MessageType.Information, "Bạn nhập quá [SỐ LƯỢNG QUY ĐỔI].");
                                    row["SO_LUONG_LE_THUCTE"] = so_tong_thuc_te - (so_thung_thuc_te * so_luong_quy_doi);
                                    return;
                                }
                                if (so_le_thuc_te == so_luong_quy_doi)
                                {
                                    row["SO_LUONG_THUNG_THUCTE"] = so_thung_thuc_te + (so_le_thuc_te / so_luong_quy_doi);
                                    row["SO_LUONG_LE_THUCTE"] = so_le_thuc_te % so_luong_quy_doi;
                                    row["SO_LUONG_TONG_THUCTE"] = so_thung_thuc_te * so_luong_quy_doi + so_le_thuc_te;
                                    return;
                                }
                                else
                                {
                                    row["SO_LUONG_TONG_THUCTE"] = so_thung_thuc_te * so_luong_quy_doi + so_le_thuc_te;
                                    return;
                                }
                            }
                            if (so_thung_thuc_te == 0)
                            {
                                row["SO_LUONG_TONG_THUCTE"] = so_le_thuc_te;
                                row["SO_LUONG_THUNG_THUCTE"] = so_thung_thuc_te + (so_le_thuc_te / so_luong_quy_doi);
                                row["SO_LUONG_LE_THUCTE"] = so_le_thuc_te % so_luong_quy_doi;
                                return;
                            }
                            if (so_tong_thuc_te == 0)
                            {
                                if (so_le_thuc_te >= so_luong_quy_doi)
                                {
                                    row["SO_LUONG_THUNG_THUCTE"] = so_thung_thuc_te + (so_le_thuc_te / so_luong_quy_doi);
                                    row["SO_LUONG_LE_THUCTE"] = so_le_thuc_te % so_luong_quy_doi;
                                    row["SO_LUONG_TONG_THUCTE"] = so_thung_thuc_te * so_luong_quy_doi + so_le_thuc_te;
                                    return;
                                }
                                else
                                {
                                    row["SO_LUONG_TONG_THUCTE"] = so_thung_thuc_te * so_luong_quy_doi + so_le_thuc_te;
                                    return;
                                }
                            }
                            if (so_le_thuc_te == 0)
                            {
                                row["SO_LUONG_TONG_THUCTE"] = so_thung_thuc_te * so_luong_quy_doi;
                                return;
                            }
                        }
                    }
                    else
                    {
                        row["SO_LUONG_THUNG_THUCTE"] = 0;
                        row["SO_LUONG_LE_THUCTE"] = 0;
                        return;
                    }
                }
                //--------------------end số lượng kiểm kê thực tế

                //--------------------Set số lượng tồn trong hệ thống
                if (e.Column.FieldName == "SO_LUONG_TONG_TON" )
                {
                    so_tong_ton = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG_TON"]);
                    so_thung_ton = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_THUNG_TON"]);
                    so_le_ton = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_LE_TON"]);
                    if (ConstCommon.NVL_NUM_NT_NEW(row["SOLUONG_QUYDOI"]) > 0)
                    {
                        so_luong_quy_doi = ConstCommon.NVL_NUM_NT_NEW(row["SOLUONG_QUYDOI"]);
                        if (e.Column.FieldName == "SO_LUONG_TONG_TON")
                        {
                            if ((so_tong_ton != 0 && so_thung_ton != 0 && so_le_ton != 0) || so_thung_ton == 0 || so_le_ton == 0 || (so_thung_ton == 0 && so_le_ton == 0))
                            {
                                row["SO_LUONG_THUNG_TON"] = so_tong_ton / so_luong_quy_doi;
                                row["SO_LUONG_LE_TON"] = so_tong_ton % so_luong_quy_doi;
                                return;
                            }
                            if (so_tong_ton == 0)
                            {
                                row["SO_LUONG_THUNG_TON"] = 0;
                                row["SO_LUONG_LE_TON"] = 0;
                                return;
                            }
                        }
                    }
                    else
                    {
                        row["SO_LUONG_THUNG_TON"] = 0;
                        row["SO_LUONG_LE_TON"] = 0;
                        return;
                    }
                }

                //--------------------end số lượng tồn trong hệ thống
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSP.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                dt_PHIEU_CTIET.AcceptChanges();
                if (this.grd.GetFocusedRow() == null) return;
                int vitri = this.grdView.FocusedRowHandle;
                if (vitri < 0) return;
                if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[vitri]["SOLUONG_QUYDOI"].ToString()) == 0)
                {

                    foreach (Column col in iGridHelper.GetColumns)
                    {
                        if (col.Name == "SO_LUONG_THUNG_THUCTE".ToString())
                        {
                            iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                        }
                        if (col.Name == "SO_LUONG_LE_THUCTE".ToString())
                        {
                            iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                        }
                    }
                }
                else
                {
                    foreach (Column col in iGridHelper.GetColumns)
                    {
                        if (col.Name == "SO_LUONG_THUNG_THUCTE".ToString())
                        {
                            iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                        }
                        if (col.Name == "SO_LUONG_LE_THUCTE".ToString())
                        {
                            iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSP.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }

}