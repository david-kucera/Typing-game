using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TypingGame.App
{
    /// <summary>
    /// Window of a current game stats.
    /// </summary>
    public partial class StatWindow
    {
        private readonly TimeSpan _totalTime;
        private readonly int _numberOfChars;
        private readonly int _numberOfErrors;
        private readonly double _cpm;
        private readonly double _wpm;
        private new const string Language = @"lang.txt";
        private readonly string _languageCode;
        private const string BackgroundColor = @"bcg_color.txt";

        /// <summary>
        /// Constructor of StatWindow class.
        /// Shows basic information about the previous game.
        /// </summary>
        /// <param name="totalTime">Total time spent typing.</param>
        /// <param name="numberOfChars">Number of chars typed.</param>
        /// <param name="numberOfErrors">Number of errors.</param>
        /// <param name="cpm">Characters per minute.</param>
        /// <param name="wpm">Words per minute.</param>
        public StatWindow(TimeSpan totalTime, int numberOfChars, int numberOfErrors, double cpm, double wpm)
        {
            InitializeComponent();
            _languageCode = File.ReadAllText(Language);
            Change_Language();
            long colorIndex = Convert.ToInt64(File.ReadAllText(BackgroundColor));
            Change_Background(colorIndex);

            // Sets the window icon
            // https://cdn-icons-png.flaticon.com/512/945/945414.png
            Uri iconUri = new Uri("icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);

            Show();
            _totalTime = totalTime;
            _numberOfChars = numberOfChars;
            _numberOfErrors = numberOfErrors;
            _cpm = cpm;
            _wpm = wpm;
            WriteIntoTextBoxes();
            WriteIntoTextBlock();
            SaveToCsv();
        }

        /// <summary>
        /// Method writes information to correct textboxes.
        /// </summary>
        private void WriteIntoTextBoxes()
        {
            TextBoxTotalTimeOfTyping.Text = _totalTime.ToString();
            TextBoxTotalNumberOfChars.Text = _numberOfChars.ToString();
            TextBoxNumberOfErrors.Text = _numberOfErrors.ToString();
            TextBoxAvgWpm.Text = Math.Round(_wpm, 2).ToString(CultureInfo.CurrentCulture);
            TextBoxAvgCpm.Text = Math.Round(_cpm).ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Method appends information into .csv file.
        /// </summary>
        private void SaveToCsv()
        {
            const string file = "UserData\\Data.csv";
            var output = new StringBuilder();
            const string separator = ";";
            string[] newLine =
            {
                _totalTime.ToString(), _numberOfChars.ToString(), _numberOfErrors.ToString(), Math.Round(_wpm, 2).ToString(CultureInfo.CurrentCulture), Math.Round(_cpm).ToString(CultureInfo.CurrentCulture)
            };
            output.AppendLine(string.Join(separator, newLine));
            try
            {
                File.AppendAllText(file, output.ToString());
            }
            catch (IOException)
            {
                MessageBox.Show("Error while saving data to csv file.", "Failed to save to CSV file" , MessageBoxButton.OK , MessageBoxImage.Error);
                Close();
            }
        }

        /// <summary>
        /// Method writes level of typist into textblock located in the bottom of window.
        /// </summary>
        private void WriteIntoTextBlock()
        {
            var level = "";
            if (_wpm <= 10) level = "Equivalent to one word every 6 seconds. Learn the proper typing technique and practice to improve your speed.";
            else if (_wpm is > 0 and <= 10) level = "Equivalent to one word every 3 seconds. Focus on your technique and keep practising.";
            else if (_wpm is > 10 and <= 20) level = "Better, but still below average. Keep practising to improve your speed and accuracy.";
            else if (_wpm is > 20 and <= 30) level = "At 41 wpm, you are now an average typist. You still have significant room for improvement.";
            else if (_wpm is > 30 and <= 40) level = "Congratulations! You're above average!";
            else if (_wpm is > 40 and <= 50) level = "This is the speed required for most jobs. You can now be a professional typist.";
            else if (_wpm is > 50 and <= 60) level = "You are way above average and would qualify for any typing job, assuming your accuracy is high enough.";
            else if (_wpm is > 60 and <= 70) level = "You're a catch! Any employer looking for a typist would love to have you!";
            else if (_wpm is > 70 and <= 80) level = "At this speed, you're probably a gamer, coder or genius. You're doing great!";
            else if (_wpm > 80) level = "You're in the top 1% of typists! Congratulations!";
            TextBlockLevel.Text = level;
        }

        /// <summary>
        /// Button for playing again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayAgainButtonClick(object sender, RoutedEventArgs e)
        {
            var mw = new MenuWindow();
            Close();
        }

        /// <summary>
        /// Button to show overall stats.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowStatsButtonClick(object sender, RoutedEventArgs e)
        {
            var sw = new StatsWindow();
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
