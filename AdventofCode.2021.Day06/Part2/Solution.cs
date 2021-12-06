using System;
using System.Collections.Generic;
using System.Linq;
using AdventofCode._2021.Day06.Part1;

namespace AdventofCode._2021.Day06.Part2
{
    public class FishSpawnCalculator
    {
        public int GetNumberOfFish(int initialState, int days)
        {
            const int daysTillBirth = 7;

            var iterations = days / daysTillBirth;
            
            var daysLeftOver = days - (iterations * daysTillBirth);
            if (daysLeftOver >= initialState + 1)
                iterations++;
            
            var totalFish = Math.Pow(2, iterations);
            return (int) totalFish;
        }

        public int GetNumberOfFish(int[] initialStates, int days)
        {
            var sum = initialStates
                .Sum(x => GetNumberOfFish(x, days));
            return sum;
        }
    }
}