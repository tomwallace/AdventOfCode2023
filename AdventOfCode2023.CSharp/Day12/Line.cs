using System.Text;

namespace AdventOfCode2023.CSharp.Day12;

public class Line
{
    public string Chars { get; }

    public List<int> Rules { get; }

    private readonly Dictionary<string, long> cache = new();

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

    public long FindValidCombinations() {
        return GetArrangements(Chars, Rules.ToArray(), Rules.Sum());
    }

    // Originally, I used a queue based approach for Part A, but with Part B the sheer number of combinations
    // made it impossible.  Then I used a straight recursive approach, but the same issue occurred.
    // I looked at the Reddit thread and found this method of gradually trimming down the string and cutting off
    // the branches that could not work.
    private long GetArrangements(string chars, int[] rules, int sum)
    {
        var key = $"{chars}{string.Join(',', rules)}";
        
        if (cache.TryGetValue(key, out var answer))
            return answer;

        answer = CalculateArrangements(chars, rules, sum);
        
        cache.Add(key, answer);

        return answer;
    }

    private long CalculateArrangements(string chars, int[] rules, int sum)
    {
        chars = chars.TrimStart('.');

        var length = chars.Length;
        var rulesLength = rules.Length;
        
        // Reached end of string, if we have expended all the rules, then it is valid
        if (length == 0)
            return rulesLength == 0 ? 1 : 0;

        // If the rule have run out, and nothing left is a #, then it is valid
        if (rulesLength == 0)
            return chars.Any(c => c == '#') ? 0 : 1;

        // If there are not enough non '.' chars left to satisfy the rule, then it is not valid
        var count = chars.Count(c => c != '.');
        if (sum > count)
            return 0;

        // Process if the current first char is a #, otherwise proceed if a ?
        if (chars[0] == '#')
        {
            var rule = rules[0];

            // The remaining length cannot satisfy the first remaining rule, so invalid
            if (length < rule)
                return 0;

            for (var i = 0; i < rule; i++)
            {
                // The chars cannot satisfy the rule, so invalid
                if (chars[i] == '.')
                    return 0;
            }

            // Rule matches our string length, which is all # or ?, and it is only one character, then valid
            if (length == rule)
                return rulesLength == 1 ? 1 : 0;

            // If final char is a #, then invalid
            if (chars[rule] == '#')
                return 0;
            
            return GetArrangements(chars[(rule + 1)..], rules[1..], sum - rules[0]);
        }

        return GetArrangements($"#{chars[1..]}", rules, sum) + GetArrangements(chars[1..], rules, sum);
    }
}
