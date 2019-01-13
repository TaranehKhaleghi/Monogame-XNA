using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TKhaleghiFinalProject;

namespace AllInOneMono
{
    public class HowToPlay : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D howToPlay;
        public HowToPlay(Game game, SpriteBatch spriteBatch) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;
            howToPlay = g.Content.Load<Texture2D>("Images/HowToPlay");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(howToPlay,new Rectangle(0,0, 1000,750), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
