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
    /// Interaction logic for frm_quytac_makytudau_sanpham.xaml
    /// </summary>
    public partial class frm_quytac_makytudau_sanpham : ControlBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;

        public frm_quytac_makytudau_sanpham()
        {
            InitializeComponent();
            Initialize_Grid_QuyTac_MaKyTuDau_SanPham();
        }
        /// <summary>
        /// Initialize_Grid_QuyTac_MaKyTuDau_SanPham
        /// </summary>
        private void Initialize_Grid_QuyTac_MaKyTuDau_SanPham()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.gridControl, this.grdViewQuyTacMaKyTuDauSanPham, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("LoaiSanPham", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_quytac_makytudau_sanpham_LoaiSanPham"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("QuyDinhKyTuDauCuaSanPham", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_quytac_makytudau_sanpham_QuyDinhKyTuDauCuaSanPham"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);
               
                this.iGridHelper.Initialize();
                this.grdViewQuyTacMaKyTuDauSanPham.AutoWidth = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid_QuyTac_MaKyTuDau_SanPham()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// btnNew_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
           //Code xu ly
        }
        private void LoadData()
        {
            try
            {
                using (var task = new AsyncTaskProcess(this))
                {

                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "LoadData()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void grdView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           //code if it have
        }
    }
}
