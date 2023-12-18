using System.Diagnostics;
using System.Text;

namespace AdventOfCode2023.CSharp.Day13;

public class Pattern
{
    public List<List<char>> Grid { get; set; }

    public List<string> Rows { get; set; }

    public List<string> Cols { get; set; }

    // Note these are index based on 1
    public int MirrorRow { get; set; }

    public int MirrorCol { get; set; }

    public int SmudgeRow { get; set; }

    public int SmudgeCol { get; set; }

    public Pattern()
    {
        Rows = new List<string>();
        Cols = new List<string>();
        Grid = new List<List<char>>();
        MirrorRow = 0;
        MirrorCol = 0;
        SmudgeRow = 0;
        SmudgeCol = 0;
    }

    public int GetSummarizationScore(bool useSmudges)
    {
        //Print();

        SetRowsAndCols();
        
        FindMirror();
        
        if (!useSmudges)
        {
            var colScore = MirrorCol > 0 ? MirrorCol : 0;
            var rowScore = MirrorRow > 0 ? MirrorRow * 100 : 0;
            return colScore + rowScore;
        }

        FindSmudges();

        var colScoreSmudge = SmudgeCol > 0 ? SmudgeCol : 0;
        var rowScoreSmudge = SmudgeRow > 0 ? SmudgeRow * 100 : 0;
        return colScoreSmudge + rowScoreSmudge;

    }

    public void Print()
    {
        foreach(var row in Grid)
        {
            Debug.WriteLine(new string(row.ToArray()));
        }
        Debug.WriteLine("");
        Debug.WriteLine("");
    }

    public void SetRowsAndCols()
    {
        Rows = Grid.Select(r => new string(r.ToArray())).ToList();
        Cols = new List<string>();
        var builder = new StringBuilder();
        for (int colId = 0; colId < Grid[0].Count; colId++)
        {
            foreach (var row in Grid)
            {
                builder.Append(row[colId]);
            }
            Cols.Add(builder.ToString());
            builder.Clear();
        }
    }

    internal void FindMirror()
    {
        // check rows - can ignore the first and last row, as something needed to reflect
        // But check the first two rows first
        if (Matches(Rows[0], Rows[1], false))
        {
            MirrorRow = 1;
            return;
        }

        for (var r = 1; r < Rows.Count - 1; r++)
        {
            for (var offset = 0; offset < Rows.Count; offset++)
            {
                var down = r + offset + 1;
                var up = r - offset;
                if (down >= Rows.Count || up < 0)
                {
                    MirrorRow = r + 1;
                    return;
                }

                if (!Matches(Rows[up], Rows[down], false))
                    break;
            }
        }

        // check cols
        if (Matches(Cols[0], Cols[1], false))
        {
            MirrorCol = 1;
            return;
        }
        for (var c = 1; c < Cols.Count - 1; c++)
        {
            for (var offset = 0; offset < Cols.Count; offset++)
            {
                var right = c + offset + 1;
                var left = c - offset;
                if (right >= Cols.Count || left < 0)
                {
                    MirrorCol = c + 1;
                    return;
                }

                if (!Matches(Cols[right], Cols[left], false))
                    break;
            }
        }
    }

    internal void FindSmudges()
    {
        if (MirrorRow != 1 && Matches(Rows[0], Rows[1], true))
        {
            SmudgeRow = 1;
            return;
        }

        for (var r = 1; r < Rows.Count - 1; r++)
        {
            if (r + 1 == MirrorRow)
                continue;

            for (var offset = 0; offset < Rows.Count; offset++)
            {
                var down = r + offset + 1;
                var up = r - offset;
                if (down >= Rows.Count || up < 0)
                {
                    SmudgeRow = r + 1;
                    return;
                }

                if (!Matches(Rows[up], Rows[down], true))
                    break;
            }
        }

        // check cols
        if (MirrorCol != 1 && Matches(Cols[0], Cols[1], true))
        {
            SmudgeCol = 1;
            return;
        }
        for (var c = 1; c < Cols.Count - 1; c++)
        {
            if (c + 1 == MirrorCol)
                continue;

            for (var offset = 0; offset < Cols.Count; offset++)
            {
                var right = c + offset + 1;
                var left = c - offset;
                if (right >= Cols.Count || left < 0)
                {
                    SmudgeCol = c + 1;
                    return;
                }

                if (!Matches(Cols[right], Cols[left], true))
                    break;
            }
        }
    }

    private bool Matches(string one, string two, bool useSmudges)
    {
        var matches = Enumerable.Range(0, one.Length)
                    .Count(c => one[c] == two[c]);
        var matchOriginal = matches == one.Length;
        var matchSmudge = matches == one.Length - 1;
        if (matchOriginal)
            return true;

        if (useSmudges && matchSmudge)
            return true;

        return false;
    }
}
