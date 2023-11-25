using BenchmarkDotNet.Attributes;

namespace BenMakesGames.RandomHelpers.Benchmark.Benchmarks;

/*
    BenchmarkDotNet v0.13.10, Windows 10 (10.0.19045.3570/22H2/2022Update)
    Intel Core i5-10210U CPU 1.60GHz, 1 CPU, 8 logical and 4 physical cores
    .NET SDK 8.0.100
      [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
      DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


    | Method                  | Mean     | Error    | StdDev   | Allocated |
    |------------------------ |---------:|---------:|---------:|----------:|
    | DotNet_NextSingle       | 11.35 ns | 0.256 ns | 0.274 ns |         - |
    | RandomHelpers_NextFloat | 11.14 ns | 0.235 ns | 0.208 ns |         - |
 */
[MemoryDiagnoser(false)]
public class NextFloatAlias
{
    private Random Random { get; set; } = null!;

    [GlobalSetup]
    public void Setup()
    {
        Random = new Random(1000);
    }

    [Benchmark]
    public float DotNet_NextSingle()
    {
        return Random.NextSingle();
    }

    [Benchmark]
    public float RandomHelpers_NextFloat()
    {
        return Random.NextFloat();
    }
}
