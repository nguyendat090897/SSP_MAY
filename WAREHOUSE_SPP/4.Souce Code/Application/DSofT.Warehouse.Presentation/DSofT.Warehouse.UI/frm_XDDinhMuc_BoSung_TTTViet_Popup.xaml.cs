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
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Ngo Gia Thien
    ///  Create date: 22/03/2018
    ///  Editor:
    ///  Modify date:
    ///  Description:Xay dung dinh muc bo sung thong tin tieng viet popup
    /// </summary>
    public partial class frm_XDDinhMuc_BoSung_TTTViet_Popup : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper1 = null;
        IXD_DM_TTTVIETBussiness XD_DM_TTTVIETBussiness;
        DataRow iRowSelGb = null;
        DataTable dt_SP = null;
        DataTable dt_NVL_T = null;
        DataTable dt_NVL_SP = null;
        public static decimal tinhgn;
        public static decimal tyle;
        public frm_XDDinhMuc_BoSung_TTTViet_Popup()
        {
            InitializeComponent();
            XD_DM_TTTVIETBussiness = new XD_DM_TTTVIETBussiness(string.Empty);
            this.iDataSource = TableSchemaDefineBingding();
            dt_SP = this.TableSchemaDefineBingding_SP();
            dt_SP.Clear();
            dt_NVL_T = this.TableSchemaDefineBingding_NVL_THUNG();
            dt_NVL_T.Clear();
            dt_NVL_SP = this.TableSchemaDefineBingding_NVL_SP();
            dt_NVL_SP.Clear();
            Initialize_Grid();
            Initialize_Grid1();
            this.DataContext = this.iDataSource;
            btnCPDV();
            loadCboDVT();

        }

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGb = this.Parameter[0] as DataRow;
                DispalayRequest();

                if (this.Parameter.Count() > 1)
                {
                    DataRow[] dr_NVL_T = this.Parameter[1] as DataRow[];
                    DataRow[] dr_NVL_SP = this.Parameter[2] as DataRow[];

                    dt_NVL_T.Clear();
                    dt_NVL_SP.Clear();
                    if (dr_NVL_T.Length > 0)
                    {
                        dt_NVL_T = dr_NVL_T.CopyToDataTable();
                    }

                    if (dt_NVL_T != null)
                    {
                        this.iGridHelper.BindItemSource(dt_NVL_T);
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }

                    if (dr_NVL_SP.Length > 0)
                    {
                        dt_NVL_SP = dr_NVL_SP.CopyToDataTable();
                    }

                    if (dt_NVL_SP != null)
                    {
                        this.iGridHelper1.BindItemSource(dt_NVL_SP);
                    }
                    else
                    {
                        grd1.ItemsSource = null;
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
        public void loadCboDVT()
        {
            try
            {
                DataTable dtdvt = XD_DM_TTTVIETBussiness.DM_DONVI_TINH_GET_LIST_ALL();
                if (dtdvt != null && dtdvt.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(this.cboDVT, "TEN_DONVI_TINH", "MA_DONVI_TINH", dtdvt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboDVT, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["MA_DONVI_TINH_DONGGOI"] = cboDVT.GetKeyValue(0);
                }
            }
            catch (Exception ex)
            {

                base.ShowMessage(MessageType.Error, ex.Message,ex);
            }
        } 

        public void btnCPDV()
        {
            if(frm_XDDinhMuc_BoSung_TTTViet.status == false)
            {
                btnKhaiBaoCPDV.IsEnabled = false;
            }
            if (frm_XDDinhMuc_BoSung_TTTViet.status == true)
            {
                btnKhaiBaoCPDV.IsEnabled = true;
            }
        }
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("DINHMUC_TEMTOA_ID", typeof(string));
                xDicUser.Add("LOAI_DINHMUC", typeof(string));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));

                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));

                xDicUser.Add("SOLUONG_DONGGOI", typeof(decimal));
                xDicUser.Add("MA_DONVI_TINH_DONGGOI", typeof(string));
                xDicUser.Add("CHIPHI_DICHVU", typeof(decimal));
                xDicUser.Add("NHANTHEM_TYLE", typeof(decimal));
                xDicUser.Add("GIANHAP", typeof(decimal));
                xDicUser.Add("GIABAN", typeof(decimal));
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
        /// <summary>
        /// TableSchemaDefineBingding_SP
        /// </summary>
        /// <returns></returns>
        private DataTable TableSchemaDefineBingding_SP()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("DINHMUC_TEMTOA_ID", typeof(string));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));

                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }

        /// <summary>
        /// TableSchemaDefineBingding_NVL_THUNG
        /// </summary>
        /// <returns></returns>
        private DataTable TableSchemaDefineBingding_NVL_THUNG()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string)); 
                xDicUser.Add("DINHMUC_TEMTOA_NVL_THUNG_ID", typeof(decimal));
                xDicUser.Add("DINHMUC_TEMTOA_ID", typeof(decimal));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));

                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));

                xDicUser.Add("SOLUONG", typeof(int));
                xDicUser.Add("TIEU_HAO", typeof(decimal));
                xDicUser.Add("GIANHAP", typeof(decimal));
                xDicUser.Add("THANHTIEN", typeof(decimal));

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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

            return xDt;
        }
        /// <summary>
        /// TableSchemaDefineBingding_NVL_SP
        /// </summary>
        /// <returns></returns>
        private DataTable TableSchemaDefineBingding_NVL_SP()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("DINHMUC_TEMTOA_NVL_SP_ID", typeof(decimal));
                xDicUser.Add("DINHMUC_TEMTOA_ID", typeof(decimal));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));

                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));

                xDicUser.Add("SOLUONG", typeof(int));
                xDicUser.Add("TIEU_HAO", typeof(decimal));
                xDicUser.Add("GIANHAP", typeof(decimal));
                xDicUser.Add("THANHTIEN", typeof(decimal));

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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

            return xDt;
        }
        /// <summary>
        /// NVL Tieu hao cho 1 thung
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

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieuxuat_tuPYC_MaNL"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieuxuat_tuPYC_TenNL"]));
                xColumn.Width = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_sanpham_dichvu_DonViTinh"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SOLUONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_vattu_dichvu_SoLuong"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GIANHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaNhap"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThanhTien"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TIEU_HAO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblHaoHut"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
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
                //this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }


        /// <summary>
        /// NVL Tieu hao cho 1 don vi san pham
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

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieuxuat_tuPYC_MaNL"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper1.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieuxuat_tuPYC_TenNL"]));
                xColumn.Width = 250;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper1.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_sanpham_dichvu_DonViTinh"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper1.Add(xColumn);

                xColumn = new Column("SOLUONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_vattu_dichvu_SoLuong"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                this.iGridHelper1.Add(xColumn);

                xColumn = new Column("GIANHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaNhap"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                this.iGridHelper1.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThanhTien"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                this.iGridHelper1.Add(xColumn);

                xColumn = new Column("TIEU_HAO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblHaoHut"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
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
                //this.grdView1.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid1()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }


        private void btnKhaiBaoCPDV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (iDataSource.Rows.Count == 0) return;
                if (iRowSelGb != null)
                {
                    tinhgn = ConstCommon.NVL_NUM_DECIMAL_NEW(txtGiaNhap.Text);
                    object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_KhaiBaoCPDV", iRowSelGb);
                    loadCPDV();
                }
                
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        /// <summary>
        /// Xoa dong NVL cho thung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void xButton_RemoveClick(object sender, RoutedEventArgs e)
        {

            try
            {
                bool msg = false;
                if ((this.dt_NVL_T == null) || (this.dt_NVL_T.Rows.Count == 0))
                {
                    return;
                }

                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;

                if (ConstCommon.NVL_NUM_LONG_NEW(row["DINHMUC_TEMTOA_NVL_THUNG_ID"]) > 0)
                {
                    msg = (bool)base.ShowMessage(MessageType.OkCancel, "BẠN CÓ MUỐN XÓA DÒNG ĐANG CHỌN KHÔNG???");
                    if (msg == true)
                    {
                        DataTable dsReturn = XD_DM_TTTVIETBussiness.HT_DINHMUC_TEMTOA_NVL_THUNG_DELETE(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(row.Row["DINHMUC_TEMTOA_NVL_THUNG_ID"]), CommonDataHelper.UserName);
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

                dt_NVL_T.Rows.Remove(row.Row);

                tinhGiaNhap();
                tinhTyLe();

                this.iGridHelper.BindItemSource(this.dt_NVL_T);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// Xoa dong NVL cho 1 san pham
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void xButton_RemoveClick1(object sender, RoutedEventArgs e)
        {

            try
            {
                bool msg = false;
                if ((this.dt_NVL_SP == null) || (this.dt_NVL_SP.Rows.Count == 0))
                {
                    return;
                }

                if (this.grd1.GetFocusedRow() == null) return;
                DataRowView row = this.grd1.GetFocusedRow() as DataRowView;

                if (ConstCommon.NVL_NUM_LONG_NEW(row["DINHMUC_TEMTOA_NVL_SP_ID"]) > 0)
                {
                    msg = (bool)base.ShowMessage(MessageType.OkCancel, "BẠN CÓ MUỐN XÓA DÒNG ĐANG CHỌN KHÔNG???");
                    if (msg == true)
                    {
                        DataTable dsReturn = XD_DM_TTTVIETBussiness.HT_DINHMUC_TEMTOA_NVL_SP_DELETE(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(row.Row["DINHMUC_TEMTOA_NVL_SP_ID"]), CommonDataHelper.UserName);
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

                dt_NVL_SP.Rows.Remove(row.Row);


                this.iGridHelper1.BindItemSource(this.dt_NVL_SP);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick1()");
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
        /// Chon San Pham
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChon_Click(object sender, RoutedEventArgs e)
        {
            dt_SP.Clear();
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP", null);
                DataTable dt_SPCHON = (DataTable)xReturn;

                if ((dt_SPCHON != null) && (dt_SPCHON.Rows.Count > 0))
                {
                    if (dt_SPCHON.Rows[0]["GHI_CHU"].ToString() == "Y")
                    {

                        for (int i = 0; i < dt_SPCHON.Rows.Count; i++)
                        {
                            if (ContainDataRowInDataTable(dt_SP, dt_SPCHON.Rows[i]) == false)
                            {
                                DataRow dr = dt_SP.NewRow();
                                dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr["DINHMUC_TEMTOA_ID"] = "0";
                                dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                dr["MA_DONVI_TINH"] = dt_SPCHON.Rows[i]["MA_DONVI_TINH"];
                                dt_SP.Rows.Add(dr);
                            }
                        }
                    }
                }

                if(dt_SP.Rows.Count > 0)
                {
                    if (dt_SP.Rows.Count == 1 && dt_SP.Rows[0]["MA_ITEM_TYPE"].ToString() != "NVL")
                    {
                        txtMaSP.Text = dt_SP.Rows[0]["MA_SANPHAM"].ToString();
                        txtTenSP.Text = dt_SP.Rows[0]["TEN_SANPHAM"].ToString();
                        txtDVT.Text = dt_SP.Rows[0]["MA_DONVI_TINH"].ToString();

                        iDataSource.Rows[0]["MA_DVIQLY"] = dt_SP.Rows[0]["MA_DVIQLY"].ToString();
                        iDataSource.Rows[0]["MA_ITEM_TYPE"] = dt_SP.Rows[0]["MA_ITEM_TYPE"].ToString();
                        iDataSource.Rows[0]["SANPHAM_ID"] = dt_SP.Rows[0]["SANPHAM_ID"].ToString();
                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN ĐÚNG SẢN PHẨM VÀ TỐI ĐA LÀ MỘT SẢN PHẨM");
                        dt_SP.Clear();
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChon.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// Chon nguyen vat lieu khai bao cho thung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChonNVL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP", null);
                DataTable dt_SPCHON = (DataTable)xReturn;

                if ((dt_SPCHON != null) && (dt_SPCHON.Rows.Count > 0))
                {
                    if (dt_SPCHON.Rows[0]["GHI_CHU"].ToString() == "Y")
                    {

                        for (int i = 0; i < dt_SPCHON.Rows.Count; i++)
                        {
                            if (ContainDataRowInDataTable(dt_NVL_T, dt_SPCHON.Rows[i]) == false)
                            {
                                DataRow dr = dt_NVL_T.NewRow();
                                dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr["DINHMUC_TEMTOA_NVL_THUNG_ID"] = "0";
                                dr["DINHMUC_TEMTOA_ID"] = "0";
                                dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                dr["MA_DONVI_TINH"] = dt_SPCHON.Rows[i]["MA_DONVI_TINH"];
                                dt_NVL_T.Rows.Add(dr);
                            }
                        }
                    }
                }

                if (dt_NVL_T.Rows.Count > 0)
                {
                    if (dt_NVL_T.Rows[0]["MA_ITEM_TYPE"].ToString() == "NVL")
                    {
                        this.iGridHelper.BindItemSource(dt_NVL_T);
                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN ĐÚNG NGUYÊN VẬT LIỆU");
                        dt_NVL_T.Clear();
                        return;
                    }

                }
                else
                {
                    this.grd.ItemsSource = null;
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonNVL.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// chon nguyen vat lieu khai bao cho san pham
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChonNVL1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP", null);
                DataTable dt_SPCHON = (DataTable)xReturn;

                if ((dt_SPCHON != null) && (dt_SPCHON.Rows.Count > 0))
                {
                    if (dt_SPCHON.Rows[0]["GHI_CHU"].ToString() == "Y")
                    {

                        for (int i = 0; i < dt_SPCHON.Rows.Count; i++)
                        {
                            if (ContainDataRowInDataTable(dt_NVL_SP, dt_SPCHON.Rows[i]) == false)
                            {
                                DataRow dr = dt_NVL_SP.NewRow();
                                dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr["DINHMUC_TEMTOA_NVL_SP_ID"] = "0";
                                dr["DINHMUC_TEMTOA_ID"] = "0";
                                dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                dr["MA_DONVI_TINH"] = dt_SPCHON.Rows[i]["MA_DONVI_TINH"];
                                dt_NVL_SP.Rows.Add(dr);
                            }
                        }
                    }
                }

                if (dt_NVL_SP.Rows.Count > 0)
                {
                    if (dt_NVL_SP.Rows[0]["MA_ITEM_TYPE"].ToString() == "NVL")
                    {
                        this.iGridHelper1.BindItemSource(dt_NVL_SP);
                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN ĐÚNG NGUYÊN VẬT LIỆU");
                        dt_NVL_SP.Clear();
                        return;
                    }
                    
                }
                else
                {
                    this.grd1.ItemsSource = null;
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonNVL1.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        /// <summary>
        /// Tinh thanh tien
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            tinhGiaNhap();
            btnKhaiBaoCPDV.IsEnabled = false;
        }

        private void grdView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd1.GetFocusedRow() as DataRowView;
                row["THANHTIEN"] = ConstCommon.NVL_NUM_DECIMAL_NEW(row["SOLUONG"]) * ConstCommon.NVL_NUM_NT_NEW(row["GIANHAP"]);

                
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView1_CellValueChanged");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void txtNhanTL_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            tyle = ConstCommon.NVL_NUM_DECIMAL_NEW(txtNhanTL.Text) / 100;
            tinhTyLe();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (checkNull() == false)
                {
                    return;
                }

                ConstCommon.mousebusy(this);

                DataSet dsReturn = null;
                dsReturn = XD_DM_TTTVIETBussiness.InsertHT_DINHMUC_TEMTOA(iDataSource, dt_NVL_T,dt_NVL_SP);
                if ((dsReturn != null) && (dsReturn.Tables.Count == 3))
                {

                    foreach (DataColumn item in dsReturn.Tables[0].Columns)
                    {
                        if (this.iDataSource.Columns[item.ColumnName] != null)
                        {
                            this.iDataSource.Rows[0][item.ColumnName] = dsReturn.Tables[0].Rows[0][item.ColumnName];
                        }
                    }

                    dt_NVL_T.Clear();
                    dt_NVL_T = dsReturn.Tables[1].Copy();
                    dt_NVL_SP.Clear();
                    dt_NVL_SP = dsReturn.Tables[2].Copy();
                    if (dt_NVL_T != null)
                    {
                        this.iGridHelper.BindItemSource(dt_NVL_T);
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }

                    if(dt_NVL_SP != null)
                    {
                        this.iGridHelper1.BindItemSource(dt_NVL_SP);
                    }
                    else
                    {
                        grd1.ItemsSource = null;
                    }
                    ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                    btnKhaiBaoCPDV.IsEnabled = true;
                    DataTable dtid = XD_DM_TTTVIETBussiness.GetListHT_DINHMUC_TEMTOA_ID(ConstCommon.DonViQuanLy);
                    if(dtid != null && dtid.Rows.Count > 0)
                    {
                        iRowSelGb = dtid.Rows[0];
                    }
                    return;
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

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.iDataSource = this.TableSchemaDefineBingding();
                this.DataContext = this.iDataSource;
                grd.ItemsSource = null;
                grd1.ItemsSource = null;
                if (dt_NVL_T != null)
                {
                    dt_NVL_T.Clear();
                }
                if (dt_NVL_SP != null)
                {
                    dt_NVL_SP.Clear();
                }
                loadCboDVT();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void loadCPDV()
        {
            decimal cpdv = 0;
            cpdv = frm_XDDinhMuc_BoSung_TTTViet_KhaiBaoCPDV.CPDV;
            txtCPDV.Text = cpdv.ToString();

            decimal giaban = 0;
            giaban = cpdv + ConstCommon.NVL_NUM_DECIMAL_NEW(txtGiaNhap.Text) + (ConstCommon.NVL_NUM_DECIMAL_NEW(txtGiaNhap.Text) * (ConstCommon.NVL_NUM_DECIMAL_NEW(txtNhanTL.Text) / 100));
            txtGiaBan.Text = giaban.ToString();

        }

        private void tinhGiaNhap()
        {
            try
            {

                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;
                row["THANHTIEN"] = ConstCommon.NVL_NUM_NT_NEW(row["SOLUONG"]) * ConstCommon.NVL_NUM_DECIMAL_NEW(row["GIANHAP"]);

                decimal gianhap = 0;
                for (int i = 0; i < this.dt_NVL_T.Rows.Count; i++)
                {
                    gianhap += ConstCommon.NVL_NUM_DECIMAL_NEW(this.dt_NVL_T.Rows[i]["THANHTIEN"].ToString());
                    txtGiaNhap.Text = gianhap.ToString();
                }

                

                decimal giaban = 0;
                giaban = ConstCommon.NVL_NUM_DECIMAL_NEW(txtGiaNhap.Text) + (ConstCommon.NVL_NUM_DECIMAL_NEW(txtGiaNhap.Text) * (ConstCommon.NVL_NUM_DECIMAL_NEW(txtNhanTL.Text) / 100) + ConstCommon.NVL_NUM_DECIMAL_NEW(txtCPDV.Text));
                txtGiaBan.Text = giaban.ToString();
            }
            catch (Exception ex)
            {

                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void tinhTyLe()
        {
            try
            {
                decimal giaban = 0;
                giaban = ConstCommon.NVL_NUM_DECIMAL_NEW(txtGiaNhap.Text) + (ConstCommon.NVL_NUM_DECIMAL_NEW(txtGiaNhap.Text) * (ConstCommon.NVL_NUM_DECIMAL_NEW(txtNhanTL.Text) / 100)) + ConstCommon.NVL_NUM_DECIMAL_NEW(txtCPDV.Text);
                txtGiaBan.Text = giaban.ToString();
            }
            catch (Exception ex)
            {

                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private bool checkNull()
        {
            if(txtMaSP.Text == string.Empty && txtTenSP.Text == string.Empty && txtDVT.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN SẢN PHẨM ĐỂ KHAI BÁO ĐỊNH MỨC");
                return false;
            }

            if (XD_DM_TTTVIETBussiness.HT_DINHMUC_TEMTOA_CHECKEXISTS(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["DINHMUC_TEMTOA_ID"]), ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["SANPHAM_ID"])) == true)
            {
                base.ShowMessage(MessageType.Information, " SẢN PHẨM ĐÃ ĐƯỢC KHAI BÁO ĐỊNH MỨC KHUYẾN MÃI");
                return false;
            }

            if(this.dt_NVL_T.Rows.Count == 0)
            {
                base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN NGUYÊN VẬT LIỆU ĐỂ KHÁI BÁO CHO THÙNG");
                return false;
            }

            for (int i = 0; i < this.dt_NVL_T.Rows.Count; i++)
            {
                if (this.dt_NVL_T.Rows[i]["SOLUONG"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "VUI LÒNG KHAI BÁO SỐ LƯỢNG CHO THÙNG");
                    return false;
                }
                if (this.dt_NVL_T.Rows[i]["GIANHAP"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, " VUI LÒNG KHAI BÁO GIÁ NHẬP CHO THÙNG");
                    return false;
                }
            }

            if (this.dt_NVL_SP.Rows.Count == 0)
            {
                base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN NGUYÊN VẬT LIỆU ĐỂ KHÁI BÁO CHO SẢN PHẨM");
                return false;
            }
   

            for (int i = 0; i < this.dt_NVL_SP.Rows.Count; i++)
            {
                if (this.dt_NVL_SP.Rows[i]["SOLUONG"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "VUI LÒNG KHAI BÁO SỐ LƯỢNG CHO SẢN PHẨM");
                    return false;
                }
                if (this.dt_NVL_SP.Rows[i]["GIANHAP"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, " VUI LÒNG KHAI BÁO GIÁ NHẬP CHO SẢN PHẨM");
                    return false;
                }
            }
            
            return true;
        }

    }
}