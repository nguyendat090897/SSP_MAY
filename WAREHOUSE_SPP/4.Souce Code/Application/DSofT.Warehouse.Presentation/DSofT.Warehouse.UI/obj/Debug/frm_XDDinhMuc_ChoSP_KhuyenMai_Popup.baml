   M S B A M L   `   `   `  ����Q  MDSofT.Framework.UICore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICoreDSofT.Framework.UICore  5	      T PDSofT.Warehouse.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.ResourcesDSofT.Warehouse.Resources 5
      T PDSofT.Framework.UIControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControlDSofT.Framework.UIControl 5      [ WDSofT.Framework.Metro.UIControls, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls DSofT.Framework.Metro.UIControls 5       DSofT.Warehouse.UI<   6DSofT.Warehouse.UI.frm_XDDinhMuc_ChoSP_KhuyenMai_Popup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      f	 bDevExpress.Xpf.Grid.v14.2.Core, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al
 hDevExpress.Xpf.Grid.v14.2.Extensions, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid 	 
  5      a ]DevExpress.Xpf.Core.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      QuicoreDclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICore  5	      XresTextJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.Resources  5
      UcntrJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControl  5      c _DevExpress.Xpf.NavBar.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxn4http://schemas.devexpress.com/winfx/2008/xaml/navbar  5      ] YDevExpress.Mvvm.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      iControlsXclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls   5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $T Npack://application:,,,/DSofT.Warehouse.Resources;component/CommonResource.xaml?�6%   %    5      .��5      � 
������   5   
   7  1DSofT.Framework.UIControl.ConverterStringDateTime   ConverterStringDateTime��%   &	          5      6+   5      2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors 5   
   ,  &DevExpress.Mvvm.UI.NotificationService  5      -   5          Name$ notificationService��5   )      ApplicationId$ DxToastTest��6F   &   UseWin8NotificationsIfAvailable .   CustomNotificationTemplate!   CustomNotificationTemplate# [ 5   #      Sound$! Notification_Looping_Alarm8=�5   #   !	   CustomNotificationPosition$	 BottomRight=�5    #   (
   !CustomNotificationVisibleMaxCount$
 5��6L   !   CustomNotificationDuration$ 0:0:3h�6r   %   PredefinedNotificationTemplate 5   #   ��  �� ElementName$ notificationStyle�� �� Path$ SelectedItem�5    r   .+�5"   
   �  � RowDefinitions 5$      �� 5&      $
��auto �6    �� 5(      $
��auto �6    �� 5)      $
��auto �6    �� 5*      $
��auto �6    �� 5+      $
��0.1* �6    �� 5,      $
��Auto �6     � ColumnDefinitions 5.      �� 5/      $
��Auto �6#   �� 50      $
��0.5* �6#   �� 51      $
��Auto �6#   �� 52      $
��0.5* �6#   �� 53      $
��Auto �6#   �� 54      $
��0.5* �6#   �� 55      $
��Auto �6#   �� 56      $
��0.5* �6#   .+�59      �� -   5         ��Name$ 
BrdBtnWrap��59      $��0��6)   $��0��66   $��8��6F     Sdk.Brd.BtnArea#��[ 6Z   ._�5:      ��   Sdk.Spnl.CommBtn.Right#��[ 6   .+�5;      �� -   5        $��btnKhaiBaoCPDV��5;   g     lblKhaiBaoChiPhiDichVu
 � Tag# [ 6   $��Right=�6K     Sdk.Btn.Comm#��[ 6}   $��Center=�6�   6�   �� 5<      -   5        $��btnNew��5<   W    
 btnNew# [ 6   $��Right=�6;   #��[ 6e   $��Center=�6�   6�   �� 5=      -   5        $��btnSave��5=   X     btnSave# [ 6   $��Right=�6<   #��[ 6g   $��Center=�6�   6�   �� 5>      -   5        $��btnClose��5>   Y     btnClose# [ 6   $��Right=�6=   #��[ 6i   $��Center=�6�   6�   5?      5@      �� 5B      -   5        $ BrdBtnWrap1��5B      $��1��6*   $��0��67   $��8��6G   #��[ 6[   ._�5C      �  5D      �� 5E      $
��Auto �6+   �� 5F      $
��0.3* �6+   �� 5G      $
��Auto �6+   �� 5H      $
��0.3* �6+   �� 5I      $
��Auto �6+   �� 5J      $
��0.3* �6+   �� 5K      $
��Auto �6+   .+�5N      �� -   5        $��lblMaSP��5N   @   $��0��6!   $��0��60   $��Center=�6O   �� 6k    	 
lblMaThuoc#�� 	 6o   �� 6�    
 	lblDauSao#�[
 6�   
���  ��6�   6�   %  DevExpress.Xpf.Editors.TextEdit  5O      -	   5        $��txtMaSP��5O   @   $��0��6#   $��1��60   {�.  )  #DevExpress.Xpf.Editors.TextEditBase   Text 6a   �� *
MA_SANPHAM+ �� UpdateSourceTrigger$ PropertyChanged=��� 5P      -
   5        $��lblTenSP��5P   @   $��0��6!   $��2��60   $��Center=�6P   �� 6l     lblTenThuoc#��  6p   �� 6�   #�[
 6�   
���  ��6�   6�     5Q      -   5        $��txtTenSP��5Q   A   $��0��6#   $��3��60   {�.   6c   �� *TEN_SANPHAM+$ PropertyChanged=��� 5R      -   5        $��lblDVT��5R   @   $��0��6!   $��4��60   $��Center=�6N   �� 6j    
 lblDVT#��  6n   �� 6�   #�[
 6�   
���  ��6�   6�     5S      -   5        $��txtDVT��5S   @   $��0��6#   $��5��60   {�.   6`   �� *MA_DONVI_TINH+$ PropertyChanged=��� 5T      -   5        $��btnChon��5T   |   $��0��6   $��6��6*     lblChonSanPham# [ 6:   $��Right=�6`   #��[ 6�   $��Center=�6�   6�   5V      5W      �� 5Y      -   5        $ BrdBtnWrap2��5Y      $��2��6*   $��0��67   $��4��6G   #��[ 6[   ._�5Z      �  5[      �� 5\      $
��Auto �6+   .+�5^      �� -   5        $��btnChonSPDongGoi��5^   ~   $��0��6   $��6��6*     btnChonSPDongGoi# [ 6:   $��Right=�6b   #��[ 6�   $��Center=�6�   6�   5_      5`      �� 5b      -   5        $ BrdBtnWrap3��5b      $��2��6*   $��5��67   $��4��6G   #��[ 6[   ._�5c      �  5d      �� 5e      $
��Auto �6+   .+�5g      �� -   5        $��
btnChonNVL��5g   x   $��0��6   $��6��6*     
btnChonNVL# [ 6:   $��Right=�6\   #��[ 6�   $��Center=�6�   6�   5h      5i      %  DevExpress.Xpf.Grid.GridControl  5k      -   5          Name$	 grd��5k   9   ) 	 #DevExpress.Xpf.Grid.DataControlBase   AutoPopulateColumns . $��3��6G   $��4��6T   $��2��6h      View 5l      #  DevExpress.Xpf.Grid.TableView  5m      -   5          Name$ grdView��5n   +      	AutoWidth . &	 	  DevExpress.Xpf.Grid.DataViewBase 	  NavigationStyle$
 Cell=�5m   6   &
   DevExpress.Xpf.Grid.GridViewBase 
  ShowGroupPanel .    
  UseAnimationWhenExpanding .  5n   <   5p        5r      -   5        $
 grd1��5r   9    . $��3��6H   $��5��6U   $��4��6e   $��2��6y    5s        5t      -   5        $ grdView1��5u   +    . $
 Cell=�5t   :    .   .  5u   =   5w      �� 5y      -   5        $ BrdBtnWrap4��5y      $��5��6*   $��0��67   $��8��6G   #��[ 6[   ._�5z      �  5{      �� 5|      $
��Auto �6+   �� 5}      $
��0.2* �6+   �� 5~      $
��Auto �6+   �� 5      $
��0.2* �6+   �� 5�      $
��Auto �6+   �� 5�      $
��0.2* �6+   �� 5�      $
��Auto �6+   �� 5�      $
��0.2* �6+   �� 5�      $
��Auto �6+   �� 5�      $
��0.2* �6+   .+�5�      �� -   5        $��lblCPDV��5�   0   $��0��6    $��Center=�6?   �� 6[     lblChiPhiDichVu#��  6_   6�     5�      -   5        $��txtCPDV��5�   4   $��1��6#   {�.     MaskType$ Numeric=�6U      Mask$/ )#,##,##,##,##,##,##,##,##,##,##,##,##,###��6h      MaskUseAsDisplayFormat .  6�   �� *CHIPHI_DICHVU+$ PropertyChanged=��� 5�      -   5        $��lblNhanTyLe��5�   0   $��2��6    $��Center=�6C   �� 6_     lblNhanThemTyLe#��  6c   6�     5�      -   5        $��txtTyLe��5�   4   $��3��6#   $ Numeric=�6C   $/ )#,##,##,##,##,##,##,##,##,##,##,##,##,###��6V    .  6�   �� *NHANTHEM_TYLE+6�   �� 5�      -   5        $��	lblGiaBan��5�   2   $��4��6"   $��Center=�6C   �� 6_    ! frm_lapphieuxuat_tuPYC_GiaBan#��  6c   6�     5�      -   5        $��	txtGiaBan��5�   4   $��5��6#   {�.  $ Numeric=�6W   $/ )#,##,##,##,##,##,##,##,##,##,##,##,##,###��6j    .  6�   �� *GIABAN+$ PropertyChanged=��� 5�      -   5        $��
lblGiaNhap��5�   1   $��6��6!   $��Center=�6C   �� 6_    " frm_lapphieuxuat_tuPYC_GiaNhap#��  6c   6�     5�      -   5        $��
txtGiaNhap��5�   5   $��7��6$   {�.  $ Numeric=�6Y   $/ )#,##,##,##,##,##,##,##,##,##,##,##,##,###��6l    .  6�   �� *	GIANHAP+$ PropertyChanged=��� 5�      -    5        $��	lblGhiChu��5�   2   $��8��6"   $��Center=�6C   �� 6_     	lblGhiChu#��  6c   6�     5�      -!   5        $��	txtGhiCHu��5�   4   $��9��6#    6E   �� *	GHI_CHU+5�      5�      5�      5�      5�      