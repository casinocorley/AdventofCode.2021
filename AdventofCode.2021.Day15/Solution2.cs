namespace AdventofCode._2021.Day15;

public class Solution2 : Solution1
{
    public Solution2(string filename) 
        : base(filename) { }

    protected override Dictionary<(int X, int Y), int> GetData(string dataFile)
    {
        var origData = base.GetData(dataFile);
        Dictionary<(int X, int Y), int> newData = new();
        
        // Assume a square
        var size = (int) Math.Sqrt(origData.Count);

        for (var y = 0; y < 5; y++)
        {
            for (var x = 0; x < 5; x++)
            {
                foreach (var k in origData.Keys)
                {
                    var newX = k.X + x * size;
                    var newY = k.Y + y * size;
                    var newValue = GetNewValue(origData[(k.X, k.Y)], x, y);
                    
                    newData.Add((newX, newY), newValue);
                }
            }
        }

        return newData;
    }

    private static int GetNewValue(int orig, int x, int y)
    {
        var result = (orig + x + y) % 9;
        
        return result == 0 
            ? 9
            : result;
    }
    
}