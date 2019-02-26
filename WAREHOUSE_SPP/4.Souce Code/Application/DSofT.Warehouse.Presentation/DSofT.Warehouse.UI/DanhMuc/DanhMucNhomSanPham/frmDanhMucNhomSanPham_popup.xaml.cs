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
using DSofT.Framework.UICore.TaskProxy;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Nguyen Van Huynh
    ///  Create date: 30/01/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Danh muc nha san xuat popup
    /// </summary>
    public partial class frmDanhMucNhomSanPham_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        IDM_NHOM_SAN_PHAMBussiness nsp;
        public frmDanhMucNhomSanPham_popup()
        {
            InitializeComponent();         
            this.iDataSource = this.TableSchemaDefineBingding();           
            this.DataContext = this.iDataSource;
            nsp = new DM_NHOM_SAN_PHAMBussiness(string.Empty);
            loadDataFor_Cmb_Item_Type();


        }

        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGb = this.Parameter[0] as DataRow;
                DispalayRequest();
            }
            loadCkb();
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
                xDicUser.Add("NHOM_SPHAM_ID", typeof(decimal));
                xDicUser.Add("MA_NHOM_SPHAM", typeof(string));
                xDicUser.Add("TEN_NHOM_SPHAM", typeof(string));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }
        void loadCkb()
        {
            try
            {
                if (iRowSelGb["TINH_TRANG"] != null)
                {
                    if (iRowSelGb["TINH_TRANG"].ToString() == "True")
                        rbtSuDung.IsChecked = true;
                    else
                        rbtKhongSuDung.IsChecked = true;
                }
                else
                {
                    rbtKhongSuDung.IsChecked = rbtSuDung.IsChecked = false;
                }
            }
            catch { }
        }
        private void loadDataFor_Cmb_Item_Type()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = nsp.GetList_ITEM_TYPE();
            }
            catch
            {
            }
            if (dt != null && dt.Rows.Count>0)
            {
                ComboBoxUtil.SetComboBoxEdit(cboItemType, "TEN_ITEM_TYPE", "MA_ITEM_TYPE", dt, SelectionTypeEnum.Native);
                ComboBoxUtil.InsertItem(cboItemType, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                this.iDataSource.Rows[0]["MA_ITEM_TYPE"] = cboItemType.GetKeyValue(0);
            }

        }

        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        int check_exist_Mansp(string mansp)
        {
            int result = 0;
            try
            {
                result = nsp.Check_Exist_MA_NHOM_SAN_PHAM(CommonDataHelper.DonViQuanLy, mansp);
            }
            catch { }
            return result;
        }

        int check_exist_Mansp_For_Update(string mansp, int idnsp)
        {
            int result = 0;
            try
            {
                result = nsp.Check_Exist_MA_NHOM_SAN_PHAM_For_Update(mansp, idnsp);
            }
            catch { }
            return result;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //-----------------------------------------------
            if (txtMaNhomSPham.Text == "" || txtTenNhomSPham.Text == "")
            {
                base.ShowMessage(MessageType.Information, "Mã và Tên nhóm sản phẩm không được để trống!", notificationService);
                return;
            }

            if (!check_String_Unicode(txtMaNhomSPham.Text))
            {
                base.ShowMessage(MessageType.Information, "Mã phải được viết liền không dấu và không chứa ký tự đặc biệt!", notificationService);
                return;
            }

            if (cboItemType.EditValue.ToString() == "0")
            {
                base.ShowMessage(MessageType.Information, "Vui lòng chọn kiểu dữ liệu!", notificationService);
                return;
            }
            var id = iDataSource.Rows[0]["NHOM_SPHAM_ID"];
            if (id.ToString() != string.Empty)
            {
                if (check_exist_Mansp_For_Update(iDataSource.Rows[0]["MA_NHOM_SPHAM"].ToString(), int.Parse(iDataSource.Rows[0]["NHOM_SPHAM_ID"].ToString())) > 0)
                {
                    base.ShowMessage(MessageType.Error, "Mã nhóm [" + txtMaNhomSPham.Text + "] đã tồn tại!");
                    txtMaNhomSPham.Focus();
                    return;
                }
            }
            else
            {
                if (check_exist_Mansp(txtMaNhomSPham.Text) > 0)
                {
                    base.ShowMessage(MessageType.Error, "Mã nhóm [" + txtMaNhomSPham.Text + "] đã tồn tại!");
                    txtMaNhomSPham.Focus();
                    return;
                }
            }
            try
            {
                iDataSource.Rows[0]["MA_NHOM_SPHAM"] = txtMaNhomSPham.Text;
            }
            catch { }

            try
            {
                iDataSource.Rows[0]["TEN_NHOM_SPHAM"] = txtTenNhomSPham.Text;
            }
            catch { }
            
            try
            {
                iDataSource.Rows[0]["GHI_CHU"] = txtGhiChu.Text;
            }
            catch { }

            try
            {
                iDataSource.Rows[0]["MA_ITEM_TYPE"] = cboItemType.EditValue.ToString();
            }
            catch { }

            try
            {
                if (rbtSuDung.IsChecked == true)
                    iDataSource.Rows[0]["TINH_TRANG"] = 1;
                if(rbtKhongSuDung.IsChecked==true)
                    iDataSource.Rows[0]["TINH_TRANG"] = 0;
            }
            catch { }

            if (iDataSource.Rows[0]["NHOM_SPHAM_ID"] == null)
            {
                if (check_exist_Mansp(txtMaNhomSPham.Text) > 0)
                {
                    base.ShowMessage(MessageType.Error, "Mã nhóm [" + txtMaNhomSPham.Text + "] đã tồn tại!");
                    txtMaNhomSPham.Focus();
                    return;
                }
            }

            try
            {
                int result = nsp.InsertorUpdateDM_NHOM_SAN_PHAM(this.iDataSource.Copy()); 
                if (result==0)
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                    return;
                }
                else
                {
                    ToastMessage.ShowToastMessage("Thông báo", "Thao tác thành công!", notificationService);
                }
            }
            catch(Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                return;
            }
            
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public bool check_String_Unicode(string str)
        {
            if (str == "")
            {
                return false;
            }
            char[] varChar = str.ToCharArray();
            int i = 0;
            while (i < varChar.Length &&
                ((Convert.ToInt32(varChar[i]) >= 97 && Convert.ToInt32(varChar[i]) <= 122)|| (Convert.ToInt32(varChar[i]) >= 65 && Convert.ToInt32(varChar[i]) <= 90)
                || (Convert.ToInt32(varChar[i]) >= 48 && Convert.ToInt32(varChar[i]) <= 57)))
            {
                i++;
            }
            if (i < varChar.Length)
            {
                return false;
            }
            return true;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            frmDanhMucNhomSanPham.status = true;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                iDataSource.Rows[0]["MA_NHOM_SPHAM"] = string.Empty;
                iDataSource.Rows[0]["TEN_NHOM_SPHAM"] = string.Empty;
                iDataSource.Rows[0]["GHI_CHU"] = string.Empty;
                cboItemType.SelectedIndex = 1;
                rbtSuDung.IsChecked = false;
                rbtKhongSuDung.IsChecked = false;
                txtMaNhomSPham.Focus();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }
    }

}