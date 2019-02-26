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
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Ngo Gia Thien
    ///  Create date: 22/03/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Xay dung dinh muc bo sung thong tin tieng viet khai bao chi phi dich vu
    /// </summary>
    public partial class frm_XDDinhMuc_ChoSP_KhuyenMai_KhaiBaoCPDV : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        DataTable dtTenDV = null;
        IXD_DM_SPKMAIBussiness XD_DM_SPKMAIBussiness;
        public static decimal CPDV;
        public frm_XDDinhMuc_ChoSP_KhuyenMai_KhaiBaoCPDV()
        {
            InitializeComponent();
            XD_DM_SPKMAIBussiness = new XD_DM_SPKMAIBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            BultCombobox();
            Initialize_Grid();
        }

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGb = this.Parameter[0] as DataRow;
                DispalayRequest();
                if (this.iGridDataSource != null)
                {
                    this.iGridDataSource.Rows.Add();
                }
                LoadData();
                if (iRowSelGb != null)
                {
                    this.iDataSource.Rows[0]["DINHMUC_TEMTOA_ID"] = this.iRowSelGb["DINHMUC_TEMTOA_ID"];
                }
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
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("DINHMUC_TEMTOA_CHIPHI_DV_ID", typeof(decimal));
                xDicUser.Add("DINHMUC_TEMTOA_ID", typeof(long));
                xDicUser.Add("DICHVU_TEMTOA_ID", typeof(decimal));
                xDicUser.Add("CHIPHI", typeof(int));
                xDicUser.Add("RowVersion", typeof(long));
                xDicUser.Add("IsDelete", typeof(string));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }

        private void BultCombobox()
        {
            dtTenDV = XD_DM_SPKMAIBussiness.GetListHT_DINHMUC_TEMTOA_CHIPHI_DV_GET_TENDV(ConstCommon.DonViQuanLy);
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
                        return XD_DM_SPKMAIBussiness.HT_DINHMUC_TEMTOA_CHIPHI_DV_GET_LIST_ALL(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(iRowSelGb["DINHMUC_TEMTOA_ID"].ToString()));
                    });
                    Action<DataTable> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iGridDataSource = dt;
                            this.iGridDataSource.Rows.Add();
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

        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;


                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                string[] header = new string[] { "Dịch vụ" };
                string[] id = new string[] { "TEN_DICHVU_TEMTOA" };
                string[] align = new string[] { "left" };

                xColumn = new Column("DICHVU_TEMTOA_ID", "Dịch vụ");
                xColumn.Width = 300;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.LookUpEdit;
                xColumn.Binding = new System.Windows.Data.Binding("DICHVU_TEMTOA_ID") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                xColumn.ValueList.DataSource = dtTenDV;
                xColumn.ValueList.DisplayMember = "TEN_DICHVU_TEMTOA";
                xColumn.ValueList.ValueMember = "CHIPHI_DICHVU_TEMTOA_ID";
                xColumn.dataTemplate = LookupEditUtil.CreateTemplateLookupEdit(dtTenDV, header, id, align);
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("CHIPHI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblChiPhiVND"]));
                xColumn.Width = 300;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Right;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
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
                bool msg = false;
                if ((this.iGridDataSource == null) || (this.iGridDataSource.Rows.Count == 0))
                {
                    return;
                }

                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;

                if (ConstCommon.NVL_NUM_LONG_NEW(row["DINHMUC_TEMTOA_CHIPHI_DV_ID"]) > 0)
                {
                    msg = (bool)base.ShowMessage(MessageType.OkCancel, "BẠN CÓ MUỐN XÓA DÒNG ĐANG CHỌN KHÔNG???");
                    if (msg == true)
                    {
                        DataTable dsReturn = XD_DM_SPKMAIBussiness.HT_DINHMUC_TEMTOA_CHIPHI_DV_DELETE(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(row.Row["DINHMUC_TEMTOA_CHIPHI_DV_ID"]), CommonDataHelper.UserName);
                       
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

                iGridDataSource.Rows.Remove(row.Row);

                tinhCPDV();

                this.iGridHelper.BindItemSource(this.iGridDataSource);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "xButton_RemoveClick()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdView_CellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            try
            {
                if (iGridDataSource != null)
                {

                    if ((grd.View as TableView).FocusedRowHandle == iGridDataSource.Rows.Count - 1)
                    {

                        iGridDataSource.Rows.Add();

                    }

                    for (int i = 0; i < iGridDataSource.Rows.Count - 1; i++)
                    {
                        this.iGridDataSource.Rows[i]["DICHVU_TEMTOA_ID"] = this.iGridDataSource.Rows[i]["DICHVU_TEMTOA_ID"];
                    }
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnChapNhan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(checkNull() == false)
                {
                    return;
                }
                
                bool result = true;

                for (int i = 0; i < this.iGridDataSource.Rows.Count - 1; i++)
                {
                    DataTable dtCPDV = new DataTable();
                    dtCPDV.Columns.Add("DINHMUC_TEMTOA_CHIPHI_DV_ID");
                    dtCPDV.Columns.Add("DINHMUC_TEMTOA_ID");
                    dtCPDV.Columns.Add("DICHVU_TEMTOA_ID");
                    dtCPDV.Columns.Add("CHIPHI");
                    dtCPDV.Columns.Add("RowVersion");

                    DataRow drNew = dtCPDV.NewRow();
                    drNew["DINHMUC_TEMTOA_CHIPHI_DV_ID"] = this.iGridDataSource.Rows[i]["DINHMUC_TEMTOA_CHIPHI_DV_ID"].ToString();
                    drNew["DINHMUC_TEMTOA_ID"] = iRowSelGb["DINHMUC_TEMTOA_ID"].ToString();
                    drNew["DICHVU_TEMTOA_ID"] = this.iGridDataSource.Rows[i]["DICHVU_TEMTOA_ID"];
                    drNew["CHIPHI"] = this.iGridDataSource.Rows[i]["CHIPHI"];

                    dtCPDV.Rows.Add(drNew);

                    if (dtCPDV != null)
                    {
                        this.iGridHelper.BindItemSource(dtCPDV);
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }



                    result = XD_DM_SPKMAIBussiness.InsertOrUpdateHT_DINHMUC_TEMTOA_CHIPHI_DV(dtCPDV);
                }

                tinhCPDV();

                if (!result)
                {

                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                    return;
                }
                else
                {
                    ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {

                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChapNhan.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void tinhCPDV()
        {
            bool result = true;
            decimal gb = 0;
            CPDV = 0;
            try
            {
                for(int i = 0; i < this.iGridDataSource.Rows.Count -1 ; i++)
                {
                    CPDV += ConstCommon.NVL_NUM_DECIMAL_NEW(iGridDataSource.Rows[i]["CHIPHI"].ToString());
                }

                gb = frm_XDDinhMuc_ChoSP_KhuyenMai_Popup.tinhgn + (frm_XDDinhMuc_ChoSP_KhuyenMai_Popup.tinhgn * frm_XDDinhMuc_ChoSP_KhuyenMai_Popup.tyle) + CPDV;

                DataTable dtTinhCPDV = new DataTable();
                dtTinhCPDV.Columns.Add("DINHMUC_TEMTOA_ID");
                dtTinhCPDV.Columns.Add("CHIPHI_DICHVU");
                dtTinhCPDV.Columns.Add("GIABAN");

                DataRow drNew = dtTinhCPDV.NewRow();
                drNew["DINHMUC_TEMTOA_ID"] = iRowSelGb["DINHMUC_TEMTOA_ID"].ToString();
                drNew["CHIPHI_DICHVU"] = CPDV;
                drNew["GIABAN"] = gb;

                dtTinhCPDV.Rows.Add(drNew);

                result = XD_DM_SPKMAIBussiness.UpdateHT_DINHMUC_TEMTOA_CPDV(dtTinhCPDV);

            }
            catch (Exception ex)
            {

                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private bool checkNull()
        {
            for (int i = 0; i < this.iGridDataSource.Rows.Count - 1; i++)
            {
                if (this.iGridDataSource.Rows[i]["DICHVU_TEMTOA_ID"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "VUI LÒNG CHỌN DỊCH VỤ");
                    return false;
                }
                if (this.iGridDataSource.Rows[i]["CHIPHI"].ToString() == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, " VUI LÒNG KHAI BÁO CHI PHÍ");
                    return false;
                }
                if (XD_DM_SPKMAIBussiness.HT_DINHMUC_TEMTOA_CHIPHI_DV_CHECKEXISTS(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[i]["DINHMUC_TEMTOA_CHIPHI_DV_ID"]), ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["DINHMUC_TEMTOA_ID"]), ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[i]["DICHVU_TEMTOA_ID"])) == true)
                {
                    base.ShowMessage(MessageType.Information, " DỊCH VỤ ĐÃ TỒN TẠI. VUI LÒNG CHỌN DỊCH VỤ KHÁC");
                    return false;
                }
                    if (ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[i]["DICHVU_TEMTOA_ID"]) == ConstCommon.NVL_NUM_LONG_NEW(this.iGridDataSource.Rows[i + 1]["DICHVU_TEMTOA_ID"]))
                    {
                        base.ShowMessage(MessageType.Information, " VUI LÒNG KHÔNG NHẬP TRÙNG DỊCH VỤ");
                        return false;
                    }
            }
            return true;
        }
    }

}
