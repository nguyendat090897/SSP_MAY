   M S B A M L   `   `   `  ����Q  MDSofT.Framework.UICore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICoreDSofT.Framework.UICore  5      T PDSofT.Warehouse.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.ResourcesDSofT.Warehouse.Resources 5	      T PDSofT.Framework.UIControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControlDSofT.Framework.UIControl 5
      [ WDSofT.Framework.Metro.UIControls, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls DSofT.Framework.Metro.UIControls 5       DSofT.Warehouse.UI    DSofT.Warehouse.UI.frmUser   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      a	 ]DevExpress.Xpf.Core.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors 	 5      f
 bDevExpress.Xpf.Grid.v14.2.Core, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Grid.v14.2.Extensions, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid 
   5      UuicoreDclr-namespace:DSofT.Framework.UICore;assembly=DSofT.Framework.UICore 
   5      \resTextJclr-namespace:DSofT.Warehouse.Resources;assembly=DSofT.Warehouse.Resources 
   5	      YcntrJclr-namespace:DSofT.Framework.UIControl;assembly=DSofT.Framework.UIControl 
   5
      c _DevExpress.Xpf.NavBar.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxn4http://schemas.devexpress.com/winfx/2008/xaml/navbar  5      ] YDevExpress.Mvvm.v14.2, Version=14.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm  	 5      iControlsXclr-namespace:DSofT.Framework.Metro.UIControls;assembly=DSofT.Framework.Metro.UIControls  	 5      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   .+�5   
   �� $��4q�6   $��0��6      frmUser_Info#���   6,   .��5      �   5      �� 5      $
��Auto �6$   64   �� 5      $��25 �6$   62   �� 5      $��25 �6$   62    � ColumnDefinitions 5      �� 5      $
��Auto �6'   66   �� 5      $
��0.1* �6'   66   �� 5      $
��Auto �6'   66   �� 5       $
��0.1* �6'   66   .+�5"      �� $��4��6   $��0��61     Sdk.Spnl.CommBtn.Right#��[ 6>   .+�5#      �� -   5        $��btnNew��5#   W    
 btnNew
 � Tag# [ 6   $��Right=�6;     Sdk.Btn.Comm.Large#��[ 6e   6�   �� 5$      -   5        $��	btnDelete��5$   Z     	btnDelete# [ 6   $��Right=�6>   #��[ 6k   6�   �� 5%      -   5        $��btnSave��5%   X     btnSave# [ 6   $��Right=�6<   #��[ 6g   6�   5&      �� 5'      -   5        $��tblTenDangNhap��5'   ,   $��0��6   $��1��6B   �� 6Q     frmUser_UserName#��  6U    �� 6�     	lblDauSao#��  6�   
���  ��6�   6�   % 	 DevExpress.Xpf.Editors.TextEdit  5(      -   5        $��txtTenDangNhap��5(   <   $��1��6   $��1��6/   % 	 DevExpress.Xpf.Editors.BaseEdit   	EditValue 6R   �� *
UserName+6q   �� 5*      -   5        $��
tblMatKhau��5*   ,   $��2��6   $��1��6>   �� 6N     frmUser_Password#��  6R    �� 6�   #��  6�   
���  ��6�   6�   , 	 &DevExpress.Xpf.Editors.PasswordBoxEdit  5+      -   5        $��
txtMatKhau��5+   U      PasswordChar$ *��6&   $��3��68   $��1��6H      DisplayFormatString$  ��6�    6g   �� *
Password+6�   �� 5-      -   5        $��frmUser_FullName��5-   ,   $��0��6   $��2��6D   �� 5.      $�Họ và tên ��6   �� 6.   #��  62   
���  ��6U   6j     5/      -	   5        $��txtTenNguoiSuDung��5/   [   $��1��6   $��2��6/    6<   �� *
FullName+6[   �� 50      $��2��6   $��2��6,   �� 6;   $�Nhóm người dùng��6?   6[   ) 	 #DevExpress.Xpf.Editors.ComboBoxEdit  51      -
   5        $��cboGroup��51   �   $��3��6#   $��2��64      ValidateOnTextInput .  + 	 %DevExpress.Xpf.Editors.LookUpEditBase   IncrementalFiltering .    ImmediatePopup . 	   FilterCondition$	 ContainsX�6�    6B   �� *UserGroupID+6�   52      53      �� 54   
   $��1��6    	 frmUser_Detail#��� 	 6    $
��6.68q�6J   .��55      � .+�56      � -   5        
 �Name$

 Body��56   $   $��2��6   .+�57      %  DevExpress.Xpf.Grid.GridControl  -   5        $��grdMenu��57   D      View 58      #  DevExpress.Xpf.Grid.TableView  59      -   5        $��grdViewMenu��59   ,      	AutoWidth . 6?   5;      5<      5=      5>      5?      5@      