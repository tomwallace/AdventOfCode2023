namespace AdventOfCode2023.CSharp.Day19;

public class Rule {
    public string Rating { get; }

    public char Eval { get; }

    public int Number { get; }

    public string Output { get; }

    // Ex: x>10:one
    public Rule(string input) {
        var splitColon = input.Split(':');
        Output = splitColon[1];
        if (splitColon[0].Contains(">")) {
            Eval = '>';
            var splitGT = splitColon[0].Split('>');
            Rating = splitGT[0];
            Number = int.Parse(splitGT[1]);
        } else {
            Eval = '<';
            var splitLT = splitColon[0].Split('<');
            Rating = splitLT[0];
            Number = int.Parse(splitLT[1]);
        }
    }

    public bool Applies(Part part) {
        var value = part.Ratings[Rating];
        if (Eval == '>') {
            return value > Number;
        }
        return value < Number;
    }

    public override string ToString()
    {
        return $"{Rating},{Eval},{Number},{Output}";
    }
}
