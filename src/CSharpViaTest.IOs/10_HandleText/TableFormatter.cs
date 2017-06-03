using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xunit;

namespace CSharpViaTest.IOs._10_HandleText
{
    public class TableFormatter
    {
        struct CellFormatRule
        {
            public CellFormatRule(bool rightAligned, short cellCharacterLength)
            {
                RightAligned = rightAligned;
                CellCharacterLength = cellCharacterLength;
            }

            public bool RightAligned { get; }
            public short CellCharacterLength { get; }
        }

        #region Please modifies the code to pass the test

        static IEnumerable<string> FormatTable(
            IEnumerable<IEnumerable<object>> data,
            CellFormatRule[] rules,
            IFormatProvider formatProvider)
        {
            return data.Select(row =>
                row.Select(
                        (cell, index) =>
                        {
                            if (index >= rules.Length) throw new ArgumentException("Data does not match with rules");
                            CellFormatRule rule = rules[index];
                            object actualCellData = cell ?? "";
                            string cellString = actualCellData is IFormattable
                                ? ((IFormattable) actualCellData).ToString(null, formatProvider)
                                : actualCellData.ToString();
                            Func<int, string> padding = rule.RightAligned
                                ? (Func<int, string>) cellString.PadLeft
                                : cellString.PadRight;
                            return padding(rule.CellCharacterLength);
                        })
                    .Aggregate(new StringBuilder(), (result, item) => result.Append(item), result => result.ToString()));
        }

        #endregion

        [Fact]
        public void should_format_table_correctly()
        {
            var rules = new[]
            {
                new CellFormatRule(false, 20),
                new CellFormatRule(false, 20),
                new CellFormatRule(true, 5)
            };

            IEnumerable<IEnumerable<object>> data = new[]
            {
                new[] {"Name", "Company", "Id"},
                new[] {"Edogawa Conan", "Tokyo", "C01"},
                new[] {"Furugawa Nagisa", "Kyoto Animation", "C02"},
                new[] {"Kirigaya Katsuto", "A1 Pictures", "C03"}
            };

            IEnumerable<string> table = FormatTable(data, rules, CultureInfo.InvariantCulture);

            Assert.Equal(
                new[]
                {
                    "Name                Company                 Id",
                    "Edogawa Conan       Tokyo                  C01",
                    "Furugawa Nagisa     Kyoto Animation        C02",
                    "Kirigaya Katsuto    A1 Pictures            C03"
                },
                table);
        }

        [Fact]
        public void should_throw_if_cell_count_is_not_equal_with_actual_data()
        {
            var rules = new[]
            {
                new CellFormatRule(false, 20),
                new CellFormatRule(false, 20),
                new CellFormatRule(true, 5)
            };

            Assert.Throws<ArgumentException>(
                () => FormatTable(
                        new[] {new[] {"c1", "c2", "c3", "c4"}},
                        rules,
                        CultureInfo.InvariantCulture)
                    .ToArray());
        }

        [Fact]
        public void should_handle_null_object_to_empty_string()
        {
            var rules = new[]
            {
                new CellFormatRule(true, 10)
            };

            IEnumerable<string> table = FormatTable(
                new []{new []{default(object)}}, rules,
                CultureInfo.InvariantCulture);

            Assert.Equal(new[] {"          "}, table);
        }

        [Fact]
        public void should_not_return_in_memory_collection()
        {
            var rules = new[]
            {
                new CellFormatRule(true, 10)
            };

            IEnumerable<string> table = FormatTable(
                new[] { new[] { "hello" } }, rules,
                CultureInfo.InvariantCulture);

            Assert.False(table is ICollection<string>, "I will give you 10GB of data to crash the app");
        }
    }
}