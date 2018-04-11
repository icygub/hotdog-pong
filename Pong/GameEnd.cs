using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong {
    public class GameEnd {
        
        private bool winnerExists;
        private PlayerTypes winner;
        private readonly SpriteFont font;
        private readonly Rectangle gameBoundaries;

        public PlayerTypes Winner { get => winner; set => winner = value; }
        public bool WinnerExists { get => winnerExists; set => winnerExists = value; }

        public GameEnd(SpriteFont font, Rectangle gameBoundaries) {
            WinnerExists = false;
            this.font = font;
            this.gameBoundaries = gameBoundaries;
        }
        public void Update(GameTime gameTime, GameObjects gameObjects) {
            if(gameObjects.Score.PlayerScore == 3) {
                WinnerExists = true;
                Winner = PlayerTypes.Human;
            }
            else if(gameObjects.Score.ComputerScore == 3 ) {
                WinnerExists = true;
                Winner = PlayerTypes.Computer;
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            if(WinnerExists) {
                string gameEndText;
                if(winner == PlayerTypes.Human) {
                    gameEndText = "Human wins!";
                }
                else {
                    gameEndText = "Computer wins!";
                }
                var xPosition = (gameBoundaries.Width / 2) - (font.MeasureString(gameEndText).X / 2);
                var position = new Vector2(xPosition, gameBoundaries.Height / 2);
                spriteBatch.DrawString(font, gameEndText, position, Color.Red);
            }
        }
    }

    
}