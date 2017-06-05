using System.Collections.Generic;
using System.Linq;
using CSharpViaTest.Collections.Helpers;
using Xunit;

namespace CSharpViaTest.Collections._30_MapReducePractices
{
    /* 
     * Description
     * ===========
     * 
     * This test will get the maximum number in multiple sequences. You have to complete
     * the test in just one statement.
     * 
     * Difficulty: Medium
     * 
     * Requirement
     * ===========
     * 
     * - No `for`, `foreach` or other loop keywords are allowed to use.
     */
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
            return collections
                .Select(c => c.Max(i => i))
                .Max(i => i);
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