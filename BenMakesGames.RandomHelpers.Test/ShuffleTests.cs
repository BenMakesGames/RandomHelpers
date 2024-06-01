using System.Collections.Immutable;
using FluentAssertions;
using Xunit;

namespace BenMakesGames.RandomHelpers.Test;

public sealed class ShuffleTests
{
    [Fact]
    public void Shuffle_ShouldBeInvokable_WhenCollectionIsAnArray()
    {
        var numberArray = Enumerable.Range(0, 10).ToArray();

        numberArray.Shuffle(Random.Shared);
    }

    [Fact]
    public void Shuffle_ShouldBeInvokable_WhenCollectionIsASpan()
    {
        var numberSpan = Enumerable.Range(0, 10).ToArray().AsSpan();

        numberSpan.Shuffle(Random.Shared);
    }

    [Fact]
    public void Shuffle_ShouldBeInvokable_WhenCollectionIsAList()
    {
        var numberList = Enumerable.Range(0, 10).ToList();

        numberList.Shuffle(Random.Shared);
    }

    [Fact]
    public void Shuffle_ShouldBeInvokable_WhenCollectionIsAnIList()
    {
        IList<int> numberList = Enumerable.Range(0, 10).ToList();

        numberList.Shuffle(Random.Shared);
    }
}
