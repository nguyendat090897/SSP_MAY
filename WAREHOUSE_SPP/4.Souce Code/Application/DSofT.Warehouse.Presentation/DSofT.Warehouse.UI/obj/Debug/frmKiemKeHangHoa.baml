   M S B A M L   `   `   `  ����M  IDSofT.Warehouse.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DSofT.Warehouse.UIDSofT.Warehouse.UI  5      Q MDSofT.Framework.UICore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICoreDSofT.Framework.UICore 5
      T PDSofT.Warehouse.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.ResourcesDSofT.Warehouse.Resources 5      T PDSofT.Framework.UIControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControlDSofT.Framework.UIControl 5      [ WDSofT.Framework.Metro.UIControls, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls DSofT.Framework.Metro.UIControls 5      )    #DSofT.Warehouse.UI.frmKiemKeHangHoa   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:DSofT.Warehouse.UI     5      f	 bDevExpress.Xpf.Grid.v14.2.Core, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al
 hDevExpress.Xpf.Grid.v14.2.Extensions, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid 	 
  5      a ]DevExpress.Xpf.Core.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5	      QuicoreDclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICore  5
      XresTextJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.Resources  5      UcntrJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControl  5      c _DevExpress.Xpf.NavBar.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxn4http://schemas.devexpress.com/winfx/2008/xaml/navbar  5      ] YDevExpress.Mvvm.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      iControlsXclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls   5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $T Npack://application:,,,/DSofT.Warehouse.Resources;component/CommonResource.xaml?�6%   %    5      .��5      � 
������   5   
   7  1DSofT.Framework.UIControl.ConverterStringDateTime   ConverterStringDateTime��%   &	          5      6+   5      2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors 5   
   ,  &DevExpress.Mvvm.UI.NotificationService  5      -   5          Name$ notificationService��5   )      ApplicationId$ DxToastTest��6F   &   UseWin8NotificationsIfAvailable .   CustomNotificationTemplate!   CustomNotificationTemplate# [ 5   #      Sound$! Notification_Looping_Alarm8=�5    #   !	   CustomNotificationPosition$	 BottomRight=�5!   #   (
   !CustomNotificationVisibleMaxCount$
 5��6L   !   CustomNotificationDuration$ 0:0:3h�6r   %   PredefinedNotificationTemplate 5   #   ��  �� ElementName$ notificationStyle�� �� Path$ SelectedItem�5!   r    � RowDefinitions 5#   
   �� 5$      $
��Auto �6   �� 5%      $	��450 �6   �� 5&      $
��0.1* �6   .+�5(   
   �� -   5         ��Name$ 
BrdBtnWrap��5(      $��0��6%   $��0��62   $��2��6B   $
��Auto��6V     Sdk.Brd.BtnArea#��[ 6c   ._�5)      �� $
��Auto��6     Sdk.Spnl.CommBtn.Right#��[ 6&   .+�5*      �� -   5        $��
btnRefresh��5*   W     
btnRefresh
 � Tag# [ 6   $��Right=�6;     Sdk.Btn.Comm#��[ 6j   $��Center=�6�   6�   �� 5+      -   5        $��	btnDelete��5+   V     	btnDelete# [ 6   $��Right=�6:   #��[ 6h   $��Center=�6�   6�   �� 5,      -   5        $��btnNew��5,   S    
 btnNew# [ 6   $��Right=�67   #��[ 6b   $��Center=�6�   6�   5-      5.      %  DevExpress.Xpf.Grid.GridControl  5/   
   -   5          Name$	 grd��5/   6   ) 	 #DevExpress.Xpf.Grid.DataControlBase   AutoPopulateColumns . $��1��6D      View 50      #  DevExpress.Xpf.Grid.TableView  51      -   5          Name$ grdView��52   +   & 	  DevExpress.Xpf.Grid.DataViewBase   NavigationStyle$
 Cell=�51       &   DevExpress.Xpf.Grid.GridViewBase   ShowGroupPanel .     ShowAutoFilterRow .    SearchPanelFindMode$ 	FindClick=�6g       UseAnimationWhenExpanding .     SearchPanelFindFilter$ Contains=�52   <   6�   54      � 56   
   $��2��6   .+�57      �� -   5         ��Name$ 	tabDuAnCT��57      $��2��6,   .@�5:      �� -	   5         ��Name$ tabThongTinChung��5:   3   $��Thông tin chung��6   .��5;      �� $
��Left=�6#   $
��Auto��6>   $
��Auto=�6L   $
��Auto=�6q   .��5<      �  5=      �� 5>   "   $��20 �60   �� 5?   "   $��20 �60   �� 5@   "   $��20 �60   �� 5A   "   $��20 �60    � ColumnDefinitions 5C      �� 5D   "   $	��120 �63   �� 5E   "   $	��300 �63   .+�5G      �� -
   5        $��lblKHACHHANG_NCC_ID��5G   E   $��0��6(   $��0��68   �� 6c     lblKHNCC#��  6g   6�   �� 5H      -   5        $��txtKHACHHANG_NCC_ID��5H   E   $��1��6(   $��0��68   ��6`   �� *TEN_KH+�� 5J      -   5        $��
tblSOPHIEU��5J   E   $��0��6(   $��1��68   �� 6Z    +	 'frm_lapphieu_xacnhan_hanghuhong_SoPhieu#�� 	 6^   6�   �� 5K      -   5        $��
txtSOPHIEU��5K   E   $��1��6(   $��1��68   ��6W   �� *	SOPHIEU+�� 5M      -   5        $��tblNGAY_KIEMKE��5M   E   $��0��6(   $��2��68   �� 6]    
 	lblNgayKK#�� 
 6a   6�   �� 5N      -   5        $��txtNGAY_KIEMKE��5N   E   $��1��6(   $��2��68   ��6[   �� *NGAY_KIEMKE+�� 5P      -   5        $��
tblGHI_CHU��5P   E   $��0��6(   $��3��68   �� 6Y     lblLyDo#��  6]   6�   �� 5Q      -   5        $��
txtGHI_CHU��5Q   E   $��1��6(   $��3��68   ��6W   �� *	GHI_CHU+5R      5S      5T      �� 5W      -   5        $ 
tabChiTiet��5W   ,   $��
Chi tiết��6   .��5X      � .+�5Y        -   5        $ 
grdChiTiet��5Y   T    . $
��Auto��6F    5Z        5[   "   -   5        $ grdViewChiTiet��5\   ,      	AutoWidth . $
 Cell=�5[   A    .   .  5\   ,   5^      5_      5`      5b      5c      5e      5f      