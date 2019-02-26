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
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Ngô Gia Thiên
    ///  Create date: 06/03/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Danh muc pallet dieu chuyen
    /// </summary>
    public partial class frm_dm_pallet_dieuchuyen : PopupBase
    {
        DataTable iDataSource = null;
        DataTable iGridDataSource = null;
        GridHelper iGridHelper = null;

        public frm_dm_pallet_dieuchuyen()
        {
            InitializeComponent();
            Initialize_Grid();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

                xColumn = new Column("Chon", Convert.ToString(UtilLanguage.ApplyLanguage()["frm_phanquyen_quanlykho_Chon"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MAPL", Convert.ToString(UtilLanguage.ApplyLanguage()["lblMaPallet"]));
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TENPL", Convert.ToString(UtilLanguage.ApplyLanguage()["lblTenPallet"]));
                xColumn.Width = 600;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                this.iGridHelper.Initialize();
                //this.grdViewDMSP.AutoWidth = false;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// lap phieu dieu chuyen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInphieu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_dm_pallet_tiepnhandieuchuyen", null);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnInphieu.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }
}
