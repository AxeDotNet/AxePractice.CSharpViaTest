using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharpViaTest.Collections.Annotations;
using CSharpViaTest.Collections.Helpers;
using Xunit;

namespace CSharpViaTest.Collections._30_MapReducePractices
{
    [Medium]
    public class CountNumberOfWordsInMultipleTextFiles
    {
        #region Please modifies the code to pass the test

        // You can add additional functions for readability and performance considerations.

        static int CountNumberOfWords(IEnumerable<Stream> streams)
        {
            throw new NotImplementedException();
        }

        #endregion

        [Fact]
        public void should_count_number_of_words()
        {
            const int fileCount = 5;
            const int wordsInEachFile = 10;

            Stream[] streams = Enumerable
                .Repeat(0, fileCount)
                .Select(_ => TextStreamFactory.Create(wordsInEachFile))
                .ToArray();

            int count = CountNumberOfWords(streams);

            Assert.Equal(fileCount * wordsInEachFile, count);

            foreach (Stream stream in streams) { stream.Dispose(); }
        }
    }
}