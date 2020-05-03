using System;
using Generator;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = new Map.Builder()
                .WithSize(32, 16)
                .WithRoomSize(4, 4)
                .Build();

            var room1 = map.CreateRoomTemplate()
                .SetCell(2, 2, Cells.Wall);

            var room2 = map.CreateRoomTemplate()
                .SetCell(1, 1, Cells.Wall)
                .SetCell(2, 2, Cells.Wall);

            var room3 = map.CreateRoomTemplate()
                .SetCell(2, 2, Cells.Treasure);

            var room4 = map.CreateRoomTemplate()
                .SetCell(2, 2, Cells.Trap);

            var room5 = map.CreateRoomTemplate();

            var mapGenerator = new MapGenerator(map)
                .AddRoomTemplates(room1, room2, room3, room4, room5);
            
            mapGenerator.Generate();

            Console.WriteLine(map);
        }
    }
}