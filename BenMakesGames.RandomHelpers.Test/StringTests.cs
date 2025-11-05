using Shouldly;
using Xunit;

namespace BenMakesGames.RandomHelpers.Test;

public sealed class StringTests
{
    private const int AnySeed = 314159;

    [Fact]
    public void NextString_ShouldReturnStringsOfTheGivenLength()
    {
        const int requestedLength = 50;
        const int iterations = 10_000;
        const string allowedCharacters = "abcdefgHIJKLMNOP";

        var random = new Random(AnySeed);

        for (var i = 0; i < iterations; i++)
            random.NextString(allowedCharacters, requestedLength).Length.ShouldBe(requestedLength);
    }

    [Fact]
    public void NextString_ShouldBeInvokable_WhenAllowedCharactersIsAString()
    {
        const int anyLength = 10;
        const string allowedCharacters = "abcdefgHIJKLMNOP";

        var random = new Random(AnySeed);

        random.NextString(allowedCharacters, anyLength);
    }

    [Fact]
    public void NextString_ShouldBeInvokable_WhenAllowedCharactersIsAnArray()
    {
        const int anyLength = 10;
        var allowedCharacters = "abcdefgHIJKLMNOP".ToCharArray();

        var random = new Random(AnySeed);

        random.NextString(allowedCharacters, anyLength);
    }

    [Fact]
    public void NextString_ShouldBeInvokable_WhenAllowedCharactersIsASpan()
    {
        const int anyLength = 10;
        var allowedCharacters = "abcdefgHIJKLMNOP".AsSpan();

        var random = new Random(AnySeed);

        random.NextString(allowedCharacters, anyLength);
    }

    [Fact]
    public void NextString_ShouldBeInvokable_WhenAllowedCharactersIsAList()
    {
        const int anyLength = 10;
        var allowedCharacters = "abcdefgHIJKLMNOP".ToCharArray().ToList();

        var random = new Random(AnySeed);

        random.NextString(allowedCharacters, anyLength);
    }
}
