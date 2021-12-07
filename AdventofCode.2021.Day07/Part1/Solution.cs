using System;
using System.Linq;

namespace AdventofCode._2021.Day07.Part1
{
    public class Solution
    {
        public (int Position, int Moves) GetMostFuelEfficientPosition(int[] initialPositions)
        {
            var max = initialPositions.Max();
            (int Postition, int Moves) bestPosition = default;

            for (var position = 0; position < initialPositions.Length; position++)
            {
                var moves = GetTotalMoves(initialPositions, position);

                if (bestPosition == default ||
                    moves < bestPosition.Moves)
                    bestPosition = new(position, moves);
            }

            return bestPosition;
        }

        public int GetTotalMoves(int[] initialPositions, int position)
        {
            var totalMoves = 0;
            foreach (var initialPosition in initialPositions)
            {
                var moves = Math.Abs(initialPosition - position);
                totalMoves += moves;
            }

            return totalMoves;
        }
    }
    
    
}