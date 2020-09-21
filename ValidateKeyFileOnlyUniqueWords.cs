using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskFromBCS
{
    class ValidateKeyFileOnlyUniqueWords : IValidate
    {
        public bool GetValidate(IEnumerable<string> patternWords, IWordComparator comparator)
        {
            if (patternWords == null)
                throw new ArgumentNullException(nameof(patternWords), "Значение не может быть null");
            for (int i = 0; i < patternWords.Count() - 1; i++)
            {
                for (int j = i + 1; j < patternWords.Count(); j++)
                    if (comparator.CompareWords(patternWords.ElementAt(i), patternWords.ElementAt(j)))
                        return false;
            }
            return true;
        }
    }
}
