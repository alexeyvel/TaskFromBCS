using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskFromBCS
{
    /// <summary>
    /// Класс выводящий данные статистики в консоль
    /// </summary>
    class WriteInCansole : IWritable
    {       
        public void WriteData(IEnumerable<WordStats> data)
        {
            int bufferSize = data.Count();
            if (bufferSize > Console.WindowHeight)
                Console.BufferHeight = bufferSize;
            Console.WriteLine($"Статистика для файла: {data.ElementAt(0).FileName}");
            foreach (var text in data)
                Console.WriteLine($"{text.Word} - {text.Count}");
        }
    }
}
