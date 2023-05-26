namespace TypingGame.Library
{
    /// <summary>
    /// Enum containing levels of difficulty of the game - number of words in a game.
    /// </summary>
    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD
    }

    /// <summary>
    /// Enum containing number of lives in CLI.
    /// </summary>
    public enum HealthPoint
    {
        ONE,
        TWO, 
        THREE
    }

    /// <summary>
    /// Enum containing states of word typed in GUI.
    /// </summary>
    public enum State
    {
        CURRENT,
        CORRECT,
        INCORRECT,
        TBT
    }

    /// <summary>
    /// Enum containing names of dictionaries available.
    /// </summary>
    public enum Dictionary
    {
        SLOVAK,
        ENGLISH,
        PROGRAMMER
    }
}