using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day08;

public class DayEight : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Haunted Wasteland";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 8;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day08\DayEightInput.txt";
        var map = new Map(filePath);

        return map.StepsToTraverse().ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day08\DayEightInput.txt";
        var map = new Map(filePath);

        return map.GhostStepsToTraverse().ToString();
    }

    

    
}

public class Map
{
    public Dictionary<string, Node> Nodes { get; }

    public char[] Steps { get; }

    public Map(string filePath) 
    { 
        var lines = FileUtility.ParseFileToList(filePath);
        Steps = lines[0].ToCharArray();
        Nodes = new Dictionary<string, Node>();

        for (int i = 2; i < lines.Count; i++)
        {
            var node = new Node(lines[i]);
            Nodes.Add(node.Key, node);
        }
    }

    public int StepsToTraverse()
    {
        var stepCount = 0;
        var stepIndex = 0;
        var currentKey = "AAA";

        do
        {
            var dir = Steps[stepIndex];
            var node = Nodes[currentKey];
            if (dir == 'L')
                currentKey = node.Left;
            else
                currentKey = node.Right;
            
            stepCount++;
            stepIndex = stepIndex == Steps.Length - 1 ? 0 : stepIndex + 1; 

        } while (currentKey != "ZZZ");

        return stepCount;
    }

    public int GhostStepsToTraverse()
    {
        var stepCount = 0;
        var stepIndex = 0;
        var currentKeys = Nodes.Keys.Where(n => n[2] == 'A').ToList();

        do
        {
            var dir = Steps[stepIndex];
            var newKeys = new List<string>();
            foreach (var currentKey in currentKeys)
            {
                var node = Nodes[currentKey];
                if (dir == 'L')
                    newKeys.Add(node.Left);
                else
                    newKeys.Add(node.Right);
            }
            
            stepCount++;
            stepIndex = stepIndex == Steps.Length - 1 ? 0 : stepIndex + 1;
            currentKeys = newKeys.ToList();

        } while (!GhostsDone(currentKeys));

        return stepCount;
    }

    private bool GhostsDone(List<string> currentKeys)
    {
        return currentKeys.All(ck => ck[2] == 'Z');
    }
}

public class Node
{
    public string Key { get; }
    
    public string Left { get; }

    public string Right { get; }

    public bool EndsInA { get; }

    public bool EndsInZ { get; }

    public Node(string line) 
    { 
        var split = line.Split('=', StringSplitOptions.TrimEntries);
        Key = split[0];
        EndsInA = Key[2] == 'A';
        EndsInZ = Key[2] == 'Z';

        var splitSteps = split[1].Split(",", StringSplitOptions.TrimEntries);
        Left = splitSteps[0].Replace("(", "");
        Right = splitSteps[1].Replace(")", "");
    }
}
