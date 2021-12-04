using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventofCode._2021.Day04.Part1
{
    public class Bingo
    {
        public List<BingoBoard> BingoBoards { get; private set; }

        public Bingo(List<BingoBoard> bingoBoards)
        {
            BingoBoards = bingoBoards;
        }

        public List<BingoBoard> CallNumber(int value)
        {
            foreach (var bingoBoard in BingoBoards)
            {
                bingoBoard.Mark(value);
            }

            return BingoBoards
                .Where(x => x.HasBingo)
                .ToList();
        }

        public int PlayGame(IEnumerable<int> numbersToCall)
        {
            List<BingoBoard> winningBingoBoards = null;
            
            foreach (var number in numbersToCall)
            {
                winningBingoBoards = CallNumber(number);

                if (winningBingoBoards.Any())
                    break;
            }

            return winningBingoBoards.FirstOrDefault().FinalScore;
        }

        public int PlayGameToFinalBoard(IEnumerable<int> numbersToCall)
        {
            List<BingoBoard> winningBingoBoards = null;
            
            foreach (var number in numbersToCall)
            {
                winningBingoBoards = CallNumber(number);

                if (winningBingoBoards.Any())
                {
                    if (BingoBoards.Count() > 1)
                        BingoBoards.RemoveAll(x => winningBingoBoards.Contains(x));
                    else
                        break;
                }
            }

            return winningBingoBoards.FirstOrDefault().FinalScore;
        }
        
    }
    
    public class BingoBoard
    {
        public List<BoardNumber> Board { get; private set; }

        public bool HasBingo { get; private set; }
        
        public List<BoardNumber> WinningNumbers { get; private set; }

        public int FinalScore { get; private set; }

        private int? lastWinningNumber = null;

        public BingoBoard(int[,] boardNumbers)
        {
            Board = new List<BoardNumber>();
            foreach(var value in boardNumbers)
            {
                Board.Add(new BoardNumber(value));
            }
        }
        
        public BingoBoard(IEnumerable<int> boardNumbers)
        {
            Board = new List<BoardNumber>();
            foreach(var value in boardNumbers)
            {
                Board.Add(new BoardNumber(value));
            }
        }

        public void Mark(int value)
        {
            var found = Board
                .FirstOrDefault(x => x.Value == value);
            
            if (found != null) 
                found.Marked = true;

            HasBingo = BingoCheck();
            
            if (HasBingo && lastWinningNumber == null)
            {
                lastWinningNumber = value;
                FinalScore = GetScore() * lastWinningNumber.Value;
            }
        }

        private bool BingoCheck()
        {
            // check bingo along the row
            var rowBingo = Board
                .ToRows()
                .Where(x => x.AllMarked())
                .ToList();

            var columnBingo = Board
                .ToColumns()
                .Where(x => x.AllMarked())
                .ToList();

            var allBingo = new List<List<BoardNumber>>();
            allBingo.AddRange(rowBingo);
            allBingo.AddRange(columnBingo);

            if (allBingo.Any())
            {
                // Use only the first detected bingo.
                WinningNumbers = allBingo.FirstOrDefault();
            }

            return allBingo.Any();
        }

        public int GetScore()
        {
            var sum = Board
                .Where(x => !x.Marked)
                .Sum(x => x.Value);

            return sum;
        }
    }

    public static class BingoBoardExtensions
    {
        public static List<List<BoardNumber>> ToRows(this List<BoardNumber> board)
        {
            // Assume board is a square of unknown size
            var rowsize = (int) Math.Sqrt(board.Count);

            var rows = board
                .Select((x, i) => new { Value = x, Index = i })
                .GroupBy(x => x.Index / rowsize)
                .Select(x => x.Select(y => y.Value).ToList())
                .ToList();
            
            return rows;
        }

        public static List<List<BoardNumber>> ToColumns(this List<BoardNumber> board)
        {
            var rowsize = (int) Math.Sqrt(board.Count);

            var columns = new List<BoardNumber>[rowsize];
            for (var i = 0; i < rowsize; i++)
            {
                var column = board
                    .Where((x, index) => (index - i) % rowsize == 0)
                    .ToList();
                
                columns[i] = column;
            }
            
            var columns2 = board
                .Select((x, i) => new { Value = x, Index = i })
                .GroupBy(x => x.Index / rowsize)
                .Select(x => x.Select(y => y.Value).ToList())
                .ToList();

            return columns.ToList();
        }

        public static bool AllMarked(this List<BoardNumber> numbers)
        {
            var unmarkedFound = numbers.Any(x => !x.Marked);
            return !unmarkedFound;
        }
    }

    public class BoardNumber
    {
        public int Value { get; set; }

        public bool Marked { get; set; }

        public BoardNumber(int value)
        {
            Value = value;
        }
    }
    
}