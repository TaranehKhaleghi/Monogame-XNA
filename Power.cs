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

namespace TKhaleghiFinalProject
{
     public class Power : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D power;
        private Vector2 position;
        private Texture2D powerTxt;
        private SoundEffect powerSound;

            public Power(Game game,
            SpriteBatch spriteBatch,
            Texture2D power,
            Vector2 position,
            SoundEffect powerSound
            ) : base(game)
            {
                this.spriteBatch = spriteBatch;
                this.power = power;
                this.position = position;
                this.powerSound = powerSound;
            }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(power, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}