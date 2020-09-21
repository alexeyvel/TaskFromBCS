using SimpleHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TaskFromBCS
{
    class Program
    {
        private static Encoding keyFileEncoding;
        private static Encoding sourceFileEncoding;
        private static IList<WordStats> wordStats;
        private static IList<WordStats> generalWordStats;
        private static IList<IList<WordStats>> totalStats;
        private static IEnumerable<string> patternParsingData;
        private static IEnumerable<string> sourceParsingData;
        private static IEnumerable<string> fullSourceFilenames;
        private static IWordComparator wordComparisonRules;
        private static string fullKeyFilename;
        private static bool ignoreCase;
        private static bool isGeneralStatistic;

        /// <summary>
        /// Метод, печатающий служебные сообщения в консоль. 
        /// </summary>  
        /// <param name="text">Текст сообщения</param>
        /// <param name="color">Цвет сообщения</param>
        private static void ShowMessageBox(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        /// <summary>
        /// Метод, инициализирующий директорию с файлами для парсинга. 
        /// </summary>  
        /// <param name="pathToFolder">Директория для парсинга</param>
        private static void InitialSourceFilesDirectory(string pathToFolder)
        {
            if (Directory.Exists(pathToFolder))
            {
                fullSourceFilenames = Directory.EnumerateFiles(pathToFolder, "*.txt", SearchOption.TopDirectoryOnly);
                if (fullSourceFilenames.Count() == 0)
                    throw new ArgumentException("В указанной директори отсутствуют файлы с расширением \".txt\"", nameof(pathToFolder));
            }
            else throw new ArgumentException("Указанная директория не существует", nameof(pathToFolder));
        }

        /// <summary>
        /// Метод, инициализирующий полный путь к файлу шаблону. 
        /// </summary>  
        /// <param name="fullFilename">Полное имя файла шаблона</param>
        private static void InitialKeyFullFilename(string fullFilename)
        {
            if (File.Exists(fullFilename))
            {
                if (fullFilename.EndsWith(@".txt", StringComparison.OrdinalIgnoreCase))
                    fullKeyFilename = fullFilename;
                else throw new ArgumentException("Указанный файл не является файлом расширеня \".txt\"", nameof(fullFilename));
            }
            else throw new ArgumentException("Файл по указанной дирректории не найден", nameof(fullFilename));
        }

        /// <summary>
        /// Метод, заполняющий лист стаистики вхождения слов для одного файла. 
        /// </summary>  
        /// <param name="sourceFiles">Данные с текущего считанного файла</param>
        /// <param name="pathToTheComparedFile">Полное име текущего считанного файла</param>
        private static void CreateSingleStats(IEnumerable<string> sourceFiles, IWordComparator wordComparator, string pathToTheComparedFile)
        {
            for (int i = 0; i < patternParsingData.Count(); i++)
            {
                var currentStats = new WordStats(pathToTheComparedFile, patternParsingData.ElementAt(i), 0);
                wordStats.Add(currentStats);
                foreach (var word in sourceFiles)
                {
                    if (wordComparator.CompareWords(word, patternParsingData.ElementAt(i)))
                    {
                        wordStats[i].Count++;
                    }
                }
            }
        }

        /// <summary>
        /// Метод, заполняющий общий лист стаистики вхождения слов для всех файлов. 
        /// </summary>  
        /// <param name="wordStats">Список со списками статистик по всем файлам</param>
        private static void CreateGeneralStats(IEnumerable<IEnumerable<WordStats>> wordStats)
        {
            generalWordStats = new List<WordStats>(); ;
            for (int j = 0; j < wordStats.ElementAt(0).Count(); j++)
            {
                var result = new WordStats("General Statistic", wordStats.ElementAt(0).ElementAt(j).Word, 0);
                for (int i = 0; i < wordStats.Count(); i++)
                {
                    result += wordStats.ElementAt(i).ElementAt(j);
                }
                generalWordStats.Add(result);
            }
        }
        /// <summary>
        /// Метод, печатающий выбранную статистику в зависимости от выбранного типа формирования (общую или по файлам). 
        /// </summary>
        private static void SelectOutputType(IWritable printer, bool typeStatistic)
        {
            if (typeStatistic)
            {
                CreateGeneralStats(totalStats);
                printer.WriteData(generalWordStats);
            }
            else
            {
                foreach(var stats in totalStats)
                    printer.WriteData(stats);
            } 
        }
        static void Main(string[] args)
        {
            var provader = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provader);

            bool isValidInputData = false;
            ShowMessageBox(AppMessages.greetingTile, ConsoleColor.DarkCyan);

            while (!isValidInputData)
            {
                ShowMessageBox(AppMessages.requestForInputSourceFiles);
                string pathToFolder = Console.ReadLine();
                try
                {
                    InitialSourceFilesDirectory(pathToFolder);
                    isValidInputData = true;
                }
                catch (Exception ex)
                {
                    ShowMessageBox(ex.Message, ConsoleColor.DarkRed);
                }
            }

            isValidInputData = false;
            while (!isValidInputData)
            {
                ShowMessageBox(AppMessages.requestForInputKeyFile);
                string fullFilename = Console.ReadLine();
                try
                {
                    InitialKeyFullFilename(fullFilename);
                    isValidInputData = true;
                }
                catch (Exception ex)
                {
                    ShowMessageBox(ex.Message, ConsoleColor.DarkRed);
                }
            }

            isValidInputData = false;
            while (!isValidInputData)
            {
                ShowMessageBox(AppMessages.requestForSelectCase);
                var keyPressed = Console.ReadKey(true);
                if (keyPressed.Key == ConsoleKey.D0 || keyPressed.Key == ConsoleKey.NumPad0)
                {
                    ignoreCase = false;
                    isValidInputData = true;
                }
                else if (keyPressed.Key == ConsoleKey.D1 || keyPressed.Key == ConsoleKey.NumPad1)
                {
                    ignoreCase = true;
                    isValidInputData = true;
                }
            }
            ShowMessageBox(AppMessages.waitValidation);
            isValidInputData = false;

            while (!isValidInputData)
            {
                try
                {
                    keyFileEncoding = FileEncoding.DetectFileEncoding(fullKeyFilename);
                    var parserRuls = new ReadOnlyWords(fullKeyFilename, keyFileEncoding);
                    var patternParsing = new TextParseAndRead(parserRuls);
                    patternParsingData = patternParsing.ReadByRules(parserRuls);
                    wordComparisonRules = new TwoWordsComparator(ignoreCase);
                    var patternMaker = new KeyTextFileMaker(patternParsingData, wordComparisonRules);
                    var validatePattern = new ValidateKeyFileOnlyUniqueWords();
                    if (patternMaker.ValidateKeyFile(validatePattern))
                    {
                        isValidInputData = true;
                    }
                    else
                    {
                        ShowMessageBox(AppMessages.failureValidation, ConsoleColor.DarkRed);
                        ShowMessageBox(AppMessages.requestForInputKeyFile);
                        string fullFilename = Console.ReadLine();
                        InitialKeyFullFilename(fullFilename);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            ShowMessageBox(AppMessages.successValidation, ConsoleColor.DarkGreen);
            ShowMessageBox(AppMessages.comparingWait);
            totalStats = new List<IList<WordStats>>();
            foreach (var currentFile in fullSourceFilenames)
            {
                try
                {
                    if (currentFile == fullKeyFilename)
                        continue;
                    sourceFileEncoding = FileEncoding.DetectFileEncoding(currentFile);
                    var parserRuls = new ReadOnlyWords(currentFile, sourceFileEncoding);
                    var sourceParsing = new TextParseAndRead(parserRuls);
                    ShowMessageBox(AppMessages.fileIsReadNow + $"{currentFile}");
                    sourceParsingData = sourceParsing.ReadByRules(parserRuls);
                    wordStats = new List<WordStats>();
                    CreateSingleStats(sourceParsingData, wordComparisonRules, currentFile);
                    totalStats.Add(wordStats);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
            isValidInputData = false;
            while (!isValidInputData)
            {
                ShowMessageBox(AppMessages.requestForOutputStatistic);
                var keyPressed = Console.ReadKey(true);
                if (keyPressed.Key == ConsoleKey.D0 || keyPressed.Key == ConsoleKey.NumPad0)
                {
                    isGeneralStatistic = false;
                    isValidInputData = true;
                }
                else if (keyPressed.Key == ConsoleKey.D1 || keyPressed.Key == ConsoleKey.NumPad1)
                {
                    isGeneralStatistic = true;
                    isValidInputData = true;
                }
            }

            isValidInputData = false;
            while (!isValidInputData)
            {
                try
                {
                    ShowMessageBox(AppMessages.requestForShowStatistic);
                    var keyPressed = Console.ReadKey(true);
                    if (keyPressed.Key == ConsoleKey.D0 || keyPressed.Key == ConsoleKey.NumPad0)
                    {
                        ShowMessageBox(AppMessages.requestForFileNameStats);
                        var pathToSave = Console.ReadLine();
                        SelectOutputType(new WriteInFile(pathToSave, keyFileEncoding), isGeneralStatistic);
                        isValidInputData = true;
                    }
                    else if (keyPressed.Key == ConsoleKey.D1 || keyPressed.Key == ConsoleKey.NumPad1)
                    {
                        SelectOutputType(new WriteInCansole(), isGeneralStatistic);
                        isValidInputData = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            ShowMessageBox(AppMessages.endSession);
        }
    }
}

