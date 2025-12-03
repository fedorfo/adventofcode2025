using System.Reflection;

namespace AdventOfCode2025.Puzzles;

public static class PuzzleRegistry
{
    private static List<TypeInfo> GetPuzzleTypeInfos()
    {
        return typeof(Program).Assembly.DefinedTypes
            .Where(x => x.ImplementedInterfaces.Contains(typeof(IPuzzle)) && !x.IsAbstract
            ).ToList();
    }

    public static List<IPuzzle> GetPuzzles()
    {
        return GetPuzzleTypeInfos()
            .Select(puzzleTypeInfo => puzzleTypeInfo.GetConstructor([]))
            .Select(constructor => (IPuzzle)constructor!.Invoke([]))
            .ToList();
    }
}