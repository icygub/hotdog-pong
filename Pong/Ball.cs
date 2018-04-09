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
        private float _rotation = 0f;

        public Ball(Texture2D texture, Vector2 location, Rectangle gameBoundaries) : base(texture, location, gameBoundaries) { }

        protected override void CheckBounds()
        {
            //if (Location.Y >= (gameBoundaries.Height - texture.Height/2) || (Location.Y - texture.Height/2 ) <= 0) {// the texture.Height divisions are for top and bottom bounds while ball is spinning

            if (Location.Y >= (gameBoundaries.Height - texture.Height) || Location.Y <= 0) { // bottom || top........
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
                var newVelocity = new Vector2(15f, _attachedToPaddle.Velocity.Y);
                Velocity = newVelocity;
                _attachedToPaddle = null;
            }

            // keeps "ball" attached to "paddle" while ball has not been fired
            else if (_attachedToPaddle != null)
            {
                Location.Y = _attachedToPaddle.Location.Y;
                Location.X = _attachedToPaddle.Location.X + this.Height;
            }

            bool isMovingRight = Velocity.X > 0;
            var paddle = isMovingRight ? gameObjects.ComputerPaddle : gameObjects.PlayerPaddle;
            //if (this.BoundingBox.Intersects(paddle.BoundingBox) && IsTouchingTopOf(paddle) || IsTouchingBottomOf(paddle))
            //    Velocity = new Vector2(Velocity.X, -Velocity.Y);
            if (isMovingRight && this.BoundingBox.Intersects(gameObjects.ComputerPaddle.BoundingBox))// && IsTouchingLeftOf(gameObjects.ComputerPaddle))
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
            else if (!isMovingRight && this.BoundingBox.Intersects(gameObjects.PlayerPaddle.BoundingBox))// && IsTouchingRightOf(gameObjects.PlayerPaddle))
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
            //if (this.Velocity.X > 0) { // if ball is moving right
            //    if (IsTouchingLeftOf(gameObjects.ComputerPaddle)) // deflect off side of paddle
            //        Velocity = new Vector2(-Velocity.X, Velocity.Y);
            //    else // if touching top and bottom of paddle, Velocity.Y is reversed
            //        Velocity = (IsTouchingTopOf(gameObjects.ComputerPaddle) || IsTouchingBottomOf(gameObjects.ComputerPaddle)) ? new Vector2(Velocity.X, -Velocity.Y) : Velocity;
            //}
            //else if (this.Velocity.X < 0) { // if ball is moving left
            //    if (IsTouchingRightOf(gameObjects.PlayerPaddle)) // deflect off side of paddle
            //        Velocity = new Vector2(-Velocity.X, Velocity.Y);
            //    else // if touching top and bottom of paddle, Velocity.Y is reversed
            //        Velocity = (IsTouchingTopOf(gameObjects.PlayerPaddle) || IsTouchingBottomOf(gameObjects.PlayerPaddle)) ? new Vector2(Velocity.X, -Velocity.Y) : Velocity;
            //}

            // makes the ball constantly rotate
            //_rotation += .04f;

            base.Update(gameTime, gameObjects);
        }

        public void AttachTo(Paddle paddle)
        {          
            _attachedToPaddle = paddle;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, Location); // without spinning
            //spriteBatch.Draw(this.texture, Location, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0f); // with spinning
            
        }

        #region Collision Detecting
        private bool IsTouchingLeftOf(Paddle paddle) {
            return this.BoundingBox.Right + this.Velocity.X > paddle.BoundingBox.Left &&
                   this.BoundingBox.Top > paddle.BoundingBox.Bottom &&
                   this.BoundingBox.Bottom < paddle.BoundingBox.Top;          
        }

        private bool IsTouchingRightOf(Paddle paddle) {
            return this.BoundingBox.Left + this.Velocity.X < paddle.BoundingBox.Right &&
                   this.BoundingBox.Top > paddle.BoundingBox.Bottom &&
                   this.BoundingBox.Bottom < paddle.BoundingBox.Top;
        }

        private bool IsTouchingTopOf(Paddle paddle) {
            return this.BoundingBox.Bottom + this.Velocity.Y < paddle.BoundingBox.Top &&
                   this.BoundingBox.Right > paddle.BoundingBox.Left &&
                   this.BoundingBox.Left < paddle.BoundingBox.Right; // actually, is this line necessary?
        }

        private bool IsTouchingBottomOf(Paddle paddle) {
            return this.BoundingBox.Top + this.Velocity.Y > paddle.BoundingBox.Bottom &&
                   this.BoundingBox.Right > paddle.BoundingBox.Left &&
                   this.BoundingBox.Left < paddle.BoundingBox.Right; // actually, is this line necessary?
        }
        #endregion
    }
}