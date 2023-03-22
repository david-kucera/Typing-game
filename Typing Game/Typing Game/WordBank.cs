namespace Typing_Game
{
    // Bank for all words available in the game.
    class WordBank
    {
        private List<WordDataType>? _words;


        public WordBank(string pathFileEasy)
        {
            FillEasyWords(WordLoader.ReadWordsFromFile(pathFileEasy));
        }

        // Fills List with Word values
        public void FillEasyWords(List<WordDataType> words) { _words = words; }

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
