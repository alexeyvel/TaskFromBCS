using System.Collections.Generic;

namespace TaskFromBCS
{
    /// <summary>
    /// Интерфейс абстрагирующий реализацию вывода обработанных данных пользователю. 
    /// </summary>
    interface IWritable
    {
        /// <summary>
        /// метод, производящий вывод данных пользователю. 
        /// </summary>
        /// <param name="data">данные для вывода пользователю</param>
        public void WriteData(IEnumerable<WordStats> data);
    }
}
