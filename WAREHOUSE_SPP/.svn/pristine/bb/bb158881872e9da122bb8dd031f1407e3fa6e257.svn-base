using System;
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
    /// dev: Ngo Gia Thien
    /// Description: Danh muc loai kho popup
    /// </summary>
    public partial class frm_DanhMucLoaiKho_Popup : PopupBase
    {
        DataTable iDataSource = null;
        GridHelper iGridHelper = null;
        DataTable iGridDataSource = null;
        IDM_LOAIKHOBussiness _DM_LOAI_KHOBussiness;
        DataRow iRowSelGb = null;
        string pTEN_LOAIKHO = string.Empty;
        public frm_DanhMucLoaiKho_Popup()
        {
            InitializeComponent();
            _DM_LOAI_KHOBussiness = new DM_LOAIKHOBussiness(String.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
        }


        public override void OnPopupShown()
        {
            if (this.Parameter != null && this.Parameter.Count() > 0)
            {
                iRowSelGb = this.Parameter[0] as DataRow;
                DispalayRequest();
                loadRDB_TinhTrang();
                pTEN_LOAIKHO = this.iDataSource.Rows[0]["TEN_LOAIKHO"].ToString();
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
                DataTable dt = _DM_LOAI_KHOBussiness.GetListDM_LOAIKHO_TEN(ConstCommon.DonViQuanLy, txtLoaiKho.Text);
                if (txtLoaiKho.Text == string.Empty)
                {
                    base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteFull"]));
                }
                else if (rdbSD.IsChecked == false && rdbKSD.IsChecked == false)
                {
                    base.ShowMessage(MessageType.Information, "BẠN CHƯA CHỌN TÌNH TRẠNG SỬ DỤNG!!!");
                }
                else
                {
                    if (frm_DanhMucLoaiKho.status == true && dt != null && dt.Rows.Count > 0)
                    {
                        base.ShowMessage(MessageType.Information, "LOẠI KHO ĐÃ TÔN TẠI. VUI LÒNG NHẬP LOẠI KHO KHÁC");
                    }
                    else
                    {
                        if (pTEN_LOAIKHO != this.iDataSource.Rows[0]["TEN_LOAIKHO"].ToString().Trim())
                        {
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                LuuRDB_TinhTrang();
                                bool result = _DM_LOAI_KHOBussiness.InsertOrUpdateDM_LOAIKHO(this.iDataSource.Copy());
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
                                base.ShowMessage(MessageType.Information, "TÊN LOẠI KHO ĐÃ TÔN TẠI. VUI LÒNG NHẬP TÊN KHÁC");
                                txtLoaiKho.Focus();
                                return;
                            }
                        }
                        else
                        {
                            LuuRDB_TinhTrang();
                            bool result = _DM_LOAI_KHOBussiness.InsertOrUpdateDM_LOAIKHO(this.iDataSource.Copy());
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
                xDicUser.Add("LOAIKHO_ID", typeof(decimal));
                xDicUser.Add("TEN_LOAIKHO", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(Int16));
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
                base.ShowMessage(MessageType.Error, ex.Message, ex);
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
                if(this.iDataSource.Rows[0]["TINH_TRANG"].ToString() == "0")
                {
                    rdbKSD.IsChecked = true;
                }
            }
            catch (Exception ex)
            {

                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            txtLoaiKho.Text = txtGC.Text = String.Empty;
            rdbKSD.IsChecked = true;
        }
    }
}



