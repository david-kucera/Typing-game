using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<string> dictionaries = new List<string>();
            dictionaries.Add("Slovak");
            dictionaries.Add("English");
            dictionaries.Add("Programmer");
            foreach (var dic in dictionaries)
            {
                Language_ComboBox.Items.Add(dic);
            }
            Language_ComboBox.SelectedIndex = 0;
        }

        private void Start_The_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Play_On_Console_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
