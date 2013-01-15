﻿﻿using System;
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
		private InputController Input;                              //The game's input manager
        private InputController.InputDeviceType CurrentInputType;   //The players input type choice

        public bool CloseGame { get; set; }     //A bool to allow the game to quit when the update loop occurs.

        private Direction playerDirection;
        public Direction PlayerDirection { get { return playerDirection; } }
        public Dictionary<Direction, int> SpriteSheetRows
        {
            get { return spriteSheetRows; }
        }


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
            : base(texture, position, movementBounds, 1, 4, 11, 1, 1, 1, 1, 1)
        {
            this.CloseGame = false;
            this.Speed = 60;
            this.CurrentInputType = InputController.InputDeviceType.GAMEPAD;
            this.Input = new InputController(CurrentInputType);
        }

		/// <summary>
		///  Update the specified gameTime. 
		/// </summary>
		/// <param name='gameTime'>
		///  Game time. 
		/// </param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Dictionary<G_key.G_KEY, Vector2> KeyDictionary;
            KeyDictionary = new Dictionary<G_key.G_KEY, Vector2>
            {
                {G_key.G_KEY.LEFT, new Vector2(-1, 0)},
                {G_key.G_KEY.RIGHT, new Vector2(1, 0)},
                {G_key.G_KEY.UP, new Vector2(0, -1)},
                {G_key.G_KEY.DOWN, new Vector2(0, 1)}
            };

            foreach (var key in KeyDictionary)
            {
                if (Input.IsPressed(key.Key))
                {
                    if (key.Key == G_key.G_KEY.LEFT)
                    {
                        playerDirection = Direction.Left;
                    }
                    else if (key.Key == G_key.G_KEY.RIGHT)
                    {
                        playerDirection = Direction.Left;
                    }
                    else if (key.Key == G_key.G_KEY.UP)
                    {
                        playerDirection = Direction.Up;
                    }
                    else if (key.Key == G_key.G_KEY.DOWN)
                    {
                        playerDirection = Direction.Down;
                    }
                }
            }


			//Update the game input controller
			Input.GetState();
            if (Input.PAUSE)
            {
                CloseGame = true;
                return;
            }

            //Implements run function.
            if (Input.LSHIFT)
                Speed = 100;
            else
                Speed = 60;

			//Update the character movement
            UpdateVelocity();

            base.Update(gameTime);
        }

		/// <summary>
		/// Updates the character's velocity.
		/// </summary>
        private void UpdateVelocity ()
		{
			//Default to a no movement - blocks the update of the sprite.
			this.moved = false;

            if (Input.LEFT_THUMBSTICK != Vector2.Zero)
            {
                moved = true;
            }
            
            Velocity = Input.LEFT_THUMBSTICK;
        }
    }
}