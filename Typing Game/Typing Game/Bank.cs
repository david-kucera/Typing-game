namespace Typing_Game
{
    // Bank for all words available in the game.
    class Bank
    {
        private List<DataType>? _words;


        public Bank(string pathFileEasy)
        {
            FillWords(FileReader.ReadWordsFromFile(pathFileEasy));
        }

        // Fills List with Word values
        public void FillWords(List<DataType> words) { _words = words; }

        // Getter
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
