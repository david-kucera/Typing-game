using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TypingGame.Mech;

namespace TypingGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private List<DataType> _words;
        private int _index_current_word;

        public GameWindow(List<DataType> words, ref HealthPoint hp)
        {
            InitializeComponent();
            Show();

            // Sets the window icon
            // https://cdn-icons-png.flaticon.com/512/945/945414.png
            Uri iconUri = new Uri("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\WpfApp1\\Files\\icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);

            _words = words;

            _index_current_word = 0;
            SetUpTextBlock();
        }

        /*
         * Sets the current word to bold.
         */
        private void SetUpTextBlock()
        {
            string previous = string.Empty;
            for (int i = 0; i < _index_current_word; i++)
            {
                previous += _words[i].Word;
                previous += " ";
            }
            TextBlock_Template.Inlines.Add(previous);
            TextBlock_Template.Inlines.Add(new Run(_words[_index_current_word].Word) { FontWeight = FontWeights.Bold });
            TextBlock_Template.Inlines.Add(" ");

            string next = string.Empty;
            for (int i = _index_current_word + 1; i < _words.Count; i++)
            {
                next += _words[i].Word;
                next += " ";
            }
            TextBlock_Template.Inlines.Add(next);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_Answer.Text.Contains(' '))
            {
                // TODO skontroluj ci sa text rovna template-u
                // TODO ak ano, pokracuj na dalsie slovo, nastav farbu predosleho na zelenu
                // TODO ak nie, pokracuj na dalsie slovo, nastav farbu predosleho na cervenu
                Close();
            }
        }
    }
}
