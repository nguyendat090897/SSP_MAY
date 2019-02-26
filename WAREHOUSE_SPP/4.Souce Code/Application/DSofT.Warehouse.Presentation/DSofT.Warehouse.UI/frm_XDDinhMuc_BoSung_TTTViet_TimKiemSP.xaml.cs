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
using DevExpress.Xpf.Grid;
using Microsoft.Win32;

namespace DSofT.Warehouse.UI
{
    /// <summary>
    ///  Author:	 Ngo Gia Thien
    ///  Create date: 22/03/2018
    ///  Editor:      
    ///  Modify date: 
    ///  Description: Xay dung dinh muc bo sung thong tin tieng viet tim kiem san pham
    /// </summary>
    public partial class frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP : PopupBase
    {
        DataTable iDataSource = null;
        DataRow iRowSelGb = null;
        GridHelper iGridHelper = null;
        GridHelper iGridHelper_chon = null;
        IclsCommonBussiness clsCommon;
        IYEUCAU_NHAPXUATKHOBussiness Yeucauxuatkho_bus;
        DataTable dtSPALL = new DataTable();
        DataTable dtSPCHON = new DataTable();
        DataTable dtSPCHON_Return = null;
        string mbChon = "N";
        RegistryKey regKey = null;
        public frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP()
        {
            InitializeComponent();
            Yeucauxuatkho_bus = new YEUCAU_NHAPXUATKHOBussiness(string.Empty);
            clsCommon = new clsCommonBussiness(string.Empty);
            this.iDataSource = this.TableSchemaDefineBingding();
            this.DataContext = this.iDataSource;
            BuildCombobox();
            Initialize_Grid();
            Initialize_Grid_chon();

        }

        private DataTable TableSchemaDefineBingding()
        {
            DataTable xDt = null;
            try
            {
                Dictionary<string, Type> xDicUser = new Dictionary<string, Type>();
                xDicUser.Add("MA_ITEM_TYPE", typeof(string));
                xDt = TableUtil.ConvertDictionaryToTable(xDicUser, true, false);

            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "TableSchemaDefineBingding()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
            return xDt;
        }


        public override void OnPopupShown()
        {
            try
            {


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

        private void BuildCombobox()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = clsCommon.GetList_ITEM_TYPE();
                if (dt != null)
                {
                    ComboBoxUtil.SetComboBoxEdit(cboItemType, "TEN_ITEM_TYPE", "MA_ITEM_TYPE", dt, SelectionTypeEnum.Native);
                    ComboBoxUtil.InsertItem(cboItemType, Convert.ToString(UtilLanguage.ApplyLanguage()["lblChonAll"]), "0", 0);



                    regKey = Registry.CurrentUser;
                    regKey = regKey.CreateSubKey("Software\\SSP");
                    string valueDefault = regKey.GetValue("MA_ITEM_TYPE") == null ? "0" : regKey.GetValue("MA_ITEM_TYPE").ToString();
                    if (valueDefault != "0")
                    {
                        this.iDataSource.Rows[0]["MA_ITEM_TYPE"] = regKey.GetValue("MA_ITEM_TYPE").ToString();
                    }
                    else
                    {
                        this.iDataSource.Rows[0]["MA_ITEM_TYPE"] = cboItemType.GetKeyValue(0);
                    }



                }

            }
            catch (Exception ex)
            {

                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "BuildCombobox()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);

            }

        }

        private void Initialize_Grid()
        {
            try
            {
                this.iGridHelper = new GridHelper(this.grdSPALL, this.grdViewSPALL, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("TEN_LOAI_SPHAM", "Loại sản phẩm");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_NHOM_SPHAM", "Nhóm sản phẩm");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_MaSanPham"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_MaSanPhamKhachHang"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_TenSPham"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_DONVI_TINH", "ĐVT");
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("HOATCHAT_CHINH", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_ThanhPhan"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("QUYCACHDONGGOI", "Quy cách đóng gói");
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);

                xColumn = new Column("TEN_NGUON_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["blb_NguonNhap"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);



                xColumn = new Column("TEN_DKIEN_BQUAN", "Điều kiện bảo quản");
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);



                xColumn = new Column("TEN_NHA_SXUAT", "Nhà sản xuất");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);



                xColumn = new Column("TEN_QUOCGIA", "Nước sản xuất");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);



                xColumn = new Column("TEN_KH", "Nhà cung cấp");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("IS_KSDB", "Thuộc KSĐB");
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);


                xColumn = new Column("SOLUONG_QUYDOI", "Qui đổi");
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper.Add(xColumn);




                this.iGridHelper.Initialize();
                //this.grdViewDMSP.AutoWidth = false;
                //hoapd set filter
                setAutoFilterConditionGrid(this.grdSPALL);
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }




        private void Initialize_Grid_chon()
        {
            try
            {
                this.iGridHelper_chon = new GridHelper(this.grdSPCHON, this.grdViewSPCHON, false);
                Column xColumn;

                xColumn = new Column("ROWNUM", Convert.ToString(UtilLanguage.ApplyLanguage()["lblOrderNo"]));
                xColumn.Width = 50;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);


                xColumn = new Column("TEN_LOAI_SPHAM", "Loại sản phẩm");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);

                xColumn = new Column("TEN_NHOM_SPHAM", "Nhóm sản phẩm");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);


                xColumn = new Column("MA_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_MaSanPham"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);

                xColumn = new Column("MA_SANPHAM_KH", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_MaSanPhamKhachHang"]));
                xColumn.Width = 120;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);

                xColumn = new Column("TEN_SANPHAM", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_TenSPham"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);

                xColumn = new Column("TEN_DONVI_TINH", "ĐVT");
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Center;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);


                xColumn = new Column("HOATCHAT_CHINH", Convert.ToString(UtilLanguage.ApplyLanguage()["lbl_ThanhPhan"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);


                xColumn = new Column("QUYCACHDONGGOI", "Quy cách đóng gói");
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);

                xColumn = new Column("TEN_NGUON_NHAP", Convert.ToString(UtilLanguage.ApplyLanguage()["blb_NguonNhap"]));
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);



                xColumn = new Column("TEN_DKIEN_BQUAN", "Điều kiện bảo quản");
                xColumn.Width = 150;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);



                xColumn = new Column("TEN_NHA_SXUAT", "Nhà sản xuất");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);



                xColumn = new Column("TEN_QUOCGIA", "Nước sản xuất");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);



                xColumn = new Column("TEN_KH", "Nhà cung cấp");
                xColumn.Width = 100;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);


                xColumn = new Column("IS_KSDB", "Thuộc KSĐB");
                xColumn.Width = 200;
                xColumn.Visible = true;
                xColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment.Left;
                xColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                this.iGridHelper_chon.Add(xColumn);
                this.iGridHelper_chon.Initialize();
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void setAutoFilterConditionGrid(GridControl grd)
        {
            try
            {
                for (int i = 0; i < grd.Columns.Count; i++)
                {
                    grd.Columns[i].ColumnFilterMode = ColumnFilterMode.DisplayText;
                    grd.Columns[i].AutoFilterCondition = AutoFilterCondition.Contains;

                }

            }
            catch
            {

            }

        }

        private void cboItemType_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                dtSPALL = Yeucauxuatkho_bus.DM_SANPHAM_GET_LIST_ALL_BY_TYPE(ConstCommon.DonViQuanLy, this.iDataSource.Rows[0]["MA_ITEM_TYPE"].ToString());
                if (dtSPALL != null)
                {
                    this.iGridHelper.BindItemSource(dtSPALL);
                }
                else
                {
                    grdSPALL.ItemsSource = null;
                }


                regKey = Registry.CurrentUser;
                regKey = regKey.CreateSubKey("Software\\SSP");
                regKey.SetValue("MA_ITEM_TYPE", this.iDataSource.Rows[0]["MA_ITEM_TYPE"].ToString());


            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }


        }


        private bool ContainDataRowInDataTable(DataTable dt, DataRow dr)
        {
            try
            {
                var k = (from r in dt.Rows.OfType<DataRow>() where r["SANPHAM_ID"].ToString() == dr["SANPHAM_ID"].ToString() select r).FirstOrDefault();
                if (k != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }


        }



        private void grdSPALL_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if ((dtSPCHON == null) || (dtSPCHON.Rows.Count == 0))
                {
                    dtSPCHON = dtSPALL.Clone();
                }


                if (this.grdSPALL.GetFocusedRow() == null) return;
                DataRowView row = this.grdSPALL.GetFocusedRow() as DataRowView;

                if (ContainDataRowInDataTable(dtSPCHON, row.Row) == false)
                {
                    dtSPCHON.ImportRow(row.Row);
                }
                this.iGridHelper_chon.BindItemSource(dtSPCHON);
                dtSPCHON_Return = dtSPCHON.Copy();



            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }


        public override object UIReturn(string source)
        {
            try
            {

                if ((dtSPCHON_Return != null) && (dtSPCHON_Return.Rows.Count > 0))
                {
                    dtSPCHON_Return.Rows[0]["GHI_CHU"] = mbChon;

                }

                return dtSPCHON_Return;
            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
                return null;
            }

        }
        private void grdSPCHON_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if ((dtSPCHON == null) || (dtSPCHON.Rows.Count == 0))
                {
                    return;
                }


                if (this.grdSPCHON.GetFocusedRow() == null) return;
                DataRowView row = this.grdSPCHON.GetFocusedRow() as DataRowView;

                dtSPCHON.Rows.Remove(row.Row);


                this.iGridHelper_chon.BindItemSource(dtSPCHON);
                dtSPCHON_Return = dtSPCHON.Copy();




            }
            catch (Exception ex)
            {
                UtilLog.WriteLogToDB(LogGroupName.PROGRAM, ex.Message, this.GetType().Name, "Initialize_Grid()");
                base.ShowMessage(MessageType.Error, ex.Message, ex);
            }
        }

        private void btnChon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mbChon = "Y";
            }
            catch
            {

                mbChon = "N";
            }
            finally
            {
                this.Close();
            }

        }
    }

}
