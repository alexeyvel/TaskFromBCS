using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskFromBCS
{
    class TextParseAndRead
    {
        private ITextParserRules textParserPattern;       
        public TextParseAndRead(ITextParserRules textParserPattern)
        {
            if (textParserPattern != null)
                this.textParserPattern = textParserPattern;
            else throw new ArgumentNullException(nameof(textParserPattern), "Значение не может быть null");        
        }

        public IEnumerable<string> ReadByRules(ITextParserRules parserRules)
        {
            return parserRules.ReadByRules();
        }

        public async Task<IEnumerable<string>> ReadByRulesAsync(ITextParserRules parserRules)
        {
            return await Task.Run(() => parserRules.ReadByRules());
        }
    }
}
