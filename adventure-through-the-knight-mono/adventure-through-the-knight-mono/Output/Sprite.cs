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
    class Sprite
    {
        private Vector2 position;
        private readonly Texture2D texture;
        protected Vector2 Velocity { get; set; }

        protected float Speed { get; set; }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.position = position;
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public virtual void Update(KeyboardState keyboardState, GameTime gameTime)
        {
            position += (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
