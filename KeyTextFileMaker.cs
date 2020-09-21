using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskFromBCS
{
    class KeyTextFileMaker
    {
        private IEnumerable<string> patternWords;
        private IWordComparator comparator;

        public KeyTextFileMaker(IEnumerable<string> patternWords, IWordComparator comparator)
        {
            if (patternWords == null)
                throw new ArgumentNullException(nameof(patternWords), "Значение не может быть null");
            if (comparator == null)
                throw new ArgumentNullException(nameof(patternWords), "Значение не может быть null");
            this.patternWords = patternWords;
            this.comparator = comparator;
        }
        public IEnumerable<string> CreateKeyPattern()
        {
            string[] resultOperation = patternWords.ToArray();
            if (comparator.IgnoreCase)
            {
                for (int i = 0; i < resultOperation.Length; i++)
                {
                    resultOperation[i] = resultOperation[i].ToLower();
                }
            }
            return resultOperation.Distinct().ToArray();
        }
        public bool ValidateKeyFile(IValidate validateMethod)
        {
            return validateMethod.GetValidate(patternWords, comparator);
        }
        public void WriteKeyFile(IWritable file)
        {
            
        }
    }
}
