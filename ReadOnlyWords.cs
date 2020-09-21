using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TaskFromBCS
{
    class ReadOnlyWords : ITextParserRules
    {
        
        private static string pattern = @"\b[\p{L}\d]\b";
        private List<string> resultTextData;
        private List<char> temporalyBuffer;
        public string FileForParsing { get; private set; }
        public Encoding CurrentEncoding { get; private set; }
        public ReadOnlyWords(string fileForParsing, Encoding encoding)
        {
            if (File.Exists(fileForParsing))
                FileForParsing = fileForParsing;
            else throw new ArgumentException("Файл по указанной дирректории не найден", nameof(fileForParsing));
            if (encoding != null)
                CurrentEncoding = encoding;
            else throw new ArgumentNullException(nameof(encoding), "Значение не может быть null");
            
        }
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
