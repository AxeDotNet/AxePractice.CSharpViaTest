using System;
using System.Collections.Generic;
using System.Linq;
using CSharpViaTest.Collections.Annotations;
using CSharpViaTest.Collections.Helpers;
using Xunit;

namespace CSharpViaTest.Collections._30_MapReducePractices
{
    [SuperEasy]
    public class GetMaxNumbersInMultipleCollections
    {
        static IEnumerable<IEnumerable<int>> CreateStreamsContainingMaxNumber(int maxNumber)
        {
            return Enumerable
                .Range(0, 10)
                .Select(reduced => maxNumber - reduced)
                .Select(max => NumberStreamFactory.CreateWithTopNumber(max, 100000));
        }

        #region Please modifies the code to pass the test

        static int GetMaxNumber(IEnumerable<IEnumerable<int>> collections)
        {
            throw new NotImplementedException();
        }

        #endregion

        [Fact]
        public void should_get_max_numbers_in_collections()
        {
            IEnumerable<IEnumerable<int>> streams = CreateStreamsContainingMaxNumber(100);
            int maxNumber = GetMaxNumber(streams);
            Assert.Equal(100, maxNumber);
        }
    }
}