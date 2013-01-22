using System;

namespace adventure_through_the_knight.Utilities.Math
{
	/// <summary>
	/// Collection of functions to test and and manipulate Ranges.
	/// </summary>
	public class Range
	{
		/// <summary>
        /// Test if a number is within the range of another number.
        /// </summary>
        /// <param name="TestVariable">True if the value is within range</param>
        /// <param name="Min">The Minimum number inclusive.</param>
        /// <param name="Max">The Maximum number non-inclusive</param>
        /// <returns></returns>
        public static bool InRange(float TestVariable, float Min, float Max)
        {
            return TestVariable < Max && TestVariable >= Min ? true : false; 
        }
	}
}

