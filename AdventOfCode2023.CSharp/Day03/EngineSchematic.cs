using System.Text;
using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day03;

public class EngineSchematic {
    public Dictionary<string, char> Symbols {get; set;}

    public List<int> PartNumbers {get; set;}

    public List<List<char>> Grid {get;}

    public Dictionary<string, List<int>> PotentialGears {get; set;}

    public EngineSchematic(string filePath) {
        Symbols = new Dictionary<string, char>();
        PartNumbers = new List<int>();
        PotentialGears = new Dictionary<string, List<int>>();

        // Pass through the grid first to create dictionary of Symbols
        Grid = FileUtility.ParseFileToList<List<char>>(filePath, line => line.ToCharArray().ToList());
        for (int y = 0; y < Grid.Count; y++) {
            for (int x = 0; x < Grid[0].Count; x++) {
                // Check if not a digit or a . - then must be a symbol
                if (!char.IsNumber(Grid[y][x]) && Grid[y][x] != '.')
                    Symbols.Add($"{x},{y}", Grid[y][x]);
                
                if (Grid[y][x] == '*')
                    PotentialGears.Add($"{x},{y}", new List<int>());
            }
        }

        // Now pass through grid and find numbers, which are horizontal and bound by edges or .
        var numStringBuilder = new StringBuilder();
        for (int y = 0; y < Grid.Count; y++) {
            for (int x = 0; x < Grid[0].Count; x++) {
                var current = Grid[y][x];

                if (char.IsNumber(current)) {
                    numStringBuilder.Append(current);
                }
                
                if (!char.IsNumber(current)) {
                    // We may have completed a number
                    if (numStringBuilder.Length > 0) {
                        var number = int.Parse(numStringBuilder.ToString());
                        // Is it a part number
                        if (IsNumberNearSymbol(y, x - numStringBuilder.Length, x - 1)) {
                            PartNumbers.Add(number);

                            HandlePotentialGearRecording(y, x - numStringBuilder.Length, x - 1, number);
                        }
                        
                        // Reset the string builder
                        numStringBuilder.Clear();
                    }
                } else if (x == Grid[0].Count - 1) {
                    // We may have completed a number by ending the row
                    if (numStringBuilder.Length > 0) {
                        var number = int.Parse(numStringBuilder.ToString());
                        // Is it a part number
                        if (IsNumberNearSymbol(y, x - numStringBuilder.Length + 1, x)) {
                            PartNumbers.Add(number);

                            HandlePotentialGearRecording(y, x - numStringBuilder.Length + 1, x, number);
                        }
                        
                        // Reset the string builder
                        numStringBuilder.Clear();
                    }
                }
            }
            numStringBuilder.Clear();
        }
    }

    private bool IsNumberNearSymbol(int currY, int minX, int maxX) {
        for (int y = currY - 1; y <= currY + 1; y++) {
            for (int x = minX - 1; x <= maxX + 1; x++) {
                if (Symbols.ContainsKey($"{x},{y}"))
                    return true;
            }
        }

        return false;
    }

    private void HandlePotentialGearRecording(int currY, int minX, int maxX, int number) {
        for (int y = currY - 1; y <= currY + 1; y++) {
            for (int x = minX - 1; x <= maxX + 1; x++) {
                if (PotentialGears.ContainsKey($"{x},{y}"))
                    PotentialGears[$"{x},{y}"].Add(number);
            }
        }
    }

    public int SumPartNumbers() {
        return PartNumbers.Sum();
    }

    public int SumGearRatios() {
        // Gears only touch 2 part numbers
        var sum = PotentialGears.Where(pg => pg.Value.Count == 2).Sum(pg => pg.Value[0] * pg.Value[1]);
        return sum;
    }
}