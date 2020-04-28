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
    class Enemy : Sprite
    {
        static public GraphicsDevice GraphicsDevice;
        static public Texture2D CharSheet;
        static private Random rand = new Random();
        private Sprite Exsplosion;
        private int movementSpeed;
        private int fallSpeed = 0;
        private int waitTimer = 0;

        public bool Hit { get; private set; }

        static List<SoundEffect> soundEffects;
        static int occurances = 0;

        public Enemy(GraphicsDevice graphicsDevice, Texture2D charSheet) : base(graphicsDevice, charSheet)
        {
            GraphicsDevice = graphicsDevice;
            CharSheet = charSheet;

            soundEffects = new List<SoundEffect>();
            soundEffects.Add(MouseClass.Instance._content.Load<SoundEffect>("Exsplosion"));
            SoundEffect.MasterVolume = 0.01f;
            AddAnimation("movement", 78, 78, 4, 0);
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
                    Reset(this);
                }
            }
            else
            {
                waitTimer -= 1;
            }

            if (Exsplosion != null)
            {
                if (Exsplosion.CurrentAnimation.stop)
                {
                    Exsplosion = null;
                }
                else
                {
                    Exsplosion.Update(gameTime);
                }
            }
        }

        public void Damage(List<Sprite> bullets)
        {
            foreach (Sprite bullet in bullets)
            {
                if (this.tempRect.Intersects(bullet.tempRect)) {
                    Exsplosed();
                    Reset(this);
                }
            }
        }

        public void Reset(Enemy enemy)
        {
            enemy.PosX = GraphicsDevice.Viewport.Width + 50;
            enemy.PosY = rand.Next(0, GraphicsDevice.Viewport.Height - 50);
            waitTimer = rand.Next(20, 100);
        }

        public void Exsplosed()
        {
            Exsplosion = new Sprite(GraphicsDevice, CharSheet);
            Exsplosion.AddAnimation("explosion", 78, 78, 4, 1, 0.1, true);
            Exsplosion.SetAnimation("explosion");
            Exsplosion.PosX = this.PosX;
            Exsplosion.PosY = this.PosY;

            if (occurances != 3)
            {
                occurances += 1;
                soundEffects[0].Play();
                occurances -= 1;
            }
        }

        public void EnemyDraw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, SpriteEffects.None);
            if (Exsplosion != null)
            {
                Exsplosion.Draw(spriteBatch, SpriteEffects.None);
            }
        }
    }
}
