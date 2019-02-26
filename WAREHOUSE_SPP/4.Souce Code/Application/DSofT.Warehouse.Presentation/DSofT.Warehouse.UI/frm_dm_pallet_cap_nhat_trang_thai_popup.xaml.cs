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
    ///  Author:	 Ngô Gia Thiên
    ///  Create date: 06/03/2018
    ///  Editor:      Nguyen Van Huynh
    ///  Modify date: 20/3/2018
    ///  Description: Danh muc cap nhap trang thai pallet popup
    ///  DEV:Nguyen Van Huynh
    /// </summary>
    public partial class frm_dm_pallet_cap_nhat_trang_thai_popup : PopupBase
    {
        DataTable iDataSource = null;
        DataTable xNhomPallet = null;
        IDM_PALLET_TRANGTHAIBussiness _DM_PALLE_TRANGTHAITBussiness;
        DataRow iRowSelGb = null;
        string pTINH_TRANG_PALLET,pLOAI_TRANG_THAI,pNGAY_TRANG_THAI = String.Empty;
        public frm_dm_pallet_cap_nhat_trang_thai_popup()
        {
            InitializeComponent();
            _DM_PALLE_TRANGTHAITBussiness = new DM_PALLET_TRANGTHAIBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            txtMA_DVIQLY.Text = ConstCommon.DonViQuanLy;
            txtNGUOI_CAP_NHAT.Text = CommonDataHelper.UserName;
            LoadComboBox();
        }

        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    pLOAI_TRANG_THAI = this.iDataSource.Rows[0]["LOAI_TRANGTHAI"].ToString().Trim();
                    pNGAY_TRANG_THAI = this.iDataSource.Rows[0]["NGAY_TRANGTHAI"].ToString().Trim();
                }
                GetTinhTrangPallet();
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
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
                xDicUser.Add("PALLET_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("MA_PALLET", typeof(string));
                xDicUser.Add("MA_VACH", typeof(string));
                xDicUser.Add("TEN_PALLET", typeof(string));
                xDicUser.Add("NHOM_PALLET_ID", typeof(decimal));
                xDicUser.Add("LOAI_KTHUOC_ID", typeof(decimal));
                xDicUser.Add("LOAI_PALLET_ID", typeof(decimal));
                xDicUser.Add("NHA_SXUAT_ID", typeof(decimal));
                xDicUser.Add("MA_PL_THEO_NSX", typeof(string));
                xDicUser.Add("TEN_PL_THEO_NSX", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(bool));
                xDicUser.Add("RowVersion", typeof(long));
                xDicUser.Add("IsDelete", typeof(bool));
                xDicUser.Add("CreateBy", typeof(string));
                xDicUser.Add("CreateDate", typeof(DateTime));
                xDicUser.Add("ModifiedBy", typeof(string));
                xDicUser.Add("ModifiedDate", typeof(DateTime));
                xDicUser.Add("TEN_NHOM_PALLET", typeof(string));
                xDicUser.Add("MA_SIZE", typeof(string));
                xDicUser.Add("TEN_LOAI_PALLET", typeof(string));
                xDicUser.Add("TEN_NHA_SXUAT", typeof(string));
                xDicUser.Add("PALLET_TRANGTHAI_ID", typeof(decimal));
                xDicUser.Add("LOAI_TRANGTHAI", typeof(string));
                xDicUser.Add("NGAY_TRANGTHAI", typeof(string));
                xDicUser.Add("GHI_CHU_TRANG_THAI", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }
        private void LoadComboBox()
        {
            try
            {
                DataTable dt = _DM_PALLE_TRANGTHAITBussiness.GetListDM_PALLET_NHOM_PALLET(ConstCommon.DonViQuanLy);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.xNhomPallet = dt;
                    ComboBoxUtil.SetComboBoxEdit(this.cboNHOM_PALLET_ID, "TEN_NHOM_PALLET", "NHOM_PALLET_ID", xNhomPallet, SelectionTypeEnum.Native);
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void SetTinhTrangPallet()
        {
            if (rdbChuaSuDung.IsChecked == true)
                pTINH_TRANG_PALLET = "CSD";
            else if (rdbDangSuDung.IsChecked == true)
                pTINH_TRANG_PALLET = "SD";
            else if (rdbHuHong.IsChecked == true)
                pTINH_TRANG_PALLET = "HH";
            else if (rdbThanhLy.IsChecked == true)
                pTINH_TRANG_PALLET = "TL";
            else
                pTINH_TRANG_PALLET = "CSD";
            txtLOAI_TRANGTHAI.Text = pTINH_TRANG_PALLET;
        }
        private void GetTinhTrangPallet()
        {
            pLOAI_TRANG_THAI = "CSD";
            if (this.iDataSource.Rows[0]["LOAI_TRANGTHAI"].ToString().Trim() != String.Empty)
                pLOAI_TRANG_THAI = this.iDataSource.Rows[0]["LOAI_TRANGTHAI"].ToString().Trim();
            try
            {
                if (pLOAI_TRANG_THAI == "CSD")
                    rdbChuaSuDung.IsChecked = true;
                else if (pLOAI_TRANG_THAI == "SD")
                    rdbDangSuDung.IsChecked = true;
                else if (pLOAI_TRANG_THAI == "HH")
                    rdbHuHong.IsChecked = true;
                else
                    rdbThanhLy.IsChecked = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SetFocus()
        {
            if(pLOAI_TRANG_THAI == String.Empty)
            {
                rdbChuaSuDung.IsChecked = true;
                base.ShowMessage(MessageType.Information, "[TRẠNG THÁI] PALLET " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
            }
            else
            {
                dtpNGAY_TRANGTHAI.Focus();
              //  dtpNGAY_TRANGTHAI.EditValue = DateTime.Now.ToString("dd/MM/yyyy");
                base.ShowMessage(MessageType.Information, "[NGÀY NHẬP] " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
            }
        }
        private void SaveData()
        {
            bool result = _DM_PALLE_TRANGTHAITBussiness.InsertorUpdateDM_PALLET_TRANGTHAI(this.iDataSource.Copy());
            if (!result)
            {

                base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                return;
            }
            else
            {
                ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                this.Close();
                return;
            }
        }
       
        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    DataTable dts = _DM_PALLE_TRANGTHAITBussiness.GetListDM_PALLET_TRANGTHAI_KEY(ConstCommon.DonViQuanLy,
                        ConstCommon.NVL_NUM_LONG_NEW(this.iDataSource.Rows[0]["PALLET_ID"].ToString().Trim()),txtLOAI_TRANGTHAI.Text, dtpNGAY_TRANGTHAI.Text);
                if (dtpNGAY_TRANGTHAI.Text != String.Empty && pLOAI_TRANG_THAI != String.Empty)
                {
                    if (dts != null && dts.Rows.Count > 0)
                    {
                        base.ShowMessage(MessageType.Information, "[TÊN],[TRẠNG THÁI],[NGÀY NHẬP] PALLET " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteID"]));
                    }
                    else
                    {
                        SaveData();
                    }
                }
                else
                    SetFocus();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rdbChuaSuDung_Click(object sender, RoutedEventArgs e)
        {
            SetTinhTrangPallet();
        }

        private void rdbDangSuDung_Click(object sender, RoutedEventArgs e)
        {
            SetTinhTrangPallet();
        }

        private void rdbHuHong_Click(object sender, RoutedEventArgs e)
        {
            SetTinhTrangPallet();
        }

        private void rdbThanhLy_Click(object sender, RoutedEventArgs e)
        {
            SetTinhTrangPallet();
        }
    }

}