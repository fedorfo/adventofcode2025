using AdventOfCode2025.Helpers;

namespace AdventOfCode2025.Puzzles;

public class Day09 : PuzzleBase
{
    private bool IsRgTile(V2 p, List<(V2 l, V2 r)> horizontalSegments)
    {
        var list = horizontalSegments
            .Where(x => x.l.X <= p.X && x.r.X >= p.X)
            .Select(x => (y: x.l.Y, p: p.X == x.l.X ? PositionOnSegment.Left : p.X == x.r.X ? PositionOnSegment.Right : PositionOnSegment.Inside))
            .OrderBy(x => x.y)
            .Where(x => x.y < p.Y)
            .ToList();
        var state = State.Outside;
        var border = PositionOnSegment.Inside;
        foreach (var item in list)
        {
            if (item.p == PositionOnSegment.Inside)
            {
                state = state == State.Outside ? State.Inside : State.Outside;
                border = PositionOnSegment.Inside;
            }
            else if (item.p == PositionOnSegment.Left)
            {
                if (border == PositionOnSegment.Left)
                {
                    border = PositionOnSegment.Inside;
                }
                else if (border == PositionOnSegment.Right)
                {
                    state = state == State.Outside ? State.Inside : State.Outside;
                    border = PositionOnSegment.Inside;
                }
                else if (border == PositionOnSegment.Inside)
                {
                    border = PositionOnSegment.Left;
                }
            }
            else if (item.p == PositionOnSegment.Right)
            {
                if (border == PositionOnSegment.Right)
                {
                    border = PositionOnSegment.Inside;
                }
                else if (border == PositionOnSegment.Left)
                {
                    state = state == State.Outside ? State.Inside : State.Outside;
                    border = PositionOnSegment.Inside;
                }
                else if (border == PositionOnSegment.Inside)
                {
                    border = PositionOnSegment.Right;
                }
            }
        }

        if (border != PositionOnSegment.Inside)
            return true;
        return state == State.Inside;
    }


    public override void Solve()
    {
        var rTiles = ReadBlockLines().Select(x => V2.Parse(x)).ToList();

        Console.WriteLine(rTiles
            .SelectMany((x, i) => rTiles.Skip(i + 1).Select(y => (x, y)))
            .Select(pair =>
            {
                var d = pair.x - pair.y;
                return (Math.Abs(d.X) + 1) * (Math.Abs(d.Y) + 1);
            }).Max());

        var borderTiles = new HashSet<V2>(rTiles);
        var horizontalSegments = new List<(V2 l, V2 r)>();
        for (var i = 0; i < rTiles.Count; i++)
        {
            var p1 = rTiles[i];
            var p2 = rTiles[(i + 1) % rTiles.Count];
            if (p1.Y == p2.Y) horizontalSegments.Add(p1.X < p2.X ? (l: p1, r: p2) : (l: p2, r: p1));

            var d = p2 - p1;
            d /= d.ManhattanLength();
            for (var x = p1; x != p2; x += d)
                borderTiles.Add(x);
        }

        var borderTilesByX = borderTiles
            .GroupBy(x => x.X)
            .ToDictionary(x => x.Key, x => x.Select(y => y.Y).OrderBy(y => y).ToList());

        var blackTiles = borderTiles
            .SelectMany(x => x.GetNeighbours8())
            .Distinct()
            .Where(x => !borderTiles.Contains(x))
            .Where(x => !IsRgTile(x, horizontalSegments))
            .ToList();

        Console.WriteLine(rTiles
            .SelectMany((x, i) => rTiles.Skip(i + 1).Select(y => (x, y)))
            .Select(pair =>
            {
                var ld = new V2(Math.Min(pair.x.X, pair.y.X), Math.Min(pair.x.Y, pair.y.Y));
                var ru = new V2(Math.Max(pair.x.X, pair.y.X), Math.Max(pair.x.Y, pair.y.Y));
                if (blackTiles.Any(x => x >= ld && x <= ru))
                    return 0;
                var d = pair.x - pair.y;
                return (Math.Abs(d.X) + 1) * (Math.Abs(d.Y) + 1);
            }).Max());
    }

    private enum State
    {
        Outside,
        Inside,
    }

    private enum PositionOnSegment
    {
        Left,
        Inside,
        Right
    }
}