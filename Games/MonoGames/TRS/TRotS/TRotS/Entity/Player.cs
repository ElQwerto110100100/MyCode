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
    class Player : Sprite
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly Texture2D charSheet;
        int moveSpeed = 5;
        int bulletSpeed = 6;
        public int score = 0;
        public int ammunation = 20;

        private Color planeColor = Color.White;

        public List<Sprite> bullets = new List<Sprite>();
        public bool Hit = false;
        private int waitTimer = 0;
        public int health = 3;

        static List<SoundEffect> soundEffects;
        static int occurances = 0;

        public Player(GraphicsDevice graphicsDevice, Texture2D charSheet) : base(graphicsDevice, charSheet)
        {
            this.graphicsDevice = graphicsDevice;
            this.charSheet = charSheet;

            soundEffects = new List<SoundEffect>();
            soundEffects.Add(MouseClass.Instance._content.Load<SoundEffect>("Sounds/Effects/planeGun"));
            SoundEffect.MasterVolume = 1f;

            AddAnimation("movement",100,60,5,0);
            SetAnimation("movement");

            SetPosXY(100, graphicsDevice.Viewport.Height / 2);
        }

        public void UpdatePlayer(GameTime gametime)
        {
            Update(gametime);
            KeyboardState currentKeyState = MouseClass.Instance.GetKeyState();
            KeyboardState previousKeyState = MouseClass.Instance.GetPrevKeyState();

            if (currentKeyState.IsKeyDown(Keys.Down))
            {
                SetPosXY(0, moveSpeed);
            }
            if (currentKeyState.IsKeyDown(Keys.Up))
            {
                SetPosXY(0, -moveSpeed);
            }
            if (currentKeyState.IsKeyDown(Keys.Left))
            {
                SetPosXY(-moveSpeed, 0);
            }
            if (currentKeyState.IsKeyDown(Keys.Right))
            {
                SetPosXY(moveSpeed, 0);
            }

            if (currentKeyState.IsKeyDown(Keys.Space) && !previousKeyState.IsKeyDown(Keys.Space))
            {
                Fire();
            }

            if (Hit)
            {
                if (waitTimer % 5 == 0)
                {
                    planeColor = Color.Red;
                } 
                else
                {
                    planeColor = Color.White;
                }
                waitTimer--;
                if (waitTimer == 0) Hit = false;
            }

            BulletUpdate(gametime);
        }

        public void Fire()
        {
            Sprite newBullet = new Sprite(graphicsDevice, RC_Framework.Util.texFromFile(graphicsDevice, @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\plane-bullet.png"));

            if (ammunation != 0)
            {
                if (occurances != 3)
                {
                    occurances += 1;
                     soundEffects[0].Play();
                    occurances -= 1;
                }
                newBullet.AddAnimation("fire", 28, 12, 1, 0, 0.01, false);
                newBullet.SetAnimation("fire");
                newBullet.SetPosXY(this.PosX + this.sourceRectangle.Width, this.PosY + this.sourceRectangle.Height - 12);
                newBullet.borderOn = this.borderOn;
                bullets.Add(newBullet);
                ammunation -= 1;
            }
        }

        public void BulletUpdate(GameTime gametime) {
            foreach (Sprite bullet in bullets)
            {
                bullet.Update(gametime);
                bullet.PosX += bulletSpeed;
            }
            bullets.RemoveAll(bullet => bullet.PosX > graphicsDevice.Viewport.Width);
        }

        public void PlaneHit()
        {
            Hit = true;
            waitTimer = 60 * 3;
            if (health != 0) health--; //prevents it from going negtive
        }

        public void PlayerDraw(SpriteBatch spriteBatch, SpriteEffects spriteEx = SpriteEffects.None)
        {
            Draw(spriteBatch, spriteEx, planeColor);

            foreach (Sprite bullet in bullets)
            {
                bullet.Draw(spriteBatch, SpriteEffects.None);
            }
        }
    }
}
