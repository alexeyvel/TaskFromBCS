using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TaskFromBCS
{
    class TextParseAndRead
    {
        public string FileForParsing { get; private set; }
        public Encoding CurrentEncoding { get; private set; }
        public TextParseAndRead(string fileForParsing, Encoding encoding)
        {
            if (File.Exists(fileForParsing))
                FileForParsing = fileForParsing;
            else throw new ArgumentException("Файл по указанной дирректории не найден", nameof(fileForParsing));
            if (encoding != null)
                CurrentEncoding = encoding;
            else throw new ArgumentNullException(nameof(encoding), "Значение не может быть null");
        }

        public IEnumerable<string> ReadByRules(ITextParserRules parserRules)
        {
            using (StreamReader reader = new StreamReader(FileForParsing, CurrentEncoding, true))
            {
                return parserRules.ReadByRules(reader);
            }
        }

        public async Task<IEnumerable<string>> ReadByRulesAsync(ITextParserRules parserRules)
        {
            using (StreamReader reader = new StreamReader(FileForParsing, CurrentEncoding, true))
            {
                return await Task.Run(() => parserRules.ReadByRules(reader));
            }
        }

    }
}
