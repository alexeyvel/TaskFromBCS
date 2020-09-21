using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TaskFromBCS
{
    class WriteInFile : IWritable
    {
        private string fileName;
        public WriteInFile(string fileName, Encoding encoding)
        {

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName), "Значение не может быть null");
            }
            this.fileName = fileName;
        }
        public void WriteData(IEnumerable<WordStats> data)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine($"Статистика для файла: {data.ElementAt(0).FileName}");
                foreach (var text in data)
                    writer.WriteLine(text.Word + " - " +  text.Count);
            }
        }
    }
}
