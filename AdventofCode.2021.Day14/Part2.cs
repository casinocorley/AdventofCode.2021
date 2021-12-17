using System.Text;

namespace AdventofCode._2021.Day14;

public class Part2
{
    private string _template;
    private readonly Dictionary<string, char> _pairInsertions;
    private Dictionary<string, long> _pairs;

    public Part2(string template, IEnumerable<PairInsertion> pairInsertions)
    {
        _template = template;
        _pairInsertions = pairInsertions
            .ToDictionary(x => x.Pair, x => x.Insertion);

        _pairs = new Dictionary<string, long>();
    }

    public void PreformInserts(int steps)
    {
        _pairs = _template
            .Where((x, i) => i != _template.Length - 1)
            .Select((x, i) => $"{_template[i]}{_template[i + 1]}")
            .GroupBy(x => x)
            .ToDictionary(g => g.Key, g => (long) g.Count());

        var step = 0;
        while (step < steps)
        {
            List<(string Key, long Count)> newPairs = new();
                
            foreach (var key in _pairs.Keys)
            {
                var keyCount = _pairs[key];
                var insert = _pairInsertions[key];
                
                var newPair1 = new string(new[] {key[0], insert});
                var newPair2 = new string(new[] {insert, key[1]});

                var newKeyPairs = new[] { newPair1, newPair2 }
                    .Select(p => (Key: p, Count: keyCount))
                    .ToList();
                
                newPairs.AddRange(newKeyPairs);
            }

            _pairs = newPairs
                .GroupBy(x => x.Key)
                .Select(g => (Key: g.Key, Count: g.Sum(x => x.Count)))
                .ToDictionary(x => x.Key, x => x.Count);
            
            step++;
        }
    }
    
    public long GetAnswer()
    {
        var letters = _pairs
            .Keys
            .SelectMany(x => x.ToCharArray())
            .Distinct();

        List<(char Letter, long Total)> letterCount = new();
        foreach (var letter in letters)
        {
            var total = _pairs.Keys
                .Where(k => k.StartsWith(letter.ToString()))
                .Sum(p => _pairs[p]);

            if (letter == _template.ToCharArray().Last())
                total++;
            
            letterCount.Add((letter, total));
        }
        
        var most = letterCount.Max(x => x.Total);
        var least = letterCount.Min(x => x.Total);

        return most - least;
    }
}


