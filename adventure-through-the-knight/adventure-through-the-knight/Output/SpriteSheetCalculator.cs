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
    class SpriteSheetCalculator
    {
        private int height;
        private int width;
        private const double TimeToNextChange = 0.5;
        private int currentRect = 0;

        public SpriteSheetCalculator(int width, int height)
        {
            this.height = height;
            this.width = width;
        }

        public Rectangle GetSourceRectangle(GameTime gameTime)
        {
            // Just walking down for now

            int previousRect = currentRect;
            if (gameTime.ElapsedGameTime.TotalSeconds > TimeToNextChange)
            {
                if (previousRect != 2)
                    currentRect +=01;
                else
                    currentRect = 1;
            }

            return new Rectangle(5 + (width * currentRect), 0, width, height);
        }
    }
}
