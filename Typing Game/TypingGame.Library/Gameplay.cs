namespace TypingGame.Library
{
    public class Gameplay
    {
        private List<DataType> _words = new();
        private int _numberOfWords;
        private readonly Random _rnd = new();
        private Difficulty _difficulty;

        /// <summary>
        /// Constructor of class Gameplay.
        /// Sets up difficulty and words to be retyped and starts the chosen UI.
        /// </summary>
        /// <param name="difficuty"></param>
        /// <param name="bank"></param>
        /// <param name="UI"></param>
        public Gameplay(Difficulty difficuty, Bank bank)
        {
            SetUp(difficuty); 
            FillWithRandomWords(ref _words, bank);
        }

        public List<DataType> get_words()
        {
            return _words;
        }

        public Difficulty get_difficulty()
        {
            return _difficulty;
        }

        public HealthPoint get_health_points()
        {
            if (_difficulty == Difficulty.EASY)
            {
                return HealthPoint.THREE;
            }

            if (_difficulty == Difficulty.MEDIUM)
            {
                return HealthPoint.TWO;
            }

            if (_difficulty == Difficulty.HARD)
            {
                return HealthPoint.ONE;
            }

            return HealthPoint.THREE;
        }

        /// <summary>
        /// Sets the number of words that will be displayed in a game and number of lives.
        /// Default is set to EASY -> 20 words and 3 lives.
        /// </summary>
        /// <param name="difficuty">Difficulty set by player.</param>
        private void SetUp(Difficulty difficuty)
        {
            _numberOfWords = difficuty switch
            {
                (Difficulty)0 => 20,    
                (Difficulty)1 => 50,    
                (Difficulty)2 => 100,   
                _ => 100,
            };
        }

        /// <summary>
        /// Fills list of words with random words from bank of words.
        /// </summary>
        /// <param name="words">List of words to be retyped in the game.</param>
        /// <param name="bank">Bank of available words.</param>
        private void FillWithRandomWords(ref List<DataType> words, Bank bank)
        {
            for (int i = 0; i < _numberOfWords; i++)
            {
                int randomIndex = _rnd.Next(bank.Words.Count);
                words.Add(bank.Words[randomIndex]);
            }
        }
    }
}