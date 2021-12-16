using System.Diagnostics;

namespace AdventofCode._2021.Day13;

[DebuggerDisplay("{X},{Y}")]
public record Point(int X, int Y);

[DebuggerDisplay("fold along {FoldAxis}={line}")]
public record Instruction(string FoldAxis, int line);

public class Part1
{
    public List<Point> Dots { get; private set; }

    private const string X = "x";
    private const string Y = "y";

    public Part1(List<Point> dots)
    {
        Dots = dots;
    }

    public void Fold(Instruction instruction)
    {
        Dots = instruction.FoldAxis == X ? 
            FoldLeft(instruction.line) : 
            FoldUp(instruction.line);
    }

    public void Fold(List<Instruction> instructions)
    {
        instructions.ForEach(Fold);
    }

    public List<Point> GetVisibleDots()
    {
        var visibleDots = Dots
            .Distinct()
            .OrderBy(p => p.Y)
            .ThenBy(p => p.X)
            .ToList();

        return visibleDots;
    }

    private List<Point> FoldLeft(int axis)
    {
        var left = Dots
            .Where(p => p.X < axis);

        var right = Dots
            .Where(p => p.X > axis)
            .ToList();
        
        // new x = 2(fold) - old y
        var leftFoldedLeft = left
            .Select(p => new Point(2 * axis - p.X, p.Y))
            .ToList();

        var both = new List<Point>(right);
        both.AddRange(leftFoldedLeft);

        // reset axis
        both = both
            .Select(p => new Point(p.X - (axis + 1), p.Y))
            .ToList();
        
        return both;
    }

    private List<Point> FoldUp(int axis)
    {
        var upper = Dots
            .Where(p => p.Y < axis)
            .ToList();

        var lower = Dots
            .Where(p => p.Y > axis);
        
        // new y = 2(fold) - old y
        var lowerFoldedUp = lower
            .Select(p => new Point(p.X, 2 * axis - p.Y))
            .ToList();

        var both = new List<Point>(upper);
        both.AddRange(lowerFoldedUp);

        return both;
    }
    
}