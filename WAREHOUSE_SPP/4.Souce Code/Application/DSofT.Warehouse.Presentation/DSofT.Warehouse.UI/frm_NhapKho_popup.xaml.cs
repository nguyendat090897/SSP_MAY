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
using DSofT.Framework.Client.ExportHelper;
using Microsoft.Win32;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;


namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_NhapKho_popup.xaml
    /// </summary>
    public partial class frm_NhapKho_popup : PopupBase
    {
        #region Khai báo
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        DataTable dt_PHIEU_CTIET = null;
        DataTable dt_KHO = null;
        DataTable dt_KHO_KHUVUC = null;
        DataTable dt_TRANGTHAI = null;
        DataTable dt_KHUVUC = null;
        DataTable dt_VITRIHANG = null;
        DataTable dt_TINHTRANGCV = null;
        DataTable dt_PALLET = null;
        bool isFirstLoad = false;
        bool isSecondLoad = false;
        bool isEndLoad = false;
        string isKiemKe = String.Empty;
        int so_tong, so_thung, so_le = 0;
        IPHIEU_NHAPXUATKHOBussiness _IPHIEU_NHAP_XUAT_KHO;
        public static long pPHIEU_XUATNHAP_KHO_ID = 0;
        public static string pN_OR_N = "N";
        string ngayPhieu;
        decimal tongTT;
        public static bool status;
        DataSet ds_ctiet_vitri = null;
        DataTable tbl = new DataTable();
        #endregion
        #region Method
        public frm_NhapKho_popup()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            dt_PHIEU_CTIET = this.TableSchemaDefineBingding_Grid();
            tbl = this.MA_VACH();
            dt_PHIEU_CTIET.Clear();
            _IPHIEU_NHAP_XUAT_KHO = new PHIEU_NHAPXUATKHOBussiness(string.Empty);
            this.DataContext = this.iDataSource;
            LoadComboBox();
            Initialize_Grid_CTPHIEUNHAP();
            this.iDataSource.Rows[0]["TEN_KHO"] = ConstCommon.pTEN_KHO;
            this.iDataSource.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
            KiemTraNhapNhieuLan();
            chkIS_NHAPNHIEULAN.IsEnabled = false;
            isSecondLoad = false;
            isEndLoad = true;
            //IsVisibleSoLuongYeuCau();
            btnBoGhiSoAll.Visibility = Visibility.Collapsed;
            btnGhiSoAll.Visibility = Visibility.Collapsed;
          //  grd.Columns["GHI_SO"].Visible = false;
          //  grd.Columns["BO_GHI_SO"].Visible = false;
        }
        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                if (this.Parameter.Count() > 2)
                {
                    isKiemKe = this.Parameter[3].ToString();
                }               
                if (isKiemKe == "KK")
                {
                    btnGhiSoAll.Visibility = Visibility.Collapsed;
                    btnBoGhiSoAll.Visibility = Visibility.Collapsed;
                    grd.Columns["GHI_SO"].Visible = false;
                    grd.Columns["BO_GHI_SO"].Visible = false;
                    this.iDataSource.Rows[0]["PHIEU_KIEMKE_ID"] = this.Parameter[1].ToString();
                    DataRow[] dr_chitiet = this.Parameter[0] as DataRow[];
                    if (dr_chitiet.Length == 0)
                    {
                        pN_OR_N = this.Parameter[1].ToString();
                    }
                    else
                    {
                        pN_OR_N = this.Parameter[2].ToString();
                    }
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
                        btnNhapHangNhieuLan.Tag = "Xuất hàng nhiều lần";
                        lblNCC.Text = "Khách hàng";
                        lbldiadiemxuathang.Text = "Địa chỉ nhận hàng";
                    }
                    LoadComboBox();
                    dt_PHIEU_CTIET.Clear();
                    if (dr_chitiet.Length > 0)
                    {
                        isSecondLoad = true;
                        //IsVisibleSoLuongYeuCau();
                        txtkhonhap.Text = dr_chitiet[0]["TEN_KHO"].ToString();
                        dt_PHIEU_CTIET = dr_chitiet.CopyToDataTable();
                    }
                    if (dt_PHIEU_CTIET != null)
                    {
                        
                        this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                    }
                    else
                    {
                        isSecondLoad = false;
                        grd.ItemsSource = null;
                    }
                }
                else
                {

                    iRowSelGb = this.Parameter[0] as DataRow;
                    if (iRowSelGb == null)
                    {
                        pN_OR_N = this.Parameter[1].ToString();
                        ds_ctiet_vitri = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_ALL(ConstCommon.DonViQuanLy, 0);
                    }
                    else
                    {
                        ds_ctiet_vitri = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_ALL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_ID"]));
                        //pN_OR_N = this.Parameter[2].ToString();
                    }
                    LoadComboBox();
                    Initialize_Grid_CTPHIEUNHAP();
                    if (ds_ctiet_vitri.Tables[0].Rows.Count <= 0)
                    {
                        //grd.Columns["GHI_SO"].Visible = false;
                        //grd.Columns["BO_GHI_SO"].Visible = false;
                    }
                    else
                    {
                        //grd.Columns["GHI_SO"].Visible = true;
                        //grd.Columns["BO_GHI_SO"].Visible = true;
                    }
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
                        btnNhapHangNhieuLan.Tag = "Xuất hàng nhiều lần";
                        lblNCC.Text = "Khách hàng";
                        lbldiadiemxuathang.Text = "Địa chỉ nhận hàng";
                    }
                    isEndLoad = false;
                    if (iRowSelGb == null)
                    { return; }
                    DispalayRequest();
                    txtID_PHIEU_YC.Text = this.iDataSource.Rows[0]["SOPHIEU_YEUCAU"].ToString();
                    pPHIEU_XUATNHAP_KHO_ID = ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"].ToString());
                    isFirstLoad = true;
                    if (this.Parameter.Count() > 1)
                    {
                        UpdateNhapNhieuLan();
                        DataRow[] dr_chitiet = this.Parameter[1] as DataRow[];
                        dt_PHIEU_CTIET.Clear();
                        if (dr_chitiet.Length > 0)
                        {
                            isSecondLoad = true;
                            //IsVisibleSoLuongYeuCau();
                            txtkhonhap.Text = dr_chitiet[0]["TEN_KHO"].ToString();
                            dt_PHIEU_CTIET = dr_chitiet.CopyToDataTable();
                        }
                        //if (dt_PHIEU_CTIET != null)
                        //{
                        //    bool flag = false;
                        //    for(int i = 0; i< dt_PHIEU_CTIET.Rows.Count;i++)
                        //    {
                        //        if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["DA_GHISO"]) == 0)
                        //        {
                        //            flag = false;
                        //            break;
                        //        }
                        //        else
                        //            flag = true;
                        //    }
                        //    if(flag == true)
                        //    {
                        //        btnGhiSoAll.Visibility = Visibility.Collapsed;
                        //        btnBoGhiSoAll.Visibility = Visibility.Visible;
                        //    }
                        //    else
                        //    {
                        //        btnGhiSoAll.Visibility = Visibility.Visible;
                        //        btnBoGhiSoAll.Visibility = Visibility.Visible;
                        //    }
                        //    this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                        //}
                        //else
                        //{
                        //    isSecondLoad = false;
                        //    grd.ItemsSource = null;
                        //}
                    }
                }
            }
            else
            {
                //if (pN_OR_N == "N")
                //{
                //    grd.Columns["GHI_SO"].Visible = false;
                //    grd.Columns["BO_GHI_SO"].Visible = false;
                //}
                //btnBoGhiSoAll.Visibility = Visibility.Collapsed;
                //btnGhiSoAll.Visibility = Visibility.Collapsed;
            }
            this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
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
                xDicUser.Add("HOPDONG_ID", typeof(string));
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
                xDicUser.Add("CreateDate", typeof(DateTime));
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
                xDicUser.Add("CHON", typeof(int));
                xDicUser.Add("STT", typeof(int));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEU_NHAPXUATKHO_CTIET_ID", typeof(decimal));
                xDicUser.Add("PHIEU_NHAPXUATKHO_ID", typeof(decimal));
                xDicUser.Add("PHIEU_NHAPXUATKHO_CTIET_VTRI_ID", typeof(int));
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
                xDicUser.Add("DA_GHISO", typeof(bool));
                xDicUser.Add("KHO_VITRI_ID", typeof(decimal));
                xDicUser.Add("MA_SANPHAM_KH", typeof(string));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string)); 

                xDicUser.Add("SO_LUONG", typeof(int));
                xDicUser.Add("SO_LUONG_TON", typeof(int));
                xDicUser.Add("SO_LUONG_TONG_THUCNHAP", typeof(int));
                xDicUser.Add("SO_LUONG_THUNG_THUCNHAP", typeof(int));
                xDicUser.Add("SO_LUONG_LE_THUCNHAP", typeof(int));
                xDicUser.Add("SOLUONG_QUYDOI", typeof(int)); 

                xDicUser.Add("GHI_SO", typeof(string)); 
                xDicUser.Add("BO_GHI_SO", typeof(string));
                xDicUser.Add("IS_XUAT", typeof(bool));
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
        /// Binding dữ liệu vào combobox
        /// </summary>
        private void LoadComboBox()
        {
            try
            {
                dt_KHO = _IPHIEU_NHAP_XUAT_KHO.GetListDM_KHO(ConstCommon.DonViQuanLy);
                dt_KHO_KHUVUC = _IPHIEU_NHAP_XUAT_KHO.DM_KHO_KHUVUC_GET_LIST_BY_KHO(ConstCommon.DonViQuanLy, 0);
                dt_PALLET = _IPHIEU_NHAP_XUAT_KHO.GetListDM_PALLET(ConstCommon.DonViQuanLy);
                //dt_TRANGTHAI = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TINHTRANG_HANG(ConstCommon.DonViQuanLy);
                dt_KHUVUC = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TENKHO_KHUVUC(ConstCommon.DonViQuanLy);
                dt_TINHTRANGCV = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TINHTRANG_CV(ConstCommon.DonViQuanLy);
                dt_VITRIHANG = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_VITRI_HANG(ConstCommon.DonViQuanLy, ConstCommon.pKHO_ID);


                DataTable dt_PHIEU_KIEM_KE = _IPHIEU_NHAP_XUAT_KHO.GetListSO_PHIEU_KIEM_KE(ConstCommon.DonViQuanLy);
                if (dt_PHIEU_KIEM_KE != null && dt_PHIEU_KIEM_KE.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboPHIEU_KIEM_KE, "SOPHIEU", "PHIEU_KIEMKE_ID", dt_PHIEU_KIEM_KE, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboPHIEU_KIEM_KE, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["PHIEU_KIEMKE_ID"] = cboPHIEU_KIEM_KE.GetKeyValue(0);
                }

                DataTable dtNCC = _IPHIEU_NHAP_XUAT_KHO.GetData_For_Cbo_KhachHang_Ncc(ConstCommon.DonViQuanLy);
                if (dtNCC != null && dtNCC.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboNCC, "TEN_KH", "KHACHHANG_NCC_ID", dtNCC, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboNCC, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["KHACHHANG_NCC_ID"] = cboNCC.GetKeyValue(0);
                }
                DataTable dtHinhhthucnhap;
                if (pN_OR_N=="N")
                {
                     dtHinhhthucnhap = _IPHIEU_NHAP_XUAT_KHO.getDM_HINHTHU_NHAPXUAT("N");
                }
                else
                {
                    dtHinhhthucnhap = _IPHIEU_NHAP_XUAT_KHO.getDM_HINHTHU_NHAPXUAT("X");
                }

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
                if (ConstCommon.check_String_Unicode(txtSO_CONT.Text.Trim()) == false)
                {
                    base.ShowMessage(MessageType.Information, "[SỐ CONTS] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteSYMBOL"]));
                    txtSO_CONT.Focus();
                    return false;
                }
                if (txtNGAYLAP.Text.Trim() == "")
                {
                    base.ShowMessage(MessageType.Information, "[NGÀY LẬP] phiếu " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    txtNGAYLAP.Focus();
                    return false;
                }
                if (pN_OR_N == "N")
                {
                    if (cbohinhthucnhap.Text == "--Chọn--")
                    {
                        base.ShowMessage(MessageType.Information, "Chưa chọn [HÌNH THỨC NHẬP].");
                        cbohinhthucnhap.Focus();
                        return false;
                    }
                }
                else
                {
                    if (cbohinhthucnhap.Text == "--Chọn--")
                    {
                        base.ShowMessage(MessageType.Information, "Chưa chọn [HÌNH THỨC XUẤT].");
                        cbohinhthucnhap.Focus();
                        return false;
                    }
                }
                if (_IPHIEU_NHAP_XUAT_KHO.KO_NHAPXUATKHO_CHECKEXISTS(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(txtPHIEUYEUCAU_NHAPKHO_ID.Text), txtSOPHIEU.Text.Trim()) == true)
                {
                    base.ShowMessage(MessageType.Information, "Số phiếu đã bị trùng, xin vui lòng nhập số khác ");
                    return false;
                }
                else
                {
                    if (dt_PHIEU_CTIET.Rows.Count == 0)
                    {
                        if (pN_OR_N == "N")
                        {
                            base.ShowMessage(MessageType.Information, "[CHI TIẾT PHIẾU NHẬP HÀNG] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                            return false;
                        }
                        else
                        {
                            base.ShowMessage(MessageType.Information, "[CHI TIẾT PHIẾU XUẤT HÀNG] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                            return false;
                        }
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
        /// <summary>
        /// Tìm kiếm tự động trên gird
        /// </summary>
        private bool ContainDataRowInDataTable(DataTable dt, DataRow dr)
        {
            try
            {
                if (pN_OR_N == "X")
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
                else
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
            }
            catch
            {

                return false;
            }
        }
        /// <summary>
        /// Set, get checkbox nhập nhiều lần
        /// </summary>
        private void KiemTraNhapNhieuLan()
        {
            if (chkIS_NHAPNHIEULAN.IsChecked == true)
            {
                this.iDataSource.Rows[0]["IS_NHAPNHIEULAN"] = 1;
                btnNhapHangNhieuLan.Visibility = Visibility.Visible;
            }
            else
            {
                this.iDataSource.Rows[0]["IS_NHAPNHIEULAN"] = 0;
                btnNhapHangNhieuLan.Visibility = Visibility.Hidden;
            }
        }
        private void UpdateNhapNhieuLan()
        {
            if (this.iDataSource.Rows[0]["IS_NHAPNHIEULAN"].ToString() == "True")
            {
                chkIS_NHAPNHIEULAN.IsChecked = true;
                btnNhapHangNhieuLan.Visibility = Visibility.Visible;
            }
            else
            {
                chkIS_NHAPNHIEULAN.IsChecked = false;
                btnNhapHangNhieuLan.Visibility = Visibility.Hidden;
            }
        }
        //private void IsVisibleSoLuongYeuCau()
        //{
        //    if (isSecondLoad == true)
        //    {
        //        grd.Columns["SO_LUONG_PHIEU_YEU_CAU"].Visible = true;
        //    }
        //    else
        //    {
        //        grd.Columns["SO_LUONG_PHIEU_YEU_CAU"].Visible = false;
        //    }
        //}
        /// <summary>
        /// Binding gird
        /// </summary>
        private void Initialize_Grid_CTPHIEUNHAP()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;

                xColumn = new Column("CHON", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_Chon"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("CHON") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                xColumn.AllowSorting = false;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
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

                //xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaThuoc1"]));
                //xColumn.Width = 150;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

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


                //xColumn = new Column("HANDUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_HanDung"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                //xColumn.Width = 150;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //this.iGridHelper.Add(xColumn);


                //xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_DonViTinh"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);
                //if (pN_OR_N == "X")
                //{
                //    xColumn = new Column("SO_LUONG_TON", "Số lượng tồn");
                //    xColumn.Width = 100;
                //    xColumn.Visible = true;
                //    xColumn.Foreground = Brushes.Red;
                //    xColumn.MaskType = MaskType.Numeric;
                //    xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                //    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                //    this.iGridHelper.Add(xColumn);
                //}
                //xColumn = new Column("SO_LUONG_PHIEU_YEU_CAU", "Số lượng phiếu yêu cầu");
                //xColumn.Width = 150;
                //xColumn.Visible = true;
                //xColumn.MaskType = MaskType.Numeric;
                //xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                //this.iGridHelper.Add(xColumn);

                if (pN_OR_N == "N")
                {
                    xColumn = new Column("SO_LUONG_TONG", "Số lượng tổng");
                }
                else
                {
                    xColumn = new Column("SO_LUONG_TONG", "Số lượng tổng");
                }
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                this.iGridHelper.Add(xColumn);
                //if (isKiemKe != "KK")
                //{
                //    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //}
                //else
                //    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("SO_LUONG_THUNG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoThung"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.MaskType = MaskType.Numeric;
                //xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("SO_LUONG_LE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLe"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.MaskType = MaskType.Numeric;
                //xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                //this.iGridHelper.Add(xColumn);

                //string[] header_tt = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblChangeStatus"]) };
                //string[] id_tt = new string[] { "TEN_TINHTRANG_HANG" };
                //string[] align_tt = new string[] { "left" };
                //xColumn = new Column("MA_TINHTRANG_HANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChangeStatus"]));
                //xColumn.Width = 150;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //xColumn.ColumnType = ColumnControl.LookUpEdit;
                //xColumn.AllowAutoFilter = true;
                //xColumn.AllowColumnFiltering = true;
                //xColumn.Binding = new System.Windows.Data.Binding("MA_TINHTRANG_HANG") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                //xColumn.ValueList.DataSource = dt_TRANGTHAI;
                //xColumn.ValueList.DisplayMember = "TEN_TINHTRANG_HANG";
                //xColumn.ValueList.ValueMember = "MA_TINHTRANG_HANG";
                //xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_TRANGTHAI, header_tt, id_tt, align_tt);
                //this.iGridHelper.Add(xColumn);//combobox

                //string[] header_kv = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]) };
                //string[] id_kv = new string[] { "TEN_KHO_KHUVUC" };
                //string[] align_kv = new string[] { "left" };
                //xColumn = new Column("KHO_KHUVUC_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblKhuVuc"]));
                //xColumn.Width = 150;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //xColumn.ColumnType = ColumnControl.LookUpEdit;
                //xColumn.AllowAutoFilter = true;
                //xColumn.AllowColumnFiltering = true;
                //xColumn.Binding = new System.Windows.Data.Binding("KHO_KHUVUC_ID") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                //xColumn.ValueList.DataSource = dt_KHUVUC;
                //xColumn.ValueList.DisplayMember = "TEN_KHO_KHUVUC";
                //xColumn.ValueList.ValueMember = "KHO_KHUVUC_ID";
                //xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_KHUVUC, header_kv, id_kv, align_kv);
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.MaskType = MaskType.Numeric;
                //xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //this.iGridHelper.Add(xColumn);

                //xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                //xColumn.Width = 120;
                //xColumn.Visible = true;
                //xColumn.MaskType = MaskType.Numeric;
                //xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; ;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);

                //string[] header_pl = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]) };
                //string[] id_pl = new string[] { "TEN_PALLET" };
                //string[] align_pl = new string[] { "left" };
                //xColumn = new Column("PALLET_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //xColumn.ColumnType = ColumnControl.LookUpEdit;
                //xColumn.AllowAutoFilter = true;
                //xColumn.AllowColumnFiltering = true;
                //xColumn.Binding = new System.Windows.Data.Binding("PALLET_ID") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                //xColumn.ValueList.DataSource = dt_PALLET;
                //xColumn.ValueList.DisplayMember = "TEN_PALLET";
                //xColumn.ValueList.ValueMember = "PALLET_ID";
                //xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_PALLET, header_pl, id_pl, align_pl);
                //this.iGridHelper.Add(xColumn);

                //string[] header_vth = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]) };
                //string[] id_vth = new string[] { "VITRI" };
                //string[] align_vth = new string[] { "left" };
                //xColumn = new Column("VITRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTriHang"]));
                //xColumn.Width = 100;
                //if (pN_OR_N == "X")
                //{
                //    xColumn.Visible = true;
                //}
                //else
                //{
                //    xColumn.Visible = false;
                //}
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //xColumn.ColumnType = ColumnControl.LookUpEdit;
                //xColumn.AllowAutoFilter = true;
                //xColumn.AllowColumnFiltering = true;
                //xColumn.Binding = new System.Windows.Data.Binding("VITRI") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                //xColumn.ValueList.DataSource = dt_VITRIHANG;
                //xColumn.ValueList.DisplayMember = "VITRI";
                //xColumn.ValueList.ValueMember = "VITRI";
                //xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_VITRIHANG, header_vth, id_vth, align_vth);
                //this.iGridHelper.Add(xColumn);//COMBOBOX

                //xColumn = new Column("MA_VACH", "Mã vạch");
                //xColumn.Width = 80;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //this.iGridHelper.Add(xColumn);//BUTTON

                //xColumn = new Column("NGAY_SANXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"]));
                //xColumn.Width = 100;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //this.iGridHelper.Add(xColumn);


                //string[] header_ttcv = new string[] { Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]) };
                //string[] id_ttcv = new string[] { "TEN_TINHTRANG_CV" };
                //string[] align_ttcv = new string[] { "left" };
                //xColumn = new Column("MA_TINHTRANG_CV", Convert.ToString(UtilLanguage.ApplyLanguage()["lbTinhTrangCV"]));
                //xColumn.Width = 150;
                //xColumn.Visible = true;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //xColumn.ColumnType = ColumnControl.LookUpEdit;
                //xColumn.AllowAutoFilter = true;
                //xColumn.AllowColumnFiltering = true;
                //xColumn.Binding = new System.Windows.Data.Binding("MA_TINHTRANG_CV") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                //xColumn.ValueList.DataSource = dt_TINHTRANGCV;
                //xColumn.ValueList.DisplayMember = "TEN_TINHTRANG_CV";
                //xColumn.ValueList.ValueMember = "MA_TINHTRANG_CV";
                //xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dt_TINHTRANGCV, header_ttcv, id_ttcv, align_ttcv);
                //this.iGridHelper.Add(xColumn);//COMBOBOX

                //if (pN_OR_N == "N")
                //{
                //    xColumn = new Column("THUC_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThucNhap"]));
                //    xColumn.Width = 80;
                //    xColumn.Visible = true;
                //    xColumn.MaskType = MaskType.Numeric;
                //    xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                //    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //    this.iGridHelper.Add(xColumn);

                //    xColumn = new Column("IS_NHAPNHIEULAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNhapNhieuLan"]));
                //    xColumn.Width = 100;
                //    xColumn.Visible = true;
                //    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //    xColumn.ColumnType = ColumnControl.Checkbox;
                //    xColumn.Binding = new Binding("IS_NHAPNHIEULAN") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                //    this.iGridHelper.Add(xColumn);//CHECKBOX
                //}
                //else
                //{
                //    xColumn = new Column("THUC_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThucXuat"]));
                //    xColumn.Width = 80;
                //    xColumn.Visible = true;
                //    xColumn.MaskType = MaskType.Numeric;
                //    xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                //    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //    this.iGridHelper.Add(xColumn);

                //    xColumn = new Column("IS_NHAPNHIEULAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblXuatNhieuLan"]));
                //    xColumn.Width = 100;
                //    xColumn.Visible = true;
                //    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //    xColumn.ColumnType = ColumnControl.Checkbox;
                //    xColumn.Binding = new Binding("IS_NHAPNHIEULAN") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                //    this.iGridHelper.Add(xColumn);//CHECKBOX
                //}
                //if (isKiemKe != "KK")
                //{
                //    ButtonEditSettings btnGhiSo = new ButtonEditSettings();
                //    btnGhiSo.AllowDefaultButton = false;
                //    ButtonInfo btnAdd = new ButtonInfo();
                //    btnGhiSo.Buttons.Add(btnAdd);
                //    btnAdd.Click += btnGhiSo_Click;
                //    btnAdd.Content = "Ghi sổ";

                //    //xColumn = new Column("GHI_SO", "Ghi sổ");
                //    //xColumn.Width = 100;
                //    //xColumn.Visible = true;
                //    //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                //    //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //    //xColumn.CustomEditor = btnGhiSo;
                //    //xColumn.ColumnType = ColumnControl.Custom;
                //    //this.iGridHelper.Add(xColumn);

                //    ButtonEditSettings btnBoGhiSo = new ButtonEditSettings();
                //    btnBoGhiSo.AllowDefaultButton = false;
                //    ButtonInfo btnRemove = new ButtonInfo();
                //    btnBoGhiSo.Buttons.Add(btnRemove);
                //    btnRemove.Click += btnBoGhiSo_Click;
                //    btnRemove.Content = "Bỏ ghi sổ";

                //    xColumn = new Column("BO_GHI_SO", "Bỏ ghi sổ");
                //    xColumn.Width = 100;
                //    xColumn.Visible = true;
                //    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                //    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //    xColumn.CustomEditor = btnBoGhiSo;
                //    xColumn.ColumnType = ColumnControl.Custom;
                //    this.iGridHelper.Add(xColumn);
                //}

                //xColumn = new Column("SOLUONG_QUYDOI", "Qui đổi");
                //xColumn.Width = 80;
                //xColumn.Visible = false;
                //xColumn.MaskType = MaskType.Numeric;
                //xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                //xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //this.iGridHelper.Add(xColumn);

                //var converter = new System.Windows.Media.BrushConverter();
                //var brush = (Brush)converter.ConvertFromString("#F2F2F2");

                //this.grdView.AddFormatCondition(new FormatCondition()
                //{
                //    FieldName = "SO_LUONG_PHIEU_YEU_CAU",
                //    Format = new Format() { Foreground = Brushes.Red },
                //    Expression = "[SO_LUONG_PHIEU_YEU_CAU] != null",
                //    ApplyToRow = false
                //});
                //this.grdView.AddFormatCondition(new FormatCondition()
                //{
                //    FieldName = "SO_LUONG_THUNG",
                //    Format = new Format() { Background = brush },
                //    Expression = "[SOLUONG_QUYDOI] == null", // #
                //    ApplyToRow = false
                //});

                //this.grdView.AddFormatCondition(new FormatCondition()
                //{
                //    FieldName = "SO_LUONG_LE",
                //    Format = new Format() { Background = brush },
                //    Expression = "[SOLUONG_QUYDOI] == null", // #
                //    ApplyToRow = false
                //});
                //this.grdView.AddFormatCondition(new FormatCondition()
                //{
                //    FieldName = "GHI_SO",
                //    Format = new Format() { Background = brush },
                //    Expression = "[DA_GHISO] == 1", // #
                //    ApplyToRow = false
                //});
                this.iGridHelper.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void SetIsNhapNhieuLan()
        {
            DataRowView row = this.grd.GetFocusedRow() as DataRowView;
            if (isSecondLoad == true)
            {
                if (isKiemKe != "KK")
                {
                    for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                    {
                        if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_PHIEU_YEU_CAU"]) > ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG"]))
                        {
                            chkIS_NHAPNHIEULAN.IsChecked = true;
                            KiemTraNhapNhieuLan();
                            break;
                        }
                        else
                        {
                            chkIS_NHAPNHIEULAN.IsChecked = false;
                            KiemTraNhapNhieuLan();
                        }
                    }
                    for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                    {
                        if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_PHIEU_YEU_CAU"]) > ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG"]))
                        {
                            dt_PHIEU_CTIET.Rows[i]["IS_NHAPNHIEULAN"] = 1;
                        }
                        else
                        {
                            dt_PHIEU_CTIET.Rows[i]["IS_NHAPNHIEULAN"] = 0;
                        }
                    }
                }
            }
        }

        #endregion
        #region UI
        private void btnGhiSo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool msg = false;
                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;
                if (ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_NHAPXUATKHO_CTIET_ID"]) > 0)
                {
                    msg = (bool)base.ShowMessage(MessageType.OkCancel, "Bạn có muốn ghi sổ sản phẩm này không?");
                    if (msg == true)
                    {
                        if(ConstCommon.NVL_NUM_LONG_NEW(row.Row["DA_GHISO"]) > 0)
                        {
                            base.ShowMessage(MessageType.Information, "Sản phẩm này đã được ghi sổ.");
                            return;
                        }
                        int so_luong_luc_nhap = 0;
                        int so_luong_da_xep = 0;
                        //MỚI CHỈNH SỬA
                        if (pN_OR_N == "N")
                        {
                            for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                            {
                                if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_NHAPXUATKHO_CTIET_ID"])
                                    && ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["SANPHAM_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(row.Row["SANPHAM_ID"])
                                    )
                                {
                                    so_luong_luc_nhap += ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG"]);
                                }
                            }
                        }
                        bool is_ghiso = false;
                        DateTime pCreatedDate = Convert.ToDateTime(this.iDataSource.Rows[0]["CreateDate"].ToString());
                        DataSet ds_ctiet_sanpham = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_BY_PHIEU(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_ID"]), ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_NHAPXUATKHO_CTIET_ID"]), ConstCommon.NVL_NUM_LONG_NEW(row.Row["SANPHAM_ID"]));
                        DataTable dt = ds_ctiet_sanpham.Tables[0];
                        //MỚI CHỈNH SỬA
                        if (pN_OR_N == "N")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                so_luong_da_xep += ConstCommon.NVL_NUM_NT_NEW(dt.Rows[i]["SO_LUONG_TONG"]);
                            }
                            if (so_luong_luc_nhap > so_luong_da_xep)
                            {
                                base.ShowMessage(MessageType.Information, "[SẢN PHẨM] này chưa được xếp hết vào các vị trí, chưa được phép ghi sổ. \n(Nhập: " + so_luong_luc_nhap.ToString() + " ,Đã xếp: " + so_luong_da_xep.ToString() + ")");
                                return;
                            }

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                is_ghiso = _IPHIEU_NHAP_XUAT_KHO.Ghiso(dt.Rows[i], pCreatedDate, CommonDataHelper.UserName, ConstCommon.DonViQuanLy, pN_OR_N, true);
                            }
                        }
                        else //Neu la xuat
                        {
                            for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                            {
                                is_ghiso = _IPHIEU_NHAP_XUAT_KHO.Ghiso(dt_PHIEU_CTIET.Rows[i], pCreatedDate, CommonDataHelper.UserName, ConstCommon.DonViQuanLy, pN_OR_N, true);
                            }
                        }
                        if (is_ghiso == false)
                        {
                            base.ShowMessage(MessageType.Information, "[CHỨNG TỪ] này đã quá hạn được phép sửa.");
                            return;
                        }
                        else
                        {
                            bool result = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_NHAPXUATKHO_CTIET_ID"]), true);
                            if (result == true)
                            {
                                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                                return;
                            }
                            else
                            {
                                base.ShowMessage(MessageType.Information, "Thất bại.");
                                return;
                            }
                        }
                    }
                    else
                    {
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
        private void btnBoGhiSo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool msg = false;
                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;
                if (ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_NHAPXUATKHO_CTIET_ID"]) > 0)
                {
                    msg = (bool)base.ShowMessage(MessageType.OkCancel, "Bạn có muốn bỏ ghi sổ sản phẩm này không?");
                    if (msg == true)
                    {
                        if (ConstCommon.NVL_NUM_LONG_NEW(row.Row["DA_GHISO"]) == 0)
                        {
                            base.ShowMessage(MessageType.Information, "Sản phẩm này chưa được ghi sổ.");
                            return;
                        }
                        bool is_boghiso = false;
                        DateTime pCreatedDate = Convert.ToDateTime(this.iDataSource.Rows[0]["CreateDate"].ToString());
                        DataSet ds_ctiet_sanpham = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_BY_PHIEU(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["PHIEU_NHAPXUATKHO_ID"]), ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_NHAPXUATKHO_CTIET_ID"]), ConstCommon.NVL_NUM_LONG_NEW(row.Row["SANPHAM_ID"]));
                        DataTable dt = ds_ctiet_sanpham.Tables[0];
                        if (pN_OR_N == "N")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                is_boghiso = _IPHIEU_NHAP_XUAT_KHO.Ghiso(dt.Rows[i], pCreatedDate, CommonDataHelper.UserName, ConstCommon.DonViQuanLy, pN_OR_N, false);
                            }
                        }
                        else//Neu la xuat
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                is_boghiso = _IPHIEU_NHAP_XUAT_KHO.Ghiso(dt_PHIEU_CTIET.Rows[i], pCreatedDate, CommonDataHelper.UserName, ConstCommon.DonViQuanLy, pN_OR_N, false);
                            }
                        }
                        if (is_boghiso == false)
                        {
                            base.ShowMessage(MessageType.Information, "[CHỨNG TỪ] này đã quá hạn được phép sửa.");
                            return;
                        }
                        bool result = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_NHAPXUATKHO_CTIET_ID"]), false);
                        if (result == true)
                        {
                            ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                            return;
                        }
                        else
                        {
                            base.ShowMessage(MessageType.Information, "Thất bại.");
                            return;
                        }
                    }
                    else
                    {
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnImport.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            finally
            {
                isFirstLoad = false;
            }
        }
        private void btnChonSPBSung_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = null;
                //xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP", null);
                if (pN_OR_N == "N")
                {
                    xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP", null);
                }
                else
                {
                    xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP_Kho", null);
                }
                DataTable dt_SPCHON = (DataTable)xReturn;
                if ((dt_SPCHON != null) && (dt_SPCHON.Rows.Count > 0))
                {
                    isSecondLoad = false;
                    isEndLoad = true;
                    //if (isEndLoad == true && isSecondLoad == true)
                    //{
                    //    grd.Columns["SO_LUONG_PHIEU_YEU_CAU"].Visible = true;
                    //}
                    //else
                    //    grd.Columns["SO_LUONG_PHIEU_YEU_CAU"].Visible = false;
                    if (dt_SPCHON.Rows[0]["GHI_CHU"].ToString() == "Y")
                    {
                        this.iDataSource.Rows[0]["PHIEUYEUCAU_NHAPXUATKHO_ID"] = 0;
                        for (int i = 0; i < dt_SPCHON.Rows.Count; i++)
                        {
                            if (ContainDataRowInDataTable(dt_PHIEU_CTIET, dt_SPCHON.Rows[i]) == false)
                            {
                                DataRow dr = dt_PHIEU_CTIET.NewRow();
                                dr["STT"] = dt_PHIEU_CTIET.Rows.Count + 1;
                                dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr["PHIEU_NHAPXUATKHO_ID"] = "0";
                                if (pN_OR_N == "X")
                                {
                                    //dr["SOLO"] = dt_SPCHON.Rows[i]["SOLO"];
                                    //dr["KHO_KHUVUC_ID"] = dt_SPCHON.Rows[i]["KHO_KHUVUC_ID"];
                                    //grd.Columns["KHO_KHUVUC_ID"].Visible = false;
                                    dr["KHO_ID"] = dt_SPCHON.Rows[i]["KHO_ID"];
                                    dr["HANDUNG"] = dt_SPCHON.Rows[i]["HANDUNG"];
                                    //dr["SO_LUONG_TON"] = dt_SPCHON.Rows[i]["SO_LUONG_TON"];
                                    dr["VITRI"] = dt_SPCHON.Rows[i]["VITRI"];
                                }
                                else
                                {
                                    //dr["SOLO"] = null;
                                    dr["KHO_KHUVUC_ID"] = "0";
                                   // grd.Columns["KHO_KHUVUC_ID"].Visible = true;
                                    dr["KHO_ID"] = ConstCommon.pKHO_ID;
                                    dr["HANDUNG"] = null;
                                }
                                dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                //dr["MA_SANPHAM_KH"] = dt_SPCHON.Rows[i]["MA_SANPHAM_KH"];
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                dr["QUYCACHDONGGOI"] = dt_SPCHON.Rows[i]["QUYCACHDONGGOI"];
                                dr["MA_DONVI_TINH"] = dt_SPCHON.Rows[i]["MA_DONVI_TINH"];
                               // dr["SOLUONG_QUYDOI"] = dt_SPCHON.Rows[i]["SOLUONG_QUYDOI"];
                               // dr["SO_LUONG_PHIEU_YEU_CAU"] = 0;
                                txtID_PHIEU_YC.Text = String.Empty;
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSPBSung.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void btnXoadong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool msg = false;
                if ((dt_PHIEU_CTIET == null) || (dt_PHIEU_CTIET.Rows.Count == 0))
                {
                    return;
                }
                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;

                if (ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_NHAPXUATKHO_CTIET_ID"]) > 0)
                {
                    msg = (bool)base.ShowMessage(MessageType.OkCancel, "Bạn có muốn xóa dòng đang chọn trong phiếu không");
                    if (msg == true)
                    {
                        DataTable dsReturn = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_DELETE(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(row.Row["PHIEU_NHAPXUATKHO_CTIET_ID"]), CommonDataHelper.UserName);
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
                dt_PHIEU_CTIET.Rows.Remove(row.Row);
                this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnXoadong.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void btnChonPhieuYC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                chkIS_NHAPNHIEULAN.IsChecked = false;
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_NhapKho_popup_phieu_yeu_cau", null);
                DataSet ds_Return= (DataSet)xReturn;
                if (ds_Return == null) return;
                DataTable dt_PHIEU_YEUCCAU_CT_CHON = ds_Return.Tables[1];
                DataTable dt_PHIEU_YEUCCAU_CHON = ds_Return.Tables[0];
                //begin tao phieu chi tiet
                if ((dt_PHIEU_YEUCCAU_CT_CHON != null) && (dt_PHIEU_YEUCCAU_CT_CHON.Rows.Count > 0))
                {
                    isSecondLoad = true;
                    //IsVisibleSoLuongYeuCau();
                    if (dt_PHIEU_YEUCCAU_CHON.Rows[0]["GHI_CHU"].ToString() == "Y")
                    {
                        dt_PHIEU_CTIET = this.TableSchemaDefineBingding_Grid();
                        dt_PHIEU_CTIET.Clear();
                        this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"] = 0;
                        txtPHIEUYEUCAU_NHAPKHO_ID.Text = "";
                        this.iDataSource.Rows[0]["PHIEUYEUCAU_NHAPXUATKHO_ID"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["PHIEUYEUCAU_NHAPXUATKHO_ID"];
                        txtID_PHIEU_YC.Text = dt_PHIEU_YEUCCAU_CHON.Rows[0]["SOPHIEU"].ToString();
                        this.iDataSource.Rows[0]["MA_DVIQLY"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["MA_DVIQLY"];
                        this.iDataSource.Rows[0]["KHACHHANG_NCC_ID"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["KHACHHANG_NCC_ID"];
                        this.iDataSource.Rows[0]["HOPDONG_ID"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["HOPDONG_ID"];
                        this.iDataSource.Rows[0]["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["KHACHHANG_NCC_DONVI_VANCHUYEN_ID"];
                        this.iDataSource.Rows[0]["KHACHHANG_NCC_NOI_XUATHANG_ID"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["KHACHHANG_NCC_NOI_XUATHANG_ID"];
                        this.iDataSource.Rows[0]["NGUOILIENHE_A"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["NGUOILIENHE_A"];
                        this.iDataSource.Rows[0]["NGUOILIENHE_B"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["NGUOILIENHE_B"];
                        this.iDataSource.Rows[0]["SO_CHUNGTU"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["SO_CHUNGTU"];
                        this.iDataSource.Rows[0]["NGAY_CHUNGTU"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["NGAY_CHUNGTU"];
                        this.iDataSource.Rows[0]["MA_HINHTHU_NHAPXUAT"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["MA_HINHTHU_NHAPXUAT"];
                        this.iDataSource.Rows[0]["SOPHIEU"] = String.Empty;
                        this.iDataSource.Rows[0]["NGAYLAP"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["NGAYLAP"];
                        this.iDataSource.Rows[0]["TEN_TAIXE"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["TEN_TAIXE"];
                        this.iDataSource.Rows[0]["SO_DIENTHOAI"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["SO_DIENTHOAI"];
                        this.iDataSource.Rows[0]["SO_XE"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["SO_XE"];
                        this.iDataSource.Rows[0]["SO_CONT"] = dt_PHIEU_YEUCCAU_CHON.Rows[0]["SO_CONT"];
                        this.iDataSource.Rows[0]["IS_NHAPNHIEULAN"] = 0;
                        this.iDataSource.Rows[0]["GHI_CHU"] = string.Empty;
                        this.iDataSource.Rows[0]["PHIEU_KIEMKE_ID"] = 0;
                        for (int i = 0; i < dt_PHIEU_YEUCCAU_CT_CHON.Rows.Count ; i++)
                        {
                            if (ContainDataRowInDataTable(dt_PHIEU_CTIET, dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]) == false)
                            {
                                DataRow dr = dt_PHIEU_CTIET.NewRow();
                                dr["STT"] = dt_PHIEU_CTIET.Rows.Count + 1;
                                dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr["PHIEU_NHAPXUATKHO_ID"] = "0";
                                dr["KHO_ID"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["KHO_ID"];
                                dr["KHO_KHUVUC_ID"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["KHO_KHUVUC_ID"];
                                dr["SANPHAM_ID"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM_KH"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["MA_SANPHAM_KH"];
                                dr["MA_SANPHAM"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["TEN_SANPHAM"];
                                dr["MA_DONVI_TINH"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["MA_DONVI_TINH"];
                                
                                dr["MA_ITEM_TYPE"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["QUYCACHDONGGOI"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["QUYCACHDONGGOI"];
                                dr["SOLO"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["SOLO"];
                                dr["HANDUNG"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["HANDUNG"];
                                dr["SO_LUONG_PHIEU_YEU_CAU"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["SO_LUONG"];
                                dr["SO_LUONG_TONG"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["SO_LUONG"];
                                dr["DONGIA"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["DONGIA"];
                                dr["THANHTIEN"] = dt_PHIEU_YEUCCAU_CT_CHON.Rows[i]["THANHTIEN"];
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonPhieuYC.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dt_PHIEU_CTIET.Rows.Count == 0)
                {
                    base.ShowMessage(MessageType.Information, "Chưa có phiếu chi tiết nào.");
                    return;
                }
                for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                {
                    //if (dt_PHIEU_CTIET.Rows[i]["IS_NHAPNHIEULAN"].ToString() == "1")
                    //{
                    //    this.iDataSource.Rows[0]["IS_NHAPNHIEULAN"] = "True";
                    //    break;
                    //}
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
                    //if (dt_PHIEU_CTIET.Rows[i]["MA_TINHTRANG_HANG"].ToString() == String.Empty)
                    //{
                    //    base.ShowMessage(MessageType.Information, "Chưa chọn [TRẠNG THÁI HÀNG].");
                    //    return;
                    //}
                    ////if (dt_PHIEU_CTIET.Rows[i]["VITRI"].ToString() == String.Empty)
                    ////{
                    ////    base.ShowMessage(MessageType.Information, "Chưa chọn [VỊ TRÍ HÀNG].");
                    ////    return;
                    ////}
                    //if (dt_PHIEU_CTIET.Rows[i]["MA_TINHTRANG_CV"].ToString() == String.Empty)
                    //{
                    //    base.ShowMessage(MessageType.Information, "Chưa chọn [TÌNH TRẠNG CV].");
                    //    return;
                    //}
                }

                DataSet dsReturn = null;
                if(pN_OR_N=="N")
                    dsReturn = _IPHIEU_NHAP_XUAT_KHO.InsertKO_PHIEU_NHAPXUATKHO(iDataSource, dt_PHIEU_CTIET, CommonDataHelper.UserName, "N");
                else
                    dsReturn = _IPHIEU_NHAP_XUAT_KHO.InsertKO_PHIEU_NHAPXUATKHO(iDataSource, dt_PHIEU_CTIET, CommonDataHelper.UserName, "X");
                if ((dsReturn != null) && (dsReturn.Tables.Count == 2))
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
                        //if (pN_OR_N == "X")
                        //{
                        //    for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                        //    {
                        //        dt_PHIEU_CTIET.Rows[i]["IS_XUAT"] = true;
                        //    }
                        //    long flag = _IPHIEU_NHAP_XUAT_KHO.InsertorUpdateKO_PHIEU_NHAPXUATKHO_CTIET_VTRI(dt_PHIEU_CTIET, CommonDataHelper.UserName);
                        //    if (flag <= 0)
                        //    {
                        //        base.ShowMessage(MessageType.Error, "Lưu không thành công");
                        //        return;
                        //    }
                        //}
                        this.btnGhiSoAll.Visibility = Visibility.Visible;
                        this.btnBoGhiSoAll.Visibility = Visibility.Visible;
                        //grd.Columns["GHI_SO"].Visible = true;
                        //grd.Columns["BO_GHI_SO"].Visible = true;
                        this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);

                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }

                    ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                    pPHIEU_XUATNHAP_KHO_ID = ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"].ToString());
                    isSecondLoad = false;
                    //IsVisibleSoLuongYeuCau();
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
            this.Close();
        }

        private void btnThemmoi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isSecondLoad = false;
                chkIS_NHAPNHIEULAN.IsChecked = false;
                txtID_PHIEU_YC.Text = String.Empty;
                this.iDataSource = this.TableSchemaDefineBingding();
                this.DataContext = this.iDataSource;
                grd.ItemsSource = null;
                if (dt_PHIEU_CTIET != null)
                {
                    dt_PHIEU_CTIET.Clear();
                }
                this.iDataSource.Rows[0]["TEN_KHO"] = ConstCommon.pTEN_KHO;
                this.iDataSource.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                LoadComboBox();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //try
            //{
            //    int so_luong_quy_doi = 0;
            //    if (this.grd.GetFocusedRow() == null) return;
            //    DataRowView row = this.grd.GetFocusedRow() as DataRowView;
            //    if (ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]) != 0)
            //        row["THANHTIEN"] = ConstCommon.NVL_NUM_DECIMAL_NEW(row["SO_LUONG_TONG"]) * ConstCommon.NVL_NUM_DECIMAL_NEW(row["DONGIA"]);
            //    if(pN_OR_N == "X")
            //    {
            //        if(e.Column.FieldName == "SO_LUONG_TON" || e.Column.FieldName == "SO_LUONG_TONG")
            //        {
            //            int so_luong_ton = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TON"]);
            //            int so_luong_xuat = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]);
            //            int so_luong_con_lai = so_luong_ton - so_luong_xuat;
            //            if(so_luong_con_lai < 0)
            //            {
            //                base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG XUẤT] vượt quá [SỐ LƯỢNG TỒN KHO].");
            //                row["SO_LUONG_TONG"] = 0;
            //            }
            //        }
            //    }
            //    if (e.Column.FieldName == "SO_LUONG_LE" || e.Column.FieldName == "SO_LUONG_TONG" || e.Column.FieldName == "SO_LUONG_THUNG")
            //    {
            //        so_tong = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]);
            //        so_thung = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_THUNG"]);
            //        so_le = ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_LE"]);
            //        if (ConstCommon.NVL_NUM_NT_NEW(row["SO_LUONG_TONG"]) != 0)
            //            row["THANHTIEN"] = ConstCommon.NVL_NUM_DECIMAL_NEW(row["SO_LUONG_TONG"]) * ConstCommon.NVL_NUM_DECIMAL_NEW(row["DONGIA"]);
            //        if(ConstCommon.NVL_NUM_NT_NEW(row["SOLUONG_QUYDOI"])> 0)
            //        {
            //           so_luong_quy_doi = ConstCommon.NVL_NUM_NT_NEW(row["SOLUONG_QUYDOI"]);
            //           SetIsNhapNhieuLan();
            //           if (e.Column.FieldName == "SO_LUONG_TONG")
            //           {
            //                if((so_tong != 0 && so_thung != 0 && so_le != 0) || so_thung == 0 || so_le == 0 ||(so_thung == 0 && so_le == 0))
            //                {
            //                    row["SO_LUONG_THUNG"] = so_tong / so_luong_quy_doi;
            //                    row["SO_LUONG_LE"] = so_tong % so_luong_quy_doi;
            //                    return;
            //                }
            //                if(so_tong == 0)
            //                {
            //                    row["SO_LUONG_THUNG"] = 0;
            //                    row["SO_LUONG_LE"] = 0;
            //                    return;
            //                }
            //           }
            //           if(e.Column.FieldName == "SO_LUONG_THUNG")
            //           {
            //                if (so_thung == 0)
            //                {
            //                    row["SO_LUONG_LE"] = so_tong;
            //                    return;
            //                }
            //                if ((so_tong != 0 && so_thung != 0 && so_le != 0) || so_tong == 0 || so_tong != 0)
            //                {
            //                    row["SO_LUONG_TONG"] = so_thung * so_luong_quy_doi + so_le;
            //                    return;
            //                }
            //           }
            //           if(e.Column.FieldName == "SO_LUONG_LE")
            //           {
            //                if(so_tong != 0 && so_thung != 0 && so_le != 0)
            //                {
            //                    if(so_le > so_luong_quy_doi)
            //                    {
            //                        base.ShowMessage(MessageType.Information, "Bạn nhập quá [SỐ LƯỢNG QUY ĐỔI].");
            //                        row["SO_LUONG_LE"] = so_tong - (so_thung * so_luong_quy_doi);
            //                        return;
            //                    }
            //                    if(so_le == so_luong_quy_doi)
            //                    {
            //                        row["SO_LUONG_THUNG"] = so_thung + (so_le / so_luong_quy_doi);
            //                        row["SO_LUONG_LE"] = so_le % so_luong_quy_doi;
            //                        row["SO_LUONG_TONG"] = so_thung * so_luong_quy_doi + so_le;
            //                        return;
            //                    }
            //                    else
            //                    {
            //                        row["SO_LUONG_TONG"] = so_thung * so_luong_quy_doi + so_le;
            //                        return;
            //                    }
            //                }
            //                if(so_thung == 0)
            //                {
            //                    row["SO_LUONG_TONG"] = so_le;
            //                    row["SO_LUONG_THUNG"] = so_thung + (so_le / so_luong_quy_doi);
            //                    row["SO_LUONG_LE"] = so_le % so_luong_quy_doi;
            //                    return;
            //                }
            //                if(so_tong == 0)
            //                {
            //                    if(so_le >= so_luong_quy_doi)
            //                    {
            //                        row["SO_LUONG_THUNG"] = so_thung + (so_le / so_luong_quy_doi);
            //                        row["SO_LUONG_LE"] = so_le % so_luong_quy_doi;
            //                        row["SO_LUONG_TONG"] = so_thung * so_luong_quy_doi + so_le;
            //                        return;
            //                    }
            //                    else
            //                    {
            //                        row["SO_LUONG_TONG"] = so_thung * so_luong_quy_doi + so_le;
            //                        return;
            //                    }
            //                }
            //                if(so_le == 0)
            //                {
            //                    row["SO_LUONG_TONG"] = so_thung * so_luong_quy_doi;
            //                    return;
            //                }
            //           }
            //        }
            //        else
            //        {
                        
            //            SetIsNhapNhieuLan();
            //            row["SO_LUONG_THUNG"] = 0;
            //            row["SO_LUONG_LE"] = 0;
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        SetIsNhapNhieuLan();
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnThemmoi.Tag.ToString());
            //    base.ShowMessage(MessageType.Error, ex.Message, ex);
            //}
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

        private void grdView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                dt_PHIEU_CTIET.AcceptChanges();
                if (this.grd.GetFocusedRow() == null) return;
                int vitri = this.grdView.FocusedRowHandle;
                if (vitri < 0) return;

                //if(isSecondLoad == true)
                //{
                //    if (isKiemKe != "KK")
                //    {
                //        if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[vitri]["SO_LUONG_PHIEU_YEU_CAU"].ToString()) > 0)
                //        {
                //            foreach (Column col in iGridHelper.GetColumns)
                //            {
                //                if (col.Name == "KHO_KHUVUC_ID".ToString())
                //                {
                //                    iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            foreach (Column col in iGridHelper.GetColumns)
                //            {
                //                if (col.Name == "KHO_KHUVUC_ID".ToString())
                //                {
                //                    iGridHelper.GetColumnByName(col.Name).AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                //                }
                //            }
                //        }
                //    }
                //}
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNhapHangNhieuLan.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

        }

        void Ngay()
        {
            string a = txtNGAYLAP.Text.Substring(0, 2);
            string b = txtNGAYLAP.Text.Substring(3, 2);
            string c = txtNGAYLAP.Text.Substring(6, 4);
            ngayPhieu = "Ngày: " + b + " tháng: " + a + " năm: " + c;
        }

        void TongTT()
        {
            tongTT = 0;
            for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
            {
                tongTT = tongTT + decimal.Parse(dt_PHIEU_CTIET.Rows[i]["THANHTIEN"].ToString());
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExportExcel exp = new ExportExcel();
                DataSet dsSource = new DataSet();
                string filename = string.Empty;
                DataTable dt = new DataTable();
                DataTable dtCT = new DataTable();
                Ngay();
                TongTT();
                string tongTT_chu = ConstCommon.So_tien_doi_ra_chu(double.Parse(tongTT.ToString()));
                dt = this.iDataSource.Copy();
                dtCT = this.dt_PHIEU_CTIET.Copy();
                dt.Rows[0]["NGAYLAP"] = ngayPhieu;
                dt.Columns.Add("TongTT", typeof(string));
                dt.Columns.Add("TongTT_CHU", typeof(string));
                dt.Rows[0]["TongTT"] = tongTT;
                dt.Rows[0]["TongTT_CHU"] = tongTT_chu;
                dt.TableName = "dt";
                dtCT.TableName = "dtCT";
                dsSource.Tables.Add(dt);
                dsSource.Tables.Add(dtCT);
                string templaleFileName;
                if (status == true)
                    templaleFileName = "RP_PHIEUNHAPKHO.xlsx";
                else
                    templaleFileName = "RP_PHIEUXUATKHO.xlsx";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Excel files (*.xlsx or .xls)|.xlsx;*.xls";
                dlg.FileName = string.Format("{0}", Guid.NewGuid());
                var result = dlg.ShowDialog();
                if (result == true)
                {
                    filename = dlg.FileName;
                    var export = exp.Return(filename, templaleFileName, dsSource);
                    if (export)
                    {
                        System.Diagnostics.Process.Start(filename);
                        ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                        return;
                    }
                }


            }
            catch (Exception ex)
            {
                //UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnExcel.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnGhiSoAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool msg = false;
                msg = (bool)base.ShowMessage(MessageType.OkCancel, "Bạn có muốn ghi sổ tất cả sản phẩm không?");
                if (msg == true)
                {
                    bool result = false;
                    bool is_ghiso = false;
                    bool is_da_ghiso = false;
                    DateTime pCreatedDate = Convert.ToDateTime(this.iDataSource.Rows[0]["CreateDate"].ToString());
                    DataSet ds_ctiet = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_ALL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]));
                    DataTable dt = ds_ctiet.Tables[0];
                    if (pN_OR_N == "N")
                    {
                        if (dt.Rows.Count == 0)
                        {
                            base.ShowMessage(MessageType.Information, "[CÁC SẢN PHẨM]" + " chưa được xếp hết vào các vị trí, chưa được phép ghi sổ.");
                            return;
                        }
                        else
                        {
                            for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                            {
                                int so_luong_luc_nhap = 0;
                                int so_luong_da_xep = 0;
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["PHIEU_NHAPXUATKHO_CTIET_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(dt.Rows[j]["PHIEU_NHAPXUATKHO_CTIET_ID"])
                                             && ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET.Rows[i]["SANPHAM_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(dt.Rows[j]["SANPHAM_ID"]))
                                    {
                                        so_luong_da_xep += ConstCommon.NVL_NUM_NT_NEW(dt.Rows[j]["SO_LUONG_TONG"]);
                                        if (so_luong_luc_nhap > 0)
                                            continue;
                                        else
                                        {
                                            so_luong_luc_nhap += ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET.Rows[i]["SO_LUONG_TONG"]);
                                        }

                                    }
                                }
                                if (so_luong_luc_nhap > so_luong_da_xep)
                                {
                                    base.ShowMessage(MessageType.Information, "[SẢN PHẨM " + dt_PHIEU_CTIET.Rows[i]["TEN_SANPHAM"].ToString() + " ]" + " chưa được xếp hết vào các vị trí, chưa được phép ghi sổ. \n(Nhập: " + so_luong_luc_nhap.ToString() + " ,Đã xếp: " + so_luong_da_xep.ToString() + ")");
                                    return;
                                }
                            }
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["DA_GHISO"].ToString() == "1")
                            {
                                is_da_ghiso = false;
                                continue;
                            }
                            else
                            {
                                is_da_ghiso = true;
                                is_ghiso = _IPHIEU_NHAP_XUAT_KHO.Ghiso(dt.Rows[i], pCreatedDate, CommonDataHelper.UserName, ConstCommon.DonViQuanLy, pN_OR_N, true);
                            }
                        }
                        if (is_da_ghiso == false)
                        {
                            base.ShowMessage(MessageType.Information, "Các sản phẩm này đã được ghi sổ.");
                            return;
                        }
                        if (is_ghiso == false)
                        {
                            base.ShowMessage(MessageType.Information, "[CHỨNG TỪ] này đã quá hạn được phép sửa.");
                            return;
                        }
                        result = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO_ALL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]), true);
                        if (result == true)
                        {
                            btnGhiSoAll.Visibility = Visibility.Collapsed;
                            btnBoGhiSoAll.Visibility = Visibility.Visible;
                            ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                            return;
                        }
                        else
                        {
                            base.ShowMessage(MessageType.Information, "Thất bại.");
                            return;
                        }
                    }
                    else //Nguoc lai la xuat kho
                    {
                        for(int i = 0;i < dt_PHIEU_CTIET.Rows.Count;i++)
                        {
                            is_ghiso = _IPHIEU_NHAP_XUAT_KHO.Ghiso(dt_PHIEU_CTIET.Rows[i], pCreatedDate, CommonDataHelper.UserName, ConstCommon.DonViQuanLy, pN_OR_N, true);
                        }
                    }
                    if (is_ghiso == false)
                    {
                        base.ShowMessage(MessageType.Information, "[CHỨNG TỪ] này đã quá hạn được phép sửa.");
                        return;
                    }
                     result = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO_ALL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]), true);
                    if (result == true)
                    {
                        btnGhiSoAll.Visibility = Visibility.Collapsed;
                        btnBoGhiSoAll.Visibility = Visibility.Visible;
                        ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                        return;
                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, "Thất bại.");
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

        private void btnBoGhiSoAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool msg = false;
                msg = (bool)base.ShowMessage(MessageType.OkCancel, "Bạn có muốn bỏ ghi sổ tất cả sản phẩm không?");
                if (msg == true)
                {
                    bool is_boghiso = false;
                    bool flag = false;
                    DateTime pCreatedDate = Convert.ToDateTime(this.iDataSource.Rows[0]["CreateDate"].ToString());
                    DataSet ds_ctiet = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_VTRI_GET_ALL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]));
                    DataTable dt = ds_ctiet.Tables[0];
                    if (pN_OR_N == "N")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["DA_GHISO"].ToString() != "0")
                            {
                                flag = true;
                            }
                            if (dt.Rows[i]["DA_GHISO"].ToString() == "0")
                            {
                                continue;
                            }
                            else
                                is_boghiso = _IPHIEU_NHAP_XUAT_KHO.Ghiso(dt.Rows[i], pCreatedDate, CommonDataHelper.UserName, ConstCommon.DonViQuanLy, pN_OR_N, false);
                        }
                        if (flag == false)
                        {
                            base.ShowMessage(MessageType.Information, "[CÁC SẢN PHẨM]" + " này chưa được ghi sổ.");
                            return;
                        }
                    }
                    else//Neu la xuat kho
                    {
                        for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                        {
                            if (dt_PHIEU_CTIET.Rows[i]["DA_GHISO"].ToString() != "0")
                            {
                                flag = true;
                            }
                            if (dt_PHIEU_CTIET.Rows[i]["DA_GHISO"].ToString() == "0")
                            {
                                continue;
                            }
                            else
                                is_boghiso = _IPHIEU_NHAP_XUAT_KHO.Ghiso(dt_PHIEU_CTIET.Rows[i], pCreatedDate, CommonDataHelper.UserName, ConstCommon.DonViQuanLy, pN_OR_N, false);
                        }
                        if (flag == false)
                        {
                            base.ShowMessage(MessageType.Information, "[CÁC SẢN PHẨM]" + " này chưa được ghi sổ.");
                            return;
                        }
                    }
                    if (is_boghiso == false)
                    {
                        base.ShowMessage(MessageType.Information, "[CHỨNG TỪ] này đã quá hạn được phép sửa.");
                        return;
                    }
                    bool result = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_CTIET_UPDATE_ISGHISO_ALL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PHIEU_NHAPXUATKHO_ID"]), false);
                    if (result == true)
                    {
                        btnGhiSoAll.Visibility = Visibility.Visible;
                        btnBoGhiSoAll.Visibility = Visibility.Collapsed;
                        ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                        return;
                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, "Thất bại.");
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
        private void btnNhapHangNhieuLan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //isSecondLoad = false;
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_NhapKho_popup_nhap_hang_nhieu_lan", null);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNhapHangNhieuLan.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        //
        #endregion
        #region PrintBarCode
        public static XRBarCode CreateCode128BarCode(string BarCodeText, int i)
        {
            // Create a bar code control.
            XRBarCode barCode = new XRBarCode();

            // Set the bar code's type to Code 128.
            barCode.Symbology = new Code128Generator();

            // Adjust the bar code's main properties.
            barCode.Text = BarCodeText;
            barCode.Width = 400;
            barCode.Height = 100;
            barCode.LocationF = new System.Drawing.PointF(200F, 150F * i);
            barCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            // Adjust the properties specific to the bar code type.
            ((Code128Generator)barCode.Symbology).CharacterSet = Code128Charset.CharsetAuto;

            return barCode;
        }

        private DataTable MA_VACH()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("pBarCodeVitri", typeof(string));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }
        private void btnInMaVach_Click(object sender, RoutedEventArgs e)
        {
            tbl.Clear();
            try
            {
                for (int i = 0; i < dt_PHIEU_CTIET.Rows.Count; i++)
                {
                    if (dt_PHIEU_CTIET.Rows[i]["CHON"].ToString() == "1")
                    {
                        dt_PHIEU_CTIET.Rows[i]["MA_VACH"] = dt_PHIEU_CTIET.Rows[i]["MA_SANPHAM"].ToString() + dt_PHIEU_CTIET.Rows[i]["SOLO"].ToString();

                        tbl.Rows.Add(dt_PHIEU_CTIET.Rows[i]["MA_VACH"]);
                    }
                }
                Rpt_NhapXuat rpt = new Rpt_NhapXuat();
                Rpt_NhapXuat.tbl = tbl;
                //rpt.DataSource = tbl;
                rpt.Parameters["txtTieude"].Value = "Danh sách mã vạch";
                XtraReportPreviewModel model = new XtraReportPreviewModel(rpt);
                rpt.CreateDocument(true);
                model.IsParametersPanelVisible = false;
                //DocumentPreview1.Model = model;


                model.ParametersModel.SubmitParameters();
                DocumentPreviewWindow dpw = new DocumentPreviewWindow();
                dpw.Model = model;
                dpw.ShowDialog();
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        #endregion
    }
}