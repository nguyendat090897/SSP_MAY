﻿using DSofT.Warehouse.Business;
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

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 23/01/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Danh muc nguyen lieu popup
    /// </summary>
    public partial class frmDanhMucNguyenLieu_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        public frmDanhMucNguyenLieu_popup()
        {
            InitializeComponent();         
            this.iDataSource = this.TableSchemaDefineBingding();           
            this.DataContext = this.iDataSource;
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
                xDicUser.Add("MaNguyenLieu", typeof(string));
                xDicUser.Add("TenNguyenLieu", typeof(string));
                xDicUser.Add("NhomNguyenLieu", typeof(string));
                xDicUser.Add("TieuChuanDuocChat", typeof(string));
                xDicUser.Add("NSX", typeof(string));
                xDicUser.Add("NhaSX", typeof(string));
                xDicUser.Add("DiaChiNhaSX", typeof(string));
                xDicUser.Add("GiaMua", typeof(decimal));
                xDicUser.Add("GhiChu", typeof(string));
                xDicUser.Add("Id", typeof(decimal));
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
            //something
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
