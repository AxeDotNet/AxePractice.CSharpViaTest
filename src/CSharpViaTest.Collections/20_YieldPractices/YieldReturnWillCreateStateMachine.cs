using System;
using System.Text;
using System.Collections.Generic;
using CSharpViaTest.Collections.Annotations;
using Xunit;

namespace CSharpViaTest.Collections._20_YieldPractices
{
    [SuperEasy]
    public class YieldReturnWillCreateStateMachine
    {
        #region Please modifies the code to pass the test 

        public IEnumerable<string> GetStringTriangle(char character, int count)
        {
            for (int i = 1; i <= count; ++i)
            {
                yield return new StringBuilder().Append(character, i).ToString();
            }
        }

        #endregion

        [Fact]
        public void should_get_skipped_sequences()
        {
            IEnumerable<string> enumerable = GetStringTriangle('*', 4);
            string[] expected =
            {
                "*",
                "**",
                "***",
                "****"
            };

            Assert.Equal(expected, enumerable);
        }

        [Fact]
        public void should_returns_enumerable_rather_than_collection()
        {
            IEnumerable<string> enumerable = GetStringTriangle('*', 2);
            Assert.False(enumerable is ICollection<string>);
        }
    }
}