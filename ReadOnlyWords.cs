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
        private Regex parsingRules; 
        private List<string> resultTextData;
        private List<char> temporalyBuffer;

        public IEnumerable<string> ReadByRules(StreamReader reader)
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
            reader.Dispose();
            return resultTextData;
        }
    }
}
