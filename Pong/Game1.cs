using System;
using Android.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private Texture2D plaindogPaddleTexture;
        private Texture2D combodogPaddleTexture;
        private Texture2D ballTexture;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private GameObjects gameObjects;

        private Paddle playerPaddle;
        private Paddle computerPaddle;
        private Ball ball;
        private Vector2 ballOrigin;
        private Vector2 ballPosition;
        private Rectangle ballRectangle;
        private float rotation;
        private Vector2 ballLocation;
        private Rectangle sourceRectangle;
        private Score score;
        //private Texture2D hotdog;
        //private Texture2D hotdog2;
        //private TouchPanel touchPanel;


        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.IsFullScreen = true;
            //graphics.PreferredBackBufferWidth = 800;
            //graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            gameObjects = new GameObjects();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            TouchPanel.EnabledGestures = GestureType.VerticalDrag | GestureType.Flick | GestureType.Tap;
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
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
            ball.AttachTo(playerPaddle);

            score = new Score(Content.Load<SpriteFont>("fonts/HighScoreFont"), gameBoundaries);

            //gameObjects = new GameObjects { PlayerPaddle = playerPaddle, ComputerPaddle = computerPaddle, Ball = ball};
            gameObjects = new GameObjects { PlayerPaddle = playerPaddle, ComputerPaddle = computerPaddle, Ball = ball, Score = score };

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            gameObjects.TouchInput = new TouchInput(); //game crashes when this line runs
            GetTouchInput();

            playerPaddle.Update(gameTime, gameObjects);
            computerPaddle.Update(gameTime, gameObjects);
            ball.Update(gameTime, gameObjects);

            //ballRectangle = new Rectangle((int)ballPosition.X, (int)ballPosition.Y,
                //ball.texture.Width, ball.texture.Height);
            //ballOrigin = new Vector2(ballRectangle.Width / 2, ballRectangle.Height / 2);
            // rotation += .1f; //spinning
            base.Update(gameTime);
        }

        private void GetTouchInput()
        {
            while(TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                if(gesture.Delta.Y > 0)
                {
                    gameObjects.TouchInput.Down = true;
                }
                if(gesture.Delta.Y < 0)
                {
                    gameObjects.TouchInput.Up = true;
                }
                if(gesture.Delta.Y == 0)
                {
                    gameObjects.TouchInput.Tap = true;
                }


            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            playerPaddle.Draw(spriteBatch);
            computerPaddle.Draw(spriteBatch);
            //ballLocation = new Vector2(playerPaddle.texture.Width + (ball.texture.Height / 2), 0); //spinning
            //sourceRectangle = new Rectangle(0, 0, ball.Width, ball.Height); //spinning
            //ballOrigin = new Vector2(ballRectangle.Width / 2, ballRectangle.Height / 2); //spinning
            //spriteBatch.Draw(ball.texture, ballLocation, sourceRectangle, Color.White, rotation, ballOrigin, 1.0f, SpriteEffects.None, 1); //spinning
            //ball.AttachTo(playerPaddle); //spinning
            ball.Draw(spriteBatch);
            //ball.Draw(spriteBatch, ballPosition, null, Color.Beige, rotation, ballOrigin, 1f, SpriteEffects.None, 0); //spinning
            //spriteBatch.Draw(plaindogTexture, ballPosition, null, Color.White, rotation, ballOrigin, 1f, SpriteEffects.None, 0); //spinning
            //spriteBatch.Draw(hotdog, position: Vector2.Zero); //spinning

            score.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

 
}
