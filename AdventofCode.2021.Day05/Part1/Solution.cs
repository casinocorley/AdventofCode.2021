using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventofCode._2021.Day05.Part1
{
    public class PuzzleInput
    {   
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }

        public PuzzleInput(Point point1, Point point2)
        {
            Point1 = point1;
            Point2 = point2;
        }
    }

    public record Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    
    public class Solution
    {
        public readonly Dictionary<Point,int> MarkedPoints = new Dictionary<Point,int>();

        public void MarkBoard(List<PuzzleInput> puzzleInputs)
        {
            puzzleInputs.ForEach(MarkBoard);
        }
        
        public void MarkBoard(PuzzleInput puzzleInput)
        {
            var inputs = new List<Point> { puzzleInput.Point1, puzzleInput.Point2 };
            
            
            if (IsHorizontalOnly(inputs[0], inputs[1]))
            {
                var y = inputs[0].Y;
                var length = Math.Abs(inputs[1].X - inputs[0].X);

                inputs = inputs.OrderBy(x => x.X).ToList();
                
                for (var i = inputs[0].X; i <= inputs[0].X + length; i++)
                {
                    MarkPoint(new Point(i, y));
                }
            }
            else if (IsVerticalOnly(inputs[0], inputs[1]))
            {
                var x = inputs[0].X;
                var length = Math.Abs(inputs[1].Y - inputs[0].Y);

                inputs = inputs.OrderBy(x => x.Y).ToList();
                
                for (var i = inputs[0].Y; i <= inputs[0].Y + length; i++)
                {
                    MarkPoint(new Point(x, i));
                }
            }
            else if (IsSame(inputs[0], inputs[1]))
            {
                // The points are the same
                MarkPoint(inputs[0]);
            }
        }

        public int GetCountWhere2Overlap()
        {
            return MarkedPoints
                .Values
                .Count(x => x >= 2);
        }

        private void MarkPoint(Point point)
        {
            if (!MarkedPoints.ContainsKey(point))
                MarkedPoints.Add(point, 1);
            else
                MarkedPoints[point] += 1;
        }

        private bool IsHorizontalOnly(Point p1, Point p2)
        {
            return p1.X != p2.X && p1.Y == p2.Y;
        }
        
        private bool IsVerticalOnly(Point p1, Point p2)
        {
            return p1.X == p2.X && p1.Y != p2.Y;
        }

        private bool IsSame(Point p1, Point p2)
        {
            return p1 == p2;
        }
    }
    
}