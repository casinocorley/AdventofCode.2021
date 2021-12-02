using System;

namespace AdventofCode._2021.Day02.Part1
{
    public class Map
    {
        public Position Position { get; private set; }

        public Map()
        {
            Position = new Position(0, 0);
        }

        public void Move(string command)
        {
            var request = command.Split(' ');

            if (request.Length != 2)
                throw new ArgumentException("Unable to parse command");
            
            var direction = request[0];
            var distance = int.Parse(request[1]);

            switch (direction)
            {
                case "forward":
                    Position.Horizontal += distance;
                    break;
                case "up":
                    Position.Depth -= distance;
                    break;
                case "down":
                    Position.Depth += distance;
                    break;
                default:
                    throw new ArgumentException($"Unable to determine direction {direction}");
            }
        }
    }

    public class Position
    {
        public int Horizontal { get; set; }

        public int Depth { get; set; }
        
        public Position()
            : this(0, 0)
        {
        }

        public Position(int startingX, int startingY)
        {
            Horizontal = startingX;
            Depth = startingY;
        }
    }
}