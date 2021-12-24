public static class PuzzleData
{
    public static Dictionary<(int X, int Y), int> GetData(string filename)
    {
        var data = File
            .ReadLines(filename)
            .Select(s => s.ToCharArray())
            .Select(x => x.Select(c => int.Parse(c.ToString())).ToList())
            .ToList();

        Dictionary<(int X, int Y), int> dict = new();

        for (var y = 0; y < data.Count; y++)
        {
            for (var x = 0; x < data[0].Count; x++)
            {
                dict.Add((x, y), data[y][x]);
            }
        }

        return dict;
    }
}