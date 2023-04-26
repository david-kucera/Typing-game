namespace TypingGame.Mech
{
    // Represents a word in game.
    public class DataType
    {
        private readonly string _word;
        private readonly int _length;
        private readonly char[] _chars;

        public DataType(string word)
        {
            _word = word;
            _length = word.Length;
            _chars = new char[word.Length];
            FillChars();
        }

        private void FillChars()
        {
            for (int i = 0; i < _length; i++)
            {
                _chars[i] = _word[i];
            }
        }

        public string Word
        {
            get { return _word; }
        }

        public int Length
        {
            get { return _length; }
        }

        public char[] Chars 
        {
            get { return _chars; } 
        }
    }
}
