namespace AdventOfCode2023.CSharp.Day08;

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
