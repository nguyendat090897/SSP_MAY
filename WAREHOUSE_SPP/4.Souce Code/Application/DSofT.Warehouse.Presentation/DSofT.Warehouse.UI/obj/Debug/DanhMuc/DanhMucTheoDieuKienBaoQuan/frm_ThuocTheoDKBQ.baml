   M S B A M L   `   `   `  ����M  IDSofT.Warehouse.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DSofT.Warehouse.UIDSofT.Warehouse.UI  5      Q MDSofT.Framework.UICore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICoreDSofT.Framework.UICore 5
      T PDSofT.Warehouse.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.ResourcesDSofT.Warehouse.Resources 5      T PDSofT.Framework.UIControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControlDSofT.Framework.UIControl 5      [ WDSofT.Framework.Metro.UIControls, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls DSofT.Framework.Metro.UIControls 5      *    $DSofT.Warehouse.UI.frm_ThuocTheoDKBQ   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:DSofT.Warehouse.UI     5      f	 bDevExpress.Xpf.Grid.v14.2.Core, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al
 hDevExpress.Xpf.Grid.v14.2.Extensions, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid 	 
  5      a ]DevExpress.Xpf.Core.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5	      QuicoreDclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICore  5
      XresTextJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.Resources  5      UcntrJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControl  5      c _DevExpress.Xpf.NavBar.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxn4http://schemas.devexpress.com/winfx/2008/xaml/navbar  5      ] YDevExpress.Mvvm.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      iControlsXclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls   5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $T Npack://application:,,,/DSofT.Warehouse.Resources;component/CommonResource.xaml?�6%   %    5      .��5      � 
������   5   
   7  1DSofT.Framework.UIControl.ConverterStringDateTime   ConverterStringDateTime��%   &	          5      6+   5      2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors 5   
   ,  &DevExpress.Mvvm.UI.NotificationService  5      -   5          Name$ notificationService��5   )      ApplicationId$ DxToastTest��6F   &   UseWin8NotificationsIfAvailable .   CustomNotificationTemplate!   CustomNotificationTemplate# [ 5   #      Sound$! Notification_Looping_Alarm8=�5    #   !	   CustomNotificationPosition$	 BottomRight=�5!   #   (
   !CustomNotificationVisibleMaxCount$
 5��6L   !   CustomNotificationDuration$ 0:0:3h�6r   %   PredefinedNotificationTemplate 5   #   ��  �� ElementName$ notificationStyle�� �� Path$ SelectedItem�5!   r    � ColumnDefinitions 5#   
   �� 5$      $
��Auto �6   �� 5%      $
��0.1* �6    � RowDefinitions 5'   
   �� 5(      $
��Auto �6   �� 5)      $
��Auto �6   �� 5*      $
��0.1* �6   .+�5,   
   �� $��20(�6   $��0��6"   $��0��62     
lblThuocBQ#��[ 6?   $
��Auto��6b   $��Center=�6o   6�   �� 5-   
   -   5        $��	txbTieuDe��5-   d   $��30(�6   $��1��6"   $��0��62     lblThuocDKBQ#��[ 6?   $
��Auto��6u   $��Center=�6�   6�   %  DevExpress.Xpf.Grid.GridControl  5/   
   -   5          Name$	 grd��5/   6   ) 	 #DevExpress.Xpf.Grid.DataControlBase   AutoPopulateColumns . $��1��6D   $��1��6Q   $��2��6a      View 50      #  DevExpress.Xpf.Grid.TableView  51      -   5          Name$ grdView��52   +   & 	  DevExpress.Xpf.Grid.DataViewBase   NavigationStyle$
 Cell=�51       &   DevExpress.Xpf.Grid.GridViewBase   ShowGroupPanel .     ShowAutoFilterRow .    SearchPanelFindMode$ 	FindClick=�6g       UseAnimationWhenExpanding .     SearchPanelFindFilter$ Contains=�52   <   54      �� 56   
   -   5         ��Name$ 	brDanhMuc��56      $��1��6$   $��0��61   $��2��6A   $
��Auto��6R     Sdk.Brd.BtnArea#��[ 6_   ._�57      �� -   5         ��Name$ 
lvwDanhMuc��57      $
��Auto��6+   $
��Auto��68   59      5:      5;      5<      