using AdventOfCode2023.CSharp.Utility;


namespace AdventOfCode2023.CSharp.Day19;

public class DayNineteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Aplenty";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 19;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day19\DayNineteenInput.txt";
        var sum = SumAcceptedPartsRatingNumbers(filePath);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day19\DayNineteenInput.txt";
        var count = FindAcceptedRatingCombosRanges(filePath);

        return count.ToString();
    }

    internal int SumAcceptedPartsRatingNumbers(string filePath) {
        var processedFilePath = ProcessFilePath(filePath);
        var workflows = processedFilePath.Workflows;
        var parts = processedFilePath.Parts;
        
        var acceptedParts = new List<Part>();
        foreach(var part in parts) {
            if (DetermineIfPartAccepted(workflows, part) == 1)
                acceptedParts.Add(part);
        }

        var sum = acceptedParts.Sum(a => a.GetRatingNumber());
        return sum;
    }

    internal long FindAcceptedRatingCombosRanges(string filePath) {
        var processedFilePath = ProcessFilePath(filePath);
        var workflows = processedFilePath.Workflows;
        var ranges = new Dictionary<string, MRange>() {
            { "x", new MRange(1, 4000) },
            { "m", new MRange(1, 4000) },
            { "a", new MRange(1, 4000) },
            { "s", new MRange(1, 4000) }
        };

        long validCombos = FindAcceptedCombosRecursive(ranges, workflows["in"], workflows);
        return validCombos;
    }

    private long FindAcceptedCombosRecursive(Dictionary<string, MRange> ranges, Workflow start, Dictionary<string, Workflow> workflows) {
        long validCombos = 0;
        foreach (var rule in start.Rules) {
            // Clone into new ranges so we don't update the previous dictionary by reference
            var newRanges = new Dictionary<string, MRange>();
            foreach(var r in ranges) {
                newRanges.Add(r.Key, r.Value.Clone());
            }

            if (rule.Eval == '>') {
                // Are there valid points at all
                if (ranges[rule.Rating].Max > rule.Number) {
                    // Set the minimum to the rule number if it is higher - narrowing the range
                    newRanges[rule.Rating].Min = newRanges[rule.Rating].Min > rule.Number + 1 ? newRanges[rule.Rating].Min : rule.Number + 1;
                
                    if (rule.Output == "A")
                        validCombos += DictionaryLength(newRanges);
                    else if (rule.Output != "R")
                        validCombos += FindAcceptedCombosRecursive(newRanges, workflows[rule.Output], workflows);
                    
                    // Invalid values need to flow forward
                    ranges[rule.Rating].Max = rule.Number;
                }
            }

            if (rule.Eval == '<') {
                // Are there valid points at all
                if (ranges[rule.Rating].Min < rule.Number) {
                    // Set the maximum to the rule number if it is lower - narrowing the range
                    newRanges[rule.Rating].Max = newRanges[rule.Rating].Max < rule.Number - 1 ? newRanges[rule.Rating].Max : rule.Number - 1;
                
                    if (rule.Output == "A")
                        validCombos += DictionaryLength(newRanges);
                    else if (rule.Output != "R")
                        validCombos += FindAcceptedCombosRecursive(newRanges, workflows[rule.Output], workflows);
                    
                    // Invalid values need to flow forward
                    ranges[rule.Rating].Min = rule.Number;
                }
            }
        }

        // Handle the default ends of the workflow
        if (start.End == "A")
            validCombos += DictionaryLength(ranges);
        else if (start.End != "R")
            validCombos += FindAcceptedCombosRecursive(ranges, workflows[start.End], workflows);

        return validCombos;
    }

    private long DictionaryLength(Dictionary<string, MRange> ranges) {
        return ranges.Aggregate(1L, (a, b) => a *= b.Value.Length());
    }

    private long DetermineIfPartAccepted(Dictionary<string, Workflow> workflows, Part part) {
        var key = "in";
        do {
            var workflow = workflows[key];
            var output = workflow.Process(part);
            if (output == "A") {
                return 1;
            }
            if (output == "R")
                return 0;
            
            key = output;

        } while (1 == 1);
    }

    private (Dictionary<string, Workflow> Workflows, List<Part> Parts) ProcessFilePath(string filePath) {
        var workflows = new Dictionary<string, Workflow>();
        var parts = new List<Part>();
        
        var lines = FileUtility.ParseFileToList(filePath);
        var switchToParts = false;
        foreach(var line in lines) {
            if (switchToParts) {
                parts.Add(new Part(line));
                continue;
            }
                
            if (line == "") {
                switchToParts = true;
                continue;
            }

            var workflow = new Workflow(line);
            workflows.Add(workflow.Id, workflow);
        }

        return (workflows, parts);
    }
}