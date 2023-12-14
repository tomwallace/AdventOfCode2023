using AdventOfCode2023.CSharp.Utility;
using System.Globalization;
using System.Text;

namespace AdventOfCode2023.CSharp.Day12;

public class DayTwelve : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Hot Springs";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 12;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day12\DayTwelveInput.txt";
        var sum = SumValidArrangements(filePath);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day12\DayTwelveInput.txt";
        //var sumPower = SumPowerOfLeastCubes(filePath);

        return "";
    }

    internal int SumValidArrangements(string filePath, bool isFolded = false)
    {
        var lines = FileUtility.ParseFileToList<Line>(filePath, line => new Line(line, isFolded));
        return lines.Sum(l => l.FindValidCombinations());
    }
    
    
}

public class Line
{
    public string Chars { get; }

    public List<int> Rules { get; }

    public Line(string input, bool isFolded = false) {
        var split = input.Split(' ');
        Chars = split[0];
        if (!isFolded)
        {
            Chars = split[0];
            Rules = split[1].Split(',').Select(c => int.Parse(c)).ToList();
        }
        else
        {
            var sb = new StringBuilder();
            Rules = new List<int>();
            var charSet = split[0];
            var ruleSet = split[1].Split(',').Select(c => int.Parse(c)).ToList();
            for (int i = 0; i < 5; i++)
            {
                sb.Append(charSet);
                sb.Append("?");
                Rules.AddRange(ruleSet);
            }
            sb.Remove(sb.Length - 1, 1);
            Chars = sb.ToString();
        }
    }

    public int FindValidCombinations() {
        //return FindValidCombinationsRecursive(Chars);
        if (!Chars.Any(c => c == '?'))
        {
            var isValid = IsLineValid(Chars);
            return isValid ? 1 : 0;
        }

        var combos = new List<string>();
        var queue = new Queue<string>();
        queue.Enqueue(new string(Chars));

        do
        {
            var current = queue.Dequeue();
            if (!current.Any(c => c == '?'))
            {
                combos.Add(current);
                continue;
            }

            var i = current.ToList().FindIndex(c => c == '?');
            var withHash = current.ToCharArray();
            withHash[i] = '#';
            queue.Enqueue(new string(withHash));

            var withDot = current.ToCharArray();
            withDot[i] = '.';
            queue.Enqueue(new string(withDot));

        } while (queue.Any());
        
        var valid = combos.Where(c => IsLineValid(c));
        return valid.Count();
    }

    // TODO: Investigate why recursive is returning numbers that are too high
    private int FindValidCombinationsRecursive(string chars)
    {
        if (!chars.Any(c => c == '?') )
        {
            var isValid = IsLineValid(chars);
            return isValid ? 1 : 0;
        }
        
        var validCombos = 0;
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] != '?')
                continue;
            var withHash = chars.ToCharArray();
            withHash[i] = '#';
            validCombos += FindValidCombinationsRecursive(new string(withHash));
            
            var withDot = chars.ToCharArray();
            withDot[i] = '.';
            validCombos += FindValidCombinationsRecursive(new string(withDot));
        }

        return validCombos;
    }

    internal bool IsLineValid(string chars)
    {
        if (chars.Any(c => c == '?'))
            throw new Exception("Cannot validate lines with ?");

        var groupsPresent = new List<int>();
        var currentSize = 0;
        var inGroup = false;
        for (int i = 0; i < chars.Length; i++)
        {
            var c = chars[i];
            if (c == '#')
            {
                currentSize++;
                if (!inGroup)
                    inGroup = true;    
            } else
            {
                if (inGroup)
                {
                    inGroup = false;
                    groupsPresent.Add(currentSize);
                    currentSize = 0;
                }
            }
        }

        // If we ended in a group, we need to add it now
        if (inGroup)
            groupsPresent.Add(currentSize);

        if (groupsPresent.Count != Rules.Count)
            return false;

        for (int i = 0; i < groupsPresent.Count; i++)
        {
            if (groupsPresent[i] != Rules[i])
                return false;
        }

        return true;
    }
}
