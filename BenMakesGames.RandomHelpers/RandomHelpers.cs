using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BenMakesGames.RandomHelpers;

public static class RandomHelpers
{
    /// <summary>
    /// Simulates rolling dice to generate a random integer.
    /// </summary>
    /// <param name="r"></param>
    /// <param name="rolls">How many dice to roll.</param>
    /// <param name="sides">How many sides each die has.</param>
    /// <returns></returns>
    public static int Roll(this Random r, int rolls, int sides)
    {
        var total = rolls;

        for (var i = 0; i < rolls; i++)
            total += r.Next(sides);

        return total;
    }

    /// <summary>
    /// Picks a single, random element from the given array, list, or read-only list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rng"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Next<T>(this Random rng, IReadOnlyList<T> list)
        => list[rng.Next(list.Count)];

    /// <summary>
    /// Picks a single, random element from the given hash set, sorted set, etc - anything that implements IReadOnlySet&lt;T&gt;.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rng"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    public static T Next<T>(this Random rng, IReadOnlySet<T> set)
        => set.ElementAt(rng.Next(set.Count));

    /// <summary>
    /// Picks a single, random key from the given dictionary, or read-only dictionary.
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="dictionary"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TKey NextKey<TKey, TValue>(this Random rng, IReadOnlyDictionary<TKey, TValue> dictionary)
        => dictionary.Keys.ElementAt(rng.Next(dictionary.Count));

    /// <summary>
    /// Generates a random string.
    ///
    /// If there are duplicate characters in the allowedCharacters, those characters will be more likely to appear in the result.
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="allowedCharacters">A string (or any ReadOnlySpan&lt;char&gt;) containing the characters allowed.</param>
    /// <param name="length">The length of string to generate.</param>
    /// <returns></returns>
    public static string NextString(this Random rng, ReadOnlySpan<char> allowedCharacters, int length)
    {
        var buffer = new char[length];

        for (var i = 0; i < length; i++)
            buffer[i] = allowedCharacters[rng.Next(allowedCharacters.Length)];

        return new string(buffer);
    }

    /// <summary>
    /// Generates a random string.
    ///
    /// If there are duplicate characters in the allowedCharacters, those characters will be more likely to appear in the result.
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="allowedCharacters">A list of the characters allowed.</param>
    /// <param name="length">The length of string to generate.</param>
    /// <returns></returns>
    public static string NextString(this Random rng, List<char> allowedCharacters, int length)
    {
        var buffer = new char[length];

        for (var i = 0; i < length; i++)
            buffer[i] = allowedCharacters[rng.Next(allowedCharacters.Count)];

        return new string(buffer);
    }

    /// <summary>
    /// Returns true or false.
    /// </summary>
    /// <param name="rng"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NextBool(this Random rng)
        => (rng.Next() & 1) == 1;

    /// <summary>
    /// Returns a random byte.
    /// </summary>
    /// <param name="rng"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte NextByte(this Random rng)
        => (byte)rng.Next(0, 256);

    /// <summary>
    /// Picks a random point inside a circle of the given radius.
    ///
    /// Never picks points at the radius itself - as with many other random methods, the maximum is exclusive.
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="radius">Radius of the circle; defaults to 1.</param>
    /// <returns>A 2-tuple representing the X and Y coordinates.</returns>
    public static (double X, double Y) NextDoublePointInACircle(this Random rng, double radius = 1)
    {
        var angle = rng.NextDouble() * Math.PI * 2;
        var distance = radius * Math.Sqrt(rng.NextDouble());

        return (Math.Cos(angle) * distance, Math.Sin(angle) * distance);
    }

    /// <summary>
    /// Picks a random point inside a circle of the given radius.
    ///
    /// Never picks points at the radius itself - as with many other random methods, the maximum is exclusive.
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="radius">Radius of the circle; defaults to 1.</param>
    /// <returns>A 2-tuple representing the X and Y coordinates.</returns>
    public static (float X, float Y) NextSinglePointInACircle(this Random rng, float radius = 1)
    {
        var angle = rng.NextSingle() * MathF.PI * 2;
        var distance = radius * MathF.Sqrt(rng.NextSingle());

        return (MathF.Cos(angle) * distance, MathF.Sin(angle) * distance);
    }

    /// <summary>
    /// An alias for Random.NextSinglePointInACircle(radius).
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="radius">Radius of the circle; defaults to 1.</param>
    /// <returns>A 2-tuple representing the X and Y coordinates.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (float X, float Y) NextFloatPointInACircle(this Random rng, float radius = 1)
        => rng.NextSinglePointInACircle(radius);

    /// <summary>
    /// Picks a single, random value from the given Enum.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rng"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T NextEnumValue<T>(this Random rng) where T: struct, Enum
        => rng.Next(Enum.GetValues<T>());

    /// <summary>
    /// An alias for Random.Shuffle(Span&lt;T&gt;).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="rng"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Shuffle<T>(this Span<T> list, Random rng) => rng.Shuffle(list);

    /// <summary>
    /// An alias for Random.Shuffle(T[]).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="rng"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Shuffle<T>(this T[] list, Random rng) => rng.Shuffle(list);

    /// <summary>
    /// Fisher-Yates Shuffle. Modifies the list in-place.
    /// from http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="rng"></param>
    public static void Shuffle<T>(this IList<T> list, Random rng)
    {
        var n = list.Count;

        while (n > 1)
        {
            n--;
            var k = rng.Next(n + 1);

            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    /// <summary>
    /// Suppose you want to increase damage by 10%. Someone deals 18 damage. Do they get +1 damage, or +2?
    ///
    /// When you deal with small base numbers, percent bonuses can be hard to work with, since a hard decision to round up or down
    /// will cause your percent modifiers to have a much larger or smaller impact than intended.
    ///
    /// There are a few ways to deal with this:
    /// * Use larger base numbers.
    /// * Use Math.Round (doesn't help much if numbers don't vary by much; especially if they're small to begin with).
    /// * Don't use % bonuses; use fixed bonuses.
    /// * Round down, but then take the remainder as a % chance to add one more.
    ///
    /// Random.NextPercentBonus helps you do the last option (with additional logic to correctly handle % penalties).
    ///
    /// I'd like to emphasize that just because this function helps you do the last option doesn't mean that it's the best
    /// option for YOUR game! Choose the system that works best for your game; if that system happens to be one where numbers
    /// are small, but % bonuses are wanted, then this function may help you solve a problem experienced by that system.
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="baseAmount">The base amount to adjust</param>
    /// <param name="percentModifier">For example, 0.10 represents +10%; -2 represents -200%.</param>
    /// <returns></returns>
    public static int NextPercentBonus(this Random rng, int baseAmount, float percentModifier)
    {
        // if the modifier happens to be 0, let's not busy ourselves with a bunch of math...
        if (percentModifier == 0)
            return baseAmount;

        var realAmount = baseAmount * (1 + percentModifier);

        // round against the modifier: round down, if the modifier is positive; round up if the modifier is negative
        var returnedAmount = (int)(percentModifier > 0 ? Math.Floor(realAmount) : Math.Ceiling(realAmount));

        // get chance of moving 1 more in the direction of the modifier (+1 if the modifier is positive; -1 if it's negative)
        var chanceOfOneMore = Math.Abs(realAmount - baseAmount);

        if(rng.Next() < chanceOfOneMore)
        {
            if (percentModifier < 0)
                returnedAmount--;
            else
                returnedAmount++;
        }

        return returnedAmount;
    }

    /// <summary>
    /// An alias for Random.NextSingle().
    /// </summary>
    /// <param name="rng"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float NextFloat(this Random rng)
        => rng.NextSingle();

    /// <summary>
    /// An alias for Random.NextInt64().
    /// </summary>
    /// <param name="rng"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long NextLong(this Random rng)
        => rng.NextInt64();

    /// <summary>
    /// An alias for Random.NextInt64(max).
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="exclusiveMax"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long NextLong(this Random rng, long exclusiveMax)
        => rng.NextInt64(exclusiveMax);

    /// <summary>
    /// An alias for Random.NextInt64(min, max).
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="inclusiveMin"></param>
    /// <param name="exclusiveMax"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long NextLong(this Random rng, long inclusiveMin, long exclusiveMax)
        => rng.NextInt64(inclusiveMin, exclusiveMax);
}
