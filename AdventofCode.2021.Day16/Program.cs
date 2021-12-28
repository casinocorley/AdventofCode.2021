using AdventofCode._2021.Day16;

Console.WriteLine("Advent of Code 2021, Day 16");
Console.WriteLine("Enter puzzle input or press enter to exit. ");
Console.WriteLine();

Console.WriteLine($"Choose: ");
Console.WriteLine($"1) part 1");
Console.WriteLine($"2) part 2");
var part =Console.ReadLine();

if (part is not ("1" or "2"))
    Environment.Exit(0);

if (part is "1")
{
    string? input;
    do
    {
        Console.Write("Input: ");
        input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
            break;
        
        var puzzle = new Puzzle1(input);
        var answer = puzzle.Solve();
    
        Console.WriteLine($"Answer: {answer}");
        Console.WriteLine();
    
    } while(true);
}
else
{
    string? input;
    do
    {
        Console.Write("Input: ");
        input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
            break;
        
        var puzzle = new Puzzle2(input);
        var answer = puzzle.Solve();
    
        Console.WriteLine($"Answer: {answer}");
        Console.WriteLine();
    
    } while(true);
}


