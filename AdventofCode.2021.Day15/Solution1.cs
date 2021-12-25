namespace AdventofCode._2021.Day15;

public class Solution1
{
    private string filename;
    
    public Solution1(string filename)
    {
        this.filename = filename;
    }

    public int Solve()
    {
        var levels = GetData(filename);
        var answer = Dijkstra(levels);
        
        return answer;
    }

    private int Dijkstra(Dictionary<(int X, int Y), int> riskLevels)
    {
        // Assume a square
        var size = (int) Math.Sqrt(riskLevels.Count);

        var destination = riskLevels
            .Keys
            .MaxBy(x => x.X + x.Y);

        PriorityQueue<(int X, int Y), int> queue = new();

        var lowest = riskLevels
            .ToDictionary(x => x.Key, x => int.MaxValue);

        (int X, int Y) start = (0, 0);
        lowest[start] = 0;
        queue.Enqueue(start, lowest[start]);
        
        (int X, int Y) current;
        do
        {
            current = queue.Dequeue();

            var neighbors = GetNeighbors(current, size);

            foreach (var neighbor in neighbors)
            {
                if (lowest[neighbor] > lowest[current] + riskLevels[neighbor])
                {
                    lowest[neighbor] = lowest[current] + riskLevels[neighbor];
                    queue.Enqueue(neighbor, lowest[neighbor]);
                }
            }
    
        } while (current != destination);
        
        return lowest[current];
    }
    
    private List<(int X, int Y)> GetNeighbors((int X, int Y) p, int size)
    {
        var adjacents = new List<(int X, int Y)>();
        if (p.X + 1 < size)
            adjacents.Add((p.X + 1, p.Y));
        if (p.Y + 1 < size)
            adjacents.Add((p.X, p.Y + 1));

        return adjacents;
    }
    
    protected virtual Dictionary<(int X, int Y), int> GetData(string dataFile)
    {
        var data = File
            .ReadLines(dataFile)
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