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
using System.Globalization;
using System.Linq;
using DevExpress.Xpf.Editors.Settings;
using System.Windows.Media;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// DEV: Nguyen Van Huynh
    /// Editdate: 10/05/2018
    /// </summary>
    public partial class frm_DSHBT_Duoc_Xuat_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataTable dt_KHO, dt_KHO_KHUVUC, dt_VITRIHANG, dt_TRANGTHAI, dt_KHUVUC = null;
        GridHelper iGridHelper = null;
        DataTable dt_PHIEU_CTIET = null;
        DataRow iRowSelGb = null;
        bool isFirstLoad = true;
        IKO_PHIEU_BT_CHOPHEPXUATBussiness _KO_PHIEU_BT_CHOPHEPXUATBussiness;
        string pMA_DVIQLY = String.Empty;
        public frm_DSHBT_Duoc_Xuat_popup()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            dt_PHIEU_CTIET = this.TableSchemaDefineBingding_Grid();
            dt_PHIEU_CTIET.Clear();
            _KO_PHIEU_BT_CHOPHEPXUATBussiness = new KO_PHIEU_BT_CHOPHEPXUATBussiness(string.Empty);
            this.DataContext = this.iDataSource;
            LoadComboBox();
            Initialize_Grid();
            pMA_DVIQLY = ConstCommon.DonViQuanLy;
        }
        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                isFirstLoad = false;
                iRowSelGb = this.Parameter[0] as DataRow;
                LoadComboBox();
                Initialize_Grid();
                if (iRowSelGb == null)
                { return; }
                DispalayRequest();
                if (this.Parameter.Count() > 1)
                {
                    cboKHO.IsEnabled = false;
                    cboKHU_VUC.IsEnabled = false;
                    DataRow[] dr_chitiet = this.Parameter[1] as DataRow[];
                    dt_PHIEU_CTIET.Clear();
                    if (dr_chitiet.Length > 0)
                    {
                        dt_PHIEU_CTIET = dr_chitiet.CopyToDataTable();
                        this.iDataSource.Rows[0]["KHO_ID"] = ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[0]["KHO_ID"]);
                        this.iDataSource.Rows[0]["KHO_KHUVUC_ID"] = ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[0]["KHO_KHUVUC_ID"]);
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
            else
            {
                this.iDataSource.Rows[0]["NGUOI_THUC_HIEN"] = CommonDataHelper.UserName;

                isFirstLoad = true;
                return;
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
                xDicUser.Add("PHIEU_BT_CHOPHEPXUAT_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("SOPHIEU", typeof(string));
                xDicUser.Add("NGAY_CHOPHEP", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TEN_KHO", typeof(string));
                xDicUser.Add("NGUOI_THUC_HIEN", typeof(string));
                xDicUser.Add("KHO_ID", typeof(decimal));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
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
                xDicUser.Add("PHIEU_BT_CHOPHEPXUAT_CTIET_ID", typeof(decimal));
                xDicUser.Add("PHIEU_BT_CHOPHEPXUAT_ID", typeof(decimal));
                xDicUser.Add("KHO_ID", typeof(decimal));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("SOLO", typeof(string));
                xDicUser.Add("HANDUNG", typeof(string));
                xDicUser.Add("VITRI", typeof(string));
                xDicUser.Add("IS_CHOPHEP", typeof(bool));
                xDicUser.Add("TU_NGAY", typeof(string));
                xDicUser.Add("DEN_NGAY", typeof(string));
                xDicUser.Add("MA_TINHTRANG_HANG", typeof(string));

                xDicUser.Add("KHO_VITRI_ID", typeof(decimal));
                xDicUser.Add("MA_SANPHAM_KH", typeof(string));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));


                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }
        private void LoadComboBox()
        {
            try
            {
                dt_KHO = _KO_PHIEU_BT_CHOPHEPXUATBussiness.GetListDM_KHO(ConstCommon.DonViQuanLy);
                dt_KHO_KHUVUC = _KO_PHIEU_BT_CHOPHEPXUATBussiness.DM_KHO_KHUVUC_GET_LIST_BY_KHO(ConstCommon.DonViQuanLy, 0);
                dt_TRANGTHAI = _KO_PHIEU_BT_CHOPHEPXUATBussiness.GetData_For_gird_TINHTRANG_HANG(ConstCommon.DonViQuanLy);
                dt_KHUVUC = _KO_PHIEU_BT_CHOPHEPXUATBussiness.GetData_For_gird_TENKHO_KHUVUC(ConstCommon.DonViQuanLy);
                dt_VITRIHANG = _KO_PHIEU_BT_CHOPHEPXUATBussiness.GetData_For_gird_VITRI_HANG(ConstCommon.DonViQuanLy, ConstCommon.pKHO_ID);

                if (dt_KHO != null && dt_KHO.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboKHO, "TEN_KHO", "KHO_ID", dt_KHO, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboKHO, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["KHO_ID"] = cboKHO.GetKeyValue(0);
                }
                if (dt_KHUVUC != null && dt_KHUVUC.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboKHU_VUC, "TEN_KHO_KHUVUC", "KHO_KHUVUC_ID", dt_KHUVUC, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboKHU_VUC, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["KHO_KHUVUC_ID"] = cboKHU_VUC.GetKeyValue(0);
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadComboBox()");
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
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
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

                //DateEditSettings xEditTU_NGAY = new DateEditSettings();
                //xEditTU_NGAY.DisplayTextConverter = DateTime.ParseExact(xEditTU_NGAY.GetDisplayText.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //xEditTU_NGAY.DisplayFormat = "dd/MM/yyyy";
                // xColumn.CustomEditor = xEditTU_NGAY;
                xColumn = new Column("TU_NGAY", "Từ ngày");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.ColumnType = ColumnControl.DateEdit;
                xColumn.DataType = typeof(DateTime);
                xColumn.MaskType = MaskType.DateTime;
                xColumn.EditMask = "dd/MM/yyyy";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.IsNullValidate = true;
                xColumn.Binding = new Binding("TU_NGAY") { Converter = new ConverterStringDateTime(), UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, ConverterParameter = "dd/MM/yyyy", Mode = BindingMode.TwoWay };
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("DEN_NGAY", "Đến ngày");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.ColumnType = ColumnControl.DateEdit;
                xColumn.DataType = typeof(DateTime);
                xColumn.MaskType = MaskType.DateTime;
                xColumn.EditMask = "dd/MM/yyyy";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.IsNullValidate = true;
                xColumn.Binding = new Binding("DEN_NGAY") { Converter = new ConverterStringDateTime(), UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, ConverterParameter = "dd/MM/yyyy", Mode = BindingMode.TwoWay };
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("IS_CHOPHEP", "Cho phép xuất");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("IS_CHOPHEP") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                this.iGridHelper.Add(xColumn);//CHECKBOX

                this.iGridHelper.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void txtNGAY_XACNHAN_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            try
            {
                txtNGAY_XACNHAN.EditValue = DateTime.ParseExact(txtNGAY_XACNHAN.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {

            }
        }

        private void cboKHO_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (isFirstLoad == true)
            {
                dt_PHIEU_CTIET.Clear();
                dt_PHIEU_CTIET = _KO_PHIEU_BT_CHOPHEPXUATBussiness.KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHO(ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["KHO_ID"]), ConstCommon.DonViQuanLy);
                if (dt_PHIEU_CTIET != null)
                {
                    this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                }
                else
                {
                    grd.ItemsSource = null;
                }
                cboKHU_VUC.ItemsSource = null;
                dt_KHO_KHUVUC = _KO_PHIEU_BT_CHOPHEPXUATBussiness.DM_KHO_KHUVUC_GET_LIST_BY_KHO(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["KHO_ID"]));
                if (dt_KHO_KHUVUC != null && dt_KHO_KHUVUC.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboKHU_VUC, "TEN_KHO_KHUVUC", "KHO_KHUVUC_ID", dt_KHO_KHUVUC, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboKHU_VUC, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    if (isFirstLoad == true)
                        this.iDataSource.Rows[0]["KHO_KHUVUC_ID"] = cboKHU_VUC.GetKeyValue(0);
                }
                else
                    cboKHU_VUC.ItemsSource = null;
            }
        }

        private void cboKHU_VUC_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (isFirstLoad == true)
            {
                if (cboKHU_VUC.ItemsSource == null || cboKHU_VUC.Text == "--Chọn--")
                {
                    dt_PHIEU_CTIET.Clear();
                    dt_PHIEU_CTIET = _KO_PHIEU_BT_CHOPHEPXUATBussiness.KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHO(ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["KHO_ID"]), ConstCommon.DonViQuanLy);
                    if (dt_PHIEU_CTIET != null)
                    {
                        this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }
                    return;
                }
                else
                {
                    dt_PHIEU_CTIET.Clear();
                    dt_PHIEU_CTIET = _KO_PHIEU_BT_CHOPHEPXUATBussiness.KO_PHIEU_BT_CHOPHEPXUAT_GET_SANPHAM_BY_KHUVUC(ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["KHO_ID"]), ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["KHO_KHUVUC_ID"]), ConstCommon.DonViQuanLy);
                    if (dt_PHIEU_CTIET != null)
                    {
                        this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }
                    return;
                }
            }
        }

        private void grdView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;
                if(e.Column.FieldName == "ROWNUM" || e.Column.FieldName == "MA_SANPHAM" || e.Column.FieldName == "MA_SANPHAM_KH" || e.Column.FieldName == "TEN_SANPHAM" || e.Column.FieldName == "SOLO"
                    || e.Column.FieldName == "HANDUNG" || e.Column.FieldName == "TEN_KHO" || e.Column.FieldName == "KHO_KHUVUC_ID" || e.Column.FieldName == "VITRI" || e.Column.FieldName == "MA_TINHTRANG_HANG"
                    || e.Column.FieldName == "IS_CHOPHEP")
                {
                    return;
                }
                else
                {
                    
                    TimeSpan so_ngay_cho_phep = (DateTime.ParseExact(row["DEN_NGAY"].ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture)) - (DateTime.ParseExact(row["TU_NGAY"].ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    if (so_ngay_cho_phep.Days < 0)
                    {
                        base.ShowMessage(MessageType.Information, "[NGÀY ĐẾN HẠN] phải lớn hơn [NGÀY BẮT ĐẦU].");
                        return;
                    }
                    

                    long pPHIEU_BT_CHOPHEPXUAT_CTIET_ID = ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_BT_CHOPHEPXUAT_CTIET_ID"]);
                    long pKHO_ID = ConstCommon.NVL_NUM_LONG_NEW(row["KHO_ID"]);
                    long pKHO_KHUVUC_ID = ConstCommon.NVL_NUM_LONG_NEW(row["KHO_KHUVUC_ID"]);
                    long pSANPHAM_ID = ConstCommon.NVL_NUM_LONG_NEW(row["SANPHAM_ID"]);
                    string pMA_ITEM_TYPE = row["MA_ITEM_TYPE"].ToString();
                    string pVITRI = row["VITRI"].ToString();
                    string pTU_NGAY = (DateTime.ParseExact(row["TU_NGAY"].ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToString();
                    string pDEN_NGAY = (DateTime.ParseExact(row["DEN_NGAY"].ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToString();
                    bool isValues = _KO_PHIEU_BT_CHOPHEPXUATBussiness.KO_PHIEU_BT_CHOPHEPXUAT_CTIET_CHECK_NGAY(pMA_DVIQLY, pPHIEU_BT_CHOPHEPXUAT_CTIET_ID, pKHO_ID, pKHO_KHUVUC_ID, pMA_ITEM_TYPE, pSANPHAM_ID, pVITRI, pTU_NGAY, pDEN_NGAY);
                    if(isValues == true)
                    {
                        base.ShowMessage(MessageType.Information, "[SẢN PHẨM] "+ row["TEN_SANPHAM"].ToString()+" đã được cho phép xuất biệt trữ trong khoảng thời gian này.");
                        if (row["IS_CHOPHEP"].ToString() == "1")
                            row["IS_CHOPHEP"] = 0;
                        foreach (Column col in iGridHelper.GetColumns)
                        {
                            if (col.Name == "IS_CHOPHEP".ToString())
                            {
                                iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                            }
                        }
                        return;
                    }
                    else
                    {
                        foreach (Column col in iGridHelper.GetColumns)
                        {
                            if (col.Name == "IS_CHOPHEP".ToString())
                            {
                                iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                            }
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
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
                TimeSpan so_ngay_cho_phep = (DateTime.ParseExact(dt_PHIEU_CTIET.Rows[vitri]["DEN_NGAY"].ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture)) - (DateTime.ParseExact(dt_PHIEU_CTIET.Rows[vitri]["TU_NGAY"].ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture));

                long pPHIEU_BT_CHOPHEPXUAT_CTIET_ID = ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[vitri]["PHIEU_BT_CHOPHEPXUAT_CTIET_ID"]);
                long pKHO_ID = ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[vitri]["KHO_ID"]);
                long pKHO_KHUVUC_ID = ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[vitri]["KHO_KHUVUC_ID"]);
                long pSANPHAM_ID = ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[vitri]["SANPHAM_ID"]);
                string pMA_ITEM_TYPE = dt_PHIEU_CTIET.Rows[vitri]["MA_ITEM_TYPE"].ToString();
                string pVITRI = dt_PHIEU_CTIET.Rows[vitri]["VITRI"].ToString();
                string pTU_NGAY = (DateTime.ParseExact(dt_PHIEU_CTIET.Rows[vitri]["TU_NGAY"].ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToString();
                string pDEN_NGAY = (DateTime.ParseExact(dt_PHIEU_CTIET.Rows[vitri]["DEN_NGAY"].ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToString();
                bool isValues = _KO_PHIEU_BT_CHOPHEPXUATBussiness.KO_PHIEU_BT_CHOPHEPXUAT_CTIET_CHECK_NGAY(pMA_DVIQLY, pPHIEU_BT_CHOPHEPXUAT_CTIET_ID, pKHO_ID, pKHO_KHUVUC_ID, pMA_ITEM_TYPE, pSANPHAM_ID, pVITRI, pTU_NGAY, pDEN_NGAY);
                if (so_ngay_cho_phep.Days < 0 || isValues == true)
                {
                    //var converter = new System.Windows.Media.BrushConverter();
                    //var brush = (Brush)converter.ConvertFromString("#F2F2F2");
                    //this.grdView.AddFormatCondition(new FormatCondition()
                    //{
                    //    FieldName = "IS_CHOPHEP",
                    //    Format = new Format() { Background = brush },
                    //    ApplyToRow = false
                    //});
                    if (dt_PHIEU_CTIET.Rows[vitri]["IS_CHOPHEP"].ToString() == "1")
                        dt_PHIEU_CTIET.Rows[vitri]["IS_CHOPHEP"] = 0;
                    foreach (Column col in iGridHelper.GetColumns)
                    {
                        if (col.Name == "IS_CHOPHEP".ToString())
                        {
                            iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                        }
                    }
                }
                else
                {
                    //var converter = new System.Windows.Media.BrushConverter();
                    //var white = (Brush)converter.ConvertFromString("#ffffff");
                    //this.grdView.AddFormatCondition(new FormatCondition()
                    //{
                    //    FieldName = "IS_CHOPHEP",
                    //    Format = new Format() { Background = white },
                    //    ApplyToRow = false
                    //});
                    foreach (Column col in iGridHelper.GetColumns)
                    {
                        if (col.Name == "IS_CHOPHEP".ToString())
                        {
                            iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
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
                if (txtNGAY_XACNHAN.Text.Trim() == "")
                {
                    base.ShowMessage(MessageType.Information, "[NGÀY XÁC NHẬN] phiếu " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    txtNGAY_XACNHAN.Focus();
                    return false;
                }
                if (_KO_PHIEU_BT_CHOPHEPXUATBussiness.KO_PHIEU_BT_CHOPHEPXUAT_CHECKEXISTS(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_BT_CHOPHEPXUAT_ID"]), txtSOPHIEU.Text.Trim()) == true)
                {
                    base.ShowMessage(MessageType.Information, "Số phiếu đã bị trùng, xin vui lòng nhập số khác ");
                    return false;
                }
                else
                {
                    if (dt_PHIEU_CTIET.Rows.Count == 0)
                    {
                        base.ShowMessage(MessageType.Information, "[CHI TIẾT PHIẾU XUẤT HÀNG BIỆT TRỮ] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
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
                dsReturn = _KO_PHIEU_BT_CHOPHEPXUATBussiness.InsertorUpdateKO_PHIEU_BT_CHOPHEPXUAT(iDataSource, dt_PHIEU_CTIET, CommonDataHelper.UserName);
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}