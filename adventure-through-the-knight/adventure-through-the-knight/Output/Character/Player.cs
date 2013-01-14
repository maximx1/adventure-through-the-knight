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
            : base(texture, position, movementBounds, 1, 4, 9)
        {
            this.Speed = 100;
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

//			var keyDictionary = new Dictionary<Boolean, Vector2>
//            {
//                {Input.LEFT, new Vector2(-1, 0)},
//                {Input.RIGHT, new Vector2(1, 0)},
//                {Input.UP, new Vector2(0, -1)},
//                {Input.DOWN, new Vector2(0, 1)}
//            };

			if(Input.LEFT)
				velocity += new Vector2(-1, 0);
			if(Input.RIGHT)
				velocity += new Vector2(1, 0);
			if(Input.UP)
				velocity += new Vector2(0, -1);
			if(Input.DOWN)
				velocity += new Vector2(0, 1);


//			foreach (var key in keyDictionary) {
//				if (key.Key)
//					velocity += key.Value;
//			}

			//TODO: Edit the input manager so that the values are all in a list so that we can linq it.

			if (velocity != Vector2.Zero)
			{
				moved = true;
				velocity.Normalize ();
			}

			//if(Input.UP

            //foreach (var keypress in keyboardState.GetPressedKeys())
            //{
                //Vector2 value = new Vector2();


                //keyDictionary.TryGetValue(keypress, out value);
                //if (value != null)
                //{
                    //velocity += value;
                //}
            //}

            Velocity = velocity;
        }
    }
}