using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day22;

public class Column {
    public List<Brick> Bricks { get; set; }

    public Column(string filePath) {
        Bricks = new List<Brick>();
        var lines = FileUtility.ParseFileToList(filePath);
        for (int i = 0; i < lines.Count; i++) {
            Bricks.Add(new Brick(lines[i], i));
        }

        // Ensure lowest brick is first
        Bricks = Bricks.OrderBy(b => b.Points[0].Z).ToList();
    }

    public int SettleAndCountBricksNotSettled(bool allowMove) {
        var settledIds = new HashSet<int>();
        var count = 0;

        foreach(var brick in Bricks) {
            // It is on the ground or already settled
            if (brick.Points[0].Z == 1 || settledIds.Contains(brick.Id))
                continue;

            var isResting = IsResting(brick);
            if (!allowMove && !isResting)
                return 1;

            if (!isResting) {
                count++;

                // Move the brick's points down one Z at a time until it is resting
                while (!isResting && brick.Points[0].Z > 1) {
                    for (var i = 0; i < brick.Points.Count; i ++) {
                        brick.Points[i] = (brick.Points[i].X, brick.Points[i].Y, brick.Points[i].Z - 1);
                    }
                    isResting = IsResting(brick);
                }

                settledIds.Add(brick.Id);
            }
        }

        return count;
    }

    public int CountSupportingBricks()
    {
        var count = 0;

        // Remove each block in turn and see what makes things unstable
        foreach (var brick in Bricks.ToList())
        {
            Bricks.Remove(brick);

            count += SettleAndCountBricksNotSettled(false) > 0 ? 1 : 0;

            Bricks.Add(brick);
        }
        
        return count;
    }

    public List<int> GetSettledBrickIds() {
        var brickIds = new List<int>();

        foreach (var brick in Bricks.ToList())
        {
            Bricks.Remove(brick);

            if (SettleAndCountBricksNotSettled(false) > 0)
            {
                brickIds.Add(brick.Id);                    
            }

            Bricks.Add(brick);
        }
        
        return brickIds;
    }

    public bool IsResting(Brick current) {
        foreach(var brick in Bricks) {
            // Don't evaluate self
            if (brick.Id == current.Id)
                continue;

            // If the brick is above current, or significantly under current one - short circuit
            if (brick.Points[0].Z >= current.Points[0].Z || brick.Points[0].Z < current.Points[0].Z - 5)
                continue;

            // Any part atop the brick
            foreach(var cp in current.Points) {
                foreach (var bp in brick.Points) {
                    if (cp.Z == bp.Z + 1 && cp.X == bp.X && cp.Y == bp.Y)
                        return true;
                }
            }
        }

        return false;
    }

    public int SumFallingBricks() {
        var settledBrickIds = GetSettledBrickIds();
        var sum = 0;

        var settledPoints = new List<(int Id, List<(int X, int Y, int Z)> Points)>();

        foreach (var brick in Bricks)
        {
            settledPoints.Add((brick.Id, brick.Points.Select(b => (b.X, b.Y, b.Z)).ToList()));
        }

        foreach (var brickId in settledBrickIds)
        {
            Bricks.RemoveAll(b => b.Id == brickId);

            sum += SettleAndCountBricksNotSettled(true);
            
            Bricks.Clear();

            foreach (var item in settledPoints)
            {
                Bricks.Add(new Brick(item.Id, item.Points.Select(b => (b.X, b.Y, b.Z)).ToList()));
            }
        }
        
        return sum;
    }
}
