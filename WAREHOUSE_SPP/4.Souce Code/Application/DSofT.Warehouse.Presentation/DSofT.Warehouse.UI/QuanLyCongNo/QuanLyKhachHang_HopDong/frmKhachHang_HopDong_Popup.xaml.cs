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
using DevExpress.Xpf.Editors;
using System.Globalization;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Ngo Gia Thien
    ///  Create date: 31/01/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: quan ly hop dong khach hang nha cung cap popup
    /// </summary>
    public partial class frmKhachHang_HopDong_Popup : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource;
        DataRow iRowSelGb = null;
        DataTable DT_GetData = null;
        I_QuanLy_CongNo_KH_NCC_Bussiness _QuanLy_CongNo_KH_NCC_Bussiness;
        bool status = true;
        string ngayBD;
        string ngayKy;
        string ngayKT;
        string HAN_TT;
        public frmKhachHang_HopDong_Popup()
        {
            _QuanLy_CongNo_KH_NCC_Bussiness = new QuanLy_CongNo_KH_NCC_Bussiness(string.Empty);
            InitializeComponent();
            Initialize_Grid();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            DT_GetData= this.TableSchemaDefineBingding();
            LoadData();
            loadComboBox();
            txtgiatri.Text= "100000000000";
            if (frmKhachHang_HopDong.status==false)
            {
                if(iGridDataSource.Rows.Count > 0 && iGridDataSource!=null)
                {
                    txtma.IsHitTestVisible = false;
                    txtTen.IsHitTestVisible = false;
                    loadtxtbox();
                }
            }
            
        }

        

        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;


                xColumn = new Column("Id", "Id");
                xColumn.Visible = false;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SO_HOPDONG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoHD"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAYKY", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgayKy"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GIATRI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaTri"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                this.iGridHelper.Add(xColumn);

                if(frmKhachHang_HopDong.Loai_KH_NCC=="KH")
                {
                    xColumn = new Column("GiaTriTT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaTriDT"]));
                    xColumn.Width = 150;
                    xColumn.Visible = true;
                    xColumn.MaskType = MaskType.Numeric;
                    xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                    this.iGridHelper.Add(xColumn);
                }
                else
                {
                    xColumn = new Column("GiaTriTT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGiaTriDC"]));
                    xColumn.Width = 150;
                    xColumn.Visible = true;
                    xColumn.MaskType = MaskType.Numeric;
                    xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                    xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                    xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                    this.iGridHelper.Add(xColumn);
                }

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["frmMenu_MenuRemark"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TienDo", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTienDo"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaKH"]));
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("TEN_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenKH"]));
                xColumn.Width = 300;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAY_BDAU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenKH"]));
                xColumn.Width = 300;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAY_KTHUC", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenKH"]));
                xColumn.Width = 300;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("HAN_TTOAN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenKH"]));
                xColumn.Width = 300;
                xColumn.Visible = false;
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
                xDicUser.Add("HOPDONG_ID", typeof(int));
                xDicUser.Add("KHACHHANG_NCC_ID", typeof(int));
                xDicUser.Add("SO_HOPDONG", typeof(string));
                xDicUser.Add("NGAYKY", typeof(string));
                xDicUser.Add("NGAY_BDAU", typeof(string));
                xDicUser.Add("NGAY_KTHUC", typeof(string));
                xDicUser.Add("GIATRI", typeof(decimal));
                xDicUser.Add("HAN_TTOAN", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("MA_KH", typeof(string));
                xDicUser.Add("TEN_KH", typeof(string));
                xDicUser.Add("GiaTriTT", typeof(string));
                xDicUser.Add("TienDo", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }

        public void loadComboBox()
        {
            try
            {
                DataTable DMKH = _QuanLy_CongNo_KH_NCC_Bussiness.GetAll(ConstCommon.DonViQuanLy,frmKhachHang_HopDong.Loai_KH_NCC);
                if (DMKH != null && DMKH.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(this.txtma, "MA_KH", "MA_KH", DMKH, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(txtma, Convert.ToString(UtilLanguage.ApplyLanguage()[/*"lblNSX"*/"lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["MA_KH"] = txtma.GetKeyValue(0);

                    ComboBoxUtil.SetComboBoxEdit(this.txtTen, "TEN_KH", "MA_KH", DMKH, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(txtTen, Convert.ToString(UtilLanguage.ApplyLanguage()[/*"lblNSX"*/"lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["MA_KH"] =txtTen.GetKeyValue(0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        int i;
                        if (frmKhachHang_HopDong.status == false)
                            i = frmKhachHang_HopDong.KH_NCC_ID;
                        else
                            i = 0;
                        iGridDataSource = _QuanLy_CongNo_KH_NCC_Bussiness.GetHD(ConstCommon.DonViQuanLy, i);
                        return _QuanLy_CongNo_KH_NCC_Bussiness.GetHD(ConstCommon.DonViQuanLy, i);  
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

        void loadtxtbox()
        {
            if (iGridDataSource.Rows.Count > 0 && iGridDataSource != null)
            {
                iRowSelGb = iGridDataSource.Rows[0];
                txtma.Text = iRowSelGb["MA_KH"].ToString();
                txtTen.Text = iRowSelGb["TEN_KH"].ToString();
                txtSoHD.Text = iRowSelGb["SO_HOPDONG"].ToString();
                txtgiatri.Text = iRowSelGb["GIATRI"].ToString();
                txtghichu.Text = iRowSelGb["GHI_CHU"].ToString();
                DispalayRequest();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
        private void SaveData()
        {
            bool result = _QuanLy_CongNo_KH_NCC_Bussiness.Insert(DT_GetData);
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

        void SetFocus()
        {
            if(txtTen.Text=="--Chọn--")
            {
                base.ShowMessage(MessageType.Information, "KHÁCH HÀNG/NHÀ CUNG CẤP " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if(txtSoHD.Text==string.Empty)
            {
                base.ShowMessage(MessageType.Information, "SỐ HỢP ĐỒNG " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if(txtgiatri.Text==string.Empty)
            {
                base.ShowMessage(MessageType.Information, "GÍA TRỊ " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if(dtpngayky.Text==string.Empty)
            {
                base.ShowMessage(MessageType.Information, "NGÀY KÝ " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if (dtpNgayBDAU.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "NGÀY BẮT ĐẦU " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if (dtpNgayKTHUC.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "NGÀY KẾT THÚC " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if (dtpHAN_TT.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "HẠN THANH TOÁN " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtTen.Text == "--Chọn--" || txtSoHD.Text == string.Empty || txtgiatri.Text == string.Empty || dtpngayky.Text == string.Empty || dtpNgayBDAU.Text == string.Empty || dtpNgayKTHUC.Text == string.Empty || dtpHAN_TT.Text == string.Empty)
                SetFocus();
            else
            {
                status = true;
                bool flag = CheckDay_bd_kt();
                if (flag == false)
                {
                    base.ShowMessage(MessageType.Information, "NGÀY KẾT THÚC KHÔNG ĐƯỢC NHỎ HƠN NGÀY BẮT ĐẦU ");
                    return;
                }
                flag = CheckDay_ngky_hanTT();
                if (flag == false)
                {
                    base.ShowMessage(MessageType.Information, "HẠN THANH TOÁN KHÔNG ĐƯỢC NHỎ HƠN NGÀY KÝ ");
                    return;
                }
                GetData();
                DataTable dt = _QuanLy_CongNo_KH_NCC_Bussiness.ChekHD(ConstCommon.DonViQuanLy, DT_GetData.Rows[0]["SO_HOPDONG"].ToString());
                if(dt.Rows.Count >0)
                {
                    base.ShowMessage(MessageType.Information, "SỐ HỢP ĐỒNG ĐÃ TỒN TẠI ");
                    return;
                }
                else
                {
                    SaveData();
                    LoadData();
                }
                
            }
            
        }

        private void GetData()
        {
            DT_GetData.Clear();
            DataRow dr = DT_GetData.NewRow();
            if(status==false)
            { dr["HOPDONG_ID"] = iRowSelGb["HOPDONG_ID"].ToString();
            }
                
            dr["MA_KH"] = txtma.Text;
            dr["TEN_KH"] = txtTen.Text;
            dr["SO_HOPDONG"] = txtSoHD.Text;
            dr["GIATRI"] = decimal.Parse( txtgiatri.Text);
            dr["GHI_CHU"] = txtghichu.Text;
            dr["NGAYKY"] = DateTime.ParseExact(dtpngayky.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString().Substring(0, 10);
            dr["NGAY_BDAU"] = DateTime.ParseExact(dtpNgayBDAU.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString().Substring(0, 10);
            dr["NGAY_KTHUC"] = DateTime.ParseExact(dtpNgayKTHUC.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString().Substring(0, 10);
            dr["HAN_TTOAN"] = DateTime.ParseExact(dtpHAN_TT.EditValue.ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString().Substring(0, 10);
            DT_GetData.Rows.Add(dr);
        }

        private void grdView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (iDataSource.Rows.Count == 0) return;
                iRowSelGb = ((DataRowView)this.grd.GetFocusedRow()).Row;
                txtma.Text = iRowSelGb["MA_KH"].ToString();
                txtTen.Text = iRowSelGb["TEN_KH"].ToString();
                txtSoHD.Text = iRowSelGb["SO_HOPDONG"].ToString();
                txtgiatri.Text = iRowSelGb["GIATRI"].ToString();
                txtghichu.Text = iRowSelGb["GHI_CHU"].ToString();
                //dtpngayky.Text = DateTime.Parse( iRowSelGb["NGAYKY"].ToString(), CultureInfo.InvariantCulture).ToString();
                // dtpNgayBDAU.Text = iRowSelGb["NGAY_BDAU"].ToString();
                // dtpNgayKTHUC.Text = iRowSelGb["NGAY_KTHUC"].ToString();
                // dtpHAN_TT.Text = iRowSelGb["HAN_TTOAN"].ToString();
                DispalayRequest();
            }
            catch (Exception ex)
            { return; }
        }

        private void UpdateData()
        {
            bool result = _QuanLy_CongNo_KH_NCC_Bussiness.Update(DT_GetData);
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(frmKhachHang_HopDong.status==true)
            {
                return;
            }
            if (txtTen.Text == "--Chọn--" || txtSoHD.Text == string.Empty || txtgiatri.Text == string.Empty || dtpngayky.Text == string.Empty || dtpNgayBDAU.Text == string.Empty || dtpNgayKTHUC.Text == string.Empty || dtpHAN_TT.Text == string.Empty)
                SetFocus();
            else
            {
                status = false;
                bool flag=CheckDay_bd_kt();
                if(flag==false)
                {
                    base.ShowMessage(MessageType.Information, "NGÀY KẾT THÚC KHÔNG ĐƯỢC NHỎ HƠN NGÀY BẮT ĐẦU ");
                    return;
                }
                flag=CheckDay_ngky_hanTT();
                if (flag == false)
                {
                    base.ShowMessage(MessageType.Information, "HẠN THANH TOÁN KHÔNG ĐƯỢC NHỎ HƠN NGÀY KÝ ");
                    return;
                }
                GetData();
                if (iRowSelGb["SO_HOPDONG"].ToString()==txtSoHD.Text)
                {
                    UpdateData();
                    LoadData();
                }
                else
                {
                    DataTable dt = _QuanLy_CongNo_KH_NCC_Bussiness.ChekHD(ConstCommon.DonViQuanLy, DT_GetData.Rows[0]["SO_HOPDONG"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        base.ShowMessage(MessageType.Information, "SỐ HỢP ĐỒNG ĐÃ TỒN TẠI ");
                        return;
                    }
                    else
                    {
                        UpdateData();
                        LoadData();
                    }
                }
            }
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.grd.GetFocusedRow() == null) return;
                iRowSelGb = null;
                if (!base.ShowMessage(MessageType.OkCancel, Convert.ToString(UtilLanguage.ApplyLanguage()["lblcomfrimDelete"])))
                    return;
                iRowSelGb = ((DataRowView)grd.GetFocusedRow()).Row;
                bool result = _QuanLy_CongNo_KH_NCC_Bussiness.Delete(ConstCommon.DonViQuanLy,int.Parse(iRowSelGb["HOPDONG_ID"].ToString()));
                if (!result)
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                    return;
                }
                else
                {
                    ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                }
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnDelete.Tag.ToString());
                base.ShowMessage(MessageType.Error, "Vui lòng chọn dữ liệu cần xóa!", ex);
            }
        }

        bool CheckDay_bd_kt()
        {
            string a= dtpNgayBDAU.Text.Substring(0, 2);
            string b = dtpNgayBDAU.Text.Substring(3, 2);
            string c = dtpNgayBDAU.Text.Substring(6, 4);
            ngayBD = c + "/" + a + "/" + b;

            a = dtpNgayKTHUC.Text.Substring(0, 2);
            b = dtpNgayKTHUC.Text.Substring(3, 2);
            c = dtpNgayKTHUC.Text.Substring(6, 4);
            ngayKT = c + "/" + a + "/" + b;

            if (string.Compare(ngayBD, ngayKT) > 0)
            {
                return false;
            }
            else
                return true;
        }
        bool CheckDay_ngky_hanTT()
        {
            string a = dtpngayky.Text.Substring(0, 2);
            string b = dtpngayky.Text.Substring(3, 2);
            string c = dtpngayky.Text.Substring(6, 4);
            ngayKy =  c + "/" + a + "/" + b;

            a = dtpHAN_TT.Text.Substring(0, 2);
            b = dtpHAN_TT.Text.Substring(3, 2);
            c = dtpHAN_TT.Text.Substring(6, 4);
            HAN_TT = c + "/" + a + "/" + b;

            if (string.Compare(ngayKy, HAN_TT) > 0)
            {
                return false;
            }
            else
                return true;
        }


    }

}
