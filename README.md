Extensions for Random and IList to help you generate random content, including dice rolls, enum values, items from lists, and more.

**Hey! Listen!** this library was designed for use in games; no effort has been made to make these methods cryptographically secure.

* nuget package: https://www.nuget.org/packages/BenMakesGames.RandomHelpers
* GitHub repo: https://github.com/BenMakesGames/RandomHelpers

[![Buy Me a Coffee at ko-fi.com](https://raw.githubusercontent.com/BenMakesGames/AssetsForNuGet/main/buymeacoffee.png)](https://ko-fi.com/A0A12KQ16)

---

## int Random.Roll(int rolls, int sides)

Simulates rolling dice to generate a random integer.

* **rolls**: Number of times to roll the die.
* **sides**: Number of sides of the die.

Example usage:

```c#
// assuming some instance of Random named "rng":
int damage = rng.Roll(2, 6) + 2; // 2d6+2 damage
```

## T Random.Next(IList<T> list)

Picks a single, random element from the given List or array.

* **list**: The list or array to pick an element from.

Example usage:

```c#
// assuming some instance of Random named "rng":
List<string> names = new List<string>() { "Abby", "Ben", "Carly" };
string name = rng.Next(names);
```

## string Random.NextString(string allowedCharacters, int length)

Generates a random string.

* **allowedCharacters**: A string containing the characters which can appear in the generated string.
* **length**: The length of the generated string.

Example usage:

```c#
// assuming some instance of Random named "rng":
string id = rng.NextString("abcdefghijklmnopqrstuvwxyz0123456789", 16);
```

## string Random.NextString(IList<char> allowedCharacters, int length)

Generates a random string.

* **allowedCharacters**: A List or array containing the characters which can appear in the generated string.
* **length**: The length of the generated string.

## bool Random.NextBool()

Returns either true, or false.

## T Random.NextEnumValue<T>()

Picks a single, random value from the given Enum. Throws an exception if the given type is not an Enum.

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
Race race = rng.NextEnumValue<Race>();
```

## int Random.NextPercentBonus(int baseAmount, float percentModifier)

Suppose you want to increase damage by 10%. Someone deals 18 damage. Do they get +1 damage, or +2?

When you deal with small base numbers, percent bonuses can be hard to work with, since a hard decision to round up or down will cause your percent modifieres to have a much larger or smaller impact than intended.

There are a few ways to deal with this:

* Use larger base numbers.
* Use Math.Round (doesn't help much if numbers don't vary by much; especially if they're small to begin with).
* Don't use % bonuses; use fixed bonuses.
* Round down, but then take the remainder as a % chance to add one more.

`Random.NextPercentBonus` helps you do the last option (with additional logic to correctly handle % penalties).

I'd like to emphasize that just because this function helps you do the last option doesn't mean that it's the best option for YOUR game! Choose the system that works best for your game; if that system happens to be one where numbers are small, but % bonuses are wanted, then this function may help you solve a problem experienced by that system.

Example usage:

```c#
// assuming some instance of Random named "rng":
int damage = rng.Roll(2, 6) + 2; // 2d6+2
float damageBonus = 0.15f; // +15%

int finalDamage = rng.NextPercentBonus(damage, damageBonus);
```

## void IList<T>.Shuffle(Random rng)

Fisher-Yates Shuffle. Modifies the list in-place.

* **rng**: The RNG to use when performing the shuffle.

Example usage:

```c#
// assuming some instance of Random named "rng":
string[] favoriteFruit = new string[] { "Mango", "Watermelon", "Raspberry", "Cantaloupe" };
favoriteFruit.Shuffle(rng);
```
