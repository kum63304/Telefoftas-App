using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelesoftasApp.Models;
using System.Collections.Generic;

namespace TelefoftasApp.Tests
{
    [TestClass]
    public class IFileProcessingTakeWordsFromListTests
    {
        private readonly IFileProcessingService fileProcessingService;

        public IFileProcessingTakeWordsFromListTests()
        {
            fileProcessingService = new FileProcessingService();
        }
        [TestMethod]
        [TestCategory("Take words from list")]
        public void TakeWordsFromList_MustNotSplitWords()
        {
            IEnumerable<string> words = new string[] { "123", "45", "678", "9ABCD"};

            String lines = fileProcessingService.WordsToLines(words, 7);
            Assert.AreEqual(
@"123 45
678
9ABCD
", lines, "Line do not match");
            
        }
        [TestMethod]
        [TestCategory("Take words from list")]
        public void TakeWordsFromList_MustReturnWordsIfLineToLong()
        {
            IEnumerable<string> words = new string[] { "123", "45"};
            String lines = fileProcessingService.WordsToLines(words, 10);
            Assert.AreEqual(
@"123 45
", lines, "Line do not match");
        }
        [TestMethod]
        [TestCategory("Take words from list")]
        public void TakeWordsFromList_MustBreakLongWords()
        {
            IEnumerable<string> words = new string[] { "123456789","A","1234567890" };
            String lines = fileProcessingService.WordsToLines(words,6);
            Assert.AreEqual(
@"123456
789 A
123456
7890
", lines, "Line do not match");
        }
  
        
    }
}
