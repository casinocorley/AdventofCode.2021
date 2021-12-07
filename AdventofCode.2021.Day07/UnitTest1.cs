using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using AdventofCode._2021.Day07.Part1;
using Xunit;

namespace AdventofCode._2021.Day07
{
    public class UnitTest1
    {
        public static IEnumerable<object[]> Part1Data
        {
            get
            {
                var puzzleInput = File
                    .ReadLines("PuzzleData.txt")
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(line => line)
                    .ToList()
                    .SelectMany(x => x.Split(','))
                    .Select(x => int.Parse(x))
                    .ToArray();
                
                return new List<object[]>
                {
                    new object[] {new[] {16,1,2,0,4,2,7,1,2,14}, 2, 37},
                    new object[] {puzzleInput, 343, 340987},
                };
            }
        }

        [Theory]
        [MemberData(nameof(Part1Data))]
        public void Part1Test(int[] initialPositions, int expectedPosition, int expectedMoves)
        {
            // Arrange
            var solution = new Part1.Solution();

            // Act
            var actual = solution.GetMostFuelEfficientPosition(initialPositions);

            // Assert
            Assert.Equal(expectedPosition, actual.Position);
            Assert.Equal(expectedMoves, actual.Moves);
        }
        
        public static IEnumerable<object[]> Part2Data
        {
            get
            {
                var puzzleInput = File
                    .ReadLines("PuzzleData.txt")
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(line => line)
                    .ToList()
                    .SelectMany(x => x.Split(','))
                    .Select(x => int.Parse(x))
                    .ToArray();
                
                return new List<object[]>
                {
                    new object[] {new[] {16,1,2,0,4,2,7,1,2,14}, 5, 168},
                    new object[] {puzzleInput, 478, 96987874},
                };
            }
        }
        
        [Theory]
        [MemberData(nameof(Part2Data))]
        public void Part2Test(int[] initialPositions, int expectedPosition, int expectedMoves)
        {
            // Arrange
            var solution = new Part2.Solution();

            // Act
            var actual = solution.GetMostFuelEfficientPosition(initialPositions);

            // Assert
            Assert.Equal(expectedPosition, actual.Position);
            Assert.Equal(expectedMoves, actual.Moves);
        }
    }
}