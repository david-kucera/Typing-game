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
                Console.WriteLine(word.Word);   // Prints a word that should be re-typed
                Console.SetCursorPosition(0,1); // Sets cursor to one line under for typing

                bool writtenWord = false;
                char[] chars = word.Chars;
                while (!writtenWord) 
                {
                    for (int i = 0; i < word.Length; i++)   // Checking each char by char
                    {
                        char pressedKey = ReadKey();

                        // Checks if written char matches the char of word
                        if (!pressedKey.Equals(chars[i]))
                        {
                            // TODO subtract 1 HP
                            // TODO red color wrong chars
                            Console.Write("WRONG");
                        }
                        else
                        {
                            // TODO green color already typed chars
                            continue;
                        }    
                    }
                    writtenWord = true; // Jump to another word
                }
                Console.Clear();    // Clear console for another word
            }
            // TOTO Write a summary of game ... CPM, total time, wrong chars, ect.
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

        private void WaitingForStart()
        {
            char keyPressed = ReadKey();
            while (keyPressed != '\r')
            {
                keyPressed = ReadKey();
            }
            Console.Clear();
        }
    }
}