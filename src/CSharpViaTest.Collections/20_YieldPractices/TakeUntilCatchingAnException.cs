using System;
using System.Collections.Generic;
using System.Linq;
using CSharpViaTest.Collections.Annotations;
using Xunit;

namespace CSharpViaTest.Collections._20_YieldPractices
{
    [Medium]
    public class TakeUntilCatchingAnException
    {
        readonly int indexThatWillThrow = new Random().Next(2, 10);

        IEnumerable<int> GetSequenceOfData()
        {
            for (int i = 0;; ++i)
            {
                if (i == indexThatWillThrow) { throw new Exception("An exception is thrown"); }
                yield return i;
            }
        }

        #region Please modifies the code to pass the test 

        static IEnumerable<int> TakeUntilError(IEnumerable<int> sequence)
        {
            using (IEnumerator<int> iterator = sequence.GetEnumerator())
            {
                while (true)
                {
                    try
                    {
                        if (!iterator.MoveNext()) { yield break; }
                    }
                    catch (Exception)
                    {
                        yield break;
                    }

                    yield return iterator.Current;
                }
            }
        }

        #endregion

        [Fact]
        public void should_get_sequence_until_an_exception_is_thrown()
        {
            IEnumerable<int> sequence = TakeUntilError(GetSequenceOfData());
            Assert.Equal(Enumerable.Range(0, indexThatWillThrow), sequence);
        }
    }
}