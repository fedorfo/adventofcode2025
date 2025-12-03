namespace AdventOfCode2025.Puzzles;

public class Day02 : PuzzleBase
{
    public override void Solve()
    {
        var intervals = ReadBlockLines().Single().Split(",")
            .Select(x => x.Split("-"))
            .Select(a => (Start: long.Parse(a[0]), End: long.Parse(a[1])))
            .ToList();
        var max = intervals.Max(x => x.End);

        bool InRange(long x)
        {
            return intervals.Any(i => x >= i.Start && x <= i.End);
        }

        long? RepeatId(long p, int r)
        {
            var str = string.Concat(Enumerable.Repeat(p.ToString(), r));
            if (str.Length > max.ToString().Length) return null;
            var v = long.Parse(str);
            return v < max ? v : null;
        }

        long result1 = 0;
        for (long i = 1; RepeatId(i, 2) is { } id; i++)
            if (InRange(id))
                result1 += id;

        var seen = new HashSet<long>();
        long result2 = 0;
        for (long i = 1; RepeatId(i, 2) != null; i++)
        for (var j = 2; RepeatId(i, j) is { } id; j++)
            if (seen.Add(id) && InRange(id))
                result2 += id;

        Console.WriteLine($"{result1}\n{result2}");
    }
}