using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TypingGame.Mech;

namespace TypingGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private List<DataType> _words;
        private State[] _states;
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

            // Set state for each word
            _states = new State[words.Count];
            for (int i = 0; i < words.Count; i++)
            {
                _states[i] = State.TBT;
            }

            _states[0] = State.CURRENT;
            ChangeTextBlock();

            TB_Answer.Focus();
        }

        /*
         * Sets the colors depending on the state of the word.
         */
        private void ChangeTextBlock()
        {
            TextBlock_Template.Text = "";
            for (int i = 0; i < _words.Count; i++)
            {
                string word = _words[i].Word;
                switch (_states[i])
                {
                    case State.TBT:
                        TextBlock_Template.Inlines.Add(word);
                        TextBlock_Template.Inlines.Add(" ");
                        break;
                    case State.CURRENT:
                        TextBlock_Template.Inlines.Add(new Run(word) { FontWeight = FontWeights.Bold });
                        TextBlock_Template.Inlines.Add(" ");
                        break;
                    case State.CORRECT:
                        TextBlock_Template.Inlines.Add(new Run(word) { Foreground = Brushes.Green });
                        TextBlock_Template.Inlines.Add(" ");
                        break;
                    case State.INCORRECT:
                        TextBlock_Template.Inlines.Add(new Run(word) { Foreground = Brushes.Red });
                        TextBlock_Template.Inlines.Add(" ");
                        break;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_Answer.Text.Contains(' '))
            {
                string input = TB_Answer.Text.Remove(TB_Answer.Text.Length - 1, 1);

                if (input.Equals(_words[_index_current_word].Word))
                {
                    _states[_index_current_word] = State.CORRECT;
                }
                else
                {
                    _states[_index_current_word] = State.INCORRECT;
                }

                _index_current_word++;

                if (_index_current_word == _words.Count)
                {
                    // TODO end game here, show stats
                    Close();
                }

                _states[_index_current_word] = State.CURRENT;
                TB_Answer.Text = "";
                ChangeTextBlock();
            }
        }
    }
}
