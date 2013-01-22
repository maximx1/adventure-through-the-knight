using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using adventure_through_the_knight.Input;
using adventure_through_the_knight.Output.Character;
using adventure_through_the_knight.Utilities.Error_Log;
using adventure_through_the_knight.Output.Walls;

namespace adventure_through_the_knight
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public WallManager MainWallManager;

        SpriteFont DebugText;

        Color ScreenColor;
        Player player;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;       //The Height of the Window
            graphics.PreferredBackBufferWidth = 800;        //The Width of the Window
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;				    //Turn off full screen.
            this.IsMouseVisible = true;
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
            ScreenColor = Color.Black;
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
            player = new Player(Content.Load<Texture2D>("player1"), new Vector2(200, 200), graphics.GraphicsDevice.Viewport.Bounds);    //Player 1
            player.SetSpriteMap(@"..\..\..\Output\\Character\\PlayerMap.xml");
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
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ScreenColor);

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