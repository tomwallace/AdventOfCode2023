using System.Text;

namespace AdventOfCode2023.CSharp.Day07;

public class Hand
{
    private List<IHandScorer> scorers = new List<IHandScorer>()
    {
        new FiveOfKind(),
        new FourOfKind(),
        new FullHouse(),
        new ThreeOfKind(),
        new TwoPair(),
        new OnePair(),
        new HighCard()
    };

    private bool useJokers;
    
    public List<char> Cards { get; }

    public int Bid { get; }

    public Hand(string input, bool jokers) 
    {
        if (jokers)
            input = input.Replace('J', '*');

        var split = input.Split(' ');
        Cards = split[0].ToCharArray().ToList();
        Bid = int.Parse(split[1]);
        useJokers = jokers;
    }

    public long Score()
    {
        var scoreBuilder = new StringBuilder();
        foreach(var scorer in scorers) 
        {
            if (scorer.Applies(Cards, useJokers))
            {
                scoreBuilder.Append(scorer.Score());
                break;
            }
        }

        // Now need to piece together rest
        foreach(var card in Cards) 
        {
            scoreBuilder.Append(CardHelper.CardValuesWithJokers[card]);
        }

        return long.Parse(scoreBuilder.ToString());
    }
   
}