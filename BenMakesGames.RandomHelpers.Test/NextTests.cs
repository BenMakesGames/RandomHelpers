using System.Collections.Immutable;
using FluentAssertions;
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

    // unsupported collection types that might be nice to support:
    // IList<T>
    // Span<T>
    // ReadOnlySpan<T>
    // ISet<T>
    // make sure to benchmark solutions!
}
