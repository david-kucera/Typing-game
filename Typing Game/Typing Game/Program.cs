namespace Typing_Game
{
    class Program
    {
        static void Main()
        {
            // English
            //Bank bank = new Bank("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\Typing Game\\english.txt");
            // Slovak 50 k dictionary
            //Bank bank = new("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\Typing Game\\slovak_50.txt");
            // Slovak full dict.
            //Bank bank = new("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\Typing Game\\slovak.txt");

            // Programming words
            Bank bank = new("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\Typing Game\\programmer.txt");

            Gameplay hra = new(Difficulty.EASY, bank);
        }
    }
}