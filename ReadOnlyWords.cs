using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TaskFromBCS
{
    /// <summary>
    /// Класс который считывает только слова - буквенно-цифровые символы, разделенные знаками пунктуации
    /// </summary>
    class ReadOnlyWords : ITextParserRules
    {
        
        private static string pattern = @"\b[\p{L}\d]\b";
        private List<string> resultTextData;
        private List<char> temporalyBuffer;
        /// <value>Полное имя файла для чтения</value>
        public string FileForParsing { get; private set; }
        /// <value>Предоставляет данные о кодировке считываемого файла</value>
        public Encoding CurrentEncoding { get; private set; }

        /// <summary>
        /// Конструктор класса. Инициализирует данные о полном имени считываемого файла и его кодировке 
        /// </summary>
        public ReadOnlyWords(string fileForParsing, Encoding encoding)
        {
            if (File.Exists(fileForParsing))
                FileForParsing = fileForParsing;
            else throw new ArgumentException("Файл по указанной дирректории не найден", nameof(fileForParsing));
            if (encoding != null)
                CurrentEncoding = encoding;
            else throw new ArgumentNullException(nameof(encoding), "Значение не может быть null");
            
        }
        ///<inheritdoc/>
        ///<remarks>Метод считывет только слова, игнорируя знаки пунктуации и пробелы</remarks>
        public IEnumerable<string> ReadByRules()
        {
            using (StreamReader reader = new StreamReader(FileForParsing, CurrentEncoding, true))
            {
                Regex parsingRules = new Regex(pattern);
                resultTextData = new List<string>();
                temporalyBuffer = new List<char>();
                while (!reader.EndOfStream)
                {
                    string currentLetter = ((char)reader.Read()).ToString();
                    if (parsingRules.IsMatch(currentLetter))
                        temporalyBuffer.Add(currentLetter[0]);
                    else
                    {
                        if (temporalyBuffer.Count > 0)
                        {
                            resultTextData.Add(new string(temporalyBuffer.ToArray()));
                            temporalyBuffer.Clear();
                        }
                    }
                }
                if (temporalyBuffer.Count > 0)
                {
                    resultTextData.Add(new string(temporalyBuffer.ToArray()));
                    temporalyBuffer.Clear();
                }
                return resultTextData;
            }
        }
    }
}
