namespace AdventofCode._2021.Day14;

public class PuzzleFile
{
    public static (string Template, List<PairInsertion> PairInsertions) GetData(string filename)
    {
        var lines = File
            .ReadLines(filename)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        var template = lines[0];

        var pairInsertions = lines
            .GetRange(1, lines.Count - 1)
            .Select(x => x.Split(" -> "))
            .Select(x => new PairInsertion(x[0], x[1].First()))
            .ToList();

        return (template, pairInsertions);
    }
}