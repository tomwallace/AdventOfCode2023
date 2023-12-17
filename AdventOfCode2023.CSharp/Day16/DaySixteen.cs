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
        var count = BestBeamLocationCountEnergizedCells(filePath);

        return count.ToString();
    }

    internal int CountEnergizedCells(string filePath) {
        var contraption = new Contraption(filePath);
        contraption.Activate(new Beam(-1, 0, 1, 0));
        return contraption.EnergizedPoints.Count;
    }

    internal int BestBeamLocationCountEnergizedCells(string filePath) {
        var lines = FileUtility.ParseFileToList(filePath);
        var best = 0;
        for (var x = 0; x < lines[0].Length; x++) {
            // North
            var northContraption = new Contraption(filePath);
            northContraption.Activate(new Beam(x, -1, 0, 1));
            best = best > northContraption.EnergizedPoints.Count ? best : northContraption.EnergizedPoints.Count;

            // South
            var southContraption = new Contraption(filePath);
            southContraption.Activate(new Beam(x, lines.Count - 1, 0, -1));
            best = best > southContraption.EnergizedPoints.Count ? best : southContraption.EnergizedPoints.Count;
        }
        for (var y = 0; y < lines.Count; y++) {
            // East
            var eastContraption = new Contraption(filePath);
            eastContraption.Activate(new Beam(-1, y, 1, 0));
            best = best > eastContraption.EnergizedPoints.Count ? best : eastContraption.EnergizedPoints.Count;

            // West
            var westContraption = new Contraption(filePath);
            westContraption.Activate(new Beam(lines[0].Length - 1, y, -1, 0));
            best = best > westContraption.EnergizedPoints.Count ? best : westContraption.EnergizedPoints.Count;
        }

        return best;
    }
    
}