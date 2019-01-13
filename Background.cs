using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using C3.XNA;
/// <summary>
///  Finanl Project
/// Revision History:
///     Taraneh Khaleghi, 2018-12-01: Created
///     Taraneh Khaleghi, 2018-12-02: UI Designed
///     Taraneh Khaleghi, 2018-12-08: Bug Fixed  
/// </summary>
namespace TKhaleghiFinalProject

{   //A class for drawing background texture and components
    class Background : DrawableGameComponent
    {
        
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;
        SpriteFont spriteFont1;
        SpriteFont spriteFont2;
        SpriteFont spriteFont3;
 
        // constructor of background
        public Background(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont1, SpriteFont spriteFont2, SpriteFont spriteFont3, Texture2D backgroundTexture) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.backgroundTexture = backgroundTexture;
            this.spriteFont1 = spriteFont1;
            this.spriteFont2 = spriteFont2;
            this.spriteFont3 = spriteFont3;           

        }

        //Draw all background components
        public override void Draw(GameTime gameTime)
        {   
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 1000, 750), Color.White);
            spriteBatch.DrawString(spriteFont2, "Runner", new Vector2(400, 20), Color.Black);
            spriteBatch.DrawString(spriteFont3, "Design and Programming: \nTaraneh Khaleghi-2018", new Vector2(700, 690), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}