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
using System.Globalization;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 6/03/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Phan cong thiet bi
    /// </summary>
    public partial class frm_phancong_thietbi_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        string pPC_TBI_ID = String.Empty;
        I_PHANCONGTHIETBIBussiness _PHANCONGTHIETBI_BUSSINESS;
        DataTable iGridDataSource = null;
        int PCONG_TBI_ID = frm_phancong_thietbi.PCONG_TBI_ID;
        string LoaiTbi = frm_phancong_thietbi.LoaiTBI;
        DataRow[] row = null;
        //DataTable idata_phancongthietbi = null;
        int count = 0;
        public frm_phancong_thietbi_popup()
        {
            InitializeComponent();
            _PHANCONGTHIETBI_BUSSINESS = new PHANCONGTHIETBIBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            Initialize_Grid();
            LoadData();
            loadComboBox();
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

        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    pPC_TBI_ID = this.iDataSource.Rows[0]["PCONG_TBI_ID"].ToString().Trim();
                    updateLoaiThietBi();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void updateLoaiThietBi()
        {
            try
            {
                if (this.iDataSource.Rows[0]["LOAI_TBI"].ToString().Trim() != string.Empty)
                {
                    if (this.iDataSource.Rows[0]["LOAI_TBI"].ToString().Trim() == "XNA")
                    {
                        rdoXeNang.IsChecked = true;
                    }
                    if (this.iDataSource.Rows[0]["LOAI_TBI"].ToString().Trim() == "PDA")
                    {
                        rdoPDA.IsChecked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("PCONG_TBI_ID", typeof(int));
                xDicUser.Add("CHON", typeof(bool));
                xDicUser.Add("THIETBI_ID", typeof(int));
                xDicUser.Add("FullName", typeof(string));
                xDicUser.Add("UserName", typeof(string));
                xDicUser.Add("LOAI_TBI", typeof(string));
                xDicUser.Add("MA_TBI", typeof(string));
                xDicUser.Add("TEN_TBI", typeof(string));
                xDicUser.Add("TU_NGAY", typeof(string));
                xDicUser.Add("DEN_NGAY", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
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
                        if (frm_phancong_thietbi.status == false)
                        {
                            return _PHANCONGTHIETBI_BUSSINESS.GetListLoaiTB(PCONG_TBI_ID, ConstCommon.DonViQuanLy, LoaiTbi);
                            PCONG_TBI_ID = 0;
                        }
                        else
                            return _PHANCONGTHIETBI_BUSSINESS.GetList_TBI(ConstCommon.DonViQuanLy, LoaiTbi);
                    });
                    Action<DataTable> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iGridDataSource = dt;
                            row = iGridDataSource.Select();
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
            finally
            {
                frm_phancong_thietbi.LoaiTBI = null;
            }
        }


        public void loadComboBox()
        {
            try
            {
                DataTable GET_USER = _PHANCONGTHIETBI_BUSSINESS.GetList_USER();
                if (GET_USER != null && GET_USER.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(this.cboUser, "FullName", "UserName", GET_USER, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboUser, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["UserName"] = cboUser.GetKeyValue(0);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }



        private void KTLoaiTB()
        {
            if (rdoPDA.IsChecked == true)
            {
                this.iDataSource.Rows[0]["LOAI_TBI"] = "PDA";
            }
            if (rdoXeNang.IsChecked == true)
            {
                this.iDataSource.Rows[0]["LOAI_TBI"] = "XNA";
            }
        }

        private bool CheckSymbolInMaPallet(string str)
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

        public void SetFocus()
        {
            if (txtTU_NGAY.Text == string.Empty)
            {
                txtTU_NGAY.Focus();
                base.ShowMessage(MessageType.Information, "Ngày bắt đầu" + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if (txtDEN_NGAY.Text == string.Empty)
            {
                txtDEN_NGAY.Focus();
                base.ShowMessage(MessageType.Information, "Ngày kết thúc" + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if (cboUser.Text == "--Chọn--")
            {
                base.ShowMessage(MessageType.Information, "User " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if (count == 0)
            {
                base.ShowMessage(MessageType.Information, "Chọn một thiết bị ");
                return;
            }
            if (DateTime.ParseExact(txtTU_NGAY.Text.ToString(), "MM/dd/yyyy hh:mm:ss", null) > DateTime.ParseExact(txtDEN_NGAY.Text.ToString(), "MM/dd/yyyy hh:mm:ss", null))
            {
                base.ShowMessage(MessageType.Information, "Ngày kết thúc phải nhỏ hơn ngày bắt đầu ");
                return;
            }
        }

        private void checkChon()
        {

            foreach (DataRow dr in row)
            {
                if (dr["CHON"].ToString() == "1")
                {
                    count = count + 1;
                    //iDataSource.Rows[] = iRowSelGb;
                }
            }

        }

        /* private bool checkNgay(DateTime tu_ngay, DateTime den_ngay)
         {
             if(tu_ngay>iRowSelGb)
         }*/



        private void SaveData()
        {
            bool result = _PHANCONGTHIETBI_BUSSINESS.Insert_Update_PhangCongThietBi(this.iDataSource.Copy());
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                iRowSelGb = ((DataRowView)this.grd.GetFocusedRow()).Row;
                checkChon();
                if (frm_phancong_thietbi.status == false && count != 1)
                    base.ShowMessage(MessageType.Information, "Chỉ chọn một ");
                else
                {
                    if (txtTU_NGAY.Text != string.Empty && cboUser.Text != "--Chọn--" && count != 0 && txtDEN_NGAY.Text != string.Empty && DateTime.ParseExact(txtTU_NGAY.Text.ToString(), "MM/dd/yyyy hh:mm:ss", null) < DateTime.ParseExact(txtDEN_NGAY.Text.ToString(), "MM/dd/yyyy hh:mm:ss", null))
                    {
                        DataTable dts = null;
                            dts = CheckDay();
                        for (int i = 0;i < iGridDataSource.Rows.Count; i++)
                        {
                            if (iGridDataSource.Rows[i]["CHON"].ToString() == "1")
                            {

                                if (dts == null || dts.Rows.Count == 0)
                                {
                                    iDataSource.Rows[0]["THIETBI_ID"] = iGridDataSource.Rows[i]["THIETBI_ID"];
                                    SaveData();
                                }
                                else
                                {
                                    base.ShowMessage(MessageType.Information, "THIẾT BỊ ĐANG ĐƯỢC SỬ DỤNG TRONG KHOẢNG THỜI GIAN NÀY BỞI USER "+dts.Rows[0]["UserName"].ToString());
                                    count--;
                                    return;
                                }
                            }
                        }
                    }
                    else
                    { SetFocus(); }
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private DataTable CheckDay()
        {
            string a = txtTU_NGAY.Text.Substring(0, 2);
            string b = txtTU_NGAY.Text.Substring(3, 2);
            string c = txtTU_NGAY.Text.Substring(6, 4);
            string NgayBD = c + "/" + a + "/" + b;
            a = txtDEN_NGAY.Text.Substring(0, 2);
            b = txtDEN_NGAY.Text.Substring(3, 2);
            c = txtDEN_NGAY.Text.Substring(6, 4);
            string NgayKT = c + "/" + a + "/" + b;
            DataTable dts = null;
            for (int i = 0; i < iGridDataSource.Rows.Count; i++)
            {
                if (iGridDataSource.Rows[i]["CHON"].ToString() == "1")
                {
                    if (frm_phancong_thietbi.status != true)
                    {
                        if (frm_phancong_thietbi.TBI_ID != int.Parse(iGridDataSource.Rows[i]["THIETBI_ID"].ToString()))
                        {
                            dts = _PHANCONGTHIETBI_BUSSINESS.GetListPhanCongThietBi_CheckDate(iGridDataSource.Rows[i]["THIETBI_ID"].ToString(), NgayBD, NgayKT);
                        }
                    }
                    else
                        dts = _PHANCONGTHIETBI_BUSSINESS.GetListPhanCongThietBi_CheckDate(iGridDataSource.Rows[i]["THIETBI_ID"].ToString(), NgayBD, NgayKT);
                }
            }
            return dts;
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



                xColumn = new Column("MA_TBI", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phancong_thietbi_MaThietBi"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_TBI", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phancong_thietbi_TenThietBi"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                

                /* xColumn = new Column("MA_TBI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaSanPham"]));
                 xColumn.Width = 100;
                 xColumn.Visible = true;
                 xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                 xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                 this.iGridHelper.Add(xColumn);

                 xColumn = new Column("TEN_TBI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenSanPham"]));
                 xColumn.Width = 100;
                 xColumn.Visible = true;
                 xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                 xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                 this.iGridHelper.Add(xColumn);

                 xColumn = new Column("lblDVT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDVT"]));
                 xColumn.Width = 100;
                 xColumn.Visible = true;
                 xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                 xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                 this.iGridHelper.Add(xColumn);

                 xColumn = new Column("lblSoLuongMIN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongMIN"]));
                 xColumn.Width = 100;
                 xColumn.Visible = true;
                 xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                 xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                 this.iGridHelper.Add(xColumn);

                 xColumn = new Column("lblSoLuongMAX", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSoLuongMAX"]));
                 xColumn.Width = 100;
                 xColumn.Visible = true;
                 xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                 xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                 this.iGridHelper.Add(xColumn);

                 xColumn = new Column("lblViTri", Convert.ToString(UtilLanguage.ApplyLanguage()["lblViTri"]));
                 xColumn.Width = 100;
                 xColumn.Visible = true;
                 xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                 xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                 this.iGridHelper.Add(xColumn);*/
                this.iGridHelper.Initialize();

                this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void rdoXeNang_Checked(object sender, RoutedEventArgs e)
        {
            LoaiTbi = "XNA";
            LoadData();
        }

        private void rdoPDA_Checked(object sender, RoutedEventArgs e)
        {
            LoaiTbi = "PDA";
            LoadData();
        }
       

    }

}
