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
    ///  Create date: 05/03/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Danh muc san pham popup
    /// </summary>
    public partial class frm_DanhMucSanPham_Popup : PopupBase
    {
        public frm_DanhMucSanPham_Popup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// dong popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// goi khai bao GPNK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGPNK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_DanhMucSanPham_KhaiBaoGPNK", null);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnGPNK.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        /// <summary>
        /// add hinh anh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnh_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = open.Filter = "JPG files (*.jpg)|*.jpg|All file (*.*)|*.*";
            open.FilterIndex = 1;
            open.RestoreDirectory = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtAnh.Text = open.FileName;
                imgAnh.Source = new BitmapImage(new Uri(txtAnh.Text));
            }
        }

        /// <summary>
        /// khai bao quy cach san pham
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQCSP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_DanhMucSanPham_Popup1", null);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnQCSP.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }
}
