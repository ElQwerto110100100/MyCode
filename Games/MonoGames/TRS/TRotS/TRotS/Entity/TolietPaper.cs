﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkTut1.Resources;
using Microsoft.Xna.Framework;

namespace TRotS.Entity
{
    class TolietPaper : Sprite
    {
        static public GraphicsDevice GraphicsDevice;
        static public Texture2D CharSheet;
        private int waitTimer = 0;
        private int movementSpeed;
        static private Random rand = new Random();
        public int ammoRefill = 5;

        public TolietPaper(GraphicsDevice graphicsDevice, Texture2D charSheet) : base(graphicsDevice, charSheet)
        {
            GraphicsDevice = graphicsDevice;
            CharSheet = charSheet;

            AddAnimation("movement", 75, 36, 7, 0);
            SetAnimation("movement");
            movementSpeed = rand.Next(7, 10);

            Reset();
        }

        public void TolietPaperUpdate(GameTime gameTime)
        {
            if (waitTimer == 0)
            {
                this.PosX -= movementSpeed;

                if (this.PosX < -80 || this.PosY > GraphicsDevice.Viewport.Height)
                {
                    Reset();
                }
            }
            else
            {
                waitTimer -= 1;
            }

            Update(gameTime);
        }

        public void Reset()
        {
            this.PosX = GraphicsDevice.Viewport.Width + 50;
            this.PosY = rand.Next(0, GraphicsDevice.Viewport.Height - 50);
            waitTimer = rand.Next(300, 500);
        }

        public void TolietPaperDraw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, SpriteEffects.None);
        }
    }
}
