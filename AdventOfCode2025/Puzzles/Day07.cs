namespace AdventOfCode2025.Puzzles;

public class Day07 : PuzzleBase
{
    public override void Solve()
    {
        var map = ReadCharMap();
        int h = map.Length, w = map[0].Length;
        var dp = new long[h, w];
        var r1 = 0;

        for (var j = 0; j < w; j++)
            if (map[0][j] == 'S')
            {
                map[0][j] = '|';
                dp[0, j] = 1;
            }

        for (var i = 1; i < h; i++)
        for (var j = 0; j < w; j++)
        {
            if (map[i - 1][j] != '|') continue;
            if (map[i][j] == '^')
            {
                map[i][j - 1] = map[i][j + 1] = '|';
                dp[i, j - 1] += dp[i - 1, j];
                dp[i, j + 1] += dp[i - 1, j];
                r1++;
            }
            else
            {
                map[i][j] = '|';
                dp[i, j] += dp[i - 1, j];
            }
        }

        Console.WriteLine(r1);
        Console.WriteLine(Enumerable.Range(0, w).Sum(j => dp[h - 1, j]));
    }
}