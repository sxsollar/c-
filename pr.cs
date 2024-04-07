using System;
using System.Collections.Generic;
using System.Linq;

namespace CADPlacing
{
    // Enum for different types of furniture
    enum FurnitureType
    {
        Chair,
        Table,
        Sofa,
        Bed,
        Wardrobe
    }

    // Class to represent furniture items
    class Furniture
    {
        public FurnitureType Type { get; private set; }
        public double Width { get; private set; }
        public double Length { get; private set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Furniture(FurnitureType type, double width, double length)
        {
            Type = type;
            Width = width;
            Length = length;
        }

        public void DisplayPosition()
        {
            Console.WriteLine($"Type: {Type}, Position: ({X}, {Y})");
        }
    }

    // Class to represent a room
    class Room
    {
        public double Width { get; private set; }
        public double Length { get; private set; }

        public Room(double width, double length)
        {
            Width = width;
            Length = length;
        }

        public double Area => Width * Length;
    }

    // Class to handle furniture placement
    class PlacementAlgorithm
    {
        public static void OptimizePlacement(Room room, List<Furniture> furniture)
        {
            double x = 0;
            double y = 0;

            foreach (var item in furniture)
            {
                // Find the first available position
                while (!IsPositionAvailable(room, furniture, item, x, y))
                {
                    x += 0.1; // Move to the next position (increment x coordinate)
                    if (x + item.Width > room.Width) // If item goes out of room's width
                    {
                        x = 0; // Reset x to the beginning
                        y += 0.1; // Move to the next row (increment y coordinate)
                    }

                    if (y + item.Length > room.Length) // If item goes out of room's length
                    {
                        Console.WriteLine($"Error: Insufficient space to place {item.Type}.");
                        return; // Abort placement
                    }
                }

                // Set the position for the item
                item.X = x;
                item.Y = y;
            }
        }

        private static bool IsPositionAvailable(Room room, List<Furniture> furniture, Furniture item, double x, double y)
        {
            foreach (var otherItem in furniture)
            {
                if (otherItem != item && AreOverlapping(item, x, y, otherItem))
                    return false;
            }
            return true;
        }

        private static bool AreOverlapping(Furniture item1, double x1, double y1, Furniture item2)
        {
            double x2 = x1 + item1.Width;
            double y2 = y1 + item1.Length;
            return x1 < item2.X + item2.Width && x2 > item2.X && y1 < item2.Y + item2.Length && y2 > item2.Y;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a room
            Room room = new Room(10, 8); // Room dimensions: 10x8 meters

            // Create furniture items
            List<Furniture> furniture = new List<Furniture>
            {
                new Furniture(FurnitureType.Chair, 0.5, 0.5),
                new Furniture(FurnitureType.Table, 1.5, 1),
                new Furniture(FurnitureType.Sofa, 2, 1),
                new Furniture(FurnitureType.Bed, 2, 1.5),
                new Furniture(FurnitureType.Wardrobe, 1, 0.5)
            };

            // Display initial positions of furniture
            Console.WriteLine("Initial positions:");
            foreach (var item in furniture)
            {
                item.DisplayPosition();
            }

            // Optimize placement of furniture within the room
            PlacementAlgorithm.OptimizePlacement(room, furniture);

            // Display final positions of furniture
            Console.WriteLine("\nFinal positions:");
            foreach (var item in furniture)
            {
                item.DisplayPosition();
            }
        }
    }
}







