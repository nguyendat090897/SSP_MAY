   M S B A M L   `   `   `  ����Q  MDSofT.Framework.UICore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICoreDSofT.Framework.UICore  5	      T PDSofT.Warehouse.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.ResourcesDSofT.Warehouse.Resources 5
      T PDSofT.Framework.UIControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControlDSofT.Framework.UIControl 5      [ WDSofT.Framework.Metro.UIControls, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls DSofT.Framework.Metro.UIControls 5       DSofT.Warehouse.UI3   -DSofT.Warehouse.UI.frm_phancong_thietbi_popup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      f	 bDevExpress.Xpf.Grid.v14.2.Core, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al
 hDevExpress.Xpf.Grid.v14.2.Extensions, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid 	 
  5      a ]DevExpress.Xpf.Core.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      QuicoreDclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICore  5	      XresTextJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.Resources  5
      UcntrJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControl  5      c _DevExpress.Xpf.NavBar.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxn4http://schemas.devexpress.com/winfx/2008/xaml/navbar  5      ] YDevExpress.Mvvm.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      iControlsXclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls   5      $	��500��5      $	��800��6     � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $T Npack://application:,,,/DSofT.Warehouse.Resources;component/CommonResource.xaml?�6%   %    5      .��5      � 
������   5   
   7  1DSofT.Framework.UIControl.ConverterStringDateTime   ConverterStringDateTime��%   &	          5      6+   5      2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors 5   
   ,  &DevExpress.Mvvm.UI.NotificationService  5      -   5          Name$ notificationService��5   )      ApplicationId$ DxToastTest��6F   &   UseWin8NotificationsIfAvailable .   CustomNotificationTemplate!   CustomNotificationTemplate# [ 5   #      Sound$! Notification_Looping_Alarm8=�5   #   !	   CustomNotificationPosition$	 BottomRight=�5    #   (
   !CustomNotificationVisibleMaxCount$
 5��6L   !   CustomNotificationDuration$ 0:0:3h�6r   %   PredefinedNotificationTemplate 5   #   ��  �� ElementName$ notificationStyle�� �� Path$ SelectedItem�5    r   .+�5"   
   �  � RowDefinitions 5$      �� 5&      $
��auto �6    �� 5(      $
��auto �6    �� 5)      $
��0.1* �6    �� 5*      $
��auto �6    .+�5-      �� -   5         ��Name$ 
BrdBtnWrap��5-      $��0��6)     Sdk.Brd.BtnArea#��[ 66   ._�5.      ��   Sdk.Spnl.CommBtn.Right#��[ 6   .+�5/      �� -   5        $��btnNew��5/   W    
 btnNew
 � Tag# [ 6   $��Right=�6;     Sdk.Btn.Comm#��[ 6e   $��Center=�6�   �� 50      -   5        $��btnSave��50   X     btnSave# [ 6   $��Right=�6<   #��[ 6g   $��Center=�6�   6�   �� 51      -   5        $��btnClose��51   Y     btnClose# [ 6   $��Right=�6=   #��[ 6i   $��Center=�6�   6�   52      53      �� 54      -   5        $ 	nbcSearch��54      $��1��6(   $��2��65   #��[ 6I   ._�55      � -   5         �Name$ Selection_Area��55       56      �� 57      $��25 �6(    � ColumnDefinitions 59      �� 5:      $
��Auto �6+   �� 5;      $
��0.1* �6+   �� 5<      $
��Auto �6+   �� 5=      $
��Auto �6+   �� 5>      $
��Auto �6+   �� 5?      $
��Auto �6+   �� 5@      $
��Auto �6+   .+�5C      �� -   5        $��lblUser��5C   =   $��0��6    $��0��60    # frm_phancong_thietbi_popup_User#��[ 5D      )  #DevExpress.Xpf.Editors.ComboBoxEdit  5E      -	   5        $��cboUser��5E   E   $��1��6'   $��0��67   +  %DevExpress.Xpf.Editors.LookUpEditBase   IncrementalFiltering . '  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable .    FilterCondition$ ContainsX�6�      ImmediatePopup . %  DevExpress.Xpf.Editors.BaseEdit   	EditValue 6T   �� *
UserName+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6�   �� 5G      -
   5        $��lblLoaiThietBi��5G   =   $��2��6    $��0��60    *	 &frm_phancong_thietbi_popup_LoaiThietBi#��[	 5H      	� 5I      -   5        $��rdoPDA��5I   @   $��3��6"   $��0��62   $	��PDA��6N   6\   	� 5J      -   5        $��	rdoXeNang��5J   @   $��4��6"   $��0��62   $��Xe nâng��6Q   6c   5K      5L      �� 5M      -   5        $ nbcgrid��5M      $��2��6&   $��2��63   #��[ 6G   ._�5N      %  DevExpress.Xpf.Grid.GridControl  -   5          Name$	 grd��5N   =   )	 	 #DevExpress.Xpf.Grid.DataControlBase 	  AutoPopulateColumns . $��1��6K   $��0��6X      View 5O      #
  DevExpress.Xpf.Grid.TableView
  5P      -   5         
 Name$ grdView��5Q   +    
  	AutoWidth .  & 	  DevExpress.Xpf.Grid.DataViewBase    NavigationStyle$
  Cell=�5P   ;   &   DevExpress.Xpf.Grid.GridViewBase!   ShowGroupPanel! .  " 
  ShowAutoFilterRow" . #   SearchPanelFindFilter$# Contains=�6�   $   SearchPanelFindMode$$ 	FindClick=�6�    %   UseAnimationWhenExpanding% .  5Q   +   5S      5T      � 5U      -   5        $ Selection_Area_2��5U      $��3��6-    5V      �� 5W      $��25 �6$    5Y      �� 5Z      $
��Auto �6'   �� 5[      $
��0.1* �6'   �� 5\      $
��Auto �6'   �� 5]      $
��0.1* �6'   .+�5`      �� -   5        $��lblDuocPhepSudungTuNgay��5`   <   $��0��6   $��0��6,   $��Center=�6[   �� 6w    3
 /frm_phancong_thietbi_popup_DuocPhepSuDungTuNgay#�� 
 6{   �� 6�     	lblDauSao#�[ 6�   
���  ��6�   6   (  "System.Windows.Controls.DatePicker  5a      -   5        $��
DateTuNGay��5a   .   $��1��6   6B   %  DevExpress.Xpf.Editors.DateEdit  5b      -   5        $��
txtTU_NGAY��5b   P   $��1��6   $��0��6/   %  DevExpress.Xpf.Editors.TextEdit&   MaskType$& DateTime=�6<   '   MaskUseAsDisplayFormat' . (   Mask$( 
dd/MM/yyyy��6%  )   ShowClearButton) .  *   MaskAutoComplete$* Strong=�6P  +   MaskSaveLiteral+ .   6�   �� *	TU_NGAY+, �� 	Converter#, [  - �� ConverterParameter$- 
dd/MM/yyyy��$ TwoWay=�$ PropertyChanged=�6j  �� 5d      -   5        $��
lblDenNgay��5d   <   $��0��6   $��2��6,   $��Center=�6N   �� 6j    & "frm_phancong_thietbi_popup_DenNgay#��  6n   �� 6�   #�[ 6�   
���  ��6�   6�     5e      -   5        $��DateDenNgay��5e   -   $��3��6   6B     5f      -   5        $��txtDEN_NGAY��5f   P   $��3��6   $��0��6/   $& DateTime=�6<   ' . $( 
dd/MM/yyyy��6'  ) .  $* Strong=�6R  + .   6�   �� *
DEN_NGAY+#, [  $- 
dd/MM/yyyy��$ TwoWay=�$ PropertyChanged=�6l  5h      5i      5j      5k      