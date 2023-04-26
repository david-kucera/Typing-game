using System;
using System.Collections.Generic;
using TypingGame.CLI;

namespace TypingGame.Mech
{
    class Gameplay
    {
        private List<DataType> _words = new();
        private int _number_of_words;
        private readonly Random _rnd = new();
        private HealthPoint _healthPoint;
        public Gameplay(Difficulty difficuty, Bank bank, string UI)
        {
            
            SetUp(difficuty); 
            FillWithRandomWords(ref _words, difficuty, bank);

            if (UI == "cli")
            {
                CommandLineInterface ctg = new(_words, ref _healthPoint);
            }
            else
            { 
                GameWindow gw = new GameWindow(_words, ref _healthPoint);
            }
            
        }

        // Sets the number of words that will be displayed in a game and number of lives.
        // Default is set to EASY -> 20 words and 3 lives.
        private void SetUp(Difficulty difficuty)
        {
            _number_of_words = difficuty switch
            {
                (Difficulty)0 => 20,    
                (Difficulty)1 => 50,    
                (Difficulty)2 => 100,   
                _ => 100,
            };

            _healthPoint = difficuty switch
            {
                (Difficulty)0 => HealthPoint.THREE,
                (Difficulty)1 => HealthPoint.TWO,
                (Difficulty)2 => HealthPoint.ONE,
                _ => HealthPoint.THREE,
            };
        }

        // Fills list of words with random words from bank of words.
        private void FillWithRandomWords(ref List<DataType> words, Difficulty difficuty, Bank bank)
        {
            for (int i = 0; i < _number_of_words; i++)
            {
                int randomIndex = _rnd.Next(bank.Words.Count);
                words.Add(bank.Words[randomIndex]);
            }
        }
    }
}