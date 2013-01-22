using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using adventure_through_the_knight.Output.Walls;

namespace adventure_through_the_knight.Output
{
    static class CollisionManager
    {
        /// <summary>
        /// Checks if a sprite's boundingBox (calculated for next move) intersects with a wall
        /// </summary>
        /// <param name="wallManager">wallManager to get list of walls</param>
        /// <param name="boundingBox">sprite's calculated boundingBox based on newPosition</param>
        /// <returns>true if any wall rectangles intersect the sprites bouding box</returns>
        public static bool CheckForWallCollison(WallManager wallManager, Rectangle boundingBox)
        {
            try
            {
                foreach (var wall in wallManager.Walls)
                {
                    if (wall.BoundingBox.Intersects(boundingBox))
                    {
                        return true;
                    }
                }
            }
            catch (Exception er) { }
            return false;
        }

        /// <summary>
        /// Basic collision detection
        /// </summary>
        /// <param name="boundingBox1">bounding rectangle of sprite 1</param>
        /// <param name="boundingBox2">bouding rectangle of sprite 2</param>
        /// <returns></returns>
        public static bool CheckForCollision(Rectangle boundingBox1, Rectangle boundingBox2)
        {
            if(boundingBox1.Intersects(boundingBox2))
            {
                return true;
            }
            return false;
        }
    }
}
