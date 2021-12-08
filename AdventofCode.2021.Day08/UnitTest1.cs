using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AdventofCode._2021.Day08.Part1;
using Xunit;

namespace AdventofCode._2021.Day08
{
    public class UnitTest1
    {
        /*
         * Part 1
         */
        
        [Theory]
        [InlineData("be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe", 2)]
        public void AnalyzePart1Test(string data, int expected )
        {
            // Arrange
            var values = data.Split('|');
            var input = values[0];
            var output = values[1];

            var solution = new Solution();

            // Act
            solution.Analyze(output);
            var actual = solution.GetTotalCount();

            // Assert
            Assert.Equal(expected, actual);
        }
        
        public static IEnumerable<object[]> Part1Data
        {
            get
            {
                var exampleData = GetData("ExampleData.txt");
                var puzzleData = GetData("PuzzleData.txt");
                
                return new List<object[]>
                {
                    new object[] {exampleData, 26},
                    new object[] {puzzleData, 274}
                };
            }
        }

        public static List<string> GetData(string fileName)
        {
            return File
                .ReadLines(fileName)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(line => line)
                .ToList();
        }
        
        [Theory]
        [MemberData(nameof(Part1Data))]
        public void AnalyzeFromFileTest(string[] lines, int expected )
        {
            // 
            var outputs = lines
                .Select(x => x.Split('|')[1])
                .ToArray();

            var solution = new Solution();

            // Act
            solution.Analyze(outputs);
            var actual = solution.GetTotalCount();

            // Assert
            Assert.Equal(expected, actual);
        }
        
        /*
         * Part 2
         */
        
        public static IEnumerable<object[]> Part2Data
        {
            get
            {
                var example1 = new[] {
                    "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf"
                };
                
                var puzzleData = GetData("PuzzleData.txt");
                
                return new List<object[]>
                {
                    new object[] {example1, 5353},
                    new object[] {puzzleData, 61229},

                };
            }
        }
        
        [Theory]
        [MemberData(nameof(Part2Data))]
        public void AnalyzePart2Test(string[] values, int expected )
        {
            // Arrange
            var solution = new Part2.Solution();

            // Act
            var actual = solution.Analyze(values);

            // Assert
            Assert.Equal(expected, actual);
        }

    }
}
