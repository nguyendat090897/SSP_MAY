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
using DSofT.Framework.UICore.TaskProxy;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 30/01/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Danh muc nha san xuat
    /// </summary>
    public partial class frmDanhMucNhaSanXuat : ControlBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        IDM_NHA_SXUATBussiness nsx;
        DataRow iRowSelGb = null;
        public static bool status = true;
        public frmDanhMucNhaSanXuat()
        {
            InitializeComponent();
            nsx = new DM_NHA_SXUATBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadData();
            Initialize_Grid();
            
        }
        
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("NHA_SXUAT_ID", typeof(decimal));
                xDicUser.Add("MA_NHA_SXUAT", typeof(string));
                xDicUser.Add("TEN_NHA_SXUAT", typeof(string));
                xDicUser.Add("DIACHI", typeof(string)); 
                xDicUser.Add("TINH_TRANG", typeof(int));
                xDicUser.Add("QUOCGIA_ID", typeof(int));
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
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_NHA_SXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaNhaSX"]));
                xColumn.Width = 60;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                 xColumn = new Column("TEN_NHA_SXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenNhaSX"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("DIACHI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDiaChiNhaSX"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TINH_TRANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTinhTrangSuDung"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_QUOCGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNSX"]));
                xColumn.Width = 60;
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

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            status = true;
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frmDanhMucNhaSanXuat_popup", null);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (iDataSource.Rows.Count == 0) return;
                iRowSelGb = ((DataRowView)this.grd.GetFocusedRow()).Row;
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frmDanhMucNhaSanXuat_popup", iRowSelGb);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_MouseDoubleClick()");
                //base.ShowMessage(MessageType.Error, ex.Message, ex);
                base.ShowMessage(MessageType.Information, "Bạn chưa chọn nhà sản xuất phù hợp!");
            }
            
        }

        //-----------------------------------------
        //----------------------code---------------------

        private void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return nsx.GetListDM_NHA_SXUAT(CommonDataHelper.DonViQuanLy.ToString());

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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string MA_DVIQLY = CommonDataHelper.DonViQuanLy;
            int NHA_SXUAT_ID;
            string ModifiledBy = CommonDataHelper.UserName;
            try
            {
                NHA_SXUAT_ID = int.Parse(grd.GetFocusedRowCellValue("NHA_SXUAT_ID").ToString());
            }catch
            {
                NHA_SXUAT_ID = 0;
            }
            if(NHA_SXUAT_ID==0)
            {
                
                base.ShowMessage(MessageType.Information, "Chưa chọn nhà sản xuất cần xóa!");
                return;
            }
            if (!base.ShowMessage(MessageType.OkCancel, "Bạn muốn xóa nhà sản xuất [" + grd.GetFocusedRowCellValue("TEN_NHA_SXUAT").ToString() + "] ?"))
                return;
            try
            {
                int result = nsx.DeleteDM_NHA_SXUAT(MA_DVIQLY, NHA_SXUAT_ID, ModifiledBy);
                if (result == 1)
                {
                    ToastMessage.ShowToastMessage("Thông báo", "Xóa thành công!", notificationService);
                    LoadData();
                }
                else
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                    return;
                }
            }catch
            {
                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                return;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }



        //----------------------end code-----------------


    }
}
