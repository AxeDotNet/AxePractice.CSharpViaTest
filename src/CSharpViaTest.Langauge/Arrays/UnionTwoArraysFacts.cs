using System;
using System.Linq;
using Xunit;

namespace CSharpViaTest.Langauge.Arrays
{
    public class UnionTwoArraysFacts
    {
        static T[] UnionArray<T>(T[] left, T[] right)
        {
            #region Please implement the method

            // You cannot use Union method provided by LINQ. You should write
            // your own implementation.
            // Note: A List<T> can be used to dynamically add/remove items.

            throw new NotImplementedException();

            #endregion
        }

        [Fact]
        public void should_combine_left_and_right()
        {
            var left = new[] {1, 2};
            var right = new[] {3, 4};

            int[] union = UnionArray(left, right);

            Assert.Equal(4, union.Length);
            Assert.Empty(new [] {1,2,3,4}.Except(union));
        }

        [Fact]
        public void should_merge_duplicates()
        {
            var left = new[] { 1, 2 };
            var right = new[] { 3, 2 };

            int[] union = UnionArray(left, right);

            Assert.Equal(3, union.Length);
            Assert.Empty(new[] { 1, 2, 3 }.Except(union));
        }

        [Fact]
        public void should_support_empty_array()
        {
            var left = Array.Empty<int>();
            var right = new[] { 3, 2 };

            int[] union = UnionArray(left, right);

            Assert.Equal(2, union.Length);
            Assert.Empty(new[] { 2, 3 }.Except(union));
        }

        [Fact]
        public void should_support_empty_arrays()
        {
            var left = Array.Empty<int>();
            var right = Array.Empty<int>();

            int[] union = UnionArray(left, right);
            
            Assert.Empty(union);
        }

        [Fact]
        public void should_throw_if_null()
        {
            int[] left = null;
            var right = new[] {1, 2};

            Assert.Throws<ArgumentNullException>(nameof(left), () => UnionArray(left, right));
        }
    }
}