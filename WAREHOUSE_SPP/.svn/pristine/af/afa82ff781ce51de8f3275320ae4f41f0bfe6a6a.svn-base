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
using DSofT.Framework.UICore.TaskProxy;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 09/03/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Phieu xuat kho vat tu dich vu
    /// </summary>
    public partial class frm_phieu_xuat_kho_vat_tu_dich_vu_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataTable iGridDataSource = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper1 = null;
        DataRow iRowSelGb = null;
        DataRow iRowSelGb1 = null;
        DataTable dt_TRANGTHAI;
        DataTable dt_Kho;
        DataTable dt_Kho_Khuvuc;
        DataTable dt_VITRIHANG = null;
        DataTable iDataSource1 = null;
        DataTable dtSL = null;
        DataTable dtSLSPChon = null;
        DataTable dtSLSPVTTL = null;
        int sl = 0;
        int slspvttl = 0;
        int slspChon = 0;
        int slNhap = 0;
        int slTong = 0;
        //public static long pPHIEU_XUATNHAP_KHO_ID = 0;
        string tenSP = null;
        IPHIEU_NHAPXUATKHO_DICHVUBussiness PHIEU_NHAPXUATKHO_DICHVUBussiness;
        public frm_phieu_xuat_kho_vat_tu_dich_vu_popup()
        {
            InitializeComponent();
            PHIEU_NHAPXUATKHO_DICHVUBussiness = new PHIEU_NHAPXUATKHO_DICHVUBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.iGridDataSource = this.TableSchemaDefineBingding_SP_NVL();
            this.iDataSource1 = this.TableSchemaDefineBingding1();
            iGridDataSource.Clear();
            BuildCombobox();
            Initialize_Grid();
            Initialize_Grid1();
            this.iDataSource.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
            this.DataContext = this.iDataSource;

        }

        private DataTable TableSchemaDefineBingding1()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("PHIEU_NHAPXUATKHO_ID", typeof(decimal));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGb = this.Parameter[0] as DataRow;
                this.iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"] = iRowSelGb["PHIEUYEUCAU_DV_ID"];
                //DispalayRequest();
                LoadData();
                txtPYCDV.Text = frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.SOPHIEUDV;
            }
        }

        //public void DispalayRequest()
        //{
        //    try
        //    {
        //        foreach (DataColumn item in this.iRowSelGb.Table.Columns)
        //        {
        //            if (this.iDataSource.Columns[item.ColumnName] != null)
        //            {
        //                this.iDataSource.Rows[0][item.ColumnName] = iRowSelGb[item.ColumnName];
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "DispalayRequest()");
        //        base.ShowMessage(MessageType.Error, ex.Message, ex);
        //    }
        //    finally
        //    {
        //        Mouse.OverrideCursor = Cursors.Arrow;
        //    }
        //}

        

        /// <summary>
        /// loadData
        /// </summary>

        private void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_LIST_ALL(ConstCommon.DonViQuanLy,ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]));
                    });
                    Action<DataTable> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iDataSource1 = dt;
                            this.iGridHelper1.BindItemSource(this.iDataSource1);
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


        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEU_NHAPXUATKHO_ID", typeof(decimal));
                xDicUser.Add("X_OR_N", typeof(string));
                xDicUser.Add("PHIEUYEUCAU_DV_ID", typeof(decimal));

                xDicUser.Add("MA_HINHTHU_NHAPXUAT", typeof(string));
                xDicUser.Add("SOPHIEU", typeof(string));
                xDicUser.Add("NGAYLAP", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));

                xDicUser.Add("IsDelete", typeof(bool));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }
        private DataTable TableSchemaDefineBingding_SP_NVL()
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
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string)); 
                xDicUser.Add("MA_DONVI_TINH", typeof(string));
                xDicUser.Add("SO_LUONG_TONG", typeof(int));
                xDicUser.Add("MA_TINHTRANG_HANG", typeof(string));
                xDicUser.Add("VITRI", typeof(string));
                xDicUser.Add("MA_VACH", typeof(string));

                xDicUser.Add("IsDelete", typeof(bool));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));
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
        /// Initialize_Grid VAT TU
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

                xColumn = new Column("MA_ITEM_TYPE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblItemType"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaVatTu"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenVatTu"]));
                xColumn.Width = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xacnhan_hanghuhong_DonViTinh"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuong"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                this.iGridHelper.Add(xColumn);


                string[] header = new string[] { "Kho" };
                string[] id = new string[] { "TEN_KHO" };
                string[] align = new string[] { "left" };

                xColumn = new Column("KHO_ID", "Kho");
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Combo;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("KHO_ID") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_Kho;
                xColumn.ValueList.DisplayMember = "TEN_KHO";
                xColumn.ValueList.ValueMember = "KHO_ID";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_Kho, header, id, align);
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
                xColumn.ValueList.DataSource = dt_Kho_Khuvuc;
                xColumn.ValueList.DisplayMember = "TEN_KHO_KHUVUC";
                xColumn.ValueList.ValueMember = "KHO_KHUVUC_ID";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_Kho_Khuvuc, header_kv, id_kv, align_kv);
                this.iGridHelper.Add(xColumn);


                string[] header_vth = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]) };
                string[] id_vth = new string[] { "VITRI" };
                string[] align_vth = new string[] { "left" };
                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                xColumn.Width = 200;
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
                this.iGridHelper.Add(xColumn); ;

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"]));
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
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("MA_TINHTRANG_HANG") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_TRANGTHAI;
                xColumn.ValueList.DisplayMember = "TEN_TINHTRANG_HANG";
                xColumn.ValueList.ValueMember = "MA_TINHTRANG_HANG";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_TRANGTHAI, header_tt, id_tt, align_tt);
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

                this.iGridHelper.Initialize();
                //this.grdViewDMSP.AutoWidth = false;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        /// <summary>
        /// Initialize_Grid1 PHIEU XUAT
        /// </summary>
        private void Initialize_Grid1()
        {
            try
            {
                this.iGridHelper1 = new GridHelper(this.grd1, this.grdView1, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper1.Add(xColumn);

                xColumn = new Column("SOPHIEU", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xacnhan_hanghuhong_SoPhieu"]));
                xColumn.Width = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper1.Add(xColumn);


                xColumn = new Column("TEN_HINHTHU_NHAPXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lbHTNX"]));
                xColumn.Width = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper1.Add(xColumn);

                xColumn = new Column("NGAYLAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lbNgayXuat"]));
                xColumn.Width = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper1.Add(xColumn);

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xacnhan_hanghuhong_GhiChu"]));
                xColumn.Width = 300;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper1.Add(xColumn);

                ButtonEditSettings xButton_Remove = new ButtonEditSettings();
                xButton_Remove.DefaultButtonClick += xButton_RemoveClick1;
                xColumn = new Column("REMOVE", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_Remove"]));
                xColumn.Width = 50;
                xColumn.CustomEditor = xButton_Remove;
                xColumn.Visible = true;
                xColumn.ColumnType = ColumnControl.Custom;
                xColumn.MaxLenth = 5;
                xColumn.HorizontalContentAlignment = EditSettingsHorizontalAlignment.Center;
                this.iGridHelper1.Add(xColumn);

                this.iGridHelper1.Initialize();
                ConstCommon.setAutoFilterConditionGrid(grd1);
                this.grdView1.AutoWidth = false;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid1()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        void xButton_RemoveClick(object sender, RoutedEventArgs e)
        {

            try
            {
                bool msg = false;
                if ((this.iGridDataSource == null) || (this.iGridDataSource.Rows.Count == 0))
                {
                    return;
                }

                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;

                if (ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_NHAPXUATKHO_CTIET_ID"]) > 0)
                {
                    msg = (bool)base.ShowMessage(MessageType.OkCancel, "BẠN CÓ MUỐN XÓA DÒNG ĐANG CHỌN KHÔNG???");
                    if (msg == true)
                    {
                        DataTable dsReturn = PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_CTIET_DELETE(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_NHAPXUATKHO_CTIET_ID"]), CommonDataHelper.UserName);
                        if ((dsReturn == null) || dsReturn.Rows.Count == 0 || dsReturn.Rows[0][0].ToString() == "0")
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                iGridDataSource.Rows.Remove(row.Row);


                this.iGridHelper.BindItemSource(this.iGridDataSource);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        void xButton_RemoveClick1(object sender, RoutedEventArgs e)
        {

            try
            {
                bool msg = false;
                if ((this.iDataSource == null) || (this.iDataSource.Rows.Count == 0))
                {
                    return;
                }

                if (this.grd1.GetFocusedRow() == null) return;
                DataRowView row = this.grd1.GetFocusedRow() as DataRowView;

                if (ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_NHAPXUATKHO_ID"]) > 0)
                {
                    msg = (bool)base.ShowMessage(MessageType.OkCancel, "BẠN CÓ MUỐN XÓA DÒNG ĐANG CHỌN KHÔNG???");
                    if (msg == true)
                    {
                        DataTable dsReturn = PHIEU_NHAPXUATKHO_DICHVUBussiness.DeleteKO_PHIEU_NHAPXUATKHO_BY_ID(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_NHAPXUATKHO_ID"]), CommonDataHelper.UserName);
                        if ((dsReturn == null) || dsReturn.Rows.Count == 0 || dsReturn.Rows[0][0].ToString() == "0")
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                iDataSource1.Rows.Remove(row.Row);


                this.iGridHelper1.BindItemSource(this.iDataSource1);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick1()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BuildCombobox()
        {
            try
            {
                dt_TRANGTHAI = PHIEU_NHAPXUATKHO_DICHVUBussiness.GetData_For_gird_TINHTRANG_HANG(ConstCommon.DonViQuanLy);
                dt_Kho = PHIEU_NHAPXUATKHO_DICHVUBussiness.GetListDM_KHO(ConstCommon.DonViQuanLy);
                dt_Kho_Khuvuc = PHIEU_NHAPXUATKHO_DICHVUBussiness.GetData_For_gird_TENKHO_KHUVUC(ConstCommon.DonViQuanLy);
                dt_VITRIHANG = PHIEU_NHAPXUATKHO_DICHVUBussiness.GetData_For_gird_VITRI_HANG(ConstCommon.DonViQuanLy, ConstCommon.pKHO_ID);

                DataTable dtHinhhthucnhap = PHIEU_NHAPXUATKHO_DICHVUBussiness.getDM_HINHTHU_NHAPXUAT("X");

                if (dtHinhhthucnhap != null && dtHinhhthucnhap.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cbohinhthucxuat, "TEN_HINHTHU_NHAPXUAT", "MA_HINHTHU_NHAPXUAT", dtHinhhthucnhap, SelectionTypeEnum.Native);
                    this.iDataSource.Rows[0]["MA_HINHTHU_NHAPXUAT"] = cbohinhthucxuat.GetKeyValue(0);
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "BuildCombobox()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void dtpNgayXuat_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            try
            {
                dtpNgayXuat.EditValue = DateTime.ParseExact(dtpNgayXuat.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
               
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
        /// Chon san pham de lap phieu xuat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChonSP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP_Kho", null);

                //begin tao phieu chi tiet
                DataTable dt_SPCHON = (DataTable)xReturn;

                if ((dt_SPCHON != null) && (dt_SPCHON.Rows.Count > 0))
                {
                    if (dt_SPCHON.Rows[0]["GHI_CHU"].ToString() == "Y")
                    {

                        for (int i = 0; i < dt_SPCHON.Rows.Count; i++)
                        {
                            if (ContainDataRowInDataTable(iGridDataSource, dt_SPCHON.Rows[i]) == false)
                            {
                                DataRow dr = iGridDataSource.NewRow();
                                dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr["PHIEU_NHAPXUATKHO_CTIET_ID"] = "0";
                                dr["KHO_ID"] = ConstCommon.pKHO_ID;
                                dr["KHO_KHUVUC_ID"] = "0";
                                dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                dr["MA_DONVI_TINH"] = dt_SPCHON.Rows[i]["MA_DONVI_TINH"];

                                for(int j = 0; j < frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows.Count;j++)
                                {
                                    if (ConstCommon.NVL_NUM_LONG_NEW(dt_SPCHON.Rows[i]["SANPHAM_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SANPHAM_ID"]))
                                    {
                                        dtSL = PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(dt_SPCHON.Rows[i]["SANPHAM_ID"]));
                                        dtSLSPVTTL = PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_VTTL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(dt_SPCHON.Rows[i]["SANPHAM_ID"])); ;
                                        if (dtSL.Rows.Count == 0)
                                        {
                                            sl = 0;
                                        }
                                        else
                                        {
                                            sl = ConstCommon.NVL_NUM_NT_NEW(dtSL.Rows[0][0].ToString());
                                        }

                                        if (dtSLSPVTTL.Rows.Count == 0)
                                        {
                                            slspvttl = 0;
                                        }
                                        else
                                        {
                                            slspvttl = ConstCommon.NVL_NUM_NT_NEW(dtSLSPVTTL.Rows[0][0].ToString());
                                        }
                                        if (PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_CHECKEXISTS_DICHVU_NHAPVTTL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(dt_SPCHON.Rows[i]["SANPHAM_ID"])) == true)
                                        {
                                            if (sl < (ConstCommon.NVL_NUM_NT_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SO_LUONG"]) + slspvttl))
                                            {
                                                iGridDataSource.Rows.Add(dr);
                                            }
                                        }
                                        else
                                        {
                                            if (sl < ConstCommon.NVL_NUM_NT_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SO_LUONG"]))
                                            {
                                                iGridDataSource.Rows.Add(dr);
                                            }
                                        }
                                            
                                       
                                        
                                    }
                                }
                               
                            }
                        }
                    }

                }


                if(iGridDataSource != null)
                {
                    this.iGridHelper.BindItemSource(iGridDataSource);
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


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (checkNull() == false)
                {
                    return;
                }

                
                if(KiemTraSoLuongAll() == false)
                {
                    return;
                }

                
                

                DataSet dsReturn = null;
                dsReturn = PHIEU_NHAPXUATKHO_DICHVUBussiness.InsertKO_PHIEU_NHAPXUATKHO(iDataSource, iGridDataSource, CommonDataHelper.UserName, "X");
                if ((dsReturn != null) && (dsReturn.Tables.Count == 2))
                {

                    foreach (DataColumn item in dsReturn.Tables[0].Columns)
                    {
                        if (this.iDataSource.Columns[item.ColumnName] != null)
                        {
                            this.iDataSource.Rows[0][item.ColumnName] = dsReturn.Tables[0].Rows[0][item.ColumnName];
                        }
                    }

                    if (iGridDataSource != null)
                    {
                        this.iGridHelper.BindItemSource(iGridDataSource);
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }
;
                    if (iDataSource != null)
                    {
                        this.iGridHelper1.BindItemSource(iDataSource);
                    }
                    else
                    {
                        grd1.ItemsSource = null;
                    }

                    ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                    LoadData();
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

        private void grdView1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (iDataSource.Rows.Count == 0) return;
                if (this.grd1.GetFocusedRow() == null) return;
                iRowSelGb1 = ((DataRowView)this.grd1.GetFocusedRow()).Row;
                this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"] = iRowSelGb1["PHIEU_NHAPXUATKHO_ID"];
                iGridDataSource = PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_DICHVU_CTIET_GET_BY_ID(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]));
                this.iGridHelper.BindItemSource(this.iGridDataSource);

               if(iRowSelGb1 != null)
                {
                    foreach (DataColumn item in iRowSelGb1.Table.Columns)
                    {
                        if (this.iDataSource.Columns[item.ColumnName] != null)
                        {
                            this.iDataSource.Rows[0][item.ColumnName] = iRowSelGb1[item.ColumnName];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_MouseDown()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = true;
            try
            {
                LoadData();
                this.iDataSource = this.TableSchemaDefineBingding();
                this.DataContext = this.iDataSource;
                BuildCombobox();
                this.iDataSource.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                this.iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"] = iRowSelGb["PHIEUYEUCAU_DV_ID"];
                if (iGridDataSource != null)
                {
                    iGridDataSource.Clear();
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private bool checkNull()
        {
            if(txtSoPhieu.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "VUI LÒNG NHẬP THÔNG TIN SỐ PHIẾU");
                txtSoPhieu.Focus();
                return false;
            }

            if (ConstCommon.check_String_Unicode(txtSoPhieu.Text) == false)
            {
                base.ShowMessage(MessageType.Information, "VUI LÒNG KHÔNG NHẬP KÝ TỰ ĐẶC BIỆT, KHOẢNG TRỐNG HOẶC CÓ DẤU");
                txtSoPhieu.Focus();
                return false;
            }

            if (PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEUYEUCAU_NHAPXUATKHO_CHECKEXISTS(ConstCommon.DonViQuanLy,ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]),txtSoPhieu.Text.Trim()) == true)
            {
                base.ShowMessage(MessageType.Information, "SỐ PHIẾU BỊ TRÙNG. VUI LÒNG NHẬP SỐ PHIẾU KHÁC");
                txtSoPhieu.Focus();
                return false;
            }

            if (dtpNgayXuat.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN NGÀY XUẤT");
                dtpNgayXuat.Focus();
                return false;
            }
            
            if(iGridDataSource.Rows.Count == 0)
            {
                base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN SẢN PHẨM ĐÊ LẬP PHIẾU");
                return false;
            }

            for(int  i = 0; i< iGridDataSource.Rows.Count;i++)
            {
                tenSP = this.iGridDataSource.Rows[i]["TEN_SANPHAM"].ToString();

                if (iGridDataSource.Rows[i]["SO_LUONG_TONG"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "VUI LÒNG KHAI BÁO SỐ LƯƠNG " +tenSP);
                    return false;
                }
                if (iGridDataSource.Rows[i]["VITRI"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN VỊ TRÍ HÀNG " +tenSP);
                    return false;
                }
                if (iGridDataSource.Rows[i]["MA_TINHTRANG_HANG"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN TRẠNG THÁI HÀNG "+tenSP);
                    return false;
                }
            }

            return true;
        }

        private bool KiemTraSoLuongAll()
        {
            for (int i = 0; i < iGridDataSource.Rows.Count; i++)
            {
                if (PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_CHECKEXISTS_DICHVU_NHAPVTTL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"])) == true)
                {
                    if (ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]) == 0)
                    {
                        dtSL = PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"]));
                        dtSLSPVTTL = PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_VTTL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"])); ;

                        if (dtSL.Rows.Count == 0)
                        {
                            sl = 0;
                        }
                        else
                        {
                            sl = ConstCommon.NVL_NUM_NT_NEW(dtSL.Rows[0][0].ToString());
                        }

                        if (dtSLSPVTTL.Rows.Count == 0)
                        {
                            slspvttl = 0;
                        }
                        else
                        {
                            slspvttl = ConstCommon.NVL_NUM_NT_NEW(dtSLSPVTTL.Rows[0][0].ToString());
                        }


                        slNhap = ConstCommon.NVL_NUM_NT_NEW(iGridDataSource.Rows[i]["SO_LUONG_TONG"]);
                        slTong = sl + slNhap;
                        tenSP = this.iGridDataSource.Rows[i]["TEN_SANPHAM"].ToString();
                        for (int j = 0; j < frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows.Count; j++)
                        {
                            if (ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SANPHAM_ID"]))
                            {
                                if (slTong > (ConstCommon.NVL_NUM_NT_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SO_LUONG"]) + slspvttl))
                                {
                                    base.ShowMessage(MessageType.Information, "SỐ LƯỢNG SẢN PHẨM" + " " + tenSP + " " + "KHÔNG ĐƯỢC VƯỢT QUÁ " + (ConstCommon.NVL_NUM_LONG_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SO_LUONG"]) - sl + slspvttl));
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtSL = PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"]));
                        dtSLSPVTTL = PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_VTTL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"])); ;
                        dtSLSPChon = PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_ID(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]), ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"]));

                        if (dtSL.Rows.Count == 0)
                        {
                            sl = 0;
                        }
                        else
                        {
                            sl = ConstCommon.NVL_NUM_NT_NEW(dtSL.Rows[0][0].ToString());
                        }

                        if (dtSLSPVTTL.Rows.Count == 0)
                        {
                            slspvttl = 0;
                        }
                        else
                        {
                            slspvttl = ConstCommon.NVL_NUM_NT_NEW(dtSLSPVTTL.Rows[0][0].ToString());
                        }

                        if (dtSLSPChon.Rows.Count == 0)
                        {
                            slspChon = 0;
                        }
                        else
                        {
                            slspChon = ConstCommon.NVL_NUM_NT_NEW(dtSLSPChon.Rows[0][0].ToString());
                        }

                        
                        slNhap = ConstCommon.NVL_NUM_NT_NEW(iGridDataSource.Rows[i]["SO_LUONG_TONG"]);
                        slTong = sl + slNhap - slspChon;
                        tenSP = this.iGridDataSource.Rows[i]["TEN_SANPHAM"].ToString();
                        for (int j = 0; j < frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows.Count; j++)
                        {
                            if (ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SANPHAM_ID"]))
                            {
                                if (slTong > (ConstCommon.NVL_NUM_NT_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SO_LUONG"]) + slspvttl))
                                {
                                    base.ShowMessage(MessageType.Information, "SỐ LƯỢNG SẢN PHẨM" + " " + tenSP + " " + "KHÔNG ĐƯỢC VƯỢT QUÁ " + (ConstCommon.NVL_NUM_LONG_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SO_LUONG"]) - sl + slspChon + slspvttl));
                                    return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]) == 0)
                    {
                        dtSL = PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"]));
                        if(dtSL.Rows.Count == 0)
                        {
                            sl = 0;
                        }
                        else
                        {
                            sl = ConstCommon.NVL_NUM_NT_NEW(dtSL.Rows[0][0].ToString());
                        }
                        
                        slNhap = ConstCommon.NVL_NUM_NT_NEW(iGridDataSource.Rows[i]["SO_LUONG_TONG"]);
                        slTong = sl + slNhap;
                        tenSP = this.iGridDataSource.Rows[i]["TEN_SANPHAM"].ToString();
                        for (int j = 0; j < frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows.Count; j++)
                        {
                            if (ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SANPHAM_ID"]))
                            {
                                if (slTong > ConstCommon.NVL_NUM_NT_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SO_LUONG"]))
                                {
                                    base.ShowMessage(MessageType.Information, "SỐ LƯỢNG SẢN PHẨM" + " " + tenSP + " " + "KHÔNG ĐƯỢC VƯỢT QUÁ " + (ConstCommon.NVL_NUM_LONG_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SO_LUONG"]) - sl));
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtSL = PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"]));
                        dtSLSPChon = PHIEU_NHAPXUATKHO_DICHVUBussiness.KO_PHIEU_NHAPXUATKHO_DICHVU_GET_SOLUONG_ID(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]), ConstCommon.NVL_NUM_LONG_NEW(iDataSource.Rows[0]["PHIEUYEUCAU_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"]));
                        if (dtSL.Rows.Count == 0)
                        {
                            sl = 0;
                        }
                        else
                        {
                            sl = ConstCommon.NVL_NUM_NT_NEW(dtSL.Rows[0][0].ToString());
                        }

                        if (dtSLSPChon.Rows.Count == 0)
                        {
                            slspChon = 0;
                        }
                        else
                        {
                            slspChon = ConstCommon.NVL_NUM_NT_NEW(dtSLSPChon.Rows[0][0].ToString());
                        }              
                        slNhap = ConstCommon.NVL_NUM_NT_NEW(iGridDataSource.Rows[i]["SO_LUONG_TONG"]);
                        slTong = sl + slNhap - slspChon;
                        tenSP = this.iGridDataSource.Rows[i]["TEN_SANPHAM"].ToString();
                        for (int j = 0; j < frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows.Count; j++)
                        {
                            if (ConstCommon.NVL_NUM_LONG_NEW(iGridDataSource.Rows[i]["SANPHAM_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SANPHAM_ID"]))
                            {
                                if (slTong > ConstCommon.NVL_NUM_NT_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SO_LUONG"]))
                                {
                                    base.ShowMessage(MessageType.Information, "SỐ LƯỢNG SẢN PHẨM" + " " + tenSP + " " + "KHÔNG ĐƯỢC VƯỢT QUÁ " + (ConstCommon.NVL_NUM_LONG_NEW(frm_lap_phieu_xuat_kho_vattu_dich_vu_popup.dt_PHIEU_CTIET_NVL.Rows[j]["SO_LUONG"]) - sl + slspChon));
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
