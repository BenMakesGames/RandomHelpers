using System;
using System.Collections.Generic;
using System.Linq;

namespace BenMakesGames.RandomHelpers
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
        /// Picks a single, random element from the given Read-only List.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Next<T>(this Random r, IReadOnlyList<T> list)
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
        /// Returns true, or false.
        /// </summary>
        /// <param name="rng"></param>
        /// <returns></returns>
        public static bool NextBool(this Random rng)
        {
            return rng.NextDouble() < 0.5;
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

        /// <summary>
        /// Suppose you want to increase damage by 10%. Someone deals 18 damage. Do they get +1 damage, or +2?
        /// When you deal with small base numbers, percent bonuses can be hard to work with, since a hard decision to round up or down
        /// will cause your percent modifieres to have a much larger or smaller impact than intended.
        /// There are a few ways to deal with this:
        /// * Use larger base numbers.
        /// * Use Math.Round (doesn't help much if numbers don't vary by much; especially if they're small to begin with).
        /// * Don't use % bonuses; use fixed bonuses.
        /// * Round down, but then take the remainder as a % chance to add one more.
        /// Random.NextPercentBonus helps you do the last option (with additional logic to correctly handle % penalties).
        /// I'd like to emphasize that just because this function helps you do the last option doesn't mean that it's the best
        /// option for YOUR game! Choose the system that works best for your game; if that system happens to be one where numbers
        /// are small, but % bonuses are wanted, then this function may help you solve a problem experienced by that system.
        /// </summary>
        /// <param name="rng"></param>
        /// <param name="baseAmount">The base amount to adjust</param>
        /// <param name="percentIncrease">For example, 0.10 represents +10%; -2 represents -200%.</param>
        /// <returns></returns>
        public static int NextPercentBonus(this Random rng, int baseAmount, float percentModifier)
        {
            // if the modifier happens to be 0, let's not busy ourselves with a bunch of math...
            if (percentModifier == 0)
                return baseAmount;

            float realAmount = baseAmount * (1 + percentModifier);

            // round against the modifier: round down, if the modifier is positive; round up if the modifier is negative
            int returnedAmount = (int)(percentModifier > 0 ? Math.Floor(realAmount) : Math.Ceiling(realAmount));

            // get chance of moving 1 more in the direction of the modifier (+1 if the modifier is positive; -1 if it's negative)
            float chanceOfOneMore = Math.Abs(realAmount - baseAmount);

            if(rng.Next() < chanceOfOneMore)
            {
                if (percentModifier < 0)
                    returnedAmount--;
                else
                    returnedAmount++;
            }

            return returnedAmount;
        }
    }
}
