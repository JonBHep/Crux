#pragma checksum "..\..\..\PortfolioStartWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D65A560EB162F747B72D38336976197922DCEEC4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace Crux {
    
    
    /// <summary>
    /// PortfolioStartWindow
    /// </summary>
    public partial class PortfolioStartWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\PortfolioStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextblockDocumentExported;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\PortfolioStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextblockAccountsCount;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\PortfolioStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextblockServicesCount;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\PortfolioStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextblockOverdue;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\PortfolioStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextblockAccountsOverdueCount;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\PortfolioStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextblockServicesOverdueCount;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\PortfolioStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImagePicture;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Crux;component/portfoliostartwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PortfolioStartWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 7 "..\..\..\PortfolioStartWindow.xaml"
            ((Crux.PortfolioStartWindow)(target)).ContentRendered += new System.EventHandler(this.Window_ContentRendered);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\PortfolioStartWindow.xaml"
            ((Crux.PortfolioStartWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 18 "..\..\..\PortfolioStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonAccounts_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 19 "..\..\..\PortfolioStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonCards_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 20 "..\..\..\PortfolioStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonServices_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 21 "..\..\..\PortfolioStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.GiftsButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 22 "..\..\..\PortfolioStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonDueDates_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 27 "..\..\..\PortfolioStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonDocument_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 28 "..\..\..\PortfolioStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PrintButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.TextblockDocumentExported = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.TextblockAccountsCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.TextblockServicesCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.TextblockOverdue = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.TextblockAccountsOverdueCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.TextblockServicesOverdueCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 15:
            
            #line 40 "..\..\..\PortfolioStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClose_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.ImagePicture = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

