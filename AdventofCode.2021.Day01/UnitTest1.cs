using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace AdventofCode._2021.Day01
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(1, 0, false)]
        [InlineData(0, 1, true)]
        public void HasIncreasedTest(int? first, int? second, bool expected)
        {
            // Arrange
            var part1 = new Part1();

            // Act
            var actual = part1.HasIncreased(first, second);

            // Assert
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> Data1
        {
            get
            {
                var testData = File
                    .ReadLines("TestData.txt")
                    .Where(x => !String.IsNullOrEmpty(x))
                    .Select(line => Int32.Parse(line))
                    .Cast<int?>()
                    .ToList();

                return new List<object[]>
                {
                    new object[] {new List<int?> {9, 10, 11, 12, 13, 14}, 3},
                    new object[] {new List<int?> {199, 200, 208, 210, 200, 207, 240, 269, 260, 263}, 7},
                    new object[] {testData, 0}
                };
            }
        }
        
        public static IEnumerable<object[]> Data2
        {
            get
            {
                var testData = File
                    .ReadLines("TestData.txt")
                    .Where(x => !String.IsNullOrEmpty(x))
                    .Select(line => Int32.Parse(line))
                    .Cast<int?>()
                    .ToList();

                return new List<object[]>
                {
                    new object[] {new List<int?> {9, 10, 11, 12, 13, 14}, 5},
                    new object[] {new List<int?> {199, 200, 208, 210, 200, 207, 240, 269, 260, 263}, 5},
                    new object[] {testData, 0}
                };
            }
        }

        [Theory]
        [MemberData(nameof(Data1))]
        public void Test1(List<int?> data, int expected)
        {
            // Arrange
            var part1 = new Part1();

            // Act
            var results = part1.DetermineIncrease(data);
            var actual = results.Count(x => x.HasIncreased);

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [MemberData(nameof(Data2))]
        public void Test2(List<int?> data, int expected)
        {
            // Arrange
            var part2 = new Part2();

            // Act
            var results = part2.DetermineIncrease(data);
            var actual = results.Count(x => x.HasIncreased);

            // Assert
            Assert.Equal(expected, actual);
        }


    }
    

}