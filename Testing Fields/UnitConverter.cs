using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testing_Fields
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
            return (radians * ((float)180.0 / (float)System.Math.PI));
        }
    }
}
