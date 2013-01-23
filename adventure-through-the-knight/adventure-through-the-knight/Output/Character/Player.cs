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

#region Project usings
using adventure_through_the_knight.Output.Base;
using adventure_through_the_knight.Input;
using adventure_through_the_knight.Utilities.Math;
using adventure_through_the_knight.Utilities.Settings;
#endregion

namespace adventure_through_the_knight.Output.Character
{
    class Player : Sprite
    {
        private InputController Input;                              //The game's input manager
        private IOSettings System_wideSettings;
        public InputController.InputDeviceType CurrentInputType;    //The players input type choice

        public bool CloseGame { get; set; }     //A bool to allow the game to quit when the update loop occurs.

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
        public Player(Texture2D texture, Vector2 position, Rectangle movementBounds, ref IOSettings system_wideSettings)
            : base(texture, position, movementBounds, 1, 4, 11, 1, 1, 1, 1, 1)
        {
            this.CloseGame = false;
            this.Speed = 60;
            this.System_wideSettings = system_wideSettings;
            this.CurrentInputType = System_wideSettings.CurrentInputType;
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
			//Update the game input controller
			Input.GetState();
			if(Input.PAUSE)
            {
				CloseGame = true;
				return;
			}

            //Implements run functionality.
            if (Input.LSHIFT)
                Speed = 100;
            else
                Speed = 60;

            //Call method to update the player's direction.
            UpdateSpriteDirectionVector();

			//Update the character movement
            UpdateVelocity();

            base.Update(gameTime);
        }

        /// <summary>
        /// Changes the player's input.
        /// </summary>
        private void ChangeInputType()
        {

            this.System_wideSettings.ChangeInputType();
            this.CurrentInputType = System_wideSettings.CurrentInputType;
            this.Input = new InputController(CurrentInputType);
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

        /// <summary>
        /// Updates the direction of the character based on the mouse of the gamepad.
        /// </summary>
        private void UpdateSpriteDirectionVector()
        {
            if (CurrentInputType == InputController.InputDeviceType.GAMEPAD)
            {
                SpriteDirectionVector = Input.RIGHT_THUMBSTICK;
            }
            else
            {
                SpriteDirectionVector = Vector2.Subtract(Input.MOUSE_POSITION, base.FindCenterOfSprite());
            }
        }
    }
}