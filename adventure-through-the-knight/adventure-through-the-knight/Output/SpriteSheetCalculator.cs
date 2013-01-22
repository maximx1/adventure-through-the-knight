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
using System.Xml.Linq;
using adventure_through_the_knight.Output.Base;

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
        public static Rectangle CalculateSourceRect(int textureWidth, int textureHeight, int columns, int rows, int currentFrame, bool Moved, XElement spriteSheetMap, Sprite.Direction direction)
        {
            currentFrame += 1;
            var SourceMap = getSourceMap(spriteSheetMap, Moved, direction, currentFrame);
            int imageWidth = (int)SourceMap.Attribute("Width");
            int imageHeight = (int)SourceMap.Attribute("Height");

            int imageX = (int)SourceMap.Attribute("X");
            int imageY = (int)SourceMap.Attribute("Y");

            return new Rectangle(imageX, imageY, imageWidth, imageHeight);
        }

        private static XElement getSourceMap(XElement spriteSheetMap, bool moved, Sprite.Direction direction, int currentFrame)
        {
            string stringDirection = GetDirectionName(direction);
            XElement movementFrames = null;
            try
            {
                if (moved)
                {
                    movementFrames = spriteSheetMap.Element("Moving");
                }
                else
                {
                    movementFrames = spriteSheetMap.Element("NotMoving");
                }
            }
            catch
            {
            }
            var DirectionFrames = movementFrames.Elements(stringDirection).ToList();
            var Frames = DirectionFrames.Elements("Frame").ToList();
            var TargetFrame = Frames.Where(x => x.Attribute("ID").Value == currentFrame.ToString()).FirstOrDefault();
            

            return TargetFrame;
        }

        private static string GetDirectionName(Sprite.Direction direction)
        {
            switch (direction)
            {
                case Sprite.Direction.Right:
                    return "Right";
                case Sprite.Direction.Up_Right:
                    return "Right-Up";
                case Sprite.Direction.Up:
                    return "Up";
                case Sprite.Direction.Up_Left:
                    return "Left-Up";
                case Sprite.Direction.Left:
                    return "Left";
                case Sprite.Direction.Down_Left:
                    return "Left-Down";
                case Sprite.Direction.Down:
                    return "Down";
                case Sprite.Direction.Down_Right:
                    return "Right-Down";
                default:
                    return "";
            }
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
