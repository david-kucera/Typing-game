namespace TypingGame.Library
{
    public class Gameplay
    {
        private List<DataType> _words = new();
        private int _numberOfWords;
        private readonly Random _rnd = new();
        private readonly Difficulty _difficulty;

        /// <summary>
        /// Constructor of class Gameplay.
        /// Sets up difficulty and words to be retyped for game.
        /// </summary>
        /// <param name="difficulty">Difficulty of the game.</param>
        /// <param name="bank">Bank of words of the game.</param>
        public Gameplay(Difficulty difficulty, Bank bank)
        {
            _difficulty = difficulty;
            SetUp(difficulty); 
            FillWithRandomWords(ref _words, bank);
        }

        public List<DataType> get_words()
        {
            return _words;
        }

        public HealthPoint get_health_points()
        {
            return _difficulty switch
            {
                Difficulty.EASY => HealthPoint.THREE,
                Difficulty.MEDIUM => HealthPoint.TWO,
                Difficulty.HARD => HealthPoint.ONE,
                _ => HealthPoint.THREE
            };
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
                0 => 20,    
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
            for (var i = 0; i < _numberOfWords; i++)
            {
                var randomIndex = _rnd.Next(bank.Words.Count);
                words.Add(bank.Words[randomIndex]);
            }
        }
    }
}