using System;

namespace TaskFromBCS
{
    /// <summary>
    /// Класс хранилище статистики вхождения слов в файле
    /// </summary>
    class WordStats
    {
        private int count = 0;
        public string FileName { get; private set; }
        public string Word { get; private set; }
        public int Count 
        {
            get
            {
                return count;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(count), "Значение повторений не может быть отрицательной величиной");
                else count = value;
            }
        }
        public WordStats(string fileName, string word, int count)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName), "Строка не может быть пустой или отсутствовать");
            else FileName = fileName;
            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException(nameof(word), "Строка не может быть пустой или отсутствовать");
            else Word = word;
            Count = count;
        }
        public static WordStats operator +(WordStats statsFirst, WordStats statsSecond)
        {
            if (statsFirst.Word == statsSecond.Word || string.IsNullOrEmpty(statsFirst.Word))
                return new WordStats(statsFirst.FileName, statsFirst.Word, statsFirst.Count + statsSecond.Count);
            else throw new ArgumentException("Операция сложения не допустима", nameof(Word));
        }
    }
}

