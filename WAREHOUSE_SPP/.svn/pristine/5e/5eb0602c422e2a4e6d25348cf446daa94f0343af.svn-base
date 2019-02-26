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
    /// Interaction logic for frm_PhieuYeuCauXuatKho_popup.xaml
    /// </summary>
    public partial class frm_lapphieu_xacnhan_hanghuhong_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataTable dt_PHIEU_CTIET = null;
        DataTable dt_TRANGTHAI = null;
        DataTable dt_KHUVUC = null;
        DataTable dt_VITRIHANG = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        IKO_PHIEU_XACNHAN_HUHONGBussiness _IKO_PHIEU_XACNHAN_HUHONG;
        int so_tong, so_thung, so_le = 0;
        public frm_lapphieu_xacnhan_hanghuhong_popup()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.dt_PHIEU_CTIET = this.TableSchemaDefineBingding_Grid();
            dt_PHIEU_CTIET.Clear();
            _IKO_PHIEU_XACNHAN_HUHONG = new KO_PHIEU_XACNHAN_HUHONGBussiness(string.Empty);
            this.DataContext = this.iDataSource;
            LoadCombobox();
            Initialize_Grid();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGb = this.Parameter[0] as DataRow;
                DispalayRequest();
                if (this.Parameter.Count() > 1)
                {
                    DataRow[] dr_chitiet = this.Parameter[1] as DataRow[];
                    LoadCombobox();
                    dt_PHIEU_CTIET.Clear();
                    if (dr_chitiet.Length > 0)
                    {
                        dt_PHIEU_CTIET = dr_chitiet.CopyToDataTable();
                    }
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
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEU_XACNHAN_HUHONG_ID", typeof(decimal));
                xDicUser.Add("SOPHIEU", typeof(string));
                xDicUser.Add("NGAY_XACNHAN", typeof(string));
                xDicUser.Add("SOPHIEU_CTU", typeof(string));
                xDicUser.Add("NGAY_CTU", typeof(string));
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
        private DataTable TableSchemaDefineBingding_Grid()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("STT", typeof(int));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEU_XACNHAN_HUHONG_CTIET_ID", typeof(decimal));
                xDicUser.Add("PHIEU_XACNHAN_HUHONG_ID", typeof(decimal));
                xDicUser.Add("KHO_ID", typeof(decimal));
                xDicUser.Add("TEN_KHO", typeof(string));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("SOLO", typeof(string));
                xDicUser.Add("HANDUNG", typeof(string));
                xDicUser.Add("QUYCACHDONGGOI", typeof(string));
                xDicUser.Add("VITRI", typeof(string));
                xDicUser.Add("LYDO", typeof(string));
                xDicUser.Add("SO_LUONG_TONG", typeof(int));
                xDicUser.Add("SO_LUONG_THUNG", typeof(int));
                xDicUser.Add("SO_LUONG_LE", typeof(int));
                xDicUser.Add("MA_TINHTRANG_HANG", typeof(string));
                xDicUser.Add("SO_DKY", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("MA_SANPHAM_KH", typeof(string));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));
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

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_MaThuoc"]));
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
                this.iGridHelper.Add(xColumn);//COMBOBOX

                xColumn = new Column("LYDO", "Lý do");
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_DKY", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                xColumn.Width = 150;
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
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("MA_TINHTRANG_HANG") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_TRANGTHAI;
                xColumn.ValueList.DisplayMember = "TEN_TINHTRANG_HANG";
                xColumn.ValueList.ValueMember = "MA_TINHTRANG_HANG";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_TRANGTHAI, header_tt, id_tt, align_tt);
                this.iGridHelper.Add(xColumn);//combobox

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGhiChuNhomSP"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                ButtonEditSettings xButton_Remove = new ButtonEditSettings();
                xButton_Remove.AllowDefaultButton = false;
                ButtonInfo btnmove = new ButtonInfo();
                xButton_Remove.Buttons.Add(btnmove);
                btnmove.Click += xButton_RemoveClick;
                btnmove.Content = "REMOVE";

                xColumn = new Column("REMOVE", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_Remove"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.CustomEditor = xButton_Remove;
                xColumn.ColumnType = ColumnControl.Custom;
                this.iGridHelper.Add(xColumn);

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
                    FieldName = "SO_LUONG_THUNG",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_LE",
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
        private void LoadCombobox()
        {
            try
            {
                dt_TRANGTHAI = _IKO_PHIEU_XACNHAN_HUHONG.GetData_For_gird_TINHTRANG_HANG(ConstCommon.DonViQuanLy);
                dt_KHUVUC = _IKO_PHIEU_XACNHAN_HUHONG.GetData_For_gird_TENKHO_KHUVUC(ConstCommon.DonViQuanLy);
                dt_VITRIHANG = _IKO_PHIEU_XACNHAN_HUHONG.GetData_For_gird_VITRI_HANG(ConstCommon.DonViQuanLy, ConstCommon.pKHO_ID);
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
                if (txtSoPhieu.Text.Trim() == "")
                {
                    base.ShowMessage(MessageType.Information, "[SỐ PHIẾU] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    txtSoPhieu.Focus();
                    return false;
                }
                if (ConstCommon.check_String_Unicode(txtSoPhieu.Text.Trim()) == false)
                {
                    base.ShowMessage(MessageType.Information, "[SỐ PHIẾU] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteSYMBOL"]));
                    txtSoPhieu.Focus();
                    return false;
                }
                if (txtNGAY_XACNHAN.Text.Trim() == "")
                {
                    base.ShowMessage(MessageType.Information, "[NGÀY XÁC NHẬN] phiếu " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    txtNGAY_XACNHAN.Focus();
                    return false;
                }
                if (_IKO_PHIEU_XACNHAN_HUHONG.KO_NHAPXUATKHO_CHECKEXISTS(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_XACNHAN_HUHONG_ID"]), txtSoPhieu.Text.Trim()) == true)
                {
                    base.ShowMessage(MessageType.Information, "Số phiếu đã bị trùng, xin vui lòng nhập số khác ");
                    return false;
                }
                else
                {
                    if (dt_PHIEU_CTIET.Rows.Count == 0)
                    {
                            base.ShowMessage(MessageType.Information, "[CHI TIẾT PHIẾU XÁC NHẬN HÀNG HƯ HỎNG] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
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
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.iDataSource.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                if (dt_PHIEU_CTIET.Rows.Count == 0)
                {
                    base.ShowMessage(MessageType.Information, "Chưa có phiếu chi tiết nào.");
                    return;
                }
                if (SetIsNull() == false)
                {
                    return;
                }
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
                }

                DataSet dsReturn = null;
                dsReturn = _IKO_PHIEU_XACNHAN_HUHONG.InsertorUpdateKO_PHIEU_XACNHAN_HUHONG(iDataSource, dt_PHIEU_CTIET, CommonDataHelper.UserName);
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

        private void btnChonSanPham_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = null;
                xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP_Kho", null);
                DataTable dt_SPCHON = (DataTable)xReturn;
                if ((dt_SPCHON != null) && (dt_SPCHON.Rows.Count > 0))
                {
                    if (dt_SPCHON.Rows[0]["GHI_CHU"].ToString() == "Y")
                    {
                        for (int i = 0; i < dt_SPCHON.Rows.Count; i++)
                        {
                            if (ContainDataRowInDataTable(dt_PHIEU_CTIET, dt_SPCHON.Rows[i]) == false)
                            {
                                DataRow dr = dt_PHIEU_CTIET.NewRow();
                                dr["STT"] = dt_PHIEU_CTIET.Rows.Count + 1;
                                dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr["PHIEU_XACNHAN_HUHONG_ID"] = "0";
                                dr["KHO_ID"] = dt_SPCHON.Rows[i]["KHO_ID"];
                                dr["TEN_KHO"] = dt_SPCHON.Rows[i]["TEN_KHO"];
                                dr["KHO_KHUVUC_ID"] = dt_SPCHON.Rows[i]["KHO_KHUVUC_ID"];
                                dr["SOLO"] = dt_SPCHON.Rows[i]["SOLO"];
                                dr["HANDUNG"] = dt_SPCHON.Rows[i]["HANDUNG"];
                              //  dr["LYDO"] = String.Empty;
                               // dr["SO_DKY"] = String.Empty;
                                dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM_KH"] = dt_SPCHON.Rows[i]["MA_SANPHAM_KH"];
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                dr["MA_TINHTRANG_HANG"] = "HH";
                                dr["QUYCACHDONGGOI"] = dt_SPCHON.Rows[i]["QUYCACHDONGGOI"];
                                dr["MA_DONVI_TINH"] = dt_SPCHON.Rows[i]["MA_DONVI_TINH"];
                                dr["SOLUONG_QUYDOI"] = dt_SPCHON.Rows[i]["SOLUONG_QUYDOI"];
                                dt_PHIEU_CTIET.Rows.Add(dr);
                            }
                        }
                    }

                }
                if (dt_PHIEU_CTIET != null)
                {
                    this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                }
                else
                {
                    grd.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSanPham.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void txtNGAY_CTU_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                txtNGAY_CTU.EditValue = DateTime.ParseExact(txtNGAY_CTU.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {

            }
        }

        private void txtNGAY_XACNHAN_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                txtNGAY_XACNHAN.EditValue = DateTime.ParseExact(txtNGAY_XACNHAN.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {

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
                        if (col.Name == "SO_LUONG_THUNG".ToString())
                        {
                            iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                        }
                        if (col.Name == "SO_LUONG_LE".ToString())
                        {
                            iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                        }
                    }
                }
                else
                {
                    foreach (Column col in iGridHelper.GetColumns)
                    {
                        if (col.Name == "SO_LUONG_THUNG".ToString())
                        {
                            iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                        }
                        if (col.Name == "SO_LUONG_LE".ToString())
                        {
                            iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSanPham.Tag.ToString());
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
                if (e.Column.FieldName == "SO_LUONG_LE" || e.Column.FieldName == "SO_LUONG_TONG" || e.Column.FieldName == "SO_LUONG_THUNG")
                {
                    so_tong = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]);
                    so_thung = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_THUNG"]);
                    so_le = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_LE"]);
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSanPham.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

        }
        void xButton_RemoveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;
                if (ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_XACNHAN_HUHONG_CTIET_ID"]) > 0)
                {
                    if (this.dt_PHIEU_CTIET != null && this.dt_PHIEU_CTIET.Rows[(grd.View as TableView).FocusedRowHandle]["PHIEU_XACNHAN_HUHONG_CTIET_ID"].ToString() != string.Empty)
                    {
                        if (!base.ShowMessage(MessageType.OkCancel, "Bạn muốn xóa không?"))
                            return;
                        bool result = _IKO_PHIEU_XACNHAN_HUHONG.KO_PHIEU_XACNHAN_HUHONG_CTIET_DELETE(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.dt_PHIEU_CTIET.Rows[(grd.View as TableView).FocusedRowHandle]["PHIEU_XACNHAN_HUHONG_CTIET_ID"].ToString()), CommonDataHelper.UserName);
                        this.dt_PHIEU_CTIET.Rows.Remove(row.Row);
                        if (!result)
                        {
                            base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                            return;
                        }
                        else
                        {
                            ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["tblThongBao"]), Convert.ToString(UtilLanguage.ApplyLanguage()["tblThanhCong"]), notificationService);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                    this.dt_PHIEU_CTIET.Rows.Remove(row.Row);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }
}
    