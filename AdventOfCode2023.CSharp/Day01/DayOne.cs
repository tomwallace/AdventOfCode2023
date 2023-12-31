using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day01;

public class DayOne : IAdventProblemSet
{
    private Dictionary<string, string> numbers = new Dictionary<string, string>()
    {
        {"one", "1"}, {"two", "2"}, {"three", "3" }, {"four", "4"}, {"five", "5"}, {"six", "6"}, {"seven", "7"}, {"eight", "8"}, {"nine", "9"}
    };
    
    /// <inheritdoc />
    public string Description()
    {
        return "Trebuchet?!";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 1;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day01\DayOneInput.txt";
        var sum = SumLinesOfFirstAndLastDigits(filePath, false);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day01\DayOneInput.txt";
        var sum = SumLinesOfFirstAndLastDigits(filePath, true);

        return sum.ToString();
    }

    internal int SumLinesOfFirstAndLastDigits(string filePath, bool useWords)
    {
        var lines = FileUtility.ParseFileToList(filePath, line => useWords ? ProcessLineAsWords(line) : ProcessLine(line));

        return lines.Sum();
    }

    internal int ProcessLine(string line)
    {
        var charInts = line.ToCharArray().Where(c => char.IsDigit(c)).ToList();
        return int.Parse($"{charInts.First()}{charInts.Last()}");
    }


    internal int ProcessLineAsWords(string line)
    {
        var lowestIndex = 99999;
        var lowestNum = "";
        var highestIndex = -1;
        var highestNum = "";

        for( int i = 0; i < line.Length; i++ )
        {
            if (!char.IsDigit(line[i]))
                continue;

            if (i <= lowestIndex)
            {
                lowestIndex = i;
                lowestNum = line[i].ToString();
            }

            if (i >= highestIndex)
            {
                highestIndex = i;
                highestNum = line[i].ToString();
            }
        }

        foreach(var d in numbers)
        {
            var indx = line.IndexOf(d.Key);
            if (indx == -1)
                continue;

            if (indx <= lowestIndex)
            {
                lowestIndex = indx;
                lowestNum = d.Value;
            }

            var highIndx = line.LastIndexOf(d.Key);
            if (highIndx >= highestIndex)
            {
                highestIndex = highIndx;
                highestNum = d.Value;
            }
        }

        return int.Parse($"{lowestNum}{highestNum}");
    }

    
}