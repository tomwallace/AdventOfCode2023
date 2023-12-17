using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day16;

public class DaySixteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "The Floor Will Be Lava";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 16;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day16\DaySixteenInput.txt";
        var count = CountEnergizedCells(filePath);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day16\DaySixteenInput.txt";
        //var sumPower = SumPowerOfLeastCubes(filePath);

        return "";
    }

    internal int CountEnergizedCells(string filePath) {
        var contraption = new Contraption(filePath);
        //contraption.Print();
        contraption.Activate();
        return contraption.EnergizedPoints.Count;
    }
    
}

public class Contraption {
    public List<List<char>> Grid { get; }

    public HashSet<string> EnergizedPoints { get; set; }

    public Dictionary<string, string> SeenBefore {get; set; }

    public List<Beam> Beams { get; set; }

    public Contraption(string filePath) {
        Grid = FileUtility.ParseFileToList<List<char>>(filePath, line => line.ToCharArray().ToList());
        var beam = new Beam(-1, 0, 1, 0);
        EnergizedPoints = new HashSet<string>();
        SeenBefore = new Dictionary<string, string>();
        Beams = new List<Beam>() { beam };
    }

    public void Activate() {
        var time = 0;
        do {
            time++;
            Beam extraBeam = null;
            for (var b = 0; b < Beams.Count; b++) {
                var beam = Beams[b];

                var nextX = beam.X + beam.ModX;
                var nextY = beam.Y + beam.ModY;

                if (IsOutOfBounds(nextX, nextY)) {
                    Beams.Remove(beam);
                    continue;
                }
                
                beam.X = nextX;
                beam.Y = nextY;
                EnergizedPoints.Add(beam.ToString());
                
                var cell = Grid[nextY][nextX];

                switch (cell) {
                    case '.':
                        break;
                    case '/': case '\\':
                        beam.HitMirror(cell);
                        break;
                    case '-':
                        if (beam.ModY != 0) {
                            extraBeam = new Beam(nextX, nextY, -1, 0);
                            beam.ModX = 1;
                            beam.ModY = 0;
                        }
                        break;
                    case '|':
                        if (beam.ModX != 0) {
                            extraBeam = new Beam(nextX, nextY, 0, -1);
                            beam.ModX = 0;
                            beam.ModY = 1;
                        }
                        break;
                    default:
                        throw new Exception($"Grid character of ${cell} is unrecognized");
                }

                if (SeenBefore.ContainsKey(beam.ToStringWithMod())) {
                    Beams.Remove(beam);
                    continue;
                }

                SeenBefore.Add(beam.ToStringWithMod(), "");
            }
            
            if (extraBeam != null && !SeenBefore.ContainsKey(extraBeam.ToStringWithMod())) {
                Beams.Add(extraBeam);
                SeenBefore.Add(extraBeam.ToStringWithMod(), "");
            }
                
            Print();

        } while (Beams.Any());
    }

    public void Print()
    {
        var builder = new StringBuilder();
        for(int y =  0; y < Grid.Count; y++)
        {
            for(int x = 0; x < Grid[0].Count; x++)
            {
                if (Beams.Any(b => b.X == x && b.Y == y))
                    builder.Append('*');
                else
                    builder.Append(Grid[y][x].ToString());
            }
            Debug.WriteLine(builder.ToString());
            builder.Clear();
        }

        Debug.WriteLine("");
        Debug.WriteLine("");
    }

    private bool IsOutOfBounds(int x, int y) {
        return x < 0 || x >= Grid[0].Count || y < 0 || y >= Grid.Count;
    }
}

public class Beam {
    public int X { get; set; }

    public int Y { get; set; }

    public int ModX { get; set; }

    public int ModY { get; set; }

    public Beam(int x, int y, int modX, int modY) {
        X = x;
        Y = y;
        ModX = modX;
        ModY = modY;
    }

    public void HitMirror(char c) {
        if (c != '/' && c != '\\')
            throw new Exception($"Called HitMirror with char ${c}");
        
        if (c == '/') {
            if (ModX == 0) {
                ModX = ModY > 0 ? -1 : 1;
                ModY = 0;
                return;
            }
            if (ModY == 0) {
                ModY = ModX > 0 ? -1 : 1;
                ModX = 0;
                return;
            }
        }
        if (c == '\\') {
            if (ModX == 0) {
                ModX = ModY > 0 ? 1 : -1;
                ModY = 0;
                return;
            }
            if (ModY == 0) {
                ModY = ModX > 0 ? 1 : -1;
                ModX = 0;
                return;
            }
        }

        throw new Exception($"Beam ${ToString()} with ModX: {ModX} and ModY: {ModY} is in an invalid state");
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }

    public string ToStringWithMod()
    {
        return $"{X},{Y},{ModX},{ModY}";
    }
}
