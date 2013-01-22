using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#region XNA framework usings
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion
#region Project usings
using adventure_through_the_knight.Output.Base;
using adventure_through_the_knight.Input;
#endregion

namespace adventure_through_the_knight.Output.Menu_Overlay
{
    class MainMenu : Sprite
    {
        public bool CloseGame { get; set; }                         //A bool to allow the game to quit when the update loop occurs.
        private InputController Input;                              //The game's input manager
        public InputController.InputDeviceType CurrentInputType;    //The players input type choice
                
        /// <summary>
        /// Constructor creates an instance of a Pause Menu
        /// </summary>
        /// <param name="texture">The texture of the Pause menu background</param>
        /// <param name="position">The inital position of the overlay.</param>
        /// <param name="movementBounds">The available bounds. (Doesn't matter here.</param>
        public MainMenu(Texture2D texture, Vector2 position, Rectangle movementBounds, ref InputController.InputDeviceType CurrentInputType)
            : base(texture, position, movementBounds, 1, 4, 11, 1, 1, 1, 1, 1)
        {
            this.CloseGame = false;
            this.Speed = 60;
            this.CurrentInputType = InputController.InputDeviceType.KEYBOARD;
            this.Input = new InputController(CurrentInputType);
        }

        /// <summary>
        ///  Update the Pause menu every loop.
        /// </summary>
        /// <param name='gameTime'>
        /// XNA game time.
        /// </param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Update the game input controller
            Input.GetState();
            if (Input.PAUSE)
            {
                CloseGame = true;
                return;
            }
        }
    }
}
