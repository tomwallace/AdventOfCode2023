namespace AdventOfCode2023.CSharp.Day07;

public class FourOfKind : IHandScorer
{
    public bool Applies(List<char> cards, bool useJokers)
    {
        if (!useJokers || !cards.Contains('*'))
            return CardHelper.CardValues.Keys.Any(a => cards.Where(c => a.Equals(c)).Count() == 4);

        // Step through all possiblities recursively
        for (var i = 0; i < cards.Count; i++)
        {
            if ((cards[i] != '*'))
                continue;

            foreach (var sub in CardHelper.CardValues.Keys)
            {
                var newHand = cards.ToList();
                newHand[i] = sub;
                if (Applies(newHand, useJokers))
                    return true;
            }
        }

        return false;
    }

    public string Score() => "6";
}
