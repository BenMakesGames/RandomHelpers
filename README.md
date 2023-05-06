Extensions for `System.Random` and `IList` to help you generate random content, including dice rolls, enum values, items from lists, sets, dictionaries, and more.

**Hey! Listen!** this library was designed for use in games; no effort has been made to make these methods cryptographically secure.

* nuget package: https://www.nuget.org/packages/BenMakesGames.RandomHelpers
* GitHub repo: https://github.com/BenMakesGames/RandomHelpers

Pro tip: don't `new` up instances of `System.Random` if you don't need to control the seed. Just use `System.Random.Shared`!

[![Buy Me a Coffee at ko-fi.com](https://raw.githubusercontent.com/BenMakesGames/AssetsForNuGet/main/buymeacoffee.png)](https://ko-fi.com/A0A12KQ16)

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

## `TKey Random.Next(IReadOnlyDictionary<TKey, TValue> dictionary)`

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

## `string Random.NextString(string allowedCharacters, int length)`

Generates a random string.

* **allowedCharacters**: A string containing the characters which can appear in the generated string.
* **length**: The length of the string to generate.

Example usage:

```c#
// assuming some instance of Random named "rng":
string id = Random.Shared.NextString("abcdefghijklmnopqrstuvwxyz0123456789", 16);
```

## `string Random.NextString(IReadOnlyList<char> allowedCharacters, int length)`

Generates a random `string`.

* **allowedCharacters**: A list or array containing the characters which can appear in the generated string.
* **length**: The length of the string to generate.

## `bool Random.NextBool()`

Returns either `true`, or `false`.

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
// assuming some instance of Random named "rng":
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

## Additional alias methods

* `NextFloat()`
  * An alias for the system-provided `NextSingle()` (in case, like me, you always get tripped up by the whole float/single nomenclature)
* `NextLong()`, `NextLong(long exclusiveMax)`, and `NextLong(long inclusiveMin, long exclusiveMax)`
  * Aliases for the system-provided `NextInt64` family