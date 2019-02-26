using System.Linq;
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
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 23/03/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: pallet dieu chuyen popup 2
    /// </summary>
    public partial class frm_dm_pallet_dieuchuyen_popup_2 : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        DataTable dtSPALL = new DataTable();
        DataTable dtPLCHON = new DataTable();
        DataTable dtPLCHON_Return = null;
        string mbChon = "N";
        frm_dm_pallet_dieuchuyen_popup dm;
        IDM_PALLET_DIEUCHUYENBussiness _DM_PALLET_DIEUCHUYENBussiness;
        public frm_dm_pallet_dieuchuyen_popup_2()
        {
            InitializeComponent();
            _DM_PALLET_DIEUCHUYENBussiness = new DM_PALLET_DIEUCHUYENBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            Initialize_Grid();
            LoadData();
        }
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("LYDO", typeof(string));
                xDicUser.Add("SO_PHIEU", typeof(string));
                xDicUser.Add("PALLET_ID", typeof(decimal));
                xDicUser.Add("PALLET_DIEUCHUYEN_CTIET_ID", typeof(decimal));
                xDicUser.Add("PALLET_DIEUCHUYEN_ID", typeof(decimal));
                xDicUser.Add("MA_PALLET", typeof(string));
                xDicUser.Add("TEN_PALLET", typeof(string));
                xDicUser.Add("TEN_DVIQLY", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }
        public void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        if (frm_dm_pallet_dieu_chuyen.status == true)
                        {
                            dtSPALL = _DM_PALLET_DIEUCHUYENBussiness.GetListDM_PALLET_DIEUCHUYEN_TEN_PALLET(ConstCommon.DonViQuanLy, 0);
                            return _DM_PALLET_DIEUCHUYENBussiness.GetListDM_PALLET_DIEUCHUYEN_TEN_PALLET(ConstCommon.DonViQuanLy, 0);
                        }
                        else
                        {
                            dtSPALL = _DM_PALLET_DIEUCHUYENBussiness.GetListDM_PALLET_DIEUCHUYEN_TEN_PALLET(ConstCommon.DonViQuanLy, frm_dm_pallet_dieuchuyen_popup.pPALLET_CHUYEN);
                            return _DM_PALLET_DIEUCHUYENBussiness.GetListDM_PALLET_DIEUCHUYEN_TEN_PALLET(ConstCommon.DonViQuanLy, frm_dm_pallet_dieuchuyen_popup.pPALLET_CHUYEN);
                        }
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
        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 40;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("CHON", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_Chon"]));
                xColumn.Width = 40;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("CHON") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                xColumn.AllowSorting = false;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaPallet"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_PALLET", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                this.iGridHelper.Initialize();
                this.grdView.AutoWidth = true;
               // setAutoFilterConditionGrid(grd);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mbChon = "Y";
            }
            catch
            {

                mbChon = "N";
            }
            finally
            {
                this.Close();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ContainDataRowInDataTable(DataTable dt, DataRow dr)
        {
            try
            {
                var k = (from r in dt.Rows.OfType<DataRow>() where r["PALLET_ID"].ToString() == dr["PALLET_ID"].ToString() select r).FirstOrDefault();
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
        public override object UIReturn(string source)
        {
            try
            {

                if ((dtPLCHON_Return != null) && (dtPLCHON_Return.Rows.Count > 0))
                {
                    dtPLCHON_Return.Rows[0]["GHI_CHU"] = mbChon;

                }

                return dtPLCHON_Return;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
                return null;
            }

        }
        private void grd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if ((dtPLCHON == null) || (dtPLCHON.Rows.Count == 0))
                {
                    dtPLCHON = dtSPALL.Clone();
                }
                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;

                if (ContainDataRowInDataTable(dtPLCHON, row.Row) == false)
                {
                    row["CHON"] = 1;
                    frm_dm_pallet_dieuchuyen_popup.pSO_LUONG_CHUYEN++;
                    if (frm_dm_pallet_dieuchuyen_popup.pSO_LUONG_CHO_PHEP != 0 && frm_dm_pallet_dieuchuyen_popup.pSO_LUONG_CHUYEN > frm_dm_pallet_dieuchuyen_popup.pSO_LUONG_CHO_PHEP)
                    {
                        base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG CHUYỂN] VƯỢT QUÁ [SỐ LƯỢNG CHO PHÉP].", null);
                    }
                    else
                        dtPLCHON.ImportRow(row.Row);
                }
                dtPLCHON_Return = dtPLCHON.Copy();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void grdView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if ((dtPLCHON == null) || (dtPLCHON.Rows.Count == 0))
                {
                    dtPLCHON = dtSPALL.Clone();
                }
                if (this.grd.GetFocusedRow() == null) return;
                DataRowView row = this.grd.GetFocusedRow() as DataRowView;

                if (ContainDataRowInDataTable(dtPLCHON, row.Row) == false)
                {
                    frm_dm_pallet_dieuchuyen_popup.pSO_LUONG_CHUYEN++;
                    if (row["CHON"].ToString() == "1")
                    {
                        if (frm_dm_pallet_dieuchuyen_popup.pSO_LUONG_CHO_PHEP != 0 && frm_dm_pallet_dieuchuyen_popup.pSO_LUONG_CHUYEN > frm_dm_pallet_dieuchuyen_popup.pSO_LUONG_CHO_PHEP)
                        {
                            base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG CHUYỂN] VƯỢT QUÁ [SỐ LƯỢNG CHO PHÉP].", null);
                        }
                        else
                            dtPLCHON.ImportRow(row.Row);
                    }
                    else
                    {
                        frm_dm_pallet_dieuchuyen_popup.pSO_LUONG_CHUYEN--;
                        return;
                    }
                }
                dtPLCHON_Return = dtPLCHON.Copy();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }

}
