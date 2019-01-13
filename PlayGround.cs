using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TKhaleghiFinalProject
{
    /// <summary>
    /// Finanl Project
    ///  Revision History:
    ///     Taraneh Khaleghi, 2018-12-01: Created
    ///     Taraneh Khaleghi, 2018-12-02: UI Designed
    ///     Taraneh Khaleghi, 2018-12-08: Bug Fixed  
    /// </summary>
    /// 
     
     // A class for creating background of the game
    public class PlayGround : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;
        SpriteFont spriteFont1;
        Texture2D block1;
        Texture2D block2;

        List<Rectangle> rigidBodyList;
        public List<Rectangle> RigidBodyList { get => rigidBodyList; }
        public PlayGround(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont1, Texture2D backgroundTexture, Texture2D block1, Texture2D block2) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.backgroundTexture = backgroundTexture;
            this.spriteFont1 = spriteFont1;
            this.block1 = block1;
            this.block2 = block2;

            rigidBodyList = new List<Rectangle>();

            //// position of  rigidbodies and add to list
            
            rigidBodyList.Add(new Rectangle(0, 650, 1000, 20)); // rectangle that is hidden under the player
            rigidBodyList.Add(new Rectangle(500, 550, 30, 100));
            rigidBodyList.Add(new Rectangle(200, 600, 51, 51));

        }

        // draw the wood blocks and the backgroung image
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 1000, 750), Color.White);
            spriteBatch.Draw(block1, rigidBodyList[1], Color.White);
            spriteBatch.Draw(block2, rigidBodyList[2], Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}