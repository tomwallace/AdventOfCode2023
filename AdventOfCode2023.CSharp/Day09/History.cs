using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day09;

public class History {
    public List<int> Readings { get; }

    public History(string line) 
    {
        Readings = line.Split(' ', StringSplitOptions.TrimEntries).Select(e => int.Parse(e)).ToList();

    }

    public int ExtrapolateNextValue(bool fromFront)
    {
        var calculations = new List<List<int>>
        {
            Readings
        };
        var current = new List<int>();

        do 
        {
            var last = calculations.Last();
            current = new List<int>();
            foreach(var pair in last.Pairwise()) 
            {
                current.Add(pair.Item2 - pair.Item1);
            }
            calculations.Add(current);

        } while (!current.All(c => c == 0));

        var reverse = calculations.ToList();
        reverse.Reverse();
        
        if (fromFront) {
            reverse[0].Insert(0, 0);

            foreach(var pair in reverse.Pairwise()) {
                var newReading = pair.Item2.First() - pair.Item1.First();
                pair.Item2.Insert(0, newReading);
            }

            return reverse.Last().First();
        }
        
        reverse[0].Add(0);

        foreach(var pair in reverse.Pairwise()) {
            var newReading = pair.Item1.Last() + pair.Item2.Last();
            pair.Item2.Add(newReading);
        }

        return reverse.Last().Last();
    }

}
