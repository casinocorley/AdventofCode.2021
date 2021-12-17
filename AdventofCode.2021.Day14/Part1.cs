using System.Diagnostics;

namespace AdventofCode._2021.Day14;

public class Part1
{
    protected List<PairInsertion> PairInsertions { get; }
    public List<char> Template { get; protected set; }

    public Part1(string template, List<PairInsertion> pairInsertions)
    {
        PairInsertions = pairInsertions;
        Template = template.ToCharArray().ToList();
    }

    public virtual Part1 PreformInserts(int steps)
    {
        if (steps == 0)
            return this;
        
        List<char> newTemplate = default;
        char prev, curr;
        
        for (var s = 0; s < steps; s++)
        {
            newTemplate = new List<char>{Template[0]};

            for (var i = 1; i < Template.Count; i++)
            {
                prev = Template[i - 1];
                curr = Template[i];
                
                var match = PairInsertions
                    .FirstOrDefault(x =>
                        x.Pair[0] == prev &&
                        x.Pair[1] == curr);

                if (match != null)
                    newTemplate.Add(match.Insertion);
                
                newTemplate.Add(curr);
            }
            
            Template = new List<char> ( newTemplate );
        }

        return this;
    }

    public (char Key, int Count) GetMostCommonElement() =>
        Template
            .GroupBy(x => x)
            .Select(g => ( Key: g.Key, Count: g.Count() ))
            .OrderByDescending(x => x.Count)
            .First();
    
    public (char Key, int Count) GetLeastCommonElement() =>
        Template
            .GroupBy(x => x)
            .Select(g => ( Key: g.Key, Count: g.Count() ))
            .OrderBy(x => x.Count)
            .First();
}

[DebuggerDisplay("{Pair} -> {Insertion}")]
public record PairInsertion(string Pair, char Insertion);