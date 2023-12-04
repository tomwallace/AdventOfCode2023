using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day04;

public class DayFour : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Cube Conundrum";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 4;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day04\DayFourInput.txt";
        var sum = SumPoints(filePath);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day04\DayFourInput.txt";
        var totalCards = RunScratchCardCopyRuleset(filePath);

        return totalCards.ToString();
    }

    internal int SumPoints(string filePath) {
        var cards = FileUtility.ParseFileToList<Scratchcard>(filePath, line => new Scratchcard(line));
        var sum = cards.Sum(c => c.CalculatePoints());

        return sum;
    }

    internal int RunScratchCardCopyRuleset(string filePath) {
        var originalCards = FileUtility.ParseFileToList<Scratchcard>(filePath, line => new Scratchcard(line));
        var cards = new Dictionary<int, Scratchcard>();
        var cardQueue = new Queue<Scratchcard>();
        var totalCards = 0;

        foreach(var card in originalCards) {
            cards.Add(card.Id, card);
            cardQueue.Enqueue(card);
        }

        while (cardQueue.Any()) {
            var current = cardQueue.Dequeue();
            totalCards++;

            // Now add cards based on number correct
            var correct = current.NumberCorrect();
            if (correct == 0)
                continue;
            
            for (int i = current.Id + 1; i <= current.Id + correct; i++) {
                cardQueue.Enqueue(cards[i]);
            }
        }
        
        return totalCards;
    }
}
