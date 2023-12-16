namespace AdventOfCode2023.CSharp.Day15;

public class Box {
    public List<Lens> Lenses { get; set; }

    public Box() {
        Lenses = new List<Lens>();
    }

    public void Add(string id, int focalLength) {
        var lens = Lenses.FirstOrDefault(l => l.Id == id);
        if (lens == null) {
            Lenses.Add(new Lens(id, focalLength));
            return;
        }

        lens.FocalLength = focalLength;
    }

    public void Remove(string id) {
        var lens = Lenses.FirstOrDefault(l => l.Id == id);
        if (lens != null) {
            Lenses.Remove(lens);
            return;
        }
    }

    public int GetFocusingPower(int id) {
        var focusingPower = 0;
        for (var i = 1; i <= Lenses.Count; i++) {
            focusingPower += (id * i * Lenses[i - 1].FocalLength);
        }
        return focusingPower;
    }

    public override string ToString()
    {
        return string.Join(',', Lenses);
    }
}
