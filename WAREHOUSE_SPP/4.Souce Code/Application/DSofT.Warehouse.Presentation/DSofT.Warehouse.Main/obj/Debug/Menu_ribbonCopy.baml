   M S B A M L   `   `   `  ����Q  MDSofT.Framework.UICore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICoreDSofT.Framework.UICore  5      O KDSofT.Warehouse.Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DSofT.Warehouse.Main.MenuDSofT.Warehouse.Main.Menu 5      /   )DSofT.Warehouse.Main.Menu.Menu_ribbonCopy   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   a ]DevExpress.Xpf.Core.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a<dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5   	   <dxc2http://schemas.devexpress.com/winfx/2008/xaml/core  5   	   QuicoreDclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICore  5      3local'clr-namespace:DSofT.Warehouse.Main.Menu  5      c _DevExpress.Xpf.Ribbon.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon  5	   	   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5
   	   %   DSofT.Framework.UICore.ViewBase    Title$  
MainWindow��5   	   $
��auto��6   $
��auto��6*   .��5   
   �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   )  #DevExpress.Xpf.Ribbon.RibbonControl  -   5          Name$ RibbonControl��5      $��0��63      ApplicationButtonText$
 File��5          PageCategoryAlignment$ Right=�5          RibbonStyle$ 
Office2010=�5          ApplicationMenu 5      +  %DevExpress.Xpf.Ribbon.ApplicationMenu  5         RightPaneWidth$	 280Q�6&      ShowRightPane . #  DevExpress.Xpf.Bars.PopupMenu	   Items	 5      '  !DevExpress.Xpf.Bars.BarButtonItem  5!      -   5        
 �Name$
 frmUser��5!   ,   !  DevExpress.Xpf.Bars.BarItem   Content)  #DevExpress.Xpf.Core.ObjectConverter$ Người dùng 6;      Glyph$? 9pack://application:,,,/Images/Menu/clt_ico_administer.png��6o     5"      -   5        $
 frm_UserRole��5"   ,   $ Vai trò 6@   $8 2pack://application:,,,/Images/Menu/icons8-shop.png��6w     5#      -   5        $
 frm_UserType��5#   ,   $ Loại người dùng 6@   $: 4pack://application:,,,/Images/Menu/icons8-school.png��6�     5$      -   5        $
 
frm_ThamSo��5$   ,   $ Tham số hệ thống 6>   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6}     5%      -   5        $
 frmPhanCongThietBi��5%   ,   $ Phân công thiết bị 6F   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6�     5(      -   5        $
 frmThietLapTonKhoTT��5(   ,   $+ %Thiết lập tồn kho tối thiểu 6G   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6�     5)      -   5        $"
 frmThietLapQuyTacHangBanCham��5)   ,   $/ )Thiết lập quy tắc hàng bán chậm 6P   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6�     5*      -	   5        $ 
 frmThietLapCanhBaoHSDThuoc��5*   ,   $9 3Thiết lập cảnh báo hạn sử dụng thuốc 6N   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6�     5+      -
   5        $
 frmKhoiTaoTonkho��5+   ,   $ Khởi tạo tồn kho 6D   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6�     5,      -   5        $"
 frm_quytac_makytudau_sanpham��5,   ,   $D >Thiết lập quy tắc mã ký tự đầu của sản phẩm 6P   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6�     5-      -   5        $"
 frm_XDDinhMuc_BoSung_TTTViet��5-   ,   $C =Xây dựng định mức bổ sung thông tin tiếng việt 6P   $: 4pack://application:,,,/Images/Menu/icons8-school.png��6�     5.      -   5        $#
 frm_XDDinhMuc_ChoSP_KhuyenMai��5.   ,   $> 8Xây dựng định mức cho sản phẩm khuyến mãi 6Q   $8 2pack://application:,,,/Images/Menu/icons8-shop.png��6�     51      -   5        $
 frmLapPhieuYeuCauNhapKho��51   ,   $# Phiếu yêu cầu nhập kho 6L   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6�     52      -   5        $
 
frmNhapKho��52   ,   $ 
Nhập kho 6>   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6v     53      -   5        $
 frmDieuChuyenNoiBo��53   ,   $! Điều chuyển nội bộ 6F   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6�     54      -   5        $
 frmDCNBTaiKhoNhan��54   ,   $3 -Danh sách phiếu ĐC chuyển chờ duyệt 6E   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6�     55      -   5        $!
 frm_lapphieu_yeucau_xuatkho��55   ,   $# Phiếu yêu cầu xuất kho 6O   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6�     56      -   5        $
 frm_XuatKho��56   ,   $ 
Xuất kho 6?   $< 6pack://application:,,,/Images/Menu/clt_ico_default.png��6x     5:      -   5        $
 	frmDSPLDV��5:   ,   $* $ Danh sách phiếu làm dịch vụ 6=   $6 0pack://application:,,,/Images/Menu/icon_list.png��6�     5;      -   5        $
 frmDSSPDongGoi��5;   ,   $) #Danh sách sản phẩm đóng gói 6B   $6 0pack://application:,,,/Images/Menu/icon_list.png��6�     5<      -   5        $
 frmLapYeuCauDichVu��5<   ,   $$ Phiếu yêu cầu dịch vụ 6F   $= 7pack://application:,,,/Images/Menu/icon_phieudichvu.png��6�     5=      -   5        $
 frmPhieuNhapVatTuDichVu��5=   ,   $* $Phiếu nhập vật tư dịch vụ 6K   $; 5pack://application:,,,/Images/Menu/icon_phieunhap.png��6�     5>      -   5        $
 frmPhieuNhapSPDV��5>   ,   $- 'Phiếu nhập sản phẩm dịch vụ 6D   $; 5pack://application:,,,/Images/Menu/icon_phieunhap.png��6�     5?      -   5        $
 frm_NhapLaiVatTuDaXuatRa��5?   ,   $B <Nhập lại vật tư đã xuất ra làm hàng dịch vụ 6L   $9 3pack://application:,,,/Images/Menu/icon_nhaplai.png��6�     5@      -   5        $*
 $frm_lap_phieu_xuat_kho_vattu_dich_vu��5@   ,   $. (Phiếu xuất kho vật tư dịch vụ 6�   $; 5pack://application:,,,/Images/Menu/icon_phieuxuat.png��6�     5C      -   5        $
 frmDSBTDPXuat��5C   ,   $. (Quản lý cho phép xuất biệt trữ 6A   $8 2pack://application:,,,/Images/Menu/icon_manage.png��6�     5D      -   5        $"
 frmLapPhieuXacNhanHangHuHong��5D   ,   $0 *Lập phiếu xác nhận hàng hư hỏng 6P   $> 8pack://application:,,,/Images/Menu/icon_phieuxacnhan.png��6�     5E      -   5        $
 frmCanhBaoHangBanCham��5E   ,   $# Cảnh báo hàng bán chậm 6I   $9 3pack://application:,,,/Images/Menu/icon_warring.png��6�     5F      -   5        $
 frmCanhBaoHSDThuoc��5F   ,   $5 /Cảnh báo hàng sắp hết hạn sử dụng 6F   $9 3pack://application:,,,/Images/Menu/icon_warring.png��6�     5G      -   5        $
 frmLapPhieuXuatTuPYC��5G   ,   $# Lập phiếu xuất từ PYC 6H   $> 8pack://application:,,,/Images/Menu/icon_phieuxacnhan.png��6�     5H      -    5        $
 frm_dm_pallet_dieu_chuyen��5H   ,   $ Điều chuyển pallet 6M   $> 8pack://application:,,,/Images/Menu/icon_phieuxacnhan.png��6�     5I      -!   5        $
 frmKhachHangHopDong��5I   ,   $< 6Quản lý hợp đồng khách hàng, nhà cung cấp 6G   $> 8pack://application:,,,/Images/Menu/icon_phieuxacnhan.png��6�     5J      -"   5        $
 frmKiemKeHangHoa��5J   ,   $ Kiểm kê hàng hóa 6D   $> 8pack://application:,,,/Images/Menu/icon_phieuxacnhan.png��6�     5K      -#   5        $
 frmQuanLyThu��5K   ,   $ Quản lý thu 6@   $> 8pack://application:,,,/Images/Menu/icon_phieuxacnhan.png��6|     5L      -$   5        $
 frmQuanLyChi��5L   ,   $ Quản lý chi 6@   $> 8pack://application:,,,/Images/Menu/icon_phieuxacnhan.png��6|     5M      -%   5        $'
 !frm_dm_pallet_cap_nhat_trang_thai��5M   ,   $' !Cập nhật trạng thái pallet 6U   $> 8pack://application:,,,/Images/Menu/icon_phieuxacnhan.png��6�     5N      -&   5        $
 frm_PhieuXuat_DieuChinh��5N   ,   $$ Điều chỉnh phiếu xuất 6K   $> 8pack://application:,,,/Images/Menu/icon_phieuxacnhan.png��6�     5P      -'   5        $
 
frmDMKHNCC��5P   ,   $- 'Danh mục khách hàng nhà cung cấp 6>   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6�     5Q      -(   5        $
 frmDMKho��5Q   ,   $ Danh mục kho 6<   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6r     5R      -)   5        $
 
frmThietBi��5R   ,   $ Danh mục thiết bị 6>   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6{     5S      -*   5        $
 frmDVT��5S   ,   $! Danh mục đơn vị tính 6:   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6v     5T      -+   5        $
 	frmPallet��5T   ,   $ Danh mục pallet 6=   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6w     5U      -,   5        $
 
frmDMTTBYT��5U   ,   $* $Danh mục trang thiết bị y tế 6>   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6�     5V      --   5        $
 
frmQuocGia��5V   ,   $ Danh mục quốc gia 6>   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6{     5W      -.   5        $
 frmNhaSX��5W   ,   $" Danh mục nhà sản xuất 6<   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6{     5X      -/   5        $
 frmDanhMucSanPham��5X   ,   $ Danh mục sản phẩm 6E   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6�     5Y      -0   5        $
 frmDanhSachLoaiPallet��5Y   ,   $ Danh mục loại pallet 6I   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6�     5Z      -1   5        $
 frm_dm_LoaiSanPham��5Z   ,   $ Loại sản phẩm 6F   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6�     5[      -2   5        $
 frm_dm_LoaiKichThuoc��5[   ,   $&  Danh mục loại kích thước 6H   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6�     5\      -3   5        $
 frm_DanhMucLoaiKho��5\   ,   $ Danh mục loại kho 6F   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6�     5]      -4   5        $
 frm_dm_nhom_pallet��5]   ,   $ Danh mục nhóm pallet 6F   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6�     5^      -5   5        $
 frmDanhMucNhomSanPham��5^   ,   $# Danh mục nhóm sản phẩm 6I   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6�     5_      -6   5        $
 frm_dm_Tinh��5_   ,   $ Danh mục tỉnh 6?   $? 9pack://application:,,,/Images/Menu/icons8-report-card.png��6y   5d         
Categories. 5g      5  /DevExpress.Xpf.Ribbon.RibbonDefaultPageCategory  2	  ,DevExpress.Xpf.Ribbon.RibbonPageCategoryBase 	  Pages. 5h      &
   DevExpress.Xpf.Ribbon.RibbonPage
   
  Caption$ Hệ thống 6!    
  Groups. 5i      +  %DevExpress.Xpf.Ribbon.RibbonPageGroup  -7   5        $
 pHethong��5i   *      Caption$ Hệ thống��6:      Items. 5j      %  DevExpress.Xpf.Bars.BarItemLink     BarItemName$ frmUser��6*      RibbonStyle$ Large=�6B     5k      $ frm_UserRole��6*   $ SmallWithText=�6E     5l      $ frm_UserType��6*   $ SmallWithText=�6E     5m      $ 
frm_ThamSo��6*   $ SmallWithText=�6C   .  (DevExpress.Xpf.Bars.BarItemLinkSeparator  5n        5o      $ frmPhanCongThietBi��6*   $ Large=�6K   5q      5r      
  5t      $ 	Khai báo 6!   . 5u        -8   5        $
 	pKhaibao1��5u   *   $ Khai báo tồn��6;   . 5v        $ frmThietLapTonKhoTT��6*   $ Large=�6L     5w      $" frmThietLapQuyTacHangBanCham��6*   $ Large=�6U     5x      $  frmThietLapCanhBaoHSDThuoc��6*   $ Large=�6S     5y      $ frmKhoiTaoTonkho��6*   $ Large=�6I   5|        5}      -9   5        $
 	pKhaibao2��5}   *   $ Khai báo định mức��6;   . 5~        $" frm_quytac_makytudau_sanpham��6*   $ Large=�6U   ,  &DevExpress.Xpf.Bars.BarSplitButtonItem  5�      -:   5        $
 sbSave��5�   1   $ Khai báo định mức 6?   $8 2pack://application:,,,/Images/Menu/icons8-shop.png��5�   %      
LargeGlyph$8 2pack://application:,,,/Images/Menu/icons8-shop.png��5�   %      RibbonStyle$ Large=�5�   %      PopupControl 5�        5�   "   .	 5�   &     $" frm_XDDinhMuc_BoSung_TTTViet��66   $ SmallWithText=�6a     5�   &   $# frm_XDDinhMuc_ChoSP_KhuyenMai��66   $ SmallWithText=�6b   5�   #   5�      5�      5�      
  5�      $ Quản lý kho 6!   . 5�        -;   5        $
 
pQuanlykho��5�   *   $
 File��6<   . 5�        $ frmLapPhieuYeuCauNhapKho��6*   $ Large=�6Q     5�      $ 
frmNhapKho��6*   $ Large=�6C     5�      $ frmDieuChuyenNoiBo��6*   $ Large=�6K     5�      $ frmDCNBTaiKhoNhan��6*   $ Large=�6J     5�      $! frm_lapphieu_yeucau_xuatkho��6*   $ Large=�6T     5�      $ frm_XuatKho��6*   $ Large=�6D   5�      5�      
  5�      $ Dịch vụ 6!   . 5�        -<   5        $
 pDichvu��5�   *   $
 File��69   . 5�        $ 	frmDSPLDV��6*   $ Large=�6B     5�      $ frmDSSPDongGoi��6*   $ Large=�6G     5�      $ frmLapYeuCauDichVu��6*   $ Large=�6K     5�      $ frmPhieuNhapVatTuDichVu��6*   $ Large=�6P     5�      $ frmPhieuNhapSPDV��6*   $ Large=�6I     5�      $ frm_NhapLaiVatTuDaXuatRa��6*   $ Large=�6Q     5�      $* $frm_lap_phieu_xuat_kho_vattu_dich_vu��6*   $ Large=�6]     5�      $ bOpen��6*   $ SmallWithText=�6>     5�      $ bClose��6*   $ SmallWithText=�6?     5�      $ bPrint��6*   $ SmallWithText=�6?     5�        5�      $ bSave��6*   $ Large=�6>   5�      5�      
  5�      $ Nghiệp vụ khác 6!   . 5�        -=   5        $
 pNghiepvukhac��5�   *   $
 File��6?   . 5�        $ frmDSBTDPXuat��6*   $ Large=�6F     5�      $" frmLapPhieuXacNhanHangHuHong��6*   $ Large=�6U     5�      $ frmCanhBaoHangBanCham��6*   $ Large=�6N     5�      $ frmCanhBaoHSDThuoc��6*   $ Large=�6K     5�      $ frmLapPhieuXuatTuPYC��6*   $ Large=�6M     5�      $ frm_dm_pallet_dieu_chuyen��6*   $ Large=�6R     5�      $ frmKhachHangHopDong��6*   $ Large=�6L     5�      $ frmKiemKeHangHoa��6*   $ Large=�6I     5�      $ frmQuanLyThu��6*   $ Large=�6E     5�      $ frmQuanLyChi��6*   $ Large=�6E     5�      $' !frm_dm_pallet_cap_nhat_trang_thai��6*   $ Large=�6Z     5�      $ frm_PhieuXuat_DieuChinh��6*   $ Large=�6P     5�      $ bOpen��6*   $ SmallWithText=�6>     5�      $ bClose��6*   $ SmallWithText=�6?     5�      $ bPrint��6*   $ SmallWithText=�6?     5�        5�      $ bSave��6*   $ Large=�6>   5�      5�      
  5�      $ 
Danh mục 6!   . 5�        ->   5        $
 pDanhmuc��5�   *   $ 
Danh mục��6:   . 5�        $ bOpen��6*   $ SmallWithText=�6>     5�      $ bClose��6*   $ SmallWithText=�6?     5�      $ bPrint��6*   $ SmallWithText=�6?     5�      $ 
frmDMKHNCC��6*   $ Large=�6C     5�      $ frmDMKho��6*   $ Large=�6A     5�      $ 
frmThietBi��6*   $ Large=�6C     5�      $ frmDVT��6*   $ Large=�6?     5�      $ 	frmPallet��6*   $ Large=�6B     5�      $ 
frmDMTTBYT��6*   $ Large=�6C     5�      $ 
frmQuocGia��6*   $ Large=�6C     5�      $ frmNhaSX��6*   $ Large=�6A     5�      $ frmDanhMucSanPham��6*   $ Large=�6J     5�      $ frmDanhSachLoaiPallet��6*   $ Large=�6N     5�      $ frm_dm_LoaiSanPham ��6*   $ Large=�6L     5�      $ frm_dm_LoaiKichThuoc��6*   $ Large=�6M     5�      $ frm_DanhMucLoaiKho��6*   $ Large=�6K     5�      $ frm_dm_nhom_pallet��6*   $ Large=�6K     5�      $ frmDanhMucNhomSanPham��6*   $ Large=�6N     5�      $ frm_dm_Tinh��6*   $ Large=�6D     5�        5�      $ bSave��6*   $ Large=�6>   5�      5�      
  5�      $ Báo cáo 6!   5�      5�      .  (DevExpress.Xpf.Ribbon.RibbonPageCategory  5�      -?   5          Name$ categorySelection��5�   %    	  Caption$ 	Selection��5�   )    	  Color$ Yellow��5�   )    	  	IsVisible .  . 5�      
  $ Gallery Page 6!   . 5�        $
 Font��6*      ShowCaptionButton .  . 5�      0  *DevExpress.Xpf.Ribbon.RibbonGalleryBarItem  -@   5        $
 gFont��5�   3   $
 Font 6A      Links 5�      )  #DevExpress.Xpf.Bars.BarEditItemLink  5�   "   $ 	eFontSize��66   $ SmallWithText=�6N      Gallery 5�      !  DevExpress.Xpf.Bars.Gallery  5�   "   -A   5           MinColCount$ 1��5�   .       ColCount$  5��6>   !   ItemCheckMode$! Single=�5�   "   "   IsGroupCaptionVisible] YDevExpress.Data.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5  /DevExpress.Utils.Design.DefaultBooleanConverter$" False 5�   "   #   IsItemCaptionVisible# .  $   IsItemDescriptionVisible$ .  #%   ItemCaptionVerticalAlignment$% Center=�5  "   &   Groups& 5  &   *  $DevExpress.Xpf.Bars.GalleryItemGroup  5  *   -B   5        $
 FontFamilyGalleryGroup��5  ?   5  #   '   DropDownGallery' 5       5	  "   -C   5        $ 1��5	  .   $  1��6>   (   RowCount$( 5��5
  "   $! Single=�5  "   $" False 5  "   )   AllowFilter) .  *   IsItemGlyphVisible* . $ .  %+   ItemContentHorizontalAlignment$+ Stretch=�5  "   #,   ItemContentVerticalAlignment$, Center=�5  "   -   SizeMode$- Vertical=�5  "   & 5  &     5  *   -D   5        $$
 FontFamilyDropDownGalleryGroup��5  ?   5  #   5     %  DevExpress.Xpf.Bars.BarEditItem  5     -E   5        $
 	eFontSize��5  *   $ SmallWithoutText=�6;   .   	EditWidth$. 90X�6Z   $ 
Font Size: 5     /   EditSettings/ 5     :  4DevExpress.Xpf.Editors.Settings.ComboBoxEditSettings  5   "   ;  5DevExpress.Xpf.Editors.Settings.PopupBaseEditSettings0   PopupMaxHeight$	0 250Q�6;   8  2DevExpress.Xpf.Editors.Settings.ButtonEditSettings1   IsTextEditable1 .  6P   5"     5%     5&     5'     5)     52     53     