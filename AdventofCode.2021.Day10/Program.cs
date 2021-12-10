// See https://aka.ms/new-console-template for more information

Console.WriteLine("******* Part 1 ********");
AnswerPart1("ExampleData1.txt");
AnswerPart1("PuzzleData.txt");

Console.WriteLine();

Console.WriteLine("******* Part 2 ********");
AnswerPart2("ExampleData2.txt");
AnswerPart2("PuzzleData.txt");

void AnswerPart1(string file)
{
    var data = GetData(file);
    var answer = GetAnswerPart1(data);
    Console.WriteLine($"{file}: {answer}");
}

void AnswerPart2(string file)
{
    var data = GetData(file);
    var answer = GetAnswerPart2(data);
    Console.WriteLine($"{file}: {answer.ToString()}");
}

List<string> GetData(string fileName)
{
    return File
        .ReadLines(fileName)
        .Where(x => !string.IsNullOrWhiteSpace(x))
        .ToList();
}

int GetAnswerPart1(List<string> data)
{
    if (!data.Any())
        return 0;

    var stack = new Stack<char>();
    
    char letter = default;
    var isBad = false;

    var answer = 0;

    foreach (var line in data)
    {
        for (var i = 0; i < line.Length; i++)
        {
            letter = line[i];
            isBad = false;
            
            switch (letter)
            {
                case '(':
                    stack.Push(letter);
                    break;
                case ')':
                    isBad = stack.SafePop() != '(';
                    break;
            
                case '[':
                    stack.Push(letter);
                    break;
                case ']':
                    isBad = stack.SafePop() != '[';
                    break;
            
                case '{':
                    stack.Push(letter);
                    break;
                case '}':
                    isBad = stack.SafePop() != '{';
                    break;
            
                case '<':
                    stack.Push(letter);
                    break;
                case '>':
                    isBad = stack.SafePop() != '<';
                    break;
            
                default:
                    break;
            }

            if (isBad)
                break;
        }
        
        if (!isBad) continue;
    
        switch (letter)
        {
            case ')': 
                answer += 3;
                break;
            case ']': 
                answer += 57;
                break;
            case '}': 
                answer += 1197;
                break;
            case '>': 
                answer += 25137;
                break;
        }
    }
    
    return answer;
}

long GetAnswerPart2(List<string> data)
{
    if (!data.Any())
        return 0;

    var answers = new List<long>();
    
    var pointsMap = new Dictionary<char, long>
    {
        {'(', 1},
        {'[', 2},
        {'{', 3},
        {'<', 4}
    };

    foreach (var line in data)
    {
        var stack = new Stack<char>();
        char letter = default;
        var isCorrupted = false;
        long answer = 0;

        for (var i = 0; i < line.Length; i++)
        {
            letter = line[i];
            isCorrupted = false;
            
            switch (letter)
            {
                case '(':
                    stack.Push(letter);
                    break;
                case ')':
                    isCorrupted = stack.SafePeek() != '(';
                    if (!isCorrupted) stack.Pop();
                    break;
            
                case '[':
                    stack.Push(letter);
                    break;
                case ']':
                    isCorrupted = stack.SafePeek() != '[';
                    if (!isCorrupted) stack.Pop();
                    break;
            
                case '{':
                    stack.Push(letter);
                    break;
                case '}':
                    isCorrupted = stack.SafePeek() != '{';
                    if (!isCorrupted) stack.Pop();
                    break;
            
                case '<':
                    stack.Push(letter);
                    break;
                case '>':
                    isCorrupted = stack.SafePeek() != '<';
                    if (!isCorrupted) stack.Pop();
                    break;
            
                default:
                    break;
            }

            if (isCorrupted)
                break;
        }
        
        if (isCorrupted)
            continue;

        while (stack.Count > 0)
        {
            var l = stack.Pop();
            answer = answer * 5 + pointsMap[l];
        }
        
        answers.Add(answer);
        
    }
    
    answers.Sort();
    var x = answers[answers.Count / 2];

    return x;
}

public static class Extensions
{
    public static char SafePop(this Stack<char> stack)
    {
        return stack.Any() ? stack.Pop() : default;
    }
    
    public static char SafePeek(this Stack<char> stack)
    {
        return stack.Any() ? stack.Peek() : default;
    }
}