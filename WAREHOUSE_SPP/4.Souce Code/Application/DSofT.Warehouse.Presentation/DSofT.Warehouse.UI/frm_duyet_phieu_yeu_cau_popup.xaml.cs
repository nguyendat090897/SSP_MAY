﻿using DSofT.Warehouse.Business;
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
using System.Globalization;

namespace DSofT.Warehouse.UI
{//
    /// <summary>
    /// Interaction logic for frm_duyet_phieu_yeu_cau_popup.xaml
    /// </summary>
    public partial class frm_duyet_phieu_yeu_cau_popup : PopupBase
    {

        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper2 = null;
        DataRow iRowSelGb = null;
        DataTable dt_KHO = null;
        DataTable dt_KHO_KHUVUC = null;
        Kho_DuyetPhieuYeuCau_DVBussiness _Kho_DuyetYeuCau_DV;
        DataTable dt_PHIEU_CTIET = null;
        DataTable dt_PHIEU_CTIET_NVL = null;
        public frm_duyet_phieu_yeu_cau_popup()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            dt_PHIEU_CTIET = this.TableSchemaDefineBingding_Grid();
            dt_PHIEU_CTIET.Clear();
            dt_PHIEU_CTIET_NVL = this.TableSchemaDefineBingding_Grid_NVL();
            dt_PHIEU_CTIET_NVL.Clear();
            _Kho_DuyetYeuCau_DV = new Kho_DuyetPhieuYeuCau_DVBussiness(string.Empty);
            BuildCombobox();
            Initialize_Grid();
            Initialize_Grid_2();
            this.DataContext = this.iDataSource;
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
            if (iDataSource != null && iDataSource.Rows.Count > 0)
            {
                if (iDataSource.Rows[0]["TRANGTHAI_DUYET"].ToString() == "1")
                {
                    rdbD.IsChecked = true;
                }
                else if (iDataSource.Rows[0]["TRANGTHAI_DUYET"].ToString() == "0")
                {
                    rdbK.IsChecked = true;
                }
                else
                {
                    rdbD.IsChecked = false;
                    rdbK.IsChecked = false;
                }
                if (iDataSource.Rows[0]["NGUOI_DUYET"].ToString() == string.Empty)
                {
                    iDataSource.Rows[0]["NGUOI_DUYET"] = CommonDataHelper.UserName;
                }
                if (iDataSource.Rows[0]["NGAY_DUYET"].ToString() == string.Empty)
                {
                    iDataSource.Rows[0]["NGAY_DUYET"] = DateTime.Today;
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

        /// <summary>
        /// Initialize_Grid
        /// </summary>
        /// 
        private void BuildCombobox()
        {
            try
            {


                dt_KHO = _Kho_DuyetYeuCau_DV.GetListDM_KHO(ConstCommon.DonViQuanLy);
                dt_KHO_KHUVUC = _Kho_DuyetYeuCau_DV.DM_KHO_KHUVUC_GET_LIST_BY_KHO(ConstCommon.DonViQuanLy, 0);
                DataTable dtLoaiDV = _Kho_DuyetYeuCau_DV.GetData_Loai_DV(ConstCommon.DonViQuanLy);
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
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
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
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
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
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblHanDung"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoDK"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GIAYPHEP_NK", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGPNK"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                this.iGridHelper.Initialize();
                //this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
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
                xColumn.Width = 150;
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

                this.iGridHelper2.Initialize();
                //this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid_2()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rdbD_Checked(object sender, RoutedEventArgs e)
        {
            txtLyDo.IsEnabled = false;
        }

        private void rdbK_Checked(object sender, RoutedEventArgs e)
        {
            txtLyDo.IsEnabled = true;
        }

        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ConstCommon.mousebusy(this);
                if (rdbD.IsChecked == true)
                {
                    iDataSource.Rows[0]["TRANGTHAI_DUYET"] = 1;
                }
                else if (rdbK.IsChecked == true)
                {
                    iDataSource.Rows[0]["TRANGTHAI_DUYET"] = 0;
                    if(txtLyDo.Text=="")
                    {
                        base.ShowMessage(MessageType.Information, "Vui lòng nhập lý do từ chối duyệt!");
                        return;
                    }
                }
                else
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng chọn trạng thái duyệt!");
                    return;
                }
                
                int Result = _Kho_DuyetYeuCau_DV.Update_Kho_Lam_Phieu_DV(CommonDataHelper.DonViQuanLy, iDataSource, CommonDataHelper.UserName);
                if(Result>0)
                { 

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

        private void dtpNgay_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                dtpNgay.EditValue = DateTime.ParseExact(dtpNgay.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
        }

        private void cboLoaiDichVu_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            lblSPCanBoSungTTTV.Text = "Bổ sung thông tin TV cho các sản phẩm thuộc " + cboLoaiDichVu.Text;
        }
    }

}
