namespace AdventOfCode2023.CSharp.Day17;

public class MapState {

    public static readonly (int, int) North = (0, -1);
    
    public static readonly (int, int) East = (1, 0);
    
    public static readonly (int, int) South = (0, 1);
    
    public static readonly (int, int) West = (-1, 0);

    public int X { get; set; }

    public int Y { get; set; }

    public (int ModX, int ModY) Direction { get; set; }

    public int StepsStraight { get; set; }

    public MapState(int x, int y, (int ModX, int ModY) direction) {
        X = x;
        Y = Y;
        Direction = direction;
        StepsStraight = 1;
    }

    public MapState(int x, int y, (int ModX, int ModY) direction, int stepsStraight) {
        X = x;
        Y = y;
        Direction = direction;
        StepsStraight = stepsStraight;
    }

    public List<(int ModX, int ModY)> AvailableSteps(int minSteps) {
        if (StepsStraight < minSteps - 1)
            return new List<(int ModX, int ModY)>() { Direction };
        
        var directions = new List<(int ModX, int ModY)>();
        if (Direction != South)
        {
            directions.Add(North);
        }
        
        if (Direction != West)
        {
            directions.Add(East);
        }
        
        if (Direction != North)
        {
            directions.Add(South);
        }

        if (Direction != East)
        {
            directions.Add(West);
        }
        
        return directions;
    }

    public override string ToString()
    {
        return $"{X},{Y},{Direction.ModX},{Direction.ModY}";
    }
}