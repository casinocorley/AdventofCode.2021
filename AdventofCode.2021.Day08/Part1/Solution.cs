using System.Linq;

namespace AdventofCode._2021.Day08.Part1
{
    public class Solution
    {
        public int One { get; set; }
        public int Four { get; set; }
        public int Seven { get; set; }
        public int Eight { get; set; }
        
        public void Analyze(string output)
        {
            var groups= output
                .Split(' ')
                .OrderBy(x => x.Length);
            
            One += groups.Count(x => x.Length == 2);
            Four += groups.Count(x => x.Length == 4);
            Seven += groups.Count(x => x.Length == 3);
            Eight += groups.Count(x => x.Length == 7);
        }
        
        public void Analyze(string[] inputs)
        {
            foreach (var input in inputs)
            {
                Analyze(input);
            }
        }

        public int GetTotalCount() => One + Four + Seven + Eight;

    }
}