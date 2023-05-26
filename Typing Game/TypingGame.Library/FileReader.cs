using System.Text.RegularExpressions;

namespace TypingGame.Library
{
    /// <summary>
    /// Class used to read words from file.
    /// </summary>
    internal class FileReader
    {
        /// <summary>
        /// Static method that reads words from file and writes them in List of DataTypes
        /// </summary>
        /// <param name="filePath">Path to the .txt file containing data.</param>
        /// <returns></returns>
        public static List<DataType> ReadWordsFromFile(string filePath)
        {
            List<DataType> words = new();
            var lines = File.ReadAllLines(filePath);

            // For better ram optimalization
            Random rnd = new();
            const int numberOfRandomWords = 1000;
            var randomWords = new string[numberOfRandomWords];
            for (var i = 0; i < numberOfRandomWords; i++)
            {
                var randomIndex = (int)rnd.NextInt64(lines.LongLength);
                randomWords[i] = lines[randomIndex];
            }

            foreach (var line in randomWords)
            {
                var word = "";
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == '\t' || line[i] == ' ' || line[i] == '\n')
                    {
                        word = line[..i];
                        break;
                    }

                    if (i != line.Length - 1) continue;
                    word = line;
                    break;
                }

                // Adds word to bank only if it contains only aplhabetical characters (no special characters or numbers)
                if (CheckWord(word))
                {
                    words.Add(new DataType(word));    // Adds a word converted to lower case
                }
            }
            return words;
        }

        /// <summary>
        /// For checking if the imported word does not contain a number or any special character, and length is more than 3.
        /// </summary>
        /// <param name="line">String of line</param>
        /// <returns>True if the word passed the checks.</returns>
        private static bool CheckWord(string line)
        {
            // Checking for any number in imported string
            if (line.Any(char.IsDigit)) return false;
            
            // Checking for any special character in imported string
            if (!Regex.IsMatch(line, @"^[a-zA-Z]+$")) return false;

            return line.Length >= 3;
            // If both controls have been passed
        }
    }
}
