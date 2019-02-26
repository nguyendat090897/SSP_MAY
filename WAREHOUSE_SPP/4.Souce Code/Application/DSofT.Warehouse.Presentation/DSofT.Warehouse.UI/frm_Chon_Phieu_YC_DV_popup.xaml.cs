﻿using System;
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
using DevExpress.Xpf.Grid;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 
    ///  Create date: 08/03/2018
    ///  Editor:      
//
    ///  Description: Xay dung dinh muc san pham - Lap yeu cau dich vu
    /// </summary>
    public partial class frm_Chon_Phieu_YC_DV_popup : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper_CT = null;
        DataTable iGridDataSource = null;
        DataTable iGridDataSource_CT = null;
        IKho_Phieu_YeuCau_DVBussiness _YCDV;
        DataRow iRowSelGb = null;
        DataRow iRowSelGb_CT = null;
        DataSet ds_Chon = null;
        int iD_PhieuYC = 0;
        string pN_OR_N = "N";
        DataTable dt_SPCHON = new DataTable();
        DataTable dtSPCHON_Return = null;
        public frm_Chon_Phieu_YC_DV_popup()
        {
            InitializeComponent();
            _YCDV = new Kho_Phieu_YeuCau_DVBussiness(string.Empty);
            Initialize_Grid();
            Initialize_Grid_CT();
            iDataSource = TableSchemaDefineBingding();
            DataContext = iDataSource;
            LoadDataGrdDichVu();
        }


        /// <summary>
        /// Initialize_Grid
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
        }
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("PHIEUYEUCAU_DV_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("SOPHIEU", typeof(string));
                xDicUser.Add("NGAYLAP", typeof(string));
                xDicUser.Add("THOIGIAN_HOANTAT", typeof(string));
                xDicUser.Add("MA_LOAI_DICHVU", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TRANGTHAI_DUYET", typeof(string));
                xDicUser.Add("NGAY_DUYET", typeof(string));
                xDicUser.Add("NGUOI_DUYET", typeof(string));
                xDicUser.Add("LYDO", typeof(string));
                xDicUser.Add("DIEUKIEN_THUCHIEN", typeof(string));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }
        void LoadDataGrdDichVu()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _YCDV.GetListKho_Lam_Phieu_DV(CommonDataHelper.DonViQuanLy);

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
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadDataGrdDichVu()");
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
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("SOPHIEU", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieuxuat_tuPYC_SoPhieu"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_LOAI_DICHVU", "Loại dịch vụ");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAYLAP", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieuxuat_tuPYC_NgayLap"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                xColumn.EditMask = "dd/MM/yyy";
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("THOIGIAN_HOANTAT", "Thời gian hoàn tất");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("DIEUKIEN_THUCHIEN", "Điều kiện thực hiện");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TRANGTHAI_DUYET", "Trạng thái duyệt");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGAY_DUYET","Ngày duyệt");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("NGUOI_DUYET", "Người duyệt");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("LYDO", "Lý do");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("GHI_CHU", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_lapphieuxuat_tuPYC_GhiChu"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                this.iGridHelper.Initialize();
                ConstCommon.setAutoFilterConditionGrid(grd);
                this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void Initialize_Grid_CT()
        {
            try
            {
                this.iGridHelper_CT = new GridHelper(this.grdChiTietDV, this.grdViewChiTietDV, false);
                Column xColumn;

                xColumn = new Column("STT", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);


                xColumn = new Column("TEN_KHO", "Thuộc kho");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_KHO_KHUVUC", "Khu vực kho");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_ITEM_TYPE", "Item type");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", "Tên sản phẩm");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SOLO", "Số lô");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("HANDUNG", "Hạn dùng");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_TONG", "Số lượng tổng");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_THUNG", "Số lượng thùng");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SO_LUONG_LE", "Số lượng lẻ");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("SODK", "Số đăng ký");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                xColumn = new Column("GIAYPHEP_NK", "Giấy phép nhập kho");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_CT.Add(xColumn);

                this.iGridHelper_CT.Initialize();
                //this.grdView.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid_CT()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        void LoadDataChiTiet()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {
                    ServiceAction<DataTable> action = ((pd) =>
                    {
                        task.SetCallContext();
                        return _YCDV.GetListKho_Lam_Phieu_DV_By_ID(CommonDataHelper.DonViQuanLy, iD_PhieuYC).Tables[1];

                    });
                    Action<DataTable> onComplete = (dt =>
                    {
                        this.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            this.iGridDataSource_CT = dt;
                            this.iGridHelper_CT.BindItemSource(this.iGridDataSource_CT);
                        }));
                    });
                    task.AsyncTaskExecute(action, onComplete);
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadDataGrdDichVu()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void grdView_FocusedRowHandleChanged(object sender, DevExpress.Xpf.Grid.FocusedRowHandleChangedEventArgs e)
        {
            if(grdView.FocusedRowHandle>=0)
            {
                iRowSelGb = ((DataRowView)this.grd.GetFocusedRow()).Row;
                DispalayRequest();
                iD_PhieuYC = NumberHelper.ConvertStringToInt(grd.GetFocusedRowCellValue("PHIEUYEUCAU_DV_ID").ToString());
                LoadDataChiTiet();
            }
        }
        
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDataGrdDichVu();
        }
        public override object UIReturn(string source)
        {
            try
            {

                return dtSPCHON_Return;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
                return null;
            }

        }
        private void btnTao_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int PHIEUYEUCAU_DV_ID = 0;
                if (grdView.FocusedRowHandle>=0)
                {
                    PHIEUYEUCAU_DV_ID=ConstCommon.NVL_NUM_NT_NEW(grd.GetFocusedRowCellValue("PHIEUYEUCAU_DV_ID"));
                    if(PHIEUYEUCAU_DV_ID==0)
                    {
                        return;
                    }
                }
                DataTable ds = _YCDV.GetList_LamPhieuDV_CTiet_NVL_NEW(CommonDataHelper.DonViQuanLy, PHIEUYEUCAU_DV_ID);
                dt_SPCHON = ds;
                dtSPCHON_Return = dt_SPCHON.Copy();


            }
            catch(Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "btnTao_Click()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            finally
            {
                this.Close();
            }
        }
    }
}