using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;
using TypingGame.UserData;

namespace TypingGame
{
    /// <summary>
    /// Window to show overall stats of player that are stored in a .csv file.
    /// </summary>
    public partial class StatsWindow : Window
    {
        /// <summary>
        /// Constructor that sets up all necessarycomponents and shows the window.
        /// </summary>
        public StatsWindow()
        {
            InitializeComponent();
            // Sets the window icon
            // https://cdn-icons-png.flaticon.com/512/945/945414.png
            Uri iconUri = new Uri("icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);
            Show();
            Reader reader = new Reader("UserData\\Data.csv");
            TextBoxTotalNumberOfChars.Text = reader.GetAverageNumberOfChars().ToString();
            TextBoxNumberOfErrors.Text = reader.GetAverageNumberOfErrors().ToString();
            TextBoxAvgWpm.Text = Math.Round(reader.GetAverageNumberOfWpm(), 2).ToString(CultureInfo.CurrentCulture);
            TextBoxAvgCpm.Text = Math.Round(reader.GetAverageNumberOfCpm()).ToString(CultureInfo.CurrentCulture);
        }
    }
}
