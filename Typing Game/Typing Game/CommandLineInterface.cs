namespace Typing_Game
{
    class CommandLineInterface
    {
        private int starting_position_x = Console.WindowWidth / 2;
        private int starting_position_y = Console.WindowHeight / 2;
        private ConsoleColor cursor_color = ConsoleColor.DarkGreen;
        private ConsoleColor basic_color = ConsoleColor.White;
        private ConsoleColor wrong_color = ConsoleColor.Red;
        private ConsoleColor right_color = ConsoleColor.Green;
        public CommandLineInterface(List<DataType> _words, ref HealthPoint hp)
        {
            Intro();
            ReloadFieldsConsole();
            WaitingForStart();
            DateTime start_of_game = DateTime.Now;   // Start the timer
            DateTime end_of_game;
            TimeSpan total_time_of_game;
            int number_of_chars_typed = 0;
            int number_of_errors = 0;         // Number of false chars
            bool ended = false;

            foreach (DataType word in _words)
            {
                int posX, posY;
                SetUp(word, out posX, out posY);

                bool writtenWord = false;
                char[] chars = word.Chars;

                while (!writtenWord)
                {
                    if (ended)
                    {
                        break;
                    }
                    Console.Beep(2000, 100);
                    for (int i = 0; i < word.Length; i++)   // Checking each char by char
                    {
                        char pressedKey = ReadKey();
                        if (pressedKey == 27)
                        {
                            var end = DateTime.Now;     // End the timer
                            var time = end - start_of_game;
                            WriteSummary(time, number_of_chars_typed, number_of_errors);
                            writtenWord = true;
                            ended = true;
                            break;
                        }

                        // Checks if written char matches the char of word and changes the color on console
                        if (!pressedKey.Equals(chars[i]))
                        {
                            number_of_errors++;
                            hp--;
                            if ((int)hp == -1)
                            {
                                //EndGame();
                                // TODO end current game .. rn just skips to next word
                                //break;
                            }

                            UpdateWord(in i, in chars, wrong_color, in posX, in posY);
                            Console.Beep(1000, 100); // Sound indication that char is wrong
                        }
                        else
                        {
                            number_of_chars_typed++;
                            UpdateWord(in i, in chars, right_color, in posX, in posY);
                            continue;
                        }
                    }
                    writtenWord = true; // Jump to another word
                    if (!ended)
                    {
                        Console.Clear();
                    }
                }
                if (ended)
                {
                    break;
                }
            }
            end_of_game = DateTime.Now;     // End the timer
            total_time_of_game = end_of_game - start_of_game;
            WriteSummary(total_time_of_game, number_of_chars_typed, number_of_errors);
        }

        private void SetUp(DataType word, out int posX, out int posY)
        {
            Console.CursorVisible = false;
            posX = starting_position_x - (word.Length / 2);
            posY = starting_position_y;
            Console.SetCursorPosition(posX, posY);      // Console start position
            Console.WriteLine(word.Word);               // Prints a word that should be re-typed
            Console.SetCursorPosition(posX, posY);      // Sets cursor to the start of word
            Console.BackgroundColor = cursor_color;
            Console.Write(word.Chars[0]);
            Console.SetCursorPosition(posX, posY);
            Console.BackgroundColor = default;
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
            starting_position_x = Console.WindowWidth / 2;
            starting_position_y = Console.WindowHeight / 2;
        }

        // Writes basic stats about player's game - total typing time, number of typed chars, characters per minute, characters per second.
        private static void WriteSummary(TimeSpan total_time, int number_of_chars, int number_of_errors)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            string str_seconds = total_time.ToString();
            var length_till_comma = str_seconds.LastIndexOf(".");
            var substring = str_seconds.Substring(0, length_till_comma);

            int seconds = total_time.Seconds;
            double minutes = (double)seconds / 60;

            int number_of_written = number_of_chars - number_of_errors;

            double cpm = number_of_written / minutes;   
            double wpm = (double)(number_of_written / 4.7) / minutes;
            
            /*
             * Checks if values are not negative
             */
            if (cpm < 0) cpm =0;
            if (wpm < 0) wpm = 0;
            if (number_of_chars < 0) number_of_chars = 0;

            Console.WriteLine("Summary of the game:");
            Console.WriteLine("Total time of typing: " + substring);
            Console.WriteLine("Total number of chars typed: " + number_of_written);
            Console.WriteLine("Number of errors: " + number_of_errors);
            Console.WriteLine("Average characters per minute (rounded): " + Math.Round(cpm));
            Console.WriteLine("Average words (4.7 char) per minute: " + Math.Round(wpm,2));
            WriteLevel(wpm);
        }

        private static void WriteLevel(double wpm)
        {
            /*
             * Info from https://i.redd.it/x9n5gr9d61f41.png
             */
            if (wpm <= 10) Console.WriteLine("Equivalent to one word every 6 seconds. Learn the proper typing technique and practice to improve your speed.");
            else if (wpm > 0 && wpm <= 10) Console.WriteLine("Equivalent to one word every 3 seconds. Focus on your technique and keep practising.");
            else if (wpm > 10 && wpm <= 20) Console.WriteLine("Better, but still below average. Keep practising to improve your speed and accuracy.");
            else if (wpm > 20 && wpm <= 30) Console.WriteLine("At 41 wpm, you are now an average typist. You still have significant room for improvement.");
            else if (wpm > 30 && wpm <= 40) Console.WriteLine("Congratulations! You're above average!");
            else if (wpm > 40 && wpm <= 50) Console.WriteLine("This is the speed required for most jobs. You can now be a professional typist.");
            else if (wpm > 50 && wpm <= 60) Console.WriteLine("You are way above average and would qualify for any typing job, assuming your accuracy is high enough.");
            else if (wpm > 60 && wpm <= 70) Console.WriteLine("You're a catch! Any employer looking for a typist would love to have you!");
            else if (wpm > 70 && wpm <= 80) Console.WriteLine("At this speed, you're probably a gamer, coder or genius. You're doing great!");
            else if (wpm > 80) Console.WriteLine("You're in the top 1% of typists! Congratulations!");
        }

        private static void EndGame()
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


            // Set background to next typed char
            if ((i + 1) < chars.Length)
            {
                Console.BackgroundColor = cursor_color;
                Console.SetCursorPosition(posX + i + 1, posY);
                Console.ForegroundColor = basic_color;
                Console.Write(chars[i + 1]);
                Console.BackgroundColor = default;
            }

            // Reset color and cursor back to typing area
            Console.ForegroundColor = basic_color;
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