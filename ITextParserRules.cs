using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TaskFromBCS
{
    interface ITextParserRules
    {
        public IEnumerable<string> ReadByRules(StreamReader reader);
    }
}
