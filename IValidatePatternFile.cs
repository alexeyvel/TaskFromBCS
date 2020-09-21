using System.Collections.Generic;

namespace TaskFromBCS
{
    /// <summary>
    /// Интерфейс абстрагирующий реализацию правил по которым происходит валидация файла шаблона для сравнения. 
    /// </summary>
    interface IValidatePatternFile
    {
        /// <summary>
        /// Метод, производящий валидацию данных из файла шаблона. 
        /// </summary>  
        /// <param name="data">Перечислитель, из строк, предоставляющий данные для валидации</param>
        /// <param name="comparator">Интерфейс по которому происходит сравнение данных при валидации</param>
        /// <returns>true - если валидация прошла успешно, в противном случае false</returns>
        public bool GetValidate(IEnumerable<string> data, IWordComparator comparator);
    }
}
