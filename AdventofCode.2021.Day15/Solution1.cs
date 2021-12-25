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
        var data = GetData(filename);
        var answer = Dijkstra(data);
        
        return answer;
    }

    private int Dijkstra(Dictionary<(int X, int Y), int> distances)
    {
        // Assume a square
        var size = (int) Math.Sqrt(distances.Count);

        var destination = distances
            .Keys
            .MaxBy(x => x.X + x.Y);
        
        (int X, int Y) start = (0, 0);

        var shortestPaths = distances
            .ToDictionary(x => x.Key, x => int.MaxValue);
        shortestPaths[start] = 0;
        
        PriorityQueue<(int X, int Y), int> queue = new();
        queue.Enqueue(start, shortestPaths[start]);
        
        (int X, int Y) current;
        
        do
        {
            current = queue.Dequeue();

            var neighbors = GetNeighbors(current, size);

            foreach (var neighbor in neighbors)
            {
                if (shortestPaths[neighbor] > shortestPaths[current] + distances[neighbor])
                {
                    shortestPaths[neighbor] = shortestPaths[current] + distances[neighbor];
                    queue.Enqueue(neighbor, shortestPaths[neighbor]);
                }
            }
    
        } while (current != destination);
        
        return shortestPaths[current];
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
        return File
            .ReadAllLines(dataFile)
            .SelectMany((line, y) => line
                .Select((c, x) => (Key: (X:x, Y:y), Value: int.Parse(c.ToString())))
                .ToArray())
            .ToDictionary(x => x.Key, x => x.Value);
    }
}