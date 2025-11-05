using Xunit;

namespace BenMakesGames.RandomHelpers.Test;

public sealed class NextTests
{
    [Fact]
    public void Next_ShouldBeInvokable_WhenCollectionIsAnArray()
    {
        var numberArray = Enumerable.Range(0, 10).ToArray();

        Random.Shared.Next(numberArray);
    }

    [Fact]
    public void Next_ShouldBeInvokable_WhenCollectionIsAList()
    {
        var numberList = Enumerable.Range(0, 10).ToList();

        Random.Shared.Next(numberList);
    }

    [Fact]
    public void Next_ShouldBeInvokable_WhenCollectionIsAHashSet()
    {
        var numberList = new HashSet<int>(Enumerable.Range(0, 10));

        Random.Shared.Next(numberList);
    }

    [Fact]
    public void Next_ShouldBeInvokable_WhenCollectionIsAReadOnlySpan()
    {
        var numberList = "0123456789".AsSpan();

        Random.Shared.Next(numberList);
    }

    [Fact]
    public void Next_ShouldBeInvokable_WhenCollectionIsASpan()
    {
        var numberList = new Span<int>([ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ]);

        Random.Shared.Next(numberList);
    }

    // unsupported collection types that might be nice to support:
    // IList<T>
    // ISet<T>
    // make sure to benchmark solutions!
}
