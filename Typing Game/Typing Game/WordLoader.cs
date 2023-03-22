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
                words.Add(new WordDataType(line));
            }
            return words;
        }
    }
}
