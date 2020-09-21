using System.Collections.Generic;

namespace TaskFromBCS
{
    /// <summary>
    /// Интерфейс абстрагирующий реализацию правил по которым считывается файл. 
    /// </summary>
    interface ITextParserRules
    {
        /// <summary>
        /// Метод, производящий чтение файла по заданным правилам. 
        /// </summary>       
        /// <returns>Перечислитель, считанных по определенным правилам строк</returns>
        public IEnumerable<string> ReadByRules();
    }
}
