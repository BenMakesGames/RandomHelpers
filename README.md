Extensions for Random and IList to help you generate random content. IMPORTANT: this library was designed for use in games; no effort has been made to make these methods cryptographically secure.

* nuget package: https://www.nuget.org/packages/BenMakesGames.RandomHelpers
* GitHub repo: https://github.com/BenMakesGames/RandomHelpers

---

## int Random.Roll(int rolls, int sides)

Simulates rolling dice to generate a random integer.

* **rolls**: Number of times to roll the die.
* **sides**: Number of sides of the die.

Example usage:

    // assuming some instance of Random named "rng":
    int damage = rng.Roll(2, 6) + 2; // 2d6+2 damage

## T Random.Next(IList<T> list)

Picks a single, random element from the given List or array.

* **list**: The list or array to pick an element from.

Example usage:

    // assuming some instance of Random named "rng":
    List<string> names = new List<string>() { "Abby", "Ben", "Carly" };
    string name = rng.Next(names);

## string Random.NextString(string allowedCharacters, int length)

Generates a random string.

* **allowedCharacters**: A string containing the characters which can appear in the generated string.
* **length**: The length of the generated string.

Example usage:

    // assuming some instance of Random named "rng":
    string id = rng.NextString("abcdefghijklmnopqrstuvwxyz0123456789", 16);

## string Random.NextString(IList<char> allowedCharacters, int length)

Generates a random string.

* **allowedCharacters**: A List or array containing the characters which can appear in the generated string.
* **length**: The length of the generated string.

## T Random.NextEnumValue<T>()

Picks a single, random value from the given Enum. Throws an exception if the given type is not an Enum.

Example usage:

    public enum Race
    {
        Elf,
        Dwarf,
        Human
    }

...

    // assuming some instance of Random named "rng":
    Race race = rng.NextEnumValue<Race>();

## void IList<T>.Shuffle(Random rng)

Fisher-Yates Shuffle. Modifies the list in-place.

* **rng**: The RNG to use when performing the shuffle.

Example usage:

    // assuming some instance of Random named "rng":
    string[] favoriteFruit = new string[] { "Mango", "Watermelon", "Raspberry", "Cantaloupe" };
    favoriteFruit.Shuffle(rng);