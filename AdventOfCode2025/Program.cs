using System.Diagnostics;
using AdventOfCode2025.Puzzles;
using AdventOfCode2025.Resources;

namespace AdventOfCode2025;

public class Program
{
    public static void Main()
    {
        var puzzles = PuzzleRegistry.GetPuzzles().ToDictionary(puzzle => puzzle.Day);
        var day = Helpers.Helpers.Max(puzzles.Keys.ToArray());
        Console.WriteLine("Day " + day);
        using var inputStream = new StreamReader(ResourceRegistry.GetResourceStream(puzzles[day].InputFileName));
        Console.SetIn(inputStream);
        var sw = Stopwatch.StartNew();
        puzzles[day].Solve();
        Console.WriteLine("Elapsed: " + sw.Elapsed);
    }
}