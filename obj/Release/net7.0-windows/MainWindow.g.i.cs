﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "34530064CAC747D58CB1DF6F4682E20ECCADE5BF"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using WpfApp1;


namespace WpfApp1 {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MenuButton;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MinimizeButton;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MaximizeButton;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseButton;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfApp1.BasicCalculator BasicCalculator;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfApp1.ScientificCalculator ScientificCalculator;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup MenuPopup;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChangeThemeButton;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup ThemePopup;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Calc 1.0;V1.0.0.0;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 20 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.TitleBar_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MenuButton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\MainWindow.xaml"
            this.MenuButton.Click += new System.Windows.RoutedEventHandler(this.MenuButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MinimizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\MainWindow.xaml"
            this.MinimizeButton.Click += new System.Windows.RoutedEventHandler(this.MinimizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MaximizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\MainWindow.xaml"
            this.MaximizeButton.Click += new System.Windows.RoutedEventHandler(this.MaximizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CloseButton = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\MainWindow.xaml"
            this.CloseButton.Click += new System.Windows.RoutedEventHandler(this.CloseButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BasicCalculator = ((WpfApp1.BasicCalculator)(target));
            return;
            case 7:
            this.ScientificCalculator = ((WpfApp1.ScientificCalculator)(target));
            return;
            case 8:
            this.MenuPopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 9:
            this.ChangeThemeButton = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\..\MainWindow.xaml"
            this.ChangeThemeButton.Click += new System.Windows.RoutedEventHandler(this.Change_Theme_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 77 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.About_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ThemePopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 12:
            
            #line 85 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LightTheme_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 86 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DarkTheme_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 87 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DarkLavenderTheme_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 88 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.VioletSunriseTheme_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 89 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NorthernLightsTheme_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

