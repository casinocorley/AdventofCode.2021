using System.Collections.Generic;
using System.Linq;

namespace AdventofCode._2021.Day01
{
    public abstract class Common
    {
        public bool HasIncreased(int? first, int? second)
        {
            if (first == null || second == null)
                return false;
            
            return second > first;
        }
    }
    
    public class Part1 : Common
    {
        public IEnumerable<IncreaseResult> DetermineIncrease(List<int?> numbers)
        {
            var results = new List<IncreaseResult>();
            
            for (var i = 0; i < numbers.Count(); i++)
            {
                var first = i == 0 ? null : numbers[i - 1];
                var second = numbers[i];
               
                var answer = new IncreaseResult {
                    First = first, 
                    Second = second, 
                    HasIncreased = HasIncreased(first, second)
                };
                
                results.Add(answer);
            }

            return results;
        }
    }

    public class Part2 : Common
    {
        public IEnumerable<IncreaseResult> DetermineIncrease(List<int?> numbers)
        {
            var results = new List<IncreaseResult>();
            var totalCount = numbers.Count;
            
            for (var i = 0; i < totalCount; i++)
            {
                var first = i == 0 || i+1 >= totalCount ? 
                    null : 
                    numbers[i - 1] + numbers[i] + numbers[i + 1];
                var second = i + 2 >= totalCount ? null:
                    numbers[i] + numbers[i + 1] + numbers[i + 2];
               
                var answer = new IncreaseResult {
                    First = first, 
                    Second = second, 
                    HasIncreased = HasIncreased(first, second)
                };
                
                results.Add(answer);
            }

            return results;
        }
    }

    public class IncreaseResult
    {
        public int? First { get; set; }
        public int? Second { get; set; }
        public bool HasIncreased { get; set; }
    }
}