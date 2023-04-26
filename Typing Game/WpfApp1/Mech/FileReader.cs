using System.Text.RegularExpressions;

namespace Typing_Game
{
    class FileReader
    {
        public static List<DataType> ReadWordsFromFile(string filePath)
        {
            List<DataType> words = new();
            string[] lines = File.ReadAllLines(filePath);

            // For better ram optimalization
            Random rnd = new();
            int number_of_random_words = 500;
            string[] random_words = new string[number_of_random_words];
            for (int i = 0; i < number_of_random_words; i++)
            {
                int random_index = (int)rnd.NextInt64(lines.LongLength);
                random_words[i] = lines[random_index];
            }

            foreach (string line in random_words)
            {
                // For slovak txt file
                string word = "";
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '\t' || line[i] == ' ' || line[i] == '\n')
                    {
                        word = line.Substring(0, i);
                        break;
                    }
                    if (i == line.Length - 1)
                    {
                        word = line;
                        break;
                    }
                }

                // Adds word to bank only if it contains only aplhabetical characters (no special characters or numbers)
                if (CheckWord(word))
                {
                    words.Add(new DataType(word.ToLower()));    // Adds a word converted to lower case
                }
            }
            return words;
        }

        // For checking if the imported word does not contain a number or any special character, and length is more than 3
        private static bool CheckWord(string line)
        {
            // Checking for any number in imported string
            if (line.Any(char.IsDigit)) return false;
            
            // Checking for any special character in imported string
            if (!Regex.IsMatch(line, @"^[a-zA-Z]+$")) return false;

            if (line.Length < 3) return false;

            // If both controls have been passed
            return true;
        }
    }
}
