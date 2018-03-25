using System;
using System.Collections.Generic;
using System.Linq;

namespace BenMakesGames
{
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
            // if this looks weird, consider that the alternative is to set "total" to 0, and increment by r.Next(sides) + 1 for each roll.
            // instead of that - instead of adding 1 to the total for each roll - I'm starting by setting total = rolls. same thing; less operations.

            int total = rolls;

            for (int i = 0; i < rolls; i++)
                total += r.Next(sides);

            return total;
        }

        /// <summary>
        /// Picks a single, random element from the given List.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Next<T>(this Random r, IList<T> list)
        {
            int i = r.Next(list.Count);

            return list[i];
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="rng"></param>
        /// <param name="allowedCharacters">A string containing the characters allowed.</param>
        /// <param name="length">The length of string to generate.</param>
        /// <returns></returns>
        public static string NextString(this Random rng, string allowedCharacters, int length)
        {
            return rng.NextString(allowedCharacters.ToCharArray(), length);
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="rng"></param>
        /// <param name="allowedCharacters">A string containing the characters allowed.</param>
        /// <param name="length">The length of string to generate.</param>
        /// <returns></returns>
        public static string NextString(this Random rng, IList<char> allowedCharacters, int length)
        {
            char[] buffer = new char[length];

            for (int i = 0; i < length; i++)
                buffer[i] = allowedCharacters[rng.Next(allowedCharacters.Count)];

            return new string(buffer);
        }

        /// <summary>
        /// Picks a single, random value from the given Enum. Throws an exception if the given type is not an Enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r"></param>
        /// <returns></returns>
        public static T NextEnumValue<T>(this Random r)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type; got \"" + typeof(T).ToString() + "\".");

            List<T> values = Enum.GetValues(typeof(T)).Cast<T>().ToList();

            return values[r.Next(values.Count)];
        }

        /// <summary>
        /// Fisher-Yates Shuffle. Modifies the list in-place.
        /// from http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="rng"></param>
        public static void Shuffle<T>(this IList<T> list, Random rng)
        {
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
