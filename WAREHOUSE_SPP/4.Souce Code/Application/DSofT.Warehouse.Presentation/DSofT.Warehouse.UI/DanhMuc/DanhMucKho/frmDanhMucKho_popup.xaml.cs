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
using DSofT.Framework.UICore.TaskProxy;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 23/01/2018
    ///  Editor:  Ngo Gia Thien
    ///  Modify date: 19/3/2018
    ///  Description: Danh muc kho popup
    /// </summary>
    public partial class frmDanhMucKho_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        IDM_LOAIKHOBussiness DM_LOAIKHOBussiness;
        IDM_KHOBussiness DM_KHOBussiness;
        string pMA_KHO = String.Empty;
        public frmDanhMucKho_popup()
        {
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            DM_KHOBussiness = new DM_KHOBussiness(string.Empty);
            DM_LOAIKHOBussiness = new DM_LOAIKHOBussiness(string.Empty);
            Initialize_Grid();
            //btnKhaiBaoKV.IsEnabled = btnKhaiBaoViTri.IsEnabled = false;
            LoadData();
            loadBTN();
            loadComboBox();
        }

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGb = this.Parameter[0] as DataRow;
                DispalayRequest();
                loadRDB_TinhTrang();
                pMA_KHO = this.iDataSource.Rows[0]["MA_KHO"].ToString().Trim();
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
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("KHO_ID", typeof(decimal));
                xDicUser.Add("MA_KHO", typeof(string));
                xDicUser.Add("TEN_KHO", typeof(string));
                xDicUser.Add("DIA_CHI", typeof(string));
                xDicUser.Add("LOAIKHO_ID", typeof(decimal));
                xDicUser.Add("DKIEN_BQUAN_ID", typeof(decimal));
                xDicUser.Add("SUC_CHUA", typeof(decimal));
                xDicUser.Add("TON_MIN", typeof(decimal));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(Int16));
                xDicUser.Add("RowVersion", typeof(long));
                xDicUser.Add("IsDelete", typeof(bool));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));
                xDicUser.Add("TEN_LOAIKHO", typeof(string));
                xDicUser.Add("TEN_DKIEN_BQUAN", typeof(string));
                xDicUser.Add("UserName", typeof(string));
                xDicUser.Add("FullName", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }

        public void loadBTN()
        {
            if (frmDanhMucKho.status == false)
            {
                btnKhaiBaoKV.IsEnabled = btnKhaiBaoViTri.IsEnabled = true;
            }
            if (frmDanhMucKho.status == true)
            {
                btnKhaiBaoKV.IsEnabled = btnKhaiBaoViTri.IsEnabled = false;
            }
        }


        /// <summary>
        /// Load ComboBox
        /// </summary>
        public void loadComboBox()
        {
            try
            {
                DataTable dtlk = DM_LOAIKHOBussiness.GetListDM_LOAIKHO(ConstCommon.DonViQuanLy);
                DataTable dtdkbq = DM_KHOBussiness.GetListDM_KHO_TENDKBQ(ConstCommon.DonViQuanLy);
                if (dtlk != null && dtlk.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(this.cboLoaiKho, "TEN_LOAIKHO", "LOAIKHO_ID", dtlk, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboLoaiKho, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["LOAIKHO_ID"] = cboLoaiKho.GetKeyValue(0);
                }
                if (dtdkbq != null && dtdkbq.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(this.cboDKBQ, "TEN_DKIEN_BQUAN", "DKIEN_BQUAN_ID", dtdkbq, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboDKBQ, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["DKIEN_BQUAN_ID"] = cboDKBQ.GetKeyValue(0);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Luu vao data
        /// </summary>
        public void LuuRDB_TinhTrang()
        {
            try
            {
                if (rdbSD.IsChecked == true)
                {
                    this.iDataSource.Rows[0]["TINH_TRANG"] = 1;
                }
                if (rdbKSD.IsChecked == true)
                {
                    this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// load tu data
        /// </summary>
        public void loadRDB_TinhTrang()
        {
            try
            {
                if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString() == "1")
                {
                    rdbSD.IsChecked = true;
                }
                if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString() == "0")
                {
                    rdbKSD.IsChecked = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void setFocus()
        {
            if (txtMaKho.Text == string.Empty)
            {
                txtMaKho.Focus();
            }
            else if (txtTenKho.Text == string.Empty)
            {
                txtTenKho.Focus();
            }
            else
            {
                cboLoaiKho.Focus();
            }
        }


        /// <summary>
        /// Kiem tra ky tu dac biet
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool check_String_Unicode(string str)
        {
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

        private bool checkNull()
        {
            if (txtMaKho.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "VUI LÒNG NHẬP THÔNG TIN MÃ KHO");
                txtMaKho.Focus();
                return false;
            }
            if (txtTenKho.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "VUI LÒNG NHẬP THÔNG TIN TÊN KHO ");
                txtTenKho.Focus();
                return false;
            }
            if (cboLoaiKho.Text == "--Chọn--")
            {
                base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN LOẠI KHO ");
                cboLoaiKho.Focus();
                return false;
            }
            if (ConstCommon.check_String_Unicode(txtMaKho.Text) == false)
            {
                base.ShowMessage(MessageType.Information, "VUI LÒNG KHÔNG NHẬP KÝ TỰ ĐẶC BIỆT, KHOẢNG TRỐNG HOẶC CÓ DẤU VÀO Ô MÃ KHO ");
                txtMaKho.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkNull() == false)
                {
                    return;
                }

                DataTable dts = DM_KHOBussiness.GetListDM_KHO_MA(ConstCommon.DonViQuanLy, txtMaKho.Text);

                if (frmDanhMucKho.status == true && dts != null && dts.Rows.Count > 0)
                {
                    base.ShowMessage(MessageType.Information, "MÃ KHO ĐÃ TÔN TẠI. VUI LÒNG NHẬP MÃ KHO KHÁC");
                    txtMaKho.Focus();
                }

                else
                {
                    if (pMA_KHO != this.iDataSource.Rows[0]["MA_KHO"].ToString())
                    {
                        if (dts == null || dts.Rows.Count == 0)
                        {
                            LuuRDB_TinhTrang();
                            bool result = DM_KHOBussiness.InsertOrUpdateDM_KHO(this.iDataSource.Copy());
                            if (!result)
                            {
                                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                                return;
                            }
                            else
                            {
                                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                                btnKhaiBaoViTri.IsEnabled = btnKhaiBaoKV.IsEnabled = true;
                                SaveUser();
                                DataTable dtid = DM_KHOBussiness.GetListDM_KHO_ID(ConstCommon.DonViQuanLy);
                                if (dtid != null && dtid.Rows.Count > 0)
                                {
                                    iRowSelGb = dtid.Rows[0];
                                }


                                return;
                            }
                        }
                        else
                        {
                            base.ShowMessage(MessageType.Information, "MÃ KHO ĐÃ TÔN TẠI. VUI LÒNG NHẬP MÃ KHÁC");
                            txtMaKho.Focus();
                            return;
                        }
                    }
                    else
                    {
                        LuuRDB_TinhTrang();
                        bool result = DM_KHOBussiness.InsertOrUpdateDM_KHO(this.iDataSource.Copy());
                        SaveUser();
                        if (!result)
                        {

                            base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                            return;
                        }
                        else
                        {
                            ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnKhaiBaoKV_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                if (iDataSource.Rows.Count == 0) return;
                if (iRowSelGb != null)
                {
                    object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_khai_bao_khuvuc_theo_kho", iRowSelGb);
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnKhaiBaoKV.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnKhaiBaoViTri_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (iDataSource.Rows.Count == 0) return;
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_khai_bao_vi_tri_kho", iRowSelGb);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            txtMaKho.Text = txtTenKho.Text = txtDiaChi.Text = txtSucChua.Text = txtTonToiThieu.Text = txtGhiChu.Text = string.Empty;
            cboDKBQ.Text = cboLoaiKho.Text = string.Empty;
            rdbKSD.IsChecked = true;
            loadComboBox();
        }

        private void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        if(frmDanhMucKho.status==true)
                             return DM_KHOBussiness.GetListUser(0);
                        else
                            return DM_KHOBussiness.GetListUser(frmDanhMucKho.kho_id);
                    });
                    Action<DataTable> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iGridDataSource = dt;
                            this.iGridHelper.BindItemSource(this.iGridDataSource);
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

        private void SaveUser()
        {
            bool result=false;
           bool result_Del_User = DM_KHOBussiness.Delete_DM_User(frmDanhMucKho.kho_id);
            int count = 0;
            if (result_Del_User)
            {
                for (int i = 0; i < iGridDataSource.Rows.Count; i++)
                {
                    if (iGridDataSource.Rows[i]["CHON"].ToString() == "1")
                    {
                        iDataSource.Rows[0]["UserName"] = iGridDataSource.Rows[i]["UserName"];
                        result = DM_KHOBussiness.InsertOrUpdateDM_KHO_User(this.iDataSource.Copy());
                        count++;
                    }
                }
                if (result == true||count==0)
                {
                    ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                    return;
                }
                else
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                    return;
                }
            }
        }

        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;

                xColumn = new Column("CHON", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_Chon"]));
                xColumn.Width = 30;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("CHON") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                xColumn.AllowSorting = false;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);



                xColumn = new Column("UserName", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phancong_thietbi_User"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("FullName", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phancong_thietbi_TenUser"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                this.iGridHelper.Initialize();

                this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }

}
