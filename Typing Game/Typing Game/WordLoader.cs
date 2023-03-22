using System.Text.RegularExpressions;

namespace Typing_Game
{
    class WordLoader
    {
        public static List<WordDataType> ReadWordsFromFile(string filePath)
        {
            List<WordDataType> words = new List<WordDataType>();
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                // Adds word to bank only if it contains only aplhabetical characters (no special characters or numbers)
                if (CheckWord(line))
                {
                    words.Add(new WordDataType(line.ToLower()));    // Adds a word converted to lower case
                }
            }
            return words;
        }

        // For checking if the imported word does not contain a number or any special character, and length is more than 3
        private static bool CheckWord(string line)
        {
            // Checking for any number in imported string
            for (int i = 0; i < 10; i++) 
            {
                if (line.Contains((char)i)) return false;
            }
            // Checking for any special character in imported string
            if (!Regex.IsMatch(line, @"^[a-zA-Z]+$")) return false;

            if (line.Length < 3) return false;

            // If both controls have been passed
            return true;
        }
    }
}
