   M S B A M L   `   `   `  ����Q  MDSofT.Framework.UICore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICoreDSofT.Framework.UICore  5	      T PDSofT.Warehouse.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.ResourcesDSofT.Warehouse.Resources 5
      T PDSofT.Framework.UIControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControlDSofT.Framework.UIControl 5      [ WDSofT.Framework.Metro.UIControls, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls DSofT.Framework.Metro.UIControls 5       DSofT.Warehouse.UIC   =DSofT.Warehouse.UI.frm_XDDinhMuc_BoSung_TTTViet_TimKiemSP_Kho   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      f	 bDevExpress.Xpf.Grid.v14.2.Core, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al
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
��0.1* �6    �� 5*      $��35 �6    �� 5+      $
��0.1* �6    .+�5.      �� -   5         ��Name$ BrdBtnWrap2��5.      $��0��6*     Sdk.Brd.BtnArea#��[ 67   ._�5/      �  � ColumnDefinitions 50      �� 51      $
��Auto �6+   �� 52      $
��0.2* �6+   �� 53      $
��0.1* �6+   �� 54      $
��0.5* �6+   �� 55      $
��0.2* �6+   .+�58      �� $��15(�6    $��0��6.   $��
Chọn kho��6>   $��Center=�6O   $��Center=�6l   6�   )  #DevExpress.Xpf.Editors.ComboBoxEdit  59      -   5        $��cboKho��59   8   $��1��6'   $
��Left=�6H   $��Center=�6c   %  DevExpress.Xpf.Editors.BaseEdit   ValidateOnTextInput .  +  %DevExpress.Xpf.Editors.LookUpEditBase   IncrementalFiltering .    ImmediatePopup .    FilterCondition$ ContainsX�6  $	��200��67     	EditValue 6�   �� *KHO_ID+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6F  �� 5<      $��15(�6    $��2��6.     lblItemType#��[ 6>   $
��Left=�6c   $��Center=�6~   6�     5=      -   5        $��cboItemType��5=   8   $��3��6'    .  $
��Left=�6h   $��Center=�6�    .  . $ ContainsX�6%  $	��200��6B   6�   �� *MA_ITEM_TYPE+$ PropertyChanged=�$ TwoWay=�6Q  5@      5A      �� 5B      -   5        $ 	nbcSearch��5B      $��1��6(   $��2��65   #��[ 6I   ._�5C      � -   5         �Name$ Selection_Area��5C       5D      �� 5E      $��25 �6(    5G      �� 5H      $
��Auto �6+   5J      5K      �� 5L      -   5        $ nbcgrid��5L      $��1��6&   $��2��63   $��2��6G   #��[ 6X   ._�5M      %  DevExpress.Xpf.Grid.GridControl  -   5          Name$ grdSPALL��5M   =   ) 	 #DevExpress.Xpf.Grid.DataControlBase   AutoPopulateColumns . $
��Auto��6O      View 5N      #	  DevExpress.Xpf.Grid.TableView	  5O      -	   5         	 Name$ grdViewSPALL��5P   +    	  	AutoWidth . &
 	  DevExpress.Xpf.Grid.DataViewBase 
  NavigationStyle$
 Cell=�5O   :   &   DevExpress.Xpf.Grid.GridViewBase    ShowGroupPanel  .  ! 	  ShowAutoFilterRow! . " 
  SearchPanelFindFilter$" Contains=�6�   # 
  SearchPanelFindMode$# 	FindClick=�6�    $   UseAnimationWhenExpanding$ .  $% 	  HorizontalScrollbarVisibility$% Visible=�6�   5P   +   5R      5S      �� 5T      -
   5        $��btnChon��5T   �   $��4��6   $��3��6&    # frm_phancong_thietbi_popup_Chon
& � Tag#& [ 6K   $
��Left=�6�     Sdk.Btn.Comm#��[ 6�   $��Center=�6�   �� 5V      -   5        $ nbcgrqid��5V      $��4��6'   $��2��64   $��2��6H   #��[ 6Y   ._�5W        -   5        $ 	grdSPCHON��5W   =    . $
��Auto��6~    5X      	  5Y      -   5        $ grdViewSPCHON��5Z   +    . $
 Cell=�5Y   :     .  $ .  5Z   +   5\      5]      5^      5_      5`      