﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TypingGame.Library;


namespace TypingGame.App
{
    /// <summary>
    /// Gameplay takes place here in this window.
    /// </summary>
    public partial class GameWindow
    {
        private List<DataType> _words;
        private State[] _states;
        private int _indexCurrentWord;
        private DateTime _startOfGame;
        private DateTime _endOfGame;
        private int _numberOfErrors;
        private const string ErrorWords = @"UserData\\errors.csv";
        private const string BackgroundColor = @"bcg_color.txt";

        /// <summary>
        /// Constructor of GameWindow.
        /// All necessary inicializations are here.
        /// </summary>
        /// <param name="words"></param>
        public GameWindow(List<DataType> words)
        {
            InitializeComponent();

            if (File.ReadAllText(BackgroundColor).Length != 0)
            {
                var colorIndex = Convert.ToInt32(File.ReadAllText(BackgroundColor));
                Change_Background(colorIndex);
            }

            Show();

            // Sets the window icon
            // https://cdn-icons-png.flaticon.com/512/945/945414.png
            var iconUri = new Uri("icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);

            _words = words;

            // Set state for each word
            _states = new State[words.Count];
            for (var i = 0; i < words.Count; i++)
            {
                _states[i] = State.TBT;
            }

            _states[0] = State.CURRENT;
            ChangeTextBlock();

            TB_Answer.Focus();
            _startOfGame = DateTime.Now;   // Start the timer
        }

        
        /// <summary>
        /// Sets the color of word depending on the state of the word.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void ChangeTextBlock()
        {
            TextBlock_Template.Text = "";
            for (var i = 0; i < _words.Count; i++)
            {
                var word = _words[i].Word;
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
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        /// <summary>
        /// Checks wether the textbox contains space (' ') - that means that the word was typed.
        /// Then checks if it was correctly typed and jumps to another word, with clearing the text box.
        /// If the player reaches the last word of the game, the window closes and shows window with his stats.
        /// </summary>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!TB_Answer.Text.Contains(' ')) return;
            var input = TB_Answer.Text.Remove(TB_Answer.Text.Length - 1, 1);

            if (input.Equals(_words[_indexCurrentWord].Word))
            {
                _states[_indexCurrentWord] = State.CORRECT;
            }
            else
            {
                _states[_indexCurrentWord] = State.INCORRECT;

                var original = _words[_indexCurrentWord].Word;
                var answer = TB_Answer.Text.Remove(TB_Answer.Text.Length - 1, 1);
                var output =  original + ";" + answer + "\n";
                File.AppendAllText(ErrorWords, output);
                Add_Number_of_errors(original, answer);
            }

            _indexCurrentWord++;

            if (_indexCurrentWord == _words.Count)
            { 
                _endOfGame = DateTime.Now;     // End the timer
                var totalTime = _endOfGame - _startOfGame;
                var numberOfChars = GetNumberOfChars(_words) - _numberOfErrors;

                var seconds = totalTime.Seconds;
                var minutes = totalTime.Minutes;
                var hours = totalTime.Hours;

                var totalSeconds = seconds + minutes * 60 + hours * 60 * 60;

                var inminutes = (double)totalSeconds / 60;

                var numberOfWritten = numberOfChars - _numberOfErrors;

                var cpm = numberOfWritten / inminutes;
                var wpm = cpm / 4.7;

                if (numberOfChars <= 0)
                {
                    var win = new StatWindow(totalTime, 0, _numberOfErrors, 0, 0);
                    win.TextBlockLevel.Text = "Why?";
                    Close();
                    return;
                }

                new StatWindow(totalTime, numberOfChars, _numberOfErrors, cpm, wpm);
                Close();
                return;
            }

            _states[_indexCurrentWord] = State.CURRENT;
            TB_Answer.Text = "";
            ChangeTextBlock();
        }

        private void Add_Number_of_errors(string original, string answer)
        {
            var difference = original.Length - answer.Length;

            if (difference == 0) // when both have the same length
            {
                for (var i = 0; i < answer.Length; i++)
                {
                    if (!answer[i].Equals(original[i]))
                    {
                        _numberOfErrors++;
                    }
                }
            }

            if (difference > 0) // when original is longer
            {
                // Firstly check the written part
                for (var i = 0; i < answer.Length; i++)
                {
                    if (!answer[i].Equals(original[i]))
                    {
                        _numberOfErrors++;
                    }
                }

                // Then add non-written chars
                _numberOfErrors += difference;
            }

            if (difference < 0) // when answer is longer
            {
                // Firstly check the written part
                for (var i = 0; i < original.Length; i++)
                {
                    if (!answer[i].Equals(original[i]))
                    {
                        _numberOfErrors++;
                    }
                }

                // Then add redundant chars
                _numberOfErrors += -(difference);
            }
        }

        /// <summary>
        /// Method uses LINQ to sum the number of chars in words list.
        /// </summary>
        /// <param name="words">List of words</param>
        /// <returns>Int value of number of chars in words list.</returns>
        private static int GetNumberOfChars(IEnumerable<DataType> words)
        {
            return words.Sum(word => word.Length);
        }

        /// <summary>
        /// Method changes the window background to index color parameter.
        /// </summary>
        /// <param name="randomIndex"></param>
        private void Change_Background(long randomIndex)
        {
            var brushes = typeof(Brushes).GetProperties().
                Select(p => new { p.Name, Brush = p.GetValue(null) as Brush }).
                ToArray();
            Background = brushes[randomIndex].Brush;
        }
    }
}
