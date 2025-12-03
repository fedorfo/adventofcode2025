namespace AdventOfCode2025.Puzzles;

public interface IPuzzle
{
    int Day { get; }
    string InputFileName { get; }
    void Solve();
}