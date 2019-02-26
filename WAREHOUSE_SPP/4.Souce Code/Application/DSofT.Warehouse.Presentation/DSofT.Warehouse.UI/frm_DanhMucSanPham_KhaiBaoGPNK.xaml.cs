using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows;
using System.Windows.Input;
using DSofT.Warehouse.Business;
using DSofT.Warehouse.Resources;
using DSofT.Warehouse.Log.UtilHelper;
using DSofT.Framework.UICore;
using DSofT.Framework.UIControl;
using System.Windows.Data;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using Aspose.Cells.Drawing;
using DSofT.Framework.UICore.TaskProxy;
using DevExpress.Xpf.Editors;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Ngô Gia Thiên
    ///  Create date: 05/03/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Danh muc san pham khai bao GPNK
    /// </summary>
    public partial class frm_DanhMucSanPham_KhaiBaoGPNK : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        DataRow iRowSelGb = null;
        IGPNKBussiness gpnk;

        public frm_DanhMucSanPham_KhaiBaoGPNK()
        {
            InitializeComponent();
            Initialize_Grid();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            gpnk = new GPNKBussiness(string.Empty);
            


        }


        private void LoadData()
        {
            try
            {

                this.iGridDataSource = gpnk.GetList_GPNK(CommonDataHelper.DonViQuanLy, int.Parse(iRowSelGb["SANPHAM_ID"].ToString()));
                this.iGridHelper.BindItemSource(this.iGridDataSource);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadData_GrdSP()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("SANPHAM_GPNK_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("STT", typeof(int));
                xDicUser.Add("SOLO", typeof(string));
                xDicUser.Add("GPNK", typeof(string));
                xDicUser.Add("NGAY_KY", typeof(string));
                xDicUser.Add("NGAY_HH", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(bool));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
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
        }
        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    LoadData();
                    if (this.iGridDataSource != null)
                    {
                        this.iGridDataSource.Rows.Add();
                        if (iRowSelGb != null)
                        {
                            this.iDataSource.Rows[0]["SANPHAM_ID"] = this.iRowSelGb["SANPHAM_ID"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "OnPopupShown()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// Initialize_Grid
        /// </summary>
        private DataTable TaoDatable_TT()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.TableName = "TT";

                DataColumn cTT = new DataColumn();
                cTT.ColumnName = "TINH_TRANG";
                cTT.DataType = typeof(bool);
                dt.Columns.Add(cTT);

                DataColumn c_HienThiTT = new DataColumn();
                c_HienThiTT.ColumnName = "HIENTHI_TT";
                c_HienThiTT.DataType = typeof(string);
                dt.Columns.Add("HIENTHI_TT");

                dt.Rows.Add();
                dt.Rows.Add();

                dt.Rows[0][0] = true;
                dt.Rows[0][1] = "Sử dụng";
                dt.Rows[1][0] = false;
                dt.Rows[1][1] = "Không sử dụng";


                return dt;
            }
            catch
            {

                return null;
            }
        }
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
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                /*xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 30;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.Binding = new System.Windows.Data.Binding("STT") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);*/

                xColumn = new Column("SOLO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblsolo"]));
                xColumn.Width = 60;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GPNK", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGPNK"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAY_KY", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgayKy"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.DateEdit;
                xColumn.DataType = typeof(DateTime);
                xColumn.MaskType = MaskType.DateTime;
                xColumn.EditMask = "dd/MM/yyyy";
                xColumn.IsNullValidate = true;
                xColumn.Binding = new System.Windows.Data.Binding("NGAY_KY") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAY_HH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNgayHH"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.DateEdit;
                xColumn.DataType = typeof(DateTime);
                xColumn.MaskType = MaskType.DateTime;
                xColumn.EditMask = "dd/MM/yyyy";
                xColumn.IsNullValidate = true;
                xColumn.Binding = new System.Windows.Data.Binding("NGAY_HH") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblGhiChu"]));
                xColumn.Width = 80;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TINH_TRANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTinhTrang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Combo;
                xColumn.Binding = new System.Windows.Data.Binding("TINH_TRANG") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = TaoDatable_TT();
                xColumn.ValueList.DisplayMember = "HIENTHI_TT";
                xColumn.ValueList.ValueMember = "TINH_TRANG";
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
                this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        void xButton_RemoveClick(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (this.iGridDataSource != null && this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["SANPHAM_GPNK_ID"].ToString()!=string.Empty)
                {
                    if (!base.ShowMessage(MessageType.OkCancel, "Bạn muốn xóa GPNK [" + grd.GetFocusedRowCellValue("GPNK").ToString() + "] ?"))
                        return;
                    int result = gpnk.DeleteGPNK(CommonDataHelper.DonViQuanLy,int.Parse(this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["SANPHAM_GPNK_ID"].ToString()),CommonDataHelper.UserName);
                    if(result==1)
                    {
                        ToastMessage.ShowToastMessage("Thông báo", "Thành công!", notificationService);
                        LoadData();
                        this.iGridDataSource.Rows.Add();
                        return;
                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                        return;
                    }
                }
                else
                {
                    base.ShowMessage(MessageType.Information, "Chưa có GPNK nào được chọn!");
                    return;
                }
            }catch(Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        int CheckEmpty()
        {
            if(grd.GetFocusedRowCellValue("ROWNUM").ToString() ==string.Empty)
            {
                base.ShowMessage(MessageType.Information, "Vui lòng nhập STT!");
                return 1;
            }
            if (grd.GetFocusedRowCellValue("SOLO").ToString() == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "Vui lòng nhập Số lô!");
                return 1;
            }
            if (grd.GetFocusedRowCellValue("GPNK").ToString() == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "Vui lòng nhập GPNK!");
                return 1;
            }
            if (grd.GetFocusedRowCellValue("NGAY_KY").ToString() == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "Vui lòng nhập ngày đăng ký!");
                return 1;
            }
            if (grd.GetFocusedRowCellValue("NGAY_HH").ToString() == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "Vui lòng nhập ngày hết hạn đăng ký!");
                return 1;
            }
            if (grd.GetFocusedRowCellValue("TINH_TRANG").ToString() == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "Vui lòng chọn tình trạng!");
                return 1;

            }
            return 0;
        }

        DataTable createData_Save()
        {
            string gpnk_ID = null;
            string sp_ID = null;
            DataTable dt = new DataTable();
            if (iRowSelGb != null)
            {
                sp_ID = iRowSelGb["SANPHAM_ID"].ToString();
                if (this.iGridDataSource != null)
                {

                    if ((grd.View as TableView).FocusedRowHandle != this.iGridDataSource.Rows.Count - 1)
                    {

                        gpnk_ID = this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["SANPHAM_GPNK_ID"].ToString();

                    }

                }
            }
            dt.Columns.Add("SANPHAM_GPNK_ID");
            dt.Columns.Add("SANPHAM_ID");
            dt.Columns.Add("STT");
            dt.Columns.Add("SOLO");
            dt.Columns.Add("GPNK");
            dt.Columns.Add("NGAY_KY");
            dt.Columns.Add("NGAY_HH");
            dt.Columns.Add("GHI_CHU");
            dt.Columns.Add("TINH_TRANG");
            dt.Rows.Add(gpnk_ID, sp_ID, grd.GetFocusedRowCellValue("ROWNUM").ToString(), grd.GetFocusedRowCellValue("SOLO").ToString(), grd.GetFocusedRowCellValue("GPNK").ToString(), grd.GetFocusedRowCellValue("NGAY_KY").ToString(), grd.GetFocusedRowCellValue("NGAY_HH").ToString(), grd.GetFocusedRowCellValue("GHI_CHU").ToString(), grd.GetFocusedRowCellValue("TINH_TRANG").ToString());
            return dt;
        }
        int SoSanhNgayThangNam(string date1, string date2)
        {
            date1=date1.Substring(0,date1.IndexOf(' '));
            date1=date2.Substring(0,date2.IndexOf(' '));
            string[] arr1 = date1.Split('/');
            string[] arr2 = date2.Split('/');
            if (int.Parse(arr2[2]) > int.Parse(arr1[2]))
                return 1;
            else if (int.Parse(arr2[2]) < int.Parse(arr1[2]))
                return -1;
            else if (int.Parse(arr2[1]) > int.Parse(arr1[1]))
                return 1;
            else if (int.Parse(arr2[1]) < int.Parse(arr1[1]))
                return -1;
            else if (int.Parse(arr2[0]) > int.Parse(arr1[0]))
                return 1;
            else if ((int.Parse(arr2[0]) < int.Parse(arr1[0])))
                return -1;
            else
                return 1;


        }
        private int CheckDateMonth()
        {
            if (SoSanhNgayThangNam(grd.GetFocusedRowCellValue("NGAY_KY").ToString(), grd.GetFocusedRowCellValue("NGAY_HH").ToString()) == -1)
            {
                base.ShowMessage(MessageType.Information, "Ngày hết hạn đăng ký phải sau ngày đăng ký!");
                return 0;
            }
            return 1;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (CheckEmpty() == 1)
                    return;
                string date1 = grd.GetFocusedRowCellValue("NGAY_KY").ToString();
                string date2 = grd.GetFocusedRowCellValue("NGAY_HH").ToString();
                string s1 = date1.Substring(0, date1.IndexOf(" ") + 1);
                string s2 = date2.Substring(0, date2.IndexOf(" ") + 1);
                string[] arr1 = s1.Split('/');
                string[] arr2 = s2.Split('/');
                if (int.Parse(arr2[2]) < int.Parse(arr1[2]))
                {
                    base.ShowMessage(MessageType.Information, "Ngày hết hạn đăng ký phải sau ngày đăng ký!");
                    return;
                }
                else if (int.Parse(arr2[0]) < int.Parse(arr1[0]))
                {
                    base.ShowMessage(MessageType.Information, "Ngày hết hạn đăng ký phải sau ngày đăng ký!");
                    return;
                }
                else if ((int.Parse(arr2[1]) < int.Parse(arr1[1])))
                {
                    base.ShowMessage(MessageType.Information, "Ngày hết hạn đăng ký phải sau ngày đăng ký!");
                    return;
                }
                //-------------------------
                DataTable dt = createData_Save();
                if (dt != null && dt.Rows.Count>0)
                {
                    int result = gpnk.InsertorUpdateGPNK(dt);
                    if (result > 0)
                    {
                        ToastMessage.ShowToastMessage("Thông báo", "Thành công!", notificationService);
                        LoadData();
                        this.iGridDataSource.Rows.Add();
                        return;
                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, "Không thành công");
                        return;
                    }

                }
            }
            catch(Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "btnSave_Click()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

        }

        private void grdView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if (this.iGridDataSource != null)
                {

                    if ((grd.View as TableView).FocusedRowHandle == this.iGridDataSource.Rows.Count - 1)
                    {
                        if(this.iGridDataSource.Rows[(grd.View as TableView).FocusedRowHandle]["SOLO"].ToString()!=string.Empty)
                            this.iGridDataSource.Rows.Add();
                    }

                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_CellValueChanged()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }

        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            this.iGridDataSource.Rows.Add();
        }
    }
}
