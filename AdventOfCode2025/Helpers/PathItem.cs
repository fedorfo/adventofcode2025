namespace AdventOfCode2025.Helpers;

public record PathItem<TState>(TState Vertex, long Distance, PathItem<TState>? Prev)
{
    public IEnumerable<TState> PathBack()
    {
        for (var c = this; c != null; c = c.Prev) yield return c.Vertex;
    }

    public IEnumerable<TState> Path()
    {
        return PathBack().Reverse();
    }
}