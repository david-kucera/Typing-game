namespace Typing_Game
{
    // Bank for all words available in the game.
    class WordBank
    {
        private List<WordDataType>? _words;


        public WordBank(string pathFileEasy)
        {
            FillWords(WordLoader.ReadWordsFromFile(pathFileEasy));
        }

        // Fills List with Word values
        public void FillWords(List<WordDataType> words) { _words = words; }

        // Getter
        public List<WordDataType> Words
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
