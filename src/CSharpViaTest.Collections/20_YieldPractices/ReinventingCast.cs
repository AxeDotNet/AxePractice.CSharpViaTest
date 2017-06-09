using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace CSharpViaTest.Collections._20_YieldPractices
{
    /* 
     * Description
     * ===========
     * 
     * Please reinvent Cast<T> extension method. Casts the elements of an IEnumerable to
     * the specified type.
     * 
     * Difficulty: Super Easy
     * 
     * Requirement
     * ===========
     * 
     * - No LINQ method is allowed to use.
     * - You can add helper method within region. But you cannot modify code outside the
     *   region.
     */
    static partial class ReinventingLinq
    {
        #region Please modifies the code to pass the test

        public static IEnumerable<TResult> MyCast<TResult>(this IEnumerable source)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class ReinventingCast
    {
        [Fact]
        [SuppressMessage("ReSharper", "UnusedVariable")]
        public void cast_int_to_long_throws()
        {
            var q = from x in new[] { 9999, 0, 888, -1, 66, -777, 1, 2, -12345 }
                where x > int.MinValue
                select x;

            var rst = q.MyCast<long>();

            Assert.Throws<InvalidCastException>(() => { foreach (long t in rst) { } });
        }

        [Fact]
        [SuppressMessage("ReSharper", "UnusedVariable")]
        public void cast_byte_to_ushort_throws()
        {
            var q = from x in new byte[] { 0, 255, 127, 128, 1, 33, 99 }
                select x;

            var rst = q.MyCast<ushort>();
            Assert.Throws<InvalidCastException>(() => { foreach (ushort t in rst) { } });
        }

        [Fact]
        public void should_get_empty_result()
        {
            object[] source = { };
            Assert.Empty(source.MyCast<int>());
        }

        [Fact]
        public void nullable_int_from_appropriate_objects()
        {
            int? i = 10;
            object[] source = { -4, 1, 2, 3, 9, i };
            int?[] expected = { -4, 1, 2, 3, 9, i };

            Assert.Equal(expected, source.MyCast<int?>());
        }
        
        [Fact]
        public void long_from_nullable_int_in_objects_throws()
        {
            int? i = 10;
            object[] source = { -4, 1, 2, 3, 9, i };

            IEnumerable<long> MyCast = source.MyCast<long>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void long_from_nullable_int_in_objects_including_null_throws()
        {
            int? i = 10;
            object[] source = { -4, 1, 2, 3, 9, null, i };

            IEnumerable<long?> MyCast = source.MyCast<long?>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void nullable_int_from_appropriate_objects_including_null()
        {
            int? i = 10;
            object[] source = { -4, 1, 2, 3, 9, null, i };
            int?[] expected = { -4, 1, 2, 3, 9, null, i };

            Assert.Equal(expected, source.MyCast<int?>());
        }

        [Fact]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void throw_on_uncastable_item()
        {
            object[] source = { -4, 1, 2, 3, 9, "45" };
            int[] expectedBeginning = { -4, 1, 2, 3, 9 };

            IEnumerable<int> MyCast = source.MyCast<int>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
            Assert.Equal(expectedBeginning, MyCast.Take(5));
            Assert.Throws<InvalidCastException>(() => MyCast.ElementAt(5));
        }

        [Fact]
        public void throw_casting_int_to_double()
        {
            int[] source = { -4, 1, 2, 9 };

            IEnumerable<double> MyCast = source.MyCast<double>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        static void TestCastThrow<T>(object o)
        {
            byte? i = 10;
            object[] source = { -1, 0, o, i };

            IEnumerable<T> MyCast = source.MyCast<T>();

            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void throw_on_heterogenous_source()
        {
            TestCastThrow<long?>(null);
            TestCastThrow<long>(9L);
        }

        [Fact]
        public void cast_to_string()
        {
            object[] source = { "Test1", "4.5", null, "Test2" };
            string[] expected = { "Test1", "4.5", null, "Test2" };

            Assert.Equal(expected, source.MyCast<string>());
        }
        
        [Fact]
        public void array_conversion_throws()
        {
            Assert.Throws<InvalidCastException>(() => new[] { -4 }.MyCast<long>().ToList());
        }

        [Fact]
        public void first_element_invalid_for_cast()
        {
            object[] source = { "Test", 3, 5, 10 };

            IEnumerable<int> MyCast = source.MyCast<int>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void last_element_invalid_for_cast()
        {
            object[] source = { -5, 9, 0, 5, 9, "Test" };

            IEnumerable<int> MyCast = source.MyCast<int>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void nullable_int_from_nulls_and_ints()
        {
            object[] source = { 3, null, 5, -4, 0, null, 9 };
            int?[] expected = { 3, null, 5, -4, 0, null, 9 };

            Assert.Equal(expected, source.MyCast<int?>());
        }

        [Fact]
        public void throw_casting_int_to_long()
        {
            int[] source = { -4, 1, 2, 3, 9 };

            IEnumerable<long> MyCast = source.MyCast<long>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void throw_casting_int_to_nullable_long()
        {
            int[] source = { -4, 1, 2, 3, 9 };

            IEnumerable<long?> MyCast = source.MyCast<long?>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void throw_casting_nullable_int_to_long()
        {
            int?[] source = { -4, 1, 2, 3, 9 };

            IEnumerable<long> MyCast = source.MyCast<long>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void throw_casting_nullable_int_to_nullable_long()
        {
            int?[] source = new int?[] { -4, 1, 2, 3, 9, null };

            IEnumerable<long?> MyCast = source.MyCast<long?>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void casting_null_to_nonnullable_is_null_reference_exception()
        {
            int?[] source = new int?[] { -4, 1, null, 3 };
            IEnumerable<int> MyCast = source.MyCast<int>();
            Assert.Throws<NullReferenceException>(() => MyCast.ToList());
        }

        [Fact]
        public void null_source()
        {
            Assert.Throws<ArgumentNullException>(() => ((IEnumerable<object>)null).MyCast<string>());
        }
    }
}