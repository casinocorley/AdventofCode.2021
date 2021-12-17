using AdventofCode._2021.Day14;

/*
AnswerPart1("Data1.txt");
AnswerPart1("Data2.txt");
*/
//AnswerPart2("Data1.txt");
AnswerPart2("Data2.txt");

void AnswerPart1(string filename)
{
    var data = PuzzleFile.GetData(filename);
    Console.WriteLine($"Template:     {data.Template}");

    Part1Iterate(data, 10);
}

void AnswerPart2(string filename)
{
    var data = PuzzleFile.GetData(filename);
    Console.WriteLine($"Template:     {data.Template}");

    Part2Iterate(data, 40);
    /*
    Part2Iterate(data, 2);
    Part2Iterate(data, 3);
    Part2Iterate(data, 4);
    Part2Iterate(data, 40);
    */

}

void Part1Iterate((string Template, List<PairInsertion> PairInsertions) data, int step)
{
    var part1 = new Part1(data.Template, data.PairInsertions);
    
    var mostCommon = part1.GetMostCommonElement();
    var leastCommon = part1.GetLeastCommonElement();
    var answer = mostCommon.Count - leastCommon.Count;
    
    Console.WriteLine($"Answer after step {step}: {answer}");
    Console.WriteLine();
}

void Part2Iterate((string Template, List<PairInsertion> PairInsertions) data, int step)
{
    var part2 = new Part2(data.Template, data.PairInsertions);
    
    part2.PreformInserts(step);
    var answer = part2.GetAnswer();

    Console.WriteLine($"Answer after step {step}: {answer}");
    //Console.WriteLine();
}



    