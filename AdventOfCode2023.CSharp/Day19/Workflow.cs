namespace AdventOfCode2023.CSharp.Day19;

public class Workflow {
    public string Id { get; }

    public List<Rule> Rules { get; }

    public string End { get; }

    // Ex: px{a<2006:qkq,m>2090:A,rfg}
    public Workflow(string line) {
        Rules = new List<Rule>();
        var splitBracket = line.Split('{');
        Id = splitBracket[0];
        var splitRules = splitBracket[1].Replace("}", "").Split(',');
        End = splitRules.Last();
        for (int i = 0; i < splitRules.Length - 1; i++) {
            Rules.Add(new Rule(splitRules[i]));
        }
    }

    public string Process(Part part) {
        for (var i = 0; i < Rules.Count; i++) {
            if (Rules[i].Applies(part))
                return Rules[i].Output;
        }

        return End;
    }

    public override string ToString()
    {
        return $"{Id}";
    }
}
