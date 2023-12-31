namespace AdventOfCode2023.CSharp.Day04;

public class Scratchcard {
    public int Id {get; set;}
    
    public List<int> Correct {get; set;}

    public List<int> Actual {get; set;}

    public Scratchcard(string line) {
        var idSplit = line.Split(':', StringSplitOptions.TrimEntries);
        Id = int.Parse(idSplit[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1].Trim());

        var numbers = idSplit[1].Split('|', StringSplitOptions.TrimEntries);
        Correct = numbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n.Trim())).ToList();
        Actual = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n.Trim())).ToList();
    }

    public int NumberCorrect() {
        return Actual.Where(a => Correct.Contains(a)).Count();;
    }
    public int CalculatePoints() {
        var numberCorrect = NumberCorrect();
        if (numberCorrect == 0)
            return 0;

        var points = 1;
        for (int i = 0; i < numberCorrect - 1; i++) {
            points = points * 2;
        }
        return points;
    }
}