namespace Typing_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            // English
            //Bank bank = new Bank("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\Typing Game\\english.txt");
            //Slovak
            Bank bank = new("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\Typing Game\\slovak_50.txt");

            Gameplay hra = new(Difficulty.EASY, bank);
        }
    }
}