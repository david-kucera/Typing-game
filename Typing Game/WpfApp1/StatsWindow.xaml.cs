using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TypingGame
{
    /// <summary>
    /// Interaction logic for StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        public StatsWindow(int avgNumberOfChars, int avgNumberOfErrors, double avgCpm, double avgWpm)
        {
            InitializeComponent();
            // Sets the window icon
            // https://cdn-icons-png.flaticon.com/512/945/945414.png
            Uri iconUri = new Uri("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\WpfApp1\\Files\\icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);
            Show();
            TextBoxTotalNumberOfChars.Text = avgNumberOfChars.ToString();
            TextBoxNumberOfErrors.Text = avgNumberOfErrors.ToString();
            TextBoxAvgWpm.Text = Math.Round(avgWpm, 2).ToString();
            TextBoxAvgCpm.Text = Math.Round(avgCpm).ToString();
        }
    }
}
