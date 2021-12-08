using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventofCode._2021.Day08.Part2
{
    public class Solution
    {
        private Dictionary<char, char> Letters = new Dictionary<char, char>();

        Dictionary<string, int> Codes = new Dictionary<string, int>();

        private string One {
            get => Codes.FirstOrDefault(x => x.Value == 1).Key;
        }
        string Four {
            get => Codes.FirstOrDefault(x => x.Value == 4).Key;
        }
        string Seven {
            get => Codes.FirstOrDefault(x => x.Value == 7).Key;
        }
        string Eight {
            get => Codes.FirstOrDefault(x => x.Value == 8).Key;
        }
        string Nine {
            get => Codes.FirstOrDefault(x => x.Value == 9).Key;
        }
        string Six {
            get => Codes.FirstOrDefault(x => x.Value == 6).Key;
        }
        string Zero {
            get => Codes.FirstOrDefault(x => x.Value == 0).Key;
        }
        string Five {
            get => Codes.FirstOrDefault(x => x.Value == 5).Key;
        }
        string Two {
            get => Codes.FirstOrDefault(x => x.Value == 2).Key;
        }
        string Three {
            get => Codes.FirstOrDefault(x => x.Value == 3).Key;
        }
        

        public int Analyze(string value)
        {
            var groups = value.Split('|');
            var inputs = groups[0]
                .Split(' ')
                .Where(x => x.Length > 1);
            var outputs = groups[1]
                .Split(' ')
                .Where(x => x.Length > 1);
            var both = inputs.Union(outputs);
            
            var one = both.FirstOrDefault(x => x.Length == 2);
            Codes[one] = 1;
            
            var four = both.FirstOrDefault(x => x.Length == 4);
            Codes[four] = 4;
            
            var seven = both.FirstOrDefault(x => x.Length == 3);
            Codes[seven] = 7;
            
            var eight = both.FirstOrDefault(x => x.Length == 7);
            Codes[eight] = 8;

            SetA();
            SetNine(both);
            SetG();
            SetE();
            SetSix(both);
            SetC();
            SetZero(both);
            SetD();
            SetFive(both);
            SetF();
            SetB();
            SetTwo();
            SetThree();

            var digits = "";
            foreach (var output in outputs)
            {
                var digit = Codes
                    .FirstOrDefault(x => 
                        x.Key.ContainsAllItems(output) && output.ContainsAllItems(x.Key))
                    .Value
                    .ToString();

                digits += digit;
            }

            var answer = int.Parse(digits);
            return answer;
        }
        
        public int Analyze(string[] inputs)
        {
            var sum = 0;
            foreach (var input in inputs)
            {
                // Reset
                Codes = new Dictionary<string, int>();
                Letters = new Dictionary<char, char>();
                sum += Analyze(input);
            }

            return sum;
        }

        public void SetNine(IEnumerable<string> values)
        {
            var nines = values
                .Where(value => 
                    value.ContainsAllItems(Four) && 
                    value.ContainsAllItems(Seven))
                .Where(value => !value.ContainsAllItems(Eight));

            Codes[nines.FirstOrDefault()] = 9;
        }

        public void SetA()
        {
            Letters['a'] = Seven
                .FirstOrDefault(x => One.All(y => y != x));
        }
        public void SetG()
        {
            var g = Nine
                .Except(Four)
                .Except(Seven);

            Letters['g'] = g.FirstOrDefault();
        }

        public void SetE()
        {
            var e = Eight.Except(Nine).FirstOrDefault();
            Letters['e'] = e;
        }
        
        public void SetSix(IEnumerable<string> values)
        {
            var sixes = values
                .Where(x => x.Length == 6)
                .Where(value => One.Except(value).Count() == 1);

            Codes[sixes.FirstOrDefault()] = 6;
        }
        
        public void SetC()
        {
            var c = Eight.Except(Six);
            Letters['c'] = c.FirstOrDefault();
        }
        
        public void SetZero(IEnumerable<string> values)
        {
            var zeros = values
                .Where(x => x.Length == 6)
                .Where(value => !value.ContainsAllItems(Six))
                .Where(value => !value.ContainsAllItems(Nine));

            Codes[zeros.FirstOrDefault()] = 0;
        }

        public void SetD()
        {
            var d = Eight.Except(Zero);
            Letters['d'] = d.FirstOrDefault();
        }

        public void SetFive(IEnumerable<string> values)
        {
            var fives = values
                .Where(x => x.Length == 5)
                .Where(value => Six.ContainsAllItems(value))
                .Where(value => !value.Contains(Letters['e']))
                .Where(value => !value.Contains(Letters['c']));

            Codes[fives.FirstOrDefault()] = 5;
        }

        public void SetF()
        {
            var f = One.Intersect(Five);
            Letters['f'] = f.FirstOrDefault();
        }

        public void SetB()
        {
            var b = Four
                .Except(Letters['c'].ToString())
                .Except(Letters['d'].ToString())
                .Except(Letters['f'].ToString());

            Letters['b'] = b.FirstOrDefault();
        }

        public void SetTwo()
        {
            var two = Letters['a'].ToString() +
                      Letters['c'].ToString() +
                      Letters['d'].ToString() +
                      Letters['e'].ToString() +
                      Letters['g'].ToString();
            Codes[two] = 2;
        }

        public void SetThree()
        {
            var three = Letters['a'].ToString() +
                        Letters['c'].ToString() +
                        Letters['d'].ToString() +
                        Letters['f'].ToString() +
                        Letters['g'].ToString();
            Codes[three] = 3;
        }
    }

    public static class Extensions
    {
        public static bool ContainsAllItems(this IEnumerable<char> a, IEnumerable<char> b)
        {
            return !b.Except(a).Any();
        }
    }
}