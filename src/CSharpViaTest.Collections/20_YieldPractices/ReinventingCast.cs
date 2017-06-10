﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CSharpViaTest.Collections.Helpers;
using Xunit;

namespace CSharpViaTest.Collections._20_YieldPractices
{
    /* 
     * Description
     * ===========
     * 
     * Please reinvent MyCast<T> extension method. Casts the elements of an IEnumerable to
     * the specified type. The test are introduced from dotnet core framework.
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
            if (source == null) { throw new ArgumentNullException(nameof(source)); }
            return MyCastIterator<TResult>(source);
        }

        static IEnumerable<TResult> MyCastIterator<TResult>(IEnumerable source)
        {
            foreach (object item in source)
            {
                yield return (TResult) item;
            }
        }

        #endregion
    }

    public class ReinventingCast
    {
        [Fact]
        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        [SuppressMessage("ReSharper", "UnusedVariable")]
        public void CastIntToLongThrows()
        {
            IEnumerable<int> q = from x in new[] { 9999, 0, 888, -1, 66, -777, 1, 2, -12345 }
                where x > int.MinValue
                select x;

            IEnumerable<long> rst = q.MyCast<long>();

            Assert.Throws<InvalidCastException>(() => { foreach (long t in rst) { } });
        }

        [Fact]
        [SuppressMessage("ReSharper", "UnusedVariable")]
        public void CastByteToUShortThrows()
        {
            IEnumerable<byte> q = from x in new byte[] { 0, 255, 127, 128, 1, 33, 99 }
                select x;

            IEnumerable<ushort> rst = q.MyCast<ushort>();
            Assert.Throws<InvalidCastException>(() => { foreach (ushort t in rst) { } });
        }

        [Fact]
        public void EmptySource()
        {
            object[] source = { };
            Assert.Empty(source.MyCast<int>());
        }

        [Fact]
        public void NullableIntFromAppropriateObjects()
        {
            int? i = 10;
            object[] source = { -4, 1, 2, 3, 9, i };
            int?[] expected = { -4, 1, 2, 3, 9, i };

            Assert.Equal(expected, source.MyCast<int?>());
        }

        [Fact]
        public void NullableIntFromAppropriateObjectsRunOnce()
        {
            int? i = 10;
            object[] source = { -4, 1, 2, 3, 9, i };
            int?[] expected = { -4, 1, 2, 3, 9, i };

            Assert.Equal(expected, source.RunOnce().MyCast<int?>());
        }

        [Fact]
        public void LongFromNullableIntInObjectsThrows()
        {
            int? i = 10;
            object[] source = { -4, 1, 2, 3, 9, i };

            IEnumerable<long> MyCast = source.MyCast<long>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void LongFromNullableIntInObjectsIncludingNullThrows()
        {
            int? i = 10;
            object[] source = { -4, 1, 2, 3, 9, null, i };

            IEnumerable<long?> MyCast = source.MyCast<long?>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void NullableIntFromAppropriateObjectsIncludingNull()
        {
            int? i = 10;
            object[] source = { -4, 1, 2, 3, 9, null, i };
            int?[] expected = { -4, 1, 2, 3, 9, null, i };

            Assert.Equal(expected, source.MyCast<int?>());
        }

        [Fact]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void ThrowOnUncastableItem()
        {
            object[] source = { -4, 1, 2, 3, 9, "45" };
            int[] expectedBeginning = { -4, 1, 2, 3, 9 };

            IEnumerable<int> MyCast = source.MyCast<int>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
            Assert.Equal(expectedBeginning, MyCast.Take(5));
            Assert.Throws<InvalidCastException>(() => MyCast.ElementAt(5));
        }

        [Fact]
        public void ThrowCastingIntToDouble()
        {
            int[] source = { -4, 1, 2, 9 };

            IEnumerable<double> MyCast = source.MyCast<double>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        private static void TestCastThrow<T>(object o)
        {
            byte? i = 10;
            object[] source = { -1, 0, o, i };

            IEnumerable<T> MyCast = source.MyCast<T>();

            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void ThrowOnHeterogenousSource()
        {
            TestCastThrow<long?>(null);
            TestCastThrow<long>(9L);
        }

        [Fact]
        public void CastToString()
        {
            object[] source = { "Test1", "4.5", null, "Test2" };
            string[] expected = { "Test1", "4.5", null, "Test2" };

            Assert.Equal(expected, source.MyCast<string>());
        }

        [Fact]
        public void CastToStringRunOnce()
        {
            object[] source = { "Test1", "4.5", null, "Test2" };
            string[] expected = { "Test1", "4.5", null, "Test2" };

            Assert.Equal(expected, source.RunOnce().MyCast<string>());
        }

        [Fact]
        public void ArrayConversionThrows()
        {
            Assert.Throws<InvalidCastException>(() => new[] { -4 }.MyCast<long>().ToList());
        }

        [Fact]
        public void FirstElementInvalidForCast()
        {
            object[] source = { "Test", 3, 5, 10 };

            IEnumerable<int> MyCast = source.MyCast<int>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void LastElementInvalidForCast()
        {
            object[] source = { -5, 9, 0, 5, 9, "Test" };

            IEnumerable<int> MyCast = source.MyCast<int>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void NullableIntFromNullsAndInts()
        {
            object[] source = { 3, null, 5, -4, 0, null, 9 };
            int?[] expected = { 3, null, 5, -4, 0, null, 9 };

            Assert.Equal(expected, source.MyCast<int?>());
        }

        [Fact]
        public void ThrowCastingIntToLong()
        {
            int[] source = { -4, 1, 2, 3, 9 };

            IEnumerable<long> MyCast = source.MyCast<long>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void ThrowCastingIntToNullableLong()
        {
            int[] source = { -4, 1, 2, 3, 9 };

            IEnumerable<long?> MyCast = source.MyCast<long?>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void ThrowCastingNullableIntToLong()
        {
            int?[] source = { -4, 1, 2, 3, 9 };

            IEnumerable<long> MyCast = source.MyCast<long>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void ThrowCastingNullableIntToNullableLong()
        {
            int?[] source = { -4, 1, 2, 3, 9, null };

            IEnumerable<long?> MyCast = source.MyCast<long?>();
            Assert.Throws<InvalidCastException>(() => MyCast.ToList());
        }

        [Fact]
        public void CastingNullToNonnullableIsNullReferenceException()
        {
            int?[] source = { -4, 1, null, 3 };
            IEnumerable<int> MyCast = source.MyCast<int>();
            Assert.Throws<NullReferenceException>(() => MyCast.ToList());
        }

        [Fact]
        public void NullSource()
        {
            Assert.Throws<ArgumentNullException>(() => ((IEnumerable<object>)null).MyCast<string>());
        }

        [Fact]
        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public void ForcedToEnumeratorDoesntEnumerate()
        {
            IEnumerable<string> iterator = new object[0].Where(i => i != null).MyCast<string>();
            // Don't insist on this behaviour, but check it's correct if it happens
            var en = iterator as IEnumerator<string>;
            Assert.False(en != null && en.MoveNext());
        }
    }
}