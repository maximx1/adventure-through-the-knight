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

namespace adventure_through_the_knight.Output
{
    class Sprite
    {
        private Vector2 position;
        private readonly Texture2D texture;
        private readonly Rectangle movementBounds;
        protected Vector2 Velocity { get; set; }
        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }
        public readonly int rows;
        public readonly int columns;
        private readonly double framesPerSecond;
        private int totalFrames;
        private double timeSinceLastFrame;
        private int currentFrame = 1;

        public Rectangle BoundingBox
        {
            get { return CreateBoundingBoxFromPosition(position); }
        }

        private Rectangle CreateBoundingBoxFromPosition(Vector2 position)
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)Width, (int)Height);
        }

        protected float Speed { get; set; }

        public Sprite(Texture2D texture, Vector2 position, Rectangle movementBounds)
            : this(texture, position, movementBounds, 1, 1, 1)
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
        public Sprite(Texture2D texture, Vector2 position, Rectangle movementBounds, int rows, int columns, double framesPerSecond)
        {
            this.texture = texture;
            this.position = position;
            this.movementBounds = movementBounds;
            this.rows = rows;
            this.columns = columns;
            this.framesPerSecond = framesPerSecond;
            this.totalFrames = rows * columns;
        }

		/// <summary>
		/// Draw the specified spriteBatch.
		/// </summary>
		/// <param name='spriteBatch'>
		/// Sprite batch.
		/// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            var sourceRectangle = SpriteSheetCalculator.CalculateSourceRect((int)Width, (int)Height, columns, rows, currentFrame);
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
            UpdateAnimation(gameTime);
            var newPosition = position + (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds) * Speed;
            if (Blocked(newPosition))
            {
                return;
            }
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
                currentFrame = 1;
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
    }
}