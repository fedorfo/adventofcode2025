using AdventOfCode2025.Helpers;

namespace AdventOfCode2025.Puzzles;

public class Day04 : PuzzleBase
{
    private static List<V2> GetWeak(Map map)
    {
        return map.EnumeratePositions()
            .Where(p => map[p] == '@' && p.GetNeighbours8().Count(n => map.InBounds(n) && map[n] == '@') < 4)
            .ToList();
    }

    public override void Solve()
    {
        var map = new Map(ReadCharMap());
        Console.WriteLine(GetWeak(map).Count);

        var r2 = 0;
        for (var weak = GetWeak(map); weak.Count > 0; weak = GetWeak(map))
        {
            r2 += weak.Count;
            weak.ForEach(p => map[p] = '.');
        }

        Console.WriteLine(r2);
    }
}