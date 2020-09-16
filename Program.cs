using SimpleHelpers;
using System;
using System.IO;
using System.Text;

namespace TaskFromBCS
{
    class Program
    {
        static void Main(string[] args)
        {
            var provader = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provader);

            Console.WriteLine("Hello World!");
            try
            {
                
                string sourceFile_1 = @"e:\test\mix.txt";
                string sourceFile_2 = @"e:\test\book.txt";
                string sourceFile_3 = @"e:\test\tbook.txt";

                var encoding = FileEncoding.DetectFileEncoding(sourceFile_1);
                var myText = new TextParseAndRead(sourceFile_3, encoding);
                Console.WriteLine($"Операция чтения начата");
                
                var finalText = myText.ReadByRulesAsync(new ReadOnlyWords());
                Console.WriteLine($"Операция чтения завершена");
                using (TextWriter writer = new StreamWriter(@"e:\test\result.txt", false, Encoding.Unicode))
                {
                    Console.WriteLine($"Операция записи начата");
                    foreach (var word in finalText.Result)
                            writer.WriteLine(word);
                    Console.WriteLine($"Операция записи завершена");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
