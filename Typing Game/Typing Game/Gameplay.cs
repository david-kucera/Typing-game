namespace Typing_Game
{
    class Gameplay
    {
        private List<WordDataType> _words;
        private int number_of_words;
        private Random rnd = new();
        public Gameplay(Difficulty difficuty, WordBank bank)
        {
            SetNumberOfWords(difficuty);
            SetRandomWords(difficuty, bank);
            PrintWords();
        }

        private void PrintWords()
        {
            for (int i = 0; i < number_of_words; i++)
            {
                Console.Write(_words[i].Word + " ");
            }
        }

        // Sets the number of words that will be displayed in a game.
        // Default is set to EASY.
        private void SetNumberOfWords(Difficulty difficuty)
        {
            switch (difficuty)
            {
                case (Difficulty)0:
                    number_of_words = 20;
                    break;
                case (Difficulty)1:
                    number_of_words = 50;
                    break;
                case (Difficulty)2:
                    number_of_words = 100;
                    break;
                default:    
                    number_of_words = 20;
                    break;
            }
        }

        // Fills list of words with random words from bank of words.
        private void SetRandomWords(Difficulty difficuty, WordBank bank)
        {
            _words = new List<WordDataType>();
            for (int i = 0; i < number_of_words; i++)
            {
                int randomIndex = rnd.Next(bank.EasyWords.Count);
                _words.Add(bank.EasyWords[randomIndex]);
            }
        }
    }
}