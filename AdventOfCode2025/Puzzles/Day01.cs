namespace AdventOfCode2025.Puzzles;

public class Day01 : PuzzleBase
{
    public override void Solve()
    {
        int position = 50, result1 = 0, result2 = 0;
        foreach (var t in ReadBlockLines())
        {
            position += (t[0] == 'L' ? -1 : 1) * int.Parse(t[1..]);
            while (position < 0)
            {
                position += 100;
                result2++;
            }

            while (position >= 100)
            {
                position -= 100;
                result2++;
            }

            if (position == 0) result1++;
        }

        Console.WriteLine($"{result1}\n{result2}");
    }
}