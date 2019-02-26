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
    ///  Description: Danh muc san pham popup 1
    /// </summary>
    public partial class frm_DanhMucSanPham_Popup1 : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        IDM_SanPham_ThungVLBussiness thungVL;
        public frm_DanhMucSanPham_Popup1()
        {
            InitializeComponent();
            thungVL = new DM_SanPham_ThungVLBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadCboLoaiThung();
            if (iRowSelGb != null)
                LoadTinhTrang();
        }
        
        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    LoadTinhTrang();
                }
            }
            catch (Exception ex)
            {
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        public void DispalayRequest()
        {
            
            try
            {
                if (this.iDataSource.Columns["SANPHAM_ID"] != null)
                {
                    this.iDataSource.Rows[0]["SANPHAM_ID"] = iRowSelGb["SANPHAM_ID"];
                }
                DataTable dt = thungVL.GetListDM_SanPham_ThungVL(CommonDataHelper.DonViQuanLy, int.Parse(iRowSelGb["SANPHAM_ID"].ToString()));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataColumn item in dt.Columns)
                    {
                        if (this.iDataSource.Columns[item.ColumnName] != null)
                        {
                            this.iDataSource.Rows[0][item.ColumnName] = dt.Rows[0][item.ColumnName];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "DispalayRequest()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("SANPHAM_THUNG_VATLY_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("KHOI_LUONG", typeof(decimal));
                xDicUser.Add("LOAI_THUNG", typeof(string));
                xDicUser.Add("CAO_MM", typeof(decimal));
                xDicUser.Add("RONG_MM", typeof(decimal));
                xDicUser.Add("DAI_MM", typeof(decimal));
                xDicUser.Add("BANKINH_MM", typeof(decimal));
                xDicUser.Add("THE_TICH", typeof(decimal));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(bool));

                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xDt;
        }
        void LoadData()
        {

        }
        void LoadCboLoaiThung()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("display");
            dt.Columns.Add("values");
            dt.Rows.Add("Dạng vuông", "VUONG");
            dt.Rows.Add("Dạng tròn", "TRON");
            if (dt != null && dt.Rows.Count > 0)
            {
                ComboBoxUtil.SetComboBoxEdit(cboLoaiThung, "display", "values", dt, SelectionTypeEnum.Native);
                ComboBoxUtil.InsertItem(cboLoaiThung, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                this.iDataSource.Rows[0]["LOAI_THUNG"] = cboLoaiThung.GetKeyValue(0);
            }
            
        }
        void LoadTinhTrang()
        {
            if (iDataSource.Rows[0]["TINH_TRANG"] != null)
            {
                if (iDataSource.Rows[0]["TINH_TRANG"].ToString() == "True")
                    rbtSuDung.IsChecked = true;
                else if(iDataSource.Rows[0]["TINH_TRANG"].ToString() == "False")
                {
                    rbtKhongSuDung.IsChecked = true;
                }
                    
            }
            else
            {
                rbtKhongSuDung.IsChecked = rbtSuDung.IsChecked = false;
            }
        }
        void LoadEnable()
        {
            if(cboLoaiThung.EditValue!=null)
            {
                if(cboLoaiThung.EditValue.ToString()=="VUONG")
                {
                    txtChieuDai.IsEnabled = true;
                    txtChieuRong.IsEnabled = true;
                    txtBanKinh.IsEnabled = false;
                    txtBanKinh.Clear();
                }
                else if(cboLoaiThung.EditValue.ToString()=="TRON")
                {
                    txtChieuDai.IsEnabled = false;
                    txtChieuRong.IsEnabled = false;
                    txtBanKinh.IsEnabled = true;
                    txtChieuDai.Clear();
                    txtChieuRong.Clear();
                }
            }
        }
        int CheckEmpty()
        {
            if(cboLoaiThung.EditValue!=null)
            {
                if(cboLoaiThung.EditValue.ToString()=="0")
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng chọn loại thùng!");
                    cboLoaiThung.Focus();
                    return 0;
                }
                if (txtChieuCao.Text == string.Empty)
                {
                    base.ShowMessage(MessageType.Information, "Vui lòng nhập chiều cao!");
                    txtChieuCao.Focus();
                    return 0;
                }
                if (cboLoaiThung.EditValue.ToString()=="VUONG")
                {
                    
                    if(txtChieuDai.Text==string.Empty)
                    {
                        base.ShowMessage(MessageType.Information, "Vui lòng nhập chiều dài!");
                        txtChieuDai.Focus();
                        return 0;
                    }
                    if(txtChieuRong.Text==string.Empty)
                    {
                        base.ShowMessage(MessageType.Information, "Vui lòng nhập chiều rộng!");
                        txtChieuRong.Focus();
                        return 0;
                    }
                }
                if(cboLoaiThung.EditValue.ToString()=="TRON")
                {
                    if (txtBanKinh.Text == string.Empty)
                    {
                        base.ShowMessage(MessageType.Information, "Vui lòng nhập bán kính!");
                        txtBanKinh.Focus();
                        return 0;
                    }
                }
            }
            if(rbtSuDung.IsChecked==false && rbtKhongSuDung.IsChecked==false)
            {
                base.ShowMessage(MessageType.Information, "Vui lòng chọn tình trạng!");
                return 0;
            }
            return 1;
        }
        int CheckSoAm()
        {
            if(txtTrongLuong.Text!=string.Empty && txtTrongLuong.Text.Substring(0,1)=="-")
            {
                base.ShowMessage(MessageType.Information, "Khối lượng phải là số dương!");
                txtTrongLuong.Focus();
                return 1;
            }
            if(txtChieuCao.Text!=string.Empty && txtChieuCao.Text.Substring(0,1)=="-")
            {
                base.ShowMessage(MessageType.Information, "Chiều cao phải là số dương!");
                txtChieuCao.Focus();
                return 1;
            }
            if(txtChieuDai.Text!=string.Empty && txtChieuDai.Text.Substring(0,1)=="-")
            {
                base.ShowMessage(MessageType.Information, "Chiều dài phải là số dương!");
                txtChieuDai.Focus();
                return 1;
            }
            if(txtChieuRong.Text!=string.Empty && txtChieuRong.Text.Substring(0,1)=="-")
            {
                base.ShowMessage(MessageType.Information, "Chiều rộng phải là số dương!");
                txtChieuRong.Focus();
                return 1;
            }
            if (txtBanKinh.Text != string.Empty && txtBanKinh.Text.Substring(0, 1) == "-")
            {
                base.ShowMessage(MessageType.Information, "Bán phải là số dương!");
                txtBanKinh.Focus();
                return 1;
            }
            return 0;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cboLoaiThung_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            LoadEnable();
        }

       
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckEmpty() == 0)
                    return;
                if (CheckSoAm() == 1)
                    return;
                if (iDataSource != null && iDataSource.Rows.Count > 0)
                {
                    if(iDataSource.Rows[0]["SANPHAM_THUNG_VATLY_ID"].ToString()==string.Empty)
                    {
                        DataTable dt = thungVL.GetListDM_SanPham_ThungVL(CommonDataHelper.DonViQuanLy, int.Parse(iRowSelGb["SANPHAM_ID"].ToString()));
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            iDataSource.Rows[0]["SANPHAM_THUNG_VATLY_ID"] = ConstCommon.NVL_NUM_DECIMAL_NEW(dt.Rows[0]["SANPHAM_THUNG_VATLY_ID"].ToString());
                        }
                    }
                    if (rbtSuDung.IsChecked == true)
                        iDataSource.Rows[0]["TINH_TRANG"] = "True";
                    else
                        iDataSource.Rows[0]["TINH_TRANG"] = "False";
                    int result = thungVL.InsertorUpdateDM_SanPham_ThungVL(this.iDataSource.Copy());
                    if (result > 0)
                    {
                        ToastMessage.ShowToastMessage("Thông báo", "Thành công!", notificationService);
                        return;
                    }
                    else
                    {
                        base.ShowMessage(MessageType.Information, "Không thành công!");
                        return;
                    }
                }
            }catch(Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void txtChieuCao_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if(iDataSource!=null && iDataSource.Rows.Count>0)
            {
                string s = "";
                string s2 = "";
                if (cboLoaiThung.EditValue != null)
                {
                    decimal dai, rong, cao, bankinh;
                    cao = ConstCommon.NVL_NUM_DECIMAL_NEW(txtChieuCao.Text.ToString());
                    if (cboLoaiThung.EditValue.ToString() == "VUONG")
                    {
                        dai = ConstCommon.NVL_NUM_DECIMAL_NEW(txtChieuDai.Text.ToString());
                        rong = ConstCommon.NVL_NUM_DECIMAL_NEW(txtChieuRong.Text.ToString());
                        txtThetich.EditValue = dai * rong * cao;
                    }
                    else if (cboLoaiThung.EditValue.ToString() == "TRON")
                    {
                        bankinh = ConstCommon.NVL_NUM_DECIMAL_NEW(txtBanKinh.Text.ToString());
                        s = (4.0 / 3 * 3.14).ToString();
                        txtThetich.EditValue = ConstCommon.NVL_NUM_DECIMAL_NEW(s) * bankinh * bankinh * bankinh;

                    }
                }
            }
        }

        private void txtBanKinh_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (iDataSource != null && iDataSource.Rows.Count > 0)
            {
                string s = "";
                if (cboLoaiThung.EditValue != null)
                {
                    decimal dai, rong, cao, bankinh;
                    cao = ConstCommon.NVL_NUM_DECIMAL_NEW(txtChieuCao.Text.ToString());
                    if (cboLoaiThung.EditValue.ToString() == "VUONG")
                    {
                        dai = ConstCommon.NVL_NUM_DECIMAL_NEW(txtChieuDai.Text.ToString());
                        rong = ConstCommon.NVL_NUM_DECIMAL_NEW(txtChieuRong.Text.ToString());
                        txtThetich.EditValue = dai * rong * cao;
                    }
                    else if (cboLoaiThung.EditValue.ToString() == "TRON")
                    {
                        bankinh = ConstCommon.NVL_NUM_DECIMAL_NEW(txtBanKinh.Text.ToString());
                        s = (4.0 / 3 * 3.14).ToString();
                        txtThetich.EditValue = ConstCommon.NVL_NUM_DECIMAL_NEW(s) * bankinh * bankinh * bankinh;
                    }
                }
            }
        }

        private void txtChieuDai_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (iDataSource != null && iDataSource.Rows.Count > 0)
            {
                string s = "";
                if (cboLoaiThung.EditValue != null)
                {
                    decimal dai, rong, cao, bankinh;
                    cao = ConstCommon.NVL_NUM_DECIMAL_NEW(txtChieuCao.Text.ToString());
                    if (cboLoaiThung.EditValue.ToString() == "VUONG")
                    {
                        dai = ConstCommon.NVL_NUM_DECIMAL_NEW(txtChieuDai.Text.ToString());
                        rong = ConstCommon.NVL_NUM_DECIMAL_NEW(txtChieuRong.Text.ToString());
                        txtThetich.EditValue = dai * rong * cao;
                    }
                    else if (cboLoaiThung.EditValue.ToString() == "TRON")
                    {
                        bankinh = ConstCommon.NVL_NUM_DECIMAL_NEW(txtBanKinh.Text.ToString());
                        s = (4.0 / 3 * 3.14).ToString();
                        txtThetich.EditValue = ConstCommon.NVL_NUM_DECIMAL_NEW(s) * bankinh * bankinh * bankinh;
                    }
                }
            }
        }

        private void txtChieuRong_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (iDataSource != null && iDataSource.Rows.Count > 0)
            {
                string s = "";
                string result = "";
                if (cboLoaiThung.EditValue != null)
                {
                    decimal dai, rong, cao, bankinh;
                    cao = ConstCommon.NVL_NUM_DECIMAL_NEW(txtChieuCao.Text.ToString());
                    if (cboLoaiThung.EditValue.ToString() == "VUONG")
                    {
                        dai = ConstCommon.NVL_NUM_DECIMAL_NEW(txtChieuDai.Text.ToString());
                        rong = ConstCommon.NVL_NUM_DECIMAL_NEW(txtChieuRong.Text.ToString());
                        txtThetich.EditValue = dai * rong * cao;
                    }
                    else if (cboLoaiThung.EditValue.ToString() == "TRON")
                    {
                        bankinh = ConstCommon.NVL_NUM_DECIMAL_NEW(txtBanKinh.Text.ToString());
                        s = (4.0 / 3 * 3.14).ToString();
                        s = (ConstCommon.NVL_NUM_DECIMAL_NEW(s) * bankinh * bankinh * bankinh).ToString();
                        result = s.Substring(0, s.IndexOf('.') + 2);
                        
                    }
                }
            }
        }
    }
}
