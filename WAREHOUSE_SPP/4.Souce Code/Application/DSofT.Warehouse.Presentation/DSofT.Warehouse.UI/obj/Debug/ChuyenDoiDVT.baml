   M S B A M L   `   `   `  ����Q  MDSofT.Framework.UICore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICoreDSofT.Framework.UICore  5	      T PDSofT.Warehouse.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.ResourcesDSofT.Warehouse.Resources 5
      T PDSofT.Framework.UIControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControlDSofT.Framework.UIControl 5      [ WDSofT.Framework.Metro.UIControls, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls DSofT.Framework.Metro.UIControls 5       DSofT.Warehouse.UI%   DSofT.Warehouse.UI.ChuyenDoiDVT   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      f	 bDevExpress.Xpf.Grid.v14.2.Core, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al
 hDevExpress.Xpf.Grid.v14.2.Extensions, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid 	 
  5      a ]DevExpress.Xpf.Core.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      QuicoreDclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICore  5	      XresTextJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.Resources  5
      UcntrJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControl  5      c _DevExpress.Xpf.NavBar.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxn4http://schemas.devexpress.com/winfx/2008/xaml/navbar  5      ] YDevExpress.Mvvm.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      iControlsXclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls   5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $T Npack://application:,,,/DSofT.Warehouse.Resources;component/CommonResource.xaml?�6%   %    5      .��5      � 
������   5   
   7  1DSofT.Framework.UIControl.ConverterStringDateTime   ConverterStringDateTime��%   &	          5      6+   5      2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors 5   
   ,  &DevExpress.Mvvm.UI.NotificationService  5      -   5          Name$ notificationService��5   )      ApplicationId$ DxToastTest��6F   &   UseWin8NotificationsIfAvailable .   CustomNotificationTemplate!   CustomNotificationTemplate# [ 5   #      Sound$! Notification_Looping_Alarm8=�5    #   !	   CustomNotificationPosition$	 BottomRight=�5!   #   (
   !CustomNotificationVisibleMaxCount$
 5��6L   !   CustomNotificationDuration$ 0:0:3h�6r   %   PredefinedNotificationTemplate 5   #   ��  �� ElementName$ notificationStyle�� �� Path$ SelectedItem�5!   r   .+�5#   
   �  � RowDefinitions 5%      �� 5'      $
��auto �6    �� 5)      $��* �6    .+�5,      �� -   5         ��Name$ tabPhieuChi��5,      $��1��6.    �� SelectedIndex$ 0��6;   $��0,5,0,0q�6M   .@�5-      �� -   5         ��Name$ tbl_QuyDoiDVT��5-   C     tbl_QuyDoiDVT#���  6   $	��100��6Z   .��5.      � -   5         �Name$ Selection_Area��5.       � ColumnDefinitions 5/      �� 50      $
��Auto �6/   �� 51      $��0.02* �6/   �� 52      $
��Auto �6/   �� 53      $��0.02* �6/   �� 54      $
��Auto �6/   �� 55      $��0.02* �6/    57      �� 58      $��25 �6,   �� 59      $��25 �6,   �� 5:      $��25 �6,   �� 5;      $��* �6,   .+�5=      �� -   5        $��lblChonSanPham��5=        lblChonSanPham
 � Tag# [ 6!   $
��Left=�6G   $��2��6b   $��1��6o     Sdk.Btn.Comm#��[ 6�   $��Center=�6�   6�   �� 5>      -   5        $��btnXoa��5>   z     	btnDelete# [ 6!   $
��Left=�6B   $��2��6]   $��4��6j   #��[ 6�   $��Center=�6�   �� 5?      -   5        $��btnLuu��5?   x     btnSave# [ 6!   $
��Left=�6@   $��2��6[   $��5��6h   #��[ 6�   $��Center=�6�   6�   �� 5@      $��0��6%   $��0��65     lblTuDVT#��[ 6B   $
��Auto��6c   $
��Left=�6p   $��Center=�6�   6�   )  #DevExpress.Xpf.Editors.ComboBoxEdit  5B      -   5        $��	txtTuDVT1��5B   H   $��0��6+   $��1��68   %  DevExpress.Xpf.Editors.BaseEdit   ValidateOnTextInput .  +  %DevExpress.Xpf.Editors.LookUpEditBase   IncrementalFiltering . '  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable .    FilterCondition$ ContainsX�6     ImmediatePopup .    	EditValue 6u   �� *MA_DONVI_TINH_TU+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6  �� 5D      $��2��6%   $��0��65     	lblDenDVT#��[ 6B   $
��Auto��6d   $
��Left=�6q   $��Center=�6�   6�     5E      -	   5        $��
txtDenDVT1��5E   H   $��0��6+   $��3��68    .   .  . $ ContainsX�6   .  6v   �� *MA_DONVI_TINH_DEN+$ PropertyChanged=�$ TwoWay=�6  �� 5G      $��4��6%   $��0��65    	 lblSLQuyDoi#��[	 6B   $
��Auto��6f   $
��Left=�6s   $��Center=�6�   6�   %  DevExpress.Xpf.Editors.TextEdit  5H      -
   5        $��txtSLQuyDoi��5H   E   $��5��6(   $��0��68   $��3��6X   )	  #DevExpress.Xpf.Editors.TextEditBase 	  Text 6l   �� *SOLUONG_QUYDOI+%
  DevExpress.Xpf.Grid.GridControl
  5J      -   5         
 Name$ grdQUYDOI_DVT��5J   E   ) 	 #DevExpress.Xpf.Grid.DataControlBase    AutoPopulateColumns  . $��1��6\   $��3��6l   $��5��6y   ! 
  View! 5K      #  DevExpress.Xpf.Grid.TableView  5L   "   -   5        "  Name$" grdViewQUYDOI_DVT��5M   +   #   	AutoWidth# . & 	  DevExpress.Xpf.Grid.DataViewBase$   NavigationStyle$
$ Cell=�5L   A   &   DevExpress.Xpf.Grid.GridViewBase%   ShowGroupPanel% .  &   ShowAutoFilterRow& . '   SearchPanelFindFilter$' Contains=�6�   (   SearchPanelFindMode$( 	FindClick=�6�    )   UseAnimationWhenExpanding) .  5M   +   5O      5P      5R      �� 5T      -   5        $" tbl_KinhDotbl_CacSPQuyDoianh��5T   E    
 tbl_CacSPQuyDoi#��� 
 6   $	��100��6k   .��5U      � -   5        $ Selection_Area1��5U       5Y      �� 5Z      $
��Auto �6/   �� 5[      $��0.02* �6/   �� 5\      $
��Auto �6/   �� 5]      $��0.02* �6/    5_      �� 5`      $��25 �6,   �� 5a      $��25 �6,   �� 5b      $��* �6,   .+�5d      �� $��0��6%   $��0��65   #��[ 6B   $
��Auto��6c   $
��Left=�6p   $��Center=�6�   6�     5e      -   5        $��txtTuDVT��5e   I   $��0��6+   $��1��68    6Z   �� *TuDVT+�� 5f      $��2��6%   $��0��65   #��[ 6B   $
��Auto��6d   $
��Left=�6q   $��Center=�6�   6�     5g      -   5        $��	txtDenDVT��5g   I   $��3��6,   $��0��6<    6Z   �� *DenDVT+�� 5h      $��0��6%   $��1��65     lblItemType#��[ 6B   $
��Auto��6f   $
��Left=�6s   $��Center=�6�   6�     5i      -   5        $��txtItemType��5i   I   $��1��6,   $��1��6<    6\   �� *	ITEM_TYPE+�� 5j      -   5        $��	btnSearch��5j   z     	btnSearch# [ 6!   $
��Left=�6B   $��2��6]   $��1��6m   #��[ 6�   $��Center=�6�     5k      -   5        $��
txtTimKiem��5k   F   $��3��6(   $��1��68    6X   �� *	TIMKIEM+
  5l      -   5        $ grdSANPHAM_QUYDOI��5l   E     . $��1��6`   $��3��6p   $��3��6}   ! 5m        5n   "   -   5        $" grdViewSANPHAM_QUYDOI��5o   +   # . $
$ Cell=�5n   A   % .  & . $' Contains=�6�   $( 	FindClick=�6�   ) .  5o   +   5q      5r      5s      5v      5w      5x      5y      