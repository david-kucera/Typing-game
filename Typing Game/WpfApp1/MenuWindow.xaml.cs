using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TypingGame.Mech;


namespace TypingGame
{

    /// <summary>
    /// Menu WPF App - Window for game setup-options, player stats, help, etc.
    /// </summary>
    public partial class MenuWindow : Window
    {
        private Difficulty _difficulty;
        private readonly List<string> _paths;
        public MenuWindow()
        {
            InitializeComponent();

            // Sets the window icon
            // https://cdn-icons-png.flaticon.com/512/945/945414.png
            Uri iconUri = new Uri("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\WpfApp1\\Files\\icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);

            // Paths to text files
            _paths = new List<string>
            {
                "C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\WpfApp1\\Files\\slovak_words_generated.txt",
                "C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\WpfApp1\\Files\\english.txt",
                "C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\WpfApp1\\Files\\programmer.txt"
            };
            // Names of files for diplay in Combo Box
            List<string> dictionaries = new List<string>
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

        /*
         * Gets the difficulty selected from radio boxes and saves to field.
         */
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

        /*
         * For console manipulation.
         */
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        /*
         * Method closes the WPH window and starts GUI of game into new WPF window.
         */
        private void Start_The_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            var lang = LanguageComboBox.SelectedIndex;
            Bank bank = new(_paths[lang]);
            _difficulty = get_difficulty();
            Gameplay hra = new(_difficulty, bank, "gui"); // Starts a new GUI game
            Close();
        }

        /*
         * Method closes the WPF window and starts the "original" console version of the game.
         * Allocates the console and sets its window width and height to maximum size.
         */
        private void Play_On_Console_Button_Click(object sender, RoutedEventArgs e)
        {
            AllocConsole(); // Allocates console
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowSize(Console.LargestWindowWidth,Console.LargestWindowHeight);
            Close(); // Closes WPF menu window

            var lang = LanguageComboBox.SelectedIndex;
            Bank bank = new(_paths[lang]);
            _difficulty = get_difficulty();
            Gameplay hra = new(_difficulty, bank, "cli"); // Starts a new CLI game
        }

        /*
         * Method closes the whole application.
         */
        private void MI_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /*
         * Method changes background color of menu WPF.
         */
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
    }
}
