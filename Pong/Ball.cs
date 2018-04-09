using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong;

namespace Pong {
    public class Ball : Sprite {

        private Paddle _attachedToPaddle;
        public Vector2 Origin;
        private float _rotation = 0f;
        private bool lastHitWasRight = false;

        public Ball(Texture2D texture, Vector2 location, Rectangle gameBoundaries) : base(texture, location, gameBoundaries) { }

        protected override void CheckBounds() {
            // this is for when the ball is spinning
            //if (Location.Y >= (gameBoundaries.Height - texture.Height/2) || (Location.Y - texture.Height/2 ) <= 0) { // the texture.Height divisions are for top and bottom bounds while ball is spinning

            if (Location.Y >= (gameBoundaries.Height - Texture.Height) || Location.Y <= 0) { // bottom || top........
                var newVelocity = new Vector2(Velocity.X, -Velocity.Y);
                Velocity = newVelocity;
            }

            // this will make ball deflect off left and right of window
            //if (Location.X <= 0 || Location.X >= (gameBoundaries.Width - texture.Width))
            //{
            //    var newVelocity = new Vector2(-Velocity.X, Velocity.Y);
            //    Velocity = newVelocity;
            //}
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects) {
            // fires the "ball"
            if (gameObjects.TouchInput.Tap && _attachedToPaddle != null) {
                var newVelocity = new Vector2(4f, _attachedToPaddle.Velocity.Y - 1.5f);
                Velocity = newVelocity;
                _attachedToPaddle = null;
            }

            // keeps "ball" attached to "paddle" while ball has not been fired
            else if (_attachedToPaddle != null) {
                Location.Y = _attachedToPaddle.Location.Y;
                Location.X = _attachedToPaddle.Location.X + this.Height;
            }

#region Collision Response
            bool isMovingRight = Velocity.X > 0;
            bool isMovingUp = Velocity.Y < 0;
            var paddle = isMovingRight ? gameObjects.ComputerPaddle : gameObjects.PlayerPaddle;

            if (isMovingRight && !lastHitWasRight) {
                if ((isMovingUp && IsTouchingBottomOf(paddle)) || (!isMovingUp && IsTouchingTopOf(paddle))) { // if touching top or bottom of paddle, ball Velocity.Y is reversed               
                    Velocity = new Vector2(Velocity.X, -Velocity.Y);
                    lastHitWasRight = true;
                }       
                else if (IsTouchingLeftOf(paddle)) {// deflect off side of paddle
                    Velocity = new Vector2(-Velocity.X, Velocity.Y);
                    lastHitWasRight = true;
                }
            }
            else if (!isMovingRight && lastHitWasRight) {
                if ((isMovingUp && IsTouchingBottomOf(paddle)) || (!isMovingUp && IsTouchingTopOf(paddle))) { // if touching top or bottom of paddle, ball Velocity.Y is reversed               
                    Velocity = new Vector2(Velocity.X, -Velocity.Y);
                    lastHitWasRight = false;
                }               
                else if (IsTouchingRightOf(paddle)) {// deflect off side of paddle
                    Velocity = new Vector2(-Velocity.X, Velocity.Y);
                    lastHitWasRight = false;
                }              
            }
#endregion
            // makes the ball constantly rotate
            //_rotation += .04f;

            base.Update(gameTime, gameObjects);
        }

        public void AttachTo(Paddle paddle) {          
            _attachedToPaddle = paddle;
            lastHitWasRight = false;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Location); // without spinning
            //spriteBatch.Draw(this.texture, Location, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0f); // with spinning           
        }

        #region Collision Detecting
        private bool IsTouchingLeftOf(Paddle paddle) {
            return this.BoundingBox.Right  >= paddle.BoundingBox.Left &&
                   this.BoundingBox.Top < paddle.BoundingBox.Bottom &&
                   this.BoundingBox.Bottom > paddle.BoundingBox.Top;          
        }

        private bool IsTouchingRightOf(Paddle paddle) {
            return this.BoundingBox.Left <= paddle.BoundingBox.Right &&
                   this.BoundingBox.Top < paddle.BoundingBox.Bottom &&
                   this.BoundingBox.Bottom > paddle.BoundingBox.Top;
        }

        private bool IsTouchingTopOf(Paddle paddle) {
            return this.BoundingBox.Bottom >= paddle.BoundingBox.Top &&
                   this.BoundingBox.Bottom < paddle.BoundingBox.Bottom && // prevents ball from deflecting without touching paddle
                   this.BoundingBox.Right > paddle.BoundingBox.Left &&
                   this.BoundingBox.Left < paddle.BoundingBox.Right;
        }

        private bool IsTouchingBottomOf(Paddle paddle) {
            return this.BoundingBox.Top <= paddle.BoundingBox.Bottom &&
                   this.BoundingBox.Top > paddle.BoundingBox.Top && // prevents ball from deflecting without touching paddle
                   this.BoundingBox.Right > paddle.BoundingBox.Left &&
                   this.BoundingBox.Left < paddle.BoundingBox.Right; 
        }
        #endregion
    }
}