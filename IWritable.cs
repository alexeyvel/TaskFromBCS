using System.Collections.Generic;

namespace TaskFromBCS
{
    interface IWritable
    {
        public void WriteData(IEnumerable<WordStats> data);
    }
}
