﻿namespace Typing_Game
{
    class Gameplay
    {
        private List<WordDataType> _words = new();
        private int _number_of_words;
        private Random _rnd = new();
        public Gameplay(Difficulty difficuty, WordBank bank)
        {
            SetNumberOfWords(difficuty); 
            FillWithRandomWords(ref _words, difficuty, bank);
            // TODO Start a window app here
            ConsoleInterface ctg = new(_words);
        }

        // For debugging needs
        private void PrintWords()
        {
            for (int i = 0; i < _number_of_words; i++)
            {
                Console.Write(_words[i].Word + " ");
            }
        }

        // Sets the number of words that will be displayed in a game.
        // Default is set to EASY -> 100.
        private void SetNumberOfWords(Difficulty difficuty)
        {
            _number_of_words = difficuty switch
            {
                (Difficulty)0 => 20,
                (Difficulty)1 => 50,
                (Difficulty)2 => 100,
                _ => 100,
            };
        }

        // Fills list of words with random words from bank of words.
        private void FillWithRandomWords(ref List<WordDataType> words, Difficulty difficuty, WordBank bank)
        {
            for (int i = 0; i < _number_of_words; i++)
            {
                int randomIndex = _rnd.Next(bank.Words.Count);
                words.Add(bank.Words[randomIndex]);
            }
        }
    }
}