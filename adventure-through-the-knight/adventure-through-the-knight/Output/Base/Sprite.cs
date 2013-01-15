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

namespace adventure_through_the_knight.Output
{
    abstract class Sprite
    {
        private Vector2 position;
        protected bool moved;
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
        public enum Direction { Left, Right, Up, Down, Still };
        protected Dictionary<Direction, int> spriteSheetRows;

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
            : this(texture, position, movementBounds, 1, 1, 1, 1, 1, 1, 1, 1)
        {

        }

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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var sourceRectangle = SpriteSheetCalculator.CalculateSourceRect((int)Width, (int)Height, columns, rows, currentFrame, moved);
            var destinationRectangle = SpriteSheetCalculator.CalculateDestinationRect(position, sourceRectangle);

           
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        private bool Blocked(Vector2 newPosition)
        {
            var boundingBox = CreateBoundingBoxFromPosition(newPosition);
            return !movementBounds.Contains(boundingBox);
        }

        public virtual void Update(GameTime gameTime)
        {
            var newPosition = position + (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds) * Speed;
            if (Blocked(newPosition))
            {
                moved = false;
                return;
            }
            if (newPosition == position)
            {
                moved = false;
                return;
            }
            moved = true;
            UpdateAnimation(gameTime);
            position = newPosition; 
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrame > SecondsBetweenFrames())
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }
            if (currentFrame == totalFrames)
            {
                currentFrame = 1;
                timeSinceLastFrame = 0;
            }
        }
        private double SecondsBetweenFrames()
        {
            return 1 / framesPerSecond;
        }
    }
}
