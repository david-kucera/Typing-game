namespace Typing_Game
{
    // Bank for all words available in the game.
    class WordBank
    {
        private List<WordDataType>? _easyWords;
        private List<WordDataType>? _mediumWords;
        private List<WordDataType>? _hardWords;

        // Constructors for each type of input files
        public WordBank(string pathFileEasy, string pathFileMedium, string pathFileHard)
        {
            FillEasyWords(WordLoader.ReadWordsFromFile(pathFileEasy));
            FillMediumWords(WordLoader.ReadWordsFromFile(pathFileMedium));
            FillHardWords(WordLoader.ReadWordsFromFile(pathFileHard));
        }

        public WordBank(string pathFileEasy, string pathFileMedium)
        {
            FillEasyWords(WordLoader.ReadWordsFromFile(pathFileEasy));
            FillMediumWords(WordLoader.ReadWordsFromFile(pathFileMedium));
        }

        public WordBank(string pathFileEasy)
        {
            FillEasyWords(WordLoader.ReadWordsFromFile(pathFileEasy));
        }

        // Fills Lists with Word values
        public void FillEasyWords(List<WordDataType> words) { _easyWords = words; }
        public void FillMediumWords(List<WordDataType> words) { _mediumWords = words; }
        public void FillHardWords(List<WordDataType> words) { _hardWords = words; }

        // Getters
        public List<WordDataType> EasyWords
        {
            get 
            {
                if (_easyWords == null)
                {
                    throw new NullReferenceException("No available data");
                }
                return _easyWords; 
            }
        }

        public List<WordDataType> MediumWords
        {
            get 
            {
                if(_mediumWords == null)
                {
                    throw new NullReferenceException("No available data");
                }
                return _mediumWords; 
            }
        }

        public List<WordDataType> HardWords
        {
            get 
            {
                if(_hardWords == null)
                {
                    throw new NullReferenceException("No available data");
                }
                return _hardWords; 
            }
        }
    }
}
