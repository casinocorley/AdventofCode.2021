// See https://aka.ms/new-console-template for more information

using AdventofCode._2021.Day13;

//GetPart1("Data1.txt");
//GetPart1("Data2.txt");

GetPart2("Data1.txt");
GetPart2("Data2.txt");

void GetPart1(string filename)
{
    var data = PuzzleFile.GetData(filename);
    var part1 = new Part1(data.Dots);
    part1.Fold(data.Instructions[0]);

    var visibleDots = part1.GetVisibleDots();

    Console.WriteLine($"Visible Dots for {filename}: {visibleDots.Count}");
    //visibleDots.ForEach(p => Console.WriteLine($"{p.X},{p.Y}"));
}

void GetPart2(string filename)
{
    var data = PuzzleFile.GetData(filename);
    var part1 = new Part1(data.Dots);
    part1.Fold(data.Instructions);

    var visibleDots = part1.GetVisibleDots();

    Console.WriteLine($"Visible Dots for {filename}: {visibleDots.Count}");
    //visibleDots.ForEach(p => Console.WriteLine($"{p.X},{p.Y}"));

    for (var y = 0; y <= visibleDots.Max(p => p.Y); y++)
    {
        for (var x = 0; x <= visibleDots.Max(p => p.X); x++)
        {
            var present = visibleDots.Any(p => p.X == x && p.Y == y);
            Console.Write(present ? "@" : " ");
        }
        Console.WriteLine();
    }
}







