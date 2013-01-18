using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace adventure_through_the_knight.Output
{
    static class SpriteSheetCalculator
    {
		/// <summary>
		/// Calculates the source rectangle.
		/// </summary>
		/// <returns>
		/// The sprite's current image from the sprite sheet.
		/// </returns>
		/// <param name='textureWidth'>
		/// Texture width.
		/// </param>
		/// <param name='textureHeight'>
		/// Texture height.
		/// </param>
		/// <param name='columns'>
		/// Columns.
		/// </param>
		/// <param name='rows'>
		/// Rows.
		/// </param>
		/// <param name='currentFrame'>
		/// Current frame.
		/// </param>
        /// <param name="Moved">
        /// Tells if the character has moved
        /// </param>
        public static Rectangle CalculateSourceRect(int textureWidth, int textureHeight, int columns, int rows, int currentFrame, bool Moved)
        {
            var imageWidth = textureWidth / columns;
            var imageHeight = textureHeight / rows;

            var currentRow = 0;
            var currentColumn = 1;
            if (Moved)
            {
                currentRow = currentFrame / columns;
                currentColumn = currentFrame % columns;
            }
            return new Rectangle(imageWidth * currentColumn, imageHeight * currentRow, imageWidth, imageHeight);
        }

		/// <summary>
		/// Calculates the location where the sprite is going to be.
		/// </summary>
		/// <returns>
		/// The destination rect.
		/// </returns>
		/// <param name='position'>
		/// Position.
		/// </param>
		/// <param name='sourceRectangle'>
		/// Source rectangle.
		/// </param>
        public static Rectangle CalculateDestinationRect(Vector2 position, Rectangle sourceRectangle)
        {
            return new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
        }
    }
}
