using System;
using Android.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Pong {
    public class Game1 : Game {   
        SpriteBatch           spriteBatch;
        private GameObjects   gameObjects;
        private Texture2D     plaindogPaddleTexture;
        private Texture2D     combodogPaddleTexture;
        private Texture2D     ballTexture;
        private Paddle        playerPaddle;
        private Paddle        computerPaddle;
        private Ball          ball;
        private Score         score;

        public Game1() {
            GraphicsDeviceManager graphics;
            graphics = new GraphicsDeviceManager(this);     
            graphics.IsFullScreen = true;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            Content.RootDirectory = "Content";

            gameObjects = new GameObjects();
        }

        protected override void Initialize() {           
            TouchPanel.EnabledGestures = GestureType.VerticalDrag | GestureType.Flick | GestureType.Tap;          
            base.Initialize();
        }

        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var gameBoundaries = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            combodogPaddleTexture = Content.Load<Texture2D>("graphics/combo_dog");
            plaindogPaddleTexture = Content.Load<Texture2D>("graphics/plain_hotdog");
            ballTexture           = Content.Load<Texture2D>("graphics/weiner");

            playerPaddle = new Paddle(combodogPaddleTexture, Vector2.Zero, gameBoundaries, PlayerTypes.Human);
            var computerPaddleLocation = new Vector2(gameBoundaries.Width - plaindogPaddleTexture.Width, 0);
            computerPaddle = new Paddle(plaindogPaddleTexture, computerPaddleLocation, gameBoundaries, PlayerTypes.Computer);

            ball = new Ball(ballTexture, Vector2.Zero, gameBoundaries);
            ball.Origin = new Vector2(ball.Texture.Width / 2, ball.Texture.Height / 2);
            ball.AttachTo(playerPaddle);

            score = new Score(Content.Load<SpriteFont>("fonts/HighScoreFont"), gameBoundaries);

            gameObjects = new GameObjects { PlayerPaddle = playerPaddle, ComputerPaddle = computerPaddle, Ball = ball, Score = score };
        }

        protected override void UnloadContent() {
            throw new NotImplementedException();
        }

        protected override void Update(GameTime gameTime) {
            gameObjects.TouchInput = new TouchInput();
            GetTouchInput();

            playerPaddle.Update(gameTime, gameObjects);
            computerPaddle.Update(gameTime, gameObjects);
            ball.Update(gameTime, gameObjects);
            score.Update(gameTime, gameObjects);
            //ballOrigin = new Vector2(ballRectangle.Width / 2, ballRectangle.Height / 2); // used for ball spinning
            base.Update(gameTime);
        }

        private void GetTouchInput() {
            while(TouchPanel.IsGestureAvailable) {
                var gesture = TouchPanel.ReadGesture();
                if(gesture.Delta.Y > 0) {
                    gameObjects.TouchInput.Down = true;
                }
                if(gesture.Delta.Y < 0) {
                    gameObjects.TouchInput.Up = true;
                }
                if(gesture.Delta.Y == 0) {
                    gameObjects.TouchInput.Tap = true;
                }
            }
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            playerPaddle.Draw(spriteBatch);
            computerPaddle.Draw(spriteBatch);         
            ball.Draw(spriteBatch);
            score.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
