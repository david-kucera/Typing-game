using System.Runtime.InteropServices;

namespace Typing_Game
{
    class ConsoleInterface
    {
        public ConsoleInterface(List<WordDataType> _words, ref HealthPoint hp)
        {
            Intro();
            WaitingForStart();

            foreach (WordDataType word in _words)
            {
                Console.SetCursorPosition(0, 0); // Console start position
                Console.WriteLine(word.Word);   // Prints a word that should be re-typed
                Console.SetCursorPosition(0, 0);

                bool writtenWord = false;
                char[] chars = word.Chars;
                while (!writtenWord) 
                {
                    for (int i = 0; i < word.Length; i++)   // Checking each char by char
                    {
                        char pressedKey = ReadKey();

                        // Checks if written char matches the char of word and changes the color on console
                        if (!pressedKey.Equals(chars[i]))
                        {
                            hp--;
                            if ((int)hp == -1)
                            {
                                EndGame();
                                // TODO end current game
                                break;
                            }
                            UpdateWord(in i, in chars, ConsoleColor.Red);
                        }
                        else
                        {
                            UpdateWord(in i, in chars, ConsoleColor.Green);
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

        private void EndGame()
        {
            Console.Clear();
            Console.WriteLine("YOU LOST!");
        }

        private void UpdateWord(in int i, in char[] chars, ConsoleColor color)
        {
            // Change color of char at index i of word
            Console.SetCursorPosition(i, 0);
            Console.ForegroundColor = color;
            Console.Write(chars[i]);

            // Reset color and cursor back to typing area
            Console.ForegroundColor= ConsoleColor.White;
            Console.SetCursorPosition(i + 1, 0);
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
            Console.WriteLine("If you are ready, press RETURN.");
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