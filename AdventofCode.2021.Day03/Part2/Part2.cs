using System.Collections.Generic;
using System.Linq;

namespace AdventofCode._2021.Day03.Part2
{
    public class OxygenCalculator
    {
        public string Calculate(List<string> readings)
        {
            var answer = "";
            
            var remaining = new List<string>();
            remaining.AddRange(readings);
            
            for (var i = 0; i < readings.Count(); i++)
            {
                var numOf1s = remaining.Count(x => x[i] == '1');
                var commonBit = numOf1s >= remaining.Count / 2.0 ? '1' : '0';

                remaining = remaining
                    .Where(x => x[i] == commonBit)
                    .ToList();

                if (remaining.Count == 1)
                {
                    answer = remaining.First();
                    break;
                }
            }
            
            return answer;
        }
    }
    
    public class Co2ScrubberCalculator
    {
        public string Calculate(List<string> readings)
        {
            var answer = "";
            
            var remaining = new List<string>();
            remaining.AddRange(readings);
            
            for (var i = 0; i < readings.Count(); i++)
            {
                var numOf1s = remaining.Count(x => x[i] == '1');
                var commonBit = numOf1s < remaining.Count / 2.0 ? '1' : '0';

                remaining = remaining
                    .Where(x => x[i] == commonBit)
                    .ToList();

                if (remaining.Count == 1)
                {
                    answer = remaining.First();
                    break;
                }
            }
            
            return answer;
        }
    }
}