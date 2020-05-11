using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarkTut1.Resources
{
    class Sprite
    {
        public List<Animations>animations = new List<Animations>();
        public Texture2D SheetTexture { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Scale { get; set; } = 1;

        public Animations CurrentAnimation { get; set; }
        public GraphicsDevice GraphicsDevice { get; }

        public Rectangle sourceRectangle;
        public Rectangle tempRect;
        static public bool borderOn = false;

        public string Direction { get; set; }

        static public Random rand = new Random();
        //set a charctersheet for sprite
        public Sprite(GraphicsDevice graphicsDevice, Texture2D charSheet)
        {
            if (SheetTexture == null)
            {
                {
                    SheetTexture = charSheet;
                }
            }
            GraphicsDevice = graphicsDevice;
            
        }

        public void SetPosXY(int x, int y, int extraX = 0, int extraY = 0)
        {
            if (GraphicsDevice.ScissorRectangle.Contains(new Rectangle(this.PosX + x + extraX, this.PosY + y + extraY, sourceRectangle.Width, sourceRectangle.Height)))
            {
                PosX += x;
                PosY += y;
            }
        }

        //add animations to the sprite
        public void AddAnimation(string animeName, int frameX, int frameY, double numOfFrames, int animationLine, double timeSpaceing = .15, bool playOnce = false)
        {
            Animations newAnime = new Animations(animeName);
            //if it has no animation just add it as only one frame
            if (numOfFrames == 1)
            {
                newAnime.AddFrame(new Rectangle((frameX * 0), (animationLine * 0), frameX, frameY), TimeSpan.FromSeconds(timeSpaceing), false);
                animations.Add(newAnime);
            }
            else
            {
                for (int cnt = 1; cnt < numOfFrames; cnt++)
                {
                    newAnime.AddFrame(new Rectangle((frameX * cnt), (animationLine * frameY), frameX, frameY), TimeSpan.FromSeconds(timeSpaceing), false);
                }
                if (playOnce)
                {
                    newAnime.AddFrame(new Rectangle(0, 0, 0, 0), TimeSpan.FromSeconds(timeSpaceing), playOnce);
                    animations.Add(newAnime);
                }
                animations.Add(newAnime);
            }

        }

        //give the set of animations for this sprite enitity
        public List<Animations> GetAnimations()
        {
            return animations;
        }

        public void SetSourceRectangle(Rectangle newRect)
        {
            sourceRectangle = newRect;
        }

        public void SetAnimation(string anName)
        {
            Animations entityAnimation = GetAnimations().Find(x => x.AniamtionName.Contains(anName));
            CurrentAnimation = entityAnimation;
        }

        public void Update(GameTime gameTime)
        {
            CurrentAnimation.Update(gameTime);
            sourceRectangle = CurrentAnimation.CurrentRectangle;

            tempRect = GetSpriteRec();
        }

        public Rectangle GetSpriteRec()
        {
            return new Rectangle(this.PosX, this.PosY, sourceRectangle.Width, sourceRectangle.Height);
        }

        public Rectangle GetRectBorder()
        {
            return sourceRectangle;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEx, Color? spriteColor = null)
        {
            //only draw it if its on screen, should help with lag iissues
            if (GraphicsDevice.ScissorRectangle.Intersects(tempRect))
            {
                //since all entitys use the sprite calss it will be easier to attach it here
                if (borderOn) RC_Framework.LineBatch.drawLineRectangle(spriteBatch, tempRect, Color.Red);

                Vector2 topLeftOfSprite = new Vector2(this.PosX, this.PosY);
                //spriteBatch.Draw(SheetTexture, topLeftOfSprite, null ,sourceRectangle, spriteColor, spriteEx, 0);
                spriteBatch.Draw(SheetTexture, position: topLeftOfSprite, sourceRectangle: sourceRectangle, color: spriteColor, rotation: 0f, origin: null, scale: null, effects: spriteEx, layerDepth: 0f);

            }
        }
    }
}
