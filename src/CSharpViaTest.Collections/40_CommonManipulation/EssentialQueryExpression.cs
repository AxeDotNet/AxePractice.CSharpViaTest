using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSharpViaTest.Collections._40_CommonManipulation
{
    /* 
     * Description
     * ===========
     * 
     * This test will try to destroy all your known of LINQ.
     * 
     * Difficulty: Medium
     * 
     * Knowledge Point
     * ===============
     * 
     * - Essentially, the query expression will be converted to query operator.
     * - Essentially, the query operator is just a method call.
     * - The priority of method decision.
     * 
     * Requirement
     * ===========
     * 
     * - No LINQ method is allowed to use in this test.
     */
    public static class EssentialQueryExpression
    {
        #region Please modifies the code to pass the test

        #endregion

        [Fact]
        public static void should_plus_2_for_every_item_instead_of_plus_1()
        {
            var array = new [] { 1, 2, 3 };
            var newArray = from item in array select item + 1;
            Assert.Equal(new [] { 3, 4, 5 }, newArray.ToArray());
        }
    }
}
