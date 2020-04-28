using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarkTut1.Resources;
using Microsoft.Xna.Framework.Audio;

namespace TRotS.Entity
{
    class SpikeBall : Sprite
    {
        static public GraphicsDevice GraphicsDevice;
        static public Texture2D CharSheet;
        static private Random rand = new Random();
        private int movementSpeed;
        private int fallSpeed = 0;
        private int waitTimer = 0;
        private Color ballColor = Color.White;

        public bool Hit { get; private set; }

        static List<SoundEffect> soundEffects;
        static int occurances = 0;

        public SpikeBall(GraphicsDevice graphicsDevice, Texture2D charSheet) : base(graphicsDevice, charSheet)
        {
            GraphicsDevice = graphicsDevice;
            CharSheet = charSheet;

            AddAnimation("movement", 96, 96, 7, 0);
            SetAnimation("movement");
            this.PosX = GraphicsDevice.Viewport.Width + 50;
            this.PosY = rand.Next(0, GraphicsDevice.Viewport.Height - 50);
            movementSpeed = rand.Next(6, 10);
        }

        public void EnemyUpdate(GameTime gameTime)
        {
            Update(gameTime);

            if (waitTimer == 0)
            {
                this.PosX -= movementSpeed;
                this.PosY += fallSpeed;

                if (this.PosX < -80 || this.PosY > GraphicsDevice.Viewport.Height)
                {
                    Reset();
                }
            }
            else
            {
                waitTimer -= 1;
            }
        }

        public void Damage(List<Sprite> bullets)
        {
            foreach (Sprite bullet in bullets)
            {
                if (this.tempRect.Intersects(bullet.tempRect))
                {
                    Reset();
                }
            }
        }

        public void Reset()
        {
            PosX = GraphicsDevice.Viewport.Width + 50;
            PosY = rand.Next(0, GraphicsDevice.Viewport.Height - 50);
            waitTimer = rand.Next(20, 100);
        }

        public void SpikeBallDraw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, SpriteEffects.None, ballColor);
        }
    }
}
