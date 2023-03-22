namespace Typing_Game
{
    class ConsoleInterface
    {
        public ConsoleInterface(List<WordDataType> _words) 
        {
            Console.WriteLine("Welcome to typing game!");
            Console.WriteLine("Characters will appear on your screen, try to re-write them as fast as you can.");
            Console.WriteLine("If you type incorrect character, don't worry, you'll have 3 lives.");
            Console.WriteLine("- just continue like nothing happened");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("If you are ready, press any key.");

            
        }
    }
}