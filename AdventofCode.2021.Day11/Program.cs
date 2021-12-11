
var filename = "Data3.txt";

var data = File
    .ReadLines(filename)
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

var grid = new Dictionary<(int X, int Y), int>();

var _offsets = new List<(int X, int Y)>
{
    (-1,-1),
    ( 0,-1),
    ( 1,-1),
    (-1, 0),
    ( 1, 0),
    (-1, 1),
    ( 0, 1),
    ( 1, 1),
};

for (var y = 0; y < data.Count; y++)
{
    for (var x = 0; x < data[y].Length; x++)
    {
        grid[(x, y)] = int.Parse(data[y][x].ToString());
    }
}

//Part1();
Part2();

void Part1()
{
    // Iterate 100 times
    var sum = (long) 0;
    for (var i = 0; i < 100; i++)
    {
        sum += Iterate(grid);
    }

    Console.WriteLine($"Part 1 Answer: {sum}");
}

void Part2()
{
    var i = 0;
    var allFlashed = false;
    while (!allFlashed)
    {
        i++;
        allFlashed = Iterate(grid) == 100;
    }
    
    Console.WriteLine($"Answer to Part 2 is {i}");
}

int Iterate(Dictionary<(int X, int Y), int> grid)
{
    // Power Up
    grid.Keys.ToList()
        .ForEach(key => grid[key]++);
    
    // Get Flashes
    var flashed = new List<(int X, int Y)>();
    var flashing = grid.Keys
        .Where(key => grid[key] > 9)
        .ToList();

    while (flashing.Any())
    {
        flashed = flashed.Concat(flashing).ToList();

        var neighbors = flashing
            .SelectMany(f =>
                _offsets.Select(o => (X: f.X + o.X, Y: f.Y + o.Y)))
            .Where(grid.ContainsKey)
            .ToList();

        neighbors.ForEach(x => grid[x]++);
        
        flashing = grid.Keys
            .Where(key => grid[key] > 9)
            .Except(flashed)
            .ToList();
    }
    
    grid.Keys.ToList()
        .Where(key => grid[key] > 9)
        .ToList()
        .ForEach(key => grid[key] = 0);

    return flashed.Count;
}


