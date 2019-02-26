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
using DevExpress.Xpf.Grid;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Media;
using DSofT.Framework.UICore.TaskProxy;
using DevExpress.Xpf.Editors.Settings;
using System.Windows.Documents;
using DSofT.Framework.Client.ExportHelper;
using Microsoft.Win32;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_NhapKho_popup.xaml
    /// </summary>
    public partial class frm_NhapKho_popup_xep_hang_vao_kho : PopupBase
    {
        #region Khai báo
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        DataTable dt_PHIEU_CTIET = null;
        DataTable dt_KHO = null;
        DataTable dt_KHO_KHUVUC = null;
        DataTable dt_VITRIHANG = null;
        DataTable dt_PHIEU_CTIET_VITRI = null;
        int so_luong_tong = 0;
        int so_luong_xep = 0;
        bool isFirstLoad = false;
        IPHIEU_NHAPXUATKHOBussiness _IPHIEU_NHAP_XUAT_KHO;
        public string pN_OR_N = "N";
        #endregion
        #region Method
        public frm_NhapKho_popup_xep_hang_vao_kho()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            dt_PHIEU_CTIET = this.TableSchemaDefineBingding_Grid();
            dt_PHIEU_CTIET.Clear();
            _IPHIEU_NHAP_XUAT_KHO = new PHIEU_NHAPXUATKHOBussiness(string.Empty);
            this.DataContext = this.iDataSource;
            LoadComboBox();
            Initialize_Grid_CTPHIEUNHAP();
            this.iDataSource.Rows[0]["TEN_KHO"] = ConstCommon.pTEN_KHO;
            this.iDataSource.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
            chkIS_NHAPNHIEULAN.IsEnabled = false;
        }
        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {

                iRowSelGb = this.Parameter[0] as DataRow;
                pN_OR_N = this.Parameter[2].ToString();
                DataSet ds_ctiet_vitri = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_ALL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_ID"]));
                LoadComboBox();
                Initialize_Grid_CTPHIEUNHAP();

                if (pN_OR_N == "X")
                {
                    lblngaynhap.Text = "Ngày xuất";
                    Run run = new Run("*");
                    run.Foreground = Brushes.Red;
                    lblngaynhap.Inlines.Add(run);

                    Run run1 = new Run("*");
                    run1.Foreground = Brushes.Red;

                    lblhinhthucnhap.Text = "Hình thức xuất";
                    lblhinhthucnhap.Inlines.Add(run1);
                    chkIS_NHAPNHIEULAN.Content = "Xuất nhiều lần";
                    lblkhonhap.Text = "Kho xuất";
                    lblNCC.Text = "Khách hàng";
                    lbldiadiemxuathang.Text = "Địa chỉ nhận hàng";
                }
                DispalayRequest();
                txtID_PHIEU_YC.Text = this.iDataSource.Rows[0]["SOPHIEU_YEUCAU"].ToString();
                if (this.iDataSource.Rows[0]["IS_NHAPNHIEULAN"].ToString() == "True")
                    chkIS_NHAPNHIEULAN.IsChecked = true;
                isFirstLoad = true;
                if (this.Parameter.Count() > 1)
                {
                    DataTable dst = ds_ctiet_vitri.Tables[0];
                    DataRow[] dr_chitiet = this.Parameter[1] as DataRow[];
                    if ((ds_ctiet_vitri != null) && (dst.Rows.Count > 0))
                    {
                        txtkhonhap.Text = dr_chitiet[0]["TEN_KHO"].ToString();
                        dt_PHIEU_CTIET = dr_chitiet.CopyToDataTable();
                        dt_PHIEU_CTIET_VITRI = TableSchemaDefineBingding_Grid();
                        dt_PHIEU_CTIET_VITRI.Clear();
                        for(int i =0;i < dt_PHIEU_CTIET.Rows.Count;i++)
                        {
                            DataRow dr = dt_PHIEU_CTIET_VITRI.NewRow();
                            dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                            dr["PHIEU_NHAPXUATKHO_CTIET_VTRI_ID"] = "0";
                            dr["PHIEU_NHAPXUATKHO_CTIET_ID"] = dt_PHIEU_CTIET.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"];
                            dr["KHO_ID"] = dt_PHIEU_CTIET.Rows[i]["KHO_ID"];
                            dr["TEN_KHO"] = dt_PHIEU_CTIET.Rows[i]["TEN_KHO"];
                            dr["SANPHAM_ID"] = dt_PHIEU_CTIET.Rows[i]["SANPHAM_ID"];
                            dr["TEN_SANPHAM"] = dt_PHIEU_CTIET.Rows[i]["TEN_SANPHAM"];
                            dr["SOLO"] = dt_PHIEU_CTIET.Rows[i]["SOLO"];
                            dr["HANDUNG"] = dt_PHIEU_CTIET.Rows[i]["HANDUNG"];
                            dr["SO_LUONG_TONG"] = dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG"];
                            dr["LA_PARENT"] = "1";
                            dt_PHIEU_CTIET_VITRI.Rows.Add(dr);
                            DataSet ds_ctiet_sanpham = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_BY_PHIEU(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_ID"]), ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"]), ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["SANPHAM_ID"]));
                            DataTable dt = ds_ctiet_sanpham.Tables[0];
                            for(int j = 0; j < dt.Rows.Count; j++)
                            {
                                DataRow dr2 = dt_PHIEU_CTIET_VITRI.NewRow();
                                dr2["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr2["PHIEU_NHAPXUATKHO_CTIET_VTRI_ID"] = dt.Rows[j]["PHIEU_NHAPXUATKHO_CTIET_VTRI_ID"]; 
                                dr2["PHIEU_NHAPXUATKHO_CTIET_ID"] = dt.Rows[j]["PHIEU_NHAPXUATKHO_CTIET_ID"];
                                dr2["KHO_ID"] = dt.Rows[j]["KHO_ID"];
                                dr2["VITRI"] = dt.Rows[j]["VITRI"];
                                dr2["SANPHAM_ID"] = dt.Rows[j]["SANPHAM_ID"];
                                dr2["TEN_SANPHAM"] = dt.Rows[j]["TEN_SANPHAM"];
                                dr2["SOLO"] = dt.Rows[j]["SOLO"];
                                dr2["HANDUNG"] = dt.Rows[j]["HANDUNG"];
                                dr2["SO_LUONG_TONG"] = dt.Rows[j]["SO_LUONG_TONG"];
                                dr2["MA_VACH"] = dt.Rows[j]["MA_VACH"];
                                dr2["LA_PARENT"] = "0";
                                dt_PHIEU_CTIET_VITRI.Rows.Add(dr2);
                            }
                            for (int  k= 0; k < (ConstCommon.pSO_VITRI_XEP_MOIDONG - dt.Rows.Count); k++)
                            {
                                if (dt.Rows.Count == ConstCommon.pSO_VITRI_XEP_MOIDONG)
                                    break;
                                DataRow dr3 = dt_PHIEU_CTIET_VITRI.NewRow();
                                dr3["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr3["PHIEU_NHAPXUATKHO_CTIET_VTRI_ID"] = 0;
                                dr3["PHIEU_NHAPXUATKHO_CTIET_ID"] = dt_PHIEU_CTIET.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"];
                                dr3["KHO_ID"] = dt_PHIEU_CTIET.Rows[i]["KHO_ID"];
                                dr3["VITRI"] = null;
                                dr3["SANPHAM_ID"] = dt_PHIEU_CTIET.Rows[i]["SANPHAM_ID"];
                                dr3["TEN_SANPHAM"] = dt_PHIEU_CTIET.Rows[i]["TEN_SANPHAM"];
                                dr3["SOLO"] = dt_PHIEU_CTIET.Rows[i]["SOLO"];
                                dr3["HANDUNG"] = dt_PHIEU_CTIET.Rows[i]["HANDUNG"];
                                dr3["SO_LUONG_TONG"] = 0;
                                dr3["MA_VACH"] = dt_PHIEU_CTIET.Rows[i]["MA_VACH"];
                                dr3["LA_PARENT"] = "0";
                                dt_PHIEU_CTIET_VITRI.Rows.Add(dr3);
                            }
                        }
                        if (dt_PHIEU_CTIET_VITRI != null)
                        {
                            this.iGridHelper.BindItemSource(dt_PHIEU_CTIET_VITRI);
                        }
                        else
                        {
                            grd.ItemsSource = null;
                        }
                    }
                    else
                    {
                        txtkhonhap.Text = dr_chitiet[0]["TEN_KHO"].ToString();
                        dt_PHIEU_CTIET = dr_chitiet.CopyToDataTable();
                        dt_PHIEU_CTIET_VITRI = TableSchemaDefineBingding_Grid();
                        dt_PHIEU_CTIET_VITRI.Clear();
                        for(int i = 0;i< dt_PHIEU_CTIET.Rows.Count;i++)
                        {
                            DataRow dr = dt_PHIEU_CTIET_VITRI.NewRow();
                            dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                            dr["PHIEU_NHAPXUATKHO_CTIET_VTRI_ID"] = "0";
                            dr["PHIEU_NHAPXUATKHO_CTIET_ID"] = dt_PHIEU_CTIET.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"];
                            dr["KHO_ID"] = dt_PHIEU_CTIET.Rows[i]["KHO_ID"];
                            dr["TEN_KHO"] = dt_PHIEU_CTIET.Rows[i]["TEN_KHO"];
                            //dr["VITRI"] = dt_PHIEU_CTIET.Rows[i]["VITRI"];
                            dr["SANPHAM_ID"] = dt_PHIEU_CTIET.Rows[i]["SANPHAM_ID"];
                            dr["TEN_SANPHAM"] = dt_PHIEU_CTIET.Rows[i]["TEN_SANPHAM"];
                            dr["SOLO"] = dt_PHIEU_CTIET.Rows[i]["SOLO"];
                            dr["HANDUNG"] = dt_PHIEU_CTIET.Rows[i]["HANDUNG"];
                            dr["SO_LUONG_TONG"] = dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG"];
                            //  dr["MA_VACH"] = dt_PHIEU_CTIET.Rows[i]["MA_VACH"];
                            dr["LA_PARENT"] = "1";
                            dt_PHIEU_CTIET_VITRI.Rows.Add(dr);
                            for(int j = 0;j < ConstCommon.pSO_VITRI_XEP_MOIDONG;j++)
                            {
                                DataRow dr2 = dt_PHIEU_CTIET_VITRI.NewRow();
                                dr2["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr2["PHIEU_NHAPXUATKHO_CTIET_VTRI_ID"] = "0";
                                dr2["PHIEU_NHAPXUATKHO_CTIET_ID"] = dt_PHIEU_CTIET.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"];
                                dr2["KHO_ID"] = dt_PHIEU_CTIET.Rows[i]["KHO_ID"];
                                dr2["VITRI"] = null;
                                dr2["SANPHAM_ID"] = dt_PHIEU_CTIET.Rows[i]["SANPHAM_ID"];
                                dr2["TEN_SANPHAM"] = dt_PHIEU_CTIET.Rows[i]["TEN_SANPHAM"];
                                dr2["SOLO"] = dt_PHIEU_CTIET.Rows[i]["SOLO"];
                                dr2["HANDUNG"] = dt_PHIEU_CTIET.Rows[i]["HANDUNG"];
                                dr2["SO_LUONG_TONG"] = 0;
                                dr2["MA_VACH"] = dt_PHIEU_CTIET.Rows[i]["MA_VACH"];
                                dr2["LA_PARENT"] = "0";
                                dt_PHIEU_CTIET_VITRI.Rows.Add(dr2);
                            }
                        }
                        if (dt_PHIEU_CTIET_VITRI != null)
                        {
                            this.iGridHelper.BindItemSource(dt_PHIEU_CTIET_VITRI);
                        }
                        else
                        {
                            grd.ItemsSource = null;
                        }
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
        public void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataSet> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _IPHIEU_NHAP_XUAT_KHO.getKO_PHIEU_NHAPXUATKHO_BY_ID(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]));
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
                xDicUser.Add("SOPHIEU_YEUCAU", typeof(string));
                xDicUser.Add("TEN_CTY", typeof(string));
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
                xDicUser.Add("PHIEU_NHAPXUATKHO_CTIET_VTRI_ID", typeof(decimal));
                xDicUser.Add("KHO_ID", typeof(decimal));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("VITRI", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("SOLO", typeof(string));
                xDicUser.Add("HANDUNG", typeof(string));
                xDicUser.Add("SO_LUONG_TONG", typeof(int));
                xDicUser.Add("MA_VACH", typeof(string));
                xDicUser.Add("IS_XUAT", typeof(bool));
                xDicUser.Add("LA_PARENT", typeof(string)); 
                xDicUser.Add("TEN_KHO", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }
        /// <summary>
        /// Binding dữ liệu vào combobox
        /// </summary>
        private void LoadComboBox()
        {
            try
            {
                dt_KHO = _IPHIEU_NHAP_XUAT_KHO.GetListDM_KHO(ConstCommon.DonViQuanLy);
                dt_KHO_KHUVUC = _IPHIEU_NHAP_XUAT_KHO.DM_KHO_KHUVUC_GET_LIST_BY_KHO(ConstCommon.DonViQuanLy, 0);
                //dt_PALLET = _IPHIEU_NHAP_XUAT_KHO.GetListDM_PALLET(ConstCommon.DonViQuanLy);
                //dt_TRANGTHAI = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TINHTRANG_HANG(ConstCommon.DonViQuanLy);
                //dt_KHUVUC = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TENKHO_KHUVUC(ConstCommon.DonViQuanLy);
                //dt_TINHTRANGCV = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TINHTRANG_CV(ConstCommon.DonViQuanLy);
                dt_VITRIHANG = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_VITRI_HANG(ConstCommon.DonViQuanLy,ConstCommon.pKHO_ID);
                 

                DataTable dtNCC = _IPHIEU_NHAP_XUAT_KHO.GetData_For_Cbo_KhachHang_Ncc(ConstCommon.DonViQuanLy);
                if (dtNCC != null && dtNCC.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboNCC, "TEN_KH", "KHACHHANG_NCC_ID", dtNCC, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboNCC, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["KHACHHANG_NCC_ID"] = cboNCC.GetKeyValue(0);
                }
                DataTable dtHinhhthucnhap = _IPHIEU_NHAP_XUAT_KHO.getDM_HINHTHU_NHAPXUAT(pN_OR_N);

                if (dtHinhhthucnhap != null && dtHinhhthucnhap.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cbohinhthucnhap, "TEN_HINHTHU_NHAPXUAT", "MA_HINHTHU_NHAPXUAT", dtHinhhthucnhap, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cbohinhthucnhap, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["MA_HINHTHU_NHAPXUAT"] = cbohinhthucnhap.GetKeyValue(0);
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadComboBox()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// Xét rỗng các control
        /// </summary>
        private bool SetIsNull()
        {
            try
            {

                if (dt_PHIEU_CTIET.Rows.Count == 0 && dt_PHIEU_CTIET_VITRI.Rows.Count == 0)
                {
                    base.ShowMessage(MessageType.Information, "Chưa xếp các sản phẩm vào vị trí trong kho.");
                    return false;
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
        private void Initialize_Grid_CTPHIEUNHAP()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;

                xColumn = new Column("PHIEU_NHAPXUATKHO_CTIET_VTRI_ID", "PHIEU_NHAPXUATKHO_CTIET_VTRI_ID");
                xColumn.Width = 120;
                xColumn.Visible = false;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SANPHAM_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_KHO",  "Tên kho");
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM",  Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("HANDUNG",  Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG",Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
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

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"])); 
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);//BUTTON

                var converter = new System.Windows.Media.BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#F2F2F2");

                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_TONG",
                    Format = new Format() { Background = brush },
                    Expression = "[LA_PARENT] == 1", // #
                    ApplyToRow = false
                });

                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "VITRI",
                    Format = new Format() { Background = brush },
                    Expression = "[LA_PARENT] == 1", // #
                    ApplyToRow = false
                });

                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "MA_VACH",
                    Format = new Format() { Background = brush },
                    Expression = "[LA_PARENT] == 1", // #
                    ApplyToRow = false
                });
                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "HANDUNG",
                    Format = new Format() { Background = brush },
                    Expression = "[LA_PARENT] == 1", // #
                    ApplyToRow = false
                });
                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "TEN_KHO",
                    Format = new Format() { Background = brush },
                    Expression = "[LA_PARENT] == 1", // #
                    ApplyToRow = false
                });
                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "TEN_SANPHAM",
                    Format = new Format() { Background = brush },
                    Expression = "[LA_PARENT] == 1", // #
                    ApplyToRow = false
                });
                this.grdView.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SOLO",
                    Format = new Format() { Background = brush },
                    Expression = "[LA_PARENT] == 1", // #
                    ApplyToRow = false
                });
                this.iGridHelper.Initialize();
                this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
   
        #endregion
        #region UI
        private void cboNCC_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            try
            {
                DataTable dtDDXH = _IPHIEU_NHAP_XUAT_KHO.getDIADIEM_XUATHANG_BY_NCC(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["KHACHHANG_NCC_ID"]));
                if (dtDDXH != null && dtDDXH.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboDiadiemxuathang, "DIADIEM_GIAO", "KHACHHANG_NCC_NOI_XUATHANG_ID", dtDDXH, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboDiadiemxuathang, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    if (isFirstLoad == false)
                    {
                        this.iDataSource.Rows[0]["KHACHHANG_NCC_NOI_XUATHANG_ID"] = cboDiadiemxuathang.GetKeyValue(0);
                    }
                }
                else
                {
                    cboDiadiemxuathang.ItemsSource = null;
                }

                DataTable dtDonvivanchuyen = _IPHIEU_NHAP_XUAT_KHO.getDONVIVANCHUYEN_BY_NCC(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["KHACHHANG_NCC_ID"]));
                if (dtDonvivanchuyen != null && dtDonvivanchuyen.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboDonvivanchuyen, "TEN_DONVI_VANCHUYEN", "KHACHHANG_NCC_DONVI_VANCHUYEN_ID", dtDonvivanchuyen, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboDonvivanchuyen, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    if (isFirstLoad == false)
                    {
                        this.iDataSource.Rows[0]["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"] = cboDonvivanchuyen.GetKeyValue(0);
                    }
                }
                else
                {
                    cboDonvivanchuyen.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            finally
            {
                isFirstLoad = false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long flag = 0;
                if (dt_PHIEU_CTIET.Rows.Count == 0 && dt_PHIEU_CTIET_VITRI.Rows.Count == 0)
                {
                    base.ShowMessage(MessageType.Information, "Chưa xếp các sản phẩm vào vị trí trong kho.");
                    return;
                }
                if (SetIsNull() == false)
                {
                    return;
                }
                if (dt_PHIEU_CTIET_VITRI.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_PHIEU_CTIET_VITRI.Rows.Count; i++)
                    {
                        if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET_VITRI.Rows[i]["SO_LUONG_TONG"]) == 0 && dt_PHIEU_CTIET_VITRI.Rows[i]["LA_PARENT"].ToString() == "0")
                        {
                            base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG CHO VÀO VỊ TRÍ TRONG KHO] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                            return;
                        }
                        if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET_VITRI.Rows[i]["LA_PARENT"]) == 0)
                        {
                            if (dt_PHIEU_CTIET_VITRI.Rows[i]["VITRI"].ToString() == String.Empty)
                            {
                                base.ShowMessage(MessageType.Information, "Chưa chọn [VỊ TRÍ HÀNG].");
                                return;
                            }
                        }
                    }
                    flag = _IPHIEU_NHAP_XUAT_KHO.InsertorUpdateKO_PHIEU_NHAPXUATKHO_CTIET_VTRI(dt_PHIEU_CTIET_VITRI, CommonDataHelper.UserName);
                }
                if (flag > 0)
                {
                   // DataSet ds_ctiet_vitri = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_ALL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]));
                    dt_PHIEU_CTIET_VITRI.Clear();
                    for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                    {
                        DataRow dr = dt_PHIEU_CTIET_VITRI.NewRow();
                        dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                        dr["PHIEU_NHAPXUATKHO_CTIET_VTRI_ID"] = "0";
                        dr["PHIEU_NHAPXUATKHO_CTIET_ID"] = dt_PHIEU_CTIET.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"];
                        dr["KHO_ID"] = dt_PHIEU_CTIET.Rows[i]["KHO_ID"];
                        dr["TEN_KHO"] = dt_PHIEU_CTIET.Rows[i]["TEN_KHO"];
                        dr["SANPHAM_ID"] = dt_PHIEU_CTIET.Rows[i]["SANPHAM_ID"];
                        dr["TEN_SANPHAM"] = dt_PHIEU_CTIET.Rows[i]["TEN_SANPHAM"];
                        dr["SOLO"] = dt_PHIEU_CTIET.Rows[i]["SOLO"];
                        dr["HANDUNG"] = dt_PHIEU_CTIET.Rows[i]["HANDUNG"];
                        dr["SO_LUONG_TONG"] = dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG"];
                        dr["LA_PARENT"] = "1";
                        dt_PHIEU_CTIET_VITRI.Rows.Add(dr);
                        DataSet ds_ctiet_sanpham = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_BY_PHIEU(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]), ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"]), ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["SANPHAM_ID"]));
                        DataTable dt = ds_ctiet_sanpham.Tables[0];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            DataRow dr2 = dt_PHIEU_CTIET_VITRI.NewRow();
                            dr2["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                            dr2["PHIEU_NHAPXUATKHO_CTIET_VTRI_ID"] = dt.Rows[j]["PHIEU_NHAPXUATKHO_CTIET_VTRI_ID"];
                            dr2["PHIEU_NHAPXUATKHO_CTIET_ID"] = dt.Rows[j]["PHIEU_NHAPXUATKHO_CTIET_ID"];
                            dr2["KHO_ID"] = dt.Rows[j]["KHO_ID"];
                            dr2["VITRI"] = dt.Rows[j]["VITRI"];
                            dr2["SANPHAM_ID"] = dt.Rows[j]["SANPHAM_ID"];
                            dr2["TEN_SANPHAM"] = dt.Rows[j]["TEN_SANPHAM"];
                            dr2["SOLO"] = dt.Rows[j]["SOLO"];
                            dr2["HANDUNG"] = dt.Rows[j]["HANDUNG"];
                            dr2["SO_LUONG_TONG"] = dt.Rows[j]["SO_LUONG_TONG"];
                            dr2["MA_VACH"] = dt.Rows[j]["MA_VACH"];
                            dr2["LA_PARENT"] = "0";
                            dt_PHIEU_CTIET_VITRI.Rows.Add(dr2);
                        }
                    }
                    if (dt_PHIEU_CTIET_VITRI != null)
                    {
                        this.iGridHelper.BindItemSource(dt_PHIEU_CTIET_VITRI);
                        ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                    }
                    else
                    {
                        grd.ItemsSource = null;
                        return;
                    } 
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

        private void txtNGAY_CHUNGTU_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            try
            {
                txtNGAY_CHUNGTU.EditValue = DateTime.ParseExact(txtNGAY_CHUNGTU.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {

            }
        }
     
        private void txtNGAYLAP_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            try
            {
                txtNGAYLAP.EditValue = DateTime.ParseExact(txtNGAYLAP.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {

            }
        }
        #endregion
        private void grdView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (this.grd.GetFocusedRow() == null) return;
            DataRowView row = this.grd.GetFocusedRow() as DataRowView;
            if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[0]["PHIEU_NHAPXUATKHO_CTIET_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_NHAPXUATKHO_CTIET_ID"])
                          && ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[0]["SANPHAM_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(row["SANPHAM_ID"]))
                so_luong_tong = 0;
            for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
            {
                if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"]) != ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_NHAPXUATKHO_CTIET_ID"])
                           && ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["SANPHAM_ID"]) != ConstCommon.NVL_NUM_LONG_NEW(row["SANPHAM_ID"]))
                    so_luong_tong = 0;

                if (so_luong_tong == 0)
                {
                    if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_NHAPXUATKHO_CTIET_ID"])
                           && ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["SANPHAM_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(row["SANPHAM_ID"]))
                    {
                        so_luong_tong += ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG"]);
                        break;
                    }
                    else
                    {
                        so_luong_tong = 0;
                       // break;
                    }
                }
                else
                    break;
            }
            so_luong_xep = 0;
            for (int j = 0; j < dt_PHIEU_CTIET_VITRI.Rows.Count; j++)
            {
                if (so_luong_tong > 0)
                {
                    if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET_VITRI.Rows[j]["PHIEU_NHAPXUATKHO_CTIET_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_NHAPXUATKHO_CTIET_ID"])
                           && ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET_VITRI.Rows[j]["SANPHAM_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(row["SANPHAM_ID"]) && dt_PHIEU_CTIET_VITRI.Rows[j]["LA_PARENT"].ToString() == "0")
                    {
                        so_luong_xep += ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET_VITRI.Rows[j]["SO_LUONG_TONG"]);
                    }
                    if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET_VITRI.Rows[j]["PHIEU_NHAPXUATKHO_CTIET_ID"]) != ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_NHAPXUATKHO_CTIET_ID"])
                           && ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET_VITRI.Rows[j]["SANPHAM_ID"]) != ConstCommon.NVL_NUM_LONG_NEW(row["SANPHAM_ID"]) && dt_PHIEU_CTIET_VITRI.Rows[j]["LA_PARENT"].ToString() == "0")
                    {
                        
                        continue;
                    }
                }
            }
            int so_luong_con_lai = so_luong_tong - so_luong_xep;
            if(so_luong_con_lai >= 0)
            {
                for (int i = 0; i < dt_PHIEU_CTIET_VITRI.Rows.Count; i++)
                {
                    if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET_VITRI.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_NHAPXUATKHO_CTIET_ID"])
                          && ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET_VITRI.Rows[i]["SANPHAM_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(row["SANPHAM_ID"]) && dt_PHIEU_CTIET_VITRI.Rows[i]["LA_PARENT"].ToString() == "1")
                    {
                        dt_PHIEU_CTIET_VITRI.Rows[i]["SO_LUONG_TONG"] = so_luong_con_lai;
                        break;
                    }
                }
                this.iGridHelper.BindItemSource(dt_PHIEU_CTIET_VITRI);
            }
            if (so_luong_con_lai < 0)
            {
                base.ShowMessage(MessageType.Information, "Số lượng đặt tại vị trí vượt quá số lượng nhập còn lại.");
                row["SO_LUONG_TONG"] = 0;
            }
        }
        private void grdView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (dt_PHIEU_CTIET_VITRI.Rows.Count > 0)
                {
                    dt_PHIEU_CTIET_VITRI.AcceptChanges();
                    if (this.grd.GetFocusedRow() == null) return;
                    int vitri = this.grdView.FocusedRowHandle;
                    if (vitri < 0) return;
                    if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET_VITRI.Rows[vitri]["LA_PARENT"].ToString()) == 1)
                    {
                        foreach (Column col in iGridHelper.GetColumns)
                        {
                            if (col.Name == "SO_LUONG_TONG".ToString())
                            {
                                iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                            }
                            if (col.Name == "VITRI".ToString())
                            {
                                iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                            }
                            if (col.Name == "MA_VACH".ToString())
                            {
                                iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                            }
                        }

                    }
                    else
                    {
                        foreach (Column col in iGridHelper.GetColumns)
                        {
                            if (col.Name == "SO_LUONG_TONG".ToString())
                            {
                                iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                            }
                            if (col.Name == "VITRI".ToString())
                            {
                                iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                            }
                            if (col.Name == "MA_VACH".ToString())
                            {
                                iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                            }
                            if (col.Name == "TEN_KHO".ToString())
                            {
                                iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                            }
                            if (col.Name == "TEN_SANPHAM".ToString())
                            {
                                iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                            }
                            if (col.Name == "SOLO".ToString())
                            {
                                iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                            }
                            if (col.Name == "HANDUNG".ToString())
                            {
                                iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                            }
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
    }
}