﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TypingGame.UserData
{
    internal class Reader
    {
        private List<string> _numberOfCharsList;
        private List<string> _numberOfErrorsList;
        private List<string> _numberOfCpmList;
        private List<string> _numberOfWpmList;
        public Reader(string path)
        {
            _numberOfCharsList = new List<string>();
            _numberOfErrorsList = new List<string>();
            _numberOfWpmList = new List<string>();
            _numberOfCpmList = new List<string>();
            using var reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                _numberOfCharsList.Add(values[1]);
                _numberOfErrorsList.Add(values[2]);
                _numberOfCpmList.Add(values[3]);
                _numberOfWpmList.Add(values[4]);
            }
            reader.Close();
        }

        public int GetAverageNumberOfChars()
        {
            if (_numberOfCharsList.Count == 0)
            {
                return 0;
            }
            var value = _numberOfCharsList.Sum(int.Parse) / _numberOfCharsList.Count;
            return value;
        }

        public int GetAverageNumberOfErrors()
        {
            if (_numberOfErrorsList.Count == 0)
            {
                return 0;
            }
            var value = _numberOfErrorsList.Sum(int.Parse) / _numberOfErrorsList.Count;
            return value;
        }

        public double GetAverageNumberOfCpm()
        {
            if (_numberOfCpmList.Count == 0)
            {
                return 0.0;
            }
            var value = _numberOfCpmList.Sum(double.Parse) / _numberOfCpmList.Count;
            return value;
        }

        public double GetAverageNumberOfWpm()
        {
            if (_numberOfWpmList.Count == 0)
            {
                return 0.0;
            }
            var value = _numberOfWpmList.Sum(double.Parse) / _numberOfWpmList.Count;
            return value;
        }
    }
}