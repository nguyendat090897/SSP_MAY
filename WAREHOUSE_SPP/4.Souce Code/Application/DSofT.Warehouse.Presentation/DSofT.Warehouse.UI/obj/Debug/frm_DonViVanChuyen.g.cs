﻿#pragma checksum "..\..\frm_DonViVanChuyen.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5A7B811BBADF41AE69AD631432B3A2D51DB97574"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DSofT.Framework.Metro.UIControls;
using DSofT.Framework.UIControl;
using DSofT.Framework.UICore;
using DSofT.Warehouse.Resources;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.DataPager;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Editors.Flyout;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Editors.Popups.Calendar;
using DevExpress.Xpf.Editors.RangeControl;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Settings.Extension;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.Xpf.NavBar;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DSofT.Warehouse.UI {
    
    
    /// <summary>
    /// frm_DonViVanChuyen
    /// </summary>
    public partial class frm_DonViVanChuyen : DSofT.Framework.UICore.PopupBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\frm_DonViVanChuyen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Mvvm.UI.NotificationService notificationService;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\frm_DonViVanChuyen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BrdBtnWrap;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\frm_DonViVanChuyen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\frm_DonViVanChuyen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border nbcSearch;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\frm_DonViVanChuyen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Selection_Area;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\frm_DonViVanChuyen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border nbcgrid;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\frm_DonViVanChuyen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridControl grd;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\frm_DonViVanChuyen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.TableView grdView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DSofT.Warehouse.UI;component/frm_donvivanchuyen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frm_DonViVanChuyen.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.notificationService = ((DevExpress.Mvvm.UI.NotificationService)(target));
            return;
            case 2:
            this.BrdBtnWrap = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\frm_DonViVanChuyen.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.nbcSearch = ((System.Windows.Controls.Border)(target));
            return;
            case 5:
            this.Selection_Area = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.nbcgrid = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.grd = ((DevExpress.Xpf.Grid.GridControl)(target));
            return;
            case 8:
            this.grdView = ((DevExpress.Xpf.Grid.TableView)(target));
            
            #line 62 "..\..\frm_DonViVanChuyen.xaml"
            this.grdView.CellValueChanged += new DevExpress.Xpf.Grid.CellValueChangedEventHandler(this.grdView_CellValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

