using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventofCode._2021.Day04.Part1;
using Xunit;

namespace AdventofCode._2021.Day04
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(new [] {1,2,3}, 3)]
        [InlineData(new [] {1,1,1,1}, 1)]
        [InlineData(new [] {0, 26, 100}, 0)]
        public void MarkBingoBoardTest(IEnumerable<int> calledNumbers, int expected)
        {
            // Arrange
            var board = new int [5, 5]
            {
                { 1, 2, 3, 4, 5 },
                { 6, 7, 8, 9, 10 },
                { 11, 12, 13, 14, 15 },
                { 16, 17, 18, 19, 20 },
                { 21, 22, 23, 24, 25 },
            };
            var bingoBoard = new BingoBoard(board);

            // Act
            foreach(var calledNumber in calledNumbers)
                bingoBoard.Mark(calledNumber);

            // Assert
            Assert.Equal(expected, bingoBoard.Board.Count(x => x.Marked));
        }
        
        [Theory]
        [InlineData(new [] {0}, false)]
        [InlineData(new [] {1, 2, 3, 4}, false)]
        [InlineData(new [] {1, 2, 3, 4, 5}, true)]
        [InlineData(new [] {21, 22, 23, 24, 25}, true)]
        [InlineData(new [] {1, 6, 11, 16, 21}, true)]
        [InlineData(new [] {5, 10, 15, 20, 25}, true)]
        [InlineData(new [] {5, 10, 15, 20}, false)]
        public void HasBingoOnBingoBoardTest(IEnumerable<int> calledNumbers, bool expected)
        {
            // Arrange
            var board = new int [5, 5]
            {
                { 1, 2, 3, 4, 5 },
                { 6, 7, 8, 9, 10 },
                { 11, 12, 13, 14, 15 },
                { 16, 17, 18, 19, 20 },
                { 21, 22, 23, 24, 25 },
            };
            var bingoBoard = new BingoBoard(board);

            // Act
            foreach(var calledNumber in calledNumbers)
                bingoBoard.Mark(calledNumber);

            // Assert
            Assert.Equal(expected, bingoBoard.HasBingo);
        }
        
        public static IEnumerable<object[]> Part1Data
        {
            get
            {
                var exampleData = GetData("ExampleData.txt");
                var testData = GetData("TestData.txt");
                
                return new List<object[]>
                {
                    new object[] { exampleData.CallNumbers, exampleData.Boards, 4512},
                    new object[] { testData.CallNumbers, testData.Boards, 0} 
                };

            }
        }

        private static (List<int> CallNumbers, List<BingoBoard> Boards) GetData(string fileName)
        {
            var exampleData = File
                .ReadLines(fileName)
                .Where(x => !String.IsNullOrEmpty(x))
                .Select(line => line)
                .ToList();

            var calledNumbers = exampleData[0]
                .Split(',')
                .Select(int.Parse)
                .ToList();

            var boardNumbers = exampleData
                .Skip(1)
                .SelectMany(x =>
                    x.Split(' ')
                        .Where(x => !string.IsNullOrEmpty(x))
                        .Select(int.Parse));
                    

            var boards = new List<BingoBoard>();
            for (var i = 0; i < boardNumbers.Count(); i += 25)
            {
                var oneBoard = boardNumbers
                    .Skip(i)
                    .Take(25);
                    
                boards.Add(new BingoBoard(oneBoard));
            }

            return new(calledNumbers, boards);
        }
        
        [Theory]
        [MemberData(nameof(Part1Data))]
        public void GetAnswer1(List<int> calledNumbers, List<BingoBoard> boards, int expected)
        {
            // Arrange
            var bingo = new Bingo(boards);

            // Act
            var actualScore = bingo.PlayGame(calledNumbers);
            
            // Assert
            Assert.Equal(expected, actualScore);
        }
        
        public static IEnumerable<object[]> Part2Data
        {
            get
            {
                var exampleData = GetData("ExampleData.txt");
                var testData = GetData("TestData.txt");
                
                return new List<object[]>
                {
                    new object[] { exampleData.CallNumbers, exampleData.Boards, 1924},
                    new object[] { testData.CallNumbers, testData.Boards, 0} 
                };

            }
        }
        
        [Theory]
        [MemberData(nameof(Part2Data))]
        public void GetAnswer2(List<int> calledNumbers, List<BingoBoard> boards, int expected)
        {
            // Arrange
            var bingo = new Bingo(boards);

            // Act
            var actualScore = bingo.PlayGameToFinalBoard(calledNumbers);
            
            // Assert
            Assert.Equal(expected, actualScore);
        }
    }
}

