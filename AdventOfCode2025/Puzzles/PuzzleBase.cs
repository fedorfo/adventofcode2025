using System.Globalization;
using AdventOfCode2025.Helpers;

namespace AdventOfCode2025.Puzzles;

public abstract class PuzzleBase : IPuzzle
{
    public int Day => int.Parse(GetType().Name.Replace("Day", ""), CultureInfo.InvariantCulture);

    public virtual string InputFileName => $"{Day:00}.in";
    public abstract void Solve();

    protected static IEnumerable<string> ReadBlockLines()
    {
        while (true)
        {
            var line = Console.ReadLine();
            if (string.IsNullOrEmpty(line)) yield break;

            yield return line;
        }
    }

    protected static string ReadBlockText()
    {
        return string.Join("\n", ReadBlockLines());
    }

    protected static char[][] ReadCharMap()
    {
        return ReadBlockLines().Select(x => x.ToArray()).ToArray();
    }

    protected static Map ReadMap()
    {
        return new Map(ReadCharMap());
    }
}