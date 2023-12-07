namespace AdventOfCode2023.CSharp.Day07;

public class FullHouse : IHandScorer
{
    public bool Applies(List<char> cards, bool useJokers)
    {
        if (!useJokers || !cards.Contains('*'))
        {
            if (!CardHelper.CardValues.Keys.Any(a => cards.Where(c => a.Equals(c)).Count() == 3))
                return false;

            var three = CardHelper.CardValues.Keys.First(a => cards.Where(c => a.Equals(c)).Count() == 3);

            var twos = CardHelper.CardValues.Keys.Where(a => cards.Where(c => a.Equals(c)).Count() == 2);

            return twos.Any(t => !t.Equals(three));
        }

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

    public string Score() => "5";
}
