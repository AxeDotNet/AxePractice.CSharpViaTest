﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSharpViaTest.Collections
{
    public class SkippedEnumeratorPractice
    {
        class SkippedEnumerable<T> : IEnumerable<T>
        {
            readonly ICollection<T> collection;

            public SkippedEnumerable(ICollection<T> collection)
            {
                this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new SkippedEnumerator<T>(collection);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        class SkippedEnumerator<T> : IEnumerator<T>
        {
            readonly ICollection<T> collection;

            public SkippedEnumerator(ICollection<T> collection)
            {
                this.collection = collection;
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public T Current => throw new NotImplementedException();

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void should_visit_elements_in_reversed_order()
        {
            int[] sequence = {1, 2, 3, 4, 5, 6};
            int[] resolved = new SkippedEnumerable<int>(sequence).ToArray();

            Assert.Equal(new [] {2, 4, 6}, resolved);
        }
    }
}