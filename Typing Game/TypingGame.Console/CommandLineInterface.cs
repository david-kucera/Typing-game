using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using TypingGame.Library;

namespace TypingGame.Console
{
    /// <summary>
    /// Class for command line interface of the game.
    /// All necessary code for console is here.
    /// </summary>
    public class CommandLineInterface
    {
        private int _startingPositionX = System.Console.WindowWidth / 2;
        private int _startingPositionY = System.Console.WindowHeight / 2;
        private const ConsoleColor CursorColor = ConsoleColor.DarkGreen;
        private const ConsoleColor BasicColor = ConsoleColor.White;
        private const ConsoleColor WrongColor = ConsoleColor.Red;
        private const ConsoleColor RightColor = ConsoleColor.Green;
        private const string OutputFilePath = @"UserData\\Data.csv";
        private const string ErrorWords = @"UserData\\errors.csv";

        /// <summary>
        /// Main for testing purposes.
        /// </summary>
        public static void Main()
        {
            var gp = new Gameplay(Difficulty.EASY, new Bank(Dictionary.SLOVAK));
            var cli = new CommandLineInterface(gp.get_words(), gp.get_health_points());
        }

        /// <summary>
        /// Constructor of the game.
        /// </summary>
        /// <param name="words">List of words to be retyped.</param>
        /// <param name="hp">Number of health points.</param>
        public CommandLineInterface(List<DataType> words, HealthPoint hp)
        {
            Intro();
            ReloadFieldsConsole();
            WaitingForStart();
            var startOfGame = DateTime.Now;   // Start the timer
            var numberOfCharsTyped = 0;
            var numberOfErrors = 0;         // Number of false chars
            var ended = false;

            foreach (var word in words)
            {
                int posX, posY;
                SetUp(word, out posX, out posY);

                var writtenWord = false;
                var wroteWord = "";
                var chars = word.Chars;

                while (!writtenWord)
                {
                    if (ended)
                    {
                        break;
                    }
                    System.Console.Beep(2000, 100);
                    for (var i = 0; i < word.Length; i++)   // Checking each char by char
                    {
                        var pressedKey = ReadKey();
                        wroteWord += pressedKey;
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
                            System.Console.Beep(1000, 100); // Sound indication that char is wrong
                        }
                        else
                        {
                            numberOfCharsTyped++;
                            UpdateWord(in i, in chars, RightColor, in posX, in posY);
                            continue;
                        }
                    }
                    writtenWord = true; // Jump to another word
                    if (!wroteWord.Equals(word.Word))
                    {
                        var output = word.Word + ";" + wroteWord + "\n";
                        File.AppendAllText(ErrorWords, output);
                    }
                    if (!ended)
                    {
                        System.Console.Clear();
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
        /// Used for console manipulation.
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
                System.Console.Clear();
                System.Console.WriteLine("YOU LOST!");
            }
            System.Console.WriteLine();
            System.Console.Write("...Press RETURN key to end the app...");
            System.Console.CursorVisible = true;
            System.Console.ReadLine(); // For not closing console window immediately
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
            System.Console.CursorVisible = false;
            posX = _startingPositionX - word.Length / 2;
            posY = _startingPositionY;
            System.Console.SetCursorPosition(posX, posY);      // Console start position
            System.Console.WriteLine(word.Word);               // Prints a word that should be re-typed
            System.Console.SetCursorPosition(posX, posY);      // Sets cursor to the start of word
            System.Console.BackgroundColor = CursorColor;
            System.Console.Write(word.Chars[0]);
            System.Console.SetCursorPosition(posX, posY);
            System.Console.BackgroundColor = default;
        }

        /// <summary>
        /// Reloads fields for console dimensions when user changes console window size before the start of the game.
        /// </summary>
        private void ReloadFieldsConsole()
        {
            _startingPositionX = System.Console.WindowWidth / 2;
            _startingPositionY = System.Console.WindowHeight / 2;
        }

        /// <summary>
        /// Writes basic stats about player's game - total typing time, number of typed chars, characters per minute, characters per second,
        /// and saves them to file.
        /// </summary>
        /// <param name="totalTime">Total time spent typing</param>
        /// <param name="numberOfChars">Number of chars typed</param>
        /// <param name="numberOfErrors">Number of errors occured when typing</param>
        private static void WriteSummary(TimeSpan totalTime, int numberOfChars, int numberOfErrors)
        {
            System.Console.Clear();
            System.Console.SetCursorPosition(0, 0);
            var strSeconds = totalTime.ToString();
            var lengthTillComma = strSeconds.LastIndexOf(".", StringComparison.Ordinal);
            var substring = strSeconds.Substring(0, lengthTillComma);

            var seconds = totalTime.Seconds;
            var minutes = (double)seconds / 60;

            var numberOfWritten = numberOfChars - numberOfErrors;

            var cpm = numberOfWritten / minutes;
            var wpm = numberOfWritten / 4.7 / minutes;

            // Checks if values are not negative
            if (cpm < 0) cpm = 0;
            if (wpm < 0) wpm = 0;
            if (numberOfChars < 0) numberOfChars = 0;

            // Save to csv file
            if (SaveToFile(totalTime, numberOfChars, numberOfErrors, wpm, cpm)) return;

            System.Console.WriteLine("Summary of the game:");
            System.Console.WriteLine("Total time of typing: " + substring);
            System.Console.WriteLine("Total number of chars typed: " + numberOfWritten);
            System.Console.WriteLine("Number of errors: " + numberOfErrors);
            System.Console.WriteLine("Average characters per minute (rounded): " + Math.Round(cpm));
            System.Console.WriteLine("Average words (4.7 char) per minute: " + Math.Round(wpm, 2));
            WriteLevel(wpm);
        }

        private static bool SaveToFile(TimeSpan totalTime, int numberOfChars, int numberOfErrors, double wpm, double cpm)
        {
            var output = new StringBuilder();
            const string separator = ";";
            string[] newLine =
            {
                totalTime.ToString(), numberOfChars.ToString(), numberOfErrors.ToString(), Math.Round(wpm, 2).ToString(CultureInfo.CurrentCulture),
                Math.Round(cpm).ToString(CultureInfo.CurrentCulture)
            };
            output.AppendLine(string.Join(separator, newLine));
            try
            {
                File.AppendAllText(OutputFilePath, output.ToString());
            }
            catch (IOException)
            {
                System.Console.WriteLine("Error while saving data to csv file.");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Prints the typing skill level after successfull game. 
        /// </summary>
        /// <param name="wpm">WPM stat of player</param>
        private static void WriteLevel(double wpm)
        {
            // Info from https://i.redd.it/x9n5gr9d61f41.png
            if (wpm <= 10) System.Console.WriteLine("Equivalent to one word every 6 seconds. Learn the proper typing technique and practice to improve your speed.");
            else if (wpm is > 0 and <= 10) System.Console.WriteLine("Equivalent to one word every 3 seconds. Focus on your technique and keep practising.");
            else if (wpm is > 10 and <= 20) System.Console.WriteLine("Better, but still below average. Keep practising to improve your speed and accuracy.");
            else if (wpm is > 20 and <= 30) System.Console.WriteLine("At 41 wpm, you are now an average typist. You still have significant room for improvement.");
            else if (wpm is > 30 and <= 40) System.Console.WriteLine("Congratulations! You're above average!");
            else if (wpm is > 40 and <= 50) System.Console.WriteLine("This is the speed required for most jobs. You can now be a professional typist.");
            else if (wpm is > 50 and <= 60) System.Console.WriteLine("You are way above average and would qualify for any typing job, assuming your accuracy is high enough.");
            else if (wpm is > 60 and <= 70) System.Console.WriteLine("You're a catch! Any employer looking for a typist would love to have you!");
            else if (wpm is > 70 and <= 80) System.Console.WriteLine("At this speed, you're probably a gamer, coder or genius. You're doing great!");
            else if (wpm > 80) System.Console.WriteLine("You're in the top 1% of typists! Congratulations!");
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
            System.Console.SetCursorPosition(posX + i, posY);
            System.Console.ForegroundColor = color;
            System.Console.Write(chars[i]);


            // Set background to next typed char
            if (i + 1 < chars.Length)
            {
                System.Console.BackgroundColor = CursorColor;
                System.Console.SetCursorPosition(posX + i + 1, posY);
                System.Console.ForegroundColor = BasicColor;
                System.Console.Write(chars[i + 1]);
                System.Console.BackgroundColor = default;
            }

            // Reset color and cursor back to typing area
            System.Console.ForegroundColor = BasicColor;
            System.Console.SetCursorPosition(posX + i + 1, posY);

        }

        /// <summary>
        /// Reads the key pressed by player
        /// </summary>
        /// <returns>Character typed</returns>
        private static char ReadKey()
        {
            var keyPressed = false;
            var charInput = '.';
            while (!keyPressed)
            {
                if (System.Console.KeyAvailable)
                {
                    charInput = System.Console.ReadKey().KeyChar;
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
            System.Console.WriteLine("Welcome to typing game!");
            System.Console.WriteLine("Words will appear on your screen, try to re-write them as fast as you can.");
            System.Console.WriteLine("If you type incorrect character, don't worry, just continue like nothing happened.");
            System.Console.WriteLine("   You will have: ");
            System.Console.WriteLine("      20 words, 3 lives - if difficulty is se to EASY");
            System.Console.WriteLine("      50 words, 2 lives - if difficulty is set to MEDIUM");
            System.Console.WriteLine("      100 words, 1 life - if difficulty is set to HARD ");
            System.Console.WriteLine("");
            System.Console.WriteLine("For better experience, set window size to full-screen");
            System.Console.WriteLine("");
            System.Console.WriteLine("If you are ready, press RETURN.");
        }

        /// <summary>
        /// Method used for waiting for right user input to start the game.
        /// </summary>
        private static void WaitingForStart()
        {
            var keyPressed = ReadKey();
            while (keyPressed != '\r')
            {
                keyPressed = ReadKey();
            }
            System.Console.Clear();
        }
    }
}