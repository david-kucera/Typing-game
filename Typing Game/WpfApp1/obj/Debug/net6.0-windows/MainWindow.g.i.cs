// Updated by XamlIntelliSenseFileGenerator 26. 4. 2023 11:17:33
#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "683616230F4A322B417E84DD2EEC373E839CAC02"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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


namespace WpfApp1
{


    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 27 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu AboutMenu;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TypingGame;component/mainwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.Language_ComboBox = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 2:
                    this.Easy_radio_box = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 3:
                    this.Medium_radio_box = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 4:
                    this.Hard_radio_box = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 5:
                    this.Start_The_Game_Button = ((System.Windows.Controls.Button)(target));

#line 25 "..\..\..\MainWindow.xaml"
                    this.Start_The_Game_Button.Click += new System.Windows.RoutedEventHandler(this.Start_The_Game_Button_Click);

#line default
#line hidden
                    return;
                case 6:
                    this.Play_On_Console_Button = ((System.Windows.Controls.Button)(target));

#line 26 "..\..\..\MainWindow.xaml"
                    this.Play_On_Console_Button.Click += new System.Windows.RoutedEventHandler(this.Play_On_Console_Button_Click);

#line default
#line hidden
                    return;
                case 7:
                    this.AboutMenu = ((System.Windows.Controls.Menu)(target));
                    return;
                case 8:
                    this.MI_File = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 9:
                    this.MI_Exit = ((System.Windows.Controls.MenuItem)(target));

#line 29 "..\..\..\MainWindow.xaml"
                    this.MI_Exit.Click += new System.Windows.RoutedEventHandler(this.MI_Exit_Click);

#line default
#line hidden
                    return;
                case 10:
                    this.MI_Stats = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 11:
                    this.MI_About = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 12:
                    this.MI_Help = ((System.Windows.Controls.MenuItem)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.ComboBox LanguageComboBox;
        internal System.Windows.Controls.RadioButton EasyRadioBox;
        internal System.Windows.Controls.RadioButton MediumRadioBox;
        internal System.Windows.Controls.RadioButton HardRadioBox;
        internal System.Windows.Controls.Button StartTheGameButton;
        internal System.Windows.Controls.Button PlayOnConsoleButton;
        internal System.Windows.Controls.MenuItem MiFile;
        internal System.Windows.Controls.MenuItem MiExit;
        internal System.Windows.Controls.MenuItem MiStats;
        internal System.Windows.Controls.MenuItem MiAbout;
        internal System.Windows.Controls.MenuItem MiHelp;
    }
}

