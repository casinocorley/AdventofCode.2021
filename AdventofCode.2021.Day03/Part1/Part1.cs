using System.Collections.Generic;
using System.Linq;

namespace AdventofCode._2021.Day03.Part1
{
    public class GammaCalculator
    {
        public string Calculate(List<string> readings)
        {
            var answer = string.Empty;

            for (var i = 0; i < readings.First().Length; i++)
            {
                var numOf1 = readings
                    .Select(x => x[i])
                    .Count(y => y == '1');
                
                answer += numOf1 > readings.Count() / 2 ? "1" : "0";
            }
            
            return answer;
        }
        
    }

    public class EpsilonCalculator
    {
        public string Calculate(string gamma)
        {
            var answer = "";
            for (var i = 0; i < gamma.Length; i++)
            {
                answer += gamma[i] == '0' ? "1" : "0";
            }

            return answer;
        }
    }
}