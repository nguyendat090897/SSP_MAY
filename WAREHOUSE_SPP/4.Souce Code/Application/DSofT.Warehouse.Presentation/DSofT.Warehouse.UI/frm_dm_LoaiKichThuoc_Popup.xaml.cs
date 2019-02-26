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

namespace DSofT.Warehouse.UI
{
    /// <summary>
    /// Interaction logic for frm_dm_LoaiKichThuoc.xaml
    /// </summary>
    public partial class frm_dm_LoaiKichThuoc_Popup : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        IDM_LOAI_KTHUOCBussiness _DM_LOAI_KTHUOCBussiness;
        DataRow iRowSelGb = null;
        decimal pDAI = 0;
        decimal pRONG = 0;
        decimal pCAO = 0;
        public frm_dm_LoaiKichThuoc_Popup()
        {
            InitializeComponent();
            _DM_LOAI_KTHUOCBussiness = new DM_LOAI_KTHUOCBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            
        }


        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGb = this.Parameter[0] as DataRow;
                DispalayRequest();
                pDAI =ConstCommon.NVL_NUM_DECIMAL_NEW(this.iDataSource.Rows[0]["DAI"].ToString().Trim());
                pRONG = ConstCommon.NVL_NUM_DECIMAL_NEW(this.iDataSource.Rows[0]["RONG"].ToString().Trim());
                pCAO = ConstCommon.NVL_NUM_DECIMAL_NEW(this.iDataSource.Rows[0]["CAO"].ToString().Trim());
                loadRDB_TinhTrang();
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

        /// <summary>
        /// Lưu dữ liệu vào db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(kiemTraNull() == false)
                {
                    return;
                }

                DataTable dt = _DM_LOAI_KTHUOCBussiness.GetListDM_LOAI_KTHUOC_DRC(ConstCommon.DonViQuanLy, ConstCommon.NVL_NUM_DECIMAL_NEW(txtD.Text), ConstCommon.NVL_NUM_DECIMAL_NEW(txtR.Text), ConstCommon.NVL_NUM_DECIMAL_NEW(txtC.Text));

                if (frm_dm_LoaiKichThuoc.status == true && dt != null && dt.Rows.Count > 0)
                {
                    base.ShowMessage(MessageType.Information, "KÍCH THƯỚC ĐÃ TÔN TẠI. VUI LÒNG NHẬP KÍCH THƯỚC KHÁC");
                }
                else
                {
                   
                        if (pDAI != ConstCommon.NVL_NUM_DECIMAL_NEW(this.iDataSource.Rows[0]["DAI"].ToString()) && pRONG != ConstCommon.NVL_NUM_DECIMAL_NEW(this.iDataSource.Rows[0]["RONG"].ToString()) && pCAO != ConstCommon.NVL_NUM_DECIMAL_NEW(this.iDataSource.Rows[0]["CAO"].ToString()))
                        {
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                LuuRDB_TinhTrang();
                                bool result = _DM_LOAI_KTHUOCBussiness.InsertOrUpdateDM_LOAI_KTHUOC(this.iDataSource.Copy());
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
                            else
                            {
                                base.ShowMessage(MessageType.Information, "KÍCH THƯỚC ĐÃ TÔN TẠI. VUI LÒNG NHẬP KÍCH THƯỚC KHÁC");
                                txtD.Focus();
                                return;
                            }
                        }
                        else
                        {
                            LuuRDB_TinhTrang();
                            bool result = _DM_LOAI_KTHUOCBussiness.InsertOrUpdateDM_LOAI_KTHUOC(this.iDataSource.Copy());
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
                }   
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("LOAI_KTHUOC_ID", typeof(decimal));
                xDicUser.Add("MA_SIZE", typeof(string));
                xDicUser.Add("DAI", typeof(decimal));
                xDicUser.Add("RONG", typeof(decimal));
                xDicUser.Add("CAO", typeof(decimal));
                xDicUser.Add("THETICH", typeof(decimal));
                xDicUser.Add("TINH_TRANG", typeof(Int16));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("RowVersion", typeof(long));
                xDicUser.Add("IsDelete", typeof(bool));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));

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
        /// Đóng Popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LuuRDB_TinhTrang()
        {
            try
            {
                if (rdbSD.IsChecked == true)
                {
                    this.iDataSource.Rows[0]["TINH_TRANG"] = 1;
                }
                 if (rdbKSD.IsChecked == true)
                {
                    this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void loadRDB_TinhTrang()
        {
            try
            {
                if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString() == "1")
                {
                    rdbSD.IsChecked = true;
                }
                if (this.iDataSource.Rows[0]["TINH_TRANG"].ToString() == "0")
                {
                    rdbKSD.IsChecked = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtD_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                txtMaKT.Text = txtD.Text + "x" + txtR.Text + "x" + txtC.Text;
                txtV.EditValue =    ConstCommon.NVL_NUM_DECIMAL_NEW(txtD.EditValue) * ConstCommon.NVL_NUM_DECIMAL_NEW(txtR.EditValue) * ConstCommon.NVL_NUM_DECIMAL_NEW(txtC.EditValue);
            }
            catch(Exception ex)
            {
                throw ex;
            }
                      
        }

        private void txtR_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                txtMaKT.Text = txtD.Text + "x" + txtR.Text + "x" + txtC.Text;
                txtV.EditValue = ConstCommon.NVL_NUM_DECIMAL_NEW(txtD.EditValue) * ConstCommon.NVL_NUM_DECIMAL_NEW(txtR.EditValue) * ConstCommon.NVL_NUM_DECIMAL_NEW(txtC.EditValue);

            }
            catch (Exception ex)
            {

                throw ex;
            }

           
        }

        private void txtC_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                txtMaKT.Text = txtD.Text + "x" + txtR.Text + "x" + txtC.Text;
                txtV.EditValue = ConstCommon.NVL_NUM_DECIMAL_NEW(txtD.EditValue) * ConstCommon.NVL_NUM_DECIMAL_NEW(txtR.EditValue) * ConstCommon.NVL_NUM_DECIMAL_NEW(txtC.EditValue);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public bool kiemTraNull()
        {
           if (txtD.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblNullCD"]));
                txtD.Focus();
                return false;
            }
           if (txtR.Text == string.Empty)
            {

                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblNullCR"]));
                txtR.Focus();
                return false;
            }
            if(txtC.Text == string.Empty)
            {

                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblNullCC"]));
                txtC.Focus();
                return false;
            }
            if (ConstCommon.NVL_NUM_DECIMAL_NEW(txtD.Text) < ConstCommon.NVL_NUM_DECIMAL_NEW(txtR.Text))
            {
                base.ShowMessage(MessageType.Information, "CHIỀU DÀI PHẢI LỚN HƠN HOẶC BẰNG CHIỀU RỘNG!!!");
                return false;
            }
   
            return true;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            txtMaKT.Text = txtD.Text = txtR.Text = txtC.Text = txtV.Text = txtGC.Text = string.Empty;
            rdbKSD.IsChecked = true;
        }
    }
}


