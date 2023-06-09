﻿namespace TypingGame.Library
{
    /// <summary>
    /// "Bank" for all words available in the game.
    /// </summary>
    public class Bank
    {
        private List<DataType>? _words;

        /// <summary>
        /// Constructor for Bank class, which just fills field _words with data.
        /// </summary>
        /// <param name="dictionary">File from wich the reader will read</param>
        public Bank(Dictionary dictionary)
        {
            var path = dictionary switch
            {
                Dictionary.ENGLISH => @"Files\\english.txt",
                Dictionary.PROGRAMMER => @"Files\\programmer.txt",
                Dictionary.SLOVAK => @"Files\\slovak.txt",
                _ => @"Files\\slovak.txt"
            };
            FillWords(FileReader.ReadWordsFromFile(path));
        }

        /// <summary>
        /// Fills List with Word values in list got from FileReader.
        /// </summary>
        /// <param name="words"></param>
        public void FillWords(List<DataType> words) { _words = words; }

        /// <summary>
        /// Getter of Words.
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
