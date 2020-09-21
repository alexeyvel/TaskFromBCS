namespace TaskFromBCS
{
    /// <summary>
    /// Интерфейс абстрагирующий реализацию правил по которым происходит сравнение данных типа string. 
    /// </summary>
    interface IWordComparator
    {
        /// <value>Свойство, отвечающее за учитывание или  игнорирование регистра при сравнении текстовых строк (true - если регистр игнорируется)</value>
        public bool IgnoreCase { get; set; }
        /// <summary>
        /// метод, производящий сравнение двух строк. 
        /// </summary>  
        /// <param name="targetWord">сравниваемая строка</param>
        /// <param name="patternWord">строка - шаблон для сравнения</param>
        /// <returns>true - если сравнение прошло успешно, в противном случае false</returns>
        public bool CompareWords(string targetWord, string patternWord);
    }
}
