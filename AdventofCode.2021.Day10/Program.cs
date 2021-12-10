// See https://aka.ms/new-console-template for more information

Console.WriteLine("******* Part 1 ********");
AnswerPart1("ExampleData.txt");
AnswerPart1("PuzzleData.txt");

Console.WriteLine();

Console.WriteLine("******* Part 2 ********");
AnswerPart2("ExampleData.txt");
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
    Console.WriteLine($"{file}: {answer}");
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


    foreach (var letter in data[0])
    {
        
    }
    
    
    
    var parenthese = new List<int>();
    var square = new List<int>();
    var bracket = new List<int>();
    var caret = new List<int>();

    char orphan = default;

    for (var i = 0; i < data[0].Length; i++)
    {
        var letter = data[0][i];
        
        if (letter == '(')
            parenthese.Add(i);
        else if (letter == ')')
        {
            if (parenthese.Count == 0)
            {
                orphan = letter;
                break;
            }
            parenthese.RemoveLast();

        }
        else if (letter == '[')
            square.Add(i);
        else if (letter == ']')
        {
            if (square.Count == 0)
            {
                orphan = letter;
                break; 
            }
            square.RemoveLast();

        }
        else if (letter == '{')
            bracket.Add(i);
        else if (letter == '}')
        {
            if (bracket.Count == 0)
            {
                orphan = letter;
                break; 
            }
            bracket.RemoveLast();

        }
        else if (letter == '<')
            caret.Add(i);
        else if (letter == '>')
        {
            if (caret.Count < 0)
            {
                orphan = letter;
                break; 
            }
            caret.RemoveLast();
        }

        if (orphan != default)
        {
            switch (orphan)
            {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;
                default:
                    return 0;
            }
        }

        int? p = parenthese.Any() ? parenthese.Min() : null;
        int? s = square.Any() ? square.Min() : null;
        int? b = bracket.Any() ?  bracket.Min() : null;
        int? c = caret.Any() ? caret.Min() : null;
    }
    
    return 0;
}


string GetAnswerPart2(List<string> data)
{
    return "";
}

public static class Extensions
{
    public static void RemoveLast(this List<int> list)
    {
        if (list.Count == 0)
            return;
        
        list.RemoveAt(list.Count - 1);
    }
}