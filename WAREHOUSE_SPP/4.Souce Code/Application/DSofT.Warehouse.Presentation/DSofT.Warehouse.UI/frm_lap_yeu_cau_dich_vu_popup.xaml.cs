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
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using System.Globalization;
using System.Drawing;
using System.Windows.Media;
using System.ComponentModel;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_lap_yeu_cau_dich_vu_popup.xaml
    /// </summary>
    /// //
    public partial class frm_lap_yeu_cau_dich_vu_popup : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper2 = null;
        DataRow iRowSelGb = null;
        DataTable dt_PHIEU_CTIET = null;
        DataTable dt_PHIEU_CTIET_NVL = null;
        DataTable dt_KHO = null;
        DataTable dt_KHO_KHUVUC = null;
        Kho_Phieu_YeuCau_DVBussiness _Kho_YeuCau_DV;
        int so_tong, so_thung, so_le = 0;
        public frm_lap_yeu_cau_dich_vu_popup()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            dt_PHIEU_CTIET = this.TableSchemaDefineBingding_Grid();
            dt_PHIEU_CTIET.Clear();
            dt_PHIEU_CTIET_NVL = this.TableSchemaDefineBingding_Grid_NVL();
            dt_PHIEU_CTIET_NVL.Clear();
            _Kho_YeuCau_DV = new Kho_Phieu_YeuCau_DVBussiness(string.Empty);
            BuildCombobox();
            Initialize_Grid();
            Initialize_Grid_2();
            this.iDataSource.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
            this.DataContext = this.iDataSource;
           
           
        }
        public override void OnPopupClosing(object sender, CancelEventArgs e)
        {
            if (iDataSource != null && iDataSource.Rows.Count > 0 && dt_PHIEU_CTIET.Rows.Count <= 0)
            {
                base.ShowMessage(MessageType.Information, "Phiếu chưa có sản phẩm nào. Không thể thoát lúc này!");

                e.Cancel = true;
            }
        }

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                cboLoaiDichVu.IsEnabled = false;
                iRowSelGb = this.Parameter[0] as DataRow;
                DispalayRequest();
                //isFirstLoad = true;

                if (this.Parameter.Count() > 1)
                {
                    //----------get data for Grid_CTiet----------
                    DataRow[] dr_chitiet = this.Parameter[1] as DataRow[];

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
                    //----------end load data Grid_CTiet------------

                    //----------get data for Grid_CTiet----------
                    DataRow[] dr_chitiet_NVL = this.Parameter[2] as DataRow[];

                    dt_PHIEU_CTIET_NVL.Clear();
                    if (dr_chitiet_NVL.Length > 0)
                    {
                        dt_PHIEU_CTIET_NVL = dr_chitiet_NVL.CopyToDataTable();
                    }
                    if (dt_PHIEU_CTIET_NVL != null)
                    {
                        this.iGridHelper2.BindItemSource(dt_PHIEU_CTIET_NVL);
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }
                    //----------end load data Grid_CTiet------------

                }

            }
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
        
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("PHIEUYEUCAU_DV_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("SOPHIEU", typeof(string));
                xDicUser.Add("NGAYLAP", typeof(string));
                xDicUser.Add("THOIGIAN_HOANTAT", typeof(string));
                xDicUser.Add("MA_LOAI_DICHVU", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TRANGTHAI_DUYET", typeof(string));
                xDicUser.Add("NGAY_DUYET", typeof(string));
                xDicUser.Add("NGUOI_DUYET", typeof(string));
                xDicUser.Add("LYDO", typeof(string));
                xDicUser.Add("DIEUKIEN_THUCHIEN", typeof(string));
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
                xDicUser.Add("PHIEUYEUCAU_DV_CTIET_ID", typeof(decimal));
                xDicUser.Add("PHIEUYEUCAU_DV_ID", typeof(decimal));
                xDicUser.Add("KHO_ID", typeof(decimal));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));

                xDicUser.Add("MA_SANPHAM_KH", typeof(string));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));

                xDicUser.Add("SOLO", typeof(string));
                xDicUser.Add("HANDUNG", typeof(string));
                xDicUser.Add("SO_LUONG_TONG", typeof(int));
                xDicUser.Add("SO_LUONG_THUNG", typeof(int));
                xDicUser.Add("SO_LUONG_LE", typeof(int));
                xDicUser.Add("SODK", typeof(string));
                xDicUser.Add("GIAYPHEP_NK", typeof(string));
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
        private DataTable TableSchemaDefineBingding_Grid_NVL()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("PHIEUYEUCAU_DV_CTIET_NVL_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEUYEUCAU_DV_ID", typeof(decimal));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("SO_LUONG", typeof(int));
                xDicUser.Add("DONGIA", typeof(decimal));
                xDicUser.Add("THANHTIEN", typeof(decimal));
                xDicUser.Add("IS_SPKM", typeof(bool));
                xDicUser.Add("RowVersion", typeof(long));
                xDicUser.Add("IsDelete", typeof(bool));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }
        private void BuildCombobox()
        {
            try
            {


                dt_KHO = _Kho_YeuCau_DV.GetListDM_KHO(ConstCommon.DonViQuanLy);
                dt_KHO_KHUVUC = _Kho_YeuCau_DV.DM_KHO_KHUVUC_GET_LIST_BY_KHO(ConstCommon.DonViQuanLy, 0);
                DataTable dtLoaiDV = _Kho_YeuCau_DV.GetData_Loai_DV(ConstCommon.DonViQuanLy);
                if (dtLoaiDV != null && dtLoaiDV.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboLoaiDichVu, "TEN_LOAI_DICHVU", "MA_LOAI_DICHVU", dtLoaiDV, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboLoaiDichVu, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["MA_LOAI_DICHVU"] = cboLoaiDichVu.GetKeyValue(0);
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "BuildCombobox()");
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

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                string[] header = new string[] { "Kho" };
                string[] id = new string[] { "TEN_KHO" };
                string[] align = new string[] { "left" };

                xColumn = new Column("KHO_ID", "Kho");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Combo;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("KHO_ID") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_KHO;
                xColumn.ValueList.DisplayMember = "TEN_KHO";
                xColumn.ValueList.ValueMember = "KHO_ID";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_KHO, header, id, align);
                this.iGridHelper.Add(xColumn);



                string[] header_kv = new string[] { "Khu vực" };
                string[] id_kv = new string[] { "TEN_KHO_KHUVUC" };
                string[] align_kv = new string[] { "left" };

                xColumn = new Column("KHO_KHUVUC_ID", "Khu vực");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Combo;
                xColumn.Binding = new System.Windows.Data.Binding("KHO_KHUVUC_ID") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_KHO_KHUVUC;
                xColumn.ValueList.DisplayMember = "TEN_KHO_KHUVUC";
                xColumn.ValueList.ValueMember = "KHO_KHUVUC_ID";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_KHO_KHUVUC, header_kv, id_kv, align_kv);
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_ITEM_TYPE", "Item type");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaSanPham"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_SANPHAM_KH", "Mã sản phẩm (KH)");
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenSanPham"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDVT"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);
                
                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblHanDung"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoDK"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GIAYPHEP_NK", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGPNK"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
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

                var converter = new System.Windows.Media.BrushConverter();
                var brush = (System.Windows.Media.Brush)converter.ConvertFromString("#F2F2F2");

                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    //FieldName = "Giảng viên đã kiểm tra",
                    FieldName = "SO_LUONG_THUNG",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    //FieldName = "Giảng viên đã kiểm tra",
                    FieldName = "SO_LUONG_LE",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.iGridHelper.Initialize();
                //this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        void xButton_RemoveClick(object sender, RoutedEventArgs e)
        {

            try
            {
                bool msg = false;
                if(grdView.FocusedRowHandle<0)
                {
                    return;
                }
                if (dt_PHIEU_CTIET != null && dt_PHIEU_CTIET.Rows.Count>0 )
                {
                    DataRowView row = this.grd.GetFocusedRow() as DataRowView;
                    if(this.dt_PHIEU_CTIET.Rows[(grd.View as TableView).FocusedRowHandle]["PHIEUYEUCAU_DV_CTIET_ID"].ToString()==string.Empty)
                    {
                        dt_PHIEU_CTIET.Rows.Remove(row.Row);
                        if (dt_PHIEU_CTIET.Rows.Count <= 0)
                        {
                            return;
                        }
                        else
                        {
                            grdView.FocusedRowHandle = 0;
                        }
                        this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                        return;
                    }
                    if (ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEUYEUCAU_DV_CTIET_ID"]) > 0)
                    {
                        msg = (bool)base.ShowMessage(MessageType.OkCancel, "Bạn có muốn xóa dòng đang chọn trong phiếu không");
                        if (msg == true)
                        {
                            int Return = _Kho_YeuCau_DV.DeleteKho_Lam_Phieu_DV_CT(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_NT_NEW(row.Row["PHIEUYEUCAU_DV_CTIET_ID"]), CommonDataHelper.UserName);
                            if (Return<=0)
                            {
                                return;
                            }
                            else
                            {
                                dt_PHIEU_CTIET.Rows.Remove(row.Row);
                                if (dt_PHIEU_CTIET.Rows.Count <= 0)
                                {
                                    _Kho_YeuCau_DV.KO_PHIEUYEUCAU_DV_CTIET_NVL_DELETE_NEW(CommonDataHelper.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), CommonDataHelper.UserName);
                                    dt_PHIEU_CTIET_NVL.Clear();
                                    this.iGridHelper2.BindItemSource(dt_PHIEU_CTIET_NVL);
                                    return;
                                }
                                else
                                {
                                    grdView.FocusedRowHandle = 0;
                                }
                                this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                                DataSet ds = null;
                                ds=_Kho_YeuCau_DV.InsertorUpdateKho_Lam_Phieu_DV_And_CTiet(CommonDataHelper.DonViQuanLy, iDataSource, dt_PHIEU_CTIET, CommonDataHelper.UserName);
                                
                                if (ds!=null && ds.Tables.Count==3)
                                {
                                    DataTable dt_PHIEU_CTIET_NVL = ds.Tables[2].Copy();
                                    if (dt_PHIEU_CTIET_NVL != null)
                                    {
                                        this.iGridHelper2.BindItemSource(dt_PHIEU_CTIET_NVL);
                                    }
                                    else
                                    {
                                        grd_2.ItemsSource = null;
                                    }
                                }
                                
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);

            }
        }
        private void Initialize_Grid_2()
        {
            iGridHelper2 = null;
            try
            {
                this.iGridHelper2 = new GridHelper(this.grd_2, this.grdView_2, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper2.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", "Mã NVL ");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper2.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", "Tên NVL ");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper2.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_sanpham_dichvu_DonViTinh"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper2.Add(xColumn);

                xColumn = new Column("SO_LUONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_vattu_dichvu_SoLuong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper2.Add(xColumn);

                xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaNhap"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper2.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThanhTien"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper2.Add(xColumn);

                xColumn = new Column("IS_SPKM", "Là SPKM");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper2.Add(xColumn);

               

                this.iGridHelper2.Initialize();
                //this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid_2()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
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
        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        int CheckNull()
        {
            try
            {
                if (txtSoPhieuPYC.Text == "")
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng nhập số phiếu!");
                    txtSoPhieuPYC.Focus();
                    return 1;
                }
                if (dtpThoiGianHoanTat.EditValue.ToString() == "")
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng chọn thời gian hoàn tất!");
                    dtpThoiGianHoanTat.Focus();
                    return 1;
                }
                if (cboLoaiDichVu.Text == "--Chọn--")
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng chọn loại dịch vụ!");
                    cboLoaiDichVu.Focus();
                    return 1;
                }
                if (dt_PHIEU_CTIET == null || dt_PHIEU_CTIET.Rows.Count == 0)
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng chọn sản phẩm trước khi lập phiếu dịch vụ!");
                    return 1;

                }
                else
                {
                    for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                    {
                        if (dt_PHIEU_CTIET.Rows[i]["SOLO"].ToString() == "")
                        {
                            base.ShowMessage(MessageType.Information, "Vui lòng nhập số lô cho sản phẩm [" + dt_PHIEU_CTIET.Rows[i]["TEN_SANPHAM"] + "]!");
                            return 1;
                        }
                        if (dt_PHIEU_CTIET.Rows[i]["HANDUNG"].ToString() == "")
                        {
                            base.ShowMessage(MessageType.Information, "Vui lòng nhập hạn dùng cho sản phẩm [" + dt_PHIEU_CTIET.Rows[i]["TEN_SANPHAM"] + "]!");
                            return 1;
                        }
                        if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG"]) == 0)
                        {
                            base.ShowMessage(MessageType.Information, "Vui lòng nhập số lượng tổng cho sản phẩm [" + dt_PHIEU_CTIET.Rows[i]["TEN_SANPHAM"] + "]!");
                            return 1;
                        }
                        
                    }
                    if (_Kho_YeuCau_DV.CheckExist(CommonDataHelper.DonViQuanLy, ConstCommon.NVL_NUM_NT_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"].ToString()), txtSoPhieuPYC.Text.Trim()) == 1)
                    {
                        base.ShowMessage(MessageType.Information, "Số phiếu [" + txtSoPhieuPYC.Text + "] đã tồn tại!");
                        txtSoPhieuPYC.Focus();
                        return 1;
                    }
                }

                return 0;
            }catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.lblChonSanPham.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
                return 1 ;
            }
        }
        
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if(iDataSource!=null && iDataSource.Rows.Count>0 && dt_PHIEU_CTIET.Rows.Count<=0)
            {
                base.ShowMessage(MessageType.Information, "Phiếu chưa có sản phẩm nào. Không thể thoát lúc này!");
                return;
            }
            this.Close();
        }

        private void lblChonSanPham_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = null;
                if (cboLoaiDichVu!=null)
                {
                    if (cboLoaiDichVu.EditValue.ToString() == "TEMTOA")
                    {
                        xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP_Kho", null);
                    }
                    else if (cboLoaiDichVu.EditValue.ToString() == "BOKIT")
                    {
                        xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP", null);
                    }
                }

                //begin tao phieu chi tiet
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
                                dr["KHO_ID"] = ConstCommon.pKHO_ID;
                                dr["KHO_KHUVUC_ID"] = "0";
                                dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["MA_SANPHAM_KH"] = dt_SPCHON.Rows[i]["MA_SANPHAM_KH"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
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


                //end tao phieu chi tiet




            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.lblChonSanPham.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                if (CheckNull() == 1)
                {
                    return;
                }
                ConstCommon.mousebusy(this);
                //----------------Kiem tra san pham chua co dinh muc-----------------------
                DataSet ds = _Kho_YeuCau_DV.Get_DinhMuc_SP_All(CommonDataHelper.DonViQuanLy);
                if (ds != null)
                {
                    DataRow dt2 = null;
                    if (dt_PHIEU_CTIET != null && dt_PHIEU_CTIET.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                        {
                            dt2 = ds.Tables[0].Select("SANPHAM_ID=" + ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["SANPHAM_ID"]).ToString()).FirstOrDefault();
                            if (dt2==null)
                            {
                                base.ShowMessage(MessageType.Information, "Sản phẩm ở dòng [" + (i+1).ToString() + "] chưa được khai báo định mức!");
                                return;
                            }

                        }
                    }
                }

                //-------------
                DataSet dsReturn = null;
                dsReturn = _Kho_YeuCau_DV.InsertorUpdateKho_Lam_Phieu_DV_And_CTiet(CommonDataHelper.DonViQuanLy, iDataSource, dt_PHIEU_CTIET, CommonDataHelper.UserName);
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
                        this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }

                    DataTable dt_PHIEU_CTIET_NVL = dsReturn.Tables[2].Copy();
                    if (dt_PHIEU_CTIET_NVL != null)
                    {
                        this.iGridHelper2.BindItemSource(dt_PHIEU_CTIET_NVL);
                    }
                    else
                    {
                        grd_2.ItemsSource = null;
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
            finally
            {
                ConstCommon.mousefree(this);
            }
        }

        private void dtpThoiGianHoanTat_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            try
            {
                dtpThoiGianHoanTat.EditValue = DateTime.ParseExact(dtpThoiGianHoanTat.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
        }

        private void cboLoaiDichVu_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (cboLoaiDichVu.Text == "--Chọn--")
            {
                lblSPCanBoSungTTTV.Text = "Các sản phẩm cần bổ sung thông tin TV";
            }
            else
            {
                lblSPCanBoSungTTTV.Text = "Bổ sung thông tin TV cho các sản phẩm thuộc " + cboLoaiDichVu.Text;
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

                //begin hoapd them vao
                if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[vitri]["SOLUONG_QUYDOI"].ToString()) == 0)
                {
                    //neu la dao tao

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

                    //end hoapd them vao
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_FocusedRowChanged()");
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_CellValueChanged()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }

}