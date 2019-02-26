using DSofT.Warehouse.Business;
using DSofT.Warehouse.Resources;
using DevExpress.Xpf.Grid;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using DSofT.Warehouse.Log.UtilHelper;
using DSofT.Framework.UICore.TaskProxy;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_phancong_thiebi.xaml
    /// </summary>
    public partial class frm_phancong_thietbi : ControlBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        DataRow iRowSelGb = null;
        I_PHANCONGTHIETBIBussiness _PHANCONGTHIETBIBussiness;
        public static bool status = true;
        public static int PCONG_TBI_ID;
        public static string LoaiTBI;
        public static int TBI_ID;

        public frm_phancong_thietbi()
        {
            InitializeComponent();
            _PHANCONGTHIETBIBussiness = new PHANCONGTHIETBIBussiness(string.Empty);
            Initialize_Grid_PhanCong_ThietBi();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadData();
        }
        /// <summary>
        /// Initialize_Grid_PhanCong_ThietBi
        /// </summary>
        private void Initialize_Grid_PhanCong_ThietBi()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.gridControl, this.grdViewPhanCongThietBi, false);
                Column xColumn;

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

                xColumn = new Column("LOAI_TBI", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phancong_thietbi_LoaiThietBi"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
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

                xColumn = new Column("TU_NGAY", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phancong_thietbi_TuNgay"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("DEN_NGAY", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phancong_thietbi_DenNgay"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);
               
                this.iGridHelper.Initialize();
                this.grdViewPhanCongThietBi.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid_PhanCOng_ThietBi()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// btnNew_Click
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            status = true;
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_phancong_thietbi_popup", null);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnNew.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.gridControl.GetFocusedRow() == null) return;
                iRowSelGb = null;
                if (!base.ShowMessage(MessageType.OkCancel, Convert.ToString(UtilLanguage.ApplyLanguage()["lblcomfrimDelete"])))
                    return;
                iRowSelGb = ((DataRowView)gridControl.GetFocusedRow()).Row;
                bool result = _PHANCONGTHIETBIBussiness.DeleteHT_PHANCONGTHIETBI(int.Parse( iRowSelGb["PCONG_TBI_ID"].ToString() ), CommonDataHelper.UserName);
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

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
           //Code xu ly
        }
        /// <summary>
        /// LoadData
        /// </summary>
        void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _PHANCONGTHIETBIBussiness.GetListPhanCongThietBi(ConstCommon.DonViQuanLy);
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
                xDicUser.Add("PCONG_TBI_ID", typeof(int));
                xDicUser.Add("FullName", typeof(string));
                xDicUser.Add("UserName", typeof(string));
                xDicUser.Add("LOAI_TBI", typeof(string));
                xDicUser.Add("MA_TBI", typeof(string));
                xDicUser.Add("TEN_TBI", typeof(string));
                xDicUser.Add("TU_NGAY", typeof(DateTime));
                xDicUser.Add("DEN_NGAY", typeof(DateTime));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }

        private void grdView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            status = false;
            try
            {
                if (iDataSource.Rows.Count == 0) return;
                iRowSelGb = ((DataRowView)this.gridControl.GetFocusedRow()).Row;
                PCONG_TBI_ID =int.Parse( this.iRowSelGb["PCONG_TBI_ID"].ToString());
                LoaiTBI= this.iRowSelGb["LOAI_TBI"].ToString();
                TBI_ID=int.Parse( this.iRowSelGb["THIETBI_ID"].ToString());
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_phancong_thietbi_popup", iRowSelGb);
                LoadData();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "grdView_MouseDoubleClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }
}
