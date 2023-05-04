using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using TypingGame.Mech;

namespace TypingGame.CLI
{
    /// <summary>
    /// Class for command line interface of the game.
    /// All necessary code for console is here.
    /// </summary>
    class CommandLineInterface
    {
        private int _startingPositionX = Console.WindowWidth / 2;
        private int _startingPositionY = Console.WindowHeight / 2;
        private const ConsoleColor CursorColor = ConsoleColor.DarkGreen;
        private const ConsoleColor BasicColor = ConsoleColor.White;
        private const ConsoleColor WrongColor = ConsoleColor.Red;
        private const ConsoleColor RightColor = ConsoleColor.Green;

        /// <summary>
        /// Constructor of the game.
        /// </summary>
        /// <param name="words">List of words to be retyped</param>
        /// <param name="hp">Number of health points depending on the difficulty chosen by player in game menu.</param>
        public CommandLineInterface(List<DataType> words, ref HealthPoint hp)
        {
            Intro();
            ReloadFieldsConsole();
            WaitingForStart();
            DateTime startOfGame = DateTime.Now;   // Start the timer
            int numberOfCharsTyped = 0;
            int numberOfErrors = 0;         // Number of false chars
            bool ended = false;

            foreach (DataType word in words)
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
                            var time = end - startOfGame;
                            WriteSummary(time, numberOfCharsTyped, numberOfErrors);
                            writtenWord = true;
                            ended = true;
                            break;
                        }

                        // Checks if written char matches the char of word and changes the color on console
                        if (!pressedKey.Equals(chars[i]))
                        {
                            numberOfErrors++;
                            hp--;
                            if ((int)hp == -1)
                            {
                                EndConsoleApp(true);
                                break;
                            }

                            UpdateWord(in i, in chars, WrongColor, in posX, in posY);
                            Console.Beep(1000, 100); // Sound indication that char is wrong
                        }
                        else
                        {
                            numberOfCharsTyped++;
                            UpdateWord(in i, in chars, RightColor, in posX, in posY);
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
            var endOfGame = DateTime.Now;     // End the timer
            var totalTimeOfGame = endOfGame - startOfGame;
            WriteSummary(totalTimeOfGame, numberOfCharsTyped, numberOfErrors);
            EndConsoleApp(false);
        }

        /// <summary>
        /// Used for console manipulation
        /// </summary>
        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        /// <summary>
        /// Method used to close the console.
        /// </summary>
        /// <param name="lost">If true, prints that the player lost and closes console.</param>
        private static void EndConsoleApp(bool lost)
        {
            if (lost)
            {
                Console.Clear();
                Console.WriteLine("YOU LOST!");
            }
            Console.WriteLine();
            Console.Write("...Press RETURN key to end the app...");
            Console.CursorVisible = true;
            Console.ReadLine(); // For not closing console window immediately
            FreeConsole(); // Closes the console
        }

        /// <summary>
        /// Method to set-up the first word and print it in the middle of console.
        /// </summary>
        /// <param name="word">Datatype of the word to be retyped</param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        private void SetUp(DataType word, out int posX, out int posY)
        {
            Console.CursorVisible = false;
            posX = _startingPositionX - word.Length / 2;
            posY = _startingPositionY;
            Console.SetCursorPosition(posX, posY);      // Console start position
            Console.WriteLine(word.Word);               // Prints a word that should be re-typed
            Console.SetCursorPosition(posX, posY);      // Sets cursor to the start of word
            Console.BackgroundColor = CursorColor;
            Console.Write(word.Chars[0]);
            Console.SetCursorPosition(posX, posY);
            Console.BackgroundColor = default;
        }

        /// <summary>
        /// Reloads fields for console dimensions when user changes console window size.
        /// </summary>
        private void ReloadFieldsConsole()
        {
            _startingPositionX = Console.WindowWidth / 2;
            _startingPositionY = Console.WindowHeight / 2;
        }

        /// <summary>
        /// Writes basic stats about player's game - total typing time, number of typed chars, characters per minute, characters per second.
        /// </summary>
        /// <param name="totalTime">Total time spent typing</param>
        /// <param name="numberOfChars">Number of chars typed</param>
        /// <param name="numberOfErrors">Number of errors occured when typing</param>
        private static void WriteSummary(TimeSpan totalTime, int numberOfChars, int numberOfErrors)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            string strSeconds = totalTime.ToString();
            var lengthTillComma = strSeconds.LastIndexOf(".", StringComparison.Ordinal);
            var substring = strSeconds.Substring(0, lengthTillComma);

            int seconds = totalTime.Seconds;
            double minutes = (double)seconds / 60;

            int numberOfWritten = numberOfChars - numberOfErrors;

            double cpm = numberOfWritten / minutes;
            double wpm = (double)(numberOfWritten / 4.7) / minutes;

            // Checks if values are not negative
            if (cpm < 0) cpm = 0;
            if (wpm < 0) wpm = 0;
            if (numberOfChars < 0) numberOfChars = 0;

            // Save to csv file
            const string file = "UserData\\Data.csv";
            var output = new StringBuilder();
            const string separator = ";";
            String[] newLine = { totalTime.ToString(), numberOfChars.ToString(), numberOfErrors.ToString(), Math.Round(wpm, 2).ToString(), Math.Round(cpm).ToString() };
            output.AppendLine(string.Join(separator, newLine));
            try
            {
                File.AppendAllText(file, output.ToString());
            }
            catch (IOException)
            {
                Console.WriteLine("Error while saving data to csv file.");
                return;
            }

            Console.WriteLine("Summary of the game:");
            Console.WriteLine("Total time of typing: " + substring);
            Console.WriteLine("Total number of chars typed: " + numberOfWritten);
            Console.WriteLine("Number of errors: " + numberOfErrors);
            Console.WriteLine("Average characters per minute (rounded): " + Math.Round(cpm));
            Console.WriteLine("Average words (4.7 char) per minute: " + Math.Round(wpm, 2));
            WriteLevel(wpm);
        }

        /// <summary>
        /// Prints the typing skill level after successfull game. 
        /// </summary>
        /// <param name="wpm">WPM stat of player</param>
        private static void WriteLevel(double wpm)
        {
            // Info from https://i.redd.it/x9n5gr9d61f41.png
            if (wpm <= 10) Console.WriteLine("Equivalent to one word every 6 seconds. Learn the proper typing technique and practice to improve your speed.");
            else if (wpm is > 0 and <= 10) Console.WriteLine("Equivalent to one word every 3 seconds. Focus on your technique and keep practising.");
            else if (wpm is > 10 and <= 20) Console.WriteLine("Better, but still below average. Keep practising to improve your speed and accuracy.");
            else if (wpm is > 20 and <= 30) Console.WriteLine("At 41 wpm, you are now an average typist. You still have significant room for improvement.");
            else if (wpm is > 30 and <= 40) Console.WriteLine("Congratulations! You're above average!");
            else if (wpm is > 40 and <= 50) Console.WriteLine("This is the speed required for most jobs. You can now be a professional typist.");
            else if (wpm is > 50 and <= 60) Console.WriteLine("You are way above average and would qualify for any typing job, assuming your accuracy is high enough.");
            else if (wpm is > 60 and <= 70) Console.WriteLine("You're a catch! Any employer looking for a typist would love to have you!");
            else if (wpm is > 70 and <= 80) Console.WriteLine("At this speed, you're probably a gamer, coder or genius. You're doing great!");
            else if (wpm > 80) Console.WriteLine("You're in the top 1% of typists! Congratulations!");
        }

        /// <summary>
        /// Method updates color of each char from word depending on wether they were typed right, or not.
        /// </summary>
        /// <param name="i">Index of current char in word</param>
        /// <param name="chars">Array of chars of word</param>
        /// <param name="color">Color which to be colored by</param>
        /// <param name="posX">Cursor position on X axis</param>
        /// <param name="posY">Cursor position on Y axis</param>
        private static void UpdateWord(in int i, in char[] chars, ConsoleColor color, in int posX, in int posY)
        {
            // Change color of char at index i of word
            Console.SetCursorPosition(posX + i, posY);
            Console.ForegroundColor = color;
            Console.Write(chars[i]);


            // Set background to next typed char
            if (i + 1 < chars.Length)
            {
                Console.BackgroundColor = CursorColor;
                Console.SetCursorPosition(posX + i + 1, posY);
                Console.ForegroundColor = BasicColor;
                Console.Write(chars[i + 1]);
                Console.BackgroundColor = default;
            }

            // Reset color and cursor back to typing area
            Console.ForegroundColor = BasicColor;
            Console.SetCursorPosition(posX + i + 1, posY);

        }

        /// <summary>
        /// Reads the key pressed by player
        /// </summary>
        /// <returns>Character typed</returns>
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

        /// <summary>
        /// Prints basic start info how to play the game.
        /// </summary>
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

        /// <summary>
        /// Method used for waiting for right user input to start the game.
        /// </summary>
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