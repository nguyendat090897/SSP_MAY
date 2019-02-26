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
using System.ComponentModel;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_XuatKho_popup_huy_dieuchinh_phieuxuat.xaml
    /// </summary>
    public partial class frm_XuatKho_popup_huy_dieuchinh_phieuxuat : PopupBase
    {
        #region Khai báo
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper_PHAN_THUC_XUAT = null;
        GridHelper iGridHelper_PHAN_TRA_LAI = null;
        DataTable dt_PHAN_THUC_XUAT = null;
        DataTable dt_PHAN_TRA_LAI = null;
        DataTable dt_KHO = null;
        DataTable dt_KHO_KHUVUC = null;
        DataTable dt_TRANGTHAI = null;
        DataTable dt_KHUVUC = null;
        DataTable dt_VITRIHANG = null;
        DataTable dt_TINHTRANGCV = null;
        DataTable dt_PALLET = null;
        DataTable dt_TEMP = null;
        DataTable dt_TEMP2 = null;
        bool isFirstLoad = false;
        bool isReFresh = false;
        bool isRemove = false;
        string pLOAI_DC = String.Empty;
        string isStatus = String.Empty;
        string isInsertorUpdate = "insert";
        int so_tong, so_thung, so_le = 0;
        IPHIEU_NHAPXUATKHOBussiness _IPHIEU_NHAP_XUAT_KHO;
        #endregion
        #region Method
        public frm_XuatKho_popup_huy_dieuchinh_phieuxuat()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            dt_PHAN_THUC_XUAT = this.TableSchemaDefineBingding_Grid();
            dt_PHAN_TRA_LAI = this.TableSchemaDefineBingding_Grid();
            dt_TEMP = this.TableSchemaDefineBingding_Grid();
            dt_TEMP2 = this.TableSchemaDefineBingding_Grid();
            dt_TEMP2.Clear();
            dt_PHAN_THUC_XUAT.Clear();
            dt_PHAN_TRA_LAI.Clear();
            _IPHIEU_NHAP_XUAT_KHO = new PHIEU_NHAPXUATKHOBussiness(string.Empty);
            this.DataContext = this.iDataSource;
            LoadComboBox();
            Initialize_Grid_PHAN_THUC_XUAT();
            Initialize_Grid_PHAN_TRA_LAI();
            this.iDataSource.Rows[0]["TEN_KHO"] = ConstCommon.pTEN_KHO;
            this.iDataSource.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
            IsDieuChinhOrHuyHoanToan();
           // isFirstLoad = true;
        }
        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                SetIsEnable();
                iRowSelGb = this.Parameter[0] as DataRow;
                LoadComboBox();
                if (iRowSelGb == null)
                { return; }
                DispalayRequest();
                isFirstLoad = true;
                isInsertorUpdate = "update";
                isRemove = false;
                cboNCC.IsEnabled = false;
                txtSOPHIEU.IsEnabled = false;
                rdoDIEU_CHINH.IsEnabled = false;
                rdoHUY_HOAN_TOAN.IsEnabled = false;
                pLOAI_DC = this.iDataSource.Rows[0]["LOAI_DC"].ToString();
                isReFresh = true;
                if (this.Parameter.Count() > 1)
                {
                    Initialize_Grid_PHAN_THUC_XUAT();
                    Initialize_Grid_PHAN_TRA_LAI();
                    btnChonPhieuXuat.Visibility = Visibility.Collapsed;
                    dt_PHAN_THUC_XUAT.Clear();
                    dt_PHAN_TRA_LAI.Clear(); 
                    //--------------loai dieu chinh la "DC"
                    if (pLOAI_DC == "DC")
                    {
                        this.Brd_PHAN_THUC_XUAT.Visibility = Visibility.Visible;
                        txtTHUCXUAT.Visibility = Visibility.Visible;
                        this.grd_PHAN_THUC_XUAT.Visibility = Visibility.Visible;
                        DataRow[] dr_PHAN_THUC_XUAT = this.Parameter[1] as DataRow[];
                        DataRow[] dr_PHAN_TRA_LAI_2 = this.Parameter[3] as DataRow[];
                        if (dr_PHAN_THUC_XUAT.Length > 0)
                        {
                            txtkhonhap.Text = dr_PHAN_THUC_XUAT[0]["TEN_KHO"].ToString();
                            dt_PHAN_THUC_XUAT = dr_PHAN_THUC_XUAT.CopyToDataTable();
                        }
                        if (dr_PHAN_TRA_LAI_2.Length > 0)
                        {
                            dt_PHAN_TRA_LAI = dr_PHAN_TRA_LAI_2.CopyToDataTable();
                        }
                        DataRow[] dr_PHAN_TRA_LAI = this.Parameter[2] as DataRow[];
                        if (dr_PHAN_TRA_LAI.Length > 0)
                        {
                            dt_TEMP = dr_PHAN_TRA_LAI.CopyToDataTable();
                        }
                        rdoDIEU_CHINH.IsChecked = true;
                        if (dt_PHAN_THUC_XUAT != null)
                        {
                            this.iGridHelper_PHAN_THUC_XUAT.BindItemSource(dt_PHAN_THUC_XUAT);
                            if (dt_PHAN_TRA_LAI != null)
                            {
                                this.iGridHelper_PHAN_TRA_LAI.BindItemSource(dt_PHAN_TRA_LAI);
                            }
                        }
                        else
                        {
                            grd_PHAN_THUC_XUAT.ItemsSource = null;
                        }
                    }
                    //-------------end set loai dieu chinh = "DC"
                    //------------- set loai dieu chinh = "HUY"
                    else
                    {
                        Initialize_Grid_PHAN_TRA_LAI();
                        DataRow[] dr_PHAN_TRA_LAI = this.Parameter[2] as DataRow[];
                        if (dr_PHAN_TRA_LAI.Length > 0)
                        {
                            dt_PHAN_TRA_LAI = dr_PHAN_TRA_LAI.CopyToDataTable();
                        }
                        this.Brd_PHAN_THUC_XUAT.Visibility = Visibility.Collapsed;
                        txtTHUCXUAT.Visibility = Visibility.Collapsed;
                        this.grd_PHAN_THUC_XUAT.Visibility = Visibility.Collapsed;
                        rdoHUY_HOAN_TOAN.IsChecked = true;
                        if (dt_PHAN_TRA_LAI.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_PHAN_TRA_LAI.Rows.Count; i++)
                            {
                                dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_TONG"] = ConstCommon.NVL_NUM_NT_NEW(dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_TONG"]) * -1;
                                dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_THUNG"] = ConstCommon.NVL_NUM_NT_NEW(dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_THUNG"]) * -1;
                                dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_LE"] = ConstCommon.NVL_NUM_NT_NEW(dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_LE"]) * -1;
                            }
                            this.iGridHelper_PHAN_TRA_LAI.BindItemSource(dt_PHAN_TRA_LAI);
                        }
                    }
                }
            }
            else
            {
                SetIsEnable();
                rdoDIEU_CHINH.IsEnabled = false;
                rdoHUY_HOAN_TOAN.IsEnabled = false;
                isFirstLoad = false;
                isInsertorUpdate = "insert";
                nbcSearchLOAIDC.Visibility = Visibility.Collapsed;
                btnInMaVach.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Collapsed;

                this.Brd_PHAN_THUC_XUAT.Visibility = Visibility.Visible;
                txtTHUCXUAT.Visibility = Visibility.Visible;
                this.grd_PHAN_THUC_XUAT.Visibility = Visibility.Visible;
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


                xDicUser.Add("LOAI_DC", typeof(string));
                xDicUser.Add("PHIEU_NHAPXUATKHO_DC_ID", typeof(decimal));
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
                xDicUser.Add("QUYCACHDONGGOI", typeof(string));

                xDicUser.Add("SO_LUONG_TRA_LAI", typeof(int));
                xDicUser.Add("SO_LUONG_XUAT_BAN_DAU", typeof(int)); 

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
                xDicUser.Add("SO_LUONG_PHIEU_YEU_CAU", typeof(int));
                xDicUser.Add("THUC_NHAP", typeof(int));

                xDicUser.Add("PHIEU_NHAPXUATKHO_DC_CTIET_ID", typeof(decimal));
                xDicUser.Add("PHIEU_NHAPXUATKHO_DC_ID", typeof(decimal));

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
        /// Radio thay doi chọn hủy hoặc điều chỉnh
        /// </summary>
        private void IsDieuChinhOrHuyHoanToan()
        {

            if (rdoDIEU_CHINH.IsChecked == true)
            {
                pLOAI_DC = "DC";
                this.Brd_PHAN_THUC_XUAT.Visibility = Visibility.Visible;
                txtTHUCXUAT.Visibility = Visibility.Visible;
                this.grd_PHAN_THUC_XUAT.Visibility = Visibility.Visible;
                this.dt_PHAN_TRA_LAI = this.TableSchemaDefineBingding_Grid();
                this.dt_PHAN_TRA_LAI.Clear();
                this.dt_PHAN_TRA_LAI = dt_TEMP2.Copy();
                Initialize_Grid_PHAN_TRA_LAI();
                this.iGridHelper_PHAN_TRA_LAI.BindItemSource(dt_PHAN_TRA_LAI);
            }
            else
            {
                pLOAI_DC = "HUY";
                this.Brd_PHAN_THUC_XUAT.Visibility = Visibility.Collapsed;
                txtTHUCXUAT.Visibility = Visibility.Collapsed;
                this.grd_PHAN_THUC_XUAT.Visibility = Visibility.Collapsed;

                dt_TEMP2.Clear();
                dt_TEMP2 = dt_PHAN_TRA_LAI.Copy();

                this.dt_PHAN_TRA_LAI = this.TableSchemaDefineBingding_Grid();
                this.dt_PHAN_TRA_LAI.Clear();
                this.dt_PHAN_TRA_LAI = this.dt_TEMP.Copy();
                Initialize_Grid_PHAN_TRA_LAI();
                this.iGridHelper_PHAN_TRA_LAI.BindItemSource(dt_PHAN_TRA_LAI);
            }
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
                dt_PALLET = _IPHIEU_NHAP_XUAT_KHO.GetListDM_PALLET(ConstCommon.DonViQuanLy);
                dt_TRANGTHAI = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TINHTRANG_HANG(ConstCommon.DonViQuanLy);
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
                DataTable dtHinhhthucnhap = _IPHIEU_NHAP_XUAT_KHO.getDM_HINHTHU_NHAPXUAT("X");

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
        /// Set so luong tra lai cua tung san pham vào bang phan tra lai
        /// </summary>
        private void SetSoLuongTraLai()
        {
            //if (isReFresh == true)
            //{
            //    dt_PHAN_TRA_LAI.Clear();
            //    isReFresh = false;
            //}
            bool result = true;
            string pma_dviqly = ConstCommon.DonViQuanLy;
            string puser_name = CommonDataHelper.UserName;
            bool iFlag = true;
            DataRowView row = this.grd_PHAN_THUC_XUAT.GetFocusedRow() as DataRowView;
            int so_luong_xuat_ban_dau = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_XUAT_BAN_DAU"]);
            int so_luong_thay_doi_thuc_xuat = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]);
            int so_luong_quy_doi = ConstCommon.NVL_NUM_NT_NEW(row["SOLUONG_QUYDOI"]);
            long don_gia = ConstCommon.NVL_NUM_LONG_NEW(row["DONGIA"]);
            //--- truong hop bang tra lai null
            if (dt_PHAN_TRA_LAI.Rows.Count == 0)
            {
                if (isStatus == "remove")
                {
                    //------set neu insert chi xoa dong hien tai
                    if (isInsertorUpdate == "insert")
                    {
                        dt_PHAN_TRA_LAI.ImportRow(row.Row);
                        dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_TRA_LAI"] = so_luong_xuat_ban_dau;
                        dt_PHAN_TRA_LAI.Rows[0]["THANHTIEN"] =  so_luong_xuat_ban_dau * don_gia; 
                        if (so_luong_quy_doi > 0)
                        {
                            if (so_luong_xuat_ban_dau >= so_luong_quy_doi)
                            {
                                dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_THUNG"] = so_luong_xuat_ban_dau / so_luong_quy_doi;
                                dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = so_luong_xuat_ban_dau % so_luong_quy_doi;
                            }
                            else
                            {
                                dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = so_luong_xuat_ban_dau;
                            }
                        }
                        else
                        {
                            dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_THUNG"] = 0;
                            dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = 0;
                        }
                        this.iGridHelper_PHAN_TRA_LAI.BindItemSource(dt_PHAN_TRA_LAI);
                        dt_PHAN_THUC_XUAT.Rows.Remove(row.Row);
                        this.iGridHelper_PHAN_THUC_XUAT.BindItemSource(dt_PHAN_THUC_XUAT); return;
                    }
                    //------neu update thi xoa ca trong database va dong hien tai
                    else
                    {
                        dt_PHAN_TRA_LAI.ImportRow(row.Row);
                        dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_TRA_LAI"] = so_luong_xuat_ban_dau;
                        dt_PHAN_TRA_LAI.Rows[0]["THANHTIEN"] = so_luong_xuat_ban_dau * don_gia;
                        if (so_luong_quy_doi > 0)
                        {
                            if (so_luong_xuat_ban_dau >= so_luong_quy_doi)
                            {
                                dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_THUNG"] = so_luong_xuat_ban_dau / so_luong_quy_doi;
                                dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = so_luong_xuat_ban_dau % so_luong_quy_doi;
                            }
                            else
                            {
                                dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = so_luong_xuat_ban_dau;
                            }
                        }
                        else
                        {
                            dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_THUNG"] = 0;
                            dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = 0;
                        }
                        this.iGridHelper_PHAN_TRA_LAI.BindItemSource(dt_PHAN_TRA_LAI);
                        result = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_DC_CT_DELETE(pma_dviqly,ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_NHAPXUATKHO_DC_CTIET_ID"]), puser_name);
                        dt_PHAN_THUC_XUAT.Rows.Remove(row.Row);
                        this.iGridHelper_PHAN_THUC_XUAT.BindItemSource(dt_PHAN_THUC_XUAT); return;
                    }
                    //------end set
                }
                dt_PHAN_TRA_LAI.ImportRow(row.Row);
                if (so_luong_thay_doi_thuc_xuat >= so_luong_xuat_ban_dau)
                {
                    dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_TRA_LAI"] = 0;
                    dt_PHAN_TRA_LAI.Rows[0]["THANHTIEN"] = 0 * don_gia;
                    dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_THUNG"] = 0;
                    dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = 0;
                }
                else
                {
                    int so_luong_tong = so_luong_xuat_ban_dau - so_luong_thay_doi_thuc_xuat;
                    dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_TRA_LAI"] = so_luong_tong;
                    dt_PHAN_TRA_LAI.Rows[0]["THANHTIEN"] = so_luong_tong * don_gia;
                    if (so_luong_quy_doi > 0)
                    {
                        if (so_luong_tong >= so_luong_quy_doi)
                        {
                            dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_THUNG"] = so_luong_tong / so_luong_quy_doi;
                            dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = so_luong_tong % so_luong_quy_doi;
                        }
                        else
                        {
                            dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = so_luong_tong;
                        }
                    }
                    else
                    {
                        dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_THUNG"] = 0;
                        dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = 0;
                    }
                }
                this.iGridHelper_PHAN_TRA_LAI.BindItemSource(dt_PHAN_TRA_LAI); return;
            }
            //--- truong hop bang tra lai khac null
            if (dt_PHAN_TRA_LAI.Rows.Count != 0)
            {
                //---- kiem tra va tinh toan co hay chua co san pham i trong bang tra lai?
                for (int i = 0; i < dt_PHAN_TRA_LAI.Rows.Count; i++)
                {
                    // if (ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_NHAPXUATKHO_DC_CTIET_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(dt_PHAN_TRA_LAI.Rows[i]["PHIEU_NHAPXUATKHO_DC_CTIET_ID"]))
                    if (row["KHO_ID"].ToString() == dt_PHAN_TRA_LAI.Rows[i]["KHO_ID"].ToString()
                      && row["KHO_KHUVUC_ID"].ToString() == dt_PHAN_TRA_LAI.Rows[i]["KHO_KHUVUC_ID"].ToString()
                      && row["MA_ITEM_TYPE"].ToString() == dt_PHAN_TRA_LAI.Rows[i]["MA_ITEM_TYPE"].ToString()
                      & row["SANPHAM_ID"].ToString() == dt_PHAN_TRA_LAI.Rows[i]["SANPHAM_ID"].ToString()
                        )
                    {
                        if (isStatus == "remove")
                        {
                            if (isInsertorUpdate == "insert")
                            {
                                dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_TRA_LAI"] = so_luong_xuat_ban_dau;
                                dt_PHAN_TRA_LAI.Rows[i]["THANHTIEN"] = so_luong_xuat_ban_dau * don_gia;
                                if (so_luong_quy_doi > 0)
                                {
                                    if (so_luong_xuat_ban_dau >= so_luong_quy_doi)
                                    {
                                        dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_THUNG"] = so_luong_xuat_ban_dau / so_luong_quy_doi;
                                        dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_LE"] = so_luong_xuat_ban_dau % so_luong_quy_doi;
                                    }
                                    else
                                    {
                                        dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_LE"] = so_luong_xuat_ban_dau;
                                    }
                                }
                                else
                                {
                                    dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_THUNG"] = 0;
                                    dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_LE"] = 0;
                                }
                                this.iGridHelper_PHAN_TRA_LAI.BindItemSource(dt_PHAN_TRA_LAI);
                                dt_PHAN_THUC_XUAT.Rows.Remove(row.Row);
                                this.iGridHelper_PHAN_THUC_XUAT.BindItemSource(dt_PHAN_THUC_XUAT); return;
                            }
                            else
                            {
                                dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_TRA_LAI"] = so_luong_xuat_ban_dau;
                                dt_PHAN_TRA_LAI.Rows[i]["THANHTIEN"] = so_luong_xuat_ban_dau * don_gia;
                                if (so_luong_quy_doi > 0)
                                {
                                    if (so_luong_xuat_ban_dau >= so_luong_quy_doi)
                                    {
                                        dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_THUNG"] = so_luong_xuat_ban_dau / so_luong_quy_doi;
                                        dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_LE"] = so_luong_xuat_ban_dau % so_luong_quy_doi;
                                    }
                                    else
                                    {
                                        dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_LE"] = so_luong_xuat_ban_dau;
                                    }
                                }
                                else
                                {
                                    dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_THUNG"] = 0;
                                    dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_LE"] = 0;
                                }
                                this.iGridHelper_PHAN_TRA_LAI.BindItemSource(dt_PHAN_TRA_LAI);
                                result = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_DC_CT_DELETE(pma_dviqly, ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_NHAPXUATKHO_DC_CTIET_ID"]), puser_name);
                                dt_PHAN_THUC_XUAT.Rows.Remove(row.Row);
                                this.iGridHelper_PHAN_THUC_XUAT.BindItemSource(dt_PHAN_THUC_XUAT); return;
                            }
                        }
                        iFlag = false;
                        if (so_luong_thay_doi_thuc_xuat >= so_luong_xuat_ban_dau)
                        {
                            dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_TRA_LAI"] = 0;
                            dt_PHAN_TRA_LAI.Rows[i]["THANHTIEN"] = 0 * don_gia;
                            dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_THUNG"] = 0;
                            dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_LE"] = 0;
                            break;
                        }
                        else
                        {
                            int so_luong_tong = so_luong_xuat_ban_dau - so_luong_thay_doi_thuc_xuat;
                            dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_TRA_LAI"] = so_luong_tong;
                            dt_PHAN_TRA_LAI.Rows[i]["THANHTIEN"] = so_luong_tong * don_gia;
                            if (so_luong_quy_doi > 0)
                            {
                                if (so_luong_tong >= so_luong_quy_doi)
                                {
                                    dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_THUNG"] = so_luong_tong / so_luong_quy_doi;
                                    dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = so_luong_tong % so_luong_quy_doi;
                                }
                                else
                                {
                                    dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = so_luong_tong;
                                }
                            }
                            else
                            {
                                dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_THUNG"] = 0;
                                dt_PHAN_TRA_LAI.Rows[0]["SO_LUONG_LE"] = 0;
                            }
                            break;
                        }
                    }
                    else
                        iFlag = true;
                }
                //-----end kiem tra
                //-----chua co san pham trong bang tra lai
                if (iFlag == true)
                {
                    dt_PHAN_TRA_LAI.ImportRow(row.Row);
                    int new_row = dt_PHAN_TRA_LAI.Rows.Count - 1;
                    if (isStatus == "remove")
                    {
                        if (isInsertorUpdate == "insert")
                        {
                            dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_TRA_LAI"] = so_luong_xuat_ban_dau;
                            dt_PHAN_TRA_LAI.Rows[new_row]["THANHTIEN"] = so_luong_xuat_ban_dau * don_gia;
                            if (so_luong_quy_doi > 0)
                            {
                                if (so_luong_xuat_ban_dau >= so_luong_quy_doi)
                                {
                                    dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_THUNG"] = so_luong_xuat_ban_dau / so_luong_quy_doi;
                                    dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_LE"] = so_luong_xuat_ban_dau % so_luong_quy_doi;
                                }
                                else
                                {
                                    dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_LE"] = so_luong_xuat_ban_dau;
                                }
                            }
                            else
                            {
                                dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_THUNG"] = 0;
                                dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_LE"] = 0;
                            }
                            this.iGridHelper_PHAN_TRA_LAI.BindItemSource(dt_PHAN_TRA_LAI);
                            dt_PHAN_THUC_XUAT.Rows.Remove(row.Row);
                            this.iGridHelper_PHAN_THUC_XUAT.BindItemSource(dt_PHAN_THUC_XUAT); return;
                        }
                        else
                        {
                            dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_TRA_LAI"] = so_luong_xuat_ban_dau;
                            dt_PHAN_TRA_LAI.Rows[new_row]["THANHTIEN"] = so_luong_xuat_ban_dau * don_gia;
                            if (so_luong_quy_doi > 0)
                            {
                                if (so_luong_xuat_ban_dau >= so_luong_quy_doi)
                                {
                                    dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_THUNG"] = so_luong_xuat_ban_dau / so_luong_quy_doi;
                                    dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_LE"] = so_luong_xuat_ban_dau % so_luong_quy_doi;
                                }
                                else
                                {
                                    dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_LE"] = so_luong_xuat_ban_dau;
                                }
                            }
                            else
                            {
                                dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_THUNG"] = 0;
                                dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_LE"] = 0;
                            }
                            this.iGridHelper_PHAN_TRA_LAI.BindItemSource(dt_PHAN_TRA_LAI);
                            result = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_DC_CT_DELETE(pma_dviqly, ConstCommon.NVL_NUM_LONG_NEW(row["PHIEU_NHAPXUATKHO_DC_CTIET_ID"]), puser_name);
                            dt_PHAN_THUC_XUAT.Rows.Remove(row.Row);
                            this.iGridHelper_PHAN_THUC_XUAT.BindItemSource(dt_PHAN_THUC_XUAT); return;
                        }
                    }
                    else
                    {
                        if (so_luong_thay_doi_thuc_xuat >= so_luong_xuat_ban_dau)
                        {
                            dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_TRA_LAI"] = 0;
                            dt_PHAN_TRA_LAI.Rows[new_row]["THANHTIEN"] = 0 * don_gia;
                            dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_THUNG"] = 0;
                            dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_LE"] = 0;
                        }
                        else
                        {
                            int so_luong_tong = so_luong_xuat_ban_dau - so_luong_thay_doi_thuc_xuat;
                            dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_TRA_LAI"] = so_luong_tong;
                            dt_PHAN_TRA_LAI.Rows[new_row]["THANHTIEN"] = so_luong_tong * don_gia;
                            if (so_luong_quy_doi > 0)
                            {
                                if (so_luong_tong >= so_luong_quy_doi)
                                {
                                    dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_THUNG"] = so_luong_tong / so_luong_quy_doi;
                                    dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_LE"] = so_luong_tong % so_luong_quy_doi;
                                }
                                else
                                {
                                    dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_LE"] = so_luong_tong;
                                }
                            }
                            else
                            {
                                dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_THUNG"] = 0;
                                dt_PHAN_TRA_LAI.Rows[new_row]["SO_LUONG_LE"] = 0;
                            }
                        }
                    }
                }
                this.iGridHelper_PHAN_TRA_LAI.BindItemSource(dt_PHAN_TRA_LAI);
                if (result == true) return;
            }
        }
        /// <summary>
        /// Binding gird
        /// </summary>
        private void Initialize_Grid_PHAN_THUC_XUAT()
        {
            try
            {
                this.iGridHelper_PHAN_THUC_XUAT = new GridHelper(this.grd_PHAN_THUC_XUAT, this.grdView_PHAN_THUC_XUAT, false);
                Column xColumn;
                //Band xBand;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);



                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_MaThuoc"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);



                xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuoc1"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuoc1"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);

       
                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]), new string[] { "TEN_SANPHAM" });
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);

            
                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


                xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);

                xColumn = new Column("SO_LUONG_XUAT_BAN_DAU", "Số lượng tổng xuất ban đầu");
                xColumn.Width = 180;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.Foreground = Brushes.Red;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", "Số lượng điều chỉnh");
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


                xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);

                xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


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
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);//combobox
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["lblChangeStatus"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


                string[] header_kv = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]) };
                string[] id_kv = new string[] { "TEN_KHO_KHUVUC" };
                string[] align_kv = new string[] { "left" };
                xColumn = new Column("KHO_KHUVUC_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("KHO_KHUVUC_ID") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_KHUVUC;
                xColumn.ValueList.DisplayMember = "TEN_KHO_KHUVUC";
                xColumn.ValueList.ValueMember = "KHO_KHUVUC_ID";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_KHUVUC, header_kv, id_kv, align_kv);
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


                xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


                xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; 
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


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
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


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
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);//COMBOBOX
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);//BUTTON
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


                xColumn = new Column("NGAY_SANXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"]));
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);


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
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);//COMBOBOX

                xColumn = new Column("THUC_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThucXuat"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);

                xColumn = new Column("SOLUONG_QUYDOI", "Qui đổi");
                xColumn.Width = 80;
                xColumn.Visible = false;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //----------------------------------------------------------//

                ButtonEditSettings btnThemdong = new ButtonEditSettings();
                btnThemdong.AllowDefaultButton = false;
                ButtonInfo btnAdd = new ButtonInfo();
                btnThemdong.Buttons.Add(btnAdd);
                btnAdd.Click += btnThemdong_Click;
                btnAdd.Content = "Add";

                xColumn = new Column("ADD", Convert.ToString(UtilLanguage.ApplyLanguage()["btnAddgird"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.CustomEditor = btnThemdong;
                xColumn.ColumnType = ColumnControl.Custom;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);

                ButtonEditSettings btnXoadong = new ButtonEditSettings();
                btnXoadong.AllowDefaultButton = false;
                ButtonInfo btn = new ButtonInfo();
                btnXoadong.Buttons.Add(btn);
                btn.Click += btnXoadong_Click;
                btn.Content = "Remove";

                xColumn = new Column("REMOVE", Convert.ToString(UtilLanguage.ApplyLanguage()["btnRemove"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.CustomEditor = btnXoadong;
                xColumn.ColumnType = ColumnControl.Custom;
                this.iGridHelper_PHAN_THUC_XUAT.Add(xColumn);
                //---------------------------------
                //xBand = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["btnThaoTac"]));
                //Band xBand2 = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["btnAddgird"]));
                //Band xBand3 = new Band(Convert.ToString(UtilLanguage.ApplyLanguage()["btnRemove"]));
                //xBand.Items.Add(xBand2);
                //xBand.Items.Add(xBand3);
                //this.iGridHelper_PHAN_THUC_XUAT.Add(xBand);
                //-------------------------------
                var converter = new System.Windows.Media.BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#F2F2F2");

                this.grdView_PHAN_THUC_XUAT.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_THUNG",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.grdView_PHAN_THUC_XUAT.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_LE",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.iGridHelper_PHAN_THUC_XUAT.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid_PHAN_THUC_XUAT()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void Initialize_Grid_PHAN_TRA_LAI()
        {
            try
            {
                this.iGridHelper_PHAN_TRA_LAI = new GridHelper(this.grd_PHAN_TRA_LAI, this.grdView_PHAN_TRA_LAI, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_MaThuoc"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuoc1"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_TenThuoc"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_SoLo"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);


                xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);


                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);
                if (pLOAI_DC == "HUY")
                {
                    xColumn = new Column("SO_LUONG_TONG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_danhsach_loaipallet_SoLuongTong"]));
                    xColumn.Width = 100;
                    xColumn.Visible = true;
                    xColumn.MaskType = MaskType.Numeric;
                    xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                    this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);
                }
                else
                {
                    xColumn = new Column("SO_LUONG_TRA_LAI", "Số lượng tổng trả lại");
                    xColumn.Width = 180;
                    xColumn.Visible = true;
                    xColumn.MaskType = MaskType.Numeric;
                    xColumn.Foreground = Brushes.Red;
                    xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                    this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);
                }
                xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

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
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);//combobox

                string[] header_kv = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]) };
                string[] id_kv = new string[] { "TEN_KHO_KHUVUC" };
                string[] align_kv = new string[] { "left" };
                xColumn = new Column("KHO_KHUVUC_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("KHO_KHUVUC_ID") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_KHUVUC;
                xColumn.ValueList.DisplayMember = "TEN_KHO_KHUVUC";
                xColumn.ValueList.ValueMember = "KHO_KHUVUC_ID";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_KHUVUC, header_kv, id_kv, align_kv);
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; ;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                string[] header_pl = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]) };
                string[] id_pl = new string[] { "TEN_PALLET" };
                string[] align_pl = new string[] { "left" };
                xColumn = new Column("PALLET_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("PALLET_ID") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_PALLET;
                xColumn.ValueList.DisplayMember = "TEN_PALLET";
                xColumn.ValueList.ValueMember = "PALLET_ID";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_PALLET, header_pl, id_pl, align_pl);
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                string[] header_vth = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]) };
                string[] id_vth = new string[] { "VITRI" };
                string[] align_vth = new string[] { "left" };
                xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("VITRI") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_VITRIHANG;
                xColumn.ValueList.DisplayMember = "VITRI";
                xColumn.ValueList.ValueMember = "VITRI";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_VITRIHANG, header_vth, id_vth, align_vth);
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);//COMBOBOX

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);//BUTTON

                xColumn = new Column("NGAY_SANXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);


                string[] header_ttcv = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]) };
                string[] id_ttcv = new string[] { "TEN_TINHTRANG_CV" };
                string[] align_ttcv = new string[] { "left" };
                xColumn = new Column("MA_TINHTRANG_CV", Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.AllowAutoFilter = true;
                xColumn.AllowColumnFiltering = true;
                xColumn.Binding = new System.Windows.Data.Binding("MA_TINHTRANG_CV") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dt_TINHTRANGCV;
                xColumn.ValueList.DisplayMember = "TEN_TINHTRANG_CV";
                xColumn.ValueList.ValueMember = "MA_TINHTRANG_CV";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_TINHTRANGCV, header_ttcv, id_ttcv, align_ttcv);
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);//COMBOBOX

                xColumn = new Column("THUC_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThucXuat"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                xColumn = new Column("SOLUONG_QUYDOI", "Qui đổi");
                xColumn.Width = 80;
                xColumn.Visible = false;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_PHAN_TRA_LAI.Add(xColumn);

                var converter = new System.Windows.Media.BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#F2F2F2");

                this.grdView_PHAN_TRA_LAI.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_THUNG",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.grdView_PHAN_TRA_LAI.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_LE",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.iGridHelper_PHAN_TRA_LAI.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid_PHAN_TRA_LAI()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        ///Set Enable
        /// </summary>
        private void SetIsEnable()
        {
            cboNCC.IsEnabled = false;
            cboDiadiemxuathang.IsEnabled = false;
            txtNGUOILIENHE_A.IsEnabled = false;
            txtSO_CHUNGTU.IsEnabled = false;
            txtNGAY_CHUNGTU.IsEnabled = false;
            txtPHIEUYEUCAU_NHAPKHO_ID.IsEnabled = false;
            txtkhonhap.IsEnabled = false;
            txtNGUOILIENHE_B.IsEnabled = false;
            txtSOPHIEU.IsEnabled = false;
            txtNGAYLAP.IsEnabled = false;
            cbohinhthucnhap.IsEnabled = false;
            cboDonvivanchuyen.IsEnabled = false;
            txtTEN_TAIXE.IsEnabled = false;
            txtSO_DIENTHOAI.IsEnabled = false;
            txtSO_XE.IsEnabled = false;
            txtSO_CONT.IsEnabled = false;
        }
        /// <summary>
        ///Save data
        /// </summary>
        private void SaveData()
        {
            if (pLOAI_DC == "DC")
            {
                if (dt_PHAN_THUC_XUAT.Rows.Count == 0)
                    return;
                //-----set null bang chinh
                if (txtNGAYLAP.Text.Trim() == "")
                {
                    base.ShowMessage(MessageType.Information, "[NGÀY LẬP] phiếu " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    txtNGAYLAP.Focus();
                    return;
                }
                if (cbohinhthucnhap.Text == "--Chọn--")
                {
                    base.ShowMessage(MessageType.Information, "Chưa chọn [HÌNH THỨC XUẤT].");
                    cbohinhthucnhap.Focus();
                    return;
                }
            }
            //-----end
            //-----set null bang chi tiet
            if (pLOAI_DC == "DC")
            {
                for (int i = 0; i < dt_PHAN_THUC_XUAT.Rows.Count; i++)
                {
                    if (ConstCommon.NVL_NUM_NT_NEW(dt_PHAN_THUC_XUAT.Rows[i]["SO_LUONG_TONG"]) == 0)
                    {
                        base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG TỔNG] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                        return;
                    }
                    if (dt_PHAN_THUC_XUAT.Rows[i]["MA_TINHTRANG_HANG"].ToString() == String.Empty)
                    {
                        base.ShowMessage(MessageType.Information, "Chưa chọn [TRẠNG THÁI HÀNG].");
                        return;
                    }
                    if (dt_PHAN_THUC_XUAT.Rows[i]["VITRI"].ToString() == String.Empty)
                    {
                        base.ShowMessage(MessageType.Information, "Chưa chọn [VỊ TRÍ HÀNG].");
                        return;
                    }
                    if (dt_PHAN_THUC_XUAT.Rows[i]["MA_TINHTRANG_CV"].ToString() == String.Empty)
                    {
                        base.ShowMessage(MessageType.Information, "Chưa chọn [TÌNH TRẠNG CV].");
                        return;
                    }
                }
            }
            //-------end
            DataSet ds_return = null;
            if(pLOAI_DC == "HUY") 
            {
                //------insert hoac update phan tra lai
                for(int i = 0;i < dt_PHAN_TRA_LAI.Rows.Count;i++)
                {
                    dt_PHAN_TRA_LAI.Rows[i]["PHIEU_NHAPXUATKHO_DC_ID"] = 0;
                    dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_TONG"] = ConstCommon.NVL_NUM_NT_NEW(dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_TONG"]) * -1;
                    dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_THUNG"] = ConstCommon.NVL_NUM_NT_NEW(dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_THUNG"]) * -1;
                    dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_LE"] = ConstCommon.NVL_NUM_NT_NEW(dt_PHAN_TRA_LAI.Rows[i]["SO_LUONG_LE"]) * -1;
                    dt_PHAN_TRA_LAI.Rows[i]["THANHTIEN"] = ConstCommon.NVL_NUM_NT_NEW(dt_PHAN_TRA_LAI.Rows[i]["THANHTIEN"]) * -1;
                    dt_PHAN_TRA_LAI.Rows[i]["DONGIA"] = ConstCommon.NVL_NUM_NT_NEW(dt_PHAN_TRA_LAI.Rows[i]["DONGIA"]) * -1;
                }
                if (isInsertorUpdate == "insert")//--- neu them moi
                {
                    ds_return = _IPHIEU_NHAP_XUAT_KHO.InsertKO_PHIEU_NHAPXUATKHO_DC(iDataSource, dt_PHAN_TRA_LAI, CommonDataHelper.UserName, pLOAI_DC);

                    if (ds_return != null && (ds_return.Tables.Count > 0))
                    {
                        ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                    }

                    else
                        base.ShowMessage(MessageType.Error, "Lưu không thành công");
                    return;
                }
                else //---------neu update
                {
                    bool result_update_huyhoantoan = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_DC_UPDATE(iDataSource, CommonDataHelper.UserName);
                    if (!result_update_huyhoantoan)
                    {

                        base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                        return;
                    }
                    else
                    {
                        ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                    }
                    return;
                }
            }
            else
            {
                //------insert  hoac update phan thuc xuat
                ds_return = _IPHIEU_NHAP_XUAT_KHO.InsertKO_PHIEU_NHAPXUATKHO_DC(iDataSource, dt_PHAN_THUC_XUAT, CommonDataHelper.UserName, pLOAI_DC);
                if (ds_return != null && (ds_return.Tables.Count > 0))
                {
                    foreach (DataColumn item in ds_return.Tables[0].Columns)
                    {
                        if (this.iDataSource.Columns[item.ColumnName] != null)
                        {
                            this.iDataSource.Rows[0][item.ColumnName] = ds_return.Tables[0].Rows[0][item.ColumnName];
                        }
                    }
                    if (isInsertorUpdate == "insert")
                    {
                        if (dt_TEMP.Rows.Count > 0 && dt_TEMP != null)
                        {
                            //------insert phan tra lai
                            long pPHIEU_NHAPXUATKHO_DC_CTIET_ID = 0;
                            for (int i = 0; i < dt_TEMP.Rows.Count; i++)
                            {
                                dt_TEMP.Rows[i]["PHIEU_NHAPXUATKHO_DC_ID"] = ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_DC_ID"]);
                                dt_TEMP.Rows[i]["SO_LUONG_TONG"] = ConstCommon.NVL_NUM_NT_NEW(dt_TEMP.Rows[i]["SO_LUONG_TONG"]) * -1;
                                dt_TEMP.Rows[i]["SO_LUONG_THUNG"] = ConstCommon.NVL_NUM_NT_NEW(dt_TEMP.Rows[i]["SO_LUONG_THUNG"]) * -1;
                                dt_TEMP.Rows[i]["SO_LUONG_LE"] = ConstCommon.NVL_NUM_NT_NEW(dt_TEMP.Rows[i]["SO_LUONG_LE"]) * -1;
                                pPHIEU_NHAPXUATKHO_DC_CTIET_ID = _IPHIEU_NHAP_XUAT_KHO.InsertorUpdateKO_PHIEU_NHAPXUATKHO_DC_CTIET(dt_TEMP.Rows[i], CommonDataHelper.UserName);
                                if (pPHIEU_NHAPXUATKHO_DC_CTIET_ID <= 0)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                }
                else
                    base.ShowMessage(MessageType.Error, "Lưu không thành công");
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
        private void btnXoadong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.grd_PHAN_THUC_XUAT.GetFocusedRow() == null) return;
                isStatus = "remove";
                SetSoLuongTraLai();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void btnThemdong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isStatus = "add";
                if (this.grd_PHAN_THUC_XUAT.GetFocusedRow() == null) return;
                SetSoLuongTraLai();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveData();
                rdoDIEU_CHINH.IsEnabled = false;
                rdoHUY_HOAN_TOAN.IsEnabled = false;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void grdView_PHAN_THUC_XUAT_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                isStatus = "edit";
                SetSoLuongTraLai();
                int so_luong_quy_doi = 0;
                if (this.grd_PHAN_THUC_XUAT.GetFocusedRow() == null) return;
                DataRowView row = this.grd_PHAN_THUC_XUAT.GetFocusedRow() as DataRowView;
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
        private void grdView_PHAN_THUC_XUAT_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                dt_PHAN_THUC_XUAT.AcceptChanges();
                if (this.grd_PHAN_THUC_XUAT.GetFocusedRow() == null) return;
                if (isRemove == false)
                {
                    int vitri = this.grdView_PHAN_THUC_XUAT.FocusedRowHandle;
                    if (vitri < 0) return;
                    //begin hoapd them vao
                    if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHAN_THUC_XUAT.Rows[vitri]["SOLUONG_QUYDOI"].ToString()) == 0)
                    {
                        //neu la dao tao

                        foreach (Column col in iGridHelper_PHAN_THUC_XUAT.GetColumns)
                        {
                            if (col.Name == "SO_LUONG_THUNG".ToString())
                            {
                                iGridHelper_PHAN_THUC_XUAT.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                            }
                            if (col.Name == "SO_LUONG_LE".ToString())
                            {
                                iGridHelper_PHAN_THUC_XUAT.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                            }
                        }

                        //end hoapd them vao
                    }
                    else
                    {
                        foreach (Column col in iGridHelper_PHAN_THUC_XUAT.GetColumns)
                        {
                            if (col.Name == "SO_LUONG_THUNG".ToString())
                            {
                                iGridHelper_PHAN_THUC_XUAT.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                            }
                            if (col.Name == "SO_LUONG_LE".ToString())
                            {
                                iGridHelper_PHAN_THUC_XUAT.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                            }

                        }
                    }
                    isRemove = false;
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

        }
        private void rdoHUY_HOAN_TOAN_Click(object sender, RoutedEventArgs e)
        {
            IsDieuChinhOrHuyHoanToan();
        }
        private void btnChonPhieuXuat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = null;
                xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_PhieuXuat_DieuChinh_chon_phieu_xuat_popup", null);
                DataSet ds_Return = (DataSet)xReturn;
                if (ds_Return == null) return;
                DataTable dt_PHIEUXUAT_CT_CHON = ds_Return.Tables[1];
                DataTable dt_PHIEUXUAT_CHON = ds_Return.Tables[0];
                //begin tao phieu chi tiet
                if ((dt_PHIEUXUAT_CT_CHON != null) && (dt_PHIEUXUAT_CT_CHON.Rows.Count > 0))
                {
                    if (dt_PHIEUXUAT_CHON.Rows[0]["GHI_CHU_RETURN"].ToString() == "Y")
                    {
                        dt_PHAN_THUC_XUAT = this.TableSchemaDefineBingding_Grid();
                        dt_PHAN_THUC_XUAT.Clear();
                        this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_DC_ID"] = 0;
                        this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"] = dt_PHIEUXUAT_CHON.Rows[0]["PHIEU_NHAPXUATKHO_ID"]; ;
                        this.iDataSource.Rows[0]["MA_DVIQLY"] = dt_PHIEUXUAT_CHON.Rows[0]["MA_DVIQLY"];
                        this.iDataSource.Rows[0]["KHACHHANG_NCC_ID"] = dt_PHIEUXUAT_CHON.Rows[0]["KHACHHANG_NCC_ID"];
                        this.iDataSource.Rows[0]["HOPDONG_ID"] = dt_PHIEUXUAT_CHON.Rows[0]["HOPDONG_ID"];
                        this.iDataSource.Rows[0]["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"] = dt_PHIEUXUAT_CHON.Rows[0]["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"];
                        this.iDataSource.Rows[0]["KHACHHANG_NCC_NOI_XUATHANG_ID"] = dt_PHIEUXUAT_CHON.Rows[0]["KHACHHANG_NCC_NOI_XUATHANG_ID"];
                        this.iDataSource.Rows[0]["NGUOILIENHE_A"] = dt_PHIEUXUAT_CHON.Rows[0]["NGUOILIENHE_A"];
                        this.iDataSource.Rows[0]["NGUOILIENHE_B"] = dt_PHIEUXUAT_CHON.Rows[0]["NGUOILIENHE_B"];
                        this.iDataSource.Rows[0]["SO_CHUNGTU"] = dt_PHIEUXUAT_CHON.Rows[0]["SO_CHUNGTU"];
                        this.iDataSource.Rows[0]["NGAY_CHUNGTU"] = dt_PHIEUXUAT_CHON.Rows[0]["NGAY_CHUNGTU"];
                        this.iDataSource.Rows[0]["MA_HINHTHU_NHAPXUAT"] = dt_PHIEUXUAT_CHON.Rows[0]["MA_HINHTHU_NHAPXUAT"];
                        this.iDataSource.Rows[0]["SOPHIEU"] = dt_PHIEUXUAT_CHON.Rows[0]["SOPHIEU"];
                        this.iDataSource.Rows[0]["NGAYLAP"] = dt_PHIEUXUAT_CHON.Rows[0]["NGAYLAP"];
                        this.iDataSource.Rows[0]["TEN_TAIXE"] = dt_PHIEUXUAT_CHON.Rows[0]["TEN_TAIXE"];
                        this.iDataSource.Rows[0]["SO_DIENTHOAI"] = dt_PHIEUXUAT_CHON.Rows[0]["SO_DIENTHOAI"];
                        this.iDataSource.Rows[0]["SO_XE"] = dt_PHIEUXUAT_CHON.Rows[0]["SO_XE"];
                        this.iDataSource.Rows[0]["SO_CONT"] = dt_PHIEUXUAT_CHON.Rows[0]["SO_CONT"];
                        this.iDataSource.Rows[0]["IS_NHAPNHIEULAN"] = dt_PHIEUXUAT_CHON.Rows[0]["IS_NHAPNHIEULAN"];
                        this.iDataSource.Rows[0]["GHI_CHU"] = string.Empty;
                        for (int i = 0; i < dt_PHIEUXUAT_CT_CHON.Rows.Count; i++)
                        {
                            if (ContainDataRowInDataTable(dt_PHAN_THUC_XUAT, dt_PHIEUXUAT_CT_CHON.Rows[i]) == false)
                            {
                                DataRow dr = dt_PHAN_THUC_XUAT.NewRow();
                                dr["STT"] = dt_PHAN_THUC_XUAT.Rows.Count + 1;
                                dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr["PHIEU_NHAPXUATKHO_ID"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["PHIEU_NHAPXUATKHO_ID"];
                                dr["PHIEU_NHAPXUATKHO_CTIET_ID"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"];
                                dr["KHO_ID"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["KHO_ID"];
                                dr["KHO_KHUVUC_ID"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["KHO_KHUVUC_ID"];
                                dr["SANPHAM_ID"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM_KH"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["MA_SANPHAM_KH"];
                                dr["MA_SANPHAM"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["TEN_SANPHAM"];
                                dr["MA_DONVI_TINH"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["MA_DONVI_TINH"];

                                dr["MA_TINHTRANG_HANG"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["MA_TINHTRANG_HANG"];
                                dr["PALLET_ID"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["PALLET_ID"];
                                dr["VITRI"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["VITRI"];
                                dr["MA_TINHTRANG_CV"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["MA_TINHTRANG_CV"];

                                dr["MA_ITEM_TYPE"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["QUYCACHDONGGOI"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["QUYCACHDONGGOI"];
                                dr["SOLO"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["SOLO"];
                                dr["HANDUNG"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["HANDUNG"];
                                dr["SO_LUONG_PHIEU_YEU_CAU"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["SO_LUONG_PHIEU_YEU_CAU"];
                                dr["SO_LUONG_TONG"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["SO_LUONG_TONG"];
                                dr["SO_LUONG_XUAT_BAN_DAU"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["SO_LUONG_XUAT_BAN_DAU"];
                                dr["SO_LUONG_THUNG"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["SO_LUONG_THUNG"];
                                dr["SO_LUONG_LE"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["SO_LUONG_LE"];
                                dr["SOLUONG_QUYDOI"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["SOLUONG_QUYDOI"];
                                dr["DONGIA"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["DONGIA"];
                                dr["MA_VACH"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["MA_VACH"];
                                dr["NGAY_SANXUAT"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["NGAY_SANXUAT"];
                                dr["SODK"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["SODK"];
                                dr["THANHTIEN"] = dt_PHIEUXUAT_CT_CHON.Rows[i]["THANHTIEN"];
                                dr["PHIEU_NHAPXUATKHO_DC_CTIET_ID"] = 0; 
                                dt_PHAN_THUC_XUAT.Rows.Add(dr);
                            }
                        }
                    }
                }
                if (dt_PHAN_THUC_XUAT != null)
                {
                    nbcSearchLOAIDC.Visibility = Visibility.Visible;
                    btnInMaVach.Visibility = Visibility.Visible;
                    btnSave.Visibility = Visibility.Visible;
                    this.iGridHelper_PHAN_THUC_XUAT.BindItemSource(dt_PHAN_THUC_XUAT);
                    rdoDIEU_CHINH.IsEnabled = true;
                    rdoHUY_HOAN_TOAN.IsEnabled = true;
                    rdoDIEU_CHINH.IsChecked = true;
                    IsDieuChinhOrHuyHoanToan();
                    this.dt_TEMP = dt_PHAN_THUC_XUAT.Copy();
                    this.dt_PHAN_TRA_LAI.Clear();
                }
                else
                {
                    nbcSearchLOAIDC.Visibility = Visibility.Collapsed;
                    btnInMaVach.Visibility = Visibility.Collapsed;
                    btnSave.Visibility = Visibility.Collapsed;
                    grd_PHAN_THUC_XUAT.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonPhieuXuat.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void rdoDIEU_CHINH_Click(object sender, RoutedEventArgs e)
        {
            IsDieuChinhOrHuyHoanToan();
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
    } 
}