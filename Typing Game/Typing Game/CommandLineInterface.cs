namespace Typing_Game
{
    class CommandLineInterface
    {
        private int startingPositionX = Console.WindowWidth / 2;
        private int startingPositionY = Console.WindowHeight / 2;
        public CommandLineInterface(List<DataType> _words, ref HealthPoint hp)
        {
            Intro();
            ReloadFieldsConsole();
            WaitingForStart();
            DateTime start_of_game = DateTime.Now;   // Start the timer

            foreach (DataType word in _words)
            {
                Console.CursorVisible = false;
                int posX = startingPositionX - (word.Length/2);
                int posY = startingPositionY;
                Console.SetCursorPosition(posX, posY);      // Console start position
                Console.WriteLine(word.Word);               // Prints a word that should be re-typed
                Console.SetCursorPosition(posX, posY);      // Sets cursor to the start of word
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Write(word.Chars[0]);
                Console.SetCursorPosition(posX, posY);
                Console.BackgroundColor = default;

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
                                // TODO end current game .. rn just skips to next word
                                break;
                            }
                            
                            UpdateWord(in i, in chars, ConsoleColor.Red, in posX, in posY);
                            Console.Beep(1000,100); // Sound indication that char is wrong
                        }
                        else
                        {
                            UpdateWord(in i, in chars, ConsoleColor.Green, in posX, in posY);
                            continue;
                        }    
                    }
                    writtenWord = true; // Jump to another word
                    Console.Beep(2000, 100);
                }
                Console.Clear();    // Clear console for another word
            }

            DateTime end_of_game = DateTime.Now;     // End the timer
            var total_time_of_game = end_of_game - start_of_game;
            WriteSummary(total_time_of_game, GetNumberOfChars(_words));
        }

        private static int GetNumberOfChars(List<DataType> words)
        {
            int value = 0;
            foreach (DataType word in words)
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

        // Writes basic stats about player's game - total typing time, number of typed chars, characters per minute, characters per second.
        private static void WriteSummary(TimeSpan total_time, int number_of_chars)
        {
            string str_seconds = total_time.ToString();
            var length_till_comma = str_seconds.LastIndexOf(".");
            var substring = str_seconds.Substring(0, length_till_comma);

            int seconds = total_time.Seconds;
            double minutes = (double)seconds / 60;

            double cpm = (double)number_of_chars / minutes;
            double wpm = (double)(number_of_chars/4.7) / minutes;

            Console.WriteLine("Summary of the game:");
            Console.WriteLine("Total time of typing: " + substring);
            Console.WriteLine("Total number of chars typed: " + number_of_chars);
            Console.WriteLine("Average characters per minute (rounded): " + Math.Round(cpm));
            Console.WriteLine("Average words (4.7 char) per minute: " + Math.Round(wpm,2));
        }

        private static void EndGame()
        {
            Console.Clear();
            Console.WriteLine("YOU LOST!");
        }

        private static void UpdateWord(in int i, in char[] chars, ConsoleColor color, in int posX, in int posY)
        {
            // Change color of char at index i of word
            Console.SetCursorPosition(posX + i, posY);
            Console.ForegroundColor = color;
            Console.Write(chars[i]);


            // Set background to next typed char
            if ((i + 1) < chars.Length)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.SetCursorPosition(posX + i + 1, posY);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(chars[i + 1]);
                Console.BackgroundColor = default;
            }

            // Reset color and cursor back to typing area
            Console.ForegroundColor= ConsoleColor.White;
            Console.SetCursorPosition(posX + i + 1, posY);

        }

        // Reads key pressed by user
        private static char ReadKey()
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

        private static void WaitingForStart()
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