## int Random.Roll(int rolls, int sides)

Simulates rolling dice to generate a random integer.

* **rolls**: Number of times to roll the die.
* **sides**: Number of sides of the die.

## T Random.Next(IList<T> list)

Picks a single, random element from the given List or array.

* **list**: The list or array to pick an element from.

## string Random.NextString(string allowedCharacters, int length)

Generates a random string.

* **allowedCharacters**: A string containing the characters which can appear in the generated string.
* **length**: The length of the generated string.

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
