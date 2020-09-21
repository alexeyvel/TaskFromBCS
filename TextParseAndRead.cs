using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskFromBCS
{
    /// <summary>
    /// Класс контейнер для различных вариантов считывания файлов
    /// </summary>
    class TextParseAndRead
    {
        private ITextParserRules textParserPattern;
        /// <summary>
        /// Конструктор класса. Устанавливает набор правил по которым происходит считывание файла.
        /// </summary>
        public TextParseAndRead(ITextParserRules textParserPattern)
        {
            SetParserPattern(textParserPattern);      
        }
        /// <summary>
        /// Метод, устанавливающий набор парвил для считывания. 
        /// </summary> 
        public void SetParserPattern(ITextParserRules textParserPattern)
        {
            if (textParserPattern != null)
                this.textParserPattern = textParserPattern;
            else throw new ArgumentNullException(nameof(textParserPattern), "Значение не может быть null");
        }
        /// <summary>
        /// Метод, производящий чтение файла по заданным правилам. 
        /// </summary>  
        /// <returns>Перечислитель, считанных по определенным правилам строк</returns>
        public IEnumerable<string> ReadByRules(ITextParserRules parserRules)
        {
            return parserRules.ReadByRules();
        }
        /// <summary>
        /// Метод, производящий чтение файла по заданным правилам асинхронно. 
        /// </summary>  
        /// <returns>Перечислитель, считанных по определенным правилам строк</returns>
        public async Task<IEnumerable<string>> ReadByRulesAsync(ITextParserRules parserRules)
        {
            return await Task.Run(() => parserRules.ReadByRules());
        }
    }
}
