using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong {
    public abstract class Sprite {
        private Texture2D texture;
        public Vector2 Location;
        protected readonly Rectangle gameBoundaries;

        public int Width { get => Texture.Width; }
        public int Height { get => Texture.Height; }

        public Rectangle BoundingBox {
            get {
                return new Rectangle((int)Location.X, (int)Location.Y, Width, Height);
            }
        }

        public Vector2 Velocity { get; set; }
        public Texture2D Texture { get => texture; set => texture = value; }

        protected Sprite(Texture2D texture, Vector2 location) {
            this.Texture = texture;
            Location = location;
            Velocity = Vector2.Zero;
        }

        protected Sprite(Texture2D texture, Vector2 location, Rectangle gameBoundaries) {
            this.Texture = texture;
            Location = location;
            this.gameBoundaries = gameBoundaries;
            Velocity = Vector2.Zero;
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Location);
        }

        public virtual void Update(GameTime gameTime, GameObjects gameObjects) {
            Location += Velocity;
            CheckBounds();
        }

        protected abstract void CheckBounds();

    }      
}