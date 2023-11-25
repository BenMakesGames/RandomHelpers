using BenchmarkDotNet.Attributes;

namespace BenMakesGames.RandomHelpers.Benchmark.Benchmarks;

/*
    BenchmarkDotNet v0.13.10, Windows 10 (10.0.19045.3570/22H2/2022Update)
    Intel Core i5-10210U CPU 1.60GHz, 1 CPU, 8 logical and 4 physical cores
    .NET SDK 8.0.100
      [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
      DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


    | Method                           | Mean     | Error     | StdDev    | Allocated |
    |--------------------------------- |---------:|----------:|----------:|----------:|
    | DotNet_RandomShuffleArray        | 1.062 us | 0.0213 us | 0.0262 us |         - |
    | RandomHelpers_ArrayShuffleRandom | 1.086 us | 0.0214 us | 0.0262 us |         - |
 */
[MemoryDiagnoser(false)]
public class ArrayShuffleAlias
{
    private Random Random { get; set; } = null!;
    private int[] Array { get; set; } = null!;

    [GlobalSetup]
    public void Setup()
    {
        Random = new Random(1000);
        Array = new int[100];

        for (var i = 0; i < 100; i++)
            Array[i] = i;
    }

    [Benchmark]
    public void DotNet_RandomShuffleArray()
    {
        Random.Shuffle(Array);
    }

    [Benchmark]
    public void RandomHelpers_ArrayShuffleRandom()
    {
        Array.Shuffle(Random);
    }
}
