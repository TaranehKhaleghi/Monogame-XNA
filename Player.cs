using C3.XNA;
using Microsoft.Xna.Framework;
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

namespace TKhaleghiFinalProject
{
    public class Player : DrawableGameComponent
    {

        const int STANDFRAMEWIDTH = 135;     //all values id founded from paint
        const int STANDFRAMEHEIGHT = 234;
        const int WALKFRAMEWIDTH = 135;
        const int WALKFRAMEHEIGHT = 234;
        const int JUMPFRAMEWIDTH = 135;
        const int JUMPFRAMEHEIGHT = 234;
        const int BITEFRAMEWIDTH = 135;
        const int BITEFRAMEHEIGHT = 234;
        const float SCALE = 0.70f;

        const int STANDFRAME = 0;
        const int FIRSTWALKFRAME = 1;
        const int WALKFRAMES = 5;
        const int JUMPFRAME = 6;
        const int BITEFRAME = 7;

        private int currentFrame = STANDFRAME;

        int currentFrameDelay = 0;
        const int MAXFRAMEDELAY = 3;

        List<Rectangle> playerFrames;
        SpriteEffects spriteDirection;

        const float SPEED = 2.3f;
        const float GRAVITY = 0.02f;

        bool isJumping = false;
        bool isGrounded = false;
        public bool isBite = false;
        public bool startGame = true;

        const int JUMPPOWER = -11;
        int currentJumpPower = 0;
        const float JUMPSTEP = 1.3f;

        Vector2 velocity;
        Vector2 position;

        SpriteBatch spriteBatch;
        Texture2D playerTexture;

        public Rectangle player;           // containing rectangle
        PlayGround playGround;
 
        private int score;
        public int Score { get { return score; } }

        public Player(Game game, SpriteBatch spriteBatch, Texture2D playerTexture, Vector2 position, PlayGround playGround, int score): base(game)
        {
            this.spriteBatch = spriteBatch;
            this.playerTexture = playerTexture;
            this.position = position;
            this.playGround = playGround;
            this.score = score;

            player = new Rectangle((int)position.X, (int)position.Y, (int)(STANDFRAMEWIDTH * SCALE), (int)(STANDFRAMEHEIGHT * SCALE));

            playerFrames = new List<Rectangle>();

            //add the stand frame
            playerFrames.Add(new Rectangle(284, 1, STANDFRAMEWIDTH, STANDFRAMEHEIGHT));

            //the walk frames
            playerFrames.Add(new Rectangle(1, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(144, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(286,1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(428, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(568, 1, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));

            //the jump frame
            playerFrames.Add(new Rectangle(862, 1, JUMPFRAMEWIDTH, JUMPFRAMEHEIGHT));
            // the bite frame
            playerFrames.Add(new Rectangle(708, 1, BITEFRAMEWIDTH, BITEFRAMEHEIGHT));

            spriteDirection = SpriteEffects.None;

            velocity = new Vector2(0);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(playerTexture,
                    player,     //containing rectangle
                    playerFrames.ElementAt<Rectangle>(currentFrame),     // key frame rectangle
                    Color.White,
                    0f,             //rotation
                    new Vector2(0),     // no change to origin
                    spriteDirection,
                    0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {

            velocity.X = 0;     //always evaluate from standing

            float deltaTime = (float)gameTime.ElapsedGameTime.Milliseconds;
            velocity.Y += deltaTime * GRAVITY;   // enable/disable gravity

            if (startGame)
            {
                KeyboardState keyState = Keyboard.GetState();

                if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
                    velocity.X = SPEED;

                if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
                    velocity.X = -SPEED;

                if (keyState.IsKeyDown(Keys.Space))
                {
                    if (!isJumping && isGrounded)  //not already jumping, and not falling
                    {
                        isJumping = true;
                        isGrounded = false;
                        currentJumpPower = JUMPPOWER;
                    }
                }

                if (isJumping)
                {
                    if (currentJumpPower < 0)
                    {
                        velocity.Y -= JUMPSTEP;
                        currentJumpPower++;
                    }
                    else
                    {
                        isJumping = false; //falling
                    }
                }

                int newWidth = (player.Width) - 20;
                // where we plan to go:
                Rectangle proposedPlayer = new Rectangle(player.X + (int)velocity.X,
                                                            player.Y + (int)velocity.Y,
                                                            newWidth,
                                                            player.Height);
                // will plan work?
                Sides collisionSides = proposedPlayer.CheckCollisions(playGround.RigidBodyList);

                if ((collisionSides & Sides.RIGHT) == Sides.RIGHT)
                    if (velocity.X > 0)
                        velocity.X = 0;

                if ((collisionSides & Sides.LEFT) == Sides.LEFT)
                    if (velocity.X < 0)
                        velocity.X = 0;

                if ((collisionSides & Sides.TOP) == Sides.TOP)
                    if (velocity.Y < 0)
                        velocity.Y = 0;

                if ((collisionSides & Sides.BOTTOM) == Sides.BOTTOM && (currentJumpPower != JUMPPOWER))
                {
                    velocity.Y = 0;
                    isGrounded = true;
                }

                player.X = player.X + (int)velocity.X;
                player.Y = player.Y + (int)velocity.Y;
                position = new Vector2(player.X, player.Y);

                //anim start
                //walking
                if (velocity.X > 0)          // face the right direction
                {
                    spriteDirection = SpriteEffects.None;
                }
                else if (velocity.X < 0)
                {
                    spriteDirection = SpriteEffects.FlipHorizontally;
                }

                if (isGrounded)      // player on ground
                {
                    if (nearlyZero(velocity.X))         // and not moving
                    {
                        currentFrame = STANDFRAME;
                    }
                    else                                // not not-moving.
                    {
                        currentFrameDelay++;
                        if (currentFrameDelay > MAXFRAMEDELAY)
                        {
                            currentFrameDelay = 0;
                            currentFrame++;
                        }
                        if (currentFrame > WALKFRAMES)
                            currentFrame = FIRSTWALKFRAME;
                    }
                }

                //jumping
                if (isJumping)
                {
                    currentFrame = JUMPFRAME;

                }
                // bite
                if (isBite)
                {
                    currentFrame = BITEFRAME;
                    isBite = false;
                }


                //anim end
            }

            base.Update(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, player.Width, player.Height);
        }

        private bool nearlyZero(float f1)
        {
            return (Math.Abs(f1) < float.Epsilon);
        }

        // if game is not over add score everytime player hit the fruits rectangle
        public void GetScore()
        {
            if (startGame)
            {
                score += 500;
            }
        }

        //if game is not over, it reduce the score when spider hit the player's rectangle
        public void LoseScore()
        {
            if (startGame)
            {
                score -= 10;
            } 
        }
    }
}
