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
    
    public partial class frm_DanhMuc_SanPham_Popup : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        IDM_SanPhamBussiness sp;
        public frm_DanhMuc_SanPham_Popup()
        {
            InitializeComponent();
            sp = new SanPhamBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            LoadCbo();
        }
        public override void OnPopupShown()
        {
            try
            {
                if (this.Parameter != null && this.Parameter.Count() > 0)
                {
                    iRowSelGb = this.Parameter[0] as DataRow;
                    DispalayRequest();
                    CheckCbo_And_Ckb();
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
        }
        private void CheckCbo_And_Ckb()
        {
            if(iRowSelGb!=null && iRowSelGb.ItemArray.Count()>0)
            {
                if (iRowSelGb["TINH_TRANG"].ToString() == "True")
                {
                    iDataSource.Rows[0]["TINH_TRANG"] = 1;
                    rbtDangSuDung.IsChecked = true;
                }
                else
                {
                    iDataSource.Rows[0]["TINH_TRANG"] = 0;
                    rbtKhongSuDung.IsChecked = true;
                }

                if(iRowSelGb["IS_KSDB"].ToString()=="True")
                    iDataSource.Rows[0]["IS_KSDB"] = 1;
                else
                    iDataSource.Rows[0]["IS_KSDB"] = 0;

                if (iRowSelGb["IS_QA"].ToString() == "True")
                    iDataSource.Rows[0]["IS_QA"] = 1;
                else
                    iDataSource.Rows[0]["IS_QA"] = 0;
                try
                {
                    if (iDataSource.Rows[0]["PATH_IMAGE"] != null)
                        imgHinh.Source = new BitmapImage(new Uri(iRowSelGb["PATH_IMAGE"].ToString()));
                }
                catch { }
            }
        }
        private void LoadCbo()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = sp.GetData_For_Cbo_DKBQ(CommonDataHelper.DonViQuanLy);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboDKBQ, "TEN_DKIEN_BQUAN", "DKIEN_BQUAN_ID", dt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboDKBQ,Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["DKIEN_BQUAN_ID"] = cboDKBQ.GetKeyValue(0);
                }

                    dt = sp.GetData_For_Cbo_Item_Type();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboItemType, "TEN_ITEM_TYPE", "MA_ITEM_TYPE", dt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboItemType, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["MA_ITEM_TYPE"] = cboItemType.GetKeyValue(0);
                }
                
                dt = sp.GetData_For_Cbo_Loai_SPham(CommonDataHelper.DonViQuanLy);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboLoaiSanPham, "TEN_LOAI_SPHAM", "LOAI_SPHAM_ID", dt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboLoaiSanPham, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["LOAI_SPHAM_ID"] = cboLoaiSanPham.GetKeyValue(0);
                }

                dt = sp.GetData_For_Cbo_Nguon_Nhap(CommonDataHelper.DonViQuanLy);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboNguonNhap, "TEN_NGUON_NHAP", "NGUON_NHAP_ID", dt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboNguonNhap, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["NGUON_NHAP_ID"] = cboNguonNhap.GetKeyValue(0);
                }

                dt = sp.GetData_For_Cbo_Nhom_SPham(CommonDataHelper.DonViQuanLy);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboNhomSanPham, "TEN_NHOM_SPHAM", "NHOM_SPHAM_ID", dt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboNhomSanPham, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["NHOM_SPHAM_ID"] = cboNhomSanPham.GetKeyValue(0);
                }

                    dt = sp.GetData_For_Cbo_NSX(CommonDataHelper.DonViQuanLy);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboNSX, "TEN_NHA_SXUAT", "NHA_SXUAT_ID", dt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboNSX, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["NHA_SXUAT_ID"] = cboNSX.GetKeyValue(0);
                }

                    dt = sp.GetData_For_Cbo_QuocGia(CommonDataHelper.DonViQuanLy);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboNuocSX, "TEN_QUOCGIA", "QUOCGIA_ID", dt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboNuocSX, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["QUOCGIA_ID"] = cboNuocSX.GetKeyValue(0);
                }

                dt = sp.GetData_For_Cbo_LoaiKichThuoc(CommonDataHelper.DonViQuanLy);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboLoaiKichThuoc, "MA_SIZE", "LOAI_KTHUOC_ID", dt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboLoaiKichThuoc, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["LOAI_KTHUOC_ID"] = cboLoaiKichThuoc.GetKeyValue(0);
                }

                    dt = sp.GetData_For_Cbo_KhachHang_Ncc(CommonDataHelper.DonViQuanLy);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboNhaCC, "TEN_KH", "KHACHHANG_NCC_ID", dt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboNhaCC, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["KHACHHANG_NCC_ID"] = cboNhaCC.GetKeyValue(0);
                }

                    dt = sp.GetData_For_Cbo_DVT();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboDVT, "TEN_DONVI_TINH", "MA_DONVI_TINH", dt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboDVT, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["MA_DONVI_TINH"] = cboDVT.GetKeyValue(0);
                    ComboBoxUtil.SetComboBoxEdit(cboDVTDongGoi, "TEN_DONVI_TINH", "MA_DONVI_TINH", dt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboDVTDongGoi, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);
                    this.iDataSource.Rows[0]["MA_DONVI_TINH_THUNG"] = cboDVTDongGoi.GetKeyValue(0);
                }

                

            }
            catch
            {
                base.ShowMessage(MessageType.Error, "Có lỗi trong quá trình sử lý thông tin!");
                return;
            }
        }

        private int CheckEmpty()
        {
            if(cboItemType.EditValue.ToString()=="0")
            {
                base.ShowMessage(MessageType.Information, "Vui lòng chọn Item Type!");
                tabPhieuChi.SelectedIndex = 0;
                cboItemType.Focus();
                return 1;
            }
            if(txtMaSanPham.Text==string.Empty)
            {
                base.ShowMessage(MessageType.Information, "Vui lòng nhập mã sản phẩm!");
                tabPhieuChi.SelectedIndex = 0;
                txtMaSanPham.Focus();
                return 1;
            }
            if(txtTenSanPham.Text==string.Empty)
            {
                base.ShowMessage(MessageType.Information, "Vui lòng nhập tên sản phẩm!");
                tabPhieuChi.SelectedIndex = 0;
                txtTenSanPham.Focus();
                return 1;
            }
            if(cboDVT.EditValue.ToString()=="0")
            {
                base.ShowMessage(MessageType.Information, "Vui lòng chọn đơn vị tính!");
                tabPhieuChi.SelectedIndex = 0;
                cboDVT.Focus();
                return 1;
            }
            if(cboNhomSanPham.EditValue.ToString()=="0")
            {
                base.ShowMessage(MessageType.Information, "Vui lòng chọn nhóm sản phẩm!");
                tabPhieuChi.SelectedIndex = 0;
                cboNhomSanPham.Focus();
                return 1;
            }
            if(cboLoaiSanPham.EditValue.ToString()=="0")
            {
                base.ShowMessage(MessageType.Information, "Vui lòng chọn loại sản phẩm!");
                tabPhieuChi.SelectedIndex = 0;
                cboLoaiSanPham.Focus();
                return 1;
            }
            if(rbtDangSuDung.IsChecked==false && rbtKhongSuDung.IsChecked==false)
            {
                base.ShowMessage(MessageType.Information, "Vui lòng chọn tình trạng sử dụng!");
                tabPhieuChi.SelectedIndex = 4;
                return 1;
            }
            return 0;
        }
        private int CheckSoAm()
        {
            if(txtTonToiThieu.Text !=string.Empty && txtTonToiThieu.Text.Substring(0,1)=="-")
            {
                base.ShowMessage(MessageType.Information, "Tồn tối thiểu phải là số dương!");
                tabPhieuChi.SelectedIndex = 0;
                return 1;
            }
            if(txtTrongLuong.Text!=string.Empty && txtTrongLuong.Text.Substring(0,1)=="-")
            {
                base.ShowMessage(MessageType.Information, "Khối lượng phải là số dương!");
                tabPhieuChi.SelectedIndex = 2;
                return 1;
            }
            if(txtThuongDS.Text!=string.Empty && txtThuongDS.Text.Substring(0,1)=="-")
            {
                base.ShowMessage(MessageType.Information, "Thưởng DS phải là số dương!");
                tabPhieuChi.SelectedIndex = 1;
                return 1;
            }
            if(txtThue.Text!=string.Empty && txtThue.Text.Substring(0,1)=="-")
            {
                base.ShowMessage(MessageType.Information, "Thuế phải là số dương!");
                tabPhieuChi.SelectedIndex = 1;
                return 1;
            }
            if(txtTheTich.Text!=string.Empty && txtTheTich.Text.Substring(0,1)=="-")
            {
                base.ShowMessage(MessageType.Information, "Thể tích phải là số dương!");
                tabPhieuChi.SelectedIndex = 2;
                return 1;
            }
            if(txtSoThungPalete.Text!=string.Empty && txtSoThungPalete.Text.Substring(0,1)=="-")
            {
                base.ShowMessage(MessageType.Information, "Số thùng trên pallet phải là số dương!");
                tabPhieuChi.SelectedIndex = 2;
                return 1;
            }

            if (txtChietKhau.Text != string.Empty && txtChietKhau.Text.Substring(0, 1) == "-")
            {
                base.ShowMessage(MessageType.Information, "Chiết khấu phải là số dương!");
                tabPhieuChi.SelectedIndex = 1;
                return 1;
            }
            if (txtGiaBanLe.Text != string.Empty && txtGiaBanLe.Text.Substring(0, 1) == "-")
            {
                base.ShowMessage(MessageType.Information, "Giá bán lẻ phải là số dương!");
                tabPhieuChi.SelectedIndex = 1;
                return 1;
            }
            if (txtGiaBanBuon.Text != string.Empty && txtGiaBanBuon.Text.Substring(0, 1) == "-")
            {
                base.ShowMessage(MessageType.Information, "Giá bán buôn phải là số dương!");
                tabPhieuChi.SelectedIndex = 1;
                return 1;
            }
            if (txtQCDongGoi.Text!=string.Empty && txtQCDongGoi.Text.Substring(0,1)=="-")
            {
                base.ShowMessage(MessageType.Information, "Quy cách đòng gói phải là số dương!");
                tabPhieuChi.SelectedIndex = 2;
                return 1;
            }
            if(txtCanhBao.Text!=string.Empty && txtCanhBao.Text.Substring(0,1)=="-")
            {
                base.ShowMessage(MessageType.Information, "Số ngày cảnh báo phải là số dương!");
                tabPhieuChi.SelectedIndex = 3;
                return 1;
            }
            return 0;
        }
        private void checkTinhTrang()
        {
            if (rbtDangSuDung.IsChecked == true)
                this.iDataSource.Rows[0]["TINH_TRANG"] = 1;
            else
                this.iDataSource.Rows[0]["TINH_TRANG"] = 0;
        }

        private int CheckSpecialChar_For_Ma(string str)
        {
            if (str == "")
            {
                return 0;
            }
            char[] varChar = str.ToCharArray();
            int i = 0;
            while (i < varChar.Length &&
                ((Convert.ToInt32(varChar[i]) >= 97 && Convert.ToInt32(varChar[i]) <= 122) || (Convert.ToInt32(varChar[i]) >= 65 && Convert.ToInt32(varChar[i]) <= 90)
                || (Convert.ToInt32(varChar[i]) >= 48 && Convert.ToInt32(varChar[i]) <= 57)))
            {
                i++;
            }
            if (i < varChar.Length)
            {
                return 0;
            }
            return 1;
        }
        /// <summary>
        /// dong popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("SANPHAM_ID", typeof(decimal));
                xDicUser.Add("MA_DVIQLY", typeof(string));
                xDicUser.Add("TEN_ITEM_TYPE", typeof(string));
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDicUser.Add("MA_SANPHAM_KH", typeof(string));
                xDicUser.Add("MA_SANPHAM", typeof(string));
                xDicUser.Add("TEN_SANPHAM", typeof(string));
                xDicUser.Add("NHOM_SPHAM_ID", typeof(decimal));
                xDicUser.Add("TEN_NHOM_SPHAM", typeof(string));
                xDicUser.Add("LOAI_SPHAM_ID", typeof(decimal));
                xDicUser.Add("TEN_LOAI_SPHAM", typeof(string));
                xDicUser.Add("MA_VACH", typeof(string));
                xDicUser.Add("IS_KSDB", typeof(bool));
                xDicUser.Add("IS_QA", typeof(bool));
                xDicUser.Add("HOATCHAT_CHINH", typeof(string));
                xDicUser.Add("MA_DONVI_TINH", typeof(string));
                xDicUser.Add("TEN_DONVI_TINH", typeof(string));
                xDicUser.Add("TON_TOITHIEU", typeof(int));
                xDicUser.Add("SONGAY_CANHBAOTRUOC", typeof(int));
                xDicUser.Add("NGUON_NHAP_ID", typeof(decimal));
                xDicUser.Add("TEN_NGUON_NHAP", typeof(string));
                xDicUser.Add("DKIEN_BQUAN_ID", typeof(decimal));
                xDicUser.Add("TEN_DKIEN_BQUAN", typeof(string));
                xDicUser.Add("GIABAN_LE", typeof(decimal));
                xDicUser.Add("GIABAN_BUON", typeof(decimal));
                xDicUser.Add("VITRI_DEXUAT", typeof(string));
                xDicUser.Add("NHA_SXUAT_ID", typeof(decimal));
                xDicUser.Add("TEN_NHA_SXUAT", typeof(string));
                xDicUser.Add("QUOCGIA_ID", typeof(decimal));
                xDicUser.Add("TEN_QUOCGIA", typeof(string));
                xDicUser.Add("KHACHHANG_NCC_ID", typeof(decimal));
                xDicUser.Add("TEN_KH", typeof(string));
                xDicUser.Add("THUE_VAT", typeof(decimal));
                xDicUser.Add("CHIET_KHAU", typeof(decimal));
                xDicUser.Add("THUONG_DS", typeof(decimal));
                xDicUser.Add("PATH_IMAGE", typeof(string));
                xDicUser.Add("SOLUONG_THUNG", typeof(int));
                xDicUser.Add("MA_DONVI_TINH_THUNG", typeof(string));
                xDicUser.Add("TEN_DONVI_TINH_THUNG", typeof(string));
                xDicUser.Add("TRONG_LUONG_KG", typeof(decimal));
                xDicUser.Add("LOAI_KTHUOC_ID", typeof(decimal));
                xDicUser.Add("MA_SIZE", typeof(string));
                xDicUser.Add("THE_TICH_M3", typeof(decimal));
                xDicUser.Add("SOLUONG_THUNG_PALLET", typeof(int));
                xDicUser.Add("SO_DANGKY", typeof(string));
                xDicUser.Add("NGAYKY_DANGKY", typeof(string));
                xDicUser.Add("NGAYHH_DANGKY", typeof(string));
                xDicUser.Add("SO_XACNHAN", typeof(string));
                xDicUser.Add("NGAYKY_XACNHAN", typeof(string));
                xDicUser.Add("NGAYHH_XACNHAN", typeof(string));
                xDicUser.Add("GHI_CHU", typeof(string));
                xDicUser.Add("TINH_TRANG", typeof(bool));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }
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
                if (iRowSelGb==null)
                {
                    base.ShowMessage(MessageType.Information, "Chưa có sản phẩm nào được chọn để khai báo GPNK!");
                    return;
                }
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_DanhMucSanPham_KhaiBaoGPNK", iRowSelGb);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnGPNK.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void btnQCSP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (iRowSelGb == null)
                {
                    base.ShowMessage(MessageType.Information, "Chưa có sản phẩm nào được chọn!");
                    return;
                }
                object xReturn = UIAgent.GetUIPopupDialog(this, null, this.GetType().ToString(), "DSofT.Warehouse.UI", "frm_DanhMucSanPham_Popup1", iRowSelGb);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnQCSP.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }
        private void setCkb()
        {
            if (ckbKSDB.IsChecked == true)
                this.iDataSource.Rows[0]["IS_KSDB"] = 1;
            else
                this.iDataSource.Rows[0]["IS_KSDB"] = 0;
            if (ckbQA.IsChecked == true)
                this.iDataSource.Rows[0]["IS_QA"] = 1;
            else
                this.iDataSource.Rows[0]["IS_QA"] = 0;
        }

        
        int CheckDateMonth(string date1, string date2)
        {
            string s1 = date1.Substring(0, date1.IndexOf(" " + 1));
            string s2 = date2.Substring(0, date2.IndexOf(" " + 1));
            string[] arr1 = s1.Split('/');
            string[] arr2 = s2.Split('/');
            if (int.Parse(arr2[2]) < int.Parse(arr1[2]))
            {
                return 0;
            }
            else if (int.Parse(arr2[1]) < int.Parse(arr1[1]))
            {
                return 0;
            }
            else if ((int.Parse(arr2[0]) < int.Parse(arr1[0])))
            {
                return 0;
            }
            return 1;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            try
            {
                if (CheckEmpty() == 1)
                    return;
                if (CheckSoAm() == 1)
                    return;
                if (dtpNgayDK.EditValue != null && dtpNgayHHDK.EditValue != null)
                {
                    if (CheckDateMonth(dtpNgayDK.EditValue.ToString(), dtpNgayHHDK.EditValue.ToString()) == 0)
                    {
                        base.ShowMessage(MessageType.Information, "Ngày hết hạn đăng ký phải sau ngày đăng ký!");
                        tabPhieuChi.SelectedIndex = 3;
                        return;
                    }
                }
                if (dtpNgayDKXN.EditValue != null && dtpNgayHHXN.EditValue != null)
                {
                    if (CheckDateMonth(dtpNgayDKXN.EditValue.ToString(), dtpNgayHHXN.EditValue.ToString()) == 0)
                    {
                        base.ShowMessage(MessageType.Information, "Ngày hết hạn xác nhận phải sau ngày xác nhận!");
                        return;
                    }
                }
                //-------------------------
                checkTinhTrang();
                setCkb();
                iDataSource.Rows[0]["PATH_IMAGE"] = txtLinkHinh.Text;
                if (CheckSpecialChar_For_Ma(txtMaSanPham.Text)==0)
                {
                    base.ShowMessage(MessageType.Information, "Mã sản phẩm chỉ gồm chữ, số và không chứa khoảng trống!");
                    txtMaSanPham.Focus();
                    return;
                }
                if(this.iRowSelGb!=null)
                {
                    this.iDataSource.Rows[0]["SANPHAM_ID"] = iRowSelGb["SANPHAM_ID"];
                    int exist_Ma = sp.Check_Exist_Ma_SPham_Update(CommonDataHelper.DonViQuanLy,this.iDataSource.Rows[0]["MA_SANPHAM"].ToString(), int.Parse(this.iRowSelGb["SANPHAM_ID"].ToString()));
                    if(exist_Ma>0)
                    {
                        base.ShowMessage(MessageType.Information, "Mã sản phẩm [" + txtMaSanPham.Text + "] đã tồn tại!");
                        txtMaSanPham.Focus();
                        return;
                    }
                    else
                    {
                        result = sp.InsertOrUpdateSanPham(CommonDataHelper.DonViQuanLy,this.iDataSource.Copy(),CommonDataHelper.UserName);
                        if (result>0)
                        {
                            ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                            iRowSelGb = iDataSource.Rows[0];
                            return;
                        }
                        else
                        {
                            base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                            return;
                        }
                    }
                }
                else
                {
                    int exist_Ma = sp.Check_Exist_Ma_SPham(CommonDataHelper.DonViQuanLy,this.iDataSource.Rows[0]["MA_SANPHAM"].ToString());
                    if(exist_Ma>0)
                    {
                        base.ShowMessage(MessageType.Information, "Mã sản phẩm [" + txtMaSanPham.Text + "] đã tồn tại!");
                        txtMaSanPham.Focus();
                        return;
                    }
                    else
                    {
                        result = sp.InsertOrUpdateSanPham(CommonDataHelper.DonViQuanLy,this.iDataSource.Copy(),CommonDataHelper.UserName);
                        if (result > 0)
                        {
                            ToastMessage.ShowToastMessage(Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessage"]), Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageSuccess"]), notificationService);
                            //imgHinh.Source = null;
                            DataTable dt = sp.GetMax_Id_SanPham(CommonDataHelper.DonViQuanLy);
                            if(dt.Rows.Count>0 && dt!=null)
                            {
                                iRowSelGb = dt.Rows[0];
                            }
                            return;
                        }
                        else
                        {
                            base.ShowMessage(MessageType.Information, Convert.ToString(UtilLanguage.ApplyLanguage()["lblMessageFailed"]));
                            return;
                        }
                    }
                }
            }catch(Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, this.btnSave.Tag.ToString());
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnThemMoi_Click(object sender, RoutedEventArgs e)
        {
            this.iDataSource.Clear();
            rbtDangSuDung.IsChecked = false;
            rbtKhongSuDung.IsChecked = false;
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            this.iRowSelGb = null;
            imgHinh.Source = null;
            cboDKBQ.SelectedIndex = 0;
            cboDVT.SelectedIndex = 0;
            cboDVTDongGoi.SelectedIndex = 0;
            cboItemType.SelectedIndex = 0;
            cboLoaiKichThuoc.SelectedIndex = 0;
            cboLoaiSanPham.SelectedIndex = 0;
            cboNguonNhap.SelectedIndex = 0;
            cboNhaCC.SelectedIndex = 0;
            cboNhomSanPham.SelectedIndex = 0;
            cboNSX.SelectedIndex = 0;
            cboNuocSX.SelectedIndex = 0;
            
        }

        private void btnAnh_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "JPG files (*.jpg)|*.jpg|All file (*.*)|*.*";
            open.FilterIndex = 1;
            open.RestoreDirectory = true;
            try
            {
                if (open.ShowDialog() == DialogResult.OK)
                {
                    txtLinkHinh.Text = open.FileName;
                    imgHinh.Source = new BitmapImage(new Uri(txtLinkHinh.Text));
                }
            }
            catch
            {
                base.ShowMessage(MessageType.Information, "File được chọn phải là file ảnh!");
                txtLinkHinh.Clear();
                return;
            }
        }
        
    }
}