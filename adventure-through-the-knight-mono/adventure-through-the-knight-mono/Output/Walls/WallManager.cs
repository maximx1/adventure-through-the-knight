using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace adventure_through_the_knight.Output.Walls
{
    public class WallManager
    {
        /// <summary>
        /// the default texture for walls
        /// </summary>
        private readonly Texture2D texture;

        /// <summary>
        /// list of wall objects
        /// </summary>
        private List<Wall> walls = new List<Wall>();
        /// <summary>
        /// graphics device for passing in viewport bounds to constructor
        /// </summary>
        private readonly GraphicsDevice graphics;


        /// <summary>
        /// Getter for the walls list
        /// </summary>
        public IEnumerable<Wall> Walls
        {
            get { return walls; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics">graphics device for getting viewport bounds</param>
        public WallManager(GraphicsDevice graphics)
        {
            this.graphics = graphics;
        }

        /// <summary>
        /// Draw method for drawing each wall in the list
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var wall in Walls)
            {
                wall.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Creates a new wall object with the default texture and adds it to the wall list
        /// </summary>
        /// <param name="position">position of the wall</param>
        public void CreateWall(Vector2 position)
        {
            var wall = new Wall(texture, position, graphics.Viewport.Bounds);
            walls.Add(wall);
        }

        /// <summary>
        /// Overloaded method that takes a different texture
        /// </summary>
        /// <param name="position">position of the wall</param>
        /// <param name="texture">texture of the wall</param>
        public void CreateWall(Vector2 position, Texture2D texture)
        {
            var wall = new Wall(texture, position, graphics.Viewport.Bounds);
            walls.Add(wall);
        }
    }
}
