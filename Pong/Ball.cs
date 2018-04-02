using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Ball : Sprite
    {

        private Paddle attachedToPaddle;

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
            if(Location.Y >= (gameBoundaries.Height - texture.Height) || Location.Y <= 0)
            {
                var newVelocity = new Vector2(Velocity.X, -Velocity.Y);
                Velocity = newVelocity;
            }
            if(Location.X <= 0 || Location.X >= (gameBoundaries.Width - texture.Width))
            {
                var newVelocity = new Vector2(-Velocity.X, Velocity.Y);
                Velocity = newVelocity;
            }
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            // fires the "ball"
            if (gameObjects.TouchInput.Tap && attachedToPaddle != null)
            {
                var newVelocity = new Vector2(15f, attachedToPaddle.Velocity.Y);
                Velocity = newVelocity;
                attachedToPaddle = null;
                
            }

            // keeps "ball" attached to "paddle"
            if (attachedToPaddle != null)
            {
                Location.Y = attachedToPaddle.Location.Y;
                Location.X = attachedToPaddle.Location.X + attachedToPaddle.Width;
            }


            base.Update(gameTime);
        }

        public void AttachTo(Paddle paddle)
        {
            attachedToPaddle = paddle;
            Location.X = attachedToPaddle.Width;
        }
    }
}