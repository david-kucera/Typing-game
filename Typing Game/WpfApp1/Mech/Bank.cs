using System;
using System.Collections.Generic;

namespace TypingGame.Mech
{
    /// <summary>
    /// "Bank" for all words available in the game.
    /// </summary>
    internal class Bank
    {
        private List<DataType>? _words;

        /// <summary>
        /// Constructor for Bank class, which just fills field _words with data.
        /// </summary>
        /// <param name="path">Path to the .txt file containing words.</param>
        public Bank(string path)
        {
            FillWords(FileReader.ReadWordsFromFile(path));
        }

        /// <summary>
        /// Fills List with Word values in list got from FileReader.
        /// </summary>
        /// <param name="words"></param>
        public void FillWords(List<DataType> words) { _words = words; }

        /// <summary>
        /// Getter of Words
        /// </summary>
        public List<DataType> Words
        {
            get 
            {
                if (_words == null)
                {
                    throw new NullReferenceException("No available data");
                }
                return _words; 
            }
        }
    }
}
