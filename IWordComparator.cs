namespace TaskFromBCS
{
    interface IWordComparator
    {
        public bool IgnoreCase { get; set; }
        public bool CompareWords(string targetWord, string patternWord);
    }
}
