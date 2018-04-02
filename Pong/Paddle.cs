using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Pong
{
    public class Paddle : Sprite
    {
        private readonly Rectangle screenBounds;

        public Paddle(Texture2D texture, Vector2 location, Rectangle screenBounds) : base(texture, location)
        {
            this.screenBounds = screenBounds;
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            
            //move up
            if (gameObjects.TouchInput.Up)
            {
                Velocity = new Vector2(0, -17f);
            }

            if (gameObjects.TouchInput.Down)
            {
                Velocity = new Vector2(0, 17f);
            }

            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected override void CheckBounds()
        {
            Location.Y = MathHelper.Clamp(Location.Y, 0, screenBounds.Height - texture.Height);
        }
    }

    public abstract class Sprite
    {
        public Texture2D texture;
        public Vector2 Location;
        public int Width { get => texture.Width; }
        public int Height { get => texture.Height; }

        public Vector2 Velocity { get; protected set; }
        //private Vector2 _velocity;


        protected Sprite(Texture2D texture, Vector2 location)
        {
            this.texture = texture;
            Location = location;
            Velocity = Vector2.Zero;
        }

        

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Location);
        }

        public virtual void Update(GameTime gameTime, GameObjects gameObjects)
        {
            Location += Velocity;
            CheckBounds();
        }

        public virtual void Update(GameTime gameTime)
        {
            Location += Velocity;
            CheckBounds();
        }

        protected abstract void CheckBounds();
        
            
        
    }

        
    }