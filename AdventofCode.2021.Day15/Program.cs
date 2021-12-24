var levels = PuzzleData.GetData("Data2.txt");

// Assume a square
var size = (int) Math.Sqrt(levels.Count);

var destination = levels
    .Keys
    .MaxBy(x => x.X + x.Y);

var lowestRisks = levels
    .Keys
    .Select(p => new RiskInfo(p))
    .ToList();

lowestRisks.First(x => x.Location == (0,0)).Value = 0;

RiskInfo? current;
do
{
    current = lowestRisks
        .Where(x => !x.IsSet)
        .MinBy(x => x.Value);

    if (current == null)
        throw new ApplicationException("Unable to determine next step");
    
    current.IsSet = true;

    var adjLocations = GetAdjacents(current.Location);

    foreach (var adjLocation in adjLocations)
    {
        var adj = lowestRisks.First(x => x.Location == adjLocation);
        if (adj.Value > current.Value + levels[adjLocation])
            adj.Value = current.Value + levels[adjLocation];
    }
    
    
} while (current.Location != destination);

Console.WriteLine($"Lowest Risk is : {current.Value}");

List<(int X, int Y)> GetAdjacents((int X, int Y) p)
{
    var adjacents = new List<(int X, int Y)>();
    if (p.X + 1 < size)
        adjacents.Add((p.X + 1, p.Y));
    if (p.Y + 1 < size)
        adjacents.Add((p.X, p.Y + 1));

    return adjacents;
}



