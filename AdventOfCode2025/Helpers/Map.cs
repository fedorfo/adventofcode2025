using MoreLinq.Extensions;

namespace AdventOfCode2025.Helpers;

public record Map
{
    public Map(char[][] Map)
    {
        map = Map;
    }

    public Map(string[] rows)
    {
        map = rows.Select(x => x.ToCharArray()).ToArray();
    }

    public Map(int h, int w, char fill = '.')
    {
        map = Enumerable.Range(0, h).Select(_ => Enumerable.Repeat(fill, w).ToArray()).ToArray();
    }

    public char this[V2 p]
    {
        get => map[p.X][p.Y];
        set => map[p.X][p.Y] = value;
    }

    public V2 Size => new(map.Length, map.Length > 0 ? map[0].Length : 0);
    public char[][] map { get; init; }

    public char[][] GetMap()
    {
        return map;
    }

    public bool InBounds(V2 p)
    {
        return p >= V2.Zero && p < Size;
    }

    public IEnumerable<V2> EnumeratePositions()
    {
        return V2.EnumerateRange(V2.Zero, Size);
    }

    public void Print()
    {
        map.ForEach(row => Console.WriteLine(new string(row)));
    }

    public Map Copy()
    {
        return new Map(map.Select(x => x.ToArray()).ToArray());
    }

    public void Deconstruct(out char[][] map)
    {
        map = this.map;
    }
}