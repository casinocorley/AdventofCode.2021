// See https://aka.ms/new-console-template for more information

Console.WriteLine("******* Part 1 ********");
AnswerPart1("ExampleData.txt");
AnswerPart1("PuzzleData.txt");

Console.WriteLine();

Console.WriteLine("******* Part 2 ********");
AnswerPart2("ExampleData.txt");
AnswerPart2("PuzzleData.txt");

void AnswerPart1(string file)
{
    var data = GetData(file);
    var answer = GetSolutionPart1(data);
    Console.WriteLine($"{file}: {answer}");
}

void AnswerPart2(string file)
{
    var data = GetData(file);
    var answer = GetSolutionPart2(data);
    Console.WriteLine($"{file}: {answer}");
}

int GetSolutionPart1(List<char[]> lines) 
{
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

int GetSolutionPart2(List<char[]> lines)
{
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
            
            // Get Basin Area
            
            var basinPoints = new List<Point> { new (x, y) };
            lines.GetSurroundingArea(basinPoints.First(), basinPoints);
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
    public static int? GetValue(this List<char[]> lines, Point point)
    {
        return lines.GetValue(point.X, point.Y);
    }
    
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

    public static bool Exists(this List<Point> points, Point point)
    {
        return points.Any(p => p.X == point.X && p.Y == point.Y);
    }
    
    public static void GetSurroundingArea(this List<char[]> lines, Point p1, List<Point> area)
    {
        var x = p1.X;
        var y = p1.Y;
        
        // left
        for (var i = x-1; i >= 0; i--)
        {
            var point = new Point(i,y);
            if (lines.GetValue(point) == 9)
                break;

            if (area.Exists(point))
                break;
            
            area.Add(point);
            lines.GetSurroundingArea(point, area);
        }
        
        // right
        for (var i = x+1; i < lines[0].Length; i++)
        {
            var point = new Point(i,y);
            if (lines.GetValue(point) == 9)
                break;

            if (area.Exists(point))
                break;
            
            area.Add(new Point(i,y));
            lines.GetSurroundingArea(point, area);
        }
        
        // above
        for (var i = y-1; i >= 0; i--)
        {
            var point = new Point(x, i);
            if (lines.GetValue(point) == 9)
                break;

            if (area.Exists(point))
                break;
            
            area.Add(point);
            lines.GetSurroundingArea(point, area);
        }
        
        // below
        for (var i = y+1; i < lines.Count; i++)
        {
            var point = new Point(x, i);
            if (lines.GetValue(point) == 9)
                break;

            if (area.Exists(point))
                break;
            
            area.Add(point);
            lines.GetSurroundingArea(point, area);
        }
    }
}
