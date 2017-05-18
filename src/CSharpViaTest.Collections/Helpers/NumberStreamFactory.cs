﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpViaTest.Collections.Helpers
{
    static class NumberStreamFactory
    {
        public static IEnumerable<int> CreateWithTopNumber(int maxNumber, int size)
        {
            int maxNumberIndex = new Random().Next(0, size);
            for (var i = 0; i < size; ++i)
            {
                if (i == maxNumberIndex) { yield return maxNumber; }
                else { yield return maxNumber - new Random().Next(1, 1000); }
            }
        }
    }
}