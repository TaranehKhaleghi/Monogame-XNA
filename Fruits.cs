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
using PROG2370CollisionLibrary;

/// <summary>
///  Finanl Project
///  Revision History:
///     Taraneh Khaleghi, 2018-12-01: Created
///     Taraneh Khaleghi, 2018-12-02: UI Designed
///     Taraneh Khaleghi, 2018-12-08: Bug Fixed  
/// </summary>

namespace TKhaleghiFinalProject
{

    public class Fruits : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Texture2D grapeTxt;
        private Texture2D cherryTxt;
        private Texture2D bananaTxt;
        public SoundEffect fruitSound;

        public List<Texture2D> fruitsList;
       public List<Rectangle> positionList;
       
        public Fruits(Game game,
            SpriteBatch spriteBatch,
            Texture2D grapeTxt,
            Texture2D cherryTxt,
            Texture2D bananaTxt,
            Vector2 position,
            SoundEffect fruitSound
            ) : base(game)
            {
                this.spriteBatch = spriteBatch;
                this.grapeTxt = grapeTxt;
                this.cherryTxt = cherryTxt;
                this.bananaTxt = bananaTxt;
                this.position = position;
                this.fruitSound = fruitSound;

            fruitsList = new List<Texture2D>();
            positionList = new List<Rectangle>();

            //// position of  fruitsList and add to list

            positionList.Add(new Rectangle(900, 410, 64, 64));
            positionList.Add(new Rectangle(700, 400, 64, 64));
            positionList.Add(new Rectangle(500, 380, 64, 64));
            positionList.Add(new Rectangle(300, 300, 64, 64));
            positionList.Add(new Rectangle(50, 350, 64, 64));

            fruitsList.Add(grapeTxt);
            fruitsList.Add(cherryTxt);
            fruitsList.Add(bananaTxt);
            fruitsList.Add(cherryTxt);
            fruitsList.Add(bananaTxt);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //  going through two list for drawing fruits textures and their postions
            for(int i=0; i< positionList.Count;i++)
            {                              
                spriteBatch.Draw(fruitsList[i], positionList[i], Color.White);                
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
       
    }
}