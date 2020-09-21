using System.Collections.Generic;

namespace TaskFromBCS
{
    interface ITextParserRules
    {
        public IEnumerable<string> ReadByRules();
    }
}
