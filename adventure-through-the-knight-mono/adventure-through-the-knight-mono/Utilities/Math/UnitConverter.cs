using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adventure_through_the_knight.Utilities.Math
{
    class UnitConverter
    {
        /// <summary>
        /// Converts Radians to Angles
        /// </summary>
        /// <param name="Rad">Angle in Radians</param>
        /// <returns></returns>
        public static float RadToDegrees(float radians)
        {
            return (radians * (180f / (float)System.Math.PI));
        }
    }
}
