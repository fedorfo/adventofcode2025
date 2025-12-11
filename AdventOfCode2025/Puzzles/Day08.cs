using AdventOfCode2025.Helpers;

namespace AdventOfCode2025.Puzzles;

public class Day08 : PuzzleBase
{
    public override void Solve()
    {
        var boxes = ReadBlockLines().Select(x => V3.Parse(x, [","])).ToList();
        var n = boxes.Count;

        var pairs = new List<(int x, int y, long dist)>();
        for (var i = 0; i < n; i++)
        for (var j = i + 1; j < n; j++)
        {
            var d = boxes[i] - boxes[j];
            pairs.Add((i, j, d.X * d.X + d.Y * d.Y + d.Z * d.Z));
        }

        pairs.Sort((a, b) => a.dist.CompareTo(b.dist));

        var color = Enumerable.Range(0, n).ToArray();
        var colors = n;
        var idx = 0;
        var r1 = 0L;
        (int x, int y) last = (0, 0);

        foreach (var (x, y, _) in pairs)
        {
            if (color[x] != color[y])
            {
                var oldColor = color[y];
                for (var j = 0; j < n; j++)
                    if (color[j] == oldColor)
                        color[j] = color[x];
                colors--;
            }

            idx++;

            if (idx == 1000)
            {
                var circuits = color.GroupBy(c => c).Select(g => g.Count()).OrderByDescending(c => c).ToList();
                r1 = (long)circuits[0] * circuits[1] * circuits[2];
            }

            if (colors == 1)
            {
                last = (x, y);
                break;
            }
        }

        Console.WriteLine(r1);
        Console.WriteLine(boxes[last.x].X * boxes[last.y].X);
    }
}