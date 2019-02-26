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
    /// Interaction logic for frmDieuChuyenNoiBo_popup.xaml
    /// </summary>
    public partial class frmDieuChuyenNoiBo_popup : PopupBase
    {
        DataTable dt_DIEUCHUYEN = null;
        DataTable dt_PHIEUXUAT = null;
        DataTable dt_PHIEUNHAP = null;
        GridHelper iGridHelper = null;
        DataRow iRowSelGb = null;
        string pLOAI_DC = String.Empty;
        DataTable dt_PHIEU_CTIET_X = null;
        DataTable dt_PHIEU_CTIET_N = null;
        int so_tong, so_thung, so_le = 0;
        IPHIEU_NHAPXUATKHOBussiness _IPHIEU_NHAP_XUAT_KHO;
        DataTable dt_TRANGTHAI , dt_KHUVUC, dt_VITRIHANG, dt_TINHTRANGCV, dt_PALLET, dt_KHO = null;
        DataTable dt_KHOXUAT, dt_KHONHAN = null;
        bool isFirstLoad = true;
        string pDA_CHUYEN = String.Empty;
        public frmDieuChuyenNoiBo_popup()
        {
            InitializeComponent();
            this.dt_DIEUCHUYEN = this.TableSchemaDefineBingding();
            this.dt_PHIEU_CTIET_X = this.TableSchemaDefineBingding_Grid();
            dt_PHIEU_CTIET_X.Clear();
            this.dt_PHIEU_CTIET_N = this.TableSchemaDefineBingding_Grid();
            dt_PHIEU_CTIET_N.Clear();
            this.dt_PHIEUXUAT = this.TableSchemaDefineBingding_PHIEUNX_KHO();
            this.dt_PHIEUNHAP = this.TableSchemaDefineBingding_PHIEUNX_KHO();
            _IPHIEU_NHAP_XUAT_KHO = new PHIEU_NHAPXUATKHOBussiness(string.Empty);
            this.DataContext = this.dt_DIEUCHUYEN;
            LoadGirdCombobox();
            Initialize_Grid();
        }
        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                
                if (this.Parameter.Count() <= 2)
                {
                    pLOAI_DC = this.Parameter[0].ToString();
                    txtLoaiChuyen.Text = this.Parameter[1].ToString();
                    isFirstLoad = true;
                    btnChapNhan.Visibility = Visibility.Collapsed;
                }
                else
                {
                    isFirstLoad = false;
                    pLOAI_DC = this.Parameter[0].ToString();
                    txtLoaiChuyen.Text = this.Parameter[1].ToString();
                    iRowSelGb = this.Parameter[2] as DataRow;
                    LoadGirdCombobox();
                    Initialize_Grid();
                    if (iRowSelGb == null)
                    { return; }
                    DispalayRequest();
                    pLOAI_DC = this.dt_DIEUCHUYEN.Rows[0]["LOAI_DC"].ToString();
                    pDA_CHUYEN = this.Parameter[7].ToString();
                    if (pDA_CHUYEN == String.Empty)
                    {
                        btnSave.Visibility = Visibility.Visible;
                        btnChapNhan.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        btnSave.Visibility = Visibility.Collapsed;
                        btnChapNhan.Visibility = Visibility.Visible;
                    }
                    if (this.Parameter.Count() > 1)
                    {
                        
                        DataRow[] dr_chitiet_x = this.Parameter[3] as DataRow[];
                        DataRow[] dr_chitiet_n = this.Parameter[4] as DataRow[];
                        DataRow[] dr_phieuxuat = this.Parameter[5] as DataRow[];
                        DataRow[] dr_phieunhap = this.Parameter[6] as DataRow[];
                        if (dr_chitiet_n.Length > 0 && dr_phieunhap.Length > 0)
                        {
                            dt_PHIEU_CTIET_X.Clear();
                            dt_PHIEU_CTIET_N.Clear();
                            dt_PHIEUXUAT.Clear();
                            dt_PHIEUNHAP.Clear();
                            if (dr_chitiet_x.Length > 0)
                            {
                                dt_PHIEUXUAT = dr_phieuxuat.CopyToDataTable();
                                dt_PHIEUNHAP = dr_phieunhap.CopyToDataTable();
                                dt_PHIEU_CTIET_X = dr_chitiet_x.CopyToDataTable();
                                dt_PHIEU_CTIET_N = dr_chitiet_n.CopyToDataTable();
                            }
                            if (dt_PHIEU_CTIET_X != null)
                            {
                                this.iGridHelper.BindItemSource(dt_PHIEU_CTIET_X);
                            }
                            else
                            {
                                grdCT_PHIEU_NXKHO.ItemsSource = null;
                            }
                        }
                        else
                        {
                            dt_PHIEU_CTIET_X.Clear();
                            dt_PHIEUXUAT.Clear();
                            if (dr_chitiet_x.Length > 0)
                            {
                                dt_PHIEUXUAT = dr_phieuxuat.CopyToDataTable();
                              //  dt_PHIEUNHAP = dr_phieunhap.CopyToDataTable();
                                dt_PHIEU_CTIET_X = dr_chitiet_x.CopyToDataTable();
                              //  dt_PHIEU_CTIET_N = dr_chitiet_n.CopyToDataTable();
                            }
                            if (dt_PHIEU_CTIET_X != null)
                            {
                                this.iGridHelper.BindItemSource(dt_PHIEU_CTIET_X);
                            }
                            else
                            {
                                grdCT_PHIEU_NXKHO.ItemsSource = null;
                            }
                        }
                    }
                }
                LoadKhoXuatorKhoNhan();
            }
            else
            {
                isFirstLoad = true;
                return;
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
        private void cboMA_DVIQLY_CHUYEN_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            try
            {
                if (pLOAI_DC == "TRONGDONVI")
                {
                    cboMA_DVIQLY_NHAN.SelectedIndex = cboMA_DVIQLY_CHUYEN.SelectedIndex;
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadKhoXuatorKhoNhan()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnChonSP_Click(object sender, RoutedEventArgs e)
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
                            if (ContainDataRowInDataTable(dt_PHIEU_CTIET_X, dt_SPCHON.Rows[i]) == false)
                            {
                                DataRow dr = dt_PHIEU_CTIET_X.NewRow();
                                dr["STT"] = dt_PHIEU_CTIET_X.Rows.Count + 1;
                                dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                                dr["PHIEU_NHAPXUATKHO_ID"] = "0";
                                dr["KHO_ID"] = ConstCommon.pKHO_ID;
                                dr["KHO_KHUVUC_ID"] = "0";
                                dr["SOLO"] = dt_SPCHON.Rows[i]["SOLO"];
                                dr["HANDUNG"] = dt_SPCHON.Rows[i]["HANDUNG"];
                                dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM_KH"] = dt_SPCHON.Rows[i]["MA_SANPHAM_KH"];
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                dr["QUYCACHDONGGOI"] = dt_SPCHON.Rows[i]["QUYCACHDONGGOI"];
                                dr["MA_DONVI_TINH"] = dt_SPCHON.Rows[i]["MA_DONVI_TINH"];
                                dr["SOLUONG_QUYDOI"] = dt_SPCHON.Rows[i]["SOLUONG_QUYDOI"];
                                dt_PHIEU_CTIET_X.Rows.Add(dr);
                            }
                        }
                    }

                }
                if (dt_PHIEU_CTIET_X != null)
                {
                    this.iGridHelper.BindItemSource(dt_PHIEU_CTIET_X);
                }
                else
                {
                    grdCT_PHIEU_NXKHO.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSP.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdViewCT_PHIEU_NXKHO_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                int so_luong_quy_doi = 0;
                if (this.grdCT_PHIEU_NXKHO.GetFocusedRow() == null) return;
                DataRowView row = this.grdCT_PHIEU_NXKHO.GetFocusedRow() as DataRowView;
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

        private void grdViewCT_PHIEU_NXKHO_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                dt_PHIEU_CTIET_X.AcceptChanges();
                if (this.grdCT_PHIEU_NXKHO.GetFocusedRow() == null) return;
                int vitri = this.grdViewCT_PHIEU_NXKHO.FocusedRowHandle;
                if (vitri < 0) return;
                if (ConstCommon.NVL_NUM_LONG_NEW(dt_PHIEU_CTIET_X.Rows[vitri]["SOLUONG_QUYDOI"].ToString()) == 0)
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSP.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

        }

        /// <summary>
        /// LoadCombobox
        /// </summary>
        private void LoadGirdCombobox()
        {
            try
            {
                dt_KHO = _IPHIEU_NHAP_XUAT_KHO.GetListDM_KHO(ConstCommon.DonViQuanLy);
                dt_PALLET = _IPHIEU_NHAP_XUAT_KHO.GetListDM_PALLET(ConstCommon.DonViQuanLy);
                dt_TRANGTHAI = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TINHTRANG_HANG(ConstCommon.DonViQuanLy);
                dt_KHUVUC = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TENKHO_KHUVUC(ConstCommon.DonViQuanLy);
                dt_TINHTRANGCV = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_TINHTRANG_CV(ConstCommon.DonViQuanLy);
                dt_VITRIHANG = _IPHIEU_NHAP_XUAT_KHO.GetData_For_gird_VITRI_HANG(ConstCommon.DonViQuanLy, ConstCommon.pKHO_ID);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadGirdCombobox()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void LoadKhoXuatorKhoNhan()
        {
            try
            {
                if (pLOAI_DC == "TRONGDONVI")
                {
                    dt_KHOXUAT = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYXUAT(ConstCommon.DonViQuanLy);
                    dt_KHONHAN = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYXUAT(ConstCommon.DonViQuanLy);
                    if (dt_KHOXUAT != null && dt_KHOXUAT.Rows.Count > 0)
                    {
                        ComboBoxUtil.SetComboBoxEdit(cboMA_DVIQLY_CHUYEN, "TEN_DVIQLY", "MA_DVIQLY", dt_KHOXUAT, SelectionTypeEnum.Native);
                        ComboBoxUtil.InsertItem(cboMA_DVIQLY_CHUYEN, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                        this.dt_DIEUCHUYEN.Rows[0]["MA_DVIQLY"] = cboMA_DVIQLY_CHUYEN.GetKeyValue(0);
                    }
                    if (dt_KHONHAN != null && dt_KHONHAN.Rows.Count > 0)
                    {
                        ComboBoxUtil.SetComboBoxEdit(cboMA_DVIQLY_NHAN, "TEN_DVIQLY", "MA_DVIQLY", dt_KHONHAN, SelectionTypeEnum.Native);
                        ComboBoxUtil.InsertItem(cboMA_DVIQLY_NHAN, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                        this.dt_DIEUCHUYEN.Rows[0]["MA_DVIQLY"] = cboMA_DVIQLY_NHAN.GetKeyValue(0);
                    }
                    cboMA_DVIQLY_NHAN.IsEnabled = false;
                }
                if(pLOAI_DC == "KHACDONVI")
                {
                    dt_KHOXUAT = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYXUAT(ConstCommon.DonViQuanLy);
                    dt_KHONHAN = _IPHIEU_NHAP_XUAT_KHO.KO_PHIEU_NHAPXUATKHO_DCNB_GET_DVQLYNHAN(ConstCommon.DonViQuanLy);
                    if (dt_KHOXUAT != null && dt_KHOXUAT.Rows.Count > 0)
                    {
                        ComboBoxUtil.SetComboBoxEdit(cboMA_DVIQLY_CHUYEN, "TEN_DVIQLY", "MA_DVIQLY", dt_KHOXUAT, SelectionTypeEnum.Native);
                        ComboBoxUtil.InsertItem(cboMA_DVIQLY_CHUYEN, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                        this.dt_DIEUCHUYEN.Rows[0]["MA_DVIQLY"] = cboMA_DVIQLY_CHUYEN.GetKeyValue(0);
                    }
                    if (dt_KHONHAN != null && dt_KHONHAN.Rows.Count > 0)
                    {
                        ComboBoxUtil.SetComboBoxEdit(cboMA_DVIQLY_NHAN, "TEN_DVIQLY", "MA_DVIQLY", dt_KHONHAN, SelectionTypeEnum.Native);
                        ComboBoxUtil.InsertItem(cboMA_DVIQLY_NHAN, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                        this.dt_DIEUCHUYEN.Rows[0]["MA_DVIQLY"] = cboMA_DVIQLY_NHAN.GetKeyValue(0);
                    }
                    cboMA_DVIQLY_NHAN.IsEnabled = true ;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadKhoXuatorKhoNhan()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// DispalayRequest
        /// </summary>
        public void DispalayRequest()
        {
            try
            {
                foreach (DataColumn item in this.iRowSelGb.Table.Columns)
                {
                    if (this.dt_DIEUCHUYEN.Columns[item.ColumnName] != null)
                    {
                        this.dt_DIEUCHUYEN.Rows[0][item.ColumnName] = iRowSelGb[item.ColumnName];
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

        private void SaveData()
        {
            try
            {
                this.dt_DIEUCHUYEN.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                if (isFirstLoad == true)
                {
                    this.dt_PHIEUXUAT.Rows[0]["PHIEUYEUCAU_NHAPXUATKHO_ID"] = 0;
                    this.dt_PHIEUXUAT.Rows[0]["SOPHIEU"] = this.dt_DIEUCHUYEN.Rows[0]["SOPHIEU_DC"];
                    this.dt_PHIEUXUAT.Rows[0]["NGAYLAP"] = this.dt_DIEUCHUYEN.Rows[0]["NGAY_DC"];

                    this.dt_PHIEUNHAP.Rows[0]["PHIEUYEUCAU_NHAPXUATKHO_ID"] = 0;
                    this.dt_PHIEUNHAP.Rows[0]["SOPHIEU"] = this.dt_DIEUCHUYEN.Rows[0]["SOPHIEU_DC"];
                    this.dt_PHIEUNHAP.Rows[0]["NGAYLAP"] = this.dt_DIEUCHUYEN.Rows[0]["NGAY_DC"];

                }
                if (SetIsNull() == false)
                {
                    return;
                }
                if (dt_PHIEU_CTIET_X.Rows.Count == 0)
                {
                    base.ShowMessage(MessageType.Information, "Chưa chọn sản phẩm nào.");
                    return;
                }
                SetIsNullGird();
                long result = _IPHIEU_NHAP_XUAT_KHO.InsertorUpdateKO_PHIEU_NHAPXUATKHO_DCNB(dt_DIEUCHUYEN, CommonDataHelper.UserName, pLOAI_DC);
                if (result > 0)
                {

                    if (pLOAI_DC == "TRONGDONVI")
                    {
                        if (isFirstLoad == true)
                        {
                            this.dt_PHIEUXUAT.Rows[0]["PHIEU_NHAPXUATKHO_DCNB_ID"] = result;
                            this.dt_PHIEUXUAT.Rows[0]["MA_HINHTHU_NHAPXUAT"] = "XUAT_NOIBO";
                            this.dt_PHIEUXUAT.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                        }
                        else
                        {
                            this.dt_PHIEUXUAT.Rows[0]["SOPHIEU"] = this.dt_DIEUCHUYEN.Rows[0]["SOPHIEU_DC"];
                            this.dt_PHIEUXUAT.Rows[0]["NGAYLAP"] = this.dt_DIEUCHUYEN.Rows[0]["NGAY_DC"];
                        }
                        DataSet ds_phieuxuat_return = null;
                        ds_phieuxuat_return = _IPHIEU_NHAP_XUAT_KHO.InsertKO_PHIEU_NHAPXUATKHO_DCNB_PHIEUNXK(dt_PHIEUXUAT, dt_PHIEU_CTIET_X, CommonDataHelper.UserName, "X");
                        if ((ds_phieuxuat_return != null) && (ds_phieuxuat_return.Tables.Count >= 2))
                        {
                            if (isFirstLoad == true)
                            {

                                this.dt_PHIEUNHAP.Rows[0]["PHIEU_NHAPXUATKHO_DCNB_ID"] = result;
                                this.dt_PHIEUNHAP.Rows[0]["MA_HINHTHU_NHAPXUAT"] = "NHAP_NOIBO";
                                this.dt_PHIEUNHAP.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                            }
                            else
                            {
                                this.dt_PHIEUNHAP.Rows[0]["SOPHIEU"] = this.dt_DIEUCHUYEN.Rows[0]["SOPHIEU_DC"];
                                this.dt_PHIEUNHAP.Rows[0]["NGAYLAP"] = this.dt_DIEUCHUYEN.Rows[0]["NGAY_DC"];
                            }
                            DataSet ds_phieunhap_return = null;
                            if (isFirstLoad == true)
                            {
                                ds_phieunhap_return = _IPHIEU_NHAP_XUAT_KHO.InsertKO_PHIEU_NHAPXUATKHO_DCNB_PHIEUNXK(dt_PHIEUNHAP, dt_PHIEU_CTIET_X, CommonDataHelper.UserName, "N");
                            }
                            else
                            {
                                for (int i = dt_PHIEU_CTIET_N.Rows.Count; i < dt_PHIEU_CTIET_X.Rows.Count; i++)
                                {
                                    DataRow dr = dt_PHIEU_CTIET_N.NewRow();
                                    dr["MA_DVIQLY"] = 0;
                                    dr["KHO_ID"] = 0;
                                    dr["KHO_KHUVUC_ID"] = 0;
                                    dr["SOLO"] = 0;
                                    dr["HANDUNG"] = 0;
                                    dr["MA_ITEM_TYPE"] = 0;
                                    dr["SANPHAM_ID"] = 0;
                                    dr["MA_SANPHAM_KH"] = 0;
                                    dr["MA_SANPHAM"] = 0;
                                    dr["TEN_SANPHAM"] = 0;
                                    dr["QUYCACHDONGGOI"] = 0;
                                    dr["MA_DONVI_TINH"] = 0;
                                    dr["SO_LUONG_TONG"] = 0;
                                    dr["SO_LUONG_THUNG"] = 0;
                                    dr["SO_LUONG_LE"] = 0;
                                    dr["SOLUONG_QUYDOI"] = 0;

                                    dr["MA_TINHTRANG_HANG"] = 0;
                                    dr["SODK"] = 0;
                                    dr["DONGIA"] = 0;
                                    dr["THANHTIEN"] = 0;
                                    dr["PALLET_ID"] = 0;
                                    dr["VITRI"] = 0;
                                    dr["MA_VACH"] = 0;
                                    dr["NGAY_SANXUAT"] = 0;
                                    dr["MA_TINHTRANG_CV"] = 0;
                                    dr["IS_NHAPNHIEULAN"] = 0;
                                    dr["SO_LUONG_TONG_THUCNHAP"] = 0;
                                    dr["SO_LUONG_THUNG_THUCNHAP"] = 0;
                                    dr["SO_LUONG_LE_THUCNHAP"] = 0;
                                    dt_PHIEU_CTIET_N.Rows.Add(dr);
                                }
                                for (int i = 0; i < dt_PHIEU_CTIET_X.Rows.Count; i++)
                                {
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_DVIQLY"] = dt_PHIEU_CTIET_X.Rows[i]["MA_DVIQLY"];
                                    dt_PHIEU_CTIET_N.Rows[i]["KHO_ID"] = dt_PHIEU_CTIET_X.Rows[i]["KHO_ID"];
                                    dt_PHIEU_CTIET_N.Rows[i]["KHO_KHUVUC_ID"] = dt_PHIEU_CTIET_X.Rows[i]["KHO_KHUVUC_ID"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SOLO"] = dt_PHIEU_CTIET_X.Rows[i]["SOLO"];
                                    dt_PHIEU_CTIET_N.Rows[i]["HANDUNG"] = dt_PHIEU_CTIET_X.Rows[i]["HANDUNG"];
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_ITEM_TYPE"] = dt_PHIEU_CTIET_X.Rows[i]["MA_ITEM_TYPE"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SANPHAM_ID"] = dt_PHIEU_CTIET_X.Rows[i]["SANPHAM_ID"];
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_SANPHAM_KH"] = dt_PHIEU_CTIET_X.Rows[i]["MA_SANPHAM_KH"];
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_SANPHAM"] = dt_PHIEU_CTIET_X.Rows[i]["MA_SANPHAM"];
                                    dt_PHIEU_CTIET_N.Rows[i]["TEN_SANPHAM"] = dt_PHIEU_CTIET_X.Rows[i]["TEN_SANPHAM"];
                                    dt_PHIEU_CTIET_N.Rows[i]["QUYCACHDONGGOI"] = dt_PHIEU_CTIET_X.Rows[i]["QUYCACHDONGGOI"];
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_DONVI_TINH"] = dt_PHIEU_CTIET_X.Rows[i]["MA_DONVI_TINH"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SO_LUONG_TONG"] = dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_TONG"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SO_LUONG_THUNG"] = dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_THUNG"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SO_LUONG_LE"] = dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_LE"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SOLUONG_QUYDOI"] = dt_PHIEU_CTIET_X.Rows[i]["SOLUONG_QUYDOI"];

                                    dt_PHIEU_CTIET_N.Rows[i]["MA_TINHTRANG_HANG"] = dt_PHIEU_CTIET_X.Rows[i]["MA_TINHTRANG_HANG"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SODK"] = dt_PHIEU_CTIET_X.Rows[i]["SODK"];
                                    dt_PHIEU_CTIET_N.Rows[i]["DONGIA"] = dt_PHIEU_CTIET_X.Rows[i]["DONGIA"];
                                    dt_PHIEU_CTIET_N.Rows[i]["THANHTIEN"] = dt_PHIEU_CTIET_X.Rows[i]["THANHTIEN"];
                                    dt_PHIEU_CTIET_N.Rows[i]["PALLET_ID"] = dt_PHIEU_CTIET_X.Rows[i]["PALLET_ID"];
                                    dt_PHIEU_CTIET_N.Rows[i]["VITRI"] = dt_PHIEU_CTIET_X.Rows[i]["VITRI"];
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_VACH"] = dt_PHIEU_CTIET_X.Rows[i]["MA_VACH"];
                                    dt_PHIEU_CTIET_N.Rows[i]["NGAY_SANXUAT"] = dt_PHIEU_CTIET_X.Rows[i]["NGAY_SANXUAT"];
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_TINHTRANG_CV"] = dt_PHIEU_CTIET_X.Rows[i]["MA_TINHTRANG_CV"];
                                    dt_PHIEU_CTIET_N.Rows[i]["IS_NHAPNHIEULAN"] = dt_PHIEU_CTIET_X.Rows[i]["IS_NHAPNHIEULAN"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SO_LUONG_TONG_THUCNHAP"] = dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_TONG_THUCNHAP"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SO_LUONG_THUNG_THUCNHAP"] = dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_THUNG_THUCNHAP"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SO_LUONG_LE_THUCNHAP"] = dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_LE_THUCNHAP"];
                                }
                                ds_phieunhap_return = _IPHIEU_NHAP_XUAT_KHO.InsertKO_PHIEU_NHAPXUATKHO_DCNB_PHIEUNXK(dt_PHIEUNHAP, dt_PHIEU_CTIET_N, CommonDataHelper.UserName, "N");
                            }
                            if ((ds_phieunhap_return != null) && (ds_phieunhap_return.Tables.Count >= 2))
                                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                            else
                            {
                                base.ShowMessage(MessageType.Error, "Lưu không thành công");
                                return;
                            }
                        }
                        else
                        {
                            base.ShowMessage(MessageType.Error, "Lưu không thành công");
                            return;
                        }
                    }
                    else
                    {
                        if (isFirstLoad == true)
                        {
                            this.dt_PHIEUXUAT.Rows[0]["PHIEU_NHAPXUATKHO_DCNB_ID"] = result;
                            this.dt_PHIEUXUAT.Rows[0]["MA_HINHTHU_NHAPXUAT"] = "XUAT_NOIBO";
                            this.dt_PHIEUXUAT.Rows[0]["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                        }
                        DataSet ds_phieuxuat_return = null;
                        ds_phieuxuat_return = _IPHIEU_NHAP_XUAT_KHO.InsertKO_PHIEU_NHAPXUATKHO_DCNB_PHIEUNXK(dt_PHIEUXUAT, dt_PHIEU_CTIET_X, CommonDataHelper.UserName, "X");
                        if ((ds_phieuxuat_return != null) && (ds_phieuxuat_return.Tables.Count >= 2))
                        {
                            if(pDA_CHUYEN == "CHUA")
                            {
                                for (int i = dt_PHIEUNHAP.Rows.Count; i < dt_PHIEUXUAT.Rows.Count; i++)
                                {
                                    DataRow dr = dt_PHIEUNHAP.NewRow();
                                    dr["MA_DVIQLY"] = 0;
                                    dr["PHIEU_NHAPXUATKHO_ID"] = 0;
                                    dr["PHIEU_NHAPXUATKHO_DCNB_ID"] = 0;
                                    dr["X_OR_N"] = "N";
                                    dr["MA_HINHTHU_NHAPXUAT"] = 0;
                                    dr["SOPHIEU"] = 0;
                                    dr["NGAYLAP"] = 0;
                                    dt_PHIEUNHAP.Rows.Add(dr);
                                }
                                for (int i = 0; i < dt_PHIEUXUAT.Rows.Count; i++)
                                {
                                    dt_PHIEUNHAP.Rows[i]["MA_DVIQLY"] = dt_PHIEUXUAT.Rows[i]["MA_DVIQLY"];
                                    dt_PHIEUNHAP.Rows[i]["X_OR_N"] = "N";
                                    dt_PHIEUNHAP.Rows[i]["PHIEU_NHAPXUATKHO_DCNB_ID"] = dt_PHIEUXUAT.Rows[i]["PHIEU_NHAPXUATKHO_DCNB_ID"];
                                    dt_PHIEUNHAP.Rows[i]["MA_HINHTHU_NHAPXUAT"] = "NHAP_NOIBO";
                                    dt_PHIEUNHAP.Rows[i]["SOPHIEU"] = dt_PHIEUXUAT.Rows[i]["SOPHIEU"];
                                    dt_PHIEUNHAP.Rows[i]["NGAYLAP"] = dt_PHIEUXUAT.Rows[i]["NGAYLAP"];
                                }
                                for (int i = dt_PHIEU_CTIET_N.Rows.Count; i < dt_PHIEU_CTIET_X.Rows.Count; i++)
                                {
                                    DataRow dr = dt_PHIEU_CTIET_N.NewRow();
                                    dr["MA_DVIQLY"] = 0;
                                    dr["KHO_ID"] = 0;
                                    dr["KHO_KHUVUC_ID"] = 0;
                                    dr["SOLO"] = 0;
                                    dr["HANDUNG"] = 0;
                                    dr["MA_ITEM_TYPE"] = 0;
                                    dr["SANPHAM_ID"] = 0;
                                    dr["MA_SANPHAM_KH"] = 0;
                                    dr["MA_SANPHAM"] = 0;
                                    dr["TEN_SANPHAM"] = 0;
                                    dr["QUYCACHDONGGOI"] = 0;
                                    dr["MA_DONVI_TINH"] = 0;
                                    dr["SO_LUONG_TONG"] = 0;
                                    dr["SO_LUONG_THUNG"] = 0;
                                    dr["SO_LUONG_LE"] = 0;
                                    dr["SOLUONG_QUYDOI"] = 0;

                                    dr["MA_TINHTRANG_HANG"] = 0;
                                    dr["SODK"] = 0;
                                    dr["DONGIA"] = 0;
                                    dr["THANHTIEN"] = 0;
                                    dr["PALLET_ID"] = 0;
                                    dr["VITRI"] = 0;
                                    dr["MA_VACH"] = 0;
                                    dr["NGAY_SANXUAT"] = 0;
                                    dr["MA_TINHTRANG_CV"] = 0;
                                    dr["IS_NHAPNHIEULAN"] = 0;
                                    dr["SO_LUONG_TONG_THUCNHAP"] = 0;
                                    dr["SO_LUONG_THUNG_THUCNHAP"] = 0;
                                    dr["SO_LUONG_LE_THUCNHAP"] = 0;
                                    dt_PHIEU_CTIET_N.Rows.Add(dr);
                                }
                                for (int i = 0; i < dt_PHIEU_CTIET_X.Rows.Count; i++)
                                {
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_DVIQLY"] = dt_PHIEU_CTIET_X.Rows[i]["MA_DVIQLY"];
                                    dt_PHIEU_CTIET_N.Rows[i]["KHO_ID"] = dt_PHIEU_CTIET_X.Rows[i]["KHO_ID"];
                                    dt_PHIEU_CTIET_N.Rows[i]["KHO_KHUVUC_ID"] = dt_PHIEU_CTIET_X.Rows[i]["KHO_KHUVUC_ID"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SOLO"] = dt_PHIEU_CTIET_X.Rows[i]["SOLO"];
                                    dt_PHIEU_CTIET_N.Rows[i]["HANDUNG"] = dt_PHIEU_CTIET_X.Rows[i]["HANDUNG"];
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_ITEM_TYPE"] = dt_PHIEU_CTIET_X.Rows[i]["MA_ITEM_TYPE"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SANPHAM_ID"] = dt_PHIEU_CTIET_X.Rows[i]["SANPHAM_ID"];
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_SANPHAM_KH"] = dt_PHIEU_CTIET_X.Rows[i]["MA_SANPHAM_KH"];
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_SANPHAM"] = dt_PHIEU_CTIET_X.Rows[i]["MA_SANPHAM"];
                                    dt_PHIEU_CTIET_N.Rows[i]["TEN_SANPHAM"] = dt_PHIEU_CTIET_X.Rows[i]["TEN_SANPHAM"];
                                    dt_PHIEU_CTIET_N.Rows[i]["QUYCACHDONGGOI"] = dt_PHIEU_CTIET_X.Rows[i]["QUYCACHDONGGOI"];
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_DONVI_TINH"] = dt_PHIEU_CTIET_X.Rows[i]["MA_DONVI_TINH"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SO_LUONG_TONG"] = dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_TONG"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SO_LUONG_THUNG"] = dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_THUNG"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SO_LUONG_LE"] = dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_LE"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SOLUONG_QUYDOI"] = dt_PHIEU_CTIET_X.Rows[i]["SOLUONG_QUYDOI"];

                                    dt_PHIEU_CTIET_N.Rows[i]["MA_TINHTRANG_HANG"] = dt_PHIEU_CTIET_X.Rows[i]["MA_TINHTRANG_HANG"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SODK"] = dt_PHIEU_CTIET_X.Rows[i]["SODK"];
                                    dt_PHIEU_CTIET_N.Rows[i]["DONGIA"] = dt_PHIEU_CTIET_X.Rows[i]["DONGIA"];
                                    dt_PHIEU_CTIET_N.Rows[i]["THANHTIEN"] = dt_PHIEU_CTIET_X.Rows[i]["THANHTIEN"];
                                    dt_PHIEU_CTIET_N.Rows[i]["PALLET_ID"] = dt_PHIEU_CTIET_X.Rows[i]["PALLET_ID"];
                                    dt_PHIEU_CTIET_N.Rows[i]["VITRI"] = dt_PHIEU_CTIET_X.Rows[i]["VITRI"];
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_VACH"] = dt_PHIEU_CTIET_X.Rows[i]["MA_VACH"];
                                    dt_PHIEU_CTIET_N.Rows[i]["NGAY_SANXUAT"] = dt_PHIEU_CTIET_X.Rows[i]["NGAY_SANXUAT"];
                                    dt_PHIEU_CTIET_N.Rows[i]["MA_TINHTRANG_CV"] = dt_PHIEU_CTIET_X.Rows[i]["MA_TINHTRANG_CV"];
                                    dt_PHIEU_CTIET_N.Rows[i]["IS_NHAPNHIEULAN"] = dt_PHIEU_CTIET_X.Rows[i]["IS_NHAPNHIEULAN"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SO_LUONG_TONG_THUCNHAP"] = dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_TONG_THUCNHAP"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SO_LUONG_THUNG_THUCNHAP"] = dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_THUNG_THUCNHAP"];
                                    dt_PHIEU_CTIET_N.Rows[i]["SO_LUONG_LE_THUCNHAP"] = dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_LE_THUCNHAP"];
                                }
                                
                                DataSet ds_phieunhap_return = null;
                                ds_phieunhap_return = _IPHIEU_NHAP_XUAT_KHO.InsertKO_PHIEU_NHAPXUATKHO_DCNB_PHIEUNXK(dt_PHIEUNHAP, dt_PHIEU_CTIET_N, CommonDataHelper.UserName, "N");
                                if (ds_phieunhap_return == null)
                                    return;
                            }
                            ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                        }
                        else
                        {
                            base.ShowMessage(MessageType.Error, "Lưu không thành công");
                            return;
                        }
                    }
                }
                else
                {
                    base.ShowMessage(MessageType.Error, "Lưu không thành công");
                    return;
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void btnChapNhan_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
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
                xDicUser.Add("PHIEU_NHAPXUATKHO_DCNB_ID", typeof(decimal));
                xDicUser.Add("LOAI_DC", typeof(string));
                xDicUser.Add("TENLOAI_DC", typeof(string));
                xDicUser.Add("SOPHIEU_DC", typeof(string));
                xDicUser.Add("NGAY_DC", typeof(string));
                xDicUser.Add("NGUOI_DC", typeof(string));
                xDicUser.Add("SOPHIEU_NHAN", typeof(string));
                xDicUser.Add("NGUOI_NHAN", typeof(string));
                xDicUser.Add("NGAY_NHAN", typeof(string));
                xDicUser.Add("MA_DVIQLY_CHUYEN", typeof(string));
                xDicUser.Add("MA_DVIQLY_NHAN", typeof(string));
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
        private DataTable TableSchemaDefineBingding_PHIEUNX_KHO()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("PHIEU_NHAPXUATKHO_ID", typeof(decimal));
                xDicUser.Add("PHIEUYEUCAU_NHAPXUATKHO_ID", typeof(decimal));
                xDicUser.Add("PHIEU_NHAPXUATKHO_DCNB_ID", typeof(decimal));
                xDicUser.Add("KHACHHANG_NCC_ID", typeof(decimal));
                xDicUser.Add("X_OR_N", typeof(string));
                xDicUser.Add("TEN_KH", typeof(string));
                xDicUser.Add("SO_HOPDONG", typeof(string));
                xDicUser.Add("TEN_DONVI_VANCHUYEN", typeof(string));
                xDicUser.Add("DIADIEM_GIAO", typeof(string));
                xDicUser.Add("KHACHHANG_NCC_DONVI_VANCHUYEN_ID", typeof(decimal));
                xDicUser.Add("KHACHHANG_NCC_NOI_XUATHANG_ID", typeof(decimal));
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
                xDicUser.Add("TEN_KHO", typeof(string));
                xDicUser.Add("KHO_KHUVUC_ID", typeof(decimal));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("SOLO", typeof(string));
                xDicUser.Add("HANDUNG", typeof(string));
                xDicUser.Add("QUYCACHDONGGOI", typeof(string));
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
                this.iGridHelper = new GridHelper(this.grdCT_PHIEU_NXKHO, this.grdViewCT_PHIEU_NXKHO, false);
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
                xColumn.Width = 150;
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

                xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_QuyCachDongGoi"]));
                xColumn.Width = 150;
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

                xColumn = new Column("SODK", Convert.ToString(UtilLanguage.ApplyLanguage()["lbSDK"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("DONGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_GiaNhap"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("THANHTIEN", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_popup_ThanhTien"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;"; ;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

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
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_VACH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTaoMaVach"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAY_SANXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgaySanXuat"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);


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
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SOLUONG_QUYDOI", "Qui đổi");
                xColumn.Width = 80;
                xColumn.Visible = false;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                var converter = new System.Windows.Media.BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#F2F2F2");

                this.grdViewCT_PHIEU_NXKHO.AddFormatCondition(new FormatCondition()
                {
                    FieldName = "SO_LUONG_THUNG",
                    Format = new Format() { Background = brush },
                    Expression = "[SOLUONG_QUYDOI] == null", // #
                    ApplyToRow = false
                });

                this.grdViewCT_PHIEU_NXKHO.AddFormatCondition(new FormatCondition()
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
        /// <summary>
        /// SetIsNull
        /// </summary>
        private bool SetIsNull()
        {
            if(cboMA_DVIQLY_CHUYEN.SelectedIndex == 0)
            {
                base.ShowMessage(MessageType.Information, "[ĐƠN VỊ CHUYỂN] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return false;
            }
            if (txtSOPHIEU_DC.Text.Trim() == "")
            {
                base.ShowMessage(MessageType.Information, "[SỐ PHIẾU ĐIỀU CHUYỂN] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                txtSOPHIEU_DC.Focus();
                return false;
            }
            if (ConstCommon.check_String_Unicode(txtSOPHIEU_DC.Text.Trim()) == false)
            {
                base.ShowMessage(MessageType.Information, "[SỐ PHIẾU ĐIỀU CHUYỂN] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteSYMBOL"]));
                txtSOPHIEU_DC.Focus();
                return false;
            }
            if (cboMA_DVIQLY_NHAN.SelectedIndex == 0)
            {
                base.ShowMessage(MessageType.Information, "[ĐƠN VỊ NHẬN] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return false;
            }
            if(txtNGUOI_DC.Text.Trim() == "")
            {
                base.ShowMessage(MessageType.Information, "[NGƯỜI ĐIỀU CHUYỂN] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return false;
            }
            if (dtNgayDC.Text.Trim() == "")
            {
                base.ShowMessage(MessageType.Information, "[NGÀY ĐIỀU CHUYỂN]" + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                dtNgayDC.Focus();
                return false;
            }
            return true;
        }
        private void SetIsNullGird()
        {
            for (int i = 0; i < dt_PHIEU_CTIET_X.Rows.Count; i++)
            {
                if (ConstCommon.NVL_NUM_NT_NEW(dt_PHIEU_CTIET_X.Rows[i]["SO_LUONG_TONG"]) == 0)
                {
                    base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG TỔNG] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    return;
                }
                if (dt_PHIEU_CTIET_X.Rows[i]["MA_TINHTRANG_HANG"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "Chưa chọn [TRẠNG THÁI HÀNG].");
                    return;
                }
                if (dt_PHIEU_CTIET_X.Rows[i]["VITRI"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "Chưa chọn [VỊ TRÍ HÀNG].");
                    return;
                }
                if (dt_PHIEU_CTIET_X.Rows[i]["MA_TINHTRANG_CV"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "Chưa chọn [TÌNH TRẠNG CV].");
                    return;
                }
            }
        }
        /// <summary>
        /// btnSave_Click
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}