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
    ///  Dev: Luong Tien Trung
    /// </summary>
    public partial class frmDanhMucNhaSanXuat_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        IDM_NHA_SXUATBussiness nsx;
        public frmDanhMucNhaSanXuat_popup()
        {
            InitializeComponent();         
            this.iDataSource = this.TableSchemaDefineBingding();           
            this.DataContext = this.iDataSource;
            nsx = new DM_NHA_SXUATBussiness(string.Empty);
            loadDataFor_Cmb_QUOCGIA();

            
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
                xDicUser.Add("NHA_SXUAT_ID", typeof(decimal));
                xDicUser.Add("MA_NHA_SXUAT", typeof(string));
                xDicUser.Add("TEN_NHA_SXUAT", typeof(string));
                xDicUser.Add("QUOCGIA_ID", typeof(decimal));
                xDicUser.Add("DIACHI", typeof(string));
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

        private void loadDataFor_Cmb_QUOCGIA()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = nsx.GetListDM_QUOCGIA(CommonDataHelper.DonViQuanLy.ToString());
            }
            catch
            {
            }
            if (dt != null && dt.Rows.Count>0)
            {
                ComboBoxUtil.SetComboBoxEdit(cboNuocSX, "TEN_QUOCGIA", "QUOCGIA_ID", dt, SelectionTypeEnum.Native);
                ComboBoxUtil.InsertItem(cboNuocSX, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                this.iDataSource.Rows[0]["QUOCGIA_ID"] = cboNuocSX.GetKeyValue(0);
            }

        }

        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        int check_exist_MaNSX(string maNSX)
        {
            int result = 0;
            try
            {
                result = nsx.Check_Exist_MA_NHA_SXUAT(CommonDataHelper.DonViQuanLy, maNSX);
            }
            catch { }
            return result;
        }

        int check_exist_MaNSX_For_Update(string maNSX, int idNSX)
        {
            int result = 0;
            try
            {
                result = nsx.check_Exist_MA_NHA_SXUAT_FOR_UPDATE(CommonDataHelper.DonViQuanLy, maNSX, idNSX);
            }
            catch { }
            return result;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //-----------------------------------------------
            //---------------------check insert or update---------------
            if (txtMaNhaSX.Text == "" || txtTenNhaSX.Text == "")
            {
                base.ShowMessage(MessageType.Information, "Mã và Tên nhà sản xuất không được để trống!");
                return;
            }

            if (!check_String_Unicode(txtMaNhaSX.Text))
            {
                base.ShowMessage(MessageType.Information, "Mã phải được viết liền không dấu và không chứa ký tự đặc biệt!");
                return;
            }

            if (cboNuocSX.EditValue.ToString() == "0")
            {
                base.ShowMessage(MessageType.Information, "Vui lòng chọn quốc gia!");
                return;
            }
            var id = iDataSource.Rows[0]["NHA_SXUAT_ID"];
            if (id.ToString() != string.Empty)
            {
                if (check_exist_MaNSX_For_Update(iDataSource.Rows[0]["MA_NHA_SXUAT"].ToString(), int.Parse(iDataSource.Rows[0]["NHA_SXUAT_ID"].ToString()))>0)
                {
                    base.ShowMessage(MessageType.Error, "Mã nhà sản xuất [" + txtMaNhaSX.Text + "] đã tồn tại!");
                    txtMaNhaSX.Focus();
                    return;
                }
            }
            else
            {
                if (check_exist_MaNSX(txtMaNhaSX.Text) > 0)
                {
                    base.ShowMessage(MessageType.Error, "Mã nhà sản xuất [" + txtMaNhaSX.Text + "] đã tồn tại!");
                    txtMaNhaSX.Focus();
                    return;
                }
            }
            try
            {
                iDataSource.Rows[0]["MA_NHA_SXUAT"] = txtMaNhaSX.Text;
            }
            catch { }

            try
            {
                iDataSource.Rows[0]["TEN_NHA_SXUAT"] = txtTenNhaSX.Text;
            }
            catch { }

            try
            {
                iDataSource.Rows[0]["DIACHI"] = txtDiaChi.Text;
            }
            catch { }

            try
            {
                iDataSource.Rows[0]["QUOCGIA_ID"] = cboNuocSX.EditValue.ToString();
            }
            catch { }

            try
            {
                if (rbtSuDung.IsChecked == true)
                    iDataSource.Rows[0]["TINH_TRANG"] = "1";
                if (rbtKhongSuDung.IsChecked == true)
                    iDataSource.Rows[0]["TINH_TRANG"] = "0";
            }
            catch { }
            try
            {
                int result = nsx.InsertorUpdateDM_NHA_SXUAT(this.iDataSource.Copy()); 
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

        private void btnThemMoi_Click(object sender, RoutedEventArgs e)
        {
            
            frmDanhMucNhaSanXuat.status = true;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                iDataSource.Rows[0]["MA_NHA_SXUAT"] = string.Empty;
                iDataSource.Rows[0]["DIACHI"] = string.Empty;
                iDataSource.Rows[0]["TEN_NHA_SXUAT"] = string.Empty;
                rbtKhongSuDung.IsChecked = rbtSuDung.IsChecked = false;
                cboNuocSX.SelectedIndex = 1;
                txtMaNhaSX.Focus();
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
