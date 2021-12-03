using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using AdventofCode._2021.Day02;

namespace AdventofCode._2021.Day02
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("forward 0", 0, 0)]
        [InlineData("forward 5", 5, 0)]
        [InlineData("forward 10", 10, 0)]
        [InlineData("down 0", 0, 0)]
        [InlineData("down 5", 0, 5)]
        [InlineData("up 0", 0, 0)]
        [InlineData("up 5", 0, -5)]
        public void TestMovePart1(string command, int expectedHorizontal, int expectedDepth)
        {
            // Arrange
            var map = new Part1.Map();

            // Act
            map.Move(command);

            // Assert
            Assert.Equal(expectedHorizontal, map.Position.Horizontal);
            Assert.Equal(expectedDepth, map.Position.Depth);
        }

        public static IEnumerable<object[]> Part1Commands
        {
            get
            {
                var testData = File
                    .ReadLines("TestData.txt")
                    .Where(x => !String.IsNullOrEmpty(x))
                    .Select(line => line)
                    .ToList();
                
                return new List<object[]>
                {
                    new object[] { new List<string>{ "forward 5", "down 10", "up 2" }, 5, 8, 40 },
                    new object[] { new List<string> { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2"}, 15, 10, 150 },
                    new object[] { testData, 0, 0, 0} 
                };

            }
        }
        
        [Theory]
        [MemberData(nameof(Part1Commands))]
        public void TestMovesPart1(List<string> commands, int expectedHorizontal, int expectedDepth, int multiply)
        {
            // Arrange
            var map = new Part1.Map();

            // Act
            foreach (var command in commands)
            {
                map.Move(command);
            }

            // Assert
            Assert.Equal(multiply, map.Position.Horizontal * map.Position.Depth);
            Assert.Equal(expectedHorizontal, map.Position.Horizontal);
            Assert.Equal(expectedDepth, map.Position.Depth);
        }
        
        [Theory]
        [InlineData("down 0", 0, 0, 0)]
        [InlineData("down 5", 0, 0, 5)]
        [InlineData("up 0", 0, 0, 0)]
        [InlineData("up 5", 0, 0, -5)]
        [InlineData("forward 0", 0, 0, 0)]
        [InlineData("forward 5", 5, 0, 0)]
        public void TestMovePart2(string command, int expectedHorizontal, int expectedDepth, int expectedAim)
        {
            // Arrange
            var map = new Part2.Map();

            // Act
            map.Move(command);

            // Assert
            Assert.Equal(expectedHorizontal, map.Position.Horizontal);
            Assert.Equal(expectedDepth, map.Position.Depth);
            Assert.Equal(expectedAim, map.Position.Aim);
        }
        
        public static IEnumerable<object[]> Part2Commands
        {
            get
            {
                var testData = File
                    .ReadLines("TestData.txt")
                    .Where(x => !String.IsNullOrEmpty(x))
                    .Select(line => line)
                    .ToList();
                
                return new List<object[]>
                {
                    new object[] { new List<string>{ "down 5", "forward 1" }, 1, 5, 5, 5 },
                    new object[] { new List<string>{ "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2",  }, 15, 60, 10, 900 },
                    new object[] { testData, 0, 0, 0, 0} 
                };

            }
        }

        [Theory]
        [MemberData(nameof(Part2Commands))]
        public void TestMovesPart2(List<string> commands, int expectedHorizontal, int expectedDepth, int expectedAim, int expectedMutliply)
        {
            // Arrange
            var map = new Part2.Map();

            // Act
            foreach (var command in commands)
            {
                map.Move(command);
            }

            // Assert
            Assert.Equal(expectedMutliply, map.Position.Horizontal * map.Position.Depth);
            Assert.Equal(expectedHorizontal, map.Position.Horizontal);
            Assert.Equal(expectedDepth, map.Position.Depth);
            Assert.Equal(expectedAim, map.Position.Aim);
        }
    }
}