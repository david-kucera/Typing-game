namespace TypingGame.Library
{
    /// <summary>
    /// Represents a word in game.
    /// </summary>
    public class DataType
    {
        public string Word { get; }

        public int Length { get; }

        public char[] Chars { get; }

        /// <summary>
        /// Constructor, which fills each field with values according to the word parameter.
        /// </summary>
        /// <param name="word">Word in the game.</param>
        public DataType(string word)
        {
            Word = word;
            Length = word.Length;
            Chars = new char[word.Length];
            FillChars();
        }

        /// <summary>
        /// Fills field Chars with individual characters of word.
        /// </summary>
        private void FillChars()
        {
            for (var i = 0; i < Length; i++)
            {
                Chars[i] = Word[i];
            }
        }

        
    }
}
