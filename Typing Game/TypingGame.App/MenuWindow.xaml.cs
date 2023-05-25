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
    public partial class MenuWindow
    {
        private readonly List<Dictionary> _dicts;
        private const string Stats = @"UserData\\Data.csv";
        private new const string Language = @"lang.txt";
        private readonly string _languageCode;
        private int _colorIndex;
        private const string BackgroundColor = @"bcg_color.txt";

        /// <summary>
        /// Constructor of class.
        /// Shows the window and sets up all necessary components.
        /// </summary>
        public MenuWindow()
        {
            InitializeComponent();
            _languageCode = File.ReadAllText(Language);
            
            if (File.ReadAllText(BackgroundColor).Length != 0)
            {
                _colorIndex = Convert.ToInt32(File.ReadAllText(BackgroundColor));
                Change_Background(_colorIndex);
            }

            Change_Language();
            Show();

            // Sets the window icon
            // https://cdn-icons-png.flaticon.com/512/945/945414.png
            var iconUri = new Uri("icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);

            _dicts = new List<Dictionary>
            {
                Dictionary.SLOVAK,
                Dictionary.ENGLISH,
                Dictionary.PROGRAMMER
            };

            // Selects the first to be selected by native.
            LanguageComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets the difficulty selected from radio boxes and saves to field.
        /// </summary>
        /// <returns>Difficulty of the game</returns>
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
            _ = new GameWindow(hra.get_words());
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
                Select(p => new { p.Name, Brush = p.GetValue(null) as Brush }).
                ToArray();
            // Choose random color from brushes and set it as window background
            var rnd = new Random();
            var randomIndex = rnd.NextInt64(brushes.Length);
            File.WriteAllText(BackgroundColor, string.Empty);
            File.WriteAllText(BackgroundColor, randomIndex.ToString());
            Change_Background(randomIndex);
        }

        private void Change_Background(long randomIndex)
        {
            // Get all brush colors
            var brushes = typeof(Brushes).GetProperties().
                Select(p => new { p.Name, Brush = p.GetValue(null) as Brush }).
                ToArray();
            Menu.Background = brushes[randomIndex].Brush;
            _colorIndex = (int)randomIndex;
        }

        private void MI_Reset_background(object sender, RoutedEventArgs e)
        {
            if (_colorIndex == 0)
            {
                switch (_languageCode)
                {
                    case "en":
                        MessageBox.Show("Background is already reset!", "Already reset", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case "sk":
                        MessageBox.Show("Pozadie je už resetované!", "Už resetované", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        break;
                }
                return;
            }
            File.WriteAllText(BackgroundColor, "0");
            Change_Background(0);
        }

        /// <summary>
        /// Method shows overall stats window to player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_Show_stats(object sender, RoutedEventArgs e)
        {
            new StatsWindow();
        }

        /// <summary>
        /// Method shows messagebox containing information about the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_About_game(object sender, RoutedEventArgs e)
        {
            var version = File.ReadAllText(@"version.txt");
            switch (_languageCode)
            {
                case "en":
                    MessageBox.Show("Game version: " + version + "\n"
                                    + "Made by David Kučera in 2023 at FRI UNIZA", "About game", MessageBoxButton.OK, MessageBoxImage.None);
                    return;
                case "sk":
                    MessageBox.Show("Verzia hry: " + version + "\n"
                                    + "Vytvoril David Kučera, 2023, FRI UNIZA", "O hre", MessageBoxButton.OK, MessageBoxImage.None);
                    return;
            }
        }

        /// <summary>
        /// Shows messagebox containing information about how to play the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_How_to_play(object sender, RoutedEventArgs e)
        {
            switch (_languageCode)
            {
                case "en":
                    MessageBox.Show("Choose the dictionary, from which the words will be taken.\n" +
                                    "Next, choose the difficulty of the game, that determines the number of words displayed.\n" +
                                    "Then click on START\n" +
                                    "After starting, retype shown words with as little errors as possible.\n" +
                                    "At the end, your stats will be shown.", "How to play", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case "sk":
                    MessageBox.Show("Vyber si slovník, z ktorého sa zoberú slová.\n" +
                                    "Následne vyber zložitosť hry, ktorá definuje počet slov v hre.\n" +
                                    "Potom klikni na SPUSTI\n" +
                                    "Po spustení, prepíš slová s čo najmenej chybami za najrýchlejší čas.\n" +
                                    "Na konci ti bude zobrazená štatistika.", "Ako hrať", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }

        /// <summary>
        /// Clears the .csv file containing data about player statistics.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_Reset_stats(object sender, RoutedEventArgs e)
        {
            if (File.ReadAllText(Stats).Equals("total_time;number_of_chars;number_of_errors;cpm;wpm\n"))
            {
                switch (_languageCode)
                {
                    case "en":
                        MessageBox.Show("Stats already reset!", "Already reset", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    case "sk":
                        MessageBox.Show("Štatistika už je resetovaná!", "Už resetovaná", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        break;
                }
            }
            File.WriteAllText(Stats, string.Empty);
            const string header = "total_time;number_of_chars;number_of_errors;cpm;wpm\n";
            File.WriteAllText(Stats, header);
        }

        private void Language_English(object sender, RoutedEventArgs e)
        {
            if (_languageCode == "en")
            {
                MessageBox.Show("Language already in use!", "Language in use", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            File.WriteAllText(Language, string.Empty);
            File.WriteAllText(Language, "en");
            var newWindow = new MenuWindow();
            Close();
        }

        private void Language_Slovak(object sender, RoutedEventArgs e)
        {
            if (_languageCode == "sk")
            {
                MessageBox.Show("Jazyk je už zvolený!", "Jazyk zvolený", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            File.WriteAllText(Language, string.Empty);
            File.WriteAllText(Language, "sk");
            var newWindow = new MenuWindow();
            Close();
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
    }
}
