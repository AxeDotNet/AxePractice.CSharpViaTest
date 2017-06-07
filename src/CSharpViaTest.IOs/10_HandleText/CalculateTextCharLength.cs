using System;
using System.Collections.Generic;
using Xunit;

namespace CSharpViaTest.IOs._10_HandleText
{
    /* 
     * Description
     * ===========
     * 
     * This test will introduce the concept of Codepoint and surrogate pair to you. But
     * for the most of the cases, the character can fit in 16-bit unicode character.
     * 
     * Difficulty: Super Easy
     */
    public class CalculateTextCharLength
    {
        static IEnumerable<object[]> TestCases() =>
            new[]
            {
                new object[]{"", 0},
                new object[]{"12345", 5},
                new object[]{char.ConvertFromUtf32(0x2A601) + "1234", 5}
            };

        #region Please modifies the code to pass the test

        static int GetCharacterLength(string text)
        {
            throw new NotImplementedException();
        }

        #endregion

        [Theory]
        [MemberData(nameof(TestCases))]
        public void should_calculate_text_character_length(string testString, int expectedLength)
        {
            Assert.Equal(expectedLength, GetCharacterLength(testString));
        }
    }
}