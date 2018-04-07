using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong;

namespace Pong
{
    public class Ball : Sprite
    {

        private Paddle attachedToPaddle;
        public Vector2 Origin;

        public Ball(Texture2D texture, Vector2 location) : base(texture, location)
        {
            //attachedToPaddle = null;
        }

        public Ball(Texture2D texture, Vector2 location, Rectangle gameBoundaries) : base(texture, location, gameBoundaries)
        {
            //attachedToPaddle = null;
        }

        protected override void CheckBounds()
        {
            if (Location.Y >= (gameBoundaries.Height - texture.Height) || Location.Y <= 0)
            {
                var newVelocity = new Vector2(Velocity.X, -Velocity.Y);
                Velocity = newVelocity;
            }
            //if (Location.X <= 0 || Location.X >= (gameBoundaries.Width - texture.Width))
            //{
            //    var newVelocity = new Vector2(-Velocity.X, Velocity.Y);
            //    Velocity = newVelocity;
            //}
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            // fires the "ball"
            if (gameObjects.TouchInput.Tap && attachedToPaddle != null)
            {
                var newVelocity = new Vector2(15f, attachedToPaddle.Velocity.Y * 1.2f);
                Velocity = newVelocity;
                attachedToPaddle = null;
            }

            // keeps "ball" attached to "paddle" while ball has not been fired
            else if (attachedToPaddle != null)
            {
                Location.Y = attachedToPaddle.Location.Y;
                Location.X = attachedToPaddle.Location.X + this.Height;
            }         

            // this makes the ball deflect off the sides of the paddles
            else if (BoundingBox.Intersects(gameObjects.PlayerPaddle.BoundingBox) || BoundingBox.Intersects(gameObjects.ComputerPaddle.BoundingBox))
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);              
            }

            base.Update(gameTime, gameObjects);
        }

        public void AttachTo(Paddle paddle)
        {          
            attachedToPaddle = paddle;
        }
    }
}