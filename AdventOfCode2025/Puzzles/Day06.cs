using AdventOfCode2025.Helpers;

namespace AdventOfCode2025.Puzzles;

public class Day06 : PuzzleBase
{
    private static long Produce(char op, List<long> nums)
    {
        return op == '*' ? nums.Aggregate(1L, (a, b) => a * b) : nums.Sum();
    }

    public override void Solve()
    {
        var lines = ReadBlockLines().ToList();
        var ops = lines[^1];
        var nums = lines[..^1];

        var tokens = ops.ExtractTokens().ToList();
        var parsed = nums.Select(x => x.ExtractTokens().Select(long.Parse).ToList()).ToList();
        var r1 = Enumerable.Range(0, parsed[0].Count)
            .Sum(i => Produce(tokens[i][0], parsed.Select(row => row[i]).ToList()));
        Console.WriteLine(r1);

        long r2 = 0;
        for (var i = 0; i < ops.Length;)
        {
            var op = ops[i];
            var col = new List<long>();
            var start = i;
            for (; i < ops.Length && (i == start || ops[i] == ' '); i++)
            {
                var s = string.Concat(nums.Select(row => row[i])).Trim();
                if (s.Length > 0) col.Add(long.Parse(s));
            }

            r2 += Produce(op, col);
        }

        Console.WriteLine(r2);
    }
}