namespace TypingGame.Mech
{
    /// <summary>
    /// Enum containing levels of difficulty of the game.
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
}