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
using DevExpress.Xpf.Editors;
using DSofT.Framework.UICore.TaskProxy;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Ngo Gia Thien
    ///  Create date: 30/01/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Thiet lap ton kho toi thieu popup
    /// </summary>
    public partial class frmThietLapTonKhoTT_Popup : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        DataRow iRowSelGb = null;
        DataTable dt_PHIEU_CTIET = null;
        I_ThietLapTonKhoTT_Bussiness _ThietLapTonKhoTT;
        string pHT_TLTK_ID = string.Empty;
        bool flag = true;
        int locate;
        public frmThietLapTonKhoTT_Popup()
        {
            _ThietLapTonKhoTT = new ThietLapTonKhoTT_Bussiness(string.Empty);
            InitializeComponent();
            Initialize_Grid();
            this.iDataSource = this.TableSchemaDefineBingding();
            dt_PHIEU_CTIET = this.TableSchemaDefineBingding();
            dt_PHIEU_CTIET.Clear();
            this.DataContext = this.iDataSource;
            loadComboBox();
            if (frmThietLapTonKhoTT.status == false)
                LoadData();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("QUITAC_BANCHAM_ID", typeof(decimal));
                xDicUser.Add("CHON", typeof(int));
                xDicUser.Add("STT", typeof(int));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("KHO_ID", typeof(decimal));
                xDicUser.Add("MA_KHO", typeof(string));
                xDicUser.Add("TEN_KHO", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("TON_MIN", typeof(decimal));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));
                xDicUser.Add("TON_TOITHIEU", typeof(string));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }

        public void loadComboBox()
        {
            try
            {
                DataTable DM_KHO = _ThietLapTonKhoTT.GetKho(ConstCommon.DonViQuanLy);
                if (DM_KHO != null && DM_KHO.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(this.cbbtenkho, "TEN_KHO", "KHO_ID", DM_KHO, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cbbtenkho, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["KHO_ID"] = cbbtenkho.GetKeyValue(0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    pHT_TLTK_ID = this.iDataSource.Rows[0]["QUITAC_BANCHAM_ID"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetIsNull()
        {
            this.iDataSource.Rows[0]["MA_TBI"] = string.Empty;
            this.iDataSource.Rows[0]["TEN_TBI"] = string.Empty;
            this.iDataSource.Rows[0]["THONG_SO"] = string.Empty;
            this.iDataSource.Rows[0]["NUOC_SAN_XUAT"] = string.Empty;
            this.iDataSource.Rows[0]["GHI_CHU"] = string.Empty;
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
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("KHO_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaKho"]));
                xColumn.Width = 100;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                /*xColumn = new Column("MA_KHO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaKho"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_KHO", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenKho"]));
                xColumn.Width = 300;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);*/

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaSanPham"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenSanPham"]));
                xColumn.Width = 300;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_ITEM_TYPE", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDVT1"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TON_MIN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTonTT"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.MaskType = MaskType.Numeric;
                xColumn.EditMask = "#,##,##,##,##,##,##,##,##,##,##,##,##,###;";
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
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

        private void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _ThietLapTonKhoTT.GetKey(frmThietLapTonKhoTT.MaDVQL,frmThietLapTonKhoTT.KHO_ID,frmThietLapTonKhoTT.SP_ID);
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

            {
                try
                {
                    if (cbbtenkho.Text != "--Chọn--")
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
                                    dr["KHO_ID"] = this.iDataSource.Rows[0]["KHO_ID"];
                                    //dr["MA_KHO"] = this.iDataSource.Rows[0]["MA_KHO"];
                                    //dr["TEN_KHO"] = this.iDataSource.Rows[0]["TEN_KHO"];
                                    dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                    dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                    dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                    dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                    dr["MA_DONVI_TINH"] = dt_SPCHON.Rows[i]["MA_DONVI_TINH"];
                                    dr["TON_MIN"] = 0 ;
                                    dt_PHIEU_CTIET.Rows.Add(dr);
                                }
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



                        //end tao phieu chi tiet



                    }
                    else SetFocus();
                }
                catch (Exception ex)
                {
                    UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnChonSP.Tag.ToString());
                    base.ShowMessage(MessageType.Error, ex.Message, ex);
                }
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
            if (cbbtenkho.Text == "--Chọn--")
            {
                base.ShowMessage(MessageType.Information, "TÊN KHO " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if(flag==false)
            {
                base.ShowMessage(MessageType.Information, "SỐ LƯỢNG MIN " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
        }

        private void SaveData()
        {
            bool result = _ThietLapTonKhoTT.HT_THIETLAPTONKHO_TT_Insert(this.iRowSelGb);
            if (result==false)
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
                if (iGridDataSource == null) return;
                iRowSelGb= ((DataRowView)this.grd.GetFocusedRow()).Row;
                if (cbbtenkho.Text != "--Chọn--")
                {
                    for (int i = 0; i < iGridDataSource.Rows.Count; i++)
                    {
                        string MaDVQL = iGridDataSource.Rows[i]["MA_DVIQLY"].ToString();
                        int KHO_ID = int.Parse(iGridDataSource.Rows[i]["KHO_ID"].ToString());
                        int SP_ID = int.Parse(iGridDataSource.Rows[i]["SANPHAM_ID"].ToString());
                        DataTable dt = _ThietLapTonKhoTT.GetKey(MaDVQL, KHO_ID, SP_ID);
                        flag = true;
                        locate = i + 1;
                        if (frmThietLapTonKhoTT.status == true)
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                base.ShowMessage(MessageType.Information, "SẢN PHẨM TẠI DÒNG "+ locate + " ĐÃ ĐƯỢC KHAI BÁO TỒN KHO");
                                return;
                            }
                        }
                        if (int.Parse( iGridDataSource.Rows[i]["TON_MIN"].ToString()) == 0)
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
                else
                    SetFocus();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }


        }
    }

        
}

