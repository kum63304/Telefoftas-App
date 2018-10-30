using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TelesoftasApp.Models
{
    public class FileProcessingService : IFileProcessingService
    {
        private static string SplitRegularExpression = "[ \n\r\t]+";
        private static string separator = " ";

        public string WordsToLines(IEnumerable<string> words, int maxLineLenght)
        {
            
            StringBuilder lines = new StringBuilder();
            while (words.Any())
            {
                int currentLineLength = 0;
                var tookWords = words.TakeWhile(w => (currentLineLength += w.Length + separator.Length) <= maxLineLenght).ToList();
                if (tookWords.Count() == 0)
                {
                    String longWord = words.ElementAt(0);
                    String line = longWord.Substring(0, maxLineLenght);
                    String residual = longWord.Substring(maxLineLenght);
                    lines.AppendLine(line);
                    if (String.IsNullOrEmpty(residual))
                        words = words.Skip(1);
                    else
                        words = words.Replace(0, residual);
                }
                else
                {
                    string line = String.Join(separator, tookWords);
                    words = words.Skip(tookWords.Count());
                    lines.AppendLine(line);
                }
            }
            return lines.ToString();
           
        }
      

        public IEnumerable<string> SplitToWords(String context)
        {
            if (String.IsNullOrWhiteSpace(context))
            {
                return Enumerable.Empty<string>();
            }
            return Regex.Split(context, SplitRegularExpression)
                .Where(s => !String.IsNullOrWhiteSpace(s));

        }
        public void SaveResultsToFile(string filePath,string context)
        {
            String dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(filePath, context);
        }
        public string LoadFileContext(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                throw new FileNotFoundException(filePath);
            }
        }
    }
}
