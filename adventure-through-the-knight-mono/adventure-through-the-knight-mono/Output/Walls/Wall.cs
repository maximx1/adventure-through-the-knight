using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace adventure_through_the_knight.Output.Walls
{
    class Wall : Sprite
    {
        public Wall(Texture2D texture, Vector2 position, Rectangle movementBounds)
            : base(texture, position, movementBounds)
        {

        }
    }
}
