namespace AdventOfCode2023.CSharp.Day07;

public interface IHandScorer
{
    public bool Applies(List<char> cards, bool useJokers);

    public string Score();
}
