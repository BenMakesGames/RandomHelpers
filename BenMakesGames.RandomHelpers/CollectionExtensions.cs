using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BenMakesGames.RandomHelpers;

public static class CollectionExtensions
{
    /// <summary>
    /// Creates a new Queue from the source collection, shuffled.
    /// </summary>
    /// <example>
    /// This can be useful when you want to randomly pull items from a list when an
    /// easy-to-use index is not available, for example in a .Select that contains
    /// conditionals.
    ///
    /// <code>
    /// var allAttackPositions = Enumerable.Range(0, 50);
    /// var allDefendPositions = Enumerable.Range(50, 50);
    /// <br/>
    /// var shuffledAttackPositions = allAttackPositions.ToShuffledQueue(Random.Shared);
    /// var shuffledDefendPositions = allDefendPositions.ToShuffledQueue(Random.Shared);
    /// <br/>
    /// var enemyPositions = enemy.Select(e => e.Role switch {
    ///     EnemyRole.Attacker => shuffledAttackPositions.Dequeue(),
    ///     EnemyRole.Defender => shuffledDefendPositions.Dequeue(),
    /// });
    /// </code>
    /// </example>
    /// <param name="source"></param>
    /// <param name="rng"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>A Queue containing all of the elements in <c>source</c>, in a random order.</returns>
    public static Queue<T> ToShuffledQueue<T>(this IEnumerable<T> source, Random rng)
    {
        var list = new List<T>(source);

        list.Shuffle(rng);

        return new Queue<T>(list);
    }

    /// <inheritdoc cref="ToShuffledQueue{T}(System.Collections.Generic.IEnumerable{T},System.Random)"/>
    public static Queue<T> ToShuffledQueue<T>(this Span<T> source, Random rng)
    {
        var list = source.ToArray();

        list.Shuffle(rng);

        return new Queue<T>(list);
    }

    /// <see cref="Shuffle{T}(System.Collections.Generic.IList{T},System.Random)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Shuffle<T>(this Span<T> list, Random rng) => rng.Shuffle(list);

    /// <see cref="Shuffle{T}(System.Collections.Generic.IList{T},System.Random)"/>
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
}
