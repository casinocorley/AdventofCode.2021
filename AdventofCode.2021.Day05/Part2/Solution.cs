using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventofCode._2021.Day05.Part1;

namespace AdventofCode._2021.Day05.Part2
{
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
            else if (IsDiagonal(inputs[0], inputs[1]))
            {
                var distance = Math.Abs(inputs[0].X - inputs[1].X);
                var leftToRight = inputs[0].X < inputs[1].X;
                var upToDown = inputs[0].Y < inputs[1].Y;

                for (var i = 0; i <= distance; i++)
                {
                    var x = leftToRight ?
                        inputs[0].X + i :
                        inputs[0].X - i;

                    var y = upToDown ? 
                        inputs[0].Y + i : 
                        inputs[0].Y - i;
                    
                    MarkPoint(new Point(x,y));
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

        private bool IsDiagonal(Point p1, Point p2)
        {
            var xDistance = Math.Abs(p1.X - p2.X);
            var yDistance = Math.Abs(p1.Y - p2.Y);

            return xDistance == yDistance;
        }

        private bool IsSame(Point p1, Point p2)
        {
            return p1 == p2;
        }
    }
    
}