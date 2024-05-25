# What is it?

Extensions for `System.Random` and `IList` to help you generate random content, including dice rolls, enum values, items from lists, sets, dictionaries, and more.

**Hey! Listen!** this library was designed for use in games; no effort has been made to make these methods cryptographically secure.

* nuget package: https://www.nuget.org/packages/BenMakesGames.RandomHelpers
* GitHub repo: https://github.com/BenMakesGames/RandomHelpers

Pro tip: don't `new` up instances of `System.Random` if you don't need to control the seed. Just use `System.Random.Shared`!

[![Buy Me a Coffee at ko-fi.com](https://raw.githubusercontent.com/BenMakesGames/AssetsForNuGet/main/buymeacoffee.png)](https://ko-fi.com/A0A12KQ16)

# Upgrading from 4.x to 5.1.0

1. .NET 8.0 is now required.
2. `Span<T>.Shuffle(Random)` and `T[].Shuffle(Random)` have been added as pass-thrus for .NET 8's built-in `Random.Shuffle` methods.

# Reference

## `int Random.Roll(int rolls, int sides)`

Simulates rolling dice to generate a random integer.

* **rolls**: Number of times to roll the die.
* **sides**: Number of sides of the die.

Example usage:

```c#
int damage = Random.Shared.Roll(2, 6) + 2; // 2d6+2 damage
```

## `T Random.Next(IReadOnlyList<T> list)`

Picks a single, random element from the given array, list, or read-only list.

Example usage:

```c#
var names = new List<string>() { "Abby", "Ben", "Carly" };
var name = Random.Shared.Next(names);
```

## `T Random.Next(IReadOnlySet<T> set)`

As above, but for sets, including `HashSet`, `SortedSet`, etc - anything that implements `IReadOnlySet<T>`.

## `TKey Random.NextKey(IReadOnlyDictionary<TKey, TValue> dictionary)`

Picks a single, random key from the given dictionary, or read-only dictionary.

Example usage:

```c#
var myFavoriteNumbers = new Dictionary<double, string>() {
    { -1 / 12.0, "negative one-twelfth" },
    { 7, "seven" },
    { 42, "forty-two" },
};

var number = Random.Shared.NextKey(myFavoriteNumbers);
```

## `string Random.NextString(ReadOnlySpan<char> allowedCharacters, int length)`

Generates a random string.

* **allowedCharacters**: A `string` (or any `ReadOnlySpan<char>`) containing the characters which can appear in the generated string.
* **length**: The length of the string to generate.

Example usage:

```c#
string id = Random.Shared.NextString("abcdefghijklmnopqrstuvwxyz0123456789", 16);
```

## `string Random.NextString(List<char> allowedCharacters, int length)`

Generates a random `string`.

* **allowedCharacters**: A list containing the characters which can appear in the generated string.
* **length**: The length of the string to generate.

## `bool Random.NextBool()`

Returns either `true`, or `false`.

## `bool Random.NextByte()`

Returns a single, random byte (a value from 0 to 255).

## `(double X, double Y) Random.NextDoublePointInACircle(double radius = 1)`

Generates a random point inside a circle of the given radius and centered at (0, 0).

Example usage:

```c#
var (x, y) = Random.Shared.NextDoublePointInACircle();
```

## `(float X, float Y) Random.NextSinglePointInACircle(double radius = 1)`

Generates a random point inside a circle of the given radius and centered at (0, 0).

Example usage:

```c#
var (x, y) = Random.Shared.NextSinglePointInACircle();
```

The alias `NextFloatPointInACircle` also exists, in case you like calling floats "Float" instead of "Single".

## `T Random.NextEnumValue<T>()`

Picks a single, random value from the given `Enum` type.

Example usage:

```c#
public enum Race
{
    Elf,
    Dwarf,
    Human
}
```

...

```c#
var race = Random.Shared.NextEnumValue<Race>();
```

## `void IList<T>.Shuffle(Random rng)`

Fisher-Yates Shuffle. Modifies the array or list in-place.

Unlike the other methods in this library, `Shuffle` operates on a list, and must be passed an instance of `Random` (instead of operating on an RNG, and passing a list).

Example usage:

```c#
var favoriteFruit = new string[] { "Mango", "Watermelon", "Raspberry", "Cantaloupe" };
favoriteFruit.Shuffle(Random.Shared);
```

> .NET 8 adds a `Random.Shuffle(...)` method, but it does not work on `IList<T>`s. RandomHelpers provides aliases for .NET 8's `Random.Shuffle(...)` methods for `Span<T>` and `T[]`. These may behave differently from RandomHelpers's `IList<T>.Shuffle` method when using the same random seed.

## `int Random.NextPercentBonus(int baseAmount, float percentModifier)`

Suppose you want to increase damage by 10%. Someone deals 18 damage. Do they get +1 damage, or +2?

When you deal with small base numbers, percent bonuses can be hard to work with, since a hard decision to round up or down will cause your percent modifiers to have a much larger or smaller impact than intended.

There are a few ways to deal with this:

* Use larger base numbers.
* Use Math.Round (doesn't help much if numbers don't vary by much; especially if they're small to begin with).
* Don't use % bonuses; use fixed bonuses.
* Round down, but then take the remainder as a % chance to add one more.

`Random.NextPercentBonus` helps you do the last option (with additional logic to correctly handle % penalties).

I'd like to emphasize that just because this function helps you do the last option doesn't mean that it's the best option for YOUR game! Choose the system that works best for your game; if that system happens to be one where numbers are small, but % bonuses are wanted, then this function may help you solve a problem experienced by that system.

Example usage:

```c#
int damage = Random.Shared.Roll(2, 6) + 2; // 2d6+2
float damageBonus = 0.15f; // +15%

int finalDamage = Random.Shared.NextPercentBonus(damage, damageBonus);
```

## Additional basic methods

* `Random.NextDouble(double exclusiveMax)`
* `Random.NextDouble(double inclusiveMin, double exclusiveMax)`
* `Random.NextSingle(float exclusiveMax)`
* `Random.NextSingle(float inclusiveMin, float exclusiveMax)`
* `Random.NextFloat(float exclusiveMax)` - alias for `Random.NextSingle(float exclusiveMax)`
* `Random.NextFloat(float inclusiveMin, float exclusiveMax)` - alias for `Random.NextSingle(float inclusiveMin, float exclusiveMax)`

## Additional alias methods

These are provided for convenience; aliases for built-in .NET methods. They utilize `[MethodImpl(MethodImplOptions.AggressiveInlining)]` to reduce any potential overhead in their use. (See the benchmark project for details.)

| RandomHelpers alias | .NET method |
| --- | --- |
| `Random.NextFloat()` | `Random.NextSingle()` |
| `Random.NextLong()` | `Random.NextInt64()` |
| `Random.NextLong(long exclusiveMax)` | `Random.NextInt64(long)` |
| `Random.NextLong(long inclusiveMin, long exclusiveMax)` | `Random.NextInt64(long, long)` |
| `Span<T>.Shuffle(Random)` | `Random.Shuffle(Span<T>)` |
| `T[].Shuffle(Random)` | `Random.Shuffle(T[])` |
