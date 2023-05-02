using System;
using System.Collections.Generic;
using System.Linq;
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
        private int _indexCurrentWord;
        private DateTime _startOfGame;
        private DateTime _endOfGame;
        private int _numberOfErrors;

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
            _startOfGame = DateTime.Now;   // Start the timer
        }

        /*
         * Sets the color of word depending on the state of the word.
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

        /*
         * Checks wether the textbox contains space (' ') - that means that the word was typed.
         * Then checks if it was correctly typed and jumps to another word, with clearing the text box.
         * If the player reaches the last word of the game, the window closes and shows window with his stats.
         */
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_Answer.Text.Contains(' '))
            {
                string input = TB_Answer.Text.Remove(TB_Answer.Text.Length - 1, 1);

                if (input.Equals(_words[_indexCurrentWord].Word))
                {
                    _states[_indexCurrentWord] = State.CORRECT;
                }
                else
                {
                    _states[_indexCurrentWord] = State.INCORRECT;
                    _numberOfErrors++;
                }

                _indexCurrentWord++;

                if (_indexCurrentWord == _words.Count)
                { 
                    _endOfGame = DateTime.Now;     // End the timer
                    var totalTime = _endOfGame - _startOfGame;
                    var numberOfChars = GetNumberOfChars(_words);

                    var seconds = totalTime.Seconds;
                    var minutes = (double)seconds / 60;

                    var numberOfWritten = numberOfChars - _numberOfErrors;

                    var cpm = numberOfWritten / minutes;
                    var wpm = (double)(numberOfWritten / 4.7) / minutes;

                    StatWindow statWindow = new StatWindow(totalTime, numberOfChars, _numberOfErrors, cpm, wpm);
                    Close();
                    return;
                }

                _states[_indexCurrentWord] = State.CURRENT;
                TB_Answer.Text = "";
                ChangeTextBlock();
            }
        }

        /*
         * Method uses LINQ to sum the number of chars in words list.
         */
        private static int GetNumberOfChars(List<DataType> words)
        {
            return words.Sum(word => word.Length);
        }
    }
}
