using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong;

namespace Pong
{
    public class Ball : Sprite
    {

        private Paddle _attachedToPaddle;
        public Vector2 Origin;
        private float _rotation;

        public Ball(Texture2D texture, Vector2 location, Rectangle gameBoundaries) : base(texture, location, gameBoundaries)
        {
            _rotation = 0f;
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
            if (gameObjects.TouchInput.Tap && _attachedToPaddle != null)
            {
                var newVelocity = new Vector2(15f, _attachedToPaddle.Velocity.Y * 1.2f);
                Velocity = newVelocity;
                _attachedToPaddle = null;
            }

            // keeps "ball" attached to "paddle" while ball has not been fired
            else if (_attachedToPaddle != null)
            {
                Location.Y = _attachedToPaddle.Location.Y + _attachedToPaddle.Height/2;
                Location.X = _attachedToPaddle.Location.X + this.Height;
            }         

            // this makes the ball deflect off the sides of the paddles
            else if (BoundingBox.Intersects(gameObjects.PlayerPaddle.BoundingBox) || BoundingBox.Intersects(gameObjects.ComputerPaddle.BoundingBox))
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);              
            }

            _rotation += .04f;
            base.Update(gameTime, gameObjects);
        }

        public void AttachTo(Paddle paddle)
        {          
            _attachedToPaddle = paddle;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            //spriteBatch.Draw(texture, Location); // without spinning
            spriteBatch.Draw(this.texture, Location, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0f); // with spinning
        }
    }
}