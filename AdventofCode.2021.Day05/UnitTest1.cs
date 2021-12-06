using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using AdventofCode._2021.Day05.Part1;
using AdventofCode._2021.Day05.Part2;

namespace AdventofCode._2021.Day05
{
    public class UnitTest1
    {
        private static List<PuzzleInput> GetData(string fileName)
        {
            var fileLines = File
                .ReadLines(fileName)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(line => line)
                .ToList();

            return fileLines
                .Select(GetPuzzleInput)
                .ToList();
        }

        private static PuzzleInput GetPuzzleInput(string puzzleEntry)
        {
            var pointStrings = puzzleEntry.Split(" -> ");
            var point1 = GetPoints(pointStrings[0]);
            var point2 = GetPoints(pointStrings[1]);
                
            
            return new PuzzleInput(point1, point2);
        }

        private static Point GetPoints(string pointString)
        {
            var values = pointString
                .Split(",")
                .Select(x => int.Parse(x))
                .ToList();
            return new Point(values[0],values[1]);
        }
        
        public static IEnumerable<object[]> TestMarkingData
        {
            get
            {
                var puzzleInput = GetData("ExampleData.txt");
                
                return new List<object[]>
                {
                    new object[] { "0,0 -> 0,0", new Point(0,0), 1 },
                    new object[] { "0,0 -> 0,1", new Point(0,1), 1 },
                    
                    new object[] { "0,0 -> 2,0", new Point(1,0), 1 },
                    new object[] { "2,0 -> 0,0", new Point(1,0), 1 },
                    new object[] { "0,0 -> 10,0", new Point(5,0), 1 },
                    
                    new object[] { "0,0 -> 0,2", new Point(0,1), 1 },
                    //new object[] { puzzleInput }
                };

            }
        }
    
        [Theory]
        [MemberData(nameof(TestMarkingData))]
        public void MarkingPointsTest(string puzzleEntry, Point expectedPoint, int expectedCount)
        {
            // Arrange
            var puzzleInput = GetPuzzleInput(puzzleEntry);
            var solution = new Part1.Solution();

            // Act
            solution.MarkBoard(puzzleInput);
            var markedPoints = solution.MarkedPoints;

            // Assert
            Assert.True(markedPoints.ContainsKey(expectedPoint));
            Assert.Equal(expectedCount,markedPoints[expectedPoint]);
        }
        
        public static IEnumerable<object[]> Part1Data
        {
            get
            {
                var exampleInput = GetData("ExampleData.txt");
                var puzzleInput = GetData("PuzzleData.txt");
                
                return new List<object[]>
                {
                    new object[] { exampleInput, 12 },
                    new object[] { puzzleInput, 0 }
                };

            }
        }

        [Theory]
        [MemberData(nameof(Part1Data))]
        public void GetAnswerPart1(List<PuzzleInput> puzzleInputs, int expected)
        {
            // Arrange
            var solution = new Part1.Solution();

            // Act
            solution.MarkBoard(puzzleInputs);
            var actual = solution.GetCountWhere2Overlap();

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [MemberData(nameof(Part1Data))]
        public void GetAnswerPart2(List<PuzzleInput> puzzleInputs, int expected)
        {
            // Arrange
            var solution = new Part2.Solution();

            // Act
            solution.MarkBoard(puzzleInputs);
            var actual = solution.GetCountWhere2Overlap();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
    
}
