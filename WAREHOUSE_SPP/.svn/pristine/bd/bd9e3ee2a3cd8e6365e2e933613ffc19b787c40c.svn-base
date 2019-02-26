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
using DevExpress.Xpf.Editors;
using DSofT.Framework.Client.ExportHelper;
using Microsoft.Win32;
using System.Linq;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Ngo Gia Thien
    ///  Create date: 31/01/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Quan ly thu popup
    /// </summary>
    public partial class frmQuanLyThu_Popup : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        I_QuanLy_PhieuThu_KH_NCC_Bussiness _QuanLy_PhieuThu_KH_NCC_Bussiness;
        I_ThamSo_Bussiness _ThamSo_Bussiness;
        string SO_PHIEUTHU;
        bool flag = true;
        DataTable DT_GetData = null;
        string ngayPhieu;
        public frmQuanLyThu_Popup()
        {
            _QuanLy_PhieuThu_KH_NCC_Bussiness = new QuanLy_PhieuThu_KH_NCC_Bussiness(string.Empty);
            _ThamSo_Bussiness = new ThamSo_Bussiness(string.Empty);
            InitializeComponent();
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            DT_GetData = this.TableSchemaDefineBingding();
            txtSoTien.Text = "100000000000";
            loadComboBoxKH();
            if (frmQuanLyThu.status == true)
                txtNguoiTao.Text = CommonDataHelper.UserName;
            else
                txtNguoiTao.Text = frmQuanLyThu.createBy;
        }

        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("PHIEUTHU_ID", typeof(int));
                xDicUser.Add("KHACHHANG_NCC_ID", typeof(decimal));
                xDicUser.Add("HOPDONG_ID", typeof(decimal));
                xDicUser.Add("TEN_KH", typeof(string));
                xDicUser.Add("LOAI_PHIEU", typeof(string));
                xDicUser.Add("MA_KH", typeof(string));
                xDicUser.Add("SO_HOPDONG", typeof(string));
                xDicUser.Add("SO_PHIEUTHU", typeof(string));
                xDicUser.Add("NGAY_PHIEU", typeof(string));
                xDicUser.Add("SO_TIEN", typeof(Decimal));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TEN_CTY", typeof(string));
                xDicUser.Add("DIA_CHI", typeof(string));
                xDicUser.Add("HO_TEN_NGUOINOP", typeof(string));
                xDicUser.Add("DIA_CHI_NGUOINOP", typeof(string));
                xDicUser.Add("KEM_THEO", typeof(string));
                xDicUser.Add("CHUNG_TU", typeof(string));
                //xDicUser.Add("CreateBy", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
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

        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    
                    SO_PHIEUTHU = this.iDataSource.Rows[0]["SO_PHIEUTHU"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void loadComboBoxKH()
        {
            try
            {
                DataTable DMKH = _QuanLy_PhieuThu_KH_NCC_Bussiness.GetKH(ConstCommon.DonViQuanLy,"KH");
                
                if (DMKH != null && DMKH.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(this.cbbTenKH, "TEN_KH", "KHACHHANG_NCC_ID", DMKH, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cbbTenKH, Convert.ToString(UtilLanguage.ApplyLanguage()[/*"lblNSX"*/"lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["KHACHHANG_NCC_ID"] = cbbTenKH.GetKeyValue(0);
                }
                loadComboBoxHD(frmQuanLyThu.KH_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void loadComboBoxHD(int HD_ID)
        {
            //DataTable DMHD = _QuanLy_PhieuThu_KH_NCC_Bussiness.GetHD(ConstCommon.DonViQuanLy, int.Parse(cbbTenKH.EditValue.ToString()));
            DataTable DMHD = _QuanLy_PhieuThu_KH_NCC_Bussiness.GetHD(ConstCommon.DonViQuanLy, HD_ID);
            if (DMHD != null || DMHD.Rows.Count > 0)
            {
                ComboBoxUtil.SetComboBoxEdit(this.cbbTenHD, "SO_HOPDONG", "HOPDONG_ID", DMHD, SelectionTypeEnum.Native);
                ComboBoxUtil.InsertItem(cbbTenHD, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                this.iDataSource.Rows[0]["HOPDONG_ID"] = cbbTenHD.GetKeyValue(0);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbbTenKH_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if(flag==false)
                loadComboBoxHD(int.Parse(this.iDataSource.Rows[0]["KHACHHANG_NCC_ID"].ToString()));
            else
            {
                flag = false;
            }


        }

        void SetFocus()
        {
            if(cbbTenKH.Text=="--Chọn--")
            {
                base.ShowMessage(MessageType.Information, "KHÁCH HÀNG " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if(cbbTenHD.Text == "--Chọn--")
            {
                base.ShowMessage(MessageType.Information, "HÓA ĐƠN " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if(txtSoPhieuThu.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "SỐ PHIẾU THU " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if (txtSoTien.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "SỐ TIỀN " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if (dtpNgayPhieu.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "NGÀY PHIẾU " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if(txtHO_TEN_NGUOINOP.Text==string.Empty)
            {
                base.ShowMessage(MessageType.Information, "Họ tên người nộp " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
            if (txtDIA_CHI_NGUOINOP.Text == string.Empty)
            {
                base.ShowMessage(MessageType.Information, "Địa chỉ người nộp " + Convert.ToString(UtilLanguage.ApplyLanguage()["lblNoteNULL"]));
                return;
            }
        }
        void SaveData()
        {
            bool result;
            if (iDataSource.Rows[0]["PHIEUTHU_ID"].ToString() == "")
            {
                iDataSource.Rows[0]["LOAI_PHIEU"] = "THU";
                result = _QuanLy_PhieuThu_KH_NCC_Bussiness.Insert(this.iDataSource.Copy());
            }
            else
                 result = _QuanLy_PhieuThu_KH_NCC_Bussiness.Update(this.iDataSource.Copy());
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

      

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(cbbTenKH.Text == "--Chọn--" || cbbTenHD.Text == "--Chọn--"|| txtSoPhieuThu.Text == string.Empty|| txtSoTien.Text == string.Empty|| dtpNgayPhieu.Text == string.Empty || txtHO_TEN_NGUOINOP.Text == string.Empty || txtDIA_CHI_NGUOINOP.Text == string.Empty)
                SetFocus();
            else
            {
                if (SO_PHIEUTHU != iDataSource.Rows[0]["SO_PHIEUTHU"].ToString())
                {
                    DataTable dt = _QuanLy_PhieuThu_KH_NCC_Bussiness.CheckPT(ConstCommon.DonViQuanLy, iDataSource.Rows[0]["SO_PHIEUTHU"].ToString(),"THU");
                    if (dt.Rows.Count > 0)
                    {
                        base.ShowMessage(MessageType.Information, "SỐ PHIẾU THU ĐÃ TỒN TẠI ");
                        return;
                    }
                    else
                    {
                        SaveData();
                    }
                }
                else
                    SaveData();


            }
            
        }

        void Ngay()
        {
            string a = dtpNgayPhieu.Text.Substring(0, 2);
            string b = dtpNgayPhieu.Text.Substring(3, 2);
            string c = dtpNgayPhieu.Text.Substring(6, 4);
            ngayPhieu = "Ngày: " + b + " tháng: " + a + " năm: " + c;
        }

        private void btnIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExportExcel exp = new ExportExcel();
                DataSet dsSource = new DataSet();
                string filename = string.Empty;
                DataTable dt = new DataTable();
                Ngay();
                dt = this.iDataSource.Copy();
                dt.Rows[0]["NGAY_PHIEU"] = ngayPhieu;
                dt.TableName = "dt";
                string sotien= ConstCommon.So_tien_doi_ra_chu(double.Parse(dt.Rows[0]["SO_TIEN"].ToString()));
                dt.Columns.Add("SO_TIEN_CHU", typeof(string));
                dt.Rows[0]["SO_TIEN_CHU"] = sotien;
                dsSource.Tables.Add(dt);
                string templaleFileName = "RP_THU.xlsx";
               // DataTable dtTitle = new DataTable();
               // dtTitle.TableName = "dt2";
               // dtTitle.Columns.Add("TitleReport", typeof(string));
               // dtTitle.Columns.Add("SchoolName", typeof(string));
               // DataRow drTitle = dtTitle.NewRow();
               // drTitle["TitleReport"] = string.Format("TIẾP PHẨM TUẦN ({0} -> {1})", this.iDataSource.Rows[0]["FromDay"].ToString(), this.iDataSource.Rows[0]["ToDay"].ToString());
                //drTitle["SchoolName"] = string.Format("Trường: {0}", cboSchool.Text);
                //dtTitle.Rows.Add(drTitle);
                //dsSource.Tables.Add(dtTitle);
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Excel files (*.xlsx or .xls)|.xlsx;*.xls";
                dlg.FileName = string.Format("{0}", Guid.NewGuid());
                var result = dlg.ShowDialog();
                if (result==true)
                {
                    filename = dlg.FileName;
                    var export = exp.Return(filename, templaleFileName, dsSource);
                    if (export)
                    {
                        System.Diagnostics.Process.Start(filename);
                        ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                //UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnExcel.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
    }

    }


