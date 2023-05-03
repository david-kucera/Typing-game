using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TypingGame
{
    /// <summary>
    /// Interaction logic for StatWindow.xaml
    /// </summary>
    public partial class StatWindow : Window
    {
        public StatWindow(TimeSpan totalTime, int numberOfChars, int numberOfErrors, double cpm, double wpm)
        {
            InitializeComponent();
            // Sets the window icon
            // https://cdn-icons-png.flaticon.com/512/945/945414.png
            Uri iconUri = new Uri("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\WpfApp1\\Files\\icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);
            Show();
            WriteIntoTextBoxes(totalTime, numberOfChars, numberOfErrors, cpm, wpm);
            WriteIntoTextBlock(wpm);
            SaveToCsv(totalTime, numberOfChars, numberOfErrors, cpm, wpm);
        }

        private void WriteIntoTextBoxes(TimeSpan totalTime, int numberOfChars, int numberOfErrors, double cpm, double wpm)
        {
            TextBoxTotalTimeOfTyping.Text = totalTime.ToString();
            TextBoxTotalNumberOfChars.Text = numberOfChars.ToString();
            TextBoxNumberOfErrors.Text = numberOfErrors.ToString();
            TextBoxAvgWpm.Text = Math.Round(wpm, 2).ToString();
            TextBoxAvgCpm.Text = Math.Round(cpm).ToString();
        }

        private static void SaveToCsv(TimeSpan totalTime, int numberOfChars, int numberOfErrors, double cpm, double wpm)
        {
            // TODO work out how to output to file in this project
            const string file = "D:\\data.csv";
            var output = new StringBuilder();
            const string separator = ";";
            String[] newLine =
            {
                totalTime.ToString(), numberOfChars.ToString(), numberOfErrors.ToString(), Math.Round(wpm, 2).ToString(), Math.Round(cpm).ToString()
            };
            output.AppendLine(string.Join(separator, newLine));
            try
            {
                File.AppendAllText(file, output.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while saving data to csv file.", "Failed to save to CSV file" , MessageBoxButton.OK , MessageBoxImage.Error);
            }
        }

        private void WriteIntoTextBlock(double wpm)
        {
            var level = "";
            if (wpm <= 10) level = "Equivalent to one word every 6 seconds. Learn the proper typing technique and practice to improve your speed.";
            else if (wpm > 0 && wpm <= 10) level = "Equivalent to one word every 3 seconds. Focus on your technique and keep practising.";
            else if (wpm > 10 && wpm <= 20) level = "Better, but still below average. Keep practising to improve your speed and accuracy.";
            else if (wpm > 20 && wpm <= 30) level = "At 41 wpm, you are now an average typist. You still have significant room for improvement.";
            else if (wpm > 30 && wpm <= 40) level = "Congratulations! You're above average!";
            else if (wpm > 40 && wpm <= 50) level = "This is the speed required for most jobs. You can now be a professional typist.";
            else if (wpm > 50 && wpm <= 60) level = "You are way above average and would qualify for any typing job, assuming your accuracy is high enough.";
            else if (wpm > 60 && wpm <= 70) level = "You're a catch! Any employer looking for a typist would love to have you!";
            else if (wpm > 70 && wpm <= 80) level = "At this speed, you're probably a gamer, coder or genius. You're doing great!";
            else if (wpm > 80) level = "You're in the top 1% of typists! Congratulations!";
            TextBlockLevel.Text = level;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow mw = new MenuWindow();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StatsWindow sw = new StatsWindow();
        }
    }
}
