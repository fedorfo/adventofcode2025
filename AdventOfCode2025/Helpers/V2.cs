using static System.Math;

namespace AdventOfCode2025.Helpers;

public record V2(long X, long Y)
{
    public static readonly V2 Zero = new(0, 0);

    public static IEnumerable<V2> EnumerateRange(V2 start, V2 end)
    {
        for (var i = start.X; i != end.X; i += start.X < end.X ? 1 : -1)
        for (var j = start.Y; j != end.Y; j += start.Y < end.Y ? 1 : -1)
            yield return new V2(i, j);
    }

    public static V2 operator +(V2 l, V2 r)
    {
        return new V2(l.X + r.X, l.Y + r.Y);
    }

    public static V2 operator -(V2 l, V2 r)
    {
        return new V2(l.X - r.X, l.Y - r.Y);
    }

    public static V2 operator /(V2 p, long l)
    {
        return new V2(p.X / l, p.Y / l);
    }

    public static V2 operator *(V2 p, long l)
    {
        return new V2(p.X * l, p.Y * l);
    }

    public static bool operator <(V2 l, V2 r)
    {
        return l.X < r.X && l.Y < r.Y;
    }

    public static bool operator <=(V2 l, V2 r)
    {
        return l.X <= r.X && l.Y <= r.Y;
    }

    public static bool operator >=(V2 l, V2 r)
    {
        return r <= l;
    }

    public static bool operator >(V2 l, V2 r)
    {
        return r < l;
    }

    public long ManhattanLength()
    {
        return Abs(X) + Abs(Y);
    }

    public long ChebyshevLength()
    {
        return Max(Abs(X), Abs(Y));
    }

    public IEnumerable<V2> GetNeighbours8()
    {
        for (var x = -1; x <= 1; x++)
        for (var y = -1; y <= 1; y++)
        {
            var candidate = new V2(x, y);
            if (candidate.ChebyshevLength() == 1) yield return this + candidate;
        }
    }

    public IEnumerable<V2> GetNeighbours4()
    {
        for (var x = -1; x <= 1; x++)
        for (var y = -1; y <= 1; y++)
        {
            var candidate = new V2(x, y);
            if (candidate.ManhattanLength() == 1) yield return this + candidate;
        }
    }

    public V2 CCW()
    {
        return new V2(-Y, X);
    }

    public V2 CW()
    {
        return new V2(Y, -X);
    }

    public override string ToString()
    {
        return $"({X},{Y})";
    }

    public static V2 Parse(string value, params string[] separators)
    {
        var args = value.ExtractTokens(separators).Select(long.Parse).ToList();
        return new V2(args[0], args[1]);
    }
}