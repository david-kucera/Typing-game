namespace Typing_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            WordBank bank = new WordBank("C:\\Users\\kucer\\OneDrive\\FRI\\2leto\\Jazyk C# a .NET\\_workspace\\semestralna_praca\\typing-game\\Typing Game\\Typing Game\\easy.txt");
            List<WordDataType> list = bank.EasyWords;
            foreach (WordDataType word in list)
            {
                Console.WriteLine(word.Word);
            }
        }
    }
}