using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CSharpViaTest.Collections.Annotations;
using Xunit;

namespace CSharpViaTest.Collections._20_YieldPractices
{
    [Medium]
    public class FibonacciEnumerable
    {
        #region Please modifies the code to pass the test

        [SuppressMessage("ReSharper", "IteratorNeverReturns", Justification = "It is indeed an infinit sequence")]
        static IEnumerable<long> GetFibonacciIntegers()
        {
            for (long current = 1, next = 1;;) {
                yield return current;
                long newValue = current + next;
                current = next;
                next = newValue;
            }
        }
        
        #endregion

        [Fact]
        public void should_get_fibonacci_integers()
        {
            IEnumerable<long> fibonacciIntegers = GetFibonacciIntegers();
            IEnumerable<long> firstSix = fibonacciIntegers.Take(6);

            Assert.Equal(new [] {1L, 1, 2, 3, 5, 8}, firstSix);
        }

        [Fact]
        public void should_not_get_concrete_collection()
        {
            IEnumerable<long> fib = GetFibonacciIntegers();

            Assert.False(fib is ICollection<long>);
        }
    }
}