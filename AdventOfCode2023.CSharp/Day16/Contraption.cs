using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day16;

public class Contraption {
    public List<List<char>> Grid { get; }

    public HashSet<string> EnergizedPoints { get; set; }

    public HashSet<string> SeenBefore {get; set; }

    public Contraption(string filePath) {
        Grid = FileUtility.ParseFileToList<List<char>>(filePath, line => line.ToCharArray().ToList());
        EnergizedPoints = new HashSet<string>();
        SeenBefore = new HashSet<string>();
    }

    public void Activate(Beam start) 
    {
        var beams = new Stack<Beam>();
        beams.Push(start);
        while (beams.Any()) 
        {
            var beam = beams.Pop();

            if (!SeenBefore.Add(beam.ToStringWithMod()))
                continue;

            var nextX = beam.X + beam.ModX;
            var nextY = beam.Y + beam.ModY;

            if (IsOutOfBounds(nextX, nextY)) {
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
                        var extraBeam = new Beam(nextX, nextY, -1, 0);
                        beam.ModX = 1;
                        beam.ModY = 0;
                        beams.Push(extraBeam);
                    }
                    break;
                case '|':
                    if (beam.ModX != 0) {
                        var extraBeam = new Beam(nextX, nextY, 0, -1);
                        beam.ModX = 0;
                        beam.ModY = 1;
                        beams.Push(extraBeam);
                    }
                    break;
                default:
                    throw new Exception($"Grid character of ${cell} is unrecognized");
            }

            beams.Push(beam);
        }
    }

    private bool IsOutOfBounds(int x, int y) {
        return x < 0 || x >= Grid[0].Count || y < 0 || y >= Grid.Count;
    }
}
