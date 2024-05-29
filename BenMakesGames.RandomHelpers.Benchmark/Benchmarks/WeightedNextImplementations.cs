using System.Diagnostics;
using BenchmarkDotNet.Attributes;

namespace BenMakesGames.RandomHelpers.Benchmark.Benchmarks;

/*
    BenchmarkDotNet v0.13.10, Windows 10 (10.0.19045.4412/22H2/2022Update)
    Intel Core i7-4790 CPU 3.60GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
    .NET SDK 8.0.300
      [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2
      DefaultJob : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX2


    | Method                | Mean     | Error   | StdDev  | Allocated |
    |---------------------- |---------:|--------:|--------:|----------:|
    | CurrentImplementation | 107.6 ns | 1.04 ns | 0.92 ns |         - |
    | WithForEach           | 112.8 ns | 1.00 ns | 0.89 ns |      32 B |
    | WithSum               | 103.6 ns | 0.72 ns | 0.60 ns |      32 B |
 */
[MemoryDiagnoser(false)]
public class WeightedNextImplementations
{
    private static readonly Random Random = new Random(1000);
    private static readonly (string Name, int Weight)[] List = [ ("A", 1), ("B", 2), ("C", 3), ("D", 4), ("E", 5) ];
    private static readonly Func<(string Name, int Weight), int> WeightSelector = i => i.Weight;

    [Benchmark]
    public (string Name, int Weight) CurrentImplementation()
    {
        return WeightedNext_CurrentImplementation(Random, List, WeightSelector);
    }

    [Benchmark]
    public (string Name, int Weight) WithForEach()
    {
        return WeightedNext_WithForEach(Random, List, WeightSelector);
    }

    [Benchmark]
    public (string Name, int Weight) WithSum()
    {
        return WeightedNext_WithSum(Random, List, WeightSelector);
    }

    public T WeightedNext_CurrentImplementation<T>(Random rng, IReadOnlyList<T> list, Func<T, int> weightSelector)
    {
        var totalWeight = 0;

        for (var i = 0; i < list.Count; i++)
        {
            var weight = weightSelector(list[i]);

            if(weight <= 0)
                throw new ArgumentException("All weights must be greater than 0.", nameof(weightSelector));

            totalWeight += weight;
        }

        var value = rng.Next(totalWeight);

        for (var i = 0; i < list.Count; i++)
        {
            var item = list[i];

            value -= weightSelector(item);

            if (value <= 0)
                return item;
        }

        throw new UnreachableException("This should never happen. (Is `weightSelector` not a pure method? It should be!)");
    }

    public T WeightedNext_WithForEach<T>(Random rng, IReadOnlyList<T> list, Func<T, int> weightSelector)
    {
        var totalWeight = 0;

        for (var i = 0; i < list.Count; i++)
        {
            var weight = weightSelector(list[i]);

            if(weight <= 0)
                throw new ArgumentException("All weights must be greater than 0.", nameof(weightSelector));

            totalWeight += weight;
        }

        var value = rng.Next(totalWeight);

        foreach (var item in list)
        {
            value -= weightSelector(item);

            if (value <= 0)
                return item;
        }

        throw new UnreachableException("This should never happen. (Is `weightSelector` not a pure method? It should be!)");
    }

    public T WeightedNext_WithSum<T>(Random rng, IReadOnlyList<T> list, Func<T, int> weightSelector)
    {
        var totalWeight = list.Sum(weightSelector);

        var value = rng.Next(totalWeight);

        for (var i = 0; i < list.Count; i++)
        {
            var item = list[i];
            var weight = weightSelector(item);

            if(weight <= 0)
                throw new ArgumentException("All weights must be greater than 0.", nameof(weightSelector));

            value -= weight;

            if (value <= 0)
                return item;
        }

        throw new UnreachableException("This should never happen. (Is `weightSelector` not a pure method? It should be!)");
    }

}
