using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarkTut1.Resources;

namespace TRotS.Entity
{
    class Ammo : Sprite
    {
        static public GraphicsDevice GraphicsDevice;
        static public Texture2D CharSheet;
        private int waitTimer = 0;
        private int movementSpeed;
        static private Random rand = new Random();
        public int ammoRefill = 5;

        public Ammo(GraphicsDevice graphicsDevice, Texture2D charSheet) : base(graphicsDevice, charSheet)
        {
            GraphicsDevice = graphicsDevice;
            CharSheet = charSheet;

            AddAnimation("movement", 45, 45, 1, 0);
            SetAnimation("movement");
            movementSpeed = rand.Next(5, 7);

            Reset();
        }

        public void AmmoUpdate(GameTime gameTime)
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

        public void EnemyDraw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, SpriteEffects.None);
        }
    }
}
