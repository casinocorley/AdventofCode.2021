namespace AdventofCode._2021.Day12;

public class Part2 : Part1
{
    public Part2(string filename) 
        : base(filename) { }

    protected override bool GoneFarEnough(string currentPath, Connection connection)
    {
        return connection.B == start ||
               VisitSmallCaveTooManyTimes(connection.B, currentPath);
    }

    protected static bool VisitSmallCaveTooManyTimes(string nextCave, string currentPath)
    {
        if (IsBigCave(nextCave))
            return false;
    
        // Can visit one small cave twice
        var multipleVisitsToSmallCaves = currentPath
            .Split(',')
            .Where(x => x != start)
            .Where(x => x != end)
            .Append(nextCave)
            .Where(IsSmallCave)
            .GroupBy(x => x)
            .Select(g => new { g.Key, Count = g.Count() })
            .Where(x => x.Count > 1)
            .ToList();

        if (multipleVisitsToSmallCaves.NotAny())
            return false;

        var tooManyCaves = multipleVisitsToSmallCaves.Count > 1;

        var tooManyVisits = multipleVisitsToSmallCaves
            .Any(x => x.Count > 2);

        return tooManyCaves || 
               tooManyVisits;
    }
}

public static class Extensions
{
    public static bool NotAny<TSource>(this List<TSource> list) =>
        !list.Any();
}

