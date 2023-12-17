namespace AdventOfCode2023.CSharp.Day16;

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
