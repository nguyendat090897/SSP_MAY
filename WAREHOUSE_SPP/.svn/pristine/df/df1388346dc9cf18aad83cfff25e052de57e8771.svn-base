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
    /// Interaction logic for frm_dm_pallet_popup.xaml
    /// </summary>
    public partial class frmTLQTHangBanCham_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        DataTable dt_PHIEU_CTIET = null;
        bool flag = true;
        int locate;
        I_QuyTacBanHangCham_Bussiness _QuyTacBanHangCham_Bussiness;
        public frmTLQTHangBanCham_popup()
        {
            _QuyTacBanHangCham_Bussiness = new QuyTacBanHangCham_Bussiness(string.Empty);
            InitializeComponent();
            Initialize_Grid();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            dt_PHIEU_CTIET = this.TableSchemaDefineBingding();
            dt_PHIEU_CTIET.Clear();
            if (frmTLQTHangBanCham.status == false)
                LoadData();
        }

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGb = this.Parameter[0] as DataRow;
                DispalayRequest();
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
                xDicUser.Add("QUITAC_BANCHAM_ID", typeof(decimal));
                xDicUser.Add("CHON", typeof(int));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("THOIGIAN_LUUKHO", typeof(int));
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
                        return _QuyTacBanHangCham_Bussiness.GetKey(frmTLQTHangBanCham.MaDVQLY, frmTLQTHangBanCham.SP_ID);
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
        /// Initialize_Grid
        /// </summary>
        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grd, this.grdView, false);
                Column xColumn;


                xColumn = new Column("Id", "Id");
                xColumn.Visible = false;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("CHON", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_Chon"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                xColumn.ColumnType = ColumnControl.Checkbox;
                xColumn.Binding = new Binding("CHON") { Converter = new ConverterStringBool(), ConverterParameter = "1:0", Mode = BindingMode.TwoWay };
                xColumn.AllowSorting = false;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 30;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaSanPham"]));
                xColumn.Width = 60;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenSanPham"]));
                xColumn.Width = 130;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("THOIGIAN_LUUKHO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblThoiGianChoPhep"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
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
        /// <summary>
        /// btnNew_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
        }

        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (iGridDataSource == null) return;
            iRowSelGb = ((DataRowView)this.grd.GetFocusedRow()).Row;
            for (int i = 0; i < iGridDataSource.Rows.Count; i++)
            {
                string MaDVQL = iGridDataSource.Rows[i]["MA_DVIQLY"].ToString();
                int SP_ID = int.Parse(iGridDataSource.Rows[i]["SANPHAM_ID"].ToString());
                DataTable dt = _QuyTacBanHangCham_Bussiness.GetKey(MaDVQL, SP_ID);
                flag = true;
                locate = i + 1;
                if (frmTLQTHangBanCham.status == true)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        base.ShowMessage(MessageType.Information, "SẢN PHẨM TẠI DÒNG " + locate + " ĐÃ ĐƯỢC KHAI BÁO THƠI GIAN LƯU KHO");
                        return;
                    }
                }
                if (int.Parse(iGridDataSource.Rows[i]["THOIGIAN_LUUKHO"].ToString()) == 0)
                {
                    flag = false;
                    SetFocus();
                    return;
                }

                else
                {
                    iRowSelGb = iGridDataSource.Rows[i];
                    SaveData();
                }
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
                var k = (from r in dt.Rows.OfType<DataRow>() where r["SANPHAM_ID"].ToString() == dr["SANPHAM_ID"].ToString() select r).FirstOrDefault();
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

        private void btnChonSP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = null;
                xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP", null);
                //begin tao phieu chi tiet
                DataTable dt_SPCHON = (DataTable)xReturn;

                if ((dt_SPCHON != null) && (dt_SPCHON.Rows.Count > 0))
                {
                    for (int i = 0; i < dt_SPCHON.Rows.Count; i++)
                    {
                        if (ContainDataRowInDataTable(dt_PHIEU_CTIET, dt_SPCHON.Rows[i]) == false)
                        {
                            DataRow dr = dt_PHIEU_CTIET.NewRow();
                            dr["CHON"] = 0;
                            dr["MA_DVIQLY"] = ConstCommon.DonViQuanLy;
                            dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                            dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                            dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                            dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                            dr["THOIGIAN_LUUKHO"] = 0;
                            dt_PHIEU_CTIET.Rows.Add(dr);
                        }
                    }
                    if (dt_PHIEU_CTIET != null)
                    {
                        this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);
                        iGridDataSource = dt_PHIEU_CTIET;
                    }
                    else
                    {
                        grd.ItemsSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSP.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnXoadong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((dt_PHIEU_CTIET == null) || (dt_PHIEU_CTIET.Rows.Count == 0))
                {
                    return;
                }
                int count = iGridDataSource.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (iGridDataSource.Rows[i]["CHON"].ToString() == "1")
                    {
                        dt_PHIEU_CTIET.Rows.Remove(iGridDataSource.Rows[i]);
                        i--;
                        count--;
                    }
                }


                iGridDataSource = dt_PHIEU_CTIET;
                this.iGridHelper.BindItemSource(dt_PHIEU_CTIET);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSP.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        public void SetFocus()
        {
            if (flag == false)
            {
                base.ShowMessage(MessageType.Information, "THỜI GIAN LƯU KHO " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
        }

        private void SaveData()
        {
            bool result = _QuyTacBanHangCham_Bussiness.Insert_Udate(this.iRowSelGb);
            if (result == false)
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