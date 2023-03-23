namespace Typing_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\Typing Game\\english.txt");
            Gameplay hra = new Gameplay(Difficulty.EASY, bank);
        }
    }
}