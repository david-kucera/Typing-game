using System.Runtime.InteropServices;

namespace Typing_Game
{
    class ConsoleInterface
    {
        public ConsoleInterface(List<WordDataType> _words)
        {
            Intro();
            WaitingForStart();

            // TODO START PROGRAM HERE
            foreach (WordDataType word in _words)
            {
                Console.SetCursorPosition(0, 0); // Console start position
                Console.WriteLine(word.Word);
                Console.SetCursorPosition(0,1); // One line under

                bool written = false;
                char[] chars = word.Chars;
                while (!written) 
                {
                    for (int i = 0; i < word.Length; i++)   // Checking each char by char
                    {
                        //char pressedKey = Console.ReadKey().KeyChar; // Char pressed
                        char pressedKey = ReadKey();

                        // Checks if written char matches the char of word
                        if (!pressedKey.Equals(chars[i]))
                        {
                            // TODO subtract 1 HP
                            // TODO red color
                            Console.WriteLine("WRONG");
                        }
                        else
                        {
                            // TODO green color already typed chars
                            Console.WriteLine("DOBRE");
                            continue;
                        }    
                    }
                    written = true; // Jump to another word
                }
                Console.WriteLine("CONGRATS END of word");
                Console.Clear();
            }

            Console.WriteLine("CONGRATS END of words");
        }

        private char ReadKey()
        {
            bool keyPressed = false;
            char charInput = '.';
            while (!keyPressed)
            {
                if (Console.KeyAvailable)
                {
                    charInput = Console.ReadKey().KeyChar;
                    keyPressed = true;
                }
            }
            return charInput;
        }

        private static void Intro()
        {
            Console.WriteLine("Welcome to typing game!");
            Console.WriteLine("Characters will appear on your screen, try to re-write them as fast as you can.");
            Console.WriteLine("If you type incorrect character, don't worry, you'll have total of 3 lives.");
            Console.WriteLine("- just continue like nothing happened");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("If you are ready, press any key.");
        }

        private static void WaitingForStart()
        {
            bool keyPressed = false;
            while (!keyPressed)
            {
                if (Console.KeyAvailable)
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                    keyPressed = true;
                }
            }
        }
    }
}