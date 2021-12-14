using AdventofCode._2021.Day12;

GetPart1("Data1.txt");
GetPart1("Data2.txt");
GetPart1("Data3.txt");
GetPart1("Data4.txt");


GetPart2("Data1.txt");
GetPart2("Data2.txt");
GetPart2("Data3.txt");
GetPart2("Data4.txt");


void GetPart1(string filename)
{
    var part1 = new Part1(filename);
    part1.Traverse();
    var completedPaths = part1.CompletedPaths;

    Console.WriteLine($"================= {filename} =================");
    Console.WriteLine($"Completed paths found: {completedPaths.Count}");
    //completedPaths.ForEach(Console.WriteLine);
}

void GetPart2(string filename)
{
    var part2 = new Part2(filename);
    part2.Traverse();
    var completedPaths = part2.CompletedPaths;

    Console.WriteLine($"================= {filename} =================");
    Console.WriteLine($"Completed paths found: {completedPaths.Count}");
    //completedPaths.ForEach(Console.WriteLine);
}


