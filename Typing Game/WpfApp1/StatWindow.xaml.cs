using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TypingGame
{
    /// <summary>
    /// Window of a current game stats.
    /// </summary>
    public partial class StatWindow : Window
    {
        private readonly TimeSpan _totalTime;
        private readonly int _numberOfChars;
        private readonly int _numberOfErrors;
        private readonly double _cpm;
        private readonly double _wpm;

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
            // Sets the window icon
            // https://cdn-icons-png.flaticon.com/512/945/945414.png
            Uri iconUri = new Uri("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\WpfApp1\\Files\\icon.ico", UriKind.RelativeOrAbsolute);
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
            TextBoxAvgWpm.Text = Math.Round(_wpm, 2).ToString();
            TextBoxAvgCpm.Text = Math.Round(_cpm).ToString();
        }

        /// <summary>
        /// Method appends information into .csv file.
        /// </summary>
        private void SaveToCsv()
        {
            //
            // TODO work out how to output to file in this project
            //

            const string file = "D:\\data.csv";
            var output = new StringBuilder();
            const string separator = ";";
            String[] newLine =
            {
                _totalTime.ToString(), _numberOfChars.ToString(), _numberOfErrors.ToString(), Math.Round(_wpm, 2).ToString(), Math.Round(_cpm).ToString()
            };
            output.AppendLine(string.Join(separator, newLine));
            try
            {
                File.AppendAllText(file, output.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Error while saving data to csv file.", "Failed to save to CSV file" , MessageBoxButton.OK , MessageBoxImage.Error);
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
            MenuWindow mw = new MenuWindow();
            Close();
        }

        /// <summary>
        /// Button to show overall stats.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowStatsButtonClick(object sender, RoutedEventArgs e)
        {
            StatsWindow sw = new StatsWindow();
        }
    }
}
