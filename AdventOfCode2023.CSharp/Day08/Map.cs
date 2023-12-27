using System.Numerics;
using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day08;

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

    public int StepsToTraverse(string currentKey)
    {
        var stepCount = 0;
        var stepIndex = 0;

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

        } while (currentKey[2] != 'Z');

        return stepCount;
    }

    public long GhostStepsToTraverse()
    {
        var startingKeys = Nodes.Keys.Where(n => n[2] == 'A').ToList();
        var counts = startingKeys.Select(s => (long) StepsToTraverse(s)).ToList();;

        // Had to look up a forumula to find the first step they would all overlap
        return LowestCommonMultiple(counts.ToList());
    }

    private long LowestCommonMultiple(List<long> input)
    {
        var queue = new Queue<long>(input.Count * 2);

        foreach (var item in input)
        {
            queue.Enqueue(item);
        }
        
        while (true)
        {
            long left;
            
            long right;
            
            if (queue.Count == 2)
            {
                left = queue.Dequeue();

                right = queue.Dequeue();

                return left * right / GreatestCommonFactor(left, right);
            }

            left = queue.Dequeue();

            right = queue.Dequeue();

            var lowestCommonMultiple = left * right / GreatestCommonFactor(left, right);

            queue.Enqueue(lowestCommonMultiple);
        }
    }

    private long GreatestCommonFactor(long left, long right)
    {
        var gcdExponentOnTwo = BitOperations.TrailingZeroCount(left | right);

        left >>= gcdExponentOnTwo;
        
        right >>= gcdExponentOnTwo;

        while (left != right)
        {
            if (left < right)
            {
                right -= left;

                right >>= BitOperations.TrailingZeroCount(right);
            }
            else
            {
                left -= right;

                left >>= BitOperations.TrailingZeroCount(left);
            }
        }

        return left << gcdExponentOnTwo;
    }
}
