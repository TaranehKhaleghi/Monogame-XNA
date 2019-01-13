using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AllInOneMono;
using C3.XNA;
using Microsoft.Xna.Framework.Audio;

/// <summary>
/// Finanl Project
/// Revision History:
///     Taraneh Khaleghi, 2018-12-01: Created
///     Taraneh Khaleghi, 2018-12-02: UI Designed
///     Taraneh Khaleghi, 2018-12-08: Bug Fixed  
/// </summary>
namespace TKhaleghiFinalProject

{   // class ActionScene inherited from GameScene : actual game 
    public class ActionScene : GameScene
    {
        private SpriteFont font1;
        private SpriteFont font2;
        private SpriteBatch spriteBatch;
        private Texture2D playGround;
        private SoundEffect biteSound;
        private SoundEffect fruitSound;

        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {          
            Game1 g = (Game1)game;
            g.IsMouseVisible = true;
            this.spriteBatch = g.spriteBatch;

           // Load all contents and add to their constructors

            font1 = g.Content.Load<SpriteFont>("Fonts/defaultFont");
            font2 = g.Content.Load<SpriteFont>("Fonts/hilightFont");
            Texture2D block1 = g.Content.Load<Texture2D>("images/block1");
            Texture2D block2 = g.Content.Load<Texture2D>("images/block2");

            playGround = g.Content.Load<Texture2D>("images/background2");      
            PlayGround b = new PlayGround(g, spriteBatch, font1, playGround, block1, block2);
            Components.Add(b);

            Texture2D spiderTxt = g.Content.Load<Texture2D>("images/spider");
            biteSound = g.Content.Load<SoundEffect>("HUD/bite");
            Vector2 spiderPos = new Vector2(650, 590);
            Vector2 speed = new Vector2(1, 0);
            Spider spider = new Spider(g, spriteBatch, spiderTxt, spiderPos, speed, b, biteSound);
            Components.Add(spider);

            Texture2D grapeTxt = g.Content.Load<Texture2D>("images/grape");
            Texture2D cherryTxt = g.Content.Load<Texture2D>("images/cherry");
            Texture2D bananaTxt = g.Content.Load<Texture2D>("images/banana");
            fruitSound = g.Content.Load<SoundEffect>("HUD/powerup");
            Fruits fruit = new Fruits(g, spriteBatch, grapeTxt, cherryTxt, bananaTxt, new Vector2(0), fruitSound);
            Components.Add(fruit);

            Texture2D playerTexture = g.Content.Load<Texture2D>("images/sp");
            Vector2 playerPos = new Vector2(0, 480);
            Player p = new Player(g, spriteBatch, playerTexture, playerPos, b, 50);
            Components.Add(p);

            Vector2 pos = new Vector2(700, 40);
            ScoreCalculator scoreCalculator = new ScoreCalculator(g, spriteBatch, font1, font2, pos, Color.Red, p, fruit, spider);
            Components.Add(scoreCalculator);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        // Draw game components : Grid, points, messages 
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();           
            spriteBatch.Draw(playGround, new Rectangle(0, 0, 1000, 750), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);

        }
   

    }
}
