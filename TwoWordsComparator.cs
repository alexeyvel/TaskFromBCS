using System;

namespace TaskFromBCS
{
    /// <summary>
    /// Класс проверяющий два слова на идентичность
    /// </summary>
    class TwoWordsComparator : IWordComparator
    {
        public bool IgnoreCase { get; set; }
        public TwoWordsComparator(bool ignoreCase)
        {
            IgnoreCase = ignoreCase;
        }
      
        public bool CompareWords(string targetWord, string patternWord)
        {
            if (string.IsNullOrEmpty(targetWord))
                throw new ArgumentNullException(nameof(targetWord), "Слово для сравнения не должно быть null или пустым");
            if (string.IsNullOrEmpty(patternWord))
                throw new ArgumentNullException(nameof(targetWord), "Слово для сравнения не должно быть null или пустым");
            return 0 == string.Compare(targetWord, patternWord, IgnoreCase);
        }
    }
}
