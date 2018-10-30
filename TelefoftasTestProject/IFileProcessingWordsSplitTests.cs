using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelesoftasApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace TelefoftasApp.Tests
{
    [TestClass]
    public class IFileProcessingWordsSplitTests
    {
        private readonly IFileProcessingService fileProcessingService;

        public IFileProcessingWordsSplitTests()
        {
            fileProcessingService = new FileProcessingService();
        }

        [TestMethod]
        [TestCategory("Words split")]
        public void WordsSplit_MustToSplitCorrectly()
        {
            String context="design pattern is a general  repeatable     solution...";
            IEnumerable<string> words= fileProcessingService.SplitToWords(context);
            
            Assert.AreEqual(7,words.Count(),"Words count do not match");

            AssertSplitIsIncorrect(words, 0, "design");
            AssertSplitIsIncorrect(words, 1, "pattern");
            AssertSplitIsIncorrect(words, 2, "is");
            AssertSplitIsIncorrect(words, 3, "a");
            AssertSplitIsIncorrect(words, 4, "general");
            AssertSplitIsIncorrect(words, 5, "repeatable");
            AssertSplitIsIncorrect(words, 6, "solution...");
       
        }
        [TestMethod]
        [TestCategory("Words split")]
        public void WordsSplit_MustSplitOneWord()
        {
            String context = "Mantas";
            IEnumerable<string> words = fileProcessingService.SplitToWords(context);
            Assert.AreEqual(1, words.Count(), "Context have one word");
        }
        [TestMethod]
        [TestCategory("Words split")]
        public void WordsSplit_MustWorkWithEmptyContext()
        {
            String context = "";
            IEnumerable<string> words = fileProcessingService.SplitToWords(context);
            Assert.AreEqual(0, words.Count(), "Context do not have any words");
        }
        [TestMethod]
        [TestCategory("Words split")]
        public void WordsSplit_MustWorkWorkWithWhiteSpace()
        {
            String context = @"           

                            ";
            IEnumerable<string> words = fileProcessingService.SplitToWords(context);
            Assert.AreEqual(0, words.Count(), "Context do not have any words");
        }
        [TestMethod]
        [TestCategory("Words split")]
        public void WordsSplit_MustWorkWithNullContext()
        {
            String context = null;
            IEnumerable<string> words = fileProcessingService.SplitToWords(context);
            Assert.AreEqual(0, words.Count(), "Context do not have any words");
        }

        private void AssertSplitIsIncorrect(IEnumerable<string> words, int actualWordIndex, string expectedWord)
        {
            Assert.AreEqual( expectedWord,words.ElementAt(actualWordIndex), "Words split not works correctly");
        }

    }
}

