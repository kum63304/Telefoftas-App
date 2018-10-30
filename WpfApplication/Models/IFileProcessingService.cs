using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelesoftasApp.Models
{
    public interface IFileProcessingService
    {
        IEnumerable<string> SplitToWords(String context);
        string WordsToLines(IEnumerable<string> words, int maxLineLenght);
        void SaveResultsToFile(string filePath,String context);
        String LoadFileContext(string filePath);
    }
}
