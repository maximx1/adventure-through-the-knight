using System;
using System.Collections.Generic;
using System.Linq;
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
using adventure_through_the_knight.Input;
using adventure_through_the_knight.Output.Character;
using adventure_through_the_knight.Output.Walls;
using adventure_through_the_knight.Utilities.Error_Log;
using adventure_through_the_knight.Utilities.Settings;
#endregion

namespace adventure_through_the_knight
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;

        //Displayable entities.
        private SpriteBatch spriteBatch;
        private SpriteFont DebugText;
        private Player player;

        public IOSettings System_wideSettings;
        public WallManager MainWallManager;

        //System wide Settings


        public Game1()
        {
            System_wideSettings = new IOSettings("local/GameSettings.xml");
            graphics = new GraphicsDeviceManager(this);                                 //The object containing the 
            Content.RootDirectory = "Content";                                          //The root of the content after the pipeline.
            graphics.PreferredBackBufferHeight = System_wideSettings.WindowHeight;      //The Height of the Window
            graphics.PreferredBackBufferWidth = System_wideSettings.WindowWidth;        //The Width of the Window
            graphics.IsFullScreen = System_wideSettings.IsFullScreen;				    //See what window size we need to use.
            this.IsMouseVisible = true;                                                 //Make sure the mouse is invisible. Don't use once we get new textures for it.
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            //TV Manafacturers use black in between frames to help smooth out frame intervals.
            MainWallManager = new WallManager(graphics.GraphicsDevice);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(Content.Load<Texture2D>("player1"), new Vector2(200, 200), graphics.GraphicsDevice.Viewport.Bounds, ref System_wideSettings);    //Player 1
            DebugText = Content.Load<SpriteFont>(@"SpriteFonts\DebugOverlay");	//In Mono make sure that spritefonts are ".xnb"
            //player.SetWallManager(MainWallManager);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            //Safely closes the game.
            //We should possible add some more functions to close databases and store last minute saves.
            if (player.CloseGame)
            {
                Exit();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Start drawing all characters, objects, and fonts.
            spriteBatch.Begin();

            player.Draw(spriteBatch);

            spriteBatch.DrawString(DebugText, "Sprite facing direction: " + player.SPRITE_DIRECTION.ToString() +
                                               " | Sprite moving direction: " + player.SPRITE_MOVEMENT_DIRECTION,
                                               Vector2.Zero, Color.White);

            spriteBatch.End();

            //Output loaded frame to the screen.
            base.Draw(gameTime);
        }
    }
}