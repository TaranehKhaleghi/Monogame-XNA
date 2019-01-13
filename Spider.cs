using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PROG2370CollisionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Finanl Project
///  Revision History:
///     Taraneh Khaleghi, 2018-12-01: Created
///     Taraneh Khaleghi, 2018-12-02: UI Designed
///     Taraneh Khaleghi, 2018-12-08: Bug Fixed 
/// </summary>
/// 

namespace TKhaleghiFinalProject
{
     
    public class Spider : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D spiderTxt;
        private Vector2 position;
        //private Vector2 speed;
        public SoundEffect biteSound;

         // all sizes is founded from the paint
        const int STANDFRAMEWIDTH = 62;     
        const int STANDFRAMEHEIGHT = 75;
        const int WALKFRAMEWIDTH = 62;
        const int WALKFRAMEHEIGHT = 75;
        const float SCALE = 0.70f;

        const int STANDFRAME = 0;
        const int FIRSTWALKFRAME = 1;
        const int WALKFRAMES = 10;

        const float SPEED = 2.3f;

        private int currentFrame = STANDFRAME;

        int currentFrameDelay = 0;
        const int MAXFRAMEDELAY = 3;

        List<Rectangle> spiderFrames;
        SpriteEffects spriteDirection;

        Vector2 velocity;
        Vector2 speed;
        Rectangle spider;           // containing rectangle
        Rectangle spiderTextureRectangle;  // the size of the raw texture 
        PlayGround playGround;

        public Spider(Game game,
            SpriteBatch spriteBatch,
            Texture2D spiderTxt,
            Vector2 position,
            Vector2 speed,
            PlayGround playGround,
            SoundEffect biteSound
            ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spiderTxt = spiderTxt;
            this.position = position;
            this.speed = speed;
            this.playGround = playGround;
            this.biteSound = biteSound;

            spiderTextureRectangle = new Rectangle((int)position.X, (int)position.Y, 63, 50);       // frame
            spider = new Rectangle((int)position.X, (int)position.Y, (int)(STANDFRAMEWIDTH * SCALE), (int)(STANDFRAMEHEIGHT * SCALE));
            spiderFrames = new List<Rectangle>();

            //add the stand frame
            spiderFrames.Add(new Rectangle(1, 1, STANDFRAMEWIDTH, STANDFRAMEHEIGHT));


            //the walk frames
            spiderFrames.Add(new Rectangle(1, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            spiderFrames.Add(new Rectangle(65, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            spiderFrames.Add(new Rectangle(128, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            spiderFrames.Add(new Rectangle(192, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            spiderFrames.Add(new Rectangle(256, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            spiderFrames.Add(new Rectangle(319, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            spiderFrames.Add(new Rectangle(383, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            spiderFrames.Add(new Rectangle(447, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            spiderFrames.Add(new Rectangle(511, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            spiderFrames.Add(new Rectangle(575, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));


            spriteDirection = SpriteEffects.None;

            velocity = new Vector2(0);

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(spiderTxt,
                    spider,     //containing rectangle
                    spiderFrames.ElementAt<Rectangle>(currentFrame),     // key frame rectangle
                    Color.White,
                    0f,             //rotation
                    new Vector2(0),     // no change to origin
                    spriteDirection,
                    0f);
            //spriteBatch.DrawRectangle(player, Color.Yellow);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // it moves the spider in the certain area
        public override void Update(GameTime gameTime)
        {

            position += speed;

            if (position.X > 750)
            {
                speed = -speed;
                spriteDirection = SpriteEffects.FlipHorizontally;
            }
            if (position.X < 650)
            {
                spriteDirection = SpriteEffects.None;
                 speed = -speed;
            }

            spider.X = spider.X + (int)speed.X;  
            spider.Y = spider.Y + (int)speed.Y;
            position = new Vector2(spider.X, spider.Y);

            
            currentFrameDelay++;
            if (currentFrameDelay > MAXFRAMEDELAY)
            {
                currentFrameDelay = 0;
                currentFrame++;
            }
            if (currentFrame > WALKFRAMES)
                currentFrame = FIRSTWALKFRAME;

            base.Update(gameTime);
        }

        private bool nearlyZero(float f1)
        {
            return (Math.Abs(f1) < float.Epsilon);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, spider.Width, spider.Height);
        }
    }
}