using AdventOfCode2025.Helpers;

namespace AdventOfCode2025.Puzzles;

public class Day09 : PuzzleBase
{
    public override void Solve()
    {
        var rTiles = ReadBlockLines().Select(x => V2.Parse(x)).ToList();

        long Area((V2 a, V2 b) p)
        {
            return (Math.Abs(p.a.X - p.b.X) + 1) * (Math.Abs(p.a.Y - p.b.Y) + 1);
        }

        var pairs = rTiles.SelectMany((x, i) => rTiles.Skip(i + 1).Select(y => (x, y))).ToList();

        Console.WriteLine(pairs.Max(Area));

        var hSegs = new List<(V2 l, V2 r)>();
        var border = new HashSet<V2>(rTiles);
        for (var i = 0; i < rTiles.Count; i++)
        {
            var (p1, p2) = (rTiles[i], rTiles[(i + 1) % rTiles.Count]);
            if (p1.Y == p2.Y) hSegs.Add(p1.X < p2.X ? (p1, p2) : (p2, p1));
            var d = (p2 - p1) / (p2 - p1).ManhattanLength();
            for (var x = p1; x != p2; x += d) border.Add(x);
        }

        bool IsInside(V2 p)
        {
            var segs = hSegs.Where(s => s.l.X <= p.X && s.r.X >= p.X && s.l.Y < p.Y)
                .Select(s => (y: s.l.Y, pos: p.X == s.l.X ? -1 : p.X == s.r.X ? 1 : 0))
                .OrderBy(s => s.y);
            int state = 0, edge = 0;
            foreach (var (_, pos) in segs)
                if (pos == 0)
                {
                    state ^= 1;
                }
                else if (edge == 0)
                {
                    edge = pos;
                }
                else
                {
                    if (edge != pos) state ^= 1;
                    edge = 0;
                }

            return edge != 0 || state == 1;
        }

        var black = border.SelectMany(x => x.GetNeighbours8()).Distinct()
            .Where(x => !border.Contains(x) && !IsInside(x)).ToList();

        Console.WriteLine(pairs.Where(p =>
        {
            var (ld, ru) = (new V2(Math.Min(p.x.X, p.y.X), Math.Min(p.x.Y, p.y.Y)),
                new V2(Math.Max(p.x.X, p.y.X), Math.Max(p.x.Y, p.y.Y)));
            return !black.Any(b => b >= ld && b <= ru);
        }).Max(Area));
    }
}