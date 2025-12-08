namespace AdventOfCode2025.Puzzles;

public class Day05 : PuzzleBase
{
    public override void Solve()
    {
        var ranges = ReadBlockLines()
            .Select(x => x.Split("-"))
            .Select(a => (L: long.Parse(a[0]), R: long.Parse(a[1])))
            .OrderBy(x => x.L)
            .ToList();
        var ingredients = ReadBlockLines().Select(long.Parse).ToList();

        var merged = new List<(long L, long R)> { ranges[0] };
        foreach (var r in ranges.Skip(1))
            if (r.L <= merged[^1].R)
                merged[^1] = (merged[^1].L, Math.Max(merged[^1].R, r.R));
            else
                merged.Add(r);

        Console.WriteLine(ingredients.Count(x => merged.Any(m => x >= m.L && x <= m.R)));
        Console.WriteLine(merged.Sum(x => x.R - x.L + 1));
    }
}