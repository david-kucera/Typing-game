using System.Data.SqlTypes;
using System.Runtime.InteropServices;

namespace Typing_Game
{
    class CommandLineInterface
    {
        private int startingPositionX = Console.WindowWidth / 2;
        private int startingPositionY = Console.WindowHeight / 2;
        public CommandLineInterface(List<WordDataType> _words, ref HealthPoint hp)
        {
            Intro();
            ReloadFieldsConsole();
            WaitingForStart();
            DateTime start_of_game = DateTime.Now;   // Start the timer

            foreach (WordDataType word in _words)
            {
                int posX = startingPositionX - (word.Length/2);
                int posY = startingPositionY;
                Console.SetCursorPosition(posX, posY);      // Console start position
                Console.WriteLine(word.Word);               // Prints a word that should be re-typed
                Console.SetCursorPosition(posX, posY);      // Sets cursor to the start of word

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
                            UpdateWord(in i, in chars, ConsoleColor.Red, in posX, in posY);
                        }
                        else
                        {
                            UpdateWord(in i, in chars, ConsoleColor.Green, in posX, in posY);
                            continue;
                        }    
                    }
                    writtenWord = true; // Jump to another word
                    
                }
                Console.Clear();    // Clear console for another word
            }

            DateTime end_of_game = DateTime.Now;     // End the timer
            var total_time_of_game = end_of_game - start_of_game;



            WriteSummary(total_time_of_game, GetNumberOfChars(_words));
        }

        private int GetNumberOfChars(List<WordDataType> words)
        {
            int value = 0;
            foreach (WordDataType word in words)
            {
                value += word.Length;
            }
            return value;
        }

        // Reloads field for console dimensions when user changes console window size.
        private void ReloadFieldsConsole()
        {
            startingPositionX = Console.WindowWidth / 2;
            startingPositionY = Console.WindowHeight / 2;
        }

        private void WriteSummary(TimeSpan total_time, int number_of_chars)
        {
            string str_seconds = total_time.ToString();
            var length_till_comma = str_seconds.LastIndexOf(".");
            var substring = str_seconds.Substring(0, length_till_comma);

            int seconds = total_time.Seconds;
            double minutes = (double)seconds / 60;

            double cpm = (double)number_of_chars / minutes;
            double cps = (double)number_of_chars / (double)seconds;

            Console.WriteLine("Summary of the game:");
            Console.WriteLine("Total time of typing: " + substring);
            Console.WriteLine("Total number of chars typed: " + number_of_chars);
            Console.WriteLine("Characters per minute: " + cpm);
            Console.WriteLine("Characters per second: " + cps);
        }

        private void EndGame()
        {
            Console.Clear();
            Console.WriteLine("YOU LOST!");
        }

        private void UpdateWord(in int i, in char[] chars, ConsoleColor color, in int posX, in int posY)
        {
            // Change color of char at index i of word
            Console.SetCursorPosition(posX + i, posY);
            Console.ForegroundColor = color;
            Console.Write(chars[i]);
            //Console.BackgroundColor = default;

            // Reset color and cursor back to typing area
            Console.ForegroundColor= ConsoleColor.White;
            Console.SetCursorPosition(posX + i + 1, posY);
            //Console.BackgroundColor = ConsoleColor.Gray;

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
            Console.WriteLine("For better experience, set window size to full-screen");
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