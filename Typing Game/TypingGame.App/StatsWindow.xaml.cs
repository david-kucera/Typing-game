using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TypingGame.App.UserData;

namespace TypingGame.App
{
    /// <summary>
    /// Window to show overall stats of player that are stored in a .csv file.
    /// </summary>
    public partial class StatsWindow
    {
        private new const string Language = @"lang.txt";
        private readonly string _languageCode;
        private const string BackgroundColor = @"bcg_color.txt";

        /// <summary>
        /// Constructor that sets up all necessary components and shows the window.
        /// </summary>
        public StatsWindow()
        {
            InitializeComponent();
            _languageCode = File.ReadAllText(Language);
            Change_Language();
            long colorIndex = Convert.ToInt64(File.ReadAllText(BackgroundColor));
            Change_Background(colorIndex);

            // Sets the window icon
            // https://cdn-icons-png.flaticon.com/512/945/945414.png
            Uri iconUri = new("icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);

            Show();

            Reader reader = new("UserData\\Data.csv");
            TextBoxTotalNumberOfChars.Text = reader.GetAverageNumberOfChars().ToString();
            TextBoxNumberOfErrors.Text = reader.GetAverageNumberOfErrors().ToString();
            TextBoxAvgWpm.Text = Math.Round(reader.GetAverageNumberOfWpm(), 2).ToString(CultureInfo.CurrentCulture);
            TextBoxAvgCpm.Text = Math.Round(reader.GetAverageNumberOfCpm()).ToString(CultureInfo.CurrentCulture);
        }

        private void Change_Language()
        {
            ResourceDictionary dictionary = new();
            dictionary.Source = _languageCode switch
            {
                "sk" => new Uri("..\\LanguageResources.sk.xaml", UriKind.Relative),
                "en" => new Uri("..\\LanguageResources.en.xaml", UriKind.Relative),
                _ => new Uri("..\\LanguageResources.en.xaml", UriKind.Relative)
            };
            Resources.MergedDictionaries.Add(dictionary);
        }

        private void Change_Background(long randomIndex)
        {
            var brushes = typeof(Brushes).GetProperties().
                Select(p => new { p.Name, Brush = p.GetValue(null) as Brush }).
                ToArray();
            Background = brushes[randomIndex].Brush;
        }
    }
}
