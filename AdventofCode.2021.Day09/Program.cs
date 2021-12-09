// See https://aka.ms/new-console-template for more information

using System.Numerics;

var riskLevel1 = GetSolutionPart1("ExampleData.txt");
var riskLevel2 = GetSolutionPart1("PuzzleData.txt");

Console.WriteLine($"Answer for ExampleData.txt is: {riskLevel1}");
Console.WriteLine($"Answer for PuzzleData.txt is: {riskLevel2}");

var part2Answer1 = GetSolutionPart2("ExampleData.txt");
var part2Answer2 = GetSolutionPart2("PuzzleData.txt");

Console.WriteLine($"Basin Level for ExampleData.txt is: {part2Answer1}");
Console.WriteLine($"Basin Level for PuzzleData.txt is: {part2Answer2}");

int GetSolutionPart1(string fileName)
{
    var lines = GetData(fileName);

    var length = lines[0].Length;
    var height = lines.Count;

    var riskLevel = 0;
    var lowPoints = 0;

    for (var y = 0; y < height ; y++)
    {
        for (var x = 0; x < length; x++)
        {
            var current = lines.GetValue(x,y);
            var right = lines.GetValue(x+1,y);
            var left = lines.GetValue(x-1,y);
            var above = lines.GetValue(x,y-1);
            var below = lines.GetValue(x,y+1);

            var surroundingPoints = new[] { right, left, above, below }
                .Where(x => x != null)
                .ToList();

            if (current < surroundingPoints.Min())
            {
                riskLevel += current.Value + 1;
                lowPoints++;
            }
        }
    }
    
    return riskLevel;
}


int GetSolutionPart2(string fileName)
{
    var lines = GetData(fileName);

    var length = lines[0].Length;
    var height = lines.Count;
    
    var basinArea = new List<int>();

    for (var y = 0; y < height ; y++)
    {
        for (var x = 0; x < length; x++)
        {
            var current = lines.GetValue(x,y);
            var right = lines.GetValue(x+1,y);
            var left = lines.GetValue(x-1,y);
            var above = lines.GetValue(x,y-1);
            var below = lines.GetValue(x,y+1);

            var surroundingPoints = new[] { right, left, above, below }
                .Where(x => x != null)
                .ToList();

            if (!(current < surroundingPoints.Min())) 
                continue;
            
            
            var basinPoints = new List<Point>();
                
            // current point
            basinPoints.Add(new Point(x,y));
                
            // left
            for (var i = x-1; i >= 0; i--)
            {
                if (lines.GetValue(i, y) == 9) 
                    break;
                    
                if (basinPoints.Exists(i,y))
                    break;
                    
                basinPoints.Add(new Point(i,y));
                lines.GetSurroundingArea(i, y, basinPoints);
            }
                
            // right
            for (var i = x+1; i < length; i++)
            {
                if (lines.GetValue(i, y) == 9)
                    break;
                    
                if (basinPoints.Exists(i,y))
                    break;
                    
                basinPoints.Add(new Point(i,y));
                lines.GetSurroundingArea(i, y, basinPoints);
            }
                
            // above
            for (var i = y-1; i >= 0; i--)
            {
                if (lines.GetValue(x, i) == 9)
                    break;
                    
                if (basinPoints.Exists(x,i))
                    break;
                    
                basinPoints.Add(new Point(x,i));
                lines.GetSurroundingArea(x, i, basinPoints);
            }
                
            // below
            for (var i = y+1; i < height; i++)
            {
                if (lines.GetValue(x, i) == 9)
                    break;
                    
                if (basinPoints.Exists(x,i))
                    break;
                    
                basinPoints.Add(new Point(x,i));
                lines.GetSurroundingArea(x, i, basinPoints);
            }
                
            basinArea.Add(basinPoints.Count);
        }
    }

    return basinArea
        .OrderByDescending(x => x)
        .Take(3)
        .Aggregate((x, y) => x * y);
}

List<char[]> GetData(string fileName)
{
    return File
        .ReadLines(fileName)
        .Where(x => !string.IsNullOrWhiteSpace(x))
        .Select(x => x.ToCharArray())
        .ToList();
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public static class Extensions
{
    public static int? GetValue(this List<char[]> lines, int x, int y)
    {
        if (x < 0 ||
            y < 0)
            return null;

        if (x >= lines[0].Length)
            return null;

        if (y >= lines.Count)
            return null;

        return int.Parse(lines[y][x].ToString());
    }

    public static bool Exists(this List<Point> points, int x, int y)
    {
        return points.Any(point => point.X == x && point.Y == y);
    }
    
    public static void GetSurroundingArea(this List<char[]> lines, int x, int y, List<Point> area)
    {
        // left
        for (var i = x-1; i >= 0; i--)
        {
            if (lines.GetValue(i, y) == 9)
                break;

            if (area.Any(point => point.X == i && point.Y == y))
                break;
            
            area.Add(new Point(i,y));
            lines.GetSurroundingArea(i, y, area);
        }
        
        // right
        for (var i = x+1; i < lines[0].Length; i++)
        {
            if (lines.GetValue(i, y) == 9)
                break;

            if (area.Any(point => point.X == i && point.Y == y))
                break;
            
            area.Add(new Point(i,y));
            lines.GetSurroundingArea(i, y, area);
        }
        
        // above
        for (var i = y-1; i >= 0; i--)
        {
            if (lines.GetValue(x, i) == 9)
                break;

            if (area.Any(point => point.X == x && point.Y == i))
                break;
            
            area.Add(new Point(x,i));
            lines.GetSurroundingArea(x, i, area);
        }
        
        // below
        for (var i = y+1; i < lines.Count; i++)
        {
            if (lines.GetValue(x, i) == 9)
                break;

            if (area.Any(point => point.X == x && point.Y == i))
                break;
            
            area.Add(new Point(x,i));
            lines.GetSurroundingArea(x, i, area);
        }
    }
}
