using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace CSharpViaTest.OtherBCLs._10_HandleDates
{
    /* 
     * Description
     * ===========
     * 
     * This test will enumerate all datetime information in a text stream. Each line is 
     * represented as:
     * 
     * "{culture information abbr. / date time pattern string}|{formatted date time string}"
     * 
     * All datetimes are defined within UTC timezone. Please implement a function called
     * `EnumerateDateTimes` to get actual DateTime objects from the stream.
     * 
     * Difficulty: Medium
     * 
     * Knowledge Point
     * ===============
     * 
     * - DateTime.ParseExtact / DateTime.Parse
     * - How to get different culture info objects.
     */
    public class ConvertDateTimeFromString
    {
        static Stream CreateTextStreamWithDateTimes(string[] dateTimeString)
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, Encoding.UTF8, 32 * 1024, true))
            {
                writer.WriteLine(dateTimeString);
            }

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        #region Please modifies the code to pass the test

        static IEnumerable<DateTime> EnumerateDateTimes(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 32 * 1024, true))
            {
                string line;
                var cultureStringPattern = new Regex(@"[a-z]{2,2}-[a-zA-Z]{2,3}");
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    string dateTimeString = parts[1];
                    string pattern = parts[0];
                    if (cultureStringPattern.IsMatch(pattern))
                    {
                        yield return DateTime.Parse(dateTimeString, new CultureInfo(pattern), DateTimeStyles.AssumeUniversal);
                    }
                    else
                    {
                        yield return DateTime.ParseExact(dateTimeString, pattern, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                    }
                }
            }
        }

        #endregion

        [Fact]
        public void should_convert_datetime_string_correctly()
        {
            var stream = CreateTextStreamWithDateTimes(
                new[]
                {
                    "tr-TR|2.1.2013 00:00:00",
                    "zh-CN|2013/1/2 0:00:00",
                    "en-US|1/2/2013 12:00:00 AM",
                    "ja-JP|2013/01/02 0:00:00",
                    "yyyyMMdd hh:mm:ss|20130102 00:00:00"
                });

            IEnumerable<DateTime> dateTimes = EnumerateDateTimes(stream);

            Assert.True(dateTimes.All(dt => dt.ToUniversalTime().Equals(new DateTime(2013, 1, 2, 0, 0, 0, DateTimeKind.Utc))));
        }

        [Fact]
        public void should_not_load_all_information_into_memories()
        {
            var stream = CreateTextStreamWithDateTimes(
                new[]
                {
                    "tr-TR|2.1.2013 00:00:00",
                    "zh-CN|2013/1/2 0:00:00",
                    "en-US|1/2/2013 12:00:00 AM",
                    "ja-JP|2013/01/02 0:00:00",
                    "yyyyMMdd hh:mm:ss|20130102 00:00:00"
                });

            IEnumerable<DateTime> dateTimes = EnumerateDateTimes(stream);

            Assert.False(dateTimes is ICollection);
        }
    }
}