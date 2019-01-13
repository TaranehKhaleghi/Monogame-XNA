using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using PROG2370CollisionLibrary;
using AllInOneMono;

namespace TKhaleghiFinalProject
{
    /// <summary>
    /// Finanl Project
    ///  Revision History:
    ///     Taraneh Khaleghi, 2018-12-01: Created
    ///     Taraneh Khaleghi, 2018-12-02: UI Designed
    ///     Taraneh Khaleghi, 2018-12-08: Bug Fixed  
    /// </summary>
    
        // A class that calculate the scores according to hit the objects
    public class ScoreCalculator : DrawableGameComponent
    {
        private Fruits fruit;
        private Spider spider;
        private Player player;
        private SpriteBatch spriteBatch;
        private SpriteFont font1;
        private SpriteFont font2;
        private Vector2 position;
        private Color color;
        public Rectangle proposedPlayer;

        public ScoreCalculator(Game game,
            SpriteBatch spriteBatch, 
            SpriteFont font1,
            SpriteFont font2,
            Vector2 position,
            Color color,
            Player player,
            Fruits fruit,
            Spider spider) : base(game)

        {
            this.player = player;
            this.fruit = fruit;
            this.spider = spider;
            this.spriteBatch = spriteBatch;
            this.font1 = font1;
            this.font2 = font2;
            this.position = position;
            this.color = color;
            
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font1, "Score: " + player.Score.ToString(), position, Color.White);
            string message = "";
            if (player.Score > 2000)
            {
                message = "You win ! \nEnter ESC to \ncome back to the menu";
                player.startGame = false;           // prevent coninue the game
            }
            else if (player.Score == 0)
            {
                message = "Game Over! \nEnter ESC to \ncome back to the menu";
                player.startGame = false;           // prevent coninue the game
            }
            Vector2 messagePos = new Vector2(50);
            spriteBatch.DrawString(font2, message, messagePos, Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        // check the player to see hit the fruits or block or spider 
        //and call related methods
        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = player.getBounds();
            Rectangle spiderRect = spider.getBounds();

            proposedPlayer = new Rectangle(playerRect.X,
                                                        playerRect.Y,
                                                        playerRect.Width,
                                                        playerRect.Height);
            Rectangle proposedSpider = new Rectangle(spiderRect.X,
                                                        spiderRect.Y,
                                                        spiderRect.Width,
                                                        spiderRect.Height);


            
            Sides collisionSidesSpider = proposedPlayer.CheckCollisions(proposedSpider);
            Sides collisionSidesFruit = proposedPlayer.CheckCollisions(fruit.positionList);

            // if player touch the spider, lose score and play the sound
            if ((collisionSidesSpider & Sides.RIGHT) == Sides.RIGHT || (collisionSidesSpider & Sides.LEFT) == Sides.LEFT || (collisionSidesSpider & Sides.TOP) == Sides.TOP || (collisionSidesSpider & Sides.BOTTOM) == Sides.BOTTOM)
            {
                player.LoseScore();
                player.isBite = true;
                if (player.startGame == true)
                {
                    spider.biteSound.Play();
                }

            }

            //// if player touch the a fruit, add score and play the sound and call the method to remove the fruit
            if ((collisionSidesFruit & Sides.RIGHT) == Sides.RIGHT || (collisionSidesFruit & Sides.LEFT) == Sides.LEFT || (collisionSidesFruit & Sides.TOP) == Sides.TOP || (collisionSidesFruit & Sides.BOTTOM) == Sides.BOTTOM)
            {
                RemoveFruites();               
            }

            base.Update(gameTime);
        }

        // remove fruit and play sound
        public void RemoveFruites()
        {
           
            for (int i = 0; i < fruit.positionList.Count;)
            {
                if (fruit.positionList[i].Intersects(proposedPlayer))
                {
                    fruit.positionList.RemoveAt(i);
                    player.GetScore();
                    fruit.fruitSound.Play();
                }
                else
                {
                    i++;
                   
                }
            }
            
        }

    }
}
