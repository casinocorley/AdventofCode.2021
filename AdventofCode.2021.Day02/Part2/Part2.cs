using System;

namespace AdventofCode._2021.Day02.Part2
{
    public class Map
    {
        public Position Position { get; private set; }

        public Map()
        {
            Position = new Position(0, 0, 0);
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
                    Position.Depth += Position.Aim * distance;
                    break;
                case "up":
                    Position.Aim -= distance;
                    break;
                case "down":
                    Position.Aim += distance;
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

        public int Aim { get; set; }
        
        public Position()
            : this(0, 0, 0)
        {
        }

        public Position(int startingX, int startingY, int aim)
        {
            Horizontal = startingX;
            Depth = startingY;
            Aim = aim;
        }
    }
}