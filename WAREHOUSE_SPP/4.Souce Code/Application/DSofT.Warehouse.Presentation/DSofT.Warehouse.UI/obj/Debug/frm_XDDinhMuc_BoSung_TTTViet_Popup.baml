   M S B A M L   `   `   `  ����Q  MDSofT.Framework.UICore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICoreDSofT.Framework.UICore  5	      T PDSofT.Warehouse.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.ResourcesDSofT.Warehouse.Resources 5
      T PDSofT.Framework.UIControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControlDSofT.Framework.UIControl 5      [ WDSofT.Framework.Metro.UIControls, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls DSofT.Framework.Metro.UIControls 5       DSofT.Warehouse.UI;   5DSofT.Warehouse.UI.frm_XDDinhMuc_BoSung_TTTViet_Popup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      f	 bDevExpress.Xpf.Grid.v14.2.Core, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al
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
��auto �6     � ColumnDefinitions 5.      �� 5/      $
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
��0.3* �6+   �� 5G      $��10 �6+   �� 5H      $
��0.3* �6+   �� 5I      $
��Auto �6+   �� 5J      $
��0.3* �6+   �� 5K      $
��0.1* �6+   .+�5N      �� -   5        $��lblMaSP��5N   @   $��0��6!   $��0��60   $��Center=�6O   �� 6k    	 
lblMaThuoc#�� 	 6o   �� 6�    
 	lblDauSao#�[
 6�   
���  ��6�   6�   %  DevExpress.Xpf.Editors.TextEdit  5O      -	   5        $��txtMaSP��5O   @   $��0��6#   $��1��60   {�.  )  #DevExpress.Xpf.Editors.TextEditBase   Text 6a   �� *
MA_SANPHAM+ �� UpdateSourceTrigger$ PropertyChanged=�  5P      -
   5        $��txtTenSP��5P   A   $��0��6#   $��3��60   {�.   6c   �� *TEN_SANPHAM+$ PropertyChanged=��� 5Q      -   5        $��lblDVT��5Q   @   $��0��6!   $��4��60   $��Center=�6N   �� 6j    
 lblDVT#��  6n   �� 6�   #�[
 6�   
���  ��6�   6�     5R      -   5        $��txtDVT��5R   @   $��0��6#   $��5��60   {�.   6`   �� *MA_DONVI_TINH+$ PropertyChanged=��� 5S      -   5        $��btnChon��5S   |   $��0��6   $��6��6*     lblChonSanPham# [ 6:   $��Right=�6`   #��[ 6�   $��Center=�6�   6�   5U      5V      �� 5X      $��2q�6   $��2��6#   $��4��60   .��5Y      �  5Z      �� 5[      $
��Auto �6(   68    5]      �� 5^      $
��Auto �6+   6:   �� 5_      $
��0.1* �6+   6:   �� 5`      $
��Auto �6+   6:   �� 5a      $
��0.1* �6+   6:   .+�5d      �� -   5        $��
lblSLThung��5d   @   $��0��6!   $��0��60   $��Center=�6R   �� 6n     lblSLDThung#��  6r   6�     5e      -   5        $��	txtSLuong��5e   A   $��0��6#   $��1��60      MaskType$ Numeric=�6R      Mask$/ )#,##,##,##,##,##,##,##,##,##,##,##,##,###��6e      MaskUseAsDisplayFormat .  6�   �� *SOLUONG_DONGGOI+�� 5f      -   5        $��lblDVT1��5f   @   $��0��6!   $��2��60   $��Center=�6O   �� 6k     lblDVTThung#��  6o   6�   )  #DevExpress.Xpf.Editors.ComboBoxEdit  5g      -   5        $��cboDVT��5g   E   $��0��6'   $��3��64   +  %DevExpress.Xpf.Editors.LookUpEditBase   IncrementalFiltering . '  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable .    FilterCondition$ ContainsX�6�      ImmediatePopup . %	  DevExpress.Xpf.Editors.BaseEdit 	  	EditValue 6S   �� *MA_DONVI_TINH_DONGGOI+$ PropertyChanged=� �� Mode$ TwoWay=�6�   5h      5i      �� 5k      -   5        $ BrdBtnWrap2��5k      $��3��6*   $��0��67   $��4��6G   #��[ 6[   ._�5l      �  5m      �� 5n      $
��Auto �6+   .+�5p      �� -   5        $��
btnChonNVL��5p   x   $��0��6   $��6��6*     
btnChonNVL# [ 6:   $��Right=�6\   #��[ 6�   $��Center=�6�   6�   5q      5r      �� 5t      -   5        $ BrdBtnWrap3��5t      $��2��6*   $��5��67   $��4��6G   #��[ 6[   ._�5u      �  5v      �� 5w      $
��Auto �6+   .+�5y      �� -   5        $��btnChonNVL1��5y   x   $��0��6   $��6��6*   # [ 6:   $��Right=�6\   #��[ 6�   $��Center=�6�   6�   5z      5{      �� 5~      $��2q�6   $��4��6#   $��4��60     lblNVLThung#���  6D   .��5      %
  DevExpress.Xpf.Grid.GridControl
  -   5         
 Name$	 grd��5   =   ) 	 #DevExpress.Xpf.Grid.DataControlBase   AutoPopulateColumns . $��1��6K   $��0��6X     
  View  5�      #  DevExpress.Xpf.Grid.TableView  5�      -   5        !  Name$! grdView��5�   +   "   	AutoWidth" . & 	  DevExpress.Xpf.Grid.DataViewBase#   NavigationStyle$
# Cell=�5�   :   &   DevExpress.Xpf.Grid.GridViewBase$   ShowGroupPanel$ .   %   UseAnimationWhenExpanding% .  5�   <   5�      5�      �� 5�      $��2q�6   $��3��6#   $��5��60   $��4��6@   $��2��6T     lblNVLSP#���  6e   .��5�      
  -   5        $
 grd1��5�   =    . $��1��6L   $��0��6Y     5�        5�      -   5        $! grdView1��5�   +   " . $
# Cell=�5�   :   $ .  % .  5�   =   5�      5�      �� 5�      -   5        $ BrdBtnWrap4��5�      $��5��6*   $��0��67   $��8��6G   #��[ 6[   $
��auto��6�   ._�5�      �  5�      �� 5�      $
��Auto �6+   �� 5�      $
��0.2* �6+   �� 5�      $
��Auto �6+   �� 5�      $
��0.2* �6+   �� 5�      $
��Auto �6+   �� 5�      $
��0.2* �6+   �� 5�      $
��Auto �6+   �� 5�      $
��0.2* �6+   �� 5�      $
��Auto �6+   �� 5�      $
��0.2* �6+   .+�5�      �� -   5        $��lblCPDV��5�   0   $��0��6    $��Center=�6?   �� 6[     lblChiPhiDichVu#��  6_   6�     5�      -   5        $��txtCPDV��5�   4   $��1��6#   $ Numeric=�6C   $/ )#,##,##,##,##,##,##,##,##,##,##,##,##,###��6V    . {�.   6�   �� *CHIPHI_DICHVU+$ PropertyChanged=��� 5�      -   5        $��lblNhanTyLe��5�   0   $��2��6    $��Center=�6C   �� 6_     lblNhanThemTyLe#��  6c   6�     5�      -   5        $��	txtNhanTL��5�   4   $��3��6#   $ Numeric=�6E   $/ )#,##,##,##,##,##,##,##,##,##,##,##,##,###��6X    6�   �� *NHANTHEM_TYLE+6�   �� 5�      -   5        $��	lblGiaBan��5�   2   $��4��6"   $��Center=�6C   �� 6_    ! frm_lapphieuxuat_tuPYC_GiaBan#��  6c   6�     5�      -    5        $��	txtGiaBan��5�   4   $��5��6#   {�.  $ Numeric=�6W   $/ )#,##,##,##,##,##,##,##,##,##,##,##,##,###��6j    .  6�   �� *GIABAN+$ PropertyChanged=��� 5�      -!   5        $��
lblGiaNhap��5�   1   $��6��6!   $��Center=�6C   �� 6_    " frm_lapphieuxuat_tuPYC_GiaNhap#��  6c   6�     5�      -"   5        $��
txtGiaNhap��5�   5   $��7��6$   {�.  $ Numeric=�6Y   $/ )#,##,##,##,##,##,##,##,##,##,##,##,##,###��6l    .  6�   �� *	GIANHAP+$ PropertyChanged=��� 5�      -#   5        $��	lblGhiChu��5�   2   $��8��6"   $��Center=�6C   �� 6_     	lblGhiChu#��  6c   6�     5�      -$   5        $��	txtGhiChu��5�   4   $��9��6#    6E   �� *	GHI_CHU+5�      5�      5�      5�      5�      