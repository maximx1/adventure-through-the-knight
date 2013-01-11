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

using adventure_through_the_knight.Output.Base;

namespace adventure_through_the_knight.Output.Character
{
    class Player : Sprite
    {
        public Player(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            this.Speed = 100;
        }

        public override void Update(Microsoft.Xna.Framework.Input.KeyboardState keyboardState, Microsoft.Xna.Framework.GameTime gameTime)
        {

            keyboardState = UpdateVelocity(keyboardState);
            base.Update(keyboardState, gameTime);
        }

        private KeyboardState UpdateVelocity(KeyboardState keyboardState)
        {
            var keyDictionary = new Dictionary<Keys, Vector2>
            {
                {Keys.Left, new Vector2(-1, 0)},
                {Keys.Right, new Vector2(1, 0)},
                {Keys.Up, new Vector2(0, -1)},
                {Keys.Down, new Vector2(0, 1)}
            };

            var velocity = Vector2.Zero;
            foreach (var keypress in keyboardState.GetPressedKeys())
            {
                Vector2 value = new Vector2();
                keyDictionary.TryGetValue(keypress, out value);
                if (value != null)
                {
                    velocity += value;
                }
            }

            Velocity = velocity * Speed;
            return keyboardState;
        }
    }
}
