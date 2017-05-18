using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            return streams
                .Select(stream => new StreamReader(stream, Encoding.UTF8, false, 1024, true))
                .SelectMany(EnumerateLines)
                .Sum(CountWordsInLine);
        }

        static int CountWordsInLine(string line)
        {
            return line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        static IEnumerable<string> EnumerateLines(TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
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