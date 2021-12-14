using System.Diagnostics;

namespace AdventofCode._2021.Day12;

[DebuggerDisplay("{A}-{B}")]
public record Connection(string A, string B);

public class Part1
{
    public List<string> Paths { get; }

    public List<string> CompletedPaths
    {
        get
        {
            return Paths
                .Where(x => x.EndsWith(end))
                .OrderBy(x => x)
                .ToList();
        }
    }

    protected readonly IEnumerable<Connection> connections;
    protected const string start = "start";
    protected const string end = "end";

    public Part1(string filename)
    {
        connections = GetConnections(filename);
        Paths = new List<string> { start };
    }

    public void Traverse()
    {
        Traverse(start);
    }
    
    public void Traverse(string currentPath)
    {
        var current = currentPath
            .Split(',')
            .Last();
    
        foreach (var connection in connections.Where(x => x.A == current))
        {
            var goneFarEnough = GoneFarEnough(currentPath, connection);
            if (goneFarEnough)
                continue;

            var newPath = $"{currentPath},{connection.B}";
                
            Paths.Remove(currentPath);
            Paths.Add(newPath);
            
            if (connection.B == end)
                continue;
        
            Traverse(newPath);
        }
    }
    
    protected virtual bool GoneFarEnough(string currentPath, Connection connection)
    {
        return connection.B == start ||
               HaveRepeatedPath(currentPath, connection) ||
               VisitSmallCaveBefore(currentPath, connection);
    }
    
    protected static bool HaveRepeatedPath(string currentPath, Connection connection)
    {
        return currentPath.Contains($"{connection.A},{connection.B}");
    }

    protected static bool VisitSmallCaveBefore(string currentPath, Connection connection) =>
        IsSmallCave(connection.B) && 
        currentPath.Split(',').Any(x => x == connection.B);
    
    protected static bool IsBigCave(string cave) => cave.All(char.IsUpper);
    
    protected static bool IsSmallCave(string cave) => !IsBigCave(cave);
    
    protected static bool NotEndpoints(Connection cave) => cave.A != start && cave.B != end;

    protected static IEnumerable<Connection> GetConnections(string fileName)
    {
        var initial = File
            .ReadLines(fileName)
            .Select(x => x.Split('-'))
            .Select(x => new Connection(x[0], x[1]))
            .ToList();

        var reversed = initial
            .Where(NotEndpoints)
            .Select(x => new Connection(x.B, x.A))
            .ToList();

        return initial.Union(reversed);
    }
}

