#pragma checksum "..\..\..\MotsStartWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DAF984804E3CF753AB611390C1E6AE47749DFD21"
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
    /// MotsStartWindow
    /// </summary>
    public partial class MotsStartWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\MotsStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextboxFilter;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\MotsStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonClear;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\MotsStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextblockCount;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\MotsStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl PasswordTabControl;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\MotsStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox FavouriteListBox;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\MotsStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox RecentListBox;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\MotsStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox EntireListBox;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\MotsStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonView;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\MotsStartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonDelete;
        
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
            System.Uri resourceLocater = new System.Uri("/Crux;component/motsstartwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MotsStartWindow.xaml"
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
            
            #line 7 "..\..\..\MotsStartWindow.xaml"
            ((Crux.MotsStartWindow)(target)).ContentRendered += new System.EventHandler(this.Window_ContentRendered);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\MotsStartWindow.xaml"
            ((Crux.MotsStartWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\MotsStartWindow.xaml"
            ((Crux.MotsStartWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TextboxFilter = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\..\MotsStartWindow.xaml"
            this.TextboxFilter.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextboxFilter_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonClear = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\MotsStartWindow.xaml"
            this.ButtonClear.Click += new System.Windows.RoutedEventHandler(this.ButtonClear_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TextblockCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.PasswordTabControl = ((System.Windows.Controls.TabControl)(target));
            return;
            case 6:
            this.FavouriteListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 37 "..\..\..\MotsStartWindow.xaml"
            this.FavouriteListBox.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListBox_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 37 "..\..\..\MotsStartWindow.xaml"
            this.FavouriteListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.RecentListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 52 "..\..\..\MotsStartWindow.xaml"
            this.RecentListBox.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListBox_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 52 "..\..\..\MotsStartWindow.xaml"
            this.RecentListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 68 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 69 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 70 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 71 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 72 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 73 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 74 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 75 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 76 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 77 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 78 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 79 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 80 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 81 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 82 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 83 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 84 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 85 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 26:
            
            #line 86 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 87 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 88 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 29:
            
            #line 89 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 90 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 31:
            
            #line 91 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 32:
            
            #line 92 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 93 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 34:
            
            #line 94 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LetterButton_Click);
            
            #line default
            #line hidden
            return;
            case 35:
            this.EntireListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 102 "..\..\..\MotsStartWindow.xaml"
            this.EntireListBox.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListBox_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 102 "..\..\..\MotsStartWindow.xaml"
            this.EntireListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 36:
            this.ButtonView = ((System.Windows.Controls.Button)(target));
            
            #line 109 "..\..\..\MotsStartWindow.xaml"
            this.ButtonView.Click += new System.Windows.RoutedEventHandler(this.ButtonView_Click);
            
            #line default
            #line hidden
            return;
            case 37:
            
            #line 110 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonAdd_Click);
            
            #line default
            #line hidden
            return;
            case 38:
            this.ButtonDelete = ((System.Windows.Controls.Button)(target));
            
            #line 111 "..\..\..\MotsStartWindow.xaml"
            this.ButtonDelete.Click += new System.Windows.RoutedEventHandler(this.ButtonDelete_Click);
            
            #line default
            #line hidden
            return;
            case 39:
            
            #line 115 "..\..\..\MotsStartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClose_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

