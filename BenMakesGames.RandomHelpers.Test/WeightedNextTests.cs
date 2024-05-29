using System.Collections.Frozen;
using FluentAssertions;
using Xunit;

namespace BenMakesGames.RandomHelpers.Test;

public sealed class WeightedNextTests
{
    [Fact]
    public void WeightedNext_HasGoodDistribution_WithIReadOnlyListAndIntWeight()
    {
        var items = new (string Name, int Weight)[]
        {
            ("A", 1),
            ("B", 2),
            ("C", 3),
            ("D", 4),
        };

        var results = new Dictionary<string, int>
        {
            { "A", 0 },
            { "B", 0 },
            { "C", 0 },
            { "D", 0 },
        };

        var rng = new Random(314159);

        for (var i = 0; i < 10_000; i++)
        {
            var pick = rng.WeightedNext(items, item => item.Weight);
            results[pick.Name]++;
        }

        results["A"].Should().BeInRange((int)(10_000 * 1 / 10.0 * 0.95), (int)(10_000 * 1 / 10.0 * 1.05));
        results["B"].Should().BeInRange((int)(10_000 * 2 / 10.0 * 0.95), (int)(10_000 * 2 / 10.0 * 1.05));
        results["C"].Should().BeInRange((int)(10_000 * 3 / 10.0 * 0.95), (int)(10_000 * 3 / 10.0 * 1.05));
        results["D"].Should().BeInRange((int)(10_000 * 4 / 10.0 * 0.95), (int)(10_000 * 4 / 10.0 * 1.05));
    }

    [Fact]
    public void WeightedNext_HasGoodDistribution_WithIReadOnlyListAndLongWeight()
    {
        var items = new (string Name, long Weight)[]
        {
            ("A", 1),
            ("B", 2),
            ("C", 3),
            ("D", 4),
        };

        var results = new Dictionary<string, int>
        {
            { "A", 0 },
            { "B", 0 },
            { "C", 0 },
            { "D", 0 },
        };

        var rng = new Random(314159);

        for (var i = 0; i < 10_000; i++)
        {
            var pick = rng.WeightedNext(items, item => item.Weight);
            results[pick.Name]++;
        }

        results["A"].Should().BeInRange((int)(10_000 * 1 / 10.0 * 0.95), (int)(10_000 * 1 / 10.0 * 1.05));
        results["B"].Should().BeInRange((int)(10_000 * 2 / 10.0 * 0.95), (int)(10_000 * 2 / 10.0 * 1.05));
        results["C"].Should().BeInRange((int)(10_000 * 3 / 10.0 * 0.95), (int)(10_000 * 3 / 10.0 * 1.05));
        results["D"].Should().BeInRange((int)(10_000 * 4 / 10.0 * 0.95), (int)(10_000 * 4 / 10.0 * 1.05));
    }

    [Fact]
    public void WeightedNext_HasGoodDistribution_WithIReadOnlySetAndIntWeight()
    {
        var items = new List<(string Name, int Weight)>()
        {
            ("A", 1),
            ("B", 2),
            ("C", 3),
            ("D", 4),
        }.ToFrozenSet();

        var results = new Dictionary<string, int>
        {
            { "A", 0 },
            { "B", 0 },
            { "C", 0 },
            { "D", 0 },
        };

        var rng = new Random(314159);

        for (var i = 0; i < 10_000; i++)
        {
            var pick = rng.WeightedNext(items, item => item.Weight);
            results[pick.Name]++;
        }

        results["A"].Should().BeInRange((int)(10_000 * 1 / 10.0 * 0.95), (int)(10_000 * 1 / 10.0 * 1.05));
        results["B"].Should().BeInRange((int)(10_000 * 2 / 10.0 * 0.95), (int)(10_000 * 2 / 10.0 * 1.05));
        results["C"].Should().BeInRange((int)(10_000 * 3 / 10.0 * 0.95), (int)(10_000 * 3 / 10.0 * 1.05));
        results["D"].Should().BeInRange((int)(10_000 * 4 / 10.0 * 0.95), (int)(10_000 * 4 / 10.0 * 1.05));
    }

    [Fact]
    public void WeightedNext_HasGoodDistribution_WithIReadOnlySetAndLongWeight()
    {
        var items = new List<(string Name, long Weight)>()
        {
            ("A", 1),
            ("B", 2),
            ("C", 3),
            ("D", 4),
        }.ToFrozenSet();

        var results = new Dictionary<string, int>
        {
            { "A", 0 },
            { "B", 0 },
            { "C", 0 },
            { "D", 0 },
        };

        var rng = new Random(314159);

        for (var i = 0; i < 10_000; i++)
        {
            var pick = rng.WeightedNext(items, item => item.Weight);
            results[pick.Name]++;
        }

        results["A"].Should().BeInRange((int)(10_000 * 1 / 10.0 * 0.95), (int)(10_000 * 1 / 10.0 * 1.05));
        results["B"].Should().BeInRange((int)(10_000 * 2 / 10.0 * 0.95), (int)(10_000 * 2 / 10.0 * 1.05));
        results["C"].Should().BeInRange((int)(10_000 * 3 / 10.0 * 0.95), (int)(10_000 * 3 / 10.0 * 1.05));
        results["D"].Should().BeInRange((int)(10_000 * 4 / 10.0 * 0.95), (int)(10_000 * 4 / 10.0 * 1.05));
    }
}
