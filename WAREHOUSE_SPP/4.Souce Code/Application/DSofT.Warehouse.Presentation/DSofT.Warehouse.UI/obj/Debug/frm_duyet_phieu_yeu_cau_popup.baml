   M S B A M L   `   `   `  ����Q  MDSofT.Framework.UICore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICoreDSofT.Framework.UICore  5	      T PDSofT.Warehouse.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.ResourcesDSofT.Warehouse.Resources 5
      T PDSofT.Framework.UIControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControlDSofT.Framework.UIControl 5      [ WDSofT.Framework.Metro.UIControls, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls DSofT.Framework.Metro.UIControls 5       DSofT.Warehouse.UI6   0DSofT.Warehouse.UI.frm_duyet_phieu_yeu_cau_popup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      f	 bDevExpress.Xpf.Grid.v14.2.Core, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al
 hDevExpress.Xpf.Grid.v14.2.Extensions, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid 	 
  5      a ]DevExpress.Xpf.Core.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      QuicoreDclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICore  5	      XresTextJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.Resources  5
      UcntrJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControl  5      c _DevExpress.Xpf.NavBar.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxn4http://schemas.devexpress.com/winfx/2008/xaml/navbar  5      ] YDevExpress.Mvvm.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      iControlsXclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls   5      $	��650��5      $
��1250��6     � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $T Npack://application:,,,/DSofT.Warehouse.Resources;component/CommonResource.xaml?�6%   %    5      .��5      � 
������   5   
   7  1DSofT.Framework.UIControl.ConverterStringDateTime   ConverterStringDateTime��%   &	          5      6+   5      2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors 5   
   ,  &DevExpress.Mvvm.UI.NotificationService  5      -   5          Name$ notificationService��5   )      ApplicationId$ DxToastTest��6F   &   UseWin8NotificationsIfAvailable .   CustomNotificationTemplate!   CustomNotificationTemplate# [ 5   #      Sound$! Notification_Looping_Alarm8=�5   #   !	   CustomNotificationPosition$	 BottomRight=�5    #   (
   !CustomNotificationVisibleMaxCount$
 5��6L   !   CustomNotificationDuration$ 0:0:3h�6r   %   PredefinedNotificationTemplate 5   #   ��  �� ElementName$ notificationStyle�� �� Path$ SelectedItem�5    r   .+�5"   
   �  � RowDefinitions 5$      �� 5&      $
��auto �6    �� 5(      $
��auto �6    �� 5)      $��* �6    �� 5*      $
��auto �6    .+�5.      �� -   5         ��Name$ 
BrdBtnWrap��5.      $��0��6)   $��2��66     Sdk.Brd.BtnArea#��[ 6J   ._�5/      ��   Sdk.Spnl.CommBtn.Right#��[ 6   .+�50      �� -   5        $��btnSave��50   E   
 � Tag$ 
Đồng ý��6   $��Right=�6)     Sdk.Btn.Comm#��[ 6T   $��Center=�6z   6�   �� 51      -   5        $��btnClose��51   Y     btnClose# [ 6   $��Right=�6=   #��[ 6i   $��Center=�6�   6�   52      53      �� 54      -   5        $ 	nbcSearch��54      $��1��6(   $��2��65   $��80��6I   #��[ 6U   ._�55      � -   5         �Name$ Selection_Area��55       56      �� 57      $��25 �6(   �� 58      $��25 �6(    � ColumnDefinitions 5:      �� 5;      $
��Auto �6+   �� 5<      $
��0.5* �6+   �� 5=      $
��Auto �6+   �� 5>      $
��0.5* �6+   �� 5?      $
��Auto �6+   �� 5@      $
��0.5* �6+   .+�5B      �� -   5        $��lblSoPhieuPYC��5B   @   $��0��6!   $��0��60   $��Center=�6U   �� 6q     lblSoPhieuPYC#��  6u   6�   %  DevExpress.Xpf.Editors.TextEdit  5C      -   5        $��txtSoPhieuPYC��5C   B   $��0��6#   $��1��61   {�.  )  #DevExpress.Xpf.Editors.TextEditBase   Text 6W   �� *	SOPHIEU+6p   �� 5E      -	   5        $��lblNgayPhieu��5E   @   $��0��6!   $��2��60   $��Center=�6T   �� 6p     lblNgayPhieu#��  6t   6�     5F      -
   5        $��txtNgayPhieu��5F   T   $��3��6#   $��0��63      MaskType$ DateTime=�6@   {�.   6h   �� *	NGAYLAP+6�   �� 5H      -   5        $��lblThoiGianHoanTat��5H   @   $��0��6!   $��4��60   $��Center=�6Z   �� 6v     lblThoiGianHoanTat#��  6z   6�     5I      -   5        $��txtThoiGianHoanTat��5I   T   $��5��6#   $��0��63   $ DateTime=�6@   {�.   6n   �� *THOIGIAN_HOANTAT+6�   �� 5L      -   5        $��lblLoaiDichVu��5L   @   $��1��6!   $��0��60   $��Center=�6U   �� 6q    	 lblLoaiDichVu#�� 	 6u   6�   )  #DevExpress.Xpf.Editors.ComboBoxEdit  5M      -   5        $��cboLoaiDichVu��5M   X   $��1��6'   $��1��64   $��3��6D   '  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable . %  DevExpress.Xpf.Editors.BaseEdit   ValidateOnTextInput .  +	  %DevExpress.Xpf.Editors.LookUpEditBase 	  IncrementalFiltering .  	  ImmediatePopup .  	  FilterCondition$ ContainsX�6'  {�.     	EditValue 6�   �� *MA_LOAI_DICHVU+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6�  �� 5O      -   5        $��lblDKThucHien��5O   @   $��1��6!   $��4��60   $��Center=�6U   �� 6q    
 lblDKThucHien#�� 
 6u   6�     5P      -   5        $��txtDKThucHien��5P   U   $��1��6#   $��5��61   $��2��6A   {�.   6l   �� *DIEUKIEN_THUCHIEN+6�   5Q      5R      �� 5S      -   5        $ 
nbcSearch1��5S      $��2��6)   $
��auto��66   $��2��6D   #��[ 6X   ._�5T      � -   5        $ Selection_Area1��5T       5U      �� 5V      $��25 �6(   �� 5W      $��* �6(    5Y      �� 5Z      $
��0.5* �6+   �� 5[      $
��Auto �6+   �� 5\      $��0.125* �6+   �� 5]      $
��0.5* �6+   .+�5_      �� -   5        $��lblSPCanBoSungTTTV��5_   U   $��0��6!   $��0��60   $��3��6@   $��Center=�6o   �� 6�     lblSPCanBoSungTTTV#��  6�   6�   �� 5`      -   5        $��lblNVLDiKem��5`   A   $��0��6!   $��4��60   $��Center=�6T   �� 6p     lblNVLDiKem#��  6t   6�   %
  DevExpress.Xpf.Grid.GridControl
  5a      -   5         
 Name$	 grd��5a   A   ) 	 #DevExpress.Xpf.Grid.DataControlBase   AutoPopulateColumns . $��0��6O   $��1��6_     
  View  5b      #  DevExpress.Xpf.Grid.TableView  5c      -   5        !  Name$! grdView��5d   +   & 	  DevExpress.Xpf.Grid.DataViewBase"   NavigationStyle$
" Cell=�5c   .   &   DevExpress.Xpf.Grid.GridViewBase#   ShowGroupPanel# .   $   UseAnimationWhenExpanding$ .  5d   +   5f      
  5g      -   5        $ grd_2��5g   C    . $��3��6S   $��1��6c     5h        5i      -   5        $! 	grdView_2��5j   +   $
" Cell=�5i   .   # .  $ .  5j   +   5l      5m      5n      �� 5o      -   5        $ 
nbcSearch2��5o      $��3��6)   $��2��67   #��[ 6K   ._�5p      � -   5        $ Selection_Area2��5p       5q      �� 5r      $��25 �6(   �� 5s      $��25 �6(    5v      �� 5w      $
��auto �6+   �� 5x      $
��auto �6+   �� 5y      $
��auto �6+   �� 5z      $
��0.5* �6+   �� 5{      $
��auto �6+   �� 5|      $
��0.5* �6+   �� 5}      $
��Auto �6+   �� 5~      $
��0.5* �6+   �� 5      $
��Auto �6+   �� 5�      $
��0.5* �6+   .+�5�      	� -   5        $
��rdbD��5�   @   $��1��6"   $��0��6/   $��Duyệt��6M   6]   	� 5�      -   5        $
��rdbK��5�   @   $��1��6"   $��1��6/   $��Từ chối��6M   6_   �� 5�      -   5        $��lblDuyetBoi��5�   @   $��1��6!   $��2��60   $��Center=�6S   �� 6o   $�Duyệt bởi��6s   6�     5�      -   5        $��txtDuyetBoi��5�   A   $��1��6#   $��3��61    6V   �� *NGUOI_DUYET+�� 5�      -   5        $��lblNgay��5�   @   $��1��6!   $��4��60   $��Center=�6O   �� 6k     lblNgay#��  6o   6�   %  DevExpress.Xpf.Editors.DateEdit  5�      -    5        $��dtpNgay��5�   T   $��5��6#   $��1��63   $ DateTime=�6@   %   MaskUseAsDisplayFormat% . &   Mask$& 
dd/MM/yyyy��6)  '   ShowClearButton' .   6�   �� *
NGAY_DUYET+( �� 	Converter#( [  ) �� ConverterParameter$) 
dd/MM/yyyy��$ TwoWay=�$ PropertyChanged=�6T  �� 5�      -!   5        $��lblLyDo��5�   @   $��1��6!   $��6��60   $��Center=�6O   �� 6k     lblLyDo#��  6o   6�     5�      -"   5        $��txtLyDo��5�   U   $��1��6#   $��7��61   $��3��6A   {�.   6f   �� *LYDO+6|   5�      5�      5�   
   5�      5�      