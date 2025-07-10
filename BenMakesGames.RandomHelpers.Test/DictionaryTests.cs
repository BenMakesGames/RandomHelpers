using Shouldly;
using Xunit;

namespace BenMakesGames.RandomHelpers.Test;

public sealed class DictionaryTests
{
    private const int AnySeed = 314159;

    [Fact]
    public void IReadOnlyDictionaryNextKey_ShouldReturnKeysWithEvenDistribution()
    {
        const string anyValue = "AnyValue";
        const int iterations = 10_000;

        var random = new Random(AnySeed);

        // char => string - random data with keys 'a' through 'j'
        var dictionary = Enumerable.Range(0, 10).ToDictionary(r => (char)(r + 'a'), _ => anyValue);

        // char => int - count of how many times each key was returned
        var keyCount = dictionary.ToDictionary(kvp => kvp.Key, _ => 0);

        // act:
        for (var i = 0; i < iterations; i++)
            keyCount[random.NextKey(dictionary)]++;

        // ensure each key occurs fairly evenly - within 10% of the average
        var averageCount = iterations / dictionary.Keys.Count;

        foreach (var kvp in keyCount)
            kvp.Value.ShouldBeInRange(averageCount * 9 / 10, averageCount * 11 / 10);
    }
}
