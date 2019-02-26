using DSofT.Warehouse.Business;
using DSofT.Warehouse.Log.UtilHelper;
using DSofT.Warehouse.Resources;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using DevExpress.Xpf.Grid;
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
    ///  Author:	 Ngô Gia Thiên
    ///  Create date: 06/03/2018
    ///  Editor:      31/3/2018
    ///  Modify date: 
    ///  Description: Danh muc pallet tiep nhan dieu chuyen
    ///  DEV: Nguyen Van Huynh
    /// </summary>
    public partial class frm_dm_pallet_tiepnhandieuchuyen : PopupBase
    {
        DataTable iDataSource = null;
        DataTable iGridDataSource = null;
        GridHelper iGridHelper = null;
        DataRow iRowSelGb = null;
        IDM_PALLET_DIEUCHUYEN_CTIETBussiness _DM_PALLET_DIEUCHUYEN_CTIETBussiness;
        public frm_dm_pallet_tiepnhandieuchuyen()
        {
            InitializeComponent();
            _DM_PALLET_DIEUCHUYEN_CTIETBussiness = new DM_PALLET_DIEUCHUYEN_CTIETBussiness(string.Empty);
            Initialize_Grid();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.iGridDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadDataGird();
        }
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("PALLET_DIEUCHUYEN_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("MA_DVIQLY_NHAN", typeof(string));
                xDicUser.Add("SO_PHIEU", typeof(string));
                xDicUser.Add("NGAY_CHUYEN", typeof(string));
                xDicUser.Add("NGAY_NHAN", typeof(string));
                xDicUser.Add("NGUOI_NHAN", typeof(string));
                xDicUser.Add("LYDO", typeof(string));
                xDicUser.Add("PALLET_ID", typeof(decimal));
                xDicUser.Add("MA_PALLET", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(bool));
                xDicUser.Add("TEN_PALLET", typeof(string));
                xDicUser.Add("PALLET_DIEUCHUYEN_CTIET_ID", typeof(decimal));
                xDicUser.Add("TEN_DVIQLY", typeof(string));
                xDicUser.Add("TEN_DVIQLY_GIAO", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }
        //public override void OnPopupShown()
        //{
        //    try
        //    {
        //        LoadData();
        //        txtNGUOI_NHAN.Text = CommonDataHelper.UserName;
        //    }
        //    catch (Exception ex)
        //    {
        //        base.ShowMessage(MessageType.Error, ex.Message, ex);
        //    }
        //    finally
        //    {
        //        Mouse.OverrideCursor = Cursors.Arrow;
        //    }
        //}
        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    if (this.iDataSource.Rows[0]["NGUOI_NHAN"].ToString().Trim() != String.Empty)
                        txtNGUOI_NHAN.Text = this.iDataSource.Rows[0]["NGUOI_NHAN"].ToString();
                    else
                        txtNGUOI_NHAN.Text = String.Empty;
                    UpdateTinhTrang();
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
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
        private void LoadDataGird()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _DM_PALLET_DIEUCHUYEN_CTIETBussiness.GetListDM_PALLET_DIEUCHUYEN_CTIET(ConstCommon.DonViQuanLy,frm_dm_pallet_dieuchuyen_popup.pPALLET_CHUYEN);
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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadDataTenPallet()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;

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
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void SaveData()
        {
            bool result = _DM_PALLET_DIEUCHUYEN_CTIETBussiness.UpdateDM_PHIEU_PALLET_DIEU_CHUYEN(iDataSource, txtNGUOI_NHAN.Text,ConstCommon.DonViQuanLy);
            if (!result)
            {

                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                return;
            }
            else
            {
                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
            }
        }
        private void KTTinhTrang()
        {
            if (rdbDONGY.IsChecked == true)
            {
                this.iDataSource.Rows[0]["TINH_TRANG"] = 1;
                txtNGUOI_NHAN.Text = CommonDataHelper.UserName;
            }
            else if (rdbTUCHOI.IsChecked == true)
            {
                this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
                txtNGUOI_NHAN.Text = String.Empty;
            }
            else
                return;
        }
        private void UpdateTinhTrang()
        {
            if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString() == "False")
                rdbTUCHOI.IsChecked = true;
            else if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString() == "True")
                rdbDONGY.IsChecked = true;
            else
                return;
        }
        private bool SetIsNull()
        {
            if (rdbDONGY.IsChecked == false && rdbTUCHOI.IsChecked == false)
            {
                base.ShowMessage(MessageType.Information, "Chưa chọn [TÌNH TRẠNG CHẤP NHẬN].");
                return false;
            }
            else if (rdbDONGY.IsChecked == true)
            {
                if (dtpNGAY_NHAN.Text == String.Empty)
                {
                    base.ShowMessage(MessageType.Information, "[NGÀY TIẾP NHẬN] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                    dtpNGAY_NHAN.Focus();
                    return false;
                }
                else
                    return true;
            }
            else
                return true;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnChapNhan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rdbDONGY.IsChecked == true)
                {
                    if (SetIsNull() == true)
                    {
                        SaveData();
                        this.Close();
                    }
                    else
                        return;
                }
                else if (rdbTUCHOI.IsChecked == true)
                    SaveData();
                else
                    SetIsNull();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void rdbTUCHOI_Click(object sender, RoutedEventArgs e)
        {
            KTTinhTrang();
        }

        private void rdbDONGY_Click(object sender, RoutedEventArgs e)
        {
            KTTinhTrang();
        }
    }
}
