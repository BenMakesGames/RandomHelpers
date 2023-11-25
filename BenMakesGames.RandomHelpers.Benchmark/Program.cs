using System.Reflection;
using BenchmarkDotNet.Running;

var allBenchmarks = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => t is { Namespace: "BenMakesGames.RandomHelpers.Benchmark.Benchmarks", IsClass: true })
    .ToList();

if (SelectBenchmark(allBenchmarks) is { } benchmark)
{
    Console.WriteLine();
    BenchmarkRunner.Run(benchmark);
}

return;

Type? SelectBenchmark(List<Type> benchmarks)
{
    var selected = 0;

    Console.CursorVisible = false;

    string Label(int i) => i < benchmarks.Count ? benchmarks[i].Name : "Cancel";

    while (true)
    {
        Console.Clear();

        Console.WriteLine("Select a benchmark to run");

        for (var i = 0; i < benchmarks.Count + 1; i++)
        {
            if (i == selected)
            {
                var oldForegroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"> {Label(i)}");
                Console.ForegroundColor = oldForegroundColor;
            }
            else
                Console.WriteLine($"  {Label(i)}");
        }

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.UpArrow:
            case ConsoleKey.NumPad8:
                selected = selected > 0 ? selected - 1 : benchmarks.Count;
                break;

            case ConsoleKey.DownArrow:
            case ConsoleKey.NumPad2:
                selected = (selected + 1) % (benchmarks.Count + 1);
                break;

            case ConsoleKey.Enter:
                Console.CursorVisible = true;
                return selected < benchmarks.Count ? benchmarks[selected] : null;
        }
    }
}
