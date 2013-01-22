using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using adventure_through_the_knight.Output.Base;

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

		/// <summary>
		/// Calculates a 2D Vector into an angle from the Vector's origin.
		/// </summary>
		/// <returns>
		/// The angle, in Radians, of the vector.
		/// </returns>
		/// <param name='twoDVector'>
		/// A vector2 to calculate.
		/// </param>
		public static float VectorToRad(Vector2 twoDVector)
		{
			return((float)System.Math.Atan2(-twoDVector.Y, twoDVector.X));
		}

		/// <summary>
		/// Calculates the 8 point direction from an angle in Degrees.
		/// </summary>
		/// <returns>
		/// The Sprite.Direction value of the angle.
		/// </returns>
		/// <param name='angleInDegrees'>
		/// Angle in degrees.
		/// </param>
		public static Sprite.Direction DegreesTo8Point(float angleInDegrees)
		{
			Sprite.Direction spriteDirection = Sprite.Direction.Down;

			angleInDegrees += 22.5f;

            if (angleInDegrees < 0)
                angleInDegrees += 360f;

            for(int i = 0; i < 8; i++)
            {
                if(Range.InRange(angleInDegrees, (float)i * 45, (float)i * 45 + 45))
                {
                    spriteDirection = (Sprite.Direction)i;
                    return spriteDirection;
                }
            }

			return spriteDirection;
		}
    }
}
