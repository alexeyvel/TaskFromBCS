using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskFromBCS
{
    /// <summary>
    /// Класс отвечающий за формирование данных файла ключа (шаблона). 
    /// </summary>
    class KeyTextFileMaker
    {
        private IEnumerable<string> patternWords;
        private IWordComparator comparator;

        /// <summary>
        /// Конструктор класса. Инициализирует строковые данные для шаблона и конкретезирующий правила сравнения для строк 
        /// </summary>
        public KeyTextFileMaker(IEnumerable<string> patternWords, IWordComparator comparator)
        {
            if (patternWords == null)
                throw new ArgumentNullException(nameof(patternWords), "Значение не может быть null");
            if (comparator == null)
                throw new ArgumentNullException(nameof(patternWords), "Значение не может быть null");
            this.patternWords = patternWords;
            this.comparator = comparator;
        }

        /// <summary>
        /// метод, создающий паттерн сравнения. 
        /// </summary>  
        /// <returns>Перечислитель, из строк, предоставляющий готовый шаблон для сравнения,
        /// удовлетворяющий условию валидации </returns>
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
        /// <summary>
        /// метод, проверяющий пригодность данных для шаблона в зависимости от типа валидации. 
        /// </summary>  
        /// <returns>true - если валидация прошла успешно, в противном случае false</returns>
        public bool ValidateKeyFile(IValidatePatternFile validateMethod)
        {
            return validateMethod.GetValidate(patternWords, comparator);
        }
    }
}
