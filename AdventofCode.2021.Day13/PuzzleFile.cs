namespace AdventofCode._2021.Day13;

public static class PuzzleFile
{
    public static (List<Point> Dots, List<Instruction> Instructions) GetData(string filename)
    {
        var puzzleInput = File
            .ReadLines(filename)
            .ToList();
    
        var dots = puzzleInput
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Where(x => !x.StartsWith("fold"))
            .Select(x => x.Split(','))
            .Select(x => new[] {int.Parse(x[0]), int.Parse(x[1])})
            .Select(x => new Point(x[0], x[1]))
            .ToList();

        var instructions = puzzleInput
            .Where(x => x.StartsWith("fold"))
            .Select(x => x.Remove("fold along "))
            .Select(x => x.Split('='))
            .Select(x => new Instruction(x[0], int.Parse(x[1])))
            .ToList();

        return (dots, instructions);
    }

    public static string Remove(this string value, string remove)
    {
        return value.Replace(remove, "");
    }
}