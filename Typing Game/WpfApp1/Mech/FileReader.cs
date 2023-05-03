using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TypingGame.Mech
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
            string[] lines = File.ReadAllLines(filePath);

            // For better ram optimalization
            Random rnd = new();
            int numberOfRandomWords = 500;
            string[] randomWords = new string[numberOfRandomWords];
            for (int i = 0; i < numberOfRandomWords; i++)
            {
                int randomIndex = (int)rnd.NextInt64(lines.LongLength);
                randomWords[i] = lines[randomIndex];
            }

            foreach (string line in randomWords)
            {
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

            if (line.Length < 3) return false;

            // If both controls have been passed
            return true;
        }
    }
}
