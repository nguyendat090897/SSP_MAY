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
using System.Windows.Data;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_ThamSo.xaml
    /// </summary>
    public partial class frm_ThamSo : ControlBase
    {
        DataRow iRowSelGb = null;
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        I_ThamSo_Bussiness _ThamSo;
        public frm_ThamSo()
        {
            _ThamSo = new ThamSo_Bussiness(string.Empty);
            InitializeComponent();
            Initialize_Grid();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadData();
        }

        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("MA_THAMSO", typeof(string));
                xDicUser.Add("TEN_THAMSO", typeof(string));
                xDicUser.Add("GIATRI", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(Boolean));
                xDicUser.Add("RowVersion", typeof(int));
                xDicUser.Add("IsDelete", typeof(Boolean));
                xDicUser.Add("CreateBy", typeof(String));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(String));
                xDicUser.Add("ModifiedDate", typeof(DateTime));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
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
                xColumn.Width = 30;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("MA_THAMSO", Convert.ToString(UtilLanguage.ApplyLanguage()["frmThamSo_MaThamSo"]));

                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("TEN_THAMSO", Convert.ToString(UtilLanguage.ApplyLanguage()["frmThamSo_TenThamSo"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GIATRI", Convert.ToString(UtilLanguage.ApplyLanguage()["frmThamSo_GTThamSo"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["frmWarehouse_Remark"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TINH_TRANG", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTinhTrang"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("TINH_TRANG") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                xColumn.AllowSorting = false;
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


        private void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _ThamSo.GetAll(ConstCommon.DonViQuanLy);
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

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private bool SaveData()
        {
            bool result;
            return  result = _ThamSo.Update(this.iRowSelGb);
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool result=false;
            if (iGridDataSource == null) return;
            iRowSelGb = ((DataRowView)this.grd.GetFocusedRow()).Row;
            int locate;
            for (int i=0;i<iGridDataSource.Rows.Count;i++)
            {
                locate = i + 1;
                DataTable dt = _ThamSo.GetGT(iGridDataSource.Rows[i]);
                if(iGridDataSource.Rows[i]["GIATRI"].ToString()=="")
                {
                    base.ShowMessage(MessageType.Information, "GIÁ TRỊ THAM SỐ TẠI DÒNG " + locate + " KHÔNG ĐƯỢC TRỐNG");
                    return;
                }
                if(dt==null || dt.Rows.Count ==0)
                {
                    base.ShowMessage(MessageType.Information, "GIÁ TRỊ THAM SỐ TẠI DÒNG " + locate + " KHÔNG HỢP LỆ");
                    return;
                }
                else
                {
                    iRowSelGb = iGridDataSource.Rows[i];
                    result=SaveData();
                }
            }
            if (result == false)
            {

                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                return;
            }
            else
            {
                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                LoadData();
                return;
            }
        }
    }
}
