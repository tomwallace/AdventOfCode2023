using System.Numerics;
using AdventOfCode2023.CSharp.Utility;
using Map = System.Collections.Generic.Dictionary<System.Numerics.Complex, char>;
using Node = long;

namespace AdventOfCode2023.CSharp.Day23;

record Edge(Node start, Node end, int distance);

// I could not get Part B to work.  I looked on Reddit for some suggestions and tried pre-compiling
// a collection of nodes that only had two exits to speed up routes, but I could not implement it
// correctly.  So, used the solution from here to solve - https://aoc.csokavar.hu/?day=23
public class NewAttempt {
    static readonly Complex Up = -Complex.ImaginaryOne;
    static readonly Complex Down = Complex.ImaginaryOne;
    static readonly Complex Left = -1;
    static readonly Complex Right = 1;
    static readonly Complex[] Dirs = [Up, Down, Left, Right];

    Dictionary<char, Complex[]> exits = new() {
        ['<'] = [Left],
        ['>'] = [Right],
        ['^'] = [Up],
        ['v'] = [Down],
        ['.'] = Dirs,
        ['#'] = []
    };

    public int PartOne(string filePath) => Solve(filePath, false);
    public int PartTwo(string filePath) => Solve(filePath, true);

    int Solve(string input, bool removeSlopes) {
        var (nodes, edges) = MakeGraph(input, removeSlopes);
        var (start, goal) = (nodes.First(), nodes.Last()); 

        // Dynamic programming using a cache, 'visited' is a bitset of 'nodes'.
        var cache = new Dictionary<(Node, long), int>();
        int LongestPath(Node node, long visited) {
            if (node == goal) {
                return 0;
            } else if ((visited & node) != 0) {
                return int.MinValue; // small enough to represent '-infinity'
            }
            var key = (node, visited);
            if (!cache.ContainsKey(key)) {
                cache[key] = edges
                    .Where(e => e.start == node)
                    .Select(e => e.distance + LongestPath(e.end, visited | node))
                    .Max();
            }
            return cache[key];
        }
        return LongestPath(start, 0); 
    }

    (Node[], Edge[]) MakeGraph(string input, bool removeSlopes) {
        var map = ParseMap(input, removeSlopes);

        // row-major order: 'entry' node comes first and 'exit' is last
        var nodePos = (
            from pos in map.Keys
            orderby pos.Imaginary, pos.Real
            where IsFree(map, pos) && !IsRoad(map, pos)
            select pos
        ).ToArray();

        var nodes = (
            from i in Enumerable.Range(0, nodePos.Length) select 1L << i
        ).ToArray();

        var edges = (
            from i in Enumerable.Range(0, nodePos.Length)
            from j in Enumerable.Range(0, nodePos.Length)
            where i != j
            let distance = Distance(map, nodePos[i], nodePos[j])
            where distance > 0
            select new Edge(nodes[i], nodes[j], distance)
        ).ToArray();

        return (nodes, edges);
    }

    // Length of the road between two crossroads; -1 if not neighbours
    int Distance(Map map, Complex crossroadA, Complex crossroadB) {
        var q = new Queue<(Complex, int)>();
        q.Enqueue((crossroadA, 0));

        var visited = new HashSet<Complex> { crossroadA };
        while (q.Any()) {
            var (pos, dist) = q.Dequeue();
            foreach (var dir in exits[map[pos]]) {
                var posT = pos + dir;
                if (posT == crossroadB) {
                    return dist + 1;
                }  else if (IsRoad(map, posT) && !visited.Contains(posT)) {
                    visited.Add(posT);
                    q.Enqueue((posT, dist + 1));
                }
            }
        }
        return -1;
    }

    bool IsFree(Map map, Complex p) =>
        map.ContainsKey(p) && map[p] != '#';

    bool IsRoad(Map map, Complex p) => 
        IsFree(map, p) && Dirs.Count(d => IsFree(map, p + d)) == 2;

    Map ParseMap(string filePath, bool removeSlopes) {
        var lines = FileUtility.ParseFileToList(filePath, line => {
            if (!removeSlopes)
                return line;
            return string.Join("", line.Select(ch => ">v<^".Contains(ch) ? '.' : ch));
        });
        return (
            from irow in Enumerable.Range(0, lines.Count)
            from icol in Enumerable.Range(0, lines[0].Length)
            let pos = new Complex(icol, irow)
            select new KeyValuePair<Complex, char>(pos, lines[irow][icol])
        ).ToDictionary();
    }
}
