namespace AdventOfCode2023.CSharp.Day19;

public class Part {
    public Dictionary<string,int> Ratings { get; set; }

    // Ex: {x=787,m=2655,a=1222,s=2876}
    public Part(string line) {
        Ratings = new Dictionary<string, int>();
        var split = line.Replace("{", "").Replace("}", "").Split(',');
        foreach (var part in split) {
            var partSplit = part.Split('=');
            Ratings.Add(partSplit[0], int.Parse(partSplit[1]));
        }
    }

    public Part(int x, int m, int a, int s) {
        Ratings = new Dictionary<string, int>
        {
            { "x", x },
            { "m", m },
            { "a", a },
            { "s", s }
        };
    }

    public int GetRatingNumber() {
        return Ratings.Sum(r => r.Value);
    }

    public override string ToString()
    {
        var x = Ratings["x"];
        var m = Ratings["m"];
        var a = Ratings["a"];
        var s = Ratings["s"];
        return $"x={x},m={m},a={a},s={s}";
    }
}
