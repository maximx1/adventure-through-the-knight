using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adventure_through_the_knight.Input
{
    /// <summary>
    /// A helper class to help a dictionary with making generic input commands.
    /// </summary>
    class G_key
    {
        /// <summary>
        /// Quantitizes The specific input commands.
        /// </summary>
        public enum G_KEY { PAUSE, UP, DOWN, LEFT, RIGHT, SHIFT, LMB, RMB };
        /*
         * 0 PAUSE = Esc
         * 1 Up = W
         * 2 DOWN = S
         * 3 LEFT = A
         * 4 RIGHT = D
         * 5 SHIFT = Left Shift
         * 6 LMB = Left Mouse Click
         * 7 RMB = Right Mouse Click
         */
    }
}
