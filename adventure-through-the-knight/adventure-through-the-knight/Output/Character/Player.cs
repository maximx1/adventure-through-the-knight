﻿using System;
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

//using adventure_through_the_knight.Output.Base;
using adventure_through_the_knight.Input;

namespace adventure_through_the_knight.Output.Character
{
    class Player : Sprite
    {
        private bool moved;

		InputController Input;

		/// <summary>
		/// Initializes a new instance of the <see cref="adventure_through_the_knight.Output.Character.Player"/> class.
		/// </summary>
		/// <param name='texture'>
		/// Texture.
		/// </param>
		/// <param name='position'>
		/// Position.
		/// </param>
		/// <param name='movementBounds'>
		/// Movement bounds.
		/// </param>
        public Player(Texture2D texture, Vector2 position, Rectangle movementBounds)
            : base(texture, position, movementBounds, 1, 4, 11)
        {
            this.Speed = 60;
			this.Input = new InputController(InputController.InputDeviceType.KEYBOARD);
        }

		/// <summary>
		///  Update the specified gameTime. 
		/// </summary>
		/// <param name='gameTime'>
		///  Game time. 
		/// </param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
			//Update the game input controller
			Input.GetState();

			//Update the character movement
            UpdateVelocity();

			/*
			 * This part is uber hacked - won't allow acceleration animations.
			 * Only updates the visuals if the character has actually moved.
			 * TODO: Edit to throw event to accelerate and deccelerate.
			 */
			if(moved)
            	base.Update(gameTime);
        }

		/// <summary>
		/// Updates the character's velocity.
		/// </summary>
        private void UpdateVelocity ()
		{
			//Default to a no movement - blocks the update of the sprite.
			moved = false;

			var velocity = Vector2.Zero;

            var keyDictionary = new Dictionary<G_key.G_KEY, Vector2>
            {
                {G_key.G_KEY.LEFT, new Vector2(-1, 0)},
                {G_key.G_KEY.RIGHT, new Vector2(1, 0)},
                {G_key.G_KEY.UP, new Vector2(0, -1)},
                {G_key.G_KEY.DOWN, new Vector2(0, 1)}
            };


            foreach (var key in keyDictionary)
            {
                if (Input.IsPressed(key.Key))
                    velocity += key.Value;
            }

			if (velocity != Vector2.Zero)
			{
				moved = true;
				//velocity.Normalize ();
			}

            Velocity = velocity;
        }
    }
}