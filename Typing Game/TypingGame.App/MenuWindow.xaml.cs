using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TypingGame.Library;
using TypingGame.Console;


namespace TypingGame.App
{

    /// <summary>
    /// Menu WPF App - Window for game setup-options, player stats, help, etc.
    /// </summary>
    public partial class MenuWindow : Window
    {
        private readonly List<Dictionary> _dicts;
        private const string Version = "1.3";

        /// <summary>
        /// Constructor of class.
        /// Shows the window and sets up all necessary components.
        /// </summary>
        public MenuWindow()
        {
            InitializeComponent();
            Show();

            // Sets the window icon
            // https://cdn-icons-png.flaticon.com/512/945/945414.png
            var iconUri = new Uri("icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);

            // Paths to text files
            _dicts = new List<Dictionary>
            {
                Dictionary.SLOVAK,
                Dictionary.ENGLISH,
                Dictionary.PROGRAMMER
            };
            // Names of files for diplay in Combo Box
            var dictionaries = new List<string>
            {
                "Slovak",
                "English",
                "Programmer"
            };
            // Filling values into Combo Box
            foreach (var dic in dictionaries)
            {
                LanguageComboBox.Items.Add(dic);
            }
            // Selects the first to be selected by native.
            LanguageComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets the difficulty selected from radio boxes and saves to field.
        /// </summary>
        /// <returns>Difficultyof the game</returns>
        private Difficulty get_difficulty()
        {
            Difficulty value;

            if (EasyRadioBox.IsChecked == true)
            {
                value = Difficulty.EASY;
            }
            else if (MediumRadioBox.IsChecked == true)
            {
                value = Difficulty.MEDIUM;
            }
            else
            {
                value = Difficulty.HARD;
            }

            return value;
        }

        /// <summary>
        /// Method closes the WPF window and starts GUI of game into new WPF window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_The_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            var lang = LanguageComboBox.SelectedIndex;
            Bank bank = new(_dicts[lang]);
            Gameplay hra = new(get_difficulty(), bank); 
            GameWindow gw = new(hra.get_words());
            Close();
        }

        /// <summary>
        /// For console manipulation.
        /// </summary>
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        /// <summary>
        /// Method closes the WPF window and starts the "original" console version of the game.
        /// Allocates the console and sets its window width and height to maximum size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Play_On_Console_Button_Click(object sender, RoutedEventArgs e)
        {
            AllocConsole(); // Allocates console
            System.Console.SetBufferSize(System.Console.LargestWindowWidth, System.Console.LargestWindowHeight);
            System.Console.SetWindowSize(System.Console.LargestWindowWidth, System.Console.LargestWindowHeight);
            Close(); // Closes WPF menu window

            var lang = LanguageComboBox.SelectedIndex;
            Bank bank = new(_dicts[lang]);
            Gameplay gp = new(get_difficulty(), bank); 
            CommandLineInterface cli = new(gp.get_words(), gp.get_health_points()); // Starts a new CLI game
        }

        /// <summary>
        /// Method closes the whole application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Method changes background color of menu window to random color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_Change_Background(object sender, RoutedEventArgs e)
        {
            // Get all brush colors
            var brushes = typeof(Brushes).GetProperties().
                Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).
                ToArray();
            // Choose random color from brushes and set it as window background
            var rnd = new Random();
            Menu.Background = brushes[rnd.NextInt64(brushes.Length)].Brush;
        }

        /// <summary>
        /// Method shows overall stats window to player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_Show_stats(object sender, RoutedEventArgs e)
        {
            var statsWindow = new StatsWindow();
        }

        /// <summary>
        /// Method shows messagebox containing information about the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_About_game(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Game version: " + Version  + "\n"
                            + "Made by David Kučera in 2023 at FRI UNIZA", "About game", MessageBoxButton.OK, MessageBoxImage.None);
        }

        /// <summary>
        /// Shows messagebox containing information about how to play the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_How_to_play(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Choose the dictionary, from which the words will be taken.\n" +
                            "Next, choose the difficulty of the game, that determines the number of words displayed.\n" +
                            "Then click on START\n" +
                            "After starting, retype shown words withou as little errors as possible.\n" +
                            "At the end, your stats will be shown.", "How to play", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Clears the .csv file containing data about player statistics.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_Reset_stats(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("UserData\\Data.csv", string.Empty);
        }
    }
}
