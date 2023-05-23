namespace TypingGame.Library
{
    /// <summary>
    /// Represents a word in game.
    /// </summary>
    public class DataType
    {
        public string Word => _word;
        public int Length => _length;
        public char[] Chars => _chars;

        private readonly string _word;
        private readonly int _length;
        private readonly char[] _chars;

        /// <summary>
        /// Constructor, which fills each field with values according to the word parameter.
        /// </summary>
        /// <param name="word"></param>
        public DataType(string word)
        {
            _word = word;
            _length = word.Length;
            _chars = new char[word.Length];
            FillChars();
        }

        /// <summary>
        /// Fills field _chars with individual characters of word
        /// </summary>
        private void FillChars()
        {
            for (int i = 0; i < _length; i++)
            {
                _chars[i] = _word[i];
            }
        }

        
    }
}
