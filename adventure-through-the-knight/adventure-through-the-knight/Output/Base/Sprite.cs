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

namespace adventure_through_the_knight.Output
{
    class Sprite
    {
        public float Width { get { return texture.Width; } }        //The Width of the texture.
        public float Height { get { return texture.Height; } }      //The Height of the texture.
        public readonly int rows;                                   //The number of available animations.
        public readonly int columns;                                //The number of animations per row.
        public enum Direction { Right, Up_Right, Up, Up_Left, Left, Down_Left, Down, Down_Right, Still };   //Lists the available directions. Do not change order.
        
        private Vector2 position;                                   //Location of the sprite in the world.
        private readonly Texture2D texture;                         //The texture for the sprite.
        private readonly Rectangle movementBounds;                  //The available movement bounds.
        private readonly double framesPerSecond;                    //The speed of how fast the frames should update.
        private int totalFrames;                                    //The number of frames available for an animation.
        private int currentFrame = 1;                               //The current frame of animation.
        private double timeSinceLastFrame;                          //A counter to determine if animation should advance.
        private Direction SpriteDirection;                          //The direction the Sprite is facing
        
        protected Dictionary<Direction, int> spriteSheetRows;       //Dictionary holding the different animations of the character.
        protected Vector2 Velocity { get; set; }                    //The direction of the sprite.
        protected Vector2 SpriteDirectionVector;                    //The direction of the Sprite.
        protected bool moved;                                       //Test if input says move.
        protected int health;                                       //Available health for the sprite.

        //The available movement bounds for the player.
        public Rectangle BoundingBox
        {
            get { return CreateBoundingBoxFromPosition(position); }
        }

        /// <summary>
        /// The bounds that the character is capable of moving.
        /// </summary>
        /// <param name="position">The actual position of the Character.</param>
        /// <returns></returns>
        private Rectangle CreateBoundingBoxFromPosition(Vector2 position)
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)Width / columns, (int)Height / rows);
        }

        //The Magnitude of the velocity.
        protected float Speed { get; set; }

        /// <summary>
        /// Constructor. Builds the characters and visual objects.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="movementBounds"></param>
        public Sprite(Texture2D texture, Vector2 position, Rectangle movementBounds)
            : this(texture, position, movementBounds, 1, 1, 1, 1, 1, 1, 1, 1)
        {

        }

		/// <summary>
		/// Initializes a new instance of the <see cref="adventure_through_the_knight.Output.Sprite"/> class.
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
		/// <param name='rows'>
		/// Rows.
		/// </param>
		/// <param name='columns'>
		/// Columns.
		/// </param>
		/// <param name='framesPerSecond'>
		/// Frames per second.
		/// </param>
        public Sprite(Texture2D texture, Vector2 position, Rectangle movementBounds, int rows, int columns,
            double framesPerSecond, int upRow, int downRow, int leftRow, int rightRow, int stillRow)
        {
            this.texture = texture;
            this.position = position;
            this.movementBounds = movementBounds;
            this.rows = rows;
            this.columns = columns;
            this.framesPerSecond = framesPerSecond;
            this.totalFrames = rows * columns;
            this.SpriteDirectionVector = Vector2.Zero;
        }

		/// <summary>
		/// Draw the specified spriteBatch.
		/// </summary>
		/// <param name='spriteBatch'>
		/// Sprite batch.
		/// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            var sourceRectangle = SpriteSheetCalculator.CalculateSourceRect((int)Width, (int)Height, columns, rows, currentFrame, moved);
            var destinationRectangle = SpriteSheetCalculator.CalculateDestinationRect(position, sourceRectangle);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

		/// <summary>
		/// Blocked the specified newPosition.
		/// </summary>
		/// <param name='newPosition'>
		/// If set to <c>true</c> new position.
		/// </param>
        private bool Blocked(Vector2 newPosition)
        {
            var boundingBox = CreateBoundingBoxFromPosition(newPosition);
            return !movementBounds.Contains(boundingBox);
        }

		/// <summary>
		/// Update the specified gameTime.
		/// </summary>
		/// <param name='gameTime'>
		/// Game time.
		/// </param>
        public virtual void Update(GameTime gameTime)
        {   
            var newPosition = position + (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds) * Speed;
            UpdateDirection();
            if (Blocked(newPosition))       //Check if at the bounds of the Game.
            {
                return;
            }
            else                            //Check if there was actually movement.
            {
                if (moved)
                    UpdateAnimation(gameTime);
                //TODO: Add else to represent breathing animation.
            }

            //Update positions for the next move.
            position = newPosition;
        }

		/// <summary>
		/// Updates the animation.
		/// </summary>
		/// <param name='gameTime'>
		/// Game time.
		/// </param>
        private void UpdateAnimation(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrame > SecondsBetweenFrames())
            {
				timeSinceLastFrame = 0;
                currentFrame++;
            }
            if (currentFrame == totalFrames)
                currentFrame = 0;               //Off by one
        }

		/// <summary>
		/// Secondses the between frames.
		/// </summary>
		/// <returns>
		/// The between frames.
		/// </returns>
        private double SecondsBetweenFrames()
        {
            return 1 / framesPerSecond;
        }

        /// <summary>
        /// Converts the vector into a set direction.
        /// </summary>
        private void UpdateDirection()
        {
            //Stop if there is no motion
            if (SpriteDirectionVector == Vector2.Zero)
            {
                this.SpriteDirection = Direction.Still;
                return;
            }

            float angleFromVector = Utilities.Math.UnitConverter.RadToDegrees((float)Math.Atan2(-SpriteDirectionVector.Y, SpriteDirectionVector.X));

            //Adjust the angle to register direction right with only one 45 degree increment.
            angleFromVector += 22.5f;
            
            if (angleFromVector < 0)
                angleFromVector += 360f;

            for(int i = 0; i < 8; i++)
            {
                if(InRange(angleFromVector, (float)i * 45, (float)i * 45 + 45))
                {
                    SpriteDirection = (Direction)i;
                    return;
                }
            }
           
            //default to no motion
            SpriteDirection = Direction.Still;
        }

        /// <summary>
        /// Test if a number is within the range of another number.
        /// </summary>
        /// <param name="TestVariable">True if the value is within range</param>
        /// <param name="Min">The Minimum number inclusive.</param>
        /// <param name="Max">The Maximum number non-inclusive</param>
        /// <returns></returns>
        public bool InRange(float TestVariable, float Min, float Max)
        {
            return TestVariable < Max && TestVariable >= Min ? true : false; 
        }

        /// <summary>
        /// Gets the player's direction.
        /// </summary>
        public Direction SPRITE_DIRECTION { get { return SpriteDirection; } }
    }
}