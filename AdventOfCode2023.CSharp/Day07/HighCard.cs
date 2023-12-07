namespace AdventOfCode2023.CSharp.Day07;

public class HighCard : IHandScorer
{
    public bool Applies(List<char> cards, bool useJokers)
    {
        // If we get here, it has to be applicable
        return true;
    }

    public string Score() => "1";
}
