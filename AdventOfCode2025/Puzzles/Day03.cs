namespace AdventOfCode2025.Puzzles;

public class Day03 : PuzzleBase
{
    private long MaxJoltage(string bank, int len)
    {
        var dp = new long[bank.Length + 1, len + 1];
        for (var i = 1; i <= bank.Length; i++)
        {
            var d = bank[i - 1] - '0';
            for (var j = 1; j <= len; j++)
            for (var k = 0; k < i; k++)
                dp[i, j] = Math.Max(dp[i, j], dp[k, j - 1] * 10 + d);
        }

        return Enumerable.Range(0, bank.Length + 1).Max(i => dp[i, len]);
    }

    public override void Solve()
    {
        var banks = ReadBlockLines().ToList();
        Console.WriteLine(banks.Sum(b => MaxJoltage(b, 2)));
        Console.WriteLine(banks.Sum(b => MaxJoltage(b, 12)));
    }
}