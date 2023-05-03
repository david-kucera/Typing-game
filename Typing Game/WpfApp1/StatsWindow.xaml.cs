using System;
using System.Windows;
using System.Windows.Media.Imaging;
using TypingGame.UserData;

namespace TypingGame
{
    /// <summary>
    /// Interaction logic for StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        public StatsWindow()
        {
            InitializeComponent();
            // Sets the window icon
            // https://cdn-icons-png.flaticon.com/512/945/945414.png
            Uri iconUri = new Uri("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\WpfApp1\\Files\\icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);
            Show();
            Reader reader = new Reader("D:\\data.csv");
            TextBoxTotalNumberOfChars.Text = reader.GetAverageNumberOfChars().ToString();
            TextBoxNumberOfErrors.Text = reader.GetAverageNumberOfErrors().ToString();
            TextBoxAvgWpm.Text = Math.Round(reader.GetAverageNumberOfWpm(), 2).ToString();
            TextBoxAvgCpm.Text = Math.Round(reader.GetAverageNumberOfCpm()).ToString();
        }
    }
}
