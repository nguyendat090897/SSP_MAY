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
    /// Interaction logic for ChuyenDoiDVT.xaml
    /// </summary>
    public partial class ChuyenDoiDVT : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper_SANPHAM_QUYDOI = null;
        DataRow iRowSelGb = null;
        DataTable dtQUYDOI_DVT = null;
        DataTable dtSANPHAM_QUYDOI = null;
        IDM_DONVI_TINHBussiness DM_DONVI_TINHBussiness;
        public ChuyenDoiDVT()
        {
            InitializeComponent();
            Initialize_Grid_QUYDOI_DVT();
            Initialize_Grid_SANPHAM_QUYDOI();
            DM_DONVI_TINHBussiness = new DM_DONVI_TINHBussiness(string.Empty);
            this.dtQUYDOI_DVT = this.TableSchemaDefineBingding_SANPHAM_QUYDOI();
            this.dtQUYDOI_DVT.Clear();
            this.dtSANPHAM_QUYDOI = this.TableSchemaDefineBingding_SANPHAM_QUYDOI();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadData();
            LoadComboBox();
        }

        #region Method
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
                xDicUser.Add("STT", typeof(int));
                xDicUser.Add("MA_DVIQLY", typeof(string));

                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("MA_DONVI_TINH_TU", typeof(string));
                xDicUser.Add("MA_DONVI_TINH_DEN", typeof(string));
                xDicUser.Add("SOLUONG_QUYDOI", typeof(int));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }

        private DataTable TableSchemaDefineBingding_SANPHAM_QUYDOI()
        {
            DataTable xDt = null;
            try
            {
                
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("STT", typeof(int));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("SANPHAM_QUYDOI_ID", typeof(decimal));
                xDicUser.Add("SOLUONG_QUYDOI", typeof(int));
                xDicUser.Add("MA_SANPHAM_KH", typeof(string));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));
                xDicUser.Add("MA_DONVI_TINH_TU", typeof(string));
                xDicUser.Add("MA_DONVI_TINH_DEN", typeof(string));


                xDicUser.Add("HOATCHAT_CHINH", typeof(string));
                xDicUser.Add("QUYCACHDONGGOI", typeof(string));
                xDicUser.Add("TEN_NGUON_NHAP", typeof(string));
                xDicUser.Add("TEN_DKIEN_BQUAN", typeof(string));
                xDicUser.Add("TEN_NHA_SXUAT", typeof(string));
                xDicUser.Add("TEN_QUOCGIA", typeof(string));
                xDicUser.Add("TEN_KH", typeof(string));
                xDicUser.Add("IS_KSDB", typeof(string));
                xDicUser.Add("TEN_NHOM_SPHAM", typeof(string));
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
        /// 
        private void Initialize_Grid_QUYDOI_DVT()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grdQUYDOI_DVT, this.grdViewQUYDOI_DVT, false);
                Column xColumn;


                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = false;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("SANPHAM_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblLoaiSanPham"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_NHOM_SPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNhomSanPham"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_MaSanPham"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_TenSPham"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xacnhan_hanghuhong_DonViTinh"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("ThanhPhan", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_ThanhPhan"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("DKIEN_BQUAN_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblQuyCachDongGoi"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("HOATCHAT_CHINH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNguonNhap"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NguonNhap", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_sanpham_dichvu_DKBQ"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_NHA_SXUAT", Convert.ToString(UtilLanguage.ApplyLanguage()["frmThietBi_NhaSX"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_QUOCGIA", Convert.ToString(UtilLanguage.ApplyLanguage()["frmThietBi_NuocSX"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_NCC"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("IS_KSDB", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_ThuocKSDB"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
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

        
        private void Initialize_Grid_SANPHAM_QUYDOI()
        {
            try
            {
                this.iGridHelper_SANPHAM_QUYDOI = new GridHelper(this.grdSANPHAM_QUYDOI, this.grdViewSANPHAM_QUYDOI, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("SANPHAM_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblLoaiSanPham"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("NHOM_SPHAM_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNhomSanPham"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_MaSanPham"]));
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_TenSPham"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDVTMD"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH_TU", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDonViTu"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("MA_DONVI_TINH_DEN", Convert.ToString(UtilLanguage.ApplyLanguage()["lblDonViDen"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("SOLUONG_QUYDOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblSLQuyDoi"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("ThanhPhan", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_ThanhPhan"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("QUYCACHDONGGOI", Convert.ToString(UtilLanguage.ApplyLanguage()["lblQuyCachDongGoi"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("NguonNhap", Convert.ToString(UtilLanguage.ApplyLanguage()["lblNguonNhap"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("DKIEN_BQUAN_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_xuatkho_sanpham_dichvu_DKBQ"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("NHA_SXUAT_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["frmThietBi_NhaSX"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("QUOCGIA_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["frmThietBi_NuocSX"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("KHACHHANG_NCC_ID", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieu_yeucau_nhapkho_NCC"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);

                xColumn = new Column("IS_KSDB", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_ThuocKSDB"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_SANPHAM_QUYDOI.Add(xColumn);


                this.iGridHelper_SANPHAM_QUYDOI.Initialize();
                //this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid2()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        public void LoadComboBox()
        {
            try
            {
                DataTable dtdvt = DM_DONVI_TINHBussiness.GetListDM_DONVI_TINH();
                if (dtdvt != null && dtdvt.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(this.txtTuDVT1, "TEN_DONVI_TINH", "MA_DONVI_TINH", dtdvt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(txtTuDVT1, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["MA_DONVI_TINH_TU"] = txtTuDVT1.GetKeyValue(0);


                }
                if (dtdvt != null && dtdvt.Rows.Count > 0)
                {

                    ComboBoxUtil.SetComboBoxEdit(this.txtDenDVT1, "TEN_DONVI_TINH", "MA_DONVI_TINH", dtdvt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(txtDenDVT1, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["MA_DONVI_TINH_DEN"] = txtDenDVT1.GetKeyValue(0);
                }
            }
            catch (Exception ex)
            {

                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// 
        private void SaveData()
        {
            bool result = DM_DONVI_TINHBussiness.InsertDM_SANPHAM_QUYDOI_DVT(dtQUYDOI_DVT,dtSANPHAM_QUYDOI,iDataSource,CommonDataHelper.UserName,ConstCommon.DonViQuanLy);
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
        private void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return DM_DONVI_TINHBussiness.GetListDM_SANPHAM_QUYDOI(ConstCommon.DonViQuanLy);
                    });
                    Action<DataTable> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.dtSANPHAM_QUYDOI = dt;
                            this.iGridHelper_SANPHAM_QUYDOI.BindItemSource(this.dtSANPHAM_QUYDOI);
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

        #endregion
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
#region UI
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dtQUYDOI_DVT.Rows.Count; i++)
            {
                if (txtTuDVT1.EditValue.ToString() != dtQUYDOI_DVT.Rows[i]["MA_DONVI_TINH"].ToString() && txtDenDVT1.EditValue.ToString() != dtQUYDOI_DVT.Rows[i]["MA_DONVI_TINH"].ToString())
                {
                    base.ShowMessage(MessageType.Information,"Sản phẩm quy đổi thuộc hàng [ " + i.ToString() + " ] không hợp lệ.");
                    return;
                }
            }
            if (txtTuDVT1.SelectedIndex == 0)
            {
                base.ShowMessage(MessageType.Information, "Chưa chọn [ĐƠN VỊ TÍNH] TỪ ");
                txtTuDVT1.Focus();
            }
            else if (txtDenDVT1.SelectedIndex == 0)
            {
                base.ShowMessage(MessageType.Information, "Chưa chọn [ĐƠN VỊ TÍNH] ĐẾN ");
                txtDenDVT1.Focus();
            }
            else if(txtTuDVT1.EditValue.ToString() == txtDenDVT1.EditValue.ToString())
            {
                base.ShowMessage(MessageType.Information, " [ĐƠN VỊ TÍNH] TỪ và [ĐƠN VỊ TÍNH] ĐẾN  không được trùng.");
                return;
            }
            else if (txtSLQuyDoi.Text == String.Empty)
                base.ShowMessage(MessageType.Information, "[SỐ LƯỢNG] QUY ĐỔI TRỐNG!");
            else
            {  
               SaveData();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lblChonSanPham_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP", null);

                //begin tao phieu chi tiet
                DataTable dt_SPCHON = (DataTable)xReturn;

                if ((dt_SPCHON != null) && (dt_SPCHON.Rows.Count > 0))
                {
                    if (dt_SPCHON.Rows[0]["GHI_CHU"].ToString() == "Y")
                    {

                        for (int i = 0; i < dt_SPCHON.Rows.Count; i++)
                        {
                            if (ContainDataRowInDataTable(this.dtQUYDOI_DVT, dt_SPCHON.Rows[i]) == false)
                            {
                                DataRow dr = this.dtQUYDOI_DVT.NewRow();
                                dr["STT"] = this.dtQUYDOI_DVT.Rows.Count + 1;
                                dr["MA_ITEM_TYPE"] = dt_SPCHON.Rows[i]["MA_ITEM_TYPE"];
                                dr["SANPHAM_ID"] = dt_SPCHON.Rows[i]["SANPHAM_ID"];
                                dr["MA_SANPHAM"] = dt_SPCHON.Rows[i]["MA_SANPHAM"];
                                dr["MA_SANPHAM_KH"] = dt_SPCHON.Rows[i]["MA_SANPHAM_KH"];
                                dr["TEN_SANPHAM"] = dt_SPCHON.Rows[i]["TEN_SANPHAM"];
                                dr["MA_DONVI_TINH"] = dt_SPCHON.Rows[i]["MA_DONVI_TINH"];

                                dr["HOATCHAT_CHINH"] = dt_SPCHON.Rows[i]["HOATCHAT_CHINH"];
                                dr["QUYCACHDONGGOI"] = dt_SPCHON.Rows[i]["QUYCACHDONGGOI"];
                                dr["TEN_NGUON_NHAP"] = dt_SPCHON.Rows[i]["TEN_NGUON_NHAP"];
                                dr["TEN_DKIEN_BQUAN"] = dt_SPCHON.Rows[i]["TEN_DKIEN_BQUAN"];
                                dr["TEN_NHA_SXUAT"] = dt_SPCHON.Rows[i]["TEN_NHA_SXUAT"];
                                dr["TEN_QUOCGIA"] = dt_SPCHON.Rows[i]["TEN_QUOCGIA"];
                                dr["TEN_KH"] = dt_SPCHON.Rows[i]["TEN_KH"];
                                dr["IS_KSDB"] = dt_SPCHON.Rows[i]["IS_KSDB"];
                                dr["TEN_NHOM_SPHAM"] = dt_SPCHON.Rows[i]["TEN_NHOM_SPHAM"];
                                this.dtQUYDOI_DVT.Rows.Add(dr);
                            }
                        }
                    }

                }

                if (this.dtQUYDOI_DVT != null)
                {
                    this.iGridHelper.BindItemSource(dtQUYDOI_DVT);
                }
                else
                {
                    grdQUYDOI_DVT.ItemsSource = null;
                }

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.lblChonSanPham.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

    }
    #endregion
}