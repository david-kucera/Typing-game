namespace Typing_Game
{
    class Program
    {
        static void Main()
        {
            // Programming words
            Bank bank = new("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\Typing Game\\programmer.txt");

            Gameplay hra = new(Difficulty.EASY, bank);
        }
    }
}