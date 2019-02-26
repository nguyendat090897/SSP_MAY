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
using DevExpress.Xpf.Grid;
using DSofT.Framework.UICore.TaskProxy;
namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_dm_khachhang_ncc_poppup.xaml
    /// </summary>
    public partial class frm_dm_khachhang_ncc_popup : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataRow iRowSelGb = null;
        IDM_KHACHHANG_NCCBussiness _DM_KHACHHANG_NCCBussiness;
        string pMA_KH = String.Empty;
        public static long pKHACHHANG_NCC_ID = 0;
        public static string pTEN_KHNCC = String.Empty;
        public static string pLOAIKH_NCC = String.Empty;
        public frm_dm_khachhang_ncc_popup()
        {
            InitializeComponent();
            _DM_KHACHHANG_NCCBussiness = new DM_KHACHHANG_NCCBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadComboBox();
            rdbKSD.IsChecked = true;
        }
        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    UpdateTinhTrang();
                    UpdateLoaiKHNhaCC();
                    pMA_KH = this.iDataSource.Rows[0]["MA_KH"].ToString().Trim();
                    pKHACHHANG_NCC_ID = ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["KHACHHANG_NCC_ID"].ToString().Trim());
                    pTEN_KHNCC = this.iDataSource.Rows[0]["TEN_KH"].ToString().Trim();
                    pLOAIKH_NCC = this.iDataSource.Rows[0]["LOAI_KHNCC"].ToString().Trim();
                }
                if(frm_dm_khachhang_ncc.status == true)
                {
                    btnDiaDiemXuatHang.IsEnabled = false;
                    btnDonViVanChuyen.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("KHACHHANG_NCC_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("LOAI_KHNCC", typeof(string));
                xDicUser.Add("MA_KH", typeof(string));
                xDicUser.Add("TEN_KH", typeof(string));
                xDicUser.Add("DIA_CHI", typeof(string));
                xDicUser.Add("TINH_ID", typeof(decimal));
                xDicUser.Add("MA_SOTHUE", typeof(string));
                xDicUser.Add("TK_NGANHANG", typeof(string));
                xDicUser.Add("TEN_NGANHANG", typeof(string));
                xDicUser.Add("DIENTHOAI", typeof(string));
                xDicUser.Add("FAX", typeof(string));
                xDicUser.Add("LOAI_KHACHHANG_ID", typeof(decimal));
                xDicUser.Add("NGUOI_LIENHE", typeof(string));
                xDicUser.Add("GIOI_TINH", typeof(bool));
                xDicUser.Add("CHUC_DANH", typeof(string));
                xDicUser.Add("DIENTHOAI_CQ", typeof(string));
                xDicUser.Add("DIENTHOAI_NR", typeof(string));
                xDicUser.Add("DIENTHOAI_DD", typeof(string));
                xDicUser.Add("CMND", typeof(string));
                xDicUser.Add("NGAY_CAP", typeof(string));
                xDicUser.Add("NOI_CAP", typeof(string));
                xDicUser.Add("EMAIL", typeof(string));
                xDicUser.Add("DIA_CHI_LHE", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(bool));
                xDicUser.Add("RowVersion", typeof(long));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }
        private void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {

                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadData()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void LoadComboBox()
        {
            try
            {
                DataTable dtGIOI_TINH = new DataTable();
                DataTable dtTINH_THANHPHO = _DM_KHACHHANG_NCCBussiness.GetListDM_KHACHHANG_NCC_TINH_THANHPHO();
                DataTable dtLOAI_KHACH_HANG = _DM_KHACHHANG_NCCBussiness.GetListDM_KHACHHANG_NCC_KHACHHANG(ConstCommon.DonViQuanLy);
                dtGIOI_TINH.Columns.Add("TEN_GIOITINH", typeof(string));
                dtGIOI_TINH.Columns.Add("GIOI_TINH", typeof(bool));
                ComboBoxUtil.SetComboBoxEdit(this.cboGIOI_TINH, "TEN_GIOITINH", "GIOI_TINH", dtGIOI_TINH, SelectionTypeEnum.Native);
               // ComboBoxUtil.InsertItem(this.cboGIOI_TINH, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "true", 0);
                ComboBoxUtil.InsertItem(this.cboGIOI_TINH, "Nam", "true", 0);
                ComboBoxUtil.InsertItem(this.cboGIOI_TINH, "Nữ", "false", 1);
                this.iDataSource.Rows[0]["GIOI_TINH"] = this.cboGIOI_TINH.GetKeyValue(0);
                if (dtTINH_THANHPHO != null && dtTINH_THANHPHO.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(this.cboTINH_ID, "TEN_TINH", "TINH_ID", dtTINH_THANHPHO, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(this.cboTINH_ID, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["TINH_ID"] = this.cboTINH_ID.GetKeyValue(0);
                }
                if (dtLOAI_KHACH_HANG != null && dtLOAI_KHACH_HANG.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(this.cboLOAI_KHACHHANG_ID, "TEN_LOAI_KHACHHANG", "LOAI_KHACHHANG_ID", dtLOAI_KHACH_HANG, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(this.cboLOAI_KHACHHANG_ID, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["LOAI_KHACHHANG_ID"] = this.cboLOAI_KHACHHANG_ID.GetKeyValue(0);
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void SetNewData()
        {
            //this.iDataSource = null;
            this.iDataSource.Rows[0]["KHACHHANG_NCC_ID"] = 0;
            rdbKH.IsChecked = false;
            rdbNCC.IsChecked = false;
            txtMA_KH.Text = String.Empty;
            txtMST.Text = String.Empty;
            txtTEN_KH.Text = String.Empty;
            txtDIA_CHI.Text = String.Empty;
            txtTK_NGANHANG.Text = String.Empty;
            txtTEN_NGANHANG.Text = String.Empty;
            txtDienThoai.Text = String.Empty;
            txtFax.Text = String.Empty;
            txtNGUOI_LIENHE.Text = String.Empty;
            txtCHUC_DANH.Text = String.Empty;
            txtDIENTHOAI_CQ.Text = String.Empty;
            txtDIENTHOAI_NR.Text = String.Empty;
            txtDIENTHOAI_DD.Text = String.Empty;
            txtCMND.Text = String.Empty;
            string s = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            dtpNGAY_CAP.Text = String.Empty;
            txtNOI_CAP.Text = String.Empty;
            txtDIA_CHI_LHE.Text = String.Empty;
            txtGHI_CHU.Text = String.Empty;
            txtEMAIL.Text = String.Empty;
            rdbKSD.IsChecked = true;
            cboGIOI_TINH.SelectedIndex = 0;
            cboTINH_ID.SelectedIndex = 0;
            cboLOAI_KHACHHANG_ID.SelectedIndex = 0;
        }
        private void SetIsNull()
        {
            if (txtMA_KH.Text == String.Empty)
            {
                base.ShowMessage(MessageType.Information, "[MÃ KHÁCH HÀNG] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                txtMA_KH.Focus();
            }
            else if (txtTEN_KH.Text == String.Empty)
            {
                base.ShowMessage(MessageType.Information, "[TÊN KHÁCH HÀNG] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                txtTEN_KH.Focus();
            }
            else if (cboGIOI_TINH.SelectedIndex == -1)
            {
                base.ShowMessage(MessageType.Information, "Chưa chọn [GIỚI TÍNH].");
                cboGIOI_TINH.Focus();
            }
            else if (rdbKH.IsChecked == false && rdbNCC.IsChecked == false)
            {
                base.ShowMessage(MessageType.Information, "Chưa chọn [KHÁCH HÀNG] hoặc [NHÀ CUNG CẤP] ");
            }
            else
                return;
        }
        private void SaveData()
        {
            bool result = _DM_KHACHHANG_NCCBussiness.InsertorUpdateDM_KHACHHANG_NCC(this.iDataSource.Copy());
            if (!result)
            {
                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                return;
            }
            else
            {
                DataTable dt = _DM_KHACHHANG_NCCBussiness.GetKHACHHANG_NCC_ID(ConstCommon.DonViQuanLy, txtMA_KH.Text);
                pKHACHHANG_NCC_ID = ConstCommon.NVL_NUM_LONG_NEW(dt.Rows[0]["KHACHHANG_NCC_ID"].ToString());
                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
            }
            //if(frm_dm_khachhang_ncc.status == false)
            //{
            //    this.Close();
            //}
            btnDiaDiemXuatHang.IsEnabled = true;
            btnDonViVanChuyen.IsEnabled = true;
        }
        private void KTTinhTrang()
        {
            if (rdbKSD.IsChecked == true)
                this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
            else if (rdbSD.IsChecked == true)
                this.iDataSource.Rows[0]["TINH_TRANG"] = 1;
            else
                this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
        }
        private void KTLoaiKHNhaCC()
        {
            if (rdbKH.IsChecked == true)
                this.iDataSource.Rows[0]["LOAI_KHNCC"] = "KH";
            else if (rdbNCC.IsChecked == true)
                this.iDataSource.Rows[0]["LOAI_KHNCC"] = "NCC";
            else
                this.iDataSource.Rows[0]["LOAI_KHNCC"] = String.Empty;
        }
        private void UpdateTinhTrang()
        {
            if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString().Trim() != String.Empty)
            {
                if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString() == "False")
                    rdbKSD.IsChecked = true;
                else
                    rdbSD.IsChecked = true;
            }
        }
        private void UpdateLoaiKHNhaCC()
        {

            if (this.iDataSource.Rows[0]["LOAI_KHNCC"].ToString().Trim() != String.Empty)
            {
                if (this.iDataSource.Rows[0]["LOAI_KHNCC"].ToString() == "KH")
                    rdbKH.IsChecked = true;
                else
                    rdbNCC.IsChecked = true;
            }
        }
        private bool CheckSymbolInMaKH(string str)
        {
            if (str == "")
            {
                return false;
            }
            char[] varChar = str.ToCharArray();
            int i = 0;
            while (i < varChar.Length &&
                ((Convert.ToInt32(varChar[i]) >= 97 && Convert.ToInt32(varChar[i]) <= 122) || (Convert.ToInt32(varChar[i]) >= 65 && Convert.ToInt32(varChar[i]) <= 90)
                || (Convert.ToInt32(varChar[i]) >= 48 && Convert.ToInt32(varChar[i]) <= 57)))
            {
                i++;
            }
            if (i < varChar.Length)
            {
                return false;
            }
            return true;
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frm_dm_khachhang_ncc.status = true;
                SetNewData();
                btnDiaDiemXuatHang.IsEnabled = false;
                btnDonViVanChuyen.IsEnabled = false;
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
        private void btnDiaDiemXuatHang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_DiaDiemXuatHang", null);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void btnDonViVanChuyen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_DonViVanChuyen", null);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rdbKH.IsChecked == true || rdbNCC.IsChecked == true)
                {
                    if (txtMA_KH.Text != String.Empty && txtTEN_KH.Text != String.Empty && cboGIOI_TINH.SelectedIndex != -1)
                    {
                        if (CheckSymbolInMaKH(txtMA_KH.Text) == true)
                        {
                            DataTable dts = _DM_KHACHHANG_NCCBussiness.GetListDM_KHACHHANG_NCC_BY_MAKH(ConstCommon.DonViQuanLy,
                            txtMA_KH.Text);
                            if (frm_dm_khachhang_ncc.status == true && dts != null && dts.Rows.Count > 0)
                            {
                                base.ShowMessage(MessageType.Information, "[MÃ KHÁCH HÀNG/NHÀ CUNG CẤP] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                            }
                            else
                            {
                                if (pMA_KH != this.iDataSource.Rows[0]["MA_KH"].ToString())
                                {
                                    if (dts == null || dts.Rows.Count == 0)
                                    {
                                        SaveData();
                                    }
                                    else
                                    {
                                        base.ShowMessage(MessageType.Information, "[MÃ KHÁCH HÀNG/NHÀ CUNG CẤP] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                                        return;
                                    }
                                }
                                else
                                {
                                    SaveData();
                                }
                            }
                        }
                        else
                        {
                            base.ShowMessage(MessageType.Information, "[MÃ KHÁCH HÀNG/NHÀ CUNG CẤP] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteSYMBOL"]));
                            return;
                        }
                    }
                    else
                        SetIsNull();
                }
                else
                    SetIsNull();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void cboGIOI_TINH_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

        }

        private void cboLOAI_KHACHHANG_ID_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

        }

        private void cboTINH_ID_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

        }

        private void rdbKSD_Click(object sender, RoutedEventArgs e)
        {
            KTTinhTrang();
        }

        private void rdbSD_Click(object sender, RoutedEventArgs e)
        {
            KTTinhTrang();
        }

        private void rdbKH_Click(object sender, RoutedEventArgs e)
        {
            KTLoaiKHNhaCC();
        }

        private void rdbNCC_Click(object sender, RoutedEventArgs e)
        {
            KTLoaiKHNhaCC();
        }
    }
}
