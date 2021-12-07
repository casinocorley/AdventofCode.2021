using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventofCode._2021.Day07.Part2
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
            var distances = GetDistances(initialPositions.Max() + 1);

            for (var i = 0; i < initialPositions.Length; i++)
            {
                var initialPosition = initialPositions[i];
                var moves = Math.Abs(initialPosition - position);
                totalMoves += distances[moves];
            }

            return totalMoves;
        }

        public Dictionary<int, int> GetDistances(int max)
        {
            var values = new Dictionary<int, int>();
            for (int i = 0, j = 0; i < max; i++)
            {
                j += i;
                values[i] = j;
            }

            return values;
        }
    }
}