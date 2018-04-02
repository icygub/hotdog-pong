using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Pong
{
    public class Paddle : Sprite
    {
        public Paddle(Texture2D texture, Vector2 location) : base(texture, location)
        {
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            
            //move up
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || gameObjects.TouchInput.Up)
            {
                velocity = new Vector2(0, -17f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || gameObjects.TouchInput.Down)
            {
                velocity = new Vector2(0, 17f);
            }

            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }


    }

    public class Sprite
    {
        private readonly Texture2D texture;
        private Vector2 location;
        protected Vector2 velocity = Vector2.Zero;

        public Sprite(Texture2D texture, Vector2 location)
        {
            this.texture = texture;
            this.location = location;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location);
        }

        public virtual void Update(GameTime gameTime, GameObjects gameObjects)
        {
            location += velocity;
            
        }

    }

        
    }